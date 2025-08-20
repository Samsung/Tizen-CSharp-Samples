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
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HeartRateMonitor.Models
{
    /// <summary>
    /// HeartRateMonitorModel class.
    /// Provides methods that allow the application to use the Tizen Sensor API.
    /// </summary>
    public class HeartRateMonitorModel
    {
        #region fields

        /// <summary>
        /// An instance of the IHeartRateMonitorService service.
        /// </summary>
        private readonly IHeartRateMonitorService _service;

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

        #endregion

        #region methods

        /// <summary>
        /// HeartRateMonitorModel class constructor.
        /// </summary>
        public HeartRateMonitorModel()
        {
            _service = DependencyService.Get<IHeartRateMonitorService>();

            _service.HeartRateMonitorDataChanged += ServiceOnHeartRateMonitorDataChanged;
            _service.HeartRateSensorNotSupported += ServiceOnHeartRateSensorNotSupported;
        }

        /// <summary>
        /// Initializes the model.
        /// </summary>
        public void Init()
        {
            _service.Init();
        }

        /// <summary>
        /// Returns current heart rate value provided by the Tizen Sensor API.
        /// </summary>
        /// <returns>Current heart rate value provided by the Tizen Sensor API.</returns>
        public int GetHeartRate()
        {
            return DependencyService.Get<IHeartRateMonitorService>().GetHeartRate();
        }

        /// <summary>
        /// Starts notification about changes of heart rate value.
        /// </summary>
        public void StartHeartRateMonitor()
        {
            DependencyService.Get<IHeartRateMonitorService>().StartHeartRateMonitor();
        }

        /// <summary>
        /// Stops notification about changes of heart rate value.
        /// </summary>
        public void StopHeartRateMonitor()
        {
            DependencyService.Get<IHeartRateMonitorService>().StopHeartRateMonitor();
        }

        /// <summary>
        /// Handles "HeartRateMonitorDataChanged" of the IHeartRateMonitorService object.
        /// Invokes "HeartRateMonitorDataChanged" to other application's modules.
        /// </summary>
        /// <param name="sender">>Object firing the event.</param>
        /// <param name="e">Agruments passed to the event.</param>
        private void ServiceOnHeartRateMonitorDataChanged(object sender, EventArgs e)
        {
            HeartRateMonitorDataChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Handles "HeartRateSensorNotSupported" of the IHeartRateMonitorService object.
        /// Invokes "HeartRateSensorNotSupported" to other application's modules.
        /// </summary>
        /// <param name="sender">>Object firing the event.</param>
        /// <param name="e">Agruments passed to the event.</param>
        private void ServiceOnHeartRateSensorNotSupported(object sender, EventArgs e)
        {
            HeartRateSensorNotSupported?.Invoke(sender, e);
        }

        /// <summary>
        /// Returns true if all required privileges are granted, false otherwise.
        /// </summary>
        /// <returns>Task with check result.</returns>
        public async Task<bool> CheckPrivileges()
        {
            return await _service.CheckPrivileges();
        }

        #endregion
    }
}