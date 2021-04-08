// Copyright 2018 Samsung Electronics Co., Ltd
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace AppHistory
{
    /// <summary>
    /// Implementation class of ILog interface
    /// </summary>
    public class LogImplementation : ILog
    {
        /// <summary>
        /// Log tag
        /// </summary>
        private const string LogTag = "AppHistoryApp";

        /// <summary>
        /// Constructor of LogImplementation
        /// </summary>
        public LogImplementation()
        {
        }

        /// <summary>
        /// Print a log message with debug priority
        /// </summary>
        /// <param name="message">Log message</param>
        public void Log(string message)
        {
            global::Tizen.Log.Debug(LogTag, message);
        }
    }
}
