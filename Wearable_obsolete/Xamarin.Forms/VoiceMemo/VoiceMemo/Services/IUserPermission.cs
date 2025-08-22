﻿/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Threading.Tasks;

namespace VoiceMemo.Services
{
    /// <summary>
    /// Interface to use DependencyService
    /// Platform-specific functionality : to get user permission
    /// User consent is mandatory when application requires privacy privileges
    /// </summary>
    public interface IUserPermission
    {
        /// <summary>
        /// Request user permission for privacy privileges
        /// </summary>
        /// <param name="service">privacy privilege</param>
        /// <returns>true if user consent is gained</returns>
        Task<bool> GetPermission(string service);
    }
}
