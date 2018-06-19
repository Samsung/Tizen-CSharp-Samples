/*
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

using Native = Tizen.Applications;
using VoiceMemo.Services;
using VoiceMemo.Tizen.Wearable.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppTerminator))]

namespace VoiceMemo.Tizen.Wearable.Services
{
    /// <summary>
    /// class AppTerminator
    /// It provides a way to kill this application
    /// </summary>
    class AppTerminator : IAppTerminator
    {
        public AppTerminator()
        {
        }

        /// <summary>
        /// Terminate the the current application.
        /// </summary>
        public void TerminateApp()
        {
            Native.Application.Current.Exit();
        }
    }
}
