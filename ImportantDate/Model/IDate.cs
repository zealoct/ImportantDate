using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace ImportantDate.Model
{  
    [Table]
    public class IDate : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // id of the important date
        private int _iDateId;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int IDateId
        {
            get
            {
                return _iDateId;
            }
            set
            {
                if (_iDateId != value)
                {
                    NotifyPropertyChanging("IDateId");
                    _iDateId = value;
                    NotifyPropertyChanged("IDateId");
                }
            }
        }

        // name of the important date
        private string _name;
        [Column]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    NotifyPropertyChanging("Name");
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        // date of the important date
        private DateTime _date;
        [Column]
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (_date != value)
                {
                    NotifyPropertyChanging("Date");
                    _date = value;
                    _dayToToday = (int)(DateTime.Today - _date).TotalDays;
                    NotifyPropertyChanged("Date");
                }
            }
        }

        // days have passed since the day
        private int _dayToToday;
        public int DayToToday { get { return _dayToToday; } }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}
