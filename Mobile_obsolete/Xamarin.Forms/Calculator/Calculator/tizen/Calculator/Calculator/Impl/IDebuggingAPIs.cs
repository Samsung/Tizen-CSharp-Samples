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


namespace Calculator.Impl
{
    /// <summary>
    /// A interface contains debugging methods which are using platform subsystems.
    /// </summary>
    /// <remarks>
    /// Implementing this class should be occurred in platform project.
    /// Also the implementation should be registered to the DependencyService in a app initialization.
    /// Please refer to Xamarin Dependency Service
    /// https://developer.xamarin.com/guides/xamarin-forms/dependency-service/introduction/
    /// </remarks>
    public interface IDebuggingAPIs
    {
        /// <summary>
        /// A method displays a debugging log. </summary>
        /// <param name="message"> A debugging message.</param>
        /// <param name="file"> A file name.</param>
        /// <param name="func"> A function name.</param>
        /// <param name="line"> A line number.</param>
        void Popup(string message, string file, string func, int line);

        /// <summary>
        /// A method displays a error log. </summary>
        /// <param name="message"> A error message.</param>
        /// <param name="file"> A file name.</param>
        /// <param name="func"> A function name.</param>
        /// <param name="line"> A line number.</param>
        void Dbg(string message, string file, string func, int line);

        /// <summary>
        /// A method displays a dialog with a given message. </summary>
        /// <param name="message"> A debugging message.</param>
        /// <param name="file"> A file name.</param>
        /// <param name="func"> A function name.</param>
        /// <param name="line"> A line number.</param>
        void Err(string message, string file, string func, int line);
    }
}
