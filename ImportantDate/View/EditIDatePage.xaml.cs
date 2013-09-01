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

namespace ImportantDate.View
{
    public partial class EditIDatePage : PhoneApplicationPage
    {
        private IDate _idate;
        public IDate IDate
        {
            get { return _idate; }
            set { _idate = value; }
        }

        public EditIDatePage()
        {
            InitializeComponent();
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
                PageName.Text = "Add Date";
            }
            else
            {
                PageName.Text = "Edit Date";
            }
        }
    }
}