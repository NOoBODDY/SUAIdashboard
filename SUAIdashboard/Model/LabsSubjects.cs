using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace SUAIdashboard.Model
{
    public class LabsSubjects : INotifyPropertyChanged
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
        string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }

        ObservableCollection<LabWork> labWorks;
        public ObservableCollection<LabWork> LabWorks { get { return labWorks; } set { labWorks = value; OnPropertyChanged("LabWorks"); } }
        #endregion
    }
}
