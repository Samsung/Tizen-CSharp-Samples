/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd. All rights reserved.
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
using Pedometer.Services;
using Pedometer.Privilege;

namespace Pedometer.ViewModels
{
    /// <summary>
    /// Provides main page view abstraction
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private int _calories = 0;
        private int _steps = 0;
        private int _speedAverage = 0;
        private string _distance = "0";

        /// <summary>
        /// Calories burned
        /// </summary>
        public int Calories
        {
            get => _calories;
            set => SetProperty(ref _calories, value);
        }

        /// <summary>
        /// Steps made
        /// </summary>
        public int Steps
        {
            get => _steps;
            set => SetProperty(ref _steps, value);
        }

        /// <summary>
        /// Average speed
        /// </summary>
        public int SpeedAverage
        {
            get => _speedAverage;
            set => SetProperty(ref _speedAverage, value);
        }

        /// <summary>
        /// Distance covered
        /// </summary>
        public string Distance
        {
            get => _distance;
            set => SetProperty(ref _distance, value);
        }

        /// <summary>
        /// Initializes MainViewModel class instance
        /// </summary>
        public MainViewModel()
        {
            var privilegeManager = PrivilegeManager.Instance;
            if (!privilegeManager.AllPermissionsGranted())
            {
                return;
            }

            var service = PedometerService.Instance;
            service.PedometerUpdated += Service_PedometerUpdated;
        }

        /// <summary>
        /// Handles execution of PedometerUpdated event
        /// </summary>
        /// <param name="sender">Object that invoked event</param>
        /// <param name="e">Event Arguments connected with PedometerUpdated event</param>
        private void Service_PedometerUpdated(object sender, PedometerUpdatedEventArgs e)
        {
            Calories = e.Calories;
            Steps = e.Steps;
            SpeedAverage = e.SpeedAverage;
            Distance = ((double)e.Distance / 1000).ToString("0.###");
        }
    }
}