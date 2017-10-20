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
using Clock.Stopwatch;
using Clock.Timer;
using Clock.Worldclock;
using System;
using Xamarin.Forms;

namespace Clock.MainTabbed
{
    /// <summary>
    /// The main TabbedPage
    /// </summary>
    public class MainTabbedPage : TabbedPage
    {
#if ALARM_XAML
        internal Clock.Pages.AlarmListPage alarmXaml;
#endif
        internal AlarmListPage alarm;
        internal WorldclockPage wolrdclock;
        internal StopwatchPage stopwatch;
        internal TimerPage timer;

        /// <summary>
        /// The main TabbedPage which has four ContentPages
        /// </summary>
        public MainTabbedPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

#if ALARM_XAML
            // Alarm Xaml Page
            alarmXaml = (Clock.Pages.AlarmListPage) AlarmXamlPageController.GetInstance(TizenClock.Tizen.AlarmXaml.Pages.AlarmPages.ListPageXaml);
            Children.Add(alarmXaml);
#else
            // Alarm Page
            alarm = (AlarmListPage)AlarmPageController.GetInstance(AlarmPages.ListPage);
            Children.Add(alarm);
#endif
            // World clock Page
            wolrdclock = WorldclockPage.GetInstance();
            Children.Add(wolrdclock);

            // Stopwatch Page
            stopwatch = new Stopwatch.StopwatchPage();
            Children.Add(stopwatch);

            // Timer Page
            timer = new Timer.TimerPage();
            Children.Add(timer);

            CurrentPageChanged += MainTabbedPage_CurrentPageChanged;
        }

        /// <summary>
        /// Called when each tab has been selected
        /// </summary>
        /// <param name="sender">Clock.MainTabbedPage object</param>
        /// <seealso cref="System.object">
        /// <param name="e">Event argument for event of TabbedPage.</param>
        /// <seealso cref="System.EventArgs">
        private void MainTabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            if (CurrentPage.Title == "Alarm")
            {
                ((App)Application.Current).floatingButton.Show();
            }

            if (CurrentPage.Title == "World clock")
            {
                ((App)Application.Current).floatingButton.Show();
                wolrdclock.ShowPage();
            }

            if (CurrentPage.Title == "Stopwatch")
            {
                ((App)Application.Current).floatingButton.Hide();
                stopwatch.ShowPage();
            }

            if (CurrentPage.Title == "Timer")
            {
                ((App)Application.Current).floatingButton.Hide();
                timer.ShowPage();
            }
        }

        /// <summary>
        /// Called when a floating button on the worldclock page is clicked
        /// </summary>
        /// <param name="sender">Clock.MainTabbedPage object</param>
        /// <seealso cref="System.object">
        /// <param name="e">Event argument for event of TabbedPage.</param>
        /// <seealso cref="System.EventArgs">
        public void WorldClock_FloatingButton_Clicked(object sender, EventArgs e)
        {
            wolrdclock.OnFloatingButtonClicked(sender, e);
        }

        /// <summary>
        /// Called when a floating button on the alarm page is clicked
        /// </summary>
        /// <param name="sender">Clock.MainTabbedPage object</param>
        /// <seealso cref="System.object">
        /// <param name="e">Event argument for event of TabbedPage.</param>
        /// <seealso cref="System.EventArgs">
        public void Alarm_FloatingButton_Clicked(object sender, EventArgs e)
        {
            alarm.OnFloatingButtonClicked(sender, e);
        }

#if ALARM_XAML
        public void AlarmXaml_OnFloatingButtonClicked(object sender, EventArgs e)
        {
            alarmXaml.OnFloatingButtonClicked(sender, e);
        }
#endif
    }
}
