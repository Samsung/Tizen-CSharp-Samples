/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Clock.Alarm;
using Clock.Interfaces;
using Clock.MainTabbed;
using Clock.Worldclock;
using Tizen.Xamarin.Forms.Extension;
using Xamarin.Forms;

namespace Clock
{
    /// <summary>
    /// The clock application
    /// </summary>
    public class App : Application
    {
        internal static NavigationPage mainNavi;
        internal static MainTabbedPage mainTabbed;
        public int ScreenWidth;
        public int ScreenHeight;
        public FloatingButton floatingButton;
        public bool Is24hourFormat;

        /// <summary>
        /// Gets/Sets instance of the <see cref="WorldclockInfo"/> class.
        /// </summary>
        public static WorldclockInfo ClockInfo
        {
            get; set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
#if ALARM_XAML
            InitializeComponent();
#endif
            // The root page of your application
            mainTabbed = new MainTabbedPage();
            MainPage = new NavigationPage(mainTabbed);
            mainNavi = MainPage as NavigationPage;

            // Create a FloatingButton
            FloatingButtonItem item = new FloatingButtonItem
            {
                Icon = "alarm/clock_floating_icon.png"
            };
            floatingButton = new FloatingButton
            {
                FirstButton = item
            };

            // By default, FloatingButton is shown when it's created and therefore it is needed to make it hidden.
            floatingButton.Hide();
        }

        /// <summary>
        /// Makes a floating button visible on alarm or worldclock page
        /// </summary>
        /// <param name="title">string</param>
        public void ShowFloatingButton(string title)
        {
            floatingButton.Show();
            if (title == "Alarm")
            {
#if ALARM_XAML
                floatingButton.FirstButton.Clicked += mainTabbed.AlarmXaml_OnFloatingButtonClicked;
#else
                floatingButton.FirstButton.Clicked += mainTabbed.Alarm_FloatingButton_Clicked;
#endif
            }
            else if (title == "World clock")
            {
                floatingButton.FirstButton.Clicked += mainTabbed.WorldClock_FloatingButton_Clicked;
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Makes a floating button invisible on stopwatch or timer page
        /// </summary>
        /// <param name="title">string</param>
        public void HideFloatingButton(string title)
        {
            floatingButton.Hide();
            if (title == "Alarm")
            {
#if ALARM_XAML
                floatingButton.FirstButton.Clicked -= mainTabbed.AlarmXaml_OnFloatingButtonClicked;
#else
                floatingButton.FirstButton.Clicked -= mainTabbed.Alarm_FloatingButton_Clicked;
#endif
            }
            else if (title == "World clock")
            {
                floatingButton.FirstButton.Clicked -= mainTabbed.WorldClock_FloatingButton_Clicked;
            }
        }

        /// <summary>
        /// Called when clock application terminates.
        /// It happens when clock application exits via BACK key
        /// </summary>
        public void Terminate()
        {
            if (ClockInfo != null)
            {
                ClockInfo.Dispose();
            }

            System.Diagnostics.Debug.WriteLine("                                       Terminate...");
            AlarmModel.PrintAll("App.Terminate()");
        }

        /// <summary>
        /// Called when clock application starts.
        /// It happens when clock application is shown at first.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Called when clock application sleeps.
        /// It happens when clock application goes background via HOME or BACK key
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Called when clock application resumes.
        /// It happens when clock application goes foreground
        /// </summary>
        async protected override void OnResume()
        {
            // Handle when your app resumes
            App currentApp = Application.Current as App;
            if (currentApp.ScreenWidth != 720 || currentApp.ScreenHeight != 1280)
            {
                await mainNavi.DisplayAlert("Notice", "The app only works in HD mobile emulator.", "OK");
                DependencyService.Get<IAppTerminator>().TerminateApp();
                return;
            }
        }
    }
}
