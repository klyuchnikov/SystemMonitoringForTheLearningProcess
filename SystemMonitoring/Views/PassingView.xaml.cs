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

namespace SystemMonitoring.Views
{
    public partial class PassingView : UserControl
    {
        public PassingView()
        {
            InitializeComponent();
        }
        private void CreateLaba()
        {
            var dc = (this.DataContext as PassingViewItem);
            if (dc.Laba != null) return;
            var laba = Model.Model.PassingWork.AddPassingWork(dc.HistoryStudentID, dc.Work.ID);
            dc.Laba = laba;
        }
        private void grade_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (grade.Value == 0.0)
                return;
            CreateLaba();
        }

        private void Progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Progress.Value == 0.0)
                return;
            IsPassed.IsChecked = Progress.Value == Progress.Maximum;
            var dc = (this.DataContext as PassingViewItem);
            CreateLaba();
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void IsPassed_Checked(object sender, RoutedEventArgs e)
        {
            if (IsPassed.IsChecked == true)
            {
                CreateLaba();
                var dc = (this.DataContext as PassingViewItem);
                dc.Laba.DateEnd = DateTime.Now.Date;
                dc.Laba.Progress = 100;
                dc.Laba.IsPassed = true;
                Client.Current.Save();
            }
        }

        private void Raiting_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Raiting.Value == 0.0)
                return;
            CreateLaba();
        }
    }
}
