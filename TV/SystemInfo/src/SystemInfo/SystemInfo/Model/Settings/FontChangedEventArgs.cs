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

using System;

namespace SystemInfo.Model.Settings
{
    /// <summary>
    /// Class that is passed with FontChanged event.
    /// </summary>
    public class FontChangedEventArgs : EventArgs
    {
        #region properties

        /// <summary>
        /// The current system font size.
        /// </summary>
        public FontSize FontSize { get; set; }

        /// <summary>
        /// The current system font type.
        /// </summary>
        public string FontType { get; set; }

        /// <summary>
        /// The current system default font type.
        /// </summary>
        public string DefaultFontType { get; set; }

        #endregion
    }
}