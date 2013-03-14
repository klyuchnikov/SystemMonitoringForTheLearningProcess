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

        private int DisciplinesTeachersTypeWorksID;
        private Model.Model.DisciplinesTeachersTypeWork dd;
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DisciplinesTeachersTypeWorksID = int.Parse(e.Uri.ToString().Substring(e.Uri.ToString().IndexOf('?')).Split('=')[1]);
            dd = Model.Model.Current.DisciplinesTeachersTypeWorks.Single(q => q.ID == DisciplinesTeachersTypeWorksID);
            discName.Text = dd._DisciplinesTeachers._Discipline.ShortName;
            Date.Value = DateTime.Now.Date;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            Model.Model.Work.AllLection(DisciplinesTeachersTypeWorksID, discName.Text, Link.Text, Date.Value.Value);
            Model.Model.Current.CurrentTeacher._DisciplinesTeachers = null;
            NavigationService.GoBack();

          /*  var discipline = dd._DisciplinesTeachers._Discipline;
            var group = Model.Model.Current.DisciplinesGroupses.Where(q => q.DisciplineID == discipline.ID).Select(a => a._Group).ToArray();
            var works = Model.Model.Current.CurrentTeacher._DisciplinesTeachers.Single(
                    q => q._Discipline.ID == discipline.ID)._DisciplinesTeachersTypeWorks.Where(a => a._TypeWork.Name == "Лекция").SelectMany(q => q._Works).ToArray();
            Model.ModelStatic.ModelStaticCurrent.Groups = group;
            Model.ModelStatic.ModelStaticCurrent.Works = works;

            NavigationService.Navigate(new Uri("/Views/LecturesView.xaml", UriKind.Relative));*/
        }
    }
}