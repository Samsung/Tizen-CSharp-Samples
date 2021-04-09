/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using FindPlace.Interfaces;
using FindPlace.Tizen.Wearable.Services;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Dependency = Xamarin.Forms.DependencyAttribute;

[assembly: Dependency(typeof(LoggerService))]

namespace FindPlace.Tizen.Wearable.Services
{
    /// <summary>
    /// Logger service.
    /// </summary>
    public class LoggerService : ILoggerService
    {
        #region fields

        /// <summary>
        /// Tag field for log messages.
        /// </summary>
        private const string Tag = "FindPlace";

        #endregion

        #region methods

        /// <summary>
        /// Logs debug information.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="file">File name.</param>
        /// <param name="func">Function name.</param>
        /// <param name="line">Line of code.</param>
        public void Debug(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            global::Tizen.Log.Debug(Tag, message, file, func, line);
        }

        /// <summary>
        /// Logs error.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="file">File name.</param>
        /// <param name="func">Function name.</param>
        /// <param name="line">Line of code.</param>
        public void Error(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            global::Tizen.Log.Error(Tag, message, file, func, line);
        }

        /// <summary>
        /// Logs fatal error.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="file">File name.</param>
        /// <param name="func">Function name.</param>
        /// <param name="line">Line of code.</param>
        public void Fatal(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            global::Tizen.Log.Fatal(Tag, message, file, func, line);
        }

        /// <summary>
        /// Logs information.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="file">File name.</param>
        /// <param name="func">Function name.</param>
        /// <param name="line">Line of code.</param>
        public void Info(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            global::Tizen.Log.Info(Tag, message, file, func, line);
        }

        /// <summary>
        /// Logs verbose information.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="file">File name.</param>
        /// <param name="func">Function name.</param>
        /// <param name="line">Line of code.</param>
        public void Verbose(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            global::Tizen.Log.Verbose(Tag, message, file, func, line);
        }

        /// <summary>
        /// Logs warning.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="file">File name.</param>
        /// <param name="func">Function name.</param>
        /// <param name="line">Line of code.</param>
        public void Warn(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            global::Tizen.Log.Warn(Tag, message, file, func, line);
        }

        #endregion
    }
}
