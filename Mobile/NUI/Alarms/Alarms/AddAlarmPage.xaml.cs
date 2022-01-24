using Alarms.Models;
using Alarms.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace Alarms
{
    public partial class AddAlaramPage : ContentPage
    {
        /// <summary>
        /// Application Info od application selected by user from list of changeable applications
        /// </summary>
        AppInfo selectedIteam;
        public AddAlaramPage(AppInfo appInfo)
        {
            InitializeComponent();
            selectedIteam = appInfo;
            SetAlarmButton.Clicked += OnClicked;
        }

        /// <summary>
        /// Event when "Set Alaram" button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClicked(object sender, ClickedEventArgs e)
        {
            AppControl appControl = new AppControl { ApplicationId = selectedIteam.AppId };
            Alarm createdAlarm;
            createdAlarm = AlarmManager.CreateAlarm(context.LocalDate, appControl);
            Navigator?.Pop();
        }
    }
}
