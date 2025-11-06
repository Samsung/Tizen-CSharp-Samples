/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

namespace Calculator
{
    /// <summary>
    /// A enum which provides App orientation values.
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
        /// <seealso cref="global::Calculator.AppOrientation">
        void OnOrientationChanged(AppOrientation orientation);
    }
}
