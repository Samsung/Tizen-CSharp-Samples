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

using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Calculator.Impl
{
    /// <summary>
    /// A debugging utility class.
    /// </summary>
    public sealed class DebuggingUtils
    {
        private static IDebuggingAPIs ism;
        private static readonly DebuggingUtils instance = new DebuggingUtils();

        /// <summary>
        /// A method provides instance of DebuggingUtils. </summary>
        public static DebuggingUtils Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Default implementation of IDebuggingAPIs interface .
        /// This is required for the unit testing of the Calculator application. </summary>
        private class DefaultDebuggingAPIInstance : IDebuggingAPIs
        {
            /// <summary>
            /// A method displays a debugging message </summary>
            /// <param name="message"> A list of command line arguments.</param>
            /// <param name="file"> A file name.</param>
            /// <param name="func"> A function name.</param>
            /// <param name="line"> A line number.</param>
            public void Dbg(string message, string file, string func, int line)
            {
            }

            /// <summary>
            /// A method displays a debugging message </summary>
            /// <param name="message"> A list of command line arguments.</param>
            /// <param name="file"> A file name.</param>
            /// <param name="func"> A function name.</param>
            /// <param name="line"> A line number.</param>
            public void Err(string message, string file, string func, int line)
            {
            }

            /// <summary>
            /// A method displays a debugging message </summary>
            /// <param name="message"> A list of command line arguments.</param>
            /// <param name="file"> A file name.</param>
            /// <param name="func"> A function name.</param>
            /// <param name="line"> A line number.</param>
            public void Popup(string message, string file, string func, int line)
            {
            }
        }

        /// <summary>
        /// DebuggingUtils constructor which set interface instance. </summary>
        private DebuggingUtils()
        {
            if (DependencyService.Get<IDebuggingAPIs>() != null)
            {
                ism = DependencyService.Get<IDebuggingAPIs>();
            }
            else
            {
                ism = new DefaultDebuggingAPIInstance();
            }
        }

        /// <summary>
        /// A method displays a debugging message </summary>
        /// <param name="message"> A list of command line arguments.</param>
        /// <param name="file"> A file name.</param>
        /// <param name="func"> A function name.</param>
        /// <param name="line"> A line number.</param>
        public static void Dbg(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            ism.Dbg(message, file, func, line);
        }

        /// <summary>
        /// A method displays a error message </summary>
        /// <param name="message"> A list of command line arguments.</param>
        /// <param name="file"> A file name.</param>
        /// <param name="func"> A function name.</param>
        /// <param name="line"> A line number.</param>
        public static void Err(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            ism.Err(message, file, func, line);
        }

        /// <summary>
        /// A method displays a pop up  message </summary>
        /// <param name="message"> A list of command line arguments.</param>
        /// <param name="file"> A file name.</param>
        /// <param name="func"> A function name.</param>
        /// <param name="line"> A line number.</param>
        public static void Popup(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            ism.Popup(message, file, func, line);
        }
    }
}
