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

using System;
using Tizen.Applications;
using TSystem = Tizen.System;
using Tizen.Wearable.CircularUI.Forms.Renderer.Watchface;
using WeatherWatch.PageModels;
using WeatherWatch.Pages;
using Xamarin.Forms;
using Tizen.System;
using WeatherWatch.Services;

namespace WeatherWatch
{
    /// <summary>
    /// WeatherWatch app class
    /// </summary>
    class Program : FormsWatchface
    {
        WeatherWatchPageModel _viewModel;

        /// <summary>
        /// Overrides this method if want to handle behavior when the application is launched.
        /// If base.OnCreated() is not called, the event 'Created' will not be emitted.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Xamarin.Forms.Application xamarinApp = new Xamarin.Forms.Application();
            TSystem.Display.StateChanged += Display_StateChanged;
            xamarinApp.MainPage = new WeatherWatchPage();
            _viewModel = (WeatherWatchPageModel)xamarinApp.MainPage.BindingContext;
            LoadWatchface(xamarinApp);
        }

        // Raised when the screen of the device is on/off/dim.
        private void Display_StateChanged(object sender, TSystem.DisplayStateChangedEventArgs e)
        {
            Console.WriteLine("[Display_StateChanged] " + e.State);
            // When device's screen turns on
            if (e.State == TSystem.DisplayState.Normal)
            {
                // Update Time for UI update
                WatchTime time = GetCurrentTime();
                _viewModel.Time = time.UtcTimestamp + TimeSpan.FromMilliseconds(time.Millisecond);
                // Update Information about Weather & Battery
                _viewModel.UpdateInformation();
            }
            else if (e.State == TSystem.DisplayState.Off)
            {
                // When the screen of the device is off
                _viewModel.UnregisterEvents();
            }
        }

        // Called when the time tick event occurs.
        // It's called when the screen of the device is on.
        protected override void OnTick(TimeEventArgs time)
        {
            base.OnTick(time);
            // Update Time for UI update
            if (_viewModel != null)
            {
                _viewModel.Time = time.Time.UtcTimestamp + TimeSpan.FromMilliseconds(time.Time.Millisecond);
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            // Initialize Tizen.Wearable.CircularUI.Forms
            Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
