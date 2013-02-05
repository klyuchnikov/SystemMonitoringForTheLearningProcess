using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace SystemMonitoring.Views
{
    public partial class Disciplines : PhoneApplicationPage
    {
        public Disciplines()
        {
            InitializeComponent();
            Client.Current.MainPage = this;
            var progressIndicator = SystemTray.GetProgressIndicator(this);
            if (progressIndicator == null)
            {
                progressIndicator = new ProgressIndicator() { IsVisible = true };
                SystemTray.SetProgressIndicator(this, progressIndicator);
            }
            var dt = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            progressIndicator.IsIndeterminate = true;
            dt.Tick += delegate
            {
                if (Client.Current.IsAuthorized)
                {
                    progressIndicator.IsIndeterminate = false;
                    dt.Stop();
                }
                if (Client.Current.IsFirstConnected)
                    progressIndicator.IsIndeterminate = false;
            };
            dt.Start();
        }

        private void Navigate_Students(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Students.xaml", UriKind.Relative));
        }

        private void Navigate_Statistics(object sender, EventArgs e)
        {
            var d = (PanoramaDisc.ItemsSource as Model.Model.DisciplinesTeachers[])[PanoramaDisc.SelectedIndex];
            NavigationService.Navigate(new Uri("/Views/Statistics.xaml?ID=" + d._Discipline.ID, UriKind.Relative));
        }

        private void Navigate_Statistics2(object sender, EventArgs e)
        {
            var d = (PanoramaDisc.ItemsSource as Model.Model.DisciplinesTeachers[])[PanoramaDisc.SelectedIndex];
            NavigationService.Navigate(new Uri("/Views/PassingWorks.xaml?ID=" + d._Discipline.ID, UriKind.Relative));
        }

        private void Navigate_Update(object sender, EventArgs e)
        {
            Client.Current.GetStartData();
        }

        private void NewLection_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Work_Click(object sender, GestureEventArgs e)
        {
            var work = (sender as FrameworkElement).DataContext as Model.Model.Work;
            if (work._DisciplinesTeachersTypeWork._TypeWork.Name == "Лекция")
            {
                NavigationService.Navigate(new Uri("/Views/Lecture.xaml?ID=" + work.ID, UriKind.Relative));
            }
        }

        private void CheckBoxEx_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            var st = (checkBox.Parent as StackPanel).Children[1] as FrameworkElement;
            var height = (checkBox.DataContext as Model.Model.Work)._DisciplinesTeachersTypeWork._TypeWork.Name ==
                         "Лекция"
                             ? 220
                             : 150;
            App.NewStoryboardHeight(st, 0, height);
        }
    }
}