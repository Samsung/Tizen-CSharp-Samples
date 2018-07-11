/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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

namespace Preference.Interfaces
{
    /// <summary>
    /// Application page interface.
    /// </summary>
    public interface IPage
    {
        #region methods

        /// <summary>
        /// Handles OnSaved event.
        /// </summary>
        /// <param name="sender">ViewModel instance.</param>
        /// <param name="args">Event parameters.</param>
        void OnSaved(object sender, EventArgs args);

        /// <summary>
        /// Handles OnLoaded event.
        /// </summary>
        /// <param name="sender">ViewModel instance.</param>
        /// <param name="args">Event parameters.</param>
        void OnLoaded(object sender, EventArgs args);

        /// <summary>
        /// Handles OnDataError event.
        /// </summary>
        /// <param name="sender">ViewModel instance.</param>
        /// <param name="args">Event parameters.</param>
        void OnDataError(object sender, EventArgs args);

        #endregion
    }
}
