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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Weather.ViewModels
{
    /// <summary>
    /// Base ViewModel class.
    /// </summary>
    public class ViewModelBase : BindableObject
    {
        /// <summary>
        /// Generic set property method which also calls OnPropertyChanged() after value modification.
        /// </summary>
        /// <param name="storage">Value storage object</param>
        /// <param name="value">Value to set</param>
        /// <param name="propertyName">Automatically obtained property name</param>
        /// <typeparam name="T">Property value type</typeparam>
        /// <returns>True if value was changed, false if value is not different from current.</returns>
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
    }
}