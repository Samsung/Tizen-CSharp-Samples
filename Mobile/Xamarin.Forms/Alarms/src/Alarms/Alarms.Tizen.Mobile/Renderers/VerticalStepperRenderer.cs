/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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

using Alarms.Controls;
using Alarms.Tizen.Mobile.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(VerticalStepper), typeof(VerticalStepperRenderer))]

namespace Alarms.Tizen.Mobile.Renderers
{
    /// <summary>
    /// Tizen vertical version of Stepper control.
    /// </summary>
    public class VerticalStepperRenderer : StepperRenderer
    {
        #region methods

        /// <summary>
        /// Overridden OnElementChanged method for setting new control style to Stepper.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Stepper> e)
        {
            base.OnElementChanged(e);
            Control.Style = "vertical";
            Control.IsEditable = true;
        }

        #endregion
    }
}
