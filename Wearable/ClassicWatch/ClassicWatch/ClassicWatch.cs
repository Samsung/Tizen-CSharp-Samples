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

namespace ClassicWatch
{
    class Program : FormsWatchface
    {
        WatchViewModel _viewModel;
        ClassicWatchApplication _classicWatch;
        bool _initialized;

        /// <summary>
        /// Called when this watchface application is launched
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            _viewModel = new WatchViewModel();
            _classicWatch = new ClassicWatchApplication(_viewModel);
            _classicWatch.BindingContext = _viewModel;
            LoadWatchface(_classicWatch);
            _initialized = false;

            _viewModel.Time = GetCurrentTime().UtcTimestamp;
            Console.WriteLine($"OnCreate Time.ToString:{_viewModel.Time.ToString()}");
            _classicWatch.MoveHands();
        }

        /// <summary>
        /// Called when the time tick event comes.
        /// </summary>
        /// <param name="time">TimeEventArgs</param>
        protected override void OnTick(TimeEventArgs time)
        {
            base.OnTick(time);
            if (_viewModel != null && _classicWatch != null)
            {
                _viewModel.Time = time.Time.UtcTimestamp;
                //Console.WriteLine($"OnTick Time.ToString:{_viewModel.Time.ToString()}");
                _classicWatch.MoveHands();
            }
        }

        /// <summary>
        /// Called when this application is resumed.
        /// </summary>
        protected override void OnResume()
        {
            if (!_initialized)
            {
                _viewModel.Time = GetCurrentTime().UtcTimestamp;
                _classicWatch.MoveHands();
                _initialized = true;
            }

            base.OnResume();
        }

        /// <summary>
        /// Called when this application is paused.
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();
        }

        /// <summary>
        /// Called when ambient mode is changed.
        /// </summary>
        /// <param name="mode">AmbientEventArgs</param>
        protected override void OnAmbientChanged(AmbientEventArgs mode)
        {
            Console.WriteLine($"OnAmbientChanged mode:{mode.ToString()}");
            base.OnAmbientChanged(mode);
        }

        /// <summary>
        /// Called when the ambient time tick event comes.
        /// </summary>
        /// <param name="time">TimeEventArgs</param>
        protected override void OnAmbientTick(TimeEventArgs time)
        {
            base.OnAmbientTick(time);

            if (_viewModel != null && _classicWatch != null)
            {
                _viewModel.Time = time.Time.UtcTimestamp;
                //Console.WriteLine($"OnAmbientTick Time.ToString:{_viewModel.Time.ToString()}");
                _classicWatch.MoveHands();
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
