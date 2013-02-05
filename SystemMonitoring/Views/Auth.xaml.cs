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

namespace SystemMonitoring.Views
{
    public partial class Auth : PhoneApplicationPage
    {
        // Конструктор
        public Auth()
        {
            InitializeComponent();
            ContentPanel.DataContext = Client.Current;
            var progressIndicator = new ProgressIndicator { IsVisible = true };
            SystemTray.SetProgressIndicator(this, progressIndicator);
            if (!Client.Current.IsFirstConnected)
                progressIndicator.IsIndeterminate = true;
            var dt = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            dt.Tick += delegate
                           {
                               if (Client.Current.IsAuthorized)
                               {
                                   this.NavigationService.GoBack();
                                   dt.Stop();
                               }
                           };
            dt.Start();

            loginTB.Text = "admin@mail.com";
            Button_Click(loginTB.Text, new RoutedEventArgs());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Client.Current.IsAuthorized)
            {
                SystemTray.GetProgressIndicator(this).IsIndeterminate = true;
                Client.Current.Auth(loginTB.Text, passTB.Password);
            }
        }
    }
}