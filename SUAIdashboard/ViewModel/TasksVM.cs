using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using SUAIdashboard.Model;

namespace SUAIdashboard.ViewModel
{
    internal class TasksVM : INotifyPropertyChanged
    {
        #region //interface
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion


    }
}
