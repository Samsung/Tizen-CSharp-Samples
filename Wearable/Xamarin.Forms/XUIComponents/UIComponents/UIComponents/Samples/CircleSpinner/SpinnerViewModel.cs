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

namespace UIComponents.Samples.CircleSpinner
{
    /// <summary>
    /// Class for BindingContext in CircleSpinner
    /// </summary>
    public class SpinnerViewModel : INotifyPropertyChanged
    {
        double _value = 9.0;
        double _hour = 0;
        double _min = 0;
        double _sec = 0;

        /// <summary>
        /// Value of Spinner
        /// </summary>
        public double Value
        {
            get => _value;
            set
            {
                if (_value == value)
                {
                    return;
                }

                _value = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Value of Hr Spinner
        /// </summary>
        public double Hour
        {
            get => _hour;
            set
            {
                if (_hour == value) 
                {
                    return;
                }

                _hour = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Value of Mm Spinner
        /// </summary>
        public double Minute
        {
            get => _min;
            set
            {
                if (_min == value)
                {
                    return;
                }

                _min = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Value of Sec Spinner
        /// </summary>
        public double Second
        {
            get => _sec;
            set
            {
                if (_sec == value)
                {
                    return;
                }

                _sec = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command is executed when ActionButton of SpinnerTimer is pressed
        /// </summary>
        public ICommand TimerButtonPressedExit { get; private set; }
        /// <summary>
        /// Command is executed when ActionButton of SpinnerDefault is pressed
        /// </summary>
        public ICommand ButtonPressedExit { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public SpinnerViewModel()
        {
            // Set button event of SpinnerDefault
            ButtonPressedExit = new Command(() =>
            {
                Console.WriteLine($"Saved and Exit Value:{Value.ToString()}");
                App.Current.MainPage.Navigation.PopAsync(true);
            });

            // Set button event of SpinnerTimer
            TimerButtonPressedExit = new Command(() =>
            {
                Console.WriteLine($"Saved and Exit Hour:{Hour}, Minute:{Minute}, Second:{Second}");
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