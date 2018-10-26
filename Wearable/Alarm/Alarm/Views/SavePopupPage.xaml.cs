/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Alarm.Models;
using Alarm.Resx;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Alarm.Views
{
    /// <summary>
    /// SavePopupPage class
    /// It shows the remaining time from the current time to the alarm time. 
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SavePopupPage : ContentPage
	{
        ///Korean CultureInfo name
        const string KoreanCultureInfoName = "ko-KR";

        private AlarmRecord _record;

        /// <summary>
        /// SavePopupPage constructor
        /// </summary>
        /// <param name="record">AlarmRecord</param>
        public SavePopupPage(AlarmRecord record)
		{
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            _record = record;
            SetPopupLabel();
            ClosePopup();
        }

        /// <summary>
        /// Calculate the remaining time and then set AlarmSetLabel.
        /// </summary>
        private void SetPopupLabel()
        {
            DateTime savedTime = _record.ScheduledDateTime;
            DateTime now = System.DateTime.Now;
            int hour = savedTime.Hour - now.Hour;
            int min = savedTime.Minute - now.Minute;
            string text = "";

            if (hour >= 0)
            {
                if (min < 0)
                {
                    min = 60 + min;
                    if (hour == 0)
                    {
                        hour = 24 - 1;
                    }
                    else
                    {
                        hour = hour - 1;
                    }
                }
            }
            else   //hour < 0
            {
                hour = 24 + hour;
                if (min < 0)
                {
                    min = 60 + min;
                    hour = hour - 1;
                }
            }

            //Console.WriteLine($"App.Culture.Name:{App.Culture.Name}");
            if (hour == 0)
            {
                //If hour is 0, display only remain minutes.
                if (min == 0)
                {
                    text = GetSetNowText();
                }
                else
                {
                    text = GetRemainMinutesText(min);
                }
            }
            else
            {
                 text = GetRemainHourMinutesText(hour, min);
            }

            AlarmSetLabel.Text = text;
        }


        /// <summary>
        /// Get localized string of alarm set now string .
        /// </summary>
        /// <returns>localized string </returns>
        private string GetSetNowText()
        {
            string text;
            if (App.Culture.Name == KoreanCultureInfoName)
            {
                text = AppResources.Now + " " + AppResources.AlarmSet;
            }
            else
            {
                text = AppResources.AlarmSet + " " + AppResources.Now;
            }

            return text;
        }

        /// <summary>
        /// Get localized string of remaining minutes.
        /// </summary>
        /// <param name="minutes">remain minutes</param>
        /// <returns>Localized string of remaining minutes</returns>
        private string GetRemainMinutesText(int minutes)
        {
            string text;
            if (App.Culture.Name == KoreanCultureInfoName)
            {
                text = AppResources.FromNow + " " + minutes + " " + AppResources.Minutes + " " + AppResources.After + " " + AppResources.AlarmSet;
            }
            else
            {
                if (minutes == 1)
                {
                    text = AppResources.AlarmSet + " " + AppResources.Minute + " " + AppResources.After + " " + AppResources.FromNow;
                }
                else
                {
                    text = AppResources.AlarmSet + " " + minutes + " " + AppResources.Minutes + " " + AppResources.After + " " + AppResources.FromNow;
                }
            }

            return text;
        }

        /// <summary>
        /// Get localized string of remaining hours and minutes.
        /// </summary>
        /// <param name="hours">remain hours</param>
        /// <param name="minutes">remain minutes</param>
        /// <returns>Localized string of remaining hours and minutes</returns>
        private string GetRemainHourMinutesText(int hours, int minutes)
        {
            string text;
            if (App.Culture.Name == KoreanCultureInfoName)
            {
                if (minutes == 0)
                {
                    text = AppResources.FromNow + " " + hours + " " + AppResources.Hours + " " + AppResources.After + " " + AppResources.AlarmSet;
                }
                else
                {
                    text = AppResources.FromNow + " " + hours + " " + AppResources.Hours + " " + minutes + " " + AppResources.Minutes + " " + AppResources.After + " " + AppResources.AlarmSet;
                }
            }
            else
            {
                
                if (hours == 1)
                {
                    text = AppResources.AlarmSet  + " " + AppResources.Hour;
                }
                else
                {
                    text = AppResources.AlarmSet + " " + hours + " " + AppResources.Hours;
                }

                if (minutes == 0)
                {
                    text = text + " " + AppResources.After + " " + AppResources.FromNow;
                }
                else if (minutes == 1)
                {
                    text = text + " " + AppResources.Minute + " " + AppResources.After + " " + AppResources.FromNow;
                }
                else
                {
                    text = text + " " + minutes + " " + AppResources.Minutes + " " + AppResources.After + " " + AppResources.FromNow;
                }
            }

            return text;
        }

        /// <summary>
        /// After 2 seconds. this page closed and then go to the Mainpage
        /// </summary>
        async private void ClosePopup()
        {
            await Task.Delay(2000);
            await Navigation.PopToRootAsync();
        }
    }
}