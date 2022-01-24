using System;
using System.Collections.Generic;
using System.IO;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Alarms.Services;
using System.Linq;
using Alarms.Models;

namespace Alarms
{
    public partial class ChooseAlarmTypePage : ContentPage
    {
        /// <summary>
        /// Application Info od application selected by user from list of changeable applications
        /// </summary>
        AppInfo selectedIteam;

        public ChooseAlarmTypePage(AppInfo appInfo)
        {
            InitializeComponent();
            selectedIteam = appInfo;
            AtSpecifiedDateButton.Clicked += OnClickedAtSpecifiedDate;
        }

        /// <summary>
        /// Event when "Set Alaram" button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedAtSpecifiedDate(object sender, ClickedEventArgs e)
        {
            AddAlaramPage addAlaramPage = new AddAlaramPage(selectedIteam);
            Navigator?.Push(addAlaramPage);
        }

    }
}
