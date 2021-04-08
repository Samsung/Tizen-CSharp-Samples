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

using NetworkApp.Tizen.Mobile;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(implementorType: typeof(LogImplementation))]

namespace NetworkApp.Tizen.Mobile
{
    /// <summary>
    /// Implementation class of ILog interface
    /// </summary>
    public class LogImplementation : ILog
    {
        /// <summary>
        /// Log tag
        /// </summary>
        private const String LOGTAG = "NetworkApp";

        /// <summary>
        /// constructor
        /// </summary>
        public LogImplementation()
        {
        }

        /// <summary>
        /// Print DEBUG mode log
        /// </summary>
        /// <param name="message">Log message</param>
        public void Log(String message)
        {
            global::Tizen.Log.Debug(LOGTAG, message);
        }

        /// <summary>
        /// Print DEBUG mode log
        /// </summary>
        /// <param name="message">Log message</param>
        public static void DLog(String message)
        {
            global::Tizen.Log.Debug(LOGTAG, message);
        }
    }
}
