using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace SystemMonitoring.Views
{
    public partial class LecturesView : PhoneApplicationPage
    {
        public LecturesView()
        {
            InitializeComponent();
             listTB.Clear();
        }
        public double Offsettt { get; set; }
        public PassingView PassingView = new PassingView();
        public double popupHeight = 560.0;
      
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (Model.ModelStatic.ModelStaticCurrent.Works.Length == 0)
                EmptyText.Visibility = Visibility.Visible;
        }
        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            Client.Current.Save();
        }

        private bool isOpen = false;
        private int workIDCurrent;
       
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

    public class PresenceViewItem : INotifyPropertyChanged, IEquatable<PresenceViewItem>
    {
        private static int count;
        private int id;
        public PresenceViewItem()
        {
            id = count++;
        }

        private Model.Model.AttendingLectures attendingLectures;
        public Model.Model.AttendingLectures AttendingLectures
        {
            get { return attendingLectures; }
            set
            {
                attendingLectures = value;
                NotifyPropertyChanged("AttendingLectures");
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

        public bool Equals(PresenceViewItem other)
        {
            return this.id == other.id;
        }
    }
}