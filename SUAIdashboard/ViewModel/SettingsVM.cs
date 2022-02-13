using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using SUAIdashboard.Model;

namespace SUAIdashboard.ViewModel
{
    public class SettingsVM : INotifyPropertyChanged
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

        #endregion

        public SettingsVM(ref UserSettings settings)
        {
            DataKeeper<UserSettings> dataKeeper = new DataKeeper<UserSettings>("settings.json");
            if (Settings !=null)
            {
                
                Settings = settings;
                this.settings = settings;
                UserSettings userSettings = dataKeeper.Load();
                if (userSettings != null)
                {
                    Settings = userSettings;
                    settings = userSettings;
                }
                else
                {
                    dataKeeper.SaveAsync(Settings);
                }
            }
            else
            {
                UserSettings userSettings = dataKeeper.Load();
                if (userSettings != null)
                {
                    Settings = userSettings;
                    settings = userSettings;
                }
                else
                {
                    Settings = new UserSettings();
                    settings = new UserSettings();
                    dataKeeper.SaveAsync(Settings);
                }
               
            }
            
        }


        #region //Commands
        RelayCommand apply;
        public RelayCommand Apply
        {
            get
            {
                return apply ??
                  (apply = new RelayCommand(obj =>
                  {
                      DataKeeper<UserSettings> dataKeeper = new DataKeeper<UserSettings>("settings.json");
                      dataKeeper.SaveAsync(Settings);
                  }));
            }
        }
        #endregion
    }
}
