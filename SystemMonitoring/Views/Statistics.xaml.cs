using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;
using System.ComponentModel;

namespace SystemMonitoring.Views
{
    public partial class Statistics : PhoneApplicationPage
    {
        public double Offsettt { get; set; }
        public PassingView PassingView = new PassingView();
        public double popupHeight = 560.0;
        public Statistics()
        {
            InitializeComponent();
            listTB.Clear();
            Grid.SetColumnSpan(PassingView, 2);
            ScrollGrid.Children.Add(PassingView);
            PassingView.VerticalAlignment = VerticalAlignment.Top;
            PassingView.Visibility = Visibility.Collapsed;
            //   PassingView.Margin = new Thickness(0, 200, 0, 0);
            PassingView.Height = 0;
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var disciplineID = int.Parse(e.Uri.ToString().Substring(e.Uri.ToString().IndexOf('?')).Split('=')[1]);
            var group = Model.Model.Current.DisciplinesGroupses.Where(q => q.DisciplineID == disciplineID).Select(a => a._Group).ToArray();
            LayoutRoot.DataContext = group;
            var works =
                Model.Model.Current.DisciplinesTeachersTypeWorks.Where(
                    q => q._DisciplinesTeachers._Discipline.ID == disciplineID).SelectMany(q => q._Works).ToArray();
            Model.ModelStatic.ModelStaticCurrent.Works = works;
            if (works.Length == 0)
                EmptyText.Visibility = Visibility.Visible;
        }

        private void TextBlock_Tap_1(object sender, GestureEventArgs e)
        {
            if (popupHeight == 0.0)
                popupHeight = PassingView.LayoutRoot.ActualHeight;
            var s = sender as ToggleButton;
            if (isOpen)
            {
                var em = listTB.Single(a => a.Height == popupHeight + 73);
                foreach (var ToggleButton in listTB)
                    ToggleButton.IsChecked = false;
                NewStoryboardHeight(em, popupHeight, 75.0);
                NewStoryboardHeight(LastSelect, popupHeight, 50.0);
                NewStoryboardHeight(PassingView, popupHeight, 0, () => { PassingView.Visibility = Visibility.Collapsed; });
                isOpen = false;
            }
            else
            {
                isOpen = true;
                var stSelect = listStudents.Single(a => (int)a.Tag == (s.DataContext as PassingViewItem).StudentID);
                var addedThickness =
                    (LayoutRoot.DataContext as Model.Model.Group[]).ToList().IndexOf(
                        (stSelect.DataContext as Model.Model.Student)._CurrentHistoryStudent._Group) * 60;
                PassingView.Margin = new Thickness(0, (listStudents.IndexOf(stSelect) + 2) * 50 + addedThickness + 20, 0, 0);
                PassingView.Visibility = Visibility.Visible;
                LastSelect = stSelect;
                workIDCurrent = (s.DataContext as PassingViewItem).Work.ID;
                NewStoryboardHeight(s, 50.0, popupHeight + 73);
                NewStoryboardHeight(LastSelect, 50.0, popupHeight + 50);
                NewStoryboardHeight(PassingView, 0, popupHeight);
                PassingView.DataContext = s.DataContext as PassingViewItem;
            }
        }

        private bool isOpen = false;
        private int workIDCurrent;
        private static void NewStoryboardHeight(FrameworkElement frameworkElement, double From, double To, Action action = null)
        {
            DoubleAnimation dbheight = new DoubleAnimation
                                           {
                                               From = From,
                                               To = To,
                                               Duration = new Duration(TimeSpan.FromMilliseconds(300))
                                           };
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(dbheight);
            Storyboard.SetTarget(dbheight, frameworkElement);
            Storyboard.SetTargetProperty(dbheight, new PropertyPath(HeightProperty));
            if (action != null)
                storyboard.Completed += delegate { action(); };
            storyboard.Begin();
        }

        private List<ToggleButton> listTB = new List<ToggleButton>();
        private List<StackPanel> listStudents = new List<StackPanel>();
        private StackPanel LastSelect;
        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            listTB.Add(sender as ToggleButton);
            if (!isOpen) return;
            var item = ((sender as FrameworkElement).DataContext as PassingViewItem);
            if (item.StudentID == (int)LastSelect.Tag && item.Work.ID == workIDCurrent)
                (sender as FrameworkElement).Height = popupHeight;
        }

        private void ItemsControl_Loaded(object sender, RoutedEventArgs e)
        {
            var st = (sender as FrameworkElement).DataContext as Model.Model.Student;
            var passingWorks = st._CurrentHistoryStudent._PassingWorks;
            var items = Model.ModelStatic.ModelStaticCurrent.Works.Select(
                q => new PassingViewItem
                {
                    Laba = passingWorks.SingleOrDefault(a => a.WorkID == q.ID) == null ? null : passingWorks.Single(a => a.WorkID == q.ID),
                    StudentID = st.ID,
                    Work = q,
                    HistoryStudentID = st._CurrentHistoryStudent.ID
                }).ToArray();
            (sender as ItemsControl).ItemsSource = items;
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            listStudents.Add(sender as StackPanel);
            if (!isOpen) return;
            if ((int)(sender as StackPanel).Tag == (int)LastSelect.Tag)
                (sender as StackPanel).Height = popupHeight;
        }

        private void listPassing_Unloaded(object sender, RoutedEventArgs e)
        {
            listTB.Clear();
        }

        private void listStudents_Unloaded(object sender, RoutedEventArgs e)
        {
            listStudents.Clear();
        }
    }

    public class PassingViewItem : INotifyPropertyChanged, IEquatable<PassingViewItem>
    {
        private static int count;
        private int id;
        public PassingViewItem()
        {
            id = count++;
        }

        private Model.Model.PassingWork passingWork;
        public Model.Model.PassingWork Laba
        {
            get { return passingWork; }
            set
            {
                passingWork = value;
                NotifyPropertyChanged("Laba");
            }
        }

        public int StudentID { get; set; }
        public Model.Model.Work Work { get; set; }
        public int HistoryStudentID { get; set; }
        public Model.Model.Student Student { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool Equals(PassingViewItem other)
        {
            return this.id == other.id;
        }
    }
}