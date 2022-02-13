using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SUAIdashboard.Model
{
    public class UserSettings : INotifyPropertyChanged
    {
        #region //interface
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        #region //Propetries

        string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged("Password"); }
        }

        bool delWeekAgo;
        public bool DelWeekAgo
        {
            get { return delWeekAgo; }
            set { delWeekAgo = value; OnPropertyChanged("DelWeekAgo"); }
        }

        bool browserUsing;
        public bool BrowserUsing
        {
            get { return browserUsing; }
            set { browserUsing = value; OnPropertyChanged("BrowserUsing"); }
        }

        #endregion
    }
}
