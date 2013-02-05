using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using System.ComponentModel;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;


namespace SystemMonitoring
{
    public struct TokenInfo
    {
        /// <summary>
        /// Token
        /// </summary>
        public string AccessToken;
        /// <summary>
        /// Date of receipt of token
        /// </summary>
        public DateTime TokenDataStart;
        /// <summary>
        /// Expires in secund
        /// </summary>
        public int Expires;
        public TokenInfo(string tokenString, DateTime tokenDataStart, int expires)
        {
            this.Expires = expires;
            this.TokenDataStart = tokenDataStart;
            this.AccessToken = tokenString;
        }
    }
    public class Client : IDisposable, INotifyPropertyChanged
    {
        private Client()
        {
            Load();
        }

        public static Client Current = new Client();
        public Page MainPage { get; set; }

        private string _login;
        public string Login
        {
            get { return _login; }
            private set
            {
                _login = value;
                NotifyPropertyChanged("Login");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            private set
            {
                _password = value;
                NotifyPropertyChanged("Password");
            }
        }

        private bool _isAuthorized;
        public bool IsAuthorized
        {
            get { return _isAuthorized; }
            private set
            {
                _isAuthorized = value;
                if (value)
                    this.Save();
                NotifyPropertyChanged("IsAuthorized");
            }
        }

        private bool isIndeterminate;
        public bool IsIndeterminate
        {
            get { return isIndeterminate; }
            private set
            {
                isIndeterminate = value;
                if (value)
                    this.Save();
                NotifyPropertyChanged("IsIndeterminate");
            }
        }

        public TokenInfo Token { get; private set; }
        private bool _isFirstConnected;

        public bool IsFirstConnected
        {
            get { return _isFirstConnected; }
            private set
            {
                _isFirstConnected = value;
                NotifyPropertyChanged("IsFirstConnected");
                if (value)
                    if (!IsAuthorized)
                        if (MainPage == null)
                        {
                            var dt = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
                            dt.Tick += delegate
                            {
                                if (MainPage == null) return;
                                MainPage.Dispatcher.BeginInvoke(
                                    () => MainPage.NavigationService.Navigate(new Uri("/Views/Auth.xaml", UriKind.Relative)));
                                dt.Stop();
                            };
                            dt.Start();
                        }
                        else
                            MainPage.Dispatcher.BeginInvoke(
                                () => MainPage.NavigationService.Navigate(new Uri("/Views/Auth.xaml", UriKind.Relative)));

            }
        }

        public bool Expired
        {
            get
            {
                if (string.IsNullOrEmpty(Token.AccessToken))
                    return true;
                return DateTime.Now > Token.TokenDataStart.AddSeconds(this.Token.Expires);
            }
        }
        public void Auth(string login, string pass)
        {
            if (IsAuthorized) return;
            this.Login = login;
            this.Password = pass;
            var client = new RestClient(string.Format("{0}?response_type=token&client_id={1}&redirect_uri={2}", this.uriAuth, login, this.uriRedirectSuccess));

            var request = new RestRequest(Method.GET);
            var response = client.ExecuteAsync(request, delegate(IRestResponse args)
            {
                if (args.ResponseUri.LocalPath == "/success.html" && args.ResponseUri.Fragment.Length > 0)
                {
                    var arr = args.ResponseUri.Fragment.Substring(1).Split('&').Select(q => q.Split('=')).ToArray();
                    var access_token = arr.Single(q => q[0] == "access_token")[1];
                    var expires_in = int.Parse(arr.Single(q => q[0] == "expires_in")[1]);
                    var refresh_token = arr.Single(q => q[0] == "refresh_token")[1];
                    this.Token = new TokenInfo(access_token, DateTime.Now, expires_in);
                    this.IsAuthorized = true;
                    this.GetStartData();
                    return;
                }
                this.IsFirstConnected = true;
                this.IsAuthorized = false;
            });
        }

        public void Save()
        {
            if (this.Token.AccessToken != null)
                IsolatedStorageSettings.ApplicationSettings["AccessToken"] = this.Token.AccessToken;
            IsolatedStorageSettings.ApplicationSettings["Expires"] = this.Token.Expires;
            IsolatedStorageSettings.ApplicationSettings["TokenDataStart"] = this.Token.TokenDataStart;
            if (this.Login != null)
                IsolatedStorageSettings.ApplicationSettings["Login"] = this.Login;
            if (this.Password != null)
                IsolatedStorageSettings.ApplicationSettings["Password"] = this.Password;

            var json = JsonConvert.SerializeObject(Model.Model.Current);
            IsolatedStorageSettings.ApplicationSettings["ModelJSON"] = json;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
        public void Load()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("ModelJSON"))
            {
                var modelJSON = IsolatedStorageSettings.ApplicationSettings["ModelJSON"] as string;
                var model = JsonConvert.DeserializeObject(modelJSON);
                Model.Model.Current.Load(model as JToken);
            }
            if (IsolatedStorageSettings.ApplicationSettings.Contains("AccessToken"))
            {
                var accessToken = IsolatedStorageSettings.ApplicationSettings["AccessToken"] as string;
                var expires = (int)IsolatedStorageSettings.ApplicationSettings["Expires"];
                var tokenDataStart = (DateTime)IsolatedStorageSettings.ApplicationSettings["TokenDataStart"];
                this.Token = new TokenInfo(accessToken, tokenDataStart, expires);
            }
            if (IsolatedStorageSettings.ApplicationSettings.Contains("Password"))
                this.Password = IsolatedStorageSettings.ApplicationSettings["Password"] as string;
            if (IsolatedStorageSettings.ApplicationSettings.Contains("Login"))
                this.Login = IsolatedStorageSettings.ApplicationSettings["Login"] as string;

            if (string.IsNullOrEmpty(this.Token.AccessToken))
                if (!string.IsNullOrEmpty(this.Login) || !string.IsNullOrEmpty(this.Password))
                    this.Auth(this.Login, this.Password);
                else
                    this.IsFirstConnected = true;
            else
                if (this.Expired)
                    this.Auth(this.Login, this.Password);
                else
                {
                    this.IsAuthorized = true;
                    this.IsFirstConnected = true;
                    //   GetStartData();
                }

        }

        private string host = "http://klyuchnikovds.byethost7.com/";

        private string uriResource
        {
            get { return host + "pdo/protected_resource.php"; }
        }
        private string uriAuth
        {
            get { return host + "pdo/authorize.php"; }
        }
        private string uriRedirectSuccess
        {
            get { return host + "success.html"; }
        }

        public void GetStartData()
        {
            GetData(Operation.get_ids_discip);
            GetData(Operation.get_ids_prepod);
            GetData(Operation.get_ids_status);
            GetData(Operation.get_ids_student);
            GetData(Operation.get_ids_etap_work);
            GetData(Operation.get_ids_type_work);
            GetData(Operation.get_ids_groups);
            // get ids entities
        }

        public void GetData(Operation operation, string paramString = null)
        {
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                //App.Current.RootVisual.
                var progressIndicator = SystemTray.GetProgressIndicator(Application.Current.RootVisual);
                if (progressIndicator == null)
                {
                    progressIndicator = new ProgressIndicator { IsVisible = true };
                    SystemTray.SetProgressIndicator(Application.Current.RootVisual, progressIndicator);
                }
                progressIndicator.IsIndeterminate = true;
                SystemTray.SetProgressIndicator(Application.Current.RootVisual, progressIndicator);
            });
            var wc = new WebClient();
            RestClient client;
            if (paramString == null)
                client = new RestClient(string.Format("{0}?oauth_token={1}&method={2}", this.uriResource, this.Token.AccessToken, operation));
            else
                client = new RestClient(string.Format("{0}?oauth_token={1}&method={2}&{3}", this.uriResource, this.Token.AccessToken, operation, paramString));

            var request = new RestRequest(Method.GET);
            var response = client.ExecuteAsync(request, delegate(IRestResponse args)
            {
                var _operation = operation;
                try
                {
                    if (args.Content == "Connection failed: SQLSTATE[42000] [1203] User a5647817_user already has more than 'max_user_connections' active connections")
                        return;
                    if (args.Content == "")
                        GetData(_operation, paramString);
                    var o = JsonConvert.DeserializeObject<JToken>(args.Content);
                    if (o.Type == JTokenType.Object)
                        if ((o as JObject).Property("error") != null)
                            return;
                    var ob = o.ToArray();
                    if (o.Type == JTokenType.Object)
                        if ((o as JObject).Count == 0)
                            return;
                    if (!ob.Any())
                        return;
                    int[] ids;
                    switch (_operation)
                    {
                        // get ids entities
                        case Operation.get_ids_discip:
                            ids = ob.Values<int>().Where(id => Model.Model.Current.Disciplines.All(a => a.ID != id)).ToArray();
                            if (ids.Length > 0)
                                GetData(Operation.get_discip, "ids=" + string.Join(",", ids));
                            break;
                        case Operation.get_ids_prepod:
                            ids = ob.Values<int>().Where(id => Model.Model.Current.Teachers.All(a => a.ID != id)).ToArray();
                            if (ids.Length > 0)
                                GetData(Operation.get_prepod, "ids=" + string.Join(",", ids));
                            foreach (var source in ob.Values<int>().ToArray())
                                Client.Current.GetData(Operation.get_ids_discip_prepod, "id_prepod=" + source);
                            break;
                        case Operation.get_ids_status:
                            ids = ob.Values<int>().Where(id => Model.Model.Current.Statuses.All(a => a.ID != id)).ToArray();
                            if (ids.Length > 0)
                                GetData(Operation.get_status, "ids=" + string.Join(",", ids));
                            break;
                        case Operation.get_ids_student:
                            ids = ob.Values<int>().Where(id => Model.Model.Current.Students.All(a => a.ID != id)).ToArray();
                            if (ids.Length > 0)
                                GetData(Operation.get_student, "ids=" + string.Join(",", ids));
                            Client.Current.GetData(Operation.get_ids_hystory_stud, "id_stud=" + string.Join(",", ob.Values<int>().ToArray()));
                            break;
                        case Operation.get_ids_etap_work:
                            ids = ob.Values<int>().Where(id => Model.Model.Current.Works.All(a => a.ID != id)).ToArray();
                            if (ids.Length > 0)
                                GetData(Operation.get_etap_work, "ids=" + string.Join(",", ids));
                            break;
                        case Operation.get_ids_type_work:
                            ids = ob.Values<int>().Where(id => Model.Model.Current.TypesWorks.All(a => a.ID != id)).ToArray();
                            if (ids.Length > 0)
                                GetData(Operation.get_type_work, "ids=" + string.Join(",", ids));
                            break;
                        case Operation.get_ids_groups:
                            ids = ob.Values<int>().Where(id => Model.Model.Current.Groups.All(a => a.ID != id)).ToArray();
                            if (ids.Length > 0)
                                GetData(Operation.get_groups, "ids=" + string.Join(",", ids));
                            break;
                        case Operation.get_discip:
                            Model.Model.Discipline.AddRangeDiscipline(ob);
                            break;
                        case Operation.get_prepod:
                            Model.Model.Teacher.AddRangeTeacher(ob);
                            break;
                        case Operation.get_status:
                            Model.Model.Status.AddRangeStatus(ob);
                            break;
                        case Operation.get_student:
                            Model.Model.Student.AddRangeStudent(ob);
                            break;
                        case Operation.get_etap_work:
                            Model.Model.Work.AddRangeWork(ob);
                            break;
                        case Operation.get_type_work:
                            Model.Model.TypeWork.AddRangeTypeWork(ob);
                            break;
                        case Operation.get_groups:
                            Model.Model.Group.AddRangeGroup(ob);
                            break;

                        case Operation.get_ids_hystory_stud:
                            ids = ob.Values<int>().Where(id => Model.Model.Current.HistoriesStudents.All(a => a.ID != id)).ToArray();
                            if (ids.Length > 0)
                                GetData(Operation.get_hystory_stud, "ids=" + string.Join(",", ids));
                            break;
                        case Operation.get_hystory_stud:
                            Model.Model.HistoryStudent.AddRangeHistoryStudent(ob);
                            break;

                        case Operation.get_ids_groups_for_dicsip:
                            var id_discip = int.Parse(paramString.Split('=')[1]);
                            var ids_groups = ob.Values<int>().ToArray();
                            Model.Model.DisciplinesGroups.AddRangeDisciplinesGroups(id_discip, ids_groups);
                            break;

                        case Operation.get_ids_discip_prepod:
                            ids = ob.Values<int>().Where(id => Model.Model.Current.DisciplinesTeacherses.All(a => a.ID != id)).ToArray();
                            if (ids.Length > 0)
                                GetData(Operation.get_discip_prepod, "ids=" + string.Join(",", ids));
                            break;
                        case Operation.get_discip_prepod:
                            Model.Model.DisciplinesTeachers.AddRangeDisciplinesTeachers(ob);
                            break;

                        case Operation.get_ids_discip_prepod_type_work:
                            ids = ob.Values<int>().Where(id => Model.Model.Current.DisciplinesTeachersTypeWorks.All(a => a.ID != id)).ToArray();
                            if (ids.Length > 0)
                                GetData(Operation.get_discip_prepod_type_work, "ids=" + string.Join(",", ids));
                            break;
                        case Operation.get_discip_prepod_type_work:
                            Model.Model.DisciplinesTeachersTypeWork.AddRangeDisciplinesTeachersTypeWork(ob);
                            break;

                        case Operation.get_work_stud:
                            Model.Model.PassingWork.AddRangePassingWork(ob);
                            break;

                        case Operation.getAllComment_discip:
                            Model.Model.CommentDisciplineForStudent.AddRangeCommentDisciplineForStudent(ob);
                            break;

                        case Operation.getAllLectures:
                            Model.Model.AttendingLectures.AddRangeAttendingLectures(ob);
                            break;
                    }
                    Model.Model.Current.CurrentTeacher = null;
                    foreach (var teacher in Model.Model.Current.Teachers)
                        teacher._DisciplinesTeachers = null;
                    Deployment.Current.Dispatcher.BeginInvoke(delegate
                    {
                        //App.Current.RootVisual.
                        var progressIndicator = SystemTray.GetProgressIndicator(Application.Current.RootVisual);
                        if (progressIndicator == null)
                        {
                            progressIndicator = new ProgressIndicator { IsVisible = true };
                            SystemTray.SetProgressIndicator(Application.Current.RootVisual, progressIndicator);
                        }
                        //   progressIndicator.IsIndeterminate = false;
                    });
                }
                catch (Exception ex)
                {
                    try
                    {
                        // Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(_operation.ToString() + "  " + ex.Message));
                        GetData(_operation, paramString);
                    }
                    catch (Exception)
                    {
                    }
                }
            });
        }
        public void Dispose()
        {
            Save();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
