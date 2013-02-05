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
using Newtonsoft.Json.Linq;

namespace SystemMonitoring.Model
{
    public partial class Model
    {
        public class Status : INotifyPropertyChanged, IEquatable<Status>
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
                private set
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
            #endregion

            private Status(JToken jObject)
            {
                this.id = jObject.Value<int>("id");
                this.name = jObject.Value<string>("name");
            }
            private Status(JObject jObject)
            {
                this.id = jObject.Value<int>("ID");
                this.name = jObject.Value<string>("Name");
            }
            private void Update(Status status)
            {
                this.name = status.name;
            }

            public static void AddStatus(JObject jObject)
            {
                var status = new Status(jObject);
                if (!Current.listStatuses.Contains(status))
                {
                    Current.listStatuses.Add(status);
                    Current.Statuses = null;
                }
                else
                    Current.listStatuses.Single(q => q.Equals(status)).Update(status);
            }

            public static void AddRangeStatus(JToken[] jToken)
            {
                var statuses = jToken.Select(q => new Status(q)).ToArray();
                foreach (var status in statuses)
                {
                    if (!Current.listStatuses.Contains(status))
                    {
                        Current.listStatuses.Add(status);
                        Current.Statuses = null;
                    }
                    else
                        Current.listStatuses.Single(q => q.Equals(status)).Update(status);
                }
            }

            public static void AddRangeStatus(JArray jArray)
            {
                var statuses = jArray.OfType<JObject>().ToArray().Select(q => new Status(q));
                Current.listStatuses.Clear();
                Current.listStatuses.AddRange(statuses);
                Current.Statuses = null;
            }
            public override string ToString()
            {
                return name;
            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(string propertyName)
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public bool Equals(Status other)
            {
                return other.ID == this.ID;
            }
        }
    }
}
