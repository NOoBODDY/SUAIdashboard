using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using SUAIdashboard.Model;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;

namespace SUAIdashboard.ViewModel
{
    public class HomeVM : INotifyPropertyChanged
    {
        #region //interface
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        #region //Properties
        UserSettings settings;
        public UserSettings Settings
        {
            get { return settings; }
            set { settings = value; OnPropertyChanged("Settings"); }
        }

        Uri framePage;
        public Uri FramePage
        {
            get { return framePage; }
            set
            {
                framePage = value;
                OnPropertyChanged("FramePage");
            }
        }

        ObservableCollection<LabWork> labWorks;
        public ObservableCollection<LabWork> LabWorks
        {
            get { return labWorks; }
            set { labWorks = value; OnPropertyChanged("LabWorks"); }
        }

        ObservableCollection<LabsSubjects> labsSubject;
        public ObservableCollection<LabsSubjects> LabsSubject
        {
            get { return labsSubject; }
            set { labsSubject = value; OnPropertyChanged("LabsSubject"); }
        }

        bool subjectORtask;
        public Visibility Subject { get { return subjectORtask? Visibility.Visible: Visibility.Collapsed; } set { subjectORtask = value == Visibility.Visible ; OnPropertyChanged("Subject"); } }

        public Visibility Task { get { return !subjectORtask ? Visibility.Visible : Visibility.Collapsed; ; } set { subjectORtask = !(value == Visibility.Visible); OnPropertyChanged("Task"); } }

        Visibility visibilityBtn;
        public Visibility VisibilityBtn { get { return visibilityBtn; } set { visibilityBtn = value; OnPropertyChanged("VisibilityBtn"); } }

        string label;
        public string Label { get { return label; } set { label = value; OnPropertyChanged("label"); } }

        #endregion

        #region //Commands

        //menu buttons' commands
        RelayCommand changeVisibility;
        public RelayCommand ChangeVisibility
        {
            get
            {
                return changeVisibility ??
                  (changeVisibility = new RelayCommand(obj =>
                  {
                      if (Visibility.Collapsed == VisibilityBtn)
                      {
                          VisibilityBtn = Visibility.Visible;
                      }
                      else
                      {
                          VisibilityBtn = Visibility.Collapsed;
                      }
                  }));
            }
        }

        RelayCommand sortDeadline;
        public RelayCommand SortDeadline
        {
            get
            {
                return sortDeadline ??
                  (sortDeadline = new RelayCommand(obj =>
                  {
                      if (!(bool) obj)
                      {
                          LabWorks = new ObservableCollection<LabWork>(LabWorks.OrderBy(x => DateTime.Parse(x.harddeadline ?? "3000-01-01")));
                      }
                      else
                      {
                          LabWorks = new ObservableCollection<LabWork>(LabWorks.OrderByDescending(x => DateTime.Parse(x.harddeadline ?? "1970-01-01")));
                      }
                  }));
            }
        }
        #endregion


        SUAI user;

        public HomeVM(UserSettings userSettings)
        {
            VisibilityBtn = Visibility.Collapsed;
            Settings = userSettings;
            subjectORtask = userSettings.SubjectGroup;
            Label = "Loading...";
            Task task = new Task(Load);
            task.Start();
        }

        void Load()
        {
            user = new SUAI(Settings.Username, Settings.Password);
            user.ErrorNotify += displayError;
            lock(Label)
            {
                Label = "Loading...1";
            }
            user.getPHPSessID();
            lock (Label)
            {
                Label = "Loading...2";
            }
            user.LogIn();
            lock (Label)
            {
                Label = "Loading...3";
            }
            user.GetUserID();
            lock (Label)
            {
                Label = "Loading...4";
            }

            LabWorks = user.GetTasks();
            LabsSubject = SplitLabs(LabWorks);
            lock (Label)
            {
                Label = "";
            }
        }

        void displayError(string message)
        {
            MessageBox.Show(message);
        }

        ObservableCollection<LabsSubjects> SplitLabs(ObservableCollection<LabWork> labs)
        {
            ObservableCollection<LabsSubjects> labsSubjects = new ObservableCollection<LabsSubjects>();
            var subjects = labs.GroupBy(x => x.subject_name);
            foreach(var subject in subjects)
            {
                LabsSubjects exp = new LabsSubjects();
                exp.Name = subject.Key;
                exp.LabWorks = new ObservableCollection<LabWork>(subject.ToList());
                labsSubjects.Add(exp);
            }
            return labsSubjects;
        }

    }
}
