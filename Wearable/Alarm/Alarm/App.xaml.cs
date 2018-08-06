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

using Alarm.Implements;
using Alarm.Models;
using Alarm.ViewModels;
using Alarm.Views;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Alarm
{
    /// <summary>
    /// Alarm Application class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        MainPage firstPage;

        public static CultureInfo Culture { get; private set; }

        public App()
        {
            // Get locale information
            UpdateLocale();
            InitializeComponent();
            var alarmModel = new AlarmModel();
            firstPage = (MainPage)AlarmPageController.GetInstance(AlarmPages.MainPage , new MainPageModel());
            MainPage = new NavigationPage(firstPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        /// <summary>
        /// Get the current locale and apply it.
        /// </summary>
        public void UpdateLocale()
        {
            // determine the correct, supported .NET culture
            var handler = new LocaleHandler();
            Culture = handler.CurrentCultureInfo;
            // set the RESX for resource localization
            Resx.AppResources.Culture = Culture;

            // Whenever language has been changed, CurrentCulture will be updated.
            MessagingCenter.Subscribe<LocaleHandler, CultureInfo>(this, MessageKeys.LanguageChanged, (obj, culture) =>
            {
                Console.WriteLine($"Received LanguageChanged  culture:{culture.ToString()}");
            });
        }

    }
}