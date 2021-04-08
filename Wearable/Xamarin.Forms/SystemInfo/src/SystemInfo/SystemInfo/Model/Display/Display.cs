/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using SystemInfo.Utils;
using Xamarin.Forms;

namespace SystemInfo.Model.Display
{
    /// <summary>
    /// Class that holds information about display.
    /// </summary>
    public class Display
    {
        #region fields

        /// <summary>
        /// Service that provides information about display.
        /// </summary>
        private readonly IDisplay _service;

        #endregion

        #region properties

        /// <summary>
        /// Gets main display max brightness.
        /// </summary>
        public int MainDisplayMaxBrightess => _service.MainDisplayMaxBrigtness;

        /// <summary>
        /// Gets main display brightness.
        /// </summary>
        public int MainDisplayBrightness => _service.MainDisplayBrightness;

        /// <summary>
        /// Gets main display state.
        /// </summary>
        public DisplayState MainDisplayState => _service.MainDisplayState;

        /// <summary>
        /// Gets number of available displays.
        /// </summary>
        public int NumberOfDisplays => _service.NumberOfDisplays;

        /// <summary>
        /// Gets screen backlight time.
        /// </summary>
        public int ScreenBacklightTime => _service.ScreenBacklightTime;

        /// <summary>
        /// Gets screen height.
        /// </summary>
        public string ScreenHeight => KeyUtil.GetKeyValue<int>("http://tizen.org/feature/screen.height");

        /// <summary>
        /// Gets screen width.
        /// </summary>
        public string ScreenWidth => KeyUtil.GetKeyValue<int>("http://tizen.org/feature/screen.width");

        /// <summary>
        /// Gets number of dots per inch on screen.
        /// </summary>
        public string ScreentDotsPerInch => KeyUtil.GetKeyValue<int>("http://tizen.org/feature/screen.dpi");

        /// <summary>
        /// Gets number of bits per pixel on screen.
        /// </summary>
        public string ScreentBitsPerPixel => KeyUtil.GetKeyValue<int>("http://tizen.org/feature/screen.bpp");

        /// <summary>
        /// Gets status of auto-rotation.
        /// </summary>
        public string AutoRotation => KeyUtil.GetKeyValue<bool>("http://tizen.org/feature/screen.auto_rotation");

        /// <summary>
        /// Event invoked when display brightness has changed.
        /// </summary>
        public event EventHandler<DisplayBrightnessEventArgs> DisplayBrightnessChanged;

        /// <summary>
        /// Event invoked when display state has changed.
        /// </summary>
        public event EventHandler<DisplayStateEventArgs> DisplayStateChanged;

        /// <summary>
        /// Event invoked when screen backlight time has changed.
        /// </summary>
        public event EventHandler<ScreenBacklightTimeChangedEventArgs> ScreenBacklightTimeChanged;

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public Display()
        {
            _service = DependencyService.Get<IDisplay>();

            _service.DisplayChanged += (s, e) => { DisplayStateChanged?.Invoke(s, e); };

            _service.ScreenBacklightTimeChanged += (s, e) => { ScreenBacklightTimeChanged?.Invoke(s, e); };

            _service.StartListening();

            var oldBrightness = MainDisplayBrightness;
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                var brightness = _service.MainDisplayBrightness;
                if (oldBrightness == brightness)
                {
                    return true;
                }

                oldBrightness = brightness;
                DisplayBrightnessChanged?.Invoke(this, new DisplayBrightnessEventArgs(brightness));

                return true;
            });
        }

        #endregion
    }
}