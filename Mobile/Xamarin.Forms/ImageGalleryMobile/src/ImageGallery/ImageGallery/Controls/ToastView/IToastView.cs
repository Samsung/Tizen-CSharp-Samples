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
namespace ImageGallery.Controls.ToastView
{
    /// <summary>
    /// IToastView interface class.
    /// It defines method that should be implemented by class responsible for toast message displaying.
    /// </summary>
    public interface IToastView
    {
        #region methods

        /// <summary>
        /// Displays toast popup with given message.
        /// </summary>
        /// <param name="message">Message to be displayed in the toast popup.</param>
        void ShowToastMessage(string message);

        #endregion
    }
}
