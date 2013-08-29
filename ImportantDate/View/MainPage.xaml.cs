using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

using ImportantDate.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ImportantDate.View
{
    public partial class MainPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        private DatabaseContext db;

        private ObservableCollection<IDate> _iDates;
        public ObservableCollection<IDate> IDates
        {
            get
            {
                return _iDates;
            }
            set
            {
                if (_iDates != value)
                {
                    _iDates = value;
                    NotifyPropertyChanged("IDates");
                }
            }
        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            db = new DatabaseContext(DatabaseContext.DBConnectionString);

            this.DataContext = this;
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            db.SubmitChanges();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            var iDatesInDb = from IDate idate in db.IDates
                             select idate;

            IDates = new ObservableCollection<IDate>(iDatesInDb);
            IDates.Add(new IDate { Name = "日子1", Date = new DateTime(2013, 1, 13) });
            IDates.Add(new IDate { Name = "日子2", Date = new DateTime(2013, 4, 3) });

            base.OnNavigatedTo(e);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private void ApplicationBarIconButtonNew_Click(object sender, EventArgs e)
        {

        }
    }
}