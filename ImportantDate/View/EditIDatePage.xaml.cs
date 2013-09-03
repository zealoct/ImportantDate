using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using ImportantDate.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ImportantDate.View
{
    public partial class EditIDatePage : PhoneApplicationPage, INotifyPropertyChanged
    {
        DatabaseContext db;
        ApplicationBarIconButton BtnSave;
        ApplicationBarIconButton BtnCancel;
        Boolean newInstance = false;

        public IDate curData { get; set; }

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

        private Collection<Int32> prevAnniversaries;
        private Collection<Int32> newAnniversaries;

        public EditIDatePage()
        {
            InitializeComponent();

            this.DataContext = this;

            ApplicationBar appBar = new ApplicationBar();

            BtnSave = new ApplicationBarIconButton(new Uri("/Images/appbar.save.rest.png", UriKind.Relative));
            BtnSave.Click += new EventHandler(BtnSave_Click);
            BtnSave.Text = "Save";
            //BtnSave.IsEnabled = false;
            appBar.Buttons.Add(BtnSave);

            BtnCancel = new ApplicationBarIconButton(new Uri("/Images/appbar.cancel.rest.png", UriKind.Relative));
            BtnCancel.Click += new EventHandler(BtnCancel_Click);
            BtnCancel.Text = "Cancel";
            appBar.Buttons.Add(BtnCancel);

            this.ApplicationBar = appBar;

            db = new DatabaseContext(DatabaseContext.DBConnectionString);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string iDateId = string.Empty;
            if (!NavigationContext.QueryString.TryGetValue("idateid", out iDateId))
            {
                throw new Exception("get IDate id error");
            }


            if (iDateId.Equals("-1"))
            {
                PageName.Text = "New Date";
                newInstance = true;

                curData = new IDate(); 
                prevAnniversaries = new Collection<int>();
                newAnniversaries = new Collection<int>();
            }
            else
            {
                PageName.Text = "Edit Date";
                newInstance = false;
            }

            var anniversariesInDb = from Anniversary anniversary in db.Anniversaries
                                    select anniversary;

            Anniversaries = new ObservableCollection<Anniversary>(anniversariesInDb);

            //SelectedAnnivesariesBox.ItemsSource = Anniversaries;
            //foreach (Anniversary anni in Anniversaries)
            //{
            //    SelectedAnnivesariesBox.Items.Add(anni);
            //}

            base.OnNavigatedTo(e);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //PageName.Text = "" + SelectedAnnivesariesBox.Items.Count + ":" + this.Anniversaries.Count + ":" + SelectedAnnivesariesBox.ItemsSource.ToString();
            curData.Name = InputName.Text;
            curData.Date = (DateTime)InputDate.Value;

            if (newInstance)
            {
                db.IDates.InsertOnSubmit(curData);
                db.SubmitChanges();
            }

            IEnumerable<int> rm = prevAnniversaries.Except(newAnniversaries);
            IEnumerable<int> add = newAnniversaries.Except(prevAnniversaries);

            foreach (int i in rm)
            {
                var a = db.IDateAnniversaries.Single(da => da.AnniId == i && da.IDateId == curData.IDateId);
                db.IDateAnniversaries.DeleteOnSubmit(a);
            }

            foreach (int i in add)
            {
                IDateAnniversary da = new IDateAnniversary { AnniId = i, IDateId = curData.IDateId };
                db.IDateAnniversaries.InsertOnSubmit(da);
            }

            BtnCancel_Click(sender, e);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                NavigationService.Navigate(new Uri("/View/MainPage.xaml", UriKind.Relative));
            }
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

        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            ToggleSwitch swth = sender as ToggleSwitch;
            Anniversary anni = swth.DataContext as Anniversary;
            swth.Foreground = this.Resources["PhoneBorderBrush"] as System.Windows.Media.Brush;
            newAnniversaries.Add(anni.AnniId);
        }

        private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleSwitch swth = sender as ToggleSwitch;
            Anniversary anni = swth.DataContext as Anniversary;
            swth.Foreground = this.Resources["PhoneForegroundBrush"] as System.Windows.Media.Brush;
            newAnniversaries.Remove(anni.AnniId);
        }
    }
}