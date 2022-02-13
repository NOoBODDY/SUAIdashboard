using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Windows.Media;
using System.Windows;

namespace SUAIdashboard.Model
{
    public class LabWork : INotifyPropertyChanged
    {
        #region //interface
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        #region //generated
        public string id { get; set; }
        public string user_id { get; set; }
        public string datecreate { get; set; }
        public string dateupdate { get; set; }

        public string description { get; set; }
        public string type { get; set; }
        public string tt_name { get; set; }
        public string _public { get; set; }
        public string semester { get; set; }
        public string markpoint { get; set; }
        public string reportRequired { get; set; }
        public string url { get; set; }
        public string ordernum { get; set; }
        public string expulsionLine { get; set; }
        public string plenty { get; set; }

        public string grid { get; set; }
        public string subject { get; set; }

        public string filename { get; set; }
        public string semester_name { get; set; }
        public string type_name { get; set; }

        public string curPoints { get; set; }
        public string status_name { get; set; }
        #endregion


        public string name { get { return Name; } set { Name = value; OnPropertyChanged("name"); } } //
        string Name;
        
        public string harddeadline { 
            get { return Harddeadline; } 
            set 
            { 
                Harddeadline = value; 
                OnPropertyChanged("harddeadline");
                ClockBrush = Brushes.DimGray/*new SolidColorBrush(Color.FromRgb(67, 67, 68))*/;//#434344
                if (value !=null)
                {
                    DateTime date = DateTime.Now;
                    DateTime deadline = DateTime.Parse(value);
                    if (date > deadline)
                    {
                        ClockBrush = Brushes.Red;
                    }
                    else
                    {
                        date.AddDays(3);
                        if (date > deadline)
                        {
                            ClockBrush = Brushes.Yellow;
                        }
                    }
                }
               
                
            } 
        }//
        string Harddeadline;
        
        public string subject_name { get { return Subject_name; } set { Subject_name = value; OnPropertyChanged("subject_name"); } } //
        string Subject_name;
        public string hash { get; set; } //2
        
        public string status { get; set; } //2

        Brush clockBrush;
        public Brush ClockBrush { get { return clockBrush; } set { clockBrush = value; OnPropertyChanged("ClockBrush"); } }
        
        //public Visibility Visibility 
        //{ 
        //    get
        //    {
        //        if (Convert.ToInt32( status) == 2 )
        //        {
        //            return Visibility.Collapsed;
        //        }
        //        else
        //        {
        //            return Visibility.Visible;
        //        }
        //    }
        //}
    }
}
