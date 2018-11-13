﻿//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System.Runtime.CompilerServices;

namespace NetworkApp.Models
{
    /// <summary>
    /// Logger class for logging the info.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Log tag
        /// </summary>
        private const string LOGTAG = "MediaAPP";

        /// <summary>
        /// Prints DEBUG mode log
        /// </summary>
        /// <param name="message">Log message</param>
        public static void Log(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            global::Tizen.Log.Debug(LOGTAG, message, file, func, line);
        }
    }
}
