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
using System;
using VoiceRecorder.Model;
using VoiceRecorder.Tizen.Mobile.Service;

[assembly: Xamarin.Forms.Dependency(typeof(ApplicationService))]

namespace VoiceRecorder.Tizen.Mobile.Service
{
    /// <summary>
    /// Controls application.
    /// </summary>
    public class ApplicationService : IApplicationService
    {
        #region methods

        /// <summary>
        /// Closes the application.
        /// </summary>
        public void Close()
        {
            try
            {
                global::Tizen.Applications.Application.Current.Exit();
            }
            catch (Exception)
            {
                global::Tizen.Log.Error(Program.LogTag, "Unable to close the application");
            }
        }

        #endregion
    }
}