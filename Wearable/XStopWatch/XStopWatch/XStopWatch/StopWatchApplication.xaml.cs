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
using Xamarin.Forms.Xaml;

namespace XStopWatch
{
    /// <summary>
    /// StopWatchApplication is implementation of a Xamarin Forms Application.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StopWatchApplication : Application
	{
        public StopWatchApplication()
		{
			InitializeComponent();

            // register handle to property changed event for catching changed state.
            StopWatch.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == StopWatch.StateProperty.PropertyName)
                {
                    // request AlwaysOn on StopWatch is started.
                    AlwaysOnRequest?.Invoke(this, StopWatch.State == State.Started);
                }
            };
        }

        /// <summary>
        /// Event that requests the AlwaysOn method of the window when screen should not be turned off.
        /// </summary>
        public event EventHandler<bool> AlwaysOnRequest;

        /// <summary>
        /// this method handles StopWatch's Lap add button click event, witch total and lap time as event arguments.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">ValueTuple, Item1 is a elapsed time and item2 is lap time.</param>
        void OnAddLap(object sender, (TimeSpan, TimeSpan) e)
        {
            // Add Lap record to LapsPage
            Laps.AddLap(e);
        }

        /// <summary>
        /// this method handles StopWatch's Stop button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An object that contains no event data.</param>
        void OnStopLap(object sender, EventArgs e)
        {
            // Reset the LapsPage
            Laps.Reset();
        }
    }
}