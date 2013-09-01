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
    public class Anniversary : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // id of the anniversary
        private int _anniId;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int AnniId
        {
            get
            {
                return _anniId;
            }
            set
            {
                if (_anniId != value)
                {
                    NotifyPropertyChanging("AnniId");
                    _anniId = value;
                    NotifyPropertyChanged("AnniId");
                }
            }
        }

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

        // period of the anniversary (in days)
        private int _period;
        [Column]
        public int Period
        {
            get
            {
                return _period;
            }
            set
            {
                if (_period != value)
                {
                    NotifyPropertyChanging("Period");
                    _period = value;
                    NotifyPropertyChanged("Period");
                }
            }
        }

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
