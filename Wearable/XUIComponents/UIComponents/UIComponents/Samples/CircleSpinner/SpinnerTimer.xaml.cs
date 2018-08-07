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

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace UIComponents.Samples.CircleSpinner
{
    /// <summary>
    /// SpinnerTimer class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpinnerTimer : CirclePage
    {
        /// <summary>
        /// Constructor of SpinnerTimer class
        /// </summary>
        public SpinnerTimer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when is focused on Hr spinner
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">Argument of ValueChangedEventArgs</param>
        void OnFocusedHr(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusObject = StepperHr3;
        }
        /// <summary>
        /// Called when is focused on Mm spinner
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">Argument of ValueChangedEventArgs</param>
        void OnFocusedMm(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusObject = StepperMm3;
        }
        /// <summary>
        /// Called when is focused on Sec spinner
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">Argument of ValueChangedEventArgs</param>
        void OnFocusedSec(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusObject = StepperSec3;
        }
    }
}
