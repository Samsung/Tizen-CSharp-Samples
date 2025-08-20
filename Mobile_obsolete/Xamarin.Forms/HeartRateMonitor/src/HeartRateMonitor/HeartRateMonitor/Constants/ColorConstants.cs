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

using Xamarin.Forms;

namespace HeartRateMonitor.Constants
{
    /// <summary>
    /// ColorConstants class.
    /// Provides definition of colors used in application.
    /// </summary>
    public class ColorConstants
    {
        #region fields

        /// <summary>
        /// Primary color used in the application.
        /// </summary>
        public static readonly Color BASE_APP_COLOR = Color.FromRgb(209, 77, 77);

        /// <summary>
        /// Color used by the application's buttons labels.
        /// </summary>
        public static readonly Color BUTTON_TEXT_COLOR = Color.FromRgb(255, 255, 255);

        /// <summary>
        /// Color used by the page's backgrounds.
        /// </summary>
        public static readonly Color PAGE_BACKGROUND_COLOR = Color.FromRgb(255, 255, 255);

        /// <summary>
        /// Color used by labels on the welcome layer.
        /// </summary>
        public static readonly Color WELCOME_TEXT_COLOR = Color.FromRgb(250, 250, 250);

        /// <summary>
        /// Color used by labels displaying heart rate value.
        /// </summary>
        public static readonly Color HEART_RATE_VALUE_TEXT_COLOR = Color.FromRgb(0, 0, 0);

        /// <summary>
        /// Color used by page's title labels.
        /// </summary>
        public static readonly Color PAGE_TITLE_TEXT_COLOR = Color.FromRgb(0, 0, 0);

        /// <summary>
        /// Color used by page's message labels.
        /// </summary>
        public static readonly Color PAGE_MESSAGE_TEXT_COLOR = Color.FromRgb(115, 115, 115);

        /// <summary>
        /// Color used by label displaying heart rate value to indicate disabled digits.
        /// </summary>
        public static readonly Color HEART_RATE_VALUE_DISABLED_TEXT_COLOR = Color.FromRgb(214, 214, 214);

        /// <summary>
        /// Color used by label displaying heart rate unit.
        /// </summary>
        public static readonly Color HEART_RATE_VALUE_UNIT_COLOR = Color.FromRgb(214, 214, 214);

        /// <summary>
        /// Color used by label displaying heart rate limit value.
        /// </summary>
        public static readonly Color HEART_RATE_LIMIT_LABEL_COLOR = Color.FromRgb(214, 214, 214);

        #endregion
    }
}