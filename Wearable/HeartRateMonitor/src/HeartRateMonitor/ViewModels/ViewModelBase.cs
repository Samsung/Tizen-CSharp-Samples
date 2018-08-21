
//Copyright 2018 Samsung Electronics Co., Ltd
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


using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HeartRateMonitor.ViewModels
{
    /// <summary>
    /// ViewModelBase class.
    /// It implements INotifyPropertyChanged interface
    /// so that supports notifying about properties changes.
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
        /// Updates value of the "storage" argument with value given by the second argument.
        /// Notifies the application about update of the property which has executed this method.
        /// </summary>
        /// <param name="storage">Value storage object</param>
        /// <param name="value">Value to set</param>
        /// <param name="propertyName">Automatically obtained property name</param>
        /// <typeparam name="T">Property value type</typeparam>
        /// <returns>Returns true if storage is successfully updated, false otherwise.</returns>
        protected bool SetProperty<T>(ref T storage, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
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
        /// <param name="propertyName">Name of the changed property.</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}