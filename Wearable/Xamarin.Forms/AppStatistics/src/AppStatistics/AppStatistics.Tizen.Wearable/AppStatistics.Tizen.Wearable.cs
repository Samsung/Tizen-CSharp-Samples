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
using AppStatistics.Tizen.Wearable.Services.Privilege;
using System;
using Xamarin.Forms;

namespace AppStatistics
{
    /// <summary>
    /// AppStatistics forms application class for Tizen wearable profile.
    /// </summary>
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        #region fields

        /// <summary>
        /// Stores <see cref="PrivilegeManager" /> class instance.
        /// </summary>
        private PrivilegeManager _privilegeManager = PrivilegeManager.Instance;

        #endregion

        #region methods

        /// <summary>
        /// Handles creation phase of the forms application.
        /// Loads Xamarin application.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            _privilegeManager.PrivilegesChecked += OnPrivilegesChecked;
            _privilegeManager.CheckAllPrivileges();
        }

        /// <summary>
        /// Entry method of the program/application.
        /// </summary>
        /// <param name="args">Launch arguments.</param>
        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            global::Tizen.Wearable.CircularUI.Forms.FormsCircularUI.Init();
            app.Run(args);
        }

        /// <summary>
        /// Handles "PrivilegesChecked" event.
        /// Loads Xamarin application.
        /// </summary>
        /// <param name="sender">Object which invoked the event.</param>
        /// <param name="args">Event arguments.</param>
        private void OnPrivilegesChecked(object sender, EventArgs args)
        {
            _privilegeManager.PrivilegesChecked -= OnPrivilegesChecked;
            LoadApplication(new App());
        }

        #endregion
    }
}