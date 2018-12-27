/* 
  * Copyright (c) 2016 Samsung Electronics Co., Ltd 
  * 
  * Licensed under the Flora License, Version 1.1 (the "License"); 
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

using System;
using SmartCardSampleApp.Tizen.Wearable;

[assembly: Xamarin.Forms.Dependency(implementorType: typeof(LogImplementation))]

namespace SmartCardSampleApp.Tizen.Wearable
{
    /// <summary>
    /// The log implementation class in mobile profile
    /// </summary>
    class LogImplementation : ILog
    {
        private const String LOGTAG = "Smartcard_APP";

        /// <summary>
        /// Constructor of log implementation class
        /// </summary>
        public LogImplementation()
        {
        }
        
        /// <summary>
        /// Print log message
        /// </summary>
        /// <param name="msg">Log message</param>
        public void Log(String msg)
        {
            global::Tizen.Log.Debug(LOGTAG, msg);
        }

        /// <summary>
        /// Print log message
        /// </summary>
        /// <param name="msg">Log message</param>
        public static void DLog(String msg)
        {
            global::Tizen.Log.Debug(LOGTAG, msg);
        }
    }
}
