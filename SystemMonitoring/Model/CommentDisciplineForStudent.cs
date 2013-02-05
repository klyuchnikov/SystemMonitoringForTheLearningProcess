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

        public class CommentDisciplineForStudent : INotifyPropertyChanged, IEquatable<CommentDisciplineForStudent>
        {
            #region StudentID
            private int studentID;
            public int StudentID
            {
                get { return studentID; }
                private set
                {
                    studentID = value;
                    NotifyPropertyChanged("StudentID");
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
            #region Comment
            private string comment;
            public string Comment
            {
                get { return comment; }
                private set
                {
                    comment = value;
                    NotifyPropertyChanged("Comment");
                }
            }
            #endregion
            private CommentDisciplineForStudent(JToken jObject)
            {
                this.studentID = jObject.Value<int>("id_stud");
                this.disciplineID = jObject.Value<int>("id_discip");
                this.comment = jObject.Value<string>("comment");
            }
            private CommentDisciplineForStudent(JObject jObject)
            {
                this.studentID = jObject.Value<int>("StudentID");
                this.disciplineID = jObject.Value<int>("DisciplineID");
                this.comment = jObject.Value<string>("Comment");
            }

            public static void AddCommentDisciplineForStudent(JObject jObject)
            {
                var commentsDisciplineForStudent = new CommentDisciplineForStudent(jObject);
                if (!Current.commentsDisciplineForStudent.Contains(commentsDisciplineForStudent))
                {
                    Current.commentsDisciplineForStudent.Add(commentsDisciplineForStudent);
                    Current.CommentsDisciplineForStudent = null;
                }
                else
                    Current.commentsDisciplineForStudent.Single(q => q.Equals(commentsDisciplineForStudent)).Update(commentsDisciplineForStudent);
            }

            private void Update(CommentDisciplineForStudent commentsDisciplineForStudent)
            {
                this.comment = commentsDisciplineForStudent.comment;
            }

            public static void AddRangeCommentDisciplineForStudent(JToken[] jToken)
            {
                var commentsDisciplineForStudent = jToken.Select(q => new CommentDisciplineForStudent(q)).ToArray();
                foreach (var commentDisciplineForStudent in commentsDisciplineForStudent)
                {
                    if (!Current.commentsDisciplineForStudent.Contains(commentDisciplineForStudent))
                    {
                        Current.commentsDisciplineForStudent.Add(commentDisciplineForStudent);
                        Current.CommentsDisciplineForStudent = null;
                    }
                    else
                        Current.commentsDisciplineForStudent.Single(q => q.Equals(commentsDisciplineForStudent)).Update(commentDisciplineForStudent);
                }
            }

            public static void AddRangeCommentDisciplineForStudent(JArray jArray)
            {
                var commentsDisciplineForStudent = jArray.OfType<JObject>().ToArray().Select(q => new CommentDisciplineForStudent(q));
                Current.commentsDisciplineForStudent.Clear();
                Current.commentsDisciplineForStudent.AddRange(commentsDisciplineForStudent);
                Current.CommentsDisciplineForStudent = null;
            }
            public override string ToString()
            {
                return "!!!___CommentDisciplineForStudent___!!!";
            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(string propertyName)
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public bool Equals(CommentDisciplineForStudent other)
            {
                return other.DisciplineID == this.DisciplineID && other.StudentID == this.StudentID;
            }
        }
    }
}
