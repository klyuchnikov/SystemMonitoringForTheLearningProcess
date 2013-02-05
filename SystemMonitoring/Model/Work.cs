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
using System.Runtime.Serialization;

namespace SystemMonitoring.Model
{
    public partial class Model
    {
        public class Work : INotifyPropertyChanged, IEquatable<Work>
        {
            #region DisciplinesTeachersTypeWorkID

            private int disciplinesTeachersTypeWorkID;

            public int DisciplinesTeachersTypeWorkID
            {
                get { return disciplinesTeachersTypeWorkID; }
                private set
                {
                    disciplinesTeachersTypeWorkID = value;
                    NotifyPropertyChanged("DisciplinesTeachersTypeWorkID");
                }
            }

            #endregion

            #region Theme

            private string theme;

            public string Theme
            {
                get { return theme; }
                set
                {
                    theme = value;
                    NotifyPropertyChanged("Theme");
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

            #region Date

            private DateTime date;

            public DateTime Date
            {
                get { return date; }
                set
                {
                    date = value;
                    NotifyPropertyChanged("Date");
                }
            }

            #endregion

            #region Link

            private string link;

            public string Link
            {
                get { return link; }
                set
                {
                    link = value;
                    NotifyPropertyChanged("Link");
                }
            }

            #endregion

            private Work(JToken jObject)
            {
                this.disciplinesTeachersTypeWorkID = jObject.Value<int>("discip_prepod_type_work_id");
                this.id = jObject.Value<int>("id");
                this.link = jObject.Value<string>("link");
                this.theme = jObject.Value<string>("theme");
                this.date = jObject.Value<DateTime>("date");
            }

            private Work(JObject jObject)
            {
                this.disciplinesTeachersTypeWorkID = jObject.Value<int>("DisciplinesTeachersTypeWorkID");
                this.id = jObject.Value<int>("ID");
                this.link = jObject.Value<string>("Link");
                this.theme = jObject.Value<string>("Theme");
                this.date = jObject.Value<DateTime>("Date");
            }

            private void Update(Work work)
            {
                this.disciplinesTeachersTypeWorkID = work.DisciplinesTeachersTypeWorkID;
                this.link = work.link;
                this.theme = work.theme;
                this.date = work.date;
            }

            public static void AddWork(JObject jObject)
            {
                var work = new Work(jObject);
                if (!Current.works.Contains(work))
                {
                    Current.works.Add(work);
                    Current.Works = null;
                }
                else
                    Current.works.Single(a => a.Equals(work)).Update(work);
            }

            public static void AddRangeWork(JToken[] jToken)
            {
                var works = jToken.Select(q => new Work(q)).ToArray();
                foreach (var work in works)
                {
                    if (!Current.works.Contains(work))
                    {
                        Current.works.Add(work);
                        Current.Works = null;
                    }
                    else
                        Current.works.Single(a => a.Equals(work)).Update(work);
                }
            }

            public static void AddRangeWork(JArray jArray)
            {
                var works = jArray.OfType<JObject>().ToArray().Select(q => new Work(q));
                Current.works.Clear();
                Current.works.AddRange(works);
                Current.Works = null;
            }

            [JsonIgnore]
            public DisciplinesTeachersTypeWork _DisciplinesTeachersTypeWork
            {
                get { return Current.disciplinesTeachersTypeWorks.Single(a => a.ID == this.disciplinesTeachersTypeWorkID); }
            }

            [JsonIgnore]
            public PassingWork[] _PassingWorks
            {
                get
                {
                    return Current.passingWorks.Where(q => q.WorkID == this.ID
                        //  && Current.DateStartCurrentSemestr < q._HistoryStudent.DateOfPlacing && Current.DateEndCurrentSemestr > q._HistoryStudent.DateOfPlacing
                        ).ToArray();
                }
            }

            public override string ToString()
            {
                if (_DisciplinesTeachersTypeWork._TypeWork.Name != "Лабораторная работа")
                    return date.ToShortDateString();
                return theme;
            }

            public string _ToString
            {
                get { return this.ToString(); }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void NotifyPropertyChanged(string propertyName)
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                    PropertyChanged(this, new PropertyChangedEventArgs("_ToString"));
                }
            }

            public bool Equals(Work other)
            {
                return other.ID == this.ID;
            }
        }
    }
}
