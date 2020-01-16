/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

﻿using System.Runtime.CompilerServices;

namespace GestureSensor.Interfaces
{
    /// <summary>
    /// Interface for LoggerService.
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Logs fatal error.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="file">File name.</param>
        /// <param name="func">Function name.</param>
        /// <param name="line">Line of code.</param>
        void Fatal(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0);

        /// <summary>
        /// Logs error.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="file">File name.</param>
        /// <param name="func">Function name.</param>
        /// <param name="line">Line of code.</param>
        void Error(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0);

        /// <summary>
        /// Logs warning.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="file">File name.</param>
        /// <param name="func">Function name.</param>
        /// <param name="line">Line of code.</param>
        void Warn(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0);

        /// <summary>
        /// Logs information.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="file">File name.</param>
        /// <param name="func">Function name.</param>
        /// <param name="line">Line of code.</param>
        void Info(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0);

        /// <summary>
        /// Logs debug information.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="file">File name.</param>
        /// <param name="func">Function name.</param>
        /// <param name="line">Line of code.</param>
        void Debug(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0);

        /// <summary>
        /// Logs verbose information.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="file">File name.</param>
        /// <param name="func">Function name.</param>
        /// <param name="line">Line of code.</param>
        void Verbose(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0);
    }
}