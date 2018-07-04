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

namespace Preference.Tizen.Mobile.Controls
{
    /// <summary>
    /// The Toast class provides properties that show simple types of messages.
    /// </summary>
    /// <example>
    /// <code>
    /// Toast.DisplayText("Hello World", 3000)
    /// </code>
    /// </example>
    public sealed class Toast
    {
        #region methods

        /// <summary>
        /// It shows the simplest form of the message.
        /// </summary>
        /// <param name="text">The body text of the toast.</param>
        /// <param name="duration">How long to display the text in milliseconds.</param>
        public static void DisplayText(string text, int duration = 3000)
        {
            new ToastProxy
            {
                Text = text,
                Duration = duration,
            }.Show();
        }

        #endregion
    }
}