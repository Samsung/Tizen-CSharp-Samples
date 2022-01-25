/* 
  * Copyright (c) 2022 Samsung Electronics Co., Ltd 
  * 
  * Licensed under the Flora License, Version 1.1 (the "License"); 
  * you may not use this file except in compliance with the License. 
  * You may obtain a copy of the License at 
  * 
  * http://www.apache.org/licenses/LICENSE-2.0 
  * 
  * Unless required by applicable law or agreed to in writing, software 
  * distributed under the License is distributed on an "AS IS" BASIS, 
  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
  * See the License for the specific language governing permissions and 
  * limitations under the License. 
  */
using Alarms.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace Alarms
{
    /// <summary>
    /// Content page of Add Alarm Page
    /// </summary>
    public partial class AddAlaramPage : ContentPage
    {
        MainPage main;
        public AddAlaramPage(MainPage mainPage)
        {
            main = mainPage;
            InitializeComponent();
            SetAlarmButton.Clicked += OnClicked;
        }

        /// <summary>
        /// Event when "Set Alaram" button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClicked(object sender, ClickedEventArgs e)
        {
            Alarm createdAlarm;
            try
            {
                if (context.LocalDate > DateTime.Now)
                {
                    Tizen.Applications.Notifications.Notification notification = new Tizen.Applications.Notifications.Notification
                    {
                        Title = "Alarm",
                        Content = "Alarm",
                        Count = 1
                    };
                    createdAlarm = AlarmManager.CreateAlarm(context.LocalDate, notification);
                    main.AddElementToScroller(createdAlarm);
                    Navigator?.Pop();
                }
                else
                {
                    // Check wrong Input parameter
                    var button = new Button()
                    {
                        Text = "OK",
                    };
                    button.Clicked += (object s, ClickedEventArgs a) =>
                    {
                        Navigator?.Pop();
                    };
                    DialogPage.ShowAlertDialog("Error!", "Please Input Correct Date For Alarm", button);
                }
            }
            catch (Exception error)
            {
                Debug.WriteLine("Cannot create alarm. Exception message: " + error.Message);
            } 
        }
    }
}
