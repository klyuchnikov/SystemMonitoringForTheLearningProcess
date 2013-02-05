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
        public class Teacher : INotifyPropertyChanged, IEquatable<Teacher>
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
            private string patronomic;
            public string Patronomic
            {
                get { return patronomic; }
                set
                {
                    patronomic = value;
                    NotifyPropertyChanged("Patronomic");
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

            private Teacher(JObject jObject)
            {
                if (jObject.Property("id") == null)
                {
                    this.id = jObject.Value<int>("ID");
                    this.name = jObject.Value<string>("Name");
                    this.surName = jObject.Value<string>("SurName");
                    this.patronomic = jObject.Value<string>("Patronomic");
                    this.email = jObject.Value<string>("Email");
                }
                else
                {
                    this.id = jObject.Value<int>("id");
                    this.name = jObject.Value<string>("name");
                    this.surName = jObject.Value<string>("surname");
                    this.patronomic = jObject.Value<string>("patronymic");
                    this.email = jObject.Value<string>("email");
                }
            }
            private void Update(Teacher teacher)
            {
                this.name = teacher.name;
                this.surName = teacher.surName;
                this.patronomic = teacher.patronomic;
            }

            public static void AddTeacher(JObject jObject)
            {
                var teacher = new Teacher(jObject);
                if (!Current.listTeachers.Contains(teacher))
                {
                    Current.listTeachers.Add(teacher);
                    Current.Teachers = null;
                }
                else
                    Current.listTeachers.Single(q => q.Equals(teacher)).Update(teacher);
            }

            public static void AddRangeTeacher(JToken[] jToken)
            {
                foreach (var token in jToken)
                    AddTeacher(token as JObject);
            }
            public static void AddRangeTeacher(JArray jArray)
            {
                var teachers = jArray.OfType<JObject>().ToArray().Select(q => new Teacher(q));
                Current.listTeachers.Clear();
                Current.listTeachers.AddRange(teachers);
                Current.Teachers = null;
            }

            public override string ToString()
            {
                if (!string.IsNullOrEmpty(this.patronomic))
                    return string.Format("{0} {1}.{2}.", this.surName, this.name[0], this.patronomic[0]);
                return string.Format("{0} {1}.", this.surName, this.name[0]);
            }

            [JsonIgnore]
            public DisciplinesTeachers[] _DisciplinesTeachers
            {
                get { return Model.Current.disciplinesTeacherses.Where(a => a.TeacherID == this.id).ToArray(); }
                set
                {
                    NotifyPropertyChanged("_DisciplinesTeachers");
                    if (_DisciplinesTeachers != null)
                        foreach (var disciplinesTeacherse in _DisciplinesTeachers)
                        {
                            disciplinesTeacherse._Discipline = null;
                        }
                }
            }


            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(string propertyName)
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public bool Equals(Teacher other)
            {
                return other.ID == this.ID;
            }
        }
    }
}
