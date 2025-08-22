/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
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

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace UIComponents.Samples.CircleDateTime
{
    /// <summary>
    /// Class for BindingContext in CircleDateTime
    /// </summary>
    public class DateTimeViewModel : INotifyPropertyChanged
    {
        static DateTime _dateTime = DateTime.Now;

        /// <summary>
        /// Setter and Getter of Datetime
        /// </summary>
        public DateTime Datetime
        {
            get => _dateTime;
            set
            {
                //Console.WriteLine($"Set Datetime value : {value.ToString()}");
                if (_dateTime == value) 
                {
                    return;
                }

                _dateTime = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command is executed when ActionButton is pressed
        /// </summary>
        public ICommand ButtonPressedExit { get; private set; }

        /// <summary>
        /// Constructor of DateTimeViewModel class
        /// </summary>
        public DateTimeViewModel()
        {
            // Set button event of CircleDateTime
            ButtonPressedExit = new Command(() =>
            {
                Console.WriteLine($"Saved and Exit Datetime:{Datetime.ToString()}");
                App.Current.MainPage.Navigation.PopAsync(true);
            });
        }

        /// <summary>
        /// Called to notify that a change of property happened
        /// </summary>
        /// <param name="propertyName">The name of the property that changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Handle the PropertyChanged event raised when a property is changed on a component
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}