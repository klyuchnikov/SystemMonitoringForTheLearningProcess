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
        public class PassingWork : INotifyPropertyChanged, IEquatable<PassingWork>
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
            #region Comment
            private string comment;
            public string Comment
            {
                get { return comment; }
                set
                {
                    comment = value;
                    NotifyPropertyChanged("Comment");
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
            #region DateBegin
            private DateTime? dateBegin;
            public DateTime? DateBegin
            {
                get { return dateBegin; }
                set
                {
                    dateBegin = value;
                    NotifyPropertyChanged("DateBegin");
                    NotifyPropertyChanged("_ToString");
                }
            }
            #endregion
            #region DateEnd
            private DateTime? dateEnd;
            public DateTime? DateEnd
            {
                get { return dateEnd; }
                set
                {
                    dateEnd = value;
                    NotifyPropertyChanged("DateEnd");
                    NotifyPropertyChanged("_ToString");
                }
            }
            #endregion
            #region LinkDoc
            private string linkDoc;
            public string LinkDoc
            {
                get { return linkDoc; }
                set
                {
                    linkDoc = value;
                    NotifyPropertyChanged("LinkDoc");
                }
            }
            #endregion
            #region IsPassed
            private bool isPassed;
            public bool IsPassed
            {
                get { return isPassed; }
                set
                {
                    isPassed = value;
                    NotifyPropertyChanged("IsPassed");
                    NotifyPropertyChanged("_ToString");
                    NotifyPropertyChanged("_ToString_");
                }
            }
            #endregion
            #region Progress
            private int progress;
            public int Progress
            {
                get { return progress; }
                set
                {
                    progress = value;
                    NotifyPropertyChanged("Progress");
                    NotifyPropertyChanged("_ToString");
                }
            }
            #endregion
            #region Raiting
            private int raiting;
            public int Raiting
            {
                get { return raiting; }
                set
                {
                    raiting = value;
                    NotifyPropertyChanged("Raiting");
                    _HistoryStudent._Student._IsRaitingVisibility = Visibility.Collapsed;
                }
            }
            #endregion
            #region Grade
            private int grade;
            public int Grade
            {
                get { return grade; }
                set
                {
                    grade = value;
                    NotifyPropertyChanged("Grade");
                }
            }
            #endregion

            private PassingWork(JToken jObject)
            {
                this.historyStudentID = jObject.Value<int>("id_history_stud");
                this.workID = jObject.Value<int>("id_etap");
                this.linkDoc = jObject.Value<string>("path_doc");
                this.comment = jObject.Value<string>("comment");
                this.dateBegin = jObject.Value<DateTime?>("date_begin");
                this.dateEnd = jObject.Value<DateTime?>("date_end");
                this.progress = jObject.Value<int>("progress");
                this.raiting = jObject.Value<int>("raiting");
                this.grade = jObject.Value<int>("grade");
                this.isPassed = jObject.Value<bool>("is_load");
            }

            private PassingWork(JObject jObject)
            {
                this.historyStudentID = jObject.Value<int>("HistoryStudentID");
                this.workID = jObject.Value<int>("WorkID");
                this.linkDoc = jObject.Value<string>("LinkDoc");
                this.comment = jObject.Value<string>("Comment");
                this.dateBegin = jObject.Value<DateTime?>("DateBegin");
                this.dateEnd = jObject.Value<DateTime?>("DateEnd");
                this.progress = jObject.Value<int>("Progress");
                this.raiting = jObject.Value<int>("Raiting");
                this.grade = jObject.Value<int>("Grade");
                this.isPassed = jObject.Value<bool>("IsPassed");
            }
            private void Update(PassingWork passingWork)
            {
                this.historyStudentID = passingWork.historyStudentID;
                this.workID = passingWork.historyStudentID;
                this.linkDoc = passingWork.linkDoc;
                this.comment = passingWork.comment;
                this.dateBegin = passingWork.dateBegin;
                this.dateEnd = passingWork.dateEnd;
                this.progress = passingWork.progress;
                this.raiting = passingWork.raiting;
                this.grade = passingWork.grade;
                this.isPassed = passingWork.isPassed;
            }
            private PassingWork(int historyStudentID, int workID)
            {
                this.historyStudentID = historyStudentID;
                this.workID = workID;
                this.dateBegin = DateTime.Now.Date;
            }

            public static void AddPassingWork(JObject jObject)
            {
                var passingWork = new PassingWork(jObject);
                if (!Current.passingWorks.Contains(passingWork))
                {
                    Current.passingWorks.Add(passingWork);
                    Current.PassingWorks = null;
                }
                else
                    Current.passingWorks.Single(q => q.Equals(passingWork)).Update(passingWork);
            }
            public static PassingWork AddPassingWork(int historyStudentID, int workID)
            {
                var passingWork = new PassingWork(historyStudentID, workID);
                if (!Current.passingWorks.Contains(passingWork))
                {
                    Current.passingWorks.Add(passingWork);
                    Current.PassingWorks = null;
                }
                else
                    Current.passingWorks.Single(q => q.Equals(passingWork)).Update(passingWork);
                return passingWork;
            }

            public static void AddRangePassingWork(JToken[] jToken)
            {
                var passingWorks = jToken.Select(q => new PassingWork(q)).ToArray();
                foreach (var passingWork in passingWorks)
                {
                    if (!Current.passingWorks.Contains(passingWork))
                    {
                        Current.passingWorks.Add(passingWork);
                        Current.PassingWorks = null;
                    }
                    else
                        Current.passingWorks.Single(q => q.Equals(passingWork)).Update(passingWork);
                }
            }

            public static void AddRangePassingWork(JArray jArray)
            {
                var passingWork = jArray.OfType<JObject>().ToArray().Select(q => new PassingWork(q));
                Current.passingWorks.Clear();
                Current.passingWorks.AddRange(passingWork);
                Current.PassingWorks = null;
            }

            public override string ToString()
            {
                if (this.IsPassed)
                    return this.DateEnd.Value.ToShortDateString();
                return this.dateBegin.HasValue ? "В процессе" : "";
            }
            public string _ToString
            {
                get
                {
                    if (this.IsPassed)
                        return this.DateEnd.Value.ToShortDateString();
                    return this.dateBegin.HasValue ? "В процессе" : "";
                }
            }
            public string _ToString_
            {
                get
                {
                    if (this.IsPassed)
                        return " - " + this.DateEnd.Value.ToShortDateString();
                    return this.dateBegin.HasValue ? "- В процессе" : "";
                }
            }

            [JsonIgnore]
            public Work _Work
            {
                get { return Current.works.Single(a => a.ID == this.workID); }
            }

            [JsonIgnore]
            public HistoryStudent _HistoryStudent
            {
                get { return Current.listHistoriesStudents.Single(a => a.ID == this.historyStudentID); }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void NotifyPropertyChanged(string propertyName)
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public bool Equals(PassingWork other)
            {
                return other.workID == this.workID && other.historyStudentID == this.historyStudentID;
            }
        }
    }
}
