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
using Microsoft.Phone.Controls;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace SystemMonitoring.Views
{
    public partial class PassingWorks : PhoneApplicationPage
    {
        public PassingView PassingView = new PassingView();
        public double popupHeight = 700.0;
        public PassingWorks()
        {
            InitializeComponent();
            PassingView.VerticalAlignment = VerticalAlignment.Top;
            PassingView.Visibility = Visibility.Collapsed;
            PassingView.Margin = new Thickness(0, 0, 0, 0);
            PassingView.Height = 0;
        }

        public int DisciplineId { get; private set; }

        private static void NewStoryboardHeight(FrameworkElement frameworkElement, double From, double To, Action action = null)
        {
            DoubleAnimation dbheight = new DoubleAnimation
            {
                From = From,
                To = To,
                Duration = new Duration(TimeSpan.FromMilliseconds(500))
            };
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(dbheight);
            Storyboard.SetTarget(dbheight, frameworkElement);
            Storyboard.SetTargetProperty(dbheight, new PropertyPath(HeightProperty));
            if (action != null)
                storyboard.Completed += delegate { action(); };
            storyboard.Begin();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var disciplineID = int.Parse(e.Uri.ToString().Substring(e.Uri.ToString().IndexOf('?')).Split('=')[1]);
            DisciplineId = disciplineID;
            var group = Model.Model.Current.DisciplinesGroupses.Where(q => q.DisciplineID == disciplineID).Select(a => a._Group).ToArray();
            //  LayoutRoot.DataContext = group;
            //  var works =
            //       Model.Model.Current.DisciplinesTeachersTypeWorks.Where(
            //           q => q._DisciplinesTeachers._Discipline.ID == disciplineID).SelectMany(q => q._Works).ToArray();
            //   Model.ModelStatic.ModelStaticCurrent.Works = works;
        }

        private void passingList_Loaded(object sender, RoutedEventArgs e)
        {
            var listBox = sender as ItemsControl;
            var d = listBox.DataContext as Model.Model.Work;
            var passingWorks = Model.Model.Current.PassingWorks.Where(q => q.WorkID == d.ID).ToArray();
            var disc = Model.Model.Current.Disciplines.Single(q => q.ID == DisciplineId);
            var students = disc._Groups.SelectMany(q => q._Students).ToArray();
            var items = students.Select(
                q => new PassingViewItem
                {
                    Laba = passingWorks.SingleOrDefault(
                    a => a.WorkID == q.ID && a._HistoryStudent.StudentId == q.ID) == null ? null : passingWorks.Single(a => a.WorkID == q.ID && a._HistoryStudent.StudentId == q.ID),
                    Student = q,
                    Work = d,
                    HistoryStudentID = q._CurrentHistoryStudent.ID
                }).ToArray();

            listBox.ItemsSource = items;
        }
        private bool isOpen = false;
        
        
        private void pivot_Loaded(object sender, RoutedEventArgs e)
        {
            var works =
                Model.Model.Current.Works.Where(
                    q => q._DisciplinesTeachersTypeWork._DisciplinesTeachers.DisciplineID == DisciplineId).ToArray();
            pivot.ItemsSource = works;

        }

        private void passingwork_tap(object sender, GestureEventArgs e)
        {

            if (isOpen)
            {
                NewStoryboardHeight(PassingView, popupHeight, 0.0, () =>
                                                                       {
                                                                           (PassingView.Parent as StackPanel).Children.Remove(PassingView);
                                                                           PassingView.Visibility = Visibility.Collapsed;
                                                                       });
                isOpen = false;
            }
            else
            {
                isOpen = true;
                var s = sender as StackPanel;
                var parent = VisualTreeHelper.GetParent(s) as StackPanel;
                parent.Children.Add(PassingView);
                PassingView.Visibility = Visibility.Visible;
                NewStoryboardHeight(PassingView, 0.0, popupHeight);
                PassingView.DataContext = s.DataContext as PassingViewItem;
            }
        }

    }
}