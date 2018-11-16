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
using System.Windows.Input;
using Xamarin.Forms;

namespace RotaryTimer.Views
{
    /// <summary>
    /// The view which displays timer components.
    /// </summary>
    public partial class TimerView : ContentPage
    {
        #region properties

        /// <summary>
        /// Command setting stop button image.
        /// </summary>
        private ICommand SetStopButtonPressed { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes timer view.
        /// </summary>
        public TimerView()
        {
            SetStopButtonPressed = new Command(() => { StopButton.Source = "button_stop_pressed.png"; });
            InitializeComponent();
        }

        #endregion
    }
}
