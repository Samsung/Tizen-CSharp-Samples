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
using Xamarin.Forms;
using Tizen.Applications;
using Tizen.Wearable.CircularUI.Forms.Renderer.Watchface;

namespace AmbientWatch
{
    /// <summary>
    /// AmbientWatch watchface app
    /// </summary>
    class Program : FormsWatchface
    {
        ClockViewModel ViewModel;

        /// <summary>
        /// Called when the application is launched.
        /// base.OnCreate() should be called to get "Created" event
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();

            var watchfaceApp = new AmbientWatchApplication();
            ViewModel = new ClockViewModel(this);
            ViewModel.Time = GetCurrentTime().UtcTimestamp;
            watchfaceApp.BindingContext = ViewModel;
            LoadWatchface(watchfaceApp);
        }

        /// <summary>
        /// Called when the time tick event occurs
        /// </summary>
        /// <param name="time">TimeEventArgs</param>
        protected override void OnTick(TimeEventArgs time)
        {
            base.OnTick(time);
            // Set time to update UI
            if (ViewModel != null)
            {
                ViewModel.Time = time.Time.UtcTimestamp + TimeSpan.FromMilliseconds(time.Time.Millisecond);
            }
        }

        //
        // Summary:
        //     Overrides this method if want to handle behavior when the ambient mode is changed.
        //     If base.OnAmbientChanged() is not called, the event 'AmbientChanged' will not
        //     be emitted.
        //
        // Parameters:
        //   mode:
        //     The received AmbientEventArgs
        protected override void OnAmbientChanged(AmbientEventArgs mode)
        {
            base.OnAmbientChanged(mode);
            ViewModel.AmbientModeDisabled = !mode.Enabled;
        }

        //
        // Summary:
        //     Overrides this method if want to handle behavior when the time tick event comes
        //     in ambient mode. If base.OnAmbientTick() is not called, the event 'AmbientTick'
        //     will not be emitted.
        //
        // Parameters:
        //   time:
        //     The received TimeEventArgs to get time information.
        protected override void OnAmbientTick(TimeEventArgs time)
        {
            base.OnAmbientTick(time);
            // Set time to update UI in ambient mode
            ViewModel.Time = time.Time.UtcTimestamp + TimeSpan.FromMilliseconds(time.Time.Millisecond);
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}