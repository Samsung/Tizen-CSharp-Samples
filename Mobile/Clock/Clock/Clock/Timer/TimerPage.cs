/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

using Clock.Common;
using Clock.Stopwatch;
using Clock.Utils;
using System;
using Xamarin.Forms;

namespace Clock.Timer
{
    ///<summary>
    ///The TimerPage of the Clock application
    ///</summary>
    partial class TimerPage : ContentPage
    {
        /// <summary>
        /// Stopwatch for measuring elapsed time
        /// </summary>
        private static System.Diagnostics.Stopwatch stopWatch;
        /// <summary>
        /// Timer state
        /// </summary>
        private SWTimerState tmState = SWTimerState.init;

        ///<summary>
        ///Initializes a new instance of the <see cref="TimerPage"/> class
        ///</summary>
        public TimerPage()
        {
            Title = "Timer";
            Icon = "maintabbed/clock_tabs_ic_timer.png";
            set_time_ = new Time();
            model_ = new Timer();
            Content = new EmptyPage();
        }

        /// <summary>
        /// Loads the TimerPage UI when it's selected via TabbedPage
        /// </summary>
        public void ShowPage()
        {
            if (mainStackLayout == null)
            {
                Content = CreateTimerPage();
            }

            if (stopWatch == null)
            {
                stopWatch = new System.Diagnostics.Stopwatch();
            }
        }

        /// <summary>
        /// Updates timer information every millisecond after the timer starts
        /// </summary>
        /// <returns> Returns true in case that the timer continues; otherwise returns false</returns>
        private bool PanelTimerFunc()
        {
            if (tmState == SWTimerState.started)
            {
                long time = model_.GetRemainingTime();
                int h = (int)((time / 3600) % 100);
                int m = (int)((time - h * 3600) / 60);
                int s = (int)((time - h * 3600 - m * 60) % 60);

                if (h <= 0 && m <= 0 && s <= 0)
                {
                    model_.Stop();
                    model_.Reset();
                    StopPanelTimer();

                    Device.StartTimer(new TimeSpan(0), () =>
                    {
                        ShowStartupMenu();
                        return false;
                    });
                }
                else
                {
                    var hmstime = string.Format("{0:00}:{1:00}:{2:00}", h, m, s);
                    counterview.DisplayTime(hmstime);
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Start the stopwatch timer.
        /// </summary>
        public void CreatePanelTimer()
        {
            stopWatch.Start();
            tmState = SWTimerState.started;
            Device.StartTimer(new TimeSpan(0, 0, 0, 1, 0), PanelTimerFunc);
        }

        /// <summary>
        /// Stop the stopwatch timer.
        /// </summary>
        public void StopPanelTimer()
        {
            stopWatch.Stop();
            tmState = SWTimerState.stopped;
        }

        /// <summary>
        /// Reset the stopwatch timer.
        /// </summary>
        public void ResetPanelTimer()
        {
            stopWatch.Reset();
            tmState = SWTimerState.init;
        }
    }
}
