using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;

namespace SystemMonitoring.Model
{
    public partial class Model
    {
        public class AttendingLectures : INotifyPropertyChanged, IEquatable<AttendingLectures>
        {
            #region HistoryStudentID
            private int historyStudentID;
            public int HistoryStudentID
            {
                get { return historyStudentID; }
                private set
                {
                    historyStudentID = value;
                    NotifyPropertyChanged("HistoryStudentID");
                }
            }
            #endregion
            #region ThemeReport
            private string themeReport;
            public string ThemeReport
            {
                get { return themeReport; }
                private set
                {
                    themeReport = value;
                    NotifyPropertyChanged("ThemeReport");
                }
            }

            #endregion
            #region WorkID
            private int workID;
            public int WorkID
            {
                get { return workID; }
                private set
                {
                    workID = value;
                    NotifyPropertyChanged("WorkID");
                }
            }

            #endregion
            #region IsWas
            private bool isWas;
            public bool IsWas
            {
                get { return isWas; }
                private set
                {
                    isWas = value;
                    NotifyPropertyChanged("IsWas");
                }
            }
            #endregion
            #region Grade

            private int grade;
            public int Grade
            {
                get { return grade; }
                private set
                {
                    grade = value;
                    NotifyPropertyChanged("Grade");
                }
            }
            #endregion

            private AttendingLectures(JToken jObject)
            {
                this.historyStudentID = jObject.Value<int>("id_histiry_stud");
                this.workID = jObject.Value<int>("id_etap");
                this.themeReport = jObject.Value<string>("theme_doklad");
                this.grade = jObject.Value<int>("grade");
                this.isWas = jObject.Value<bool>("presence");
            }
            private AttendingLectures(JObject jObject)
            {
                this.historyStudentID = jObject.Value<int>("HistoryStudentID");
                this.workID = jObject.Value<int>("WorkID");
                this.themeReport = jObject.Value<string>("ThemeReport");
                this.grade = jObject.Value<int>("Grade");
                this.isWas = jObject.Value<bool>("IsWas");
            }

            public static void AddAttendingLectures(JObject jObject)
            {
                var attendingLectures = new AttendingLectures(jObject);
                if (!Current.attendingsLectures.Contains(attendingLectures))
                {
                    Current.attendingsLectures.Add(attendingLectures);
                    Current.AttendingsLectures = null;
                }
                else
                    Current.attendingsLectures.Single(a => a.Equals(attendingLectures)).Update(attendingLectures);
            }

            public void Update(AttendingLectures attendingLectures)
            {
                this.historyStudentID = attendingLectures.historyStudentID;
                this.workID = attendingLectures.workID;
                this.themeReport = attendingLectures.themeReport;
                this.grade = attendingLectures.grade;
                this.isWas = attendingLectures.isWas;
            }

            public static void AddRangeAttendingLectures(JToken[] jToken)
            {
                var attendingLectures = jToken.Select(q => new AttendingLectures(q)).ToArray();
                foreach (var attendingLecturese in attendingLectures)
                {
                    if (!Current.attendingsLectures.Contains(attendingLecturese))
                    {
                        Current.attendingsLectures.Add(attendingLecturese);
                        Current.AttendingsLectures = null;
                    }
                    else
                        Current.attendingsLectures.Single(a => a.Equals(attendingLectures)).Update(attendingLecturese);
                }
            }

            public static void AddRangeAttendingLectures(JArray jArray)
            {
                var attendingLectures = jArray.OfType<JObject>().ToArray().Select(q => new AttendingLectures(q));
                Current.attendingsLectures.Clear();
                Current.attendingsLectures.AddRange(attendingLectures);
                Current.AttendingsLectures = null;
            }

            public override string ToString()
            {
                return "!!!___PassingWork___!!!";
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void NotifyPropertyChanged(string propertyName)
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public bool Equals(AttendingLectures other)
            {
                return other.HistoryStudentID == this.HistoryStudentID && other.WorkID == this.WorkID;
            }
        }


    }
}
