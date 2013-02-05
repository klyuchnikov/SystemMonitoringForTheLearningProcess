using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SystemMonitoring.Model
{
    public partial class Model
    {
        public class Group : INotifyPropertyChanged, IEquatable<Group>
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
            #region FullName
            private string fullName;
            public string FullName
            {
                get { return fullName; }
                set
                {
                    fullName = value;
                    NotifyPropertyChanged("FullName");
                }
            }
            #endregion
            #region ShortName
            private string shortName;
            public string ShortName
            {
                get { return shortName; }
                 set
                {
                    shortName = value;
                    NotifyPropertyChanged("ShortName");
                }
            }
            #endregion
            #region ShortName
            private int courseNumber;
            public int CourseNumber
            {
                get { return courseNumber; }
                set
                {
                    courseNumber = value;
                    NotifyPropertyChanged("CourseNumber");
                }
            }
            #endregion
            #region GroupNumber
            private int groupNumber;
            public int GroupNumber
            {
                get { return groupNumber; }
                set
                {
                    groupNumber = value;
                    NotifyPropertyChanged("GroupNumber");
                }
            }
            #endregion

            private Group(JToken jObject)
            {
                this.CourseNumber = jObject.Value<int>("kurs");
                this.ID = jObject.Value<int>("id");
                this.FullName = jObject.Value<string>("full_name");
                this.ShortName = jObject.Value<string>("socr");
                this.GroupNumber = jObject.Value<int>("num_group");
            }
            private Group(JObject jObject)
            {
                this.CourseNumber = jObject.Value<int>("CourseNumber");
                this.ID = jObject.Value<int>("ID");
                this.FullName = jObject.Value<string>("FullName");
                this.ShortName = jObject.Value<string>("ShortName");
                this.GroupNumber = jObject.Value<int>("GroupNumber");
            }

            public static void AddGroup(JObject jObject)
            {
                var group = new Group(jObject);
                if (!Current.listGroup.Contains(group))
                {
                    Current.listGroup.Add(group);
                    Current.Groups = null;
                }
                else
                    Current.listGroup.Single(q => q.Equals(group)).Update(group);
            }

            private void Update(Group group)
            {
                this.CourseNumber = group.CourseNumber;
                this.FullName = group.FullName;
                this.ShortName = group.ShortName;
                this.GroupNumber = group.GroupNumber;
            }
            public static void AddRangeGroup(JToken[] jToken)
            {
                var groups = jToken.Select(q => new Group(q)).ToArray();
                foreach (var group in groups)
                {
                    if (!Current.listGroup.Contains(group))
                    {
                        Current.listGroup.Add(group);
                        Current.Groups = null;
                    }
                    else
                        Current.listGroup.Single(q => q.Equals(group)).Update(group);
                }
            }

            public static void AddRangeGroup(JArray jArray)
            {
                var groups = jArray.OfType<JObject>().ToArray().Select(q => new Group(q));
                Current.listGroup.Clear();
                Current.listGroup.AddRange(groups);
                Current.Groups = null;
            }

            [JsonIgnore]
            public Student[] _Students
            {
                get
                {
                    return
                        Current.listHistoriesStudents.Where(q => q.GroupId == this.ID).Select(
                            a => Current.listStudents.Single(q => q.ID == a.StudentId)).OrderBy(q => q.SurName).ToArray();
                }
            }

            public override string ToString()
            {
                return this.fullName;
            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(string propertyName)
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public bool Equals(Group other)
            {
                return other.ID == this.id;
            }
        }

    }
}

