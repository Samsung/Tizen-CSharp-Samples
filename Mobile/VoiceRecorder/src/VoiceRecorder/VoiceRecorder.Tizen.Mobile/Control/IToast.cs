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
namespace VoiceRecorder.Tizen.Mobile.Control
{
    /// <summary>
    /// This interface, which defines the ability to display simple text, is used internally.
    /// </summary>
    internal interface IToast
    {
        #region properties

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        int Duration { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        string Text { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Shows the view for the specified duration.
        /// </summary>
        void Show();

        /// <summary>
        /// Dismisses the specified view.
        /// </summary>
        void Dismiss();

        #endregion
    }
}