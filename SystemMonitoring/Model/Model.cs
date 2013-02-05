using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace SystemMonitoring.Model
{

    public partial class Model : INotifyPropertyChanged
    {
        private Model()
        {
        }

        public static Model Current = new Model();

        public Teacher CurrentTeacher
        {
            get
            {
                if (Client.Current != null)
                    if (!string.IsNullOrEmpty(Client.Current.Login))
                        return Current.Teachers.SingleOrDefault(q => q.Email == Client.Current.Login);
                return null;
            }
            set
            {
                NotifyPropertyChanged("CurrentTeacher");
            }
        }
        public DateTime DateStartCurrentSemestr
        {
            get
            {
                var oldYear = DateTime.Now.Year - 1;
                return new DateTime(DateTime.Now.Month < 9 ? oldYear : DateTime.Now.Year, 9, 1);
            }
        }
        public DateTime DateEndCurrentSemestr
        {
            get
            {
                var nextYear = DateTime.Now.Year + 1;
                return new DateTime(DateTime.Now.Month < 9 ? DateTime.Now.Year : nextYear, 8, 31);
            }
        }

        private readonly List<Group> listGroup = new List<Group>();
        public Group[] Groups
        {
            get { return listGroup.ToArray(); }
            set { NotifyPropertyChanged("Groups"); }
        }

        private readonly List<Discipline> listDisciplines = new List<Discipline>();
        public Discipline[] Disciplines
        {
            get { return listDisciplines.ToArray(); }
            set { NotifyPropertyChanged("Disciplines"); }
        }

        private readonly List<Student> listStudents = new List<Student>();
        public Student[] Students
        {
            get { return listStudents.ToArray(); }
            set { NotifyPropertyChanged("Students"); }
        }

        private readonly List<HistoryStudent> listHistoriesStudents = new List<HistoryStudent>();
        public HistoryStudent[] HistoriesStudents
        {
            get { return listHistoriesStudents.ToArray(); }
            set { NotifyPropertyChanged("HistoryStudent"); }
        }

        private readonly List<Teacher> listTeachers = new List<Teacher>();
        public Teacher[] Teachers
        {
            get { return listTeachers.ToArray(); }
            set { NotifyPropertyChanged("Teacher"); }
        }

        private readonly List<Status> listStatuses = new List<Status>();
        public Status[] Statuses
        {
            get { return listStatuses.ToArray(); }
            set { NotifyPropertyChanged("Statuses"); }
        }

        private readonly List<TypeWork> listTypeWorks = new List<TypeWork>();
        public TypeWork[] TypesWorks
        {
            get { return listTypeWorks.ToArray(); }
            set { NotifyPropertyChanged("TypesWorks"); }
        }
        private readonly List<DisciplinesGroups> disciplinesGroupses = new List<DisciplinesGroups>();
        public DisciplinesGroups[] DisciplinesGroupses
        {
            get { return disciplinesGroupses.ToArray(); }
            set { NotifyPropertyChanged("DisciplinesGroupses"); }
        }

        private readonly List<DisciplinesTeachers> disciplinesTeacherses = new List<DisciplinesTeachers>();
        public DisciplinesTeachers[] DisciplinesTeacherses
        {
            get { return disciplinesTeacherses.ToArray(); }
            set { NotifyPropertyChanged("DisciplinesTeacherses"); }
        }
        private readonly List<CommentDisciplineForStudent> commentsDisciplineForStudent = new List<CommentDisciplineForStudent>();
        public CommentDisciplineForStudent[] CommentsDisciplineForStudent
        {
            get { return commentsDisciplineForStudent.ToArray(); }
            set { NotifyPropertyChanged("CommentsDisciplineForStudent"); }
        }
        private readonly List<DisciplinesTeachersTypeWork> disciplinesTeachersTypeWorks = new List<DisciplinesTeachersTypeWork>();
        public DisciplinesTeachersTypeWork[] DisciplinesTeachersTypeWorks
        {
            get { return disciplinesTeachersTypeWorks.ToArray(); }
            set { NotifyPropertyChanged("DisciplinesTeachersTypeWorks"); }
        }

        private readonly List<Work> works = new List<Work>();
        public Work[] Works
        {
            get { return works.ToArray(); }
            set { NotifyPropertyChanged("Works"); }
        }

        private readonly List<PassingWork> passingWorks = new List<PassingWork>();
        public PassingWork[] PassingWorks
        {
            get { return passingWorks.ToArray(); }
            set { NotifyPropertyChanged("PassingWorks"); }
        }

        private readonly List<AttendingLectures> attendingsLectures = new List<AttendingLectures>();
        public AttendingLectures[] AttendingsLectures
        {
            get { return attendingsLectures.ToArray(); }
            set { NotifyPropertyChanged("AttendingsLectures"); }
        }
        public void Load(JToken jToken)
        {
            var arr = jToken.OfType<JProperty>().ToArray();
            foreach (var jProperty in arr)
            {
                switch (jProperty.Name)
                {
                    case "Groups":
                        Model.Group.AddRangeGroup(jProperty.Value as JArray);
                        break;
                    case "Disciplines":
                        Model.Discipline.AddRangeDiscipline(jProperty.Value as JArray);
                        break;
                    case "Students":
                        Model.Student.AddRangeStudent(jProperty.Value as JArray);
                        break;
                    case "HistoriesStudents":
                        Model.HistoryStudent.AddRangeHistoryStudent(jProperty.Value as JArray);
                        break;
                    case "Teachers":
                        Model.Teacher.AddRangeTeacher(jProperty.Value as JArray);
                        CurrentTeacher = null;
                        break;
                    case "Statuses":
                        Model.Status.AddRangeStatus(jProperty.Value as JArray);
                        break;
                    case "TypesWorks":
                        Model.TypeWork.AddRangeTypeWork(jProperty.Value as JArray);
                        break;
                    case "DisciplinesGroupses":
                        Model.DisciplinesGroups.AddRangeDisciplinesGroups(jProperty.Value as JArray);
                        break;
                    case "CommentsDisciplineForStudent":
                        Model.CommentDisciplineForStudent.AddRangeCommentDisciplineForStudent(jProperty.Value as JArray);
                        break;
                    case "DisciplinesTeachersTypeWorks":
                        Model.DisciplinesTeachersTypeWork.AddRangeDisciplinesTeachersTypeWork(jProperty.Value as JArray);
                        break;
                    case "Works":
                        Model.Work.AddRangeWork(jProperty.Value as JArray);
                        break;
                    case "PassingWorks":
                        Model.PassingWork.AddRangePassingWork(jProperty.Value as JArray);
                        break;
                    case "AttendingsLectures":
                        Model.AttendingLectures.AddRangeAttendingLectures(jProperty.Value as JArray);
                        break;
                    case "DisciplinesTeacherses":
                        Model.DisciplinesTeachers.AddRangeDisciplinesTeachers(jProperty.Value as JArray);
                        break;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => PropertyChanged(this,
                                                                                new PropertyChangedEventArgs(
                                                                                    propertyName)));
            }
        }
    }
    public class ModelStatic : INotifyPropertyChanged
    {
        public static Model Current
        {
            get { return Model.Current; }
        }
        public static Client CurrentClient
        {
            get { return Client.Current; }
        }

        private static readonly ModelStatic modelStaticCurrent = new ModelStatic();

        public static ModelStatic ModelStaticCurrent { get { return modelStaticCurrent; } }

        public Model.DisciplinesTeachersTypeWork[] DisciplinesTeachersTypeWorks { get; set; }

        private Model.Work[] works;

        public Model.Work[] Works
        {
            get { return works; }
            set
            {
                works = value;
                NotifyPropertyChanged("Works");
            }
        }

        public static int Parametr { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => PropertyChanged(this,
                                                                                new PropertyChangedEventArgs(
                                                                                    propertyName)));
            }
        }
    }
}
