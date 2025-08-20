/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Preference.ViewModels
{
    /// <summary>
    /// ViewModelBase class.
    /// Provides dispatching event for property change.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region properties

        /// <summary>
        /// Property change event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region methods

        /// <summary>
        /// Sets field value with
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage">Field to store value.</param>
        /// <param name="value">Value.</param>
        /// <param name="propertyName">Name of property triggering value change.</param>
        /// <returns>True if new value differs with value stored. False otherwise.</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Invokes PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Changed property name</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}