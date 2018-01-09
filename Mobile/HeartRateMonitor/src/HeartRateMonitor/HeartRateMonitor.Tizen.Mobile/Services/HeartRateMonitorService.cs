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
using HeartRateMonitor.Models;
using HeartRateMonitor.Tizen.Mobile.Services;
using Tizen.Sensor;
using HRM = Tizen.Sensor.HeartRateMonitor;
using System.Threading.Tasks;
using Tizen.Security;
using Tizen;

[assembly: Xamarin.Forms.Dependency(typeof(HeartRateMonitorService))]
namespace HeartRateMonitor.Tizen.Mobile.Services
{
    /// <summary>
    /// HeartRateMonitorService class.
    /// Provides methods that allow the application to use the Tizen Sensor API.
    /// Implements IHeartRateMonitorService interface to be available
    /// from portable part of the application source code.
    /// </summary>
    public class HeartRateMonitorService : IHeartRateMonitorService
    {
        #region fields

        /// <summary>
        /// An instance of the HeartRateMonitor class provided by the Tizen Sensor API.
        /// </summary>
        private HRM _hrm;

        /// <summary>
        /// Number representing value of the current heart rate.
        /// </summary>
        private int _currentHeartRate;

        /// <summary>
        /// The check privileges task.
        /// </summary>
        private TaskCompletionSource<bool> _checkPrivilegesTask;

        #endregion

        #region properties

        /// <summary>
        /// HeartRateMonitorDataChanged event.
        /// Notifies UI about heart rate value update.
        /// </summary>
        public event EventHandler HeartRateMonitorDataChanged;

        /// <summary>
        /// HeartRateSensorNotSupported event.
        /// Notifies application about lack of heart rate sensor.
        /// </summary>
        public event EventHandler HeartRateSensorNotSupported;

        /// <summary>
        /// Healthinfo privilege key.
        /// </summary>
        private const string HEALTHINFO_PRIVILEGE = "http://tizen.org/privilege/healthinfo";

        #endregion

        #region methods

        /// <summary>
        /// Initializes HeartRateMonitorService class.
        /// Invokes HeartRateSensorNotSupported event if heart rate sensor is not supported.
        /// </summary>
        public void Init()
        {
            try
            {
                _hrm = new HRM
                {
                    Interval = 1000
                };

                _hrm.DataUpdated += OnDataUpdated;
            }
            catch (Exception)
            {
                HeartRateSensorNotSupported?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Returns current heart rate value provided by the Tizen Sensor API.
        /// </summary>
        /// <returns>Current heart rate value provided by the Tizen Sensor API.</returns>
        public int GetHeartRate()
        {
            return _currentHeartRate;
        }

        /// <summary>
        /// Starts notification about changes of heart rate value.
        /// </summary>
        public void StartHeartRateMonitor()
        {
            _hrm.Start();
        }

        /// <summary>
        /// Stops notification about changes of heart rate value.
        /// </summary>
        public void StopHeartRateMonitor()
        {
            _hrm.Stop();
        }

        /// <summary>
        /// Handles "DataUpdated" event of the HeartRateMonitor object provided by the Tizen Sensor API.
        /// Saves current heart rate value in the _currentHeartRate field.
        /// Invokes "HeartRateMonitorDataChanged" event.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">An instance of the HeartRateMonitorDataUpdatedEventArgs class providing detailed information about the event.</param>
        private void OnDataUpdated(object sender, HeartRateMonitorDataUpdatedEventArgs e)
        {
            _currentHeartRate = e.HeartRate;
            HeartRateMonitorDataChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Handles privilege request response from the privacy privilege manager.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="requestResponseEventArgs">Event arguments.</param>
        private void PrivilegeManagerOnResponseFetched(object sender,
            RequestResponseEventArgs requestResponseEventArgs)
        {
            if (requestResponseEventArgs.cause == CallCause.Answer)
            {
                _checkPrivilegesTask.SetResult(requestResponseEventArgs.result == RequestResult.AllowForever);
            }
            else
            {
                Log.Error("HeartRateMonitor", "Error occurred during requesting permission");
                _checkPrivilegesTask.SetResult(false);
            }
        }

        /// <summary>
        /// Returns true if all required privileges are granted, false otherwise.
        /// </summary>
        /// <returns>Task with check result.</returns>
        public async Task<bool> CheckPrivileges()
        {
            CheckResult result = PrivacyPrivilegeManager.CheckPermission(HEALTHINFO_PRIVILEGE);

            switch (result)
            {
                case CheckResult.Allow:
                    return true;
                case CheckResult.Deny:
                    return false;
                case CheckResult.Ask:
                    PrivacyPrivilegeManager.ResponseContext context = null;
                    PrivacyPrivilegeManager.GetResponseContext(HEALTHINFO_PRIVILEGE)
                        .TryGetTarget(out context);

                    if (context == null)
                    {
                        Log.Error("STT", "Unable to get privilege response context");
                        return false;
                    }

                    _checkPrivilegesTask = new TaskCompletionSource<bool>();

                    context.ResponseFetched += PrivilegeManagerOnResponseFetched;

                    PrivacyPrivilegeManager.RequestPermission(HEALTHINFO_PRIVILEGE);
                    return await _checkPrivilegesTask.Task;
                default:
                    return false;
            }
        }

        #endregion
    }
}