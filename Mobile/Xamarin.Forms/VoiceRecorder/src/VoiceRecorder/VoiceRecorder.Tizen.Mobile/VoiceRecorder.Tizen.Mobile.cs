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
using ElmSharp;
using System;
using Xamarin.Forms;
using VoiceRecorder.Tizen.Mobile.Service.Privilege;

namespace VoiceRecorder.Tizen.Mobile
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        #region

        /// <summary>
        /// Tag used for Tizen.Log entries.
        /// </summary>
        internal static String LogTag = "VoiceRecorder";

        /// <summary>
        /// Stores <see cref="PrivilegeManager" /> class instance.
        /// </summary>
        private PrivilegeManager _privilegeManager = PrivilegeManager.Instance;

        #endregion

        #region methods

        /// <summary>
        /// Handles creation phase of the forms application.
        /// Sets up windows settings.
        /// Checks privileges.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            MainWindow.AvailableRotations = DisplayRotation.Degree_0;
            _privilegeManager.PrivilegesChecked += OnPrivilegesChecked;
            _privilegeManager.CheckAllPrivileges();
        }

        /// <summary>
        /// Application entry point.
        /// Initializes Xamarin.Forms application.
        /// </summary>
        /// <param name="args">Command line arguments. Not used.</param>
        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
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
