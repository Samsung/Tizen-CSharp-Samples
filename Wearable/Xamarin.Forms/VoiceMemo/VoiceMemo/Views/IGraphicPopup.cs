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

namespace VoiceMemo.Views
{
    /// <summary>
    /// Interface IPopup for Toast Popup
    /// </summary>
    public interface IGraphicPopup
    {
        /// <summary>
        /// Text for Toast Popup
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Duration for Toast Popup
        /// How long to display the text message
        /// </summary>
        double Duration { get; set; }

        /// <summary>
        /// Make Toast Popup show
        /// </summary>
        void Show();

        /// <summary>
        /// Raised when back button is pressed
        /// </summary>
        event EventHandler BackButtonPressed;

        /// <summary>
        /// Raised after the text is shown for the specified duration
        /// </summary>
        event EventHandler TimedOut;
    }
}
