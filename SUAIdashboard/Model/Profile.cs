using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SUAIdashboard.Model
{
    public class Profile : INotifyPropertyChanged
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
        string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }

        //Settings
        

        //

        #endregion
    }
}
