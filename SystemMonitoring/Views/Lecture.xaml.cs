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
    public partial class Lecture : PhoneApplicationPage
    {
        public Lecture()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var lectureID = int.Parse(e.Uri.ToString().Substring(e.Uri.ToString().IndexOf('?')).Split('=')[1]);
            var lecture = Model.Model.Current.Works.Single(q => q.ID == lectureID);
            this.DataContext = lecture;
        }
    }
}