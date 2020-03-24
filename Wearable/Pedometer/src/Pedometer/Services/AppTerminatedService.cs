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
using System;

namespace Pedometer.Services
{
    /// <summary>
    /// Service for managing app state
    /// </summary>
    public sealed class AppTerminatedService
    {
        private static AppTerminatedService _this;

        /// <summary>
        /// Initializes AppTerminatedService class
        /// </summary>
        private AppTerminatedService()
        {
        }

        /// <summary>
        /// Provides singleton instance of AppTerminatedService class
        /// </summary>
        public static AppTerminatedService Instance
        {
            get
            {
                if (_this == null)
                {
                    _this = new AppTerminatedService();
                }

                return _this;
            }
        }

        /// <summary>
        /// Event invoked when app is terminated
        /// </summary>
        public event EventHandler Terminated;

        /// <summary>
        /// Invokes Terminated event
        /// </summary>
        public void Terminate()
        {
            Terminated?.Invoke(this, EventArgs.Empty);
        }
    }
}