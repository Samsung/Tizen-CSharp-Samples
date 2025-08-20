/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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

namespace Alarms.Controls
{
    /// <summary>
    /// Dialog control with OK button.
    /// </summary>
    public class DialogOK : DialogBase
    {
        #region methods

        /// <summary>
        /// Displays OK alert dialog with title from the Title property and message text from the Message property.
        /// </summary>
        protected override async void DisplayAlert()
        {
            await App.Current.MainPage.DisplayAlert(Title, Message, "OK");
        }

        #endregion
    }
}
