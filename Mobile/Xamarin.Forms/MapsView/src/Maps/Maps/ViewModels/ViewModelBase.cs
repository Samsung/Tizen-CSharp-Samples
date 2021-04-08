/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

namespace Maps.ViewModels
{
    /// <summary>
    /// ViewModelBase class.
    /// It implements INotifyPropertyChanged interface
    /// so that supports creating bindable properties in other view model classes.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region properties

        /// <summary>
        /// PropertyChanged event handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region methods

        /// <summary>
        /// Updates value of the 'storage' argument with value given by the second argument.
        /// Notifies the application about update of the property which has executed this method.
        /// </summary>
        /// <param name="storage">Property's backing field.</param>
        /// <param name="value">New value to set.</param>
        /// <param name="propertyName">Property name.</param>
        /// <typeparam name="T">Property type.</typeparam>
        /// <returns>True if property value changed, false otherwise.</returns>
        protected bool SetProperty<T>(ref T storage, T value,
            [CallerMemberName] string propertyName = null)
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
        /// Dispatches attached events with name of changed property name.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}