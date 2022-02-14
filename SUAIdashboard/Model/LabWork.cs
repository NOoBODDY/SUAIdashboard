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
            } 
        }//
        string Harddeadline;
        
        public string subject_name { get { return Subject_name; } set { Subject_name = value; OnPropertyChanged("subject_name"); } } //
        string Subject_name;
        public string hash { get; set; } //2
        
        public string status { get; set; } //2

        public Brush ClockBrush 
        { 
            get 
            {
                if (Harddeadline != null)
                {
                    DateTime date = DateTime.Now;
                    DateTime deadline = DateTime.Parse(Harddeadline);
                    if (date > deadline)
                    {
                        return Brushes.Red;
                    }
                    else
                    {
                        date = date.AddDays(3);
                        if (date > deadline)
                        {
                            return Brushes.YellowGreen;
                        }
                    }
                }
                return new SolidColorBrush(Color.FromRgb(67, 67, 68));
            } 
        }

        public Visibility Visibility
        {
            get
            {
                if (status == "2")
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        double BackroundOpacity = 0.5;

        public Brush StatusBrush
        {
            get
            {
                Brush brush;
                switch (status)
                {
                    case "1":
                        brush = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                        brush.Opacity = BackroundOpacity;
                        return brush;//YellowColor
                    case "2":
                        brush = new SolidColorBrush(Color.FromRgb(19, 224, 4));
                        brush.Opacity = BackroundOpacity;
                        return brush; //GreenColor
                    case "3":
                        brush = new SolidColorBrush(Color.FromRgb(219, 4, 0));
                        brush.Opacity = BackroundOpacity;
                        return brush; //RedColor
                    default:
                        return Brushes.Transparent;
                }
            }
        }

    }
}
