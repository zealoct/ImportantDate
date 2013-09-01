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
        DatabaseContext db;
        //private IDate IDateToPass;

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

        private ObservableCollection<Anniversary> _anniversaries;
        public ObservableCollection<Anniversary> Anniversaries
        {
            get
            {
                return _anniversaries;
            }
            set
            {
                if (_anniversaries != value)
                {
                    _anniversaries = value;
                    NotifyPropertyChanged("Anniversaries");
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
            //if (e.Uri.Equals(new Uri("/View/EditIDatePage.xaml", UriKind.Relative)))
            //{
            //    EditIDatePage destPage = e.Content as EditIDatePage;
            //    if(destPage != null)
            //        destPage.IDate = IDateToPass;
            //}
            base.OnNavigatedFrom(e);
            db.SubmitChanges();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.IDates);
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.IDateAnniversaries);
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.Anniversaries);

            var iDatesInDb = from IDate idate in db.IDates
                             select idate;

            IDates = new ObservableCollection<IDate>(iDatesInDb);
            IDates.Add(new IDate { Name = "日子1", Date = new DateTime(2013, 1, 13) });
            IDates.Add(new IDate { Name = "日子2", Date = new DateTime(2013, 4, 3) });
            IDates.Add(new IDate { Name = "日子3", Date = new DateTime(2013, 1, 13) });
            IDates.Add(new IDate { Name = "日子4", Date = new DateTime(2013, 4, 3) });
            IDates.Add(new IDate { Name = "日子5", Date = new DateTime(2013, 1, 13) });
            IDates.Add(new IDate { Name = "日子6", Date = new DateTime(2013, 4, 3) });
            IDates.Add(new IDate { Name = "日子7", Date = new DateTime(2013, 1, 13) });
            IDates.Add(new IDate { Name = "日子8", Date = new DateTime(2013, 4, 3) });
            IDates.Add(new IDate { Name = "日子9", Date = new DateTime(2013, 1, 13) });
            IDates.Add(new IDate { Name = "日子10", Date = new DateTime(2013, 4, 3) });
            IDates.Add(new IDate { Name = "日子11", Date = new DateTime(2013, 1, 13) });
            IDates.Add(new IDate { Name = "日子12", Date = new DateTime(2013, 1, 13) });
            IDates.Add(new IDate { Name = "日子13", Date = new DateTime(2013, 4, 3) });
            IDates.Add(new IDate { Name = "日子14", Date = new DateTime(2013, 1, 13) });
            IDates.Add(new IDate { Name = "日子15", Date = new DateTime(2013, 4, 3) });
            IDates.Add(new IDate { Name = "日子16", Date = new DateTime(2013, 4, 3) });

            var anniversariesInDb = from Anniversary anniversary in db.Anniversaries
                                    select anniversary;

            Anniversaries = new ObservableCollection<Anniversary>(anniversariesInDb);

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
            if (this.MainPivot.SelectedIndex == 0)
            {
                //IDateToPass = null;
                this.NavigationService.Navigate(new Uri("/View/EditIDatePage.xaml?idateid=-1", UriKind.Relative));
            }
            if (MainPivot.SelectedIndex == 1)
            {
                this.NavigationService.Navigate(new Uri("/View/EditAnniversaryPage.xaml?anniid=-1", UriKind.Relative));
            }
        }

        private void ApplicationBarIconButtonRefresh_Click(object sender, EventArgs e)
        {
            var anniversariesInDb = from Anniversary anniversary in db.Anniversaries
                                    select anniversary;

            Anniversaries = new ObservableCollection<Anniversary>(anniversariesInDb);
        }

        private void AnnivesariesContextMenuItex_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            Anniversary data = menu.DataContext as Anniversary;
            if (menu.Header.Equals("Edit"))
            {
                NavigationService.Navigate(new Uri("/View/EditAnniversaryPage.xaml?anniid=" + data.AnniId, UriKind.Relative));
            }
        }
    }
}