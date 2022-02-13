using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using SUAIdashboard.Model;
using System.Threading.Tasks;
using System.Windows;

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

        string label;
        public string Label { get { return label; } set { label = value; OnPropertyChanged("label"); } }

        #endregion

        SUAI user;

        public HomeVM(UserSettings userSettings)
        {
            Settings = userSettings;
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
            lock (Label)
            {
                Label = "";
            }
        }

        void displayError(string message)
        {
            MessageBox.Show(message);
        }


    }
}
