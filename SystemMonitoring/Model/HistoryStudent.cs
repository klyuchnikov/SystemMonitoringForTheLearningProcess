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
        public class HistoryStudent : INotifyPropertyChanged, IEquatable<HistoryStudent>
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
            #region StatusId
            private int statusId;
            public int StatusId
            {
                get { return statusId; }
                set
                {
                    statusId = value;
                    NotifyPropertyChanged("StatusId");
                }
            }

            #endregion
            #region StudentId
            private int studentId;
            public int StudentId
            {
                get { return studentId; }
                private set
                {
                    studentId = value;
                    NotifyPropertyChanged("StudentId");
                }
            }

            #endregion
            #region GroupId
            private int groupId;
            public int GroupId
            {
                get { return groupId; }
                set
                {
                    groupId = value;
                    NotifyPropertyChanged("GroupId");
                }
            }
            #endregion
            #region DateOfPlacing
            private DateTime dateOfPlacing;
            public DateTime DateOfPlacing
            {
                get { return dateOfPlacing; }
                set
                {
                    dateOfPlacing = value;
                    NotifyPropertyChanged("DateOfPlacing");
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

            private HistoryStudent(JObject jObject)
            {
                if (jObject.Property("id") == null)
                {
                    this.id = jObject.Value<int>("ID");
                    this.statusId = jObject.Value<int>("StatusId");
                    this.studentId = jObject.Value<int>("StudentId");
                    this.groupId = jObject.Value<int>("GroupId");
                    this.comment = jObject.Value<string>("Comment");
                    this.dateOfPlacing = jObject.Value<DateTime>("DateOfPlacing");
                }
                else
                {
                    this.id = jObject.Value<int>("id");
                    this.statusId = jObject.Value<int>("status_id");
                    this.studentId = jObject.Value<int>("id_stud");
                    this.groupId = jObject.Value<int>("groups_id");
                    this.comment = jObject.Value<string>("comment");
                    this.dateOfPlacing = jObject.Value<DateTime>("date");
                }
            }

            public static void AddHistoryStudent(JObject jObject)
            {
                var historyStudent = new HistoryStudent(jObject);
                if (!Current.listHistoriesStudents.Contains(historyStudent))
                {
                    Current.listHistoriesStudents.Add(historyStudent);
                    Current.Students = null;
                    if (Current.works.Count > 0)
                        Client.Current.GetData(Operation.get_work_stud, "id_etap=" + string.Join(",", Current.works.Select(a => a.ID)) + "&id_hystory_stud=" + historyStudent.ID);
                }
                else
                    Current.listHistoriesStudents.Single(q => q.Equals(historyStudent)).Update(historyStudent);
            }

            private void Update(HistoryStudent historyStudent)
            {
                this.statusId = historyStudent.statusId;
                this.studentId = historyStudent.studentId;
                this.groupId = historyStudent.groupId;
                this.comment = historyStudent.comment;
            }

            public static void AddRangeHistoryStudent(JToken[] jToken)
            {
                foreach (var token in jToken)
                    AddHistoryStudent(token as JObject);
            }

            public static void AddRangeHistoryStudent(JArray jArray)
            {
                var historyStudents = jArray.OfType<JObject>().ToArray().Select(q => new HistoryStudent(q));
                Current.listHistoriesStudents.Clear();
                Current.listHistoriesStudents.AddRange(historyStudents);
                Current.HistoriesStudents = null;
            }

            [JsonIgnore]
            public PassingWork[] _PassingWorks
            {
                get { return Current.passingWorks.Where(a => a.HistoryStudentID == this.id).ToArray(); }
            }

            [JsonIgnore]
            public AttendingLectures[] _AttendingLectureses
            {
                get { return Current.attendingsLectures.Where(a => a.HistoryStudentID == this.id).ToArray(); }
            }


            [JsonIgnore]
            public Student _Student
            {
                get { return Current.listStudents.Single(a => a.ID == this.StudentId); }
            }

            [JsonIgnore]
            public Group _Group
            {
                get { return Current.listGroup.Single(a => a.ID == this.groupId); }
            }


            public override string ToString()
            {
                return "!__HistoryStudent__!";
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void NotifyPropertyChanged(string propertyName)
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public bool Equals(HistoryStudent other)
            {
                return other.ID == this.id;
            }
        }
    }
}
