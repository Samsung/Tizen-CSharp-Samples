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

using HeartRateMonitor.Models;
using System;
using HeartRateMonitor.Tizen.Mobile.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ApplicationService))]
namespace HeartRateMonitor.Tizen.Mobile.Services
{
    /// <summary>
    /// The service class handling application operations.
    /// </summary>
    class ApplicationService : IApplicationService
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
                global::Tizen.Log.Error("HeartRateMonitor", "Unable to close the application");
            }
        }

        #endregion
    }
}
