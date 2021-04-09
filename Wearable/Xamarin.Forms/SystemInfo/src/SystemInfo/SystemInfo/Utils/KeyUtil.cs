/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace SystemInfo.Utils
{
    /// <summary>
    /// Class that allows getting value of device's features.
    /// </summary>
    public static class KeyUtil
    {
        #region methods

        /// <summary>
        /// Gets string value of feature by its name.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>Value of the feature.</returns>
        public static string GetKeyValue<T>(string key)
        {
            var result = string.Empty;


            if (typeof(T) == typeof(bool))
            {
                try
                {
                    var boolValue = DependencyService.Get<IKeyUtil>().TryGetValueToBool(key);
                    result = boolValue ? "Supported" : "Not Supported";
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            else if (typeof(T) == typeof(int))
            {
                try
                {
                    var intValue = DependencyService.Get<IKeyUtil>().TryGetValueToInt(key);
                    result = intValue.ToString();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            else if (typeof(T) == typeof(string))
            {
                try
                {
                    var stringValue = DependencyService.Get<IKeyUtil>().TryGetValueToString(key);
                    result = stringValue;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            else if (typeof(T) == typeof(double))
            {
                try
                {
                    var doubleValue = DependencyService.Get<IKeyUtil>().TryGetValueToDouble(key);
                    result = doubleValue.ToString();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            else if (typeof(T) == typeof(DateTime))
            {
                try
                {
                    var datetimeValue = DependencyService.Get<IKeyUtil>().TryGetValueToDateTime(key);
                    result = datetimeValue.ToString();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            return result;
        }

        #endregion
    }
}