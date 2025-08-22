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


using ElmSharp;
using Calculator.Impl;
using Tizen;
using System.Runtime.CompilerServices;

namespace Calculator.Tizen
{
    /// <summary>
    /// Platform dependent implementation for the Logging and the Pop-up displaying.
    /// DebuggingPort is implementing IDebuggingAPIs which is defined in Calculator shared project.
    /// </summary>
    /// <remarks>
    /// Please refer to Xamarin Dependency Service
    /// https://developer.xamarin.com/guides/xamarin-forms/dependency-service/introduction/
    /// </remarks>
    class DebuggingPort : IDebuggingAPIs
    {
        /// <summary>
        /// A Calculator Windows reference. This is used to display a Dialog</summary>
        public static Window MainWindow
        {
            set;
            get;
        }

        /// <summary>
        /// A Logging Tag. </summary>
        public static string TAG = "calculator";

        /// <summary>
        /// A method displays a debugging log. </summary>
        /// <param name="message"> A debugging message.</param>
        /// <param name="file"> A file name.</param>
        /// <param name="func"> A function name.</param>
        /// <param name="line"> A line number.</param>
        public void Dbg(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            Log.Debug(TAG, message, file, func, line);
        }

        /// <summary>
        /// A method displays a error log. </summary>
        /// <param name="message"> A error message.</param>
        /// <param name="file"> A file name.</param>
        /// <param name="func"> A function name.</param>
        /// <param name="line"> A line number.</param>
        public void Err(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            Log.Error(TAG, message, file, func, line);
        }

        /// <summary>
        /// A method displays a dialog with a given message. </summary>
        /// <param name="message"> A debugging message.</param>
        /// <param name="file"> A file name.</param>
        /// <param name="func"> A function name.</param>
        /// <param name="line"> A line number.</param>
        public void Popup(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            if (MainWindow == null)
            {
                return;
            }
            /*
            Dialog toast = new Dialog(MainWindow);
            toast.Title = message;
            toast.Timeout = 2.3;
            toast.BackButtonPressed += (s, e) =>
            {
                toast.Dismiss();
            };
            toast.Show();
            */
            Popup popup = new Popup(MainWindow);
            popup.Append(message);
            popup.Timeout = 2.3;
            popup.OutsideClicked += (s, e) =>
            {
                popup.Dismiss();
            };
            popup.Show();
        }

        /// <summary>
        /// A method displays a debugging log. </summary>
        /// <param name="message"> A debugging message.</param>
        /// <param name="file"> A file name.</param>
        /// <param name="func"> A function name.</param>
        /// <param name="line"> A line number.</param>
        public static void D(string message, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            Log.Debug(TAG, message, file, func, line);
        }
    }
}