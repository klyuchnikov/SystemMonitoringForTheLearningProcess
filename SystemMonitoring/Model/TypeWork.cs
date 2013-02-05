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
        public class TypeWork : INotifyPropertyChanged, IEquatable<TypeWork>
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

            private TypeWork(JToken jObject)
            {
                this.id = jObject.Value<int>("id");
                this.name = jObject.Value<string>("name");
            }
            private TypeWork(JObject jObject)
            {
                this.id = jObject.Value<int>("ID");
                this.name = jObject.Value<string>("Name");
            }
            private void Update(TypeWork typeWork)
            {
                this.name = typeWork.name;
            }

            public static void AddTypeWork(JObject jObject)
            {
                var typeWork = new TypeWork(jObject);
                if (!Current.listTypeWorks.Contains(typeWork))
                {
                    Current.listTypeWorks.Add(typeWork);
                    Current.TypesWorks = null;
                }
                else
                    Current.listTypeWorks.Single(q => q.Equals(typeWork)).Update(typeWork);
            }

            public static void AddRangeTypeWork(JToken[] jToken)
            {
                var typeWorks = jToken.Select(q => new TypeWork(q)).ToArray();
                foreach (var typeWork in typeWorks)
                {
                    if (!Current.listTypeWorks.Contains(typeWork))
                    {
                        Current.listTypeWorks.Add(typeWork);
                        Current.TypesWorks = null;
                    }
                    else
                        Current.listTypeWorks.Single(q => q.Equals(typeWork)).Update(typeWork);
                }
            }
            public static void AddRangeTypeWork(JArray jArray)
            {
                var typeWorks = jArray.OfType<JObject>().ToArray().Select(q => new TypeWork(q));
                Current.listTypeWorks.Clear();
                Current.listTypeWorks.AddRange(typeWorks);
                Current.TypesWorks = null;
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

            public bool Equals(TypeWork other)
            {
                return other.ID == this.ID;
            }
        }
    }
}
