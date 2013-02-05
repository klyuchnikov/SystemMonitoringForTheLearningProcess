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

namespace SystemMonitoring.Views
{
    public partial class StudentView : PhoneApplicationPage
    {
        private Brush oldBrush;
        private ControlTemplate oldControlTemplate;
        public StudentView()
        {
            InitializeComponent();
            oldControlTemplate = Surname.Template;
            Surname.Template = (this.Resources["TextTemplate"] as ControlTemplate);

        }
        private void SP_Loaded(object sender, RoutedEventArgs e)
        {
            var arr = SP.Children.OfType<PhoneTextBox>().ToArray();
            foreach (var phoneTextBox in arr)
            {
                if (!string.IsNullOrEmpty(phoneTextBox.Text))
                {
                    phoneTextBox.Template = (this.Resources["TextTemplate"] as ControlTemplate);
                }
                phoneTextBox.GotFocus += PhoneTextBox_GotFocus;
                phoneTextBox.LostFocus += PhoneTextBox_LostFocus;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var studentId = int.Parse(e.Uri.ToString().Substring(e.Uri.ToString().IndexOf('?')).Split('=')[1]);
            LayoutRoot.DataContext = Model.Model.Current.Students.SingleOrDefault(a => a.ID == studentId);
        }

        private void PhoneTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as PhoneTextBox).Template = oldControlTemplate;
        }

        private void PhoneTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty((sender as PhoneTextBox).Text))
            {
                (sender as PhoneTextBox).Template = oldControlTemplate;
            }
            else
            {
                (sender as PhoneTextBox).Template = (this.Resources["TextTemplate"] as ControlTemplate);
            }
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj, string name = null) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (name != null)
                {
                    if (child != null && child is childItem && (string)child.GetValue(NameProperty) == name)
                        return (childItem)child;
                    else
                    {
                        childItem childOfChild = FindVisualChild<childItem>(child, name);
                        if (childOfChild != null)
                            return childOfChild;
                    }
                }
                else
                {
                    if (child != null && child is childItem)
                        return (childItem)child;
                    else
                    {
                        childItem childOfChild = FindVisualChild<childItem>(child);
                        if (childOfChild != null)
                            return childOfChild;
                    }
                }

            }
            return null;
        }

    }
}