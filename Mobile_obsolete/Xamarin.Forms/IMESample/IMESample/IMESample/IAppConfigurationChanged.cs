/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
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

namespace IMESample
{
    /// <summary>
    /// Define app orientation value.
    /// </summary>
    public enum AppOrientation
    {
        Portrait,
        Landscape,
    }

    /// <summary>
    /// A interface which is using for notifying application configuration change
    /// from the platform project to the shared project.
    /// </summary>
    /// <remarks>
    /// Currently only orientation changed is notified by this interface,
    /// but other configuration such as platform dependent event can be notified
    /// by adding new interface method.
    /// </remarks>
    interface IAppConfigurationChanged
    {
        /// <summary>
        /// A method which provides the device orientation change
        /// </summary>
        /// <param name="orientation">
        /// A device orientation value, AppOrientation.Portrait, AppOrientation.Landscape
        /// </param>
        void OnOrientationChanged(AppOrientation orientation);
    }
}