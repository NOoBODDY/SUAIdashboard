using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SUAIdashboard.Model;
using SUAIdashboard.View;

namespace SUAIdashboard.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        #region //interface
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        #endregion

        public MainVM()
        {
            if (LoadSettings())
            {
                GoHome.Execute(null);
            }
            else
            {
                GoSettings.Execute(null);
            }
        }

        #region //Properties
        Uri framePage;
        public Uri FramePage { 
            get { return framePage; }
            set { 
                framePage = value;
                OnPropertyChanged("FramePage");
                }
        }

        object frameContext;
        public object FrameContext
        {
            get { return frameContext; }
            set { frameContext = value; OnPropertyChanged("FrameContext"); }
        }

        Page page;
        public Page Page
        {
            get { return page; }
            set
            {
                page = value; OnPropertyChanged("Page");
            }
        }

        UserSettings settings;
        public UserSettings Settings
        {
            get { return settings; }
            set { settings = value; OnPropertyChanged("Settings"); }
        }

        ObservableCollection<LabWork> labWorks;
        public ObservableCollection<LabWork> LabWorks 
        { 
            get { return labWorks; } 
            set { labWorks = value; OnPropertyChanged("LabWorks"); } 
        }

        #endregion

        #region //Commands

        //menu buttons' commands
        RelayCommand goHome;
        public RelayCommand GoHome
        {
            get { return goHome ??
                    (goHome = new RelayCommand(obj =>
                    {
                        HomeVM VM = new HomeVM(Settings);
                        Page = new HomePage();
                        Page.DataContext = VM;
                    })); 
                }
        }

        RelayCommand goTasks;
        public RelayCommand GoTasks
        {
            get
            {
                return goTasks ??
                  (goTasks = new RelayCommand(obj =>
                  {
                      TasksVM VM = new TasksVM();
                      Page = new TasksPage();
                      Page.DataContext = VM;
                      
                  }));
            }
        }

        RelayCommand goSettings;
        public RelayCommand GoSettings
        {
            get
            {
                return goSettings ??
                  (goSettings = new RelayCommand(obj =>
                  {
                      //SettingsVM VM = new SettingsVM(ref settings);
                      //FrameContext = VM;
                      //VM.FramePage = new Uri("SettingsPage.xaml", UriKind.RelativeOrAbsolute);

                      Page = new SettingsPage();
                      Page.DataContext = new SettingsVM(ref settings);

                  }));
            }
        }

        //window controls' buttons
        //yes, I know that it's a shit to use Window class in VM
        RelayCommand winClose;
        public RelayCommand WinClose
        {
            get
            {
                return winClose ??
                  (winClose = new RelayCommand(obj =>
                  {
                      Window window = obj as Window;
                      window.Close();  
                  }));
            }
        }

        RelayCommand winRestore;
        public RelayCommand WinRestore
        {
            get
            {
                return winRestore ??
                  (winRestore = new RelayCommand(obj =>
                  {
                      Window window = obj as Window;
                      if (window.WindowState == WindowState.Normal)
                          window.WindowState = WindowState.Maximized;
                      else
                          window.WindowState = WindowState.Normal;
                  }));
            }
        }

        RelayCommand winMinimize;
        public RelayCommand WinMinimize
        {
            get
            {
                return winMinimize ??
                  (winClose = new RelayCommand(obj =>
                  {
                      Window window = obj as Window;
                      window.WindowState = WindowState.Minimized;
                  }));
            }
        }

        #endregion

        bool LoadSettings()
        {
            DataKeeper<UserSettings> dataKeeper = new DataKeeper<UserSettings>("settings.json");
            UserSettings userSettings = dataKeeper.Load();
            if (userSettings != null)
            {
                Settings = userSettings;

                if (userSettings.Username != null && userSettings.Username != "" && userSettings.Password !=null && userSettings.Password != "")
                {
                    return true;
                }

                return false;
            }
            return false;
        }
    }
}
