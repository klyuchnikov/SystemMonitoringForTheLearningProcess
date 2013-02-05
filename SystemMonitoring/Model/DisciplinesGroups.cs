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
        public class DisciplinesGroups : INotifyPropertyChanged, IEquatable<DisciplinesGroups>
        {
            #region GroupID
            private int groupID;
            public int GroupID
            {
                get { return groupID; }
                private set
                {
                    groupID = value;
                    NotifyPropertyChanged("GroupID");
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

            private DisciplinesGroups(JToken jObject)
            {
                this.groupID = jObject.Value<int>("id_group");
                this.disciplineID = jObject.Value<int>("id_discip");
            }
            private DisciplinesGroups(int groupID, int disciplineID)
            {
                this.groupID = groupID;
                this.disciplineID = disciplineID;
            }
            private DisciplinesGroups(JObject jObject)
            {
                this.groupID = jObject.Value<int>("GroupID");
                this.disciplineID = jObject.Value<int>("DisciplineID");
            }

            public static void AddDisciplinesGroups(JObject jObject)
            {
                var disciplinesGroups = new DisciplinesGroups(jObject);
                if (Current.disciplinesGroupses.Contains(disciplinesGroups)) return;
                Current.disciplinesGroupses.Add(disciplinesGroups);
                Current.DisciplinesGroupses = null;
            }

            public static void AddRangeDisciplinesGroups(JToken[] jToken)
            {
                var disciplinesGroups = jToken.Select(q => new DisciplinesGroups(q)).ToArray();
                foreach (var disciplinesGroupse in disciplinesGroups.Where(disciplinesGroupse => !Current.disciplinesGroupses.Contains(disciplinesGroupse)))
                {
                    Current.disciplinesGroupses.Add(disciplinesGroupse);
                    Current.DisciplinesGroupses = null;
                }
            }

            public static void AddRangeDisciplinesGroups(int id_discip, int[] ids_groups)
            {
                var disciplinesGroups = ids_groups.Select(q => new DisciplinesGroups(q, id_discip)).ToArray();
                foreach (var disciplinesGroupse in disciplinesGroups.Where(disciplinesGroupse => !Current.disciplinesGroupses.Contains(disciplinesGroupse)))
                {
                    Current.disciplinesGroupses.Add(disciplinesGroupse);
                    Current.DisciplinesGroupses = null;
                }
            }

            public static void AddRangeDisciplinesGroups(JArray jArray)
            {
                var disciplinesGroups = jArray.OfType<JObject>().ToArray().Select(q => new DisciplinesGroups(q));
                Current.disciplinesGroupses.Clear();
                Current.disciplinesGroupses.AddRange(disciplinesGroups);
                Current.DisciplinesGroupses = null;
            }

            [JsonIgnore]
            public Discipline _Discipline
            {
                get { return Model.Current.listDisciplines.Single(q => q.ID == this.disciplineID); }
            }

            [JsonIgnore]
            public Group _Group
            {
                get { return Model.Current.listGroup.Single(q => q.ID == this.groupID); }
            }
            public override string ToString()
            {
                return "!!!___DisciplinesGroupses___!!!";
            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(string propertyName)
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public bool Equals(DisciplinesGroups other)
            {
                return other.DisciplineID == this.DisciplineID && other.GroupID == this.GroupID;
            }
        }
    }
}
