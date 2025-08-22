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

namespace BadgeCounter.Models
{
    /// <summary>
    /// Interface of services which allow to manage badge count for current application.
    /// </summary>
    public interface ICurrentAppBadgeControlService
    {
        #region properties

        /// <summary>
        /// Badge count for current application.
        /// </summary>
        int BadgeCount { get; set; }

        /// <summary>
        /// Event invoked when badge count for current application was changed.
        /// </summary>
        event EventHandler Changed;

        #endregion
    }
}
