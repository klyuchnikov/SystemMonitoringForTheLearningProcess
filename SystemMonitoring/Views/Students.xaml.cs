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
    public partial class Students : PhoneApplicationPage
    {
        public Students()
        {
            InitializeComponent();
        }

        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var st = sender as StackPanel;
            NavigationService.Navigate(new Uri("/Views/StudentView.xaml?ID=" + (int)st.Tag, UriKind.Relative));
        }

        private void Navigate_Disciplines(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Navigate_Update(object sender, EventArgs e)
        {
            Client.Current.GetStartData();
        }
    }
}