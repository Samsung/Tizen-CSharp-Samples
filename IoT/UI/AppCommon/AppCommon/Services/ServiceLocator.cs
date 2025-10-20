/*
 * Copyright (c) 2025 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;

namespace AppCommon.Services
{
    /// <summary>
    /// Simple service locator to replace Xamarin.Forms DependencyService
    /// </summary>
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        /// <summary>
        /// Register a service implementation
        /// </summary>
        public static void Register<TInterface>(TInterface implementation)
        {
            _services[typeof(TInterface)] = implementation;
        }

        /// <summary>
        /// Get a registered service
        /// </summary>
        public static TInterface Get<TInterface>()
        {
            if (_services.TryGetValue(typeof(TInterface), out var service))
            {
                return (TInterface)service;
            }
            throw new InvalidOperationException($"Service {typeof(TInterface).Name} not registered");
        }

        /// <summary>
        /// Initialize all services
        /// </summary>
        public static void Initialize()
        {
            Register<IAppInformationService>(new AppInformationService());
            Register<IBatteryInformationService>(new BatteryInformationService());
            Register<IDirectoryService>(new DirectoryService());
            Register<ISystemSettingsService>(new SystemSettingsService());
        }
    }
}
