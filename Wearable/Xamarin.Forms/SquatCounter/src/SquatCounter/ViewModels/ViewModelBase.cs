/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd. All rights reserved.
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

namespace SquatCounter.ViewModels
{
    /// <summary>
    /// ViewModelBase class.
    /// Implements INotifyPropertyChanged interface
    /// so that it supports creating properties in other view model classes.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// PropertyChanged event handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Updates value of the "storage" argument with value given by the second argument.
        /// Notifies the application about update of the property which has executed this method.
        /// </summary>
        /// <typeparam name="T">Type of storage field.</typeparam>
        /// <param name="storage">Field reference.</param>
        /// <param name="value">New value.</param>
        /// <param name="propertyName">Name of property triggered change.</param>
        /// <returns>Returns true if storage is successfully updated, false otherwise.</returns>
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
        /// Notifies the application about update of the property with name given as a parameter.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}