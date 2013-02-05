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
        public class DisciplinesTeachersTypeWork : INotifyPropertyChanged, IEquatable<DisciplinesTeachersTypeWork>
        {
            #region DisciplinesTeachersID
            private int disciplinesTeachersID;
            public int DisciplinesTeachersID
            {
                get { return disciplinesTeachersID; }
                private set
                {
                    disciplinesTeachersID = value;
                    NotifyPropertyChanged("DisciplinesTeachersID");
                }
            }
            #endregion
            #region TypeWorkID
            private int typeWorkID;
            public int TypeWorkID
            {
                get { return typeWorkID; }
                private set
                {
                    typeWorkID = value;
                    NotifyPropertyChanged("TypeWorkID");
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
            #region StudyHours
            private int studyHours;
            public int StudyHours
            {
                get { return studyHours; }
                private set
                {
                    studyHours = value;
                    NotifyPropertyChanged("StudyHours");
                }
            }
            #endregion

            private DisciplinesTeachersTypeWork(JToken jObject)
            {
                this.disciplinesTeachersID = jObject.Value<int>("discip_prepod_id");
                this.typeWorkID = jObject.Value<int>("type_work_id");
                this.studyHours = jObject.Value<int>("study_hours");
                this.id = jObject.Value<int>("id");
            }
            private DisciplinesTeachersTypeWork(JObject jObject)
            {
                this.disciplinesTeachersID = jObject.Value<int>("DisciplinesTeachersID");
                this.typeWorkID = jObject.Value<int>("TypeWorkID");
                this.studyHours = jObject.Value<int>("StudyHours");
                this.id = jObject.Value<int>("ID");
            }

            public static void AddDisciplinesTeachersTypeWork(JObject jObject)
            {
                var disciplinesTeachersTypeWorks = new DisciplinesTeachersTypeWork(jObject);
                if (!Current.disciplinesTeachersTypeWorks.Contains(disciplinesTeachersTypeWorks))
                {
                    Current.disciplinesTeachersTypeWorks.Add(disciplinesTeachersTypeWorks);
                    Current.DisciplinesTeachersTypeWorks = null;
                }
                else
                    Current.disciplinesTeachersTypeWorks.Single(q => q.Equals(disciplinesTeachersTypeWorks)).Update(
                        disciplinesTeachersTypeWorks);
            }

            private void Update(DisciplinesTeachersTypeWork disciplinesTeachersTypeWorks)
            {
                this.disciplinesTeachersID = disciplinesTeachersTypeWorks.disciplinesTeachersID;
                this.typeWorkID = disciplinesTeachersTypeWorks.typeWorkID;
                this.studyHours = disciplinesTeachersTypeWorks.studyHours;
            }

            public static void AddRangeDisciplinesTeachersTypeWork(JToken[] jToken)
            {
                var disciplinesTeachersTypeWorks = jToken.Select(q => new DisciplinesTeachersTypeWork(q)).ToArray();
                foreach (var disciplinesTeachersTypeWork in disciplinesTeachersTypeWorks)
                {
                    if (!Current.disciplinesTeachersTypeWorks.Contains(disciplinesTeachersTypeWork))
                    {
                        Current.disciplinesTeachersTypeWorks.Add(disciplinesTeachersTypeWork);
                        Current.DisciplinesTeachersTypeWorks = null;
                    }
                    else
                        Current.disciplinesTeachersTypeWorks.Single(q => q.Equals(disciplinesTeachersTypeWorks)).Update(
                            disciplinesTeachersTypeWork);
                }
            }

            public static void AddRangeDisciplinesTeachersTypeWork(JArray jArray)
            {
                var disciplinesTeachersTypeWorks = jArray.OfType<JObject>().ToArray().Select(q => new DisciplinesTeachersTypeWork(q));
                Current.disciplinesTeachersTypeWorks.Clear();
                Current.disciplinesTeachersTypeWorks.AddRange(disciplinesTeachersTypeWorks);
                Current.DisciplinesTeachersTypeWorks = null;
            }

            [JsonIgnore]
            public DisciplinesTeachers _DisciplinesTeachers
            {
                get { return Model.Current.disciplinesTeacherses.Single(q => q.ID == this.DisciplinesTeachersID); }
            }

            [JsonIgnore]
            public TypeWork _TypeWork
            {
                get { return Model.Current.listTypeWorks.Single(q => q.ID == this.typeWorkID); }
            }

            [JsonIgnore]
            public Work[] _Works
            {
                get { return Current.works.Where(q => q.DisciplinesTeachersTypeWorkID == this.ID).OrderBy(q => q.Date).ToArray(); }
            }

            public override string ToString()
            {
                return "!!!___DisciplinesTeachersTypeWork___!!!";
            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(string propertyName)
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public bool Equals(DisciplinesTeachersTypeWork other)
            {
                return this.ID == other.ID;
            }
        }
    }
}
