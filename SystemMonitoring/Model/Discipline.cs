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
        public class Discipline : INotifyPropertyChanged, IEquatable<Discipline>
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

            private Discipline(JToken jObject)
            {
                this.ID = jObject.Value<int>("id");
                this.FullName = jObject.Value<string>("full_name");
                this.ShortName = jObject.Value<string>("socr");
            }
            private Discipline(JObject jObject)
            {
                this.ID = jObject.Value<int>("ID");
                this.FullName = jObject.Value<string>("FullName");
                this.ShortName = jObject.Value<string>("ShortName");
            }

            public static void AddDiscipline(JObject jObject)
            {
                var discipline = new Discipline(jObject);
                if (!Current.listDisciplines.Contains(discipline))
                {
                    Current.listDisciplines.Add(discipline);
                    Current.Disciplines = null;
                    Client.Current.GetData(Operation.get_ids_groups_for_dicsip, "id_discip=" + discipline.id);
                }
                else
                    Current.listDisciplines.Single(q => q.ID == discipline.id).Update(discipline);
            }

            public void Update(Discipline discipline)
            {
                this.ID = discipline.ID;
                this.FullName = discipline.FullName;
                this.ShortName = discipline.ShortName;
            }

            public static void AddRangeDiscipline(JToken[] jToken)
            {
                var disciplines = jToken.Select(q => new Discipline(q)).ToArray();
                foreach (var discipline in disciplines)
                {
                    if (!Current.listDisciplines.Contains(discipline))
                    {
                        Current.listDisciplines.Add(discipline);
                        Current.Disciplines = null;
                        Client.Current.GetData(Operation.get_ids_groups_for_dicsip, "id_discip=" + discipline.id);
                    }
                    else
                        Current.listDisciplines.Single(q => q.ID == discipline.id).Update(discipline);
                }

            }

            public static void AddRangeDiscipline(JArray jArray)
            {
                var disciplines = jArray.OfType<JObject>().ToArray().Select(q => new Discipline(q));
                Current.listDisciplines.Clear();
                Current.listDisciplines.AddRange(disciplines);
                Current.Disciplines = null;
            }

            public Group[] _Groups
            {
                get { return Current.disciplinesGroupses.Where(a => a.DisciplineID == this.ID).Select(q => q._Group).ToArray(); }
            }

            public override string ToString()
            {
                return this.ShortName;
            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(string propertyName)
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public bool Equals(Discipline other)
            {
                return other.ID == this.ID;
            }
        }
    }
}
