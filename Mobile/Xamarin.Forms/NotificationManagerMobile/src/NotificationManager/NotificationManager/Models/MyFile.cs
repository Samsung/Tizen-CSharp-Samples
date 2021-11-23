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
using Xamarin.Forms;

namespace NotificationManager.Models
{
    /// <summary>
    /// MyFile class.
    /// Provides method responsible for launching MyFile application.
    /// </summary>
    public class MyFile
    {
        #region fields

        /// <summary>
        /// An instance of class that implements IMyFile interface.
        /// </summary>
        private readonly IMyFile _service;

        #endregion

        #region properties

        /// <summary>
        /// Event fired when a file is selected.
        /// </summary>
        public event EventHandler<FileSelectedEventArgs> FileSelected;

        #endregion

        #region methods

        /// <summary>
        /// MyFile class constructor.
        /// Initializes component and registers an event handler to respond
        /// to MyFile's 'FileSelected' event.
        /// </summary>
        public MyFile()
        {
            _service = DependencyService.Get<IMyFile>();

            _service.FileSelected += (s, e) => { FileSelected?.Invoke(this, e); };
        }

        /// <summary>
        /// Launches MyFile application.
        /// </summary>
        /// <param name="pathCategory">Path which should be updated after file selection.</param>
        public void Launch(PathCategory pathCategory)
        {
            _service.Launch(pathCategory);
        }

        #endregion
    }
}