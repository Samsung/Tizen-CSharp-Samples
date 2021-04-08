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
using Xamarin.Forms;

namespace UIComponents.Samples
{
    /// <summary>
    /// Class for BindingContext in CircleProgressBar
    /// </summary>
    public class CircleProgressBarViewModel : INotifyPropertyChanged
    {
        double _progress1;
        double _progress2;
        string _progressLabel1 = "0 %";
        string _progressLabel2 = "0 %";

        /// <summary>
        /// String of Label of Progress 1
        /// </summary>
        public string ProgressLabel1
        {
            get => _progressLabel1;
            set
            {
                if (_progressLabel1 == value)
                {
                    return;
                }

                _progressLabel1 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// String of Label of Progress 2
        /// </summary>
        public string ProgressLabel2
        {
            get => _progressLabel2;
            set
            {
                if (_progressLabel2 == value)
                {
                    return;
                }

                _progressLabel2 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Value of Progress 1
        /// </summary>
        public double ProgressValue1
        {
            get => _progress1;
            set
            {
                if (_progress1 == value)
                {
                    return;
                }

                _progress1 = value;
                ProgressLabel1 = _progress1 * 100 + " %";
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Value of Progress 2
        /// </summary>
        public double ProgressValue2
        {
            get => _progress2;
            set
            {
                if (_progress2 == value)
                {
                    return;
                }

                _progress2 = value;
                ProgressLabel2 = _progress2 * 100 + " %";
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Whether it is playing
        /// </summary>
        public bool Playing { get; set; }

        /// <summary>
        /// Constructor of CircleProgressBarViewModel class
        /// </summary>
        public CircleProgressBarViewModel()
        {
            Playing = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(100), UpdateProgress1);
            Device.StartTimer(TimeSpan.FromMilliseconds(200), UpdateProgress2);
        }

        /// <summary>
        /// Called when state of Progress 1 is changed
        /// </summary>
        /// <returns>Playing value</returns>
        bool UpdateProgress1()
        {
            ProgressValue1 += 0.05;
            if (ProgressValue1 > 1.0)
            {
                ProgressValue1 = 0;
            }

            return Playing;
        }

        /// <summary>
        /// Called when state of Progress 2 is changed
        /// </summary>
        /// <returns>Playing value</returns>
        bool UpdateProgress2()
        {
            ProgressValue2 += 0.05;
            if (ProgressValue2 > 1.0)
            {
                ProgressValue2 = 0;
            }

            return Playing;
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
        /// Event raised when a property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}