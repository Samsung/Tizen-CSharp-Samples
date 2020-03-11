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

namespace ChronographWatch
{
    /// <summary>
    /// This class has a main function, which is the starting point of the program.
    /// It is a class that controls the lifecycle of the watchface application.
    /// You must use FormsWatchface to control the lifecycle.
    /// This class is a class that makes use of Tizen.Wearable.CircularUI.Forms.Renderer.Watchface.FormsWatchface by inheriting Tizen.Application.WatchApplication
    /// </summary>
    class Program : FormsWatchface
    {
        WatchViewModel _viewModel;
        ChronographWatchApplication _watch;
        bool _initialized;

        /// <summary>
        /// Called when this watchface application is launched
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            _viewModel = new WatchViewModel();
            _watch = new ChronographWatchApplication(_viewModel);
            _watch.BindingContext = _viewModel;
            LoadWatchface(_watch);
            _initialized = false;

            _viewModel.Time = GetCurrentTime().UtcTimestamp;
            _watch.MoveHands();
        }

        /// <summary>
        /// Called when the time tick event comes.
        /// </summary>
        /// <param name="time">TimeEventArgs</param>
        protected override void OnTick(TimeEventArgs time)
        {
            base.OnTick(time);
            if (_viewModel != null && _watch != null)
            {
                _viewModel.Time = time.Time.UtcTimestamp;
                _watch.MoveHands();
            }
        }

        /// <summary>
        /// Called when this application is resumed.
        /// </summary>
        protected override void OnResume()
        {
            //Console.WriteLine($"OnResume()");
            if (!_initialized)
            {
                _viewModel.Time = GetCurrentTime().UtcTimestamp;
                _watch.MoveHands();
                _initialized = true;
            }

            _watch.RunSecHand();
            base.OnResume();
        }

        /// <summary>
        /// Called when this application is paused.
        /// </summary>
        protected override void OnPause()
        {
            //Console.WriteLine($"OnPause()");
            base.OnPause();
            _watch.StopSecHand();
        }

        /// <summary>
        /// Called when ambient mode is changed.
        /// </summary>
        /// <param name="mode">AmbientEventArgs</param>
        protected override void OnAmbientChanged(AmbientEventArgs mode)
        {
            Console.WriteLine($"OnAmbientChanged mode:{mode.Enabled}");
            _viewModel.IsAmbientMode = mode.Enabled;
            _viewModel.AmbientModeDisabled = !mode.Enabled;
            base.OnAmbientChanged(mode);
        }

        /// <summary>
        /// Called when the ambient time tick event comes.
        /// </summary>
        /// <param name="time">TimeEventArgs</param>
        protected override void OnAmbientTick(TimeEventArgs time)
        {
            base.OnAmbientTick(time);

            if (_viewModel != null && _watch != null)
            {
                _viewModel.Time = time.Time.UtcTimestamp;
                //Console.WriteLine($"OnAmbientTick Time.ToString:{_viewModel.Time.ToString()}");
                _watch.MoveHands();
                _watch.RunSecHand();
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            // It's mandatory to initialize Circular UI for using Tizen Wearable Circular UI API
            global::Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
