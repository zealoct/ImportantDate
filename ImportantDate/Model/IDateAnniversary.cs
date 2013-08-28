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
    class IDateAnniversary : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // id, should be no use
        private int _id;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    NotifyPropertyChanging("Id");
                    _id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        // associated IDate id
        private int _iDateId;
        [Column]
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

        // associated anniversary id
        private int _anniId;
        [Column]
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
