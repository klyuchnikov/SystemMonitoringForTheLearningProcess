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

namespace SystemMonitoring.Views
{
    public partial class PassingGrid : UserControl
    {
        public PassingGrid()
        {
            InitializeComponent();
        }

        public double HeightRow
        {
            get
            {
                return (double)GetValue(HeightRowProperty);
            }
            set
            {
                SetValue(HeightRowProperty, value);
            }
        }

        public static readonly DependencyProperty HeightRowProperty =
            DependencyProperty.Register(
                "HeightRow",
                typeof(double),
                typeof(PassingGrid),
                new PropertyMetadata(OnHeightRowPropertyChanged)
            );

        static void OnHeightRowPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PassingGrid)obj).OnHeightRowPropertyChanged(e);
        }

        private void OnHeightRowPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        public double WidthRow
        {
            get
            {
                return (double)GetValue(WidthRowProperty);
            }
            set
            {
                SetValue(WidthRowProperty, value);
            }
        }

        public static readonly DependencyProperty WidthRowProperty =
            DependencyProperty.Register(
                "WidthRow",
                typeof(double),
                typeof(PassingGrid),
                new PropertyMetadata(OnWidthRowPropertyChanged)
            );

        static void OnWidthRowPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PassingGrid)obj).OnWidthRowPropertyChanged(e);
        }

        private void OnWidthRowPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var students = this.DataContext as Model.Model.Student[];
          //  this.LayoutRoot.ShowGridLines = true;
            foreach (var work in Model.ModelStatic.ModelStaticCurrent.Works)
            {
                this.LayoutRoot.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(this.WidthRow) });
            }
            for (int k = 0; k < students.Length; k++)
            {
                var student = students[k];
                this.LayoutRoot.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(this.HeightRow) });

                var passingWorks = student._CurrentHistoryStudent._PassingWorks;
                var items = Model.ModelStatic.ModelStaticCurrent.Works.Select(
                    q => new PassingViewItem
                    {
                        Laba = passingWorks.SingleOrDefault(a => a.WorkID == q.ID) == null ? null : passingWorks.Single(a => a.WorkID == q.ID),
                        StudentID = student.ID,
                        Work = q,
                        HistoryStudentID = student._CurrentHistoryStudent.ID
                    }).ToArray();
                for (int i = 0; i < items.Length; i++)
                {
                    var passingViewItem = items[i];
                    var button = new ToggleButton
                                     {
                                         DataContext = passingViewItem,
                                         Style = this.Resources["ToggleButtonStyle"] as Style
                                     };
                    button.Content = new TextBlock() {Text = "dsd"};
                    Grid.SetColumn(button, i);
                    Grid.SetRow(button, k);
                    LayoutRoot.Children.Add(button);
                }
                //   (sender as ItemsControl).ItemsSource = items;
            }
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
