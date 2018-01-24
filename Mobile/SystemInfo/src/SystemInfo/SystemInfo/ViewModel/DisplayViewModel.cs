/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using SystemInfo.Model.Display;
using SystemInfo.Utils;
using SystemInfo.ViewModel.List;

namespace SystemInfo.ViewModel
{
    /// <summary>
    /// ViewModel class for display page.
    /// </summary>
    public class DisplayViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Properties of device's main display size.
        /// </summary>
        public static readonly string[] Size = {"Width", "Height"};

        /// <summary>
        /// Properties of device's main display density.
        /// </summary>
        public static readonly string[] Density = {"Dots per inch", "Bits per pixel"};

        /// <summary>
        /// Properties of device's displays.
        /// </summary>
        public static readonly string[] Display = {"Number of displays", "State"};

        /// <summary>
        /// Properties of device's screen.
        /// </summary>
        public static readonly string[] Screen =
        {
            "Max Brightness", "Brightness", "Auto Rotation",
            "Screen Backlight Time"
        };

        /// <summary>
        /// Local storage of collection of device's display properties.
        /// </summary>
        private GroupViewModel _groupList;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets collection of device's display properties.
        /// </summary>
        public GroupViewModel GroupList
        {
            get => _groupList;
            set => SetProperty(ref _groupList, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public DisplayViewModel()
        {
            var display = new Display();

            string[] sizeInitialValues =
            {
                display.ScreenWidth,
                display.ScreenHeight
            };

            string[] densityInitialValues =
            {
                display.ScreentDotsPerInch,
                display.ScreentBitsPerPixel
            };

            string[] displayInitialValues =
            {
                display.NumberOfDisplays.ToString(),
                display.MainDisplayState.ToString()
            };

            string[] screenInitialValues =
            {
                display.MainDisplayMaxBrightess.ToString(),
                display.MainDisplayBrightness.ToString(),
                display.AutoRotation,
                display.ScreenBacklightTime.ToString()
            };

            _groupList = new GroupViewModel
            {
                ListUtils.CreateGroupedItemsList(Size, nameof(Size), sizeInitialValues),
                ListUtils.CreateGroupedItemsList(Density, nameof(Density), densityInitialValues),
                ListUtils.CreateGroupedItemsList(Display, nameof(Display), displayInitialValues),
                ListUtils.CreateGroupedItemsList(Screen, nameof(Screen), screenInitialValues)
            };

            display.DisplayStateChanged += OnDisplayStateChanged;

            display.ScreenBacklightTimeChanged += OnScreenBacklightTimeChanged;

            display.DisplayBrightnessChanged += OnDisplayBrightnessChanged;
        }

        /// <summary>
        /// Updates device's display brightness.
        /// </summary>
        /// <param name="s">Object that sent event.</param>
        /// <param name="e">Event's argument.</param>
        private void OnDisplayBrightnessChanged(object s, DisplayBrightnessEventArgs e)
        {
            GroupList[nameof(Screen)]["Brightness"] = e.Brightness.ToString();
        }

        /// <summary>
        /// Updates device's screen backlight time.
        /// </summary>
        /// <param name="s">Object that sent event.</param>
        /// <param name="e">Event's argument.</param>
        private void OnScreenBacklightTimeChanged(object s, ScreenBacklightTimeChangedEventArgs e)
        {
            GroupList[nameof(Screen)]["Screen Backlight Time"] = e.ScreenBacklightTime.ToString();
        }

        /// <summary>
        /// Updates device's display state.
        /// </summary>
        /// <param name="s">Object that sent event.</param>
        /// <param name="e">Event's argument.</param>
        private void OnDisplayStateChanged(object s, DisplayStateEventArgs e)
        {
            GroupList[nameof(Display)]["State"] = e.DisplayState.ToString();
        }

        #endregion
    }
}