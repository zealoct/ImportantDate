﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using ImportantDate.Model;

namespace ImportantDate.View
{
    public partial class EditAnniversaryPage : PhoneApplicationPage
    {
        DatabaseContext db;

        ApplicationBarIconButton BtnSave;
        ApplicationBarIconButton BtnCancel;

        Anniversary curData;
        Boolean newInstance = false;

        public EditAnniversaryPage()
        {
            InitializeComponent();
            ApplicationBar appBar = new ApplicationBar();

            BtnSave = new ApplicationBarIconButton(new Uri("/Images/appbar.save.rest.png", UriKind.Relative));
            BtnSave.Click += new EventHandler(BtnSave_Click);
            BtnSave.Text = "Save";
            BtnSave.IsEnabled = false;
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
            string anniId = string.Empty;
            if (!NavigationContext.QueryString.TryGetValue("anniid", out anniId))
            {
                throw new Exception("get anniId id error");
            }

            if (anniId.Equals("-1"))
            {
                PageName.Text = "New Anniversary";
                newInstance = true;
                curData = new Anniversary();
            }
            else
            {
                PageName.Text = "Edit Anniversary";
                newInstance = false;

                curData = db.Anniversaries.Single(anni => anni.AnniId == Int32.Parse(anniId));

                InputName.Text = curData.Name;
                InputPeriod.Text = curData.Period.ToString();
            }

            PageName.Text = "" + curData.AnniId;

            CheckSaveBtnAvailability();

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            db.SubmitChanges();
        }

        private void CheckSaveBtnAvailability()
        {
            if (InputName.Text != null && InputPeriod.Text != null)
            {
                BtnSave.IsEnabled = true;
            }
            else
            {
                BtnSave.IsEnabled = false;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            curData.Name = InputName.Text;
            curData.Period = Int32.Parse(InputPeriod.Text);

            if (newInstance)
            {
                db.Anniversaries.InsertOnSubmit(curData);
            }
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

        private void All_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckSaveBtnAvailability();
        }


    }
}