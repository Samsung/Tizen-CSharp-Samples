/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
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
using System;
using AnalogWatch.Tizen.Wearable.Views;
using ElmSharp;
using Tizen.Applications;

namespace AnalogWatch.Tizen.Wearable
{
    /// <summary>
    /// Analog Watch WatchApplication app class for Tizen wearable profile.
    /// </summary>
    internal class Program : global::Tizen.Applications.WatchApplication
    {
        #region fields

        /// <summary>
        /// The program/application instance.
        /// </summary>
        private static Program _app;

        /// <summary>
        /// The application's main window.
        /// </summary>
        private MainPage _mainPage;

        #endregion fields

        #region properties

        /// <summary>
        /// Log tag displayed in dlog.
        /// </summary>
        public string LogTag { get; } = "AnalogWatch";

        #endregion properties

        #region methods

        /// <summary>
        /// Handles creation phase of the application.
        /// Creates the window and displayed widgets.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            _mainPage = new MainPage(this.Window);
        }

        /// <summary>
        /// Callback invoked, in the normal mode, for every watch application tick.
        /// In the normal mode the callback is invoked once every second.
        /// </summary>
        /// <param name="time">Structure containing the current time data.</param>
        protected override void OnTick(TimeEventArgs time)
        {
            base.OnTick(time);
            WatchTime wt = time.Time;
            _mainPage.SetTime(wt.Year, wt.Month, wt.Day, wt.Hour, wt.Minute, wt.Second);
        }

        /// <summary>
        /// Callback invoked, in the ambient mode, for every watch application tick.
        /// In the ambient mode the callback is invoked once every minute.
        /// </summary>
        /// <param name="time">Structure containing the current time data.</param>
        protected override void OnAmbientTick(TimeEventArgs time)
        {
            base.OnAmbientTick(time);
            WatchTime wt = time.Time;
            _mainPage.SetTime(wt.Year, wt.Month, wt.Day, wt.Hour, wt.Minute, wt.Second);
        }

        /// <summary>
        /// Handles the behavior when the ambient mode is changed.
        /// </summary>
        /// <param name="mode">Contains information whether the ambient mode is enabled or not.</param>
        protected override void OnAmbientChanged(AmbientEventArgs mode)
        {
            base.OnAmbientChanged(mode);
            _mainPage.SetSecondsVisibility(!mode.Enabled);
        }

        /// <summary>
        /// Entry method of the program/application.
        /// </summary>
        /// <param name="args">Launch arguments.</param>
        private static void Main(string[] args)
        {
            Elementary.Initialize();
            Elementary.ThemeOverlay();
            _app = new Program();
            _app.Run(args);
        }

        /// <summary>
        /// The program/application termination handler.
        /// </summary>
        protected override void OnTerminate()
        {
            base.OnTerminate();
            _app.Dispose();
        }

        #endregion methods
    }
}
