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
        public class DisciplinesTeachers : INotifyPropertyChanged, IEquatable<DisciplinesTeachers>
        {
            #region TeacherID
            private int teacherID;
            public int TeacherID
            {
                get { return teacherID; }
                private set
                {
                    teacherID = value;
                    NotifyPropertyChanged("TeacherID");
                }
            }
            #endregion
            #region DisciplineID
            private int disciplineID;
            public int DisciplineID
            {
                get { return disciplineID; }
                private set
                {
                    disciplineID = value;
                    NotifyPropertyChanged("DisciplineID");
                }
            }
            #endregion
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
            #region StudyHoursTotal
            private int studyHoursTotal;
            public int StudyHoursTotal
            {
                get { return studyHoursTotal; }
                private set
                {
                    studyHoursTotal = value;
                    NotifyPropertyChanged("StudyHoursTotal");
                }
            }
            #endregion
            #region Semestr
            private int semestr;
            public int Semestr
            {
                get { return semestr; }
                private set
                {
                    semestr = value;
                    NotifyPropertyChanged("Semestr");
                }
            }
            #endregion

            private DisciplinesTeachers(JObject jObject)
            {
                if (jObject.Property("id_prepod") == null)
                {
                    this.teacherID = jObject.Value<int>("TeacherID");
                    this.disciplineID = jObject.Value<int>("DisciplineID");
                    this.studyHoursTotal = jObject.Value<int>("StudyHoursTotal");
                    this.semestr = jObject.Value<int>("Semestr");
                    this.id = jObject.Value<int>("ID");
                }
                else
                {
                    this.teacherID = jObject.Value<int>("id_prepod");
                    this.disciplineID = jObject.Value<int>("id_discip");
                    this.studyHoursTotal = jObject.Value<int>("study_hours_itog");
                    this.semestr = jObject.Value<int>("semestr");
                    this.id = jObject.Value<int>("id");
                }
            }

            public static void AddDisciplinesTeachers(JObject jObject)
            {
                var disciplinesGroups = new DisciplinesTeachers(jObject);
                if (!Current.disciplinesTeacherses.Contains(disciplinesGroups))
                {
                    Current.disciplinesTeacherses.Add(disciplinesGroups);
                    Current.DisciplinesTeacherses = null;
                    Client.Current.GetData(Operation.get_ids_discip_prepod_type_work, "discip_prepod_id=" + disciplinesGroups.id);
                }
                else
                    Current.disciplinesTeacherses.Single(q => q.Equals(disciplinesGroups)).Update(disciplinesGroups);
            }

            private void Update(DisciplinesTeachers disciplinesGroups)
            {
                this.teacherID = disciplinesGroups.teacherID;
                this.disciplineID = disciplinesGroups.disciplineID;
                this.studyHoursTotal = disciplinesGroups.studyHoursTotal;
                this.semestr = disciplinesGroups.semestr;
            }

            public static void AddRangeDisciplinesTeachers(JToken[] jToken)
            {
                foreach (var token in jToken)
                    AddDisciplinesTeachers(token as JObject);
            }

            public static void AddRangeDisciplinesTeachers(JArray jArray)
            {
                var disciplinesGroups = jArray.OfType<JObject>().ToArray().Select(q => new DisciplinesTeachers(q));
                Current.disciplinesTeacherses.Clear();
                Current.disciplinesTeacherses.AddRange(disciplinesGroups);
                Current.DisciplinesTeacherses = null;
            }

            [JsonIgnore]
            public Discipline _Discipline
            {
                get { return Current.listDisciplines.Single(a => a.ID == this.disciplineID); }
                set { NotifyPropertyChanged("_Discipline"); }
            }

            [JsonIgnore]
            public Teacher _Teacher
            {
                get { return Current.listTeachers.Single(a => a.ID == this.teacherID); }
                set { NotifyPropertyChanged("_Teacher"); }
            }
            [JsonIgnore]
            public DisciplinesTeachersTypeWork[] _DisciplinesTeachersTypeWorks
            {
                get { return Current.disciplinesTeachersTypeWorks.Where(a => a.DisciplinesTeachersID == this.id).ToArray(); }
                set { NotifyPropertyChanged("_DisciplinesTeachersTypeWorks"); }
            }

            public override string ToString()
            {
                return _Discipline.ToString();
            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(string propertyName)
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public bool Equals(DisciplinesTeachers other)
            {
                return other.id == this.id;
            }
        }
    }
}
