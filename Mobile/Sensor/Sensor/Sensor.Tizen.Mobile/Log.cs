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

using Sensor.Tizen.Mobile;
using System.Runtime.CompilerServices;

[assembly: Xamarin.Forms.Dependency(implementorType: typeof(Log))]

namespace Sensor.Tizen.Mobile
{
    /// <summary>
    /// Implementation class of ILog interface
    /// </summary>
    public class Log : ILog
    {
        /// <summary>
        /// Log tag
        /// </summary>
        public static readonly string LogTag = "Sensor.Tizen.Mobile";

        /// <summary>
        /// Constructor of Log
        /// </summary>
        public Log()
        {
        }

        /// <summary>
        /// Print a log message with error priority
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="file">File name</param>
        /// <param name="func">Method name</param>
        /// <param name="line">Line Number</param>
        public void Error(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            global::Tizen.Log.Error(LogTag, message, file, func, line);
        }

        /// <summary>
        /// Print a log message with info priority
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="file">File name</param>
        /// <param name="func">Method name</param>
        /// <param name="line">Line Number</param>
        public void Info(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            global::Tizen.Log.Info(LogTag, message, file, func, line);
        }

        /// <summary>
        /// Print a log message with debug priority
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="file">File name</param>
        /// <param name="func">Method name</param>
        /// <param name="line">Line Number</param>
        public void Debug(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            global::Tizen.Log.Debug(LogTag, message, file, func, line);
        }
    }
}
