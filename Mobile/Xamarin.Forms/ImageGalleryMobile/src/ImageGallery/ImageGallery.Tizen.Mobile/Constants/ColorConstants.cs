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
using Xamarin.Forms;

namespace ImageGallery.Tizen.Mobile.Constants
{
    /// <summary>
    /// ColorConstants class.
    /// Provides definition of colors used in application.
    /// </summary>
    public class ColorConstants
    {
        #region properties

        /// <summary>
        /// Text color of the labels on the welcome page.
        /// </summary>
        public static readonly Color WELCOME_TEXT_COLOR = Color.FromRgb(255, 255, 255);

        /// <summary>
        /// Text color of the buttons.
        /// </summary>
        public static readonly Color BUTTON_TEXT_COLOR = Color.FromRgb(250, 250, 250);

        /// <summary>
        /// Background color of the buttons.
        /// </summary>
        public static readonly Color BUTTON_BACKGROUND_NORMAL_COLOR = Color.FromRgb(61, 185, 204);

        /// <summary>
        /// Color used by the application's navigation bars.
        /// </summary>
        public static readonly Color NAVIGATION_BAR_COLOR_DEFAULT = Color.FromRgb(61, 185, 204);

        /// <summary>
        /// Color used by the application's navigation bars.
        /// </summary>
        public static readonly Color NAVIGATION_BAR_COLOR_DETAILS = Color.FromRgb(0, 0, 0);

        /// <summary>
        /// Color used by the application's pages background.
        /// </summary>
        public static readonly Color PAGE_BACKGROUND_DEFAULT_COLOR = Color.FromRgb(250, 250, 250);

        /// <summary>
        /// Color used by select all label.
        /// </summary>
        public static readonly Color SELECT_ALL_TEXT_COLOR = Color.FromRgb(61, 185, 204);

        #endregion
    }
}
