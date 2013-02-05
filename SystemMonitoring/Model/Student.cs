using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SystemMonitoring.Model
{

    public partial class Model
    {
        public class Student : INotifyPropertyChanged, IEquatable<Student>
        {
            #region ID
            private int id;
            public int ID
            {
                get { return id; }
                private set
                {
                    id = value;
                    NotifyPropertyChanged("ID");
                }
            }
            #endregion
            #region Name
            private string name;
            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
            #endregion
            #region SurName
            private string surName;
            public string SurName
            {
                get { return surName; }
                set
                {
                    surName = value;
                    NotifyPropertyChanged("SurName");
                }
            }
            #endregion
            #region Patronomic
            private string patronymic;
            public string Patronymic
            {
                get { return patronymic; }
                set
                {
                    patronymic = value;
                    NotifyPropertyChanged("Patronomic");
                }
            }
            #endregion
            #region IsStarosta
            private bool isStarosta;
            public bool IsStarosta
            {
                get { return isStarosta; }
                set
                {
                    if (value)
                    {
                        foreach (var student in this._CurrentHistoryStudent._Group._Students)
                            student.IsStarosta = false;
                    }
                    isStarosta = value;
                    NotifyPropertyChanged("IsStarosta");
                }
            }
            #endregion
            #region Phone
            private string phone;
            public string Phone
            {
                get { return phone; }
                set
                {
                    phone = value;
                    NotifyPropertyChanged("Phone");
                }
            }
            #endregion
            #region Email
            private string email;
            public string Email
            {
                get { return email; }
                set
                {
                    email = value;
                    NotifyPropertyChanged("Email");
                }
            }
            #endregion
            #region CommentForStudent
            private string commentForStudent;
            public string CommentForStudent
            {
                get { return commentForStudent; }
                set
                {
                    commentForStudent = value;
                    NotifyPropertyChanged("CommentForStudent");
                }
            }
            #endregion

            private Student(JObject jObject)
            {
                if (jObject.Property("id") == null)
                {
                    this.id = jObject.Value<int>("ID");
                    this.name = jObject.Value<string>("Name");
                    this.surName = jObject.Value<string>("SurName");
                    this.patronymic = jObject.Value<string>("Patronomic");
                    this.isStarosta = jObject.Value<bool>("IsStarosta");
                    this.email = jObject.Value<string>("Email");
                    this.phone = jObject.Value<string>("Phone");
                    this.commentForStudent = jObject.Value<string>("CommentForStudent");
                }
                else
                {
                    this.id = jObject.Value<int>("id");
                    this.name = jObject.Value<string>("name");
                    this.surName = jObject.Value<string>("surname");
                    this.patronymic = jObject.Value<string>("patronomic");
                    this.isStarosta = jObject.Value<bool>("is_starosta");
                    this.email = jObject.Value<string>("mail");
                    this.phone = jObject.Value<string>("phone");
                    this.commentForStudent = jObject.Value<string>("comment_stud");
                }
            }

            private void Update(Student student)
            {
                this.name = student.name;
                this.surName = student.surName;
                this.patronymic = student.patronymic;
                this.isStarosta = student.isStarosta;
                this.email = student.email;
                this.phone = student.phone;
                this.commentForStudent = student.commentForStudent;
            }

            public static void AddStudent(JObject jObject)
            {
                var student = new Student(jObject);
                if (!Current.listStudents.Contains(student))
                {
                    Current.listStudents.Add(student);
                    Current.Students = null;
                }
                else
                    Current.listStudents.Single(a => a.Equals(student)).Update(student);
            }

            public static void AddRangeStudent(JToken[] jToken)
            {
                foreach (var token in jToken)
                    AddStudent(token as JObject);
            }
            public static void AddRangeStudent(JArray jArray)
            {
                var students = jArray.OfType<JObject>().ToArray().Select(q => new Student(q));
                Current.listStudents.Clear();
                Current.listStudents.AddRange(students);
                Current.Students = null;
            }

            [JsonIgnore]
            public HistoryStudent _CurrentHistoryStudent
            {
                get
                {
                    return Model.Current.listHistoriesStudents.First(a => a.StudentId == this.id && a.DateOfPlacing >= Current.DateStartCurrentSemestr && a.DateOfPlacing <= Current.DateEndCurrentSemestr);
                }
            }
            [JsonIgnore]
            public bool _IsRaiting
            {
                get
                {
                    return ModelStatic.ModelStaticCurrent.Works.Any(
                         q => q._PassingWorks.Any(a => a._HistoryStudent.StudentId == this.ID && a.Raiting > 0));
                }
            }
            [JsonIgnore]
            public Visibility _IsRaitingVisibility
            {
                get { return _IsRaiting ? Visibility.Visible : Visibility.Collapsed; }
                set { NotifyPropertyChanged("_IsRaitingVisibility"); }
            }

            public override string ToString()
            {
                if (!string.IsNullOrEmpty(this.patronymic))
                    return string.Format("{0} {1}.{2}.", this.surName, this.name[0], this.patronymic[0]);
                return string.Format("{0} {1}.", this.surName, this.name[0]);
            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(string propertyName)
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public bool Equals(Student other)
            {
                return other.ID == this.ID;
            }
        }
    }
}
