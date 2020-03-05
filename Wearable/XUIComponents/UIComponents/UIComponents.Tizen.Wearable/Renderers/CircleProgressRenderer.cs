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
using UIComponents.Extensions;
using UIComponents.Tizen.Wearable.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using EProgressBar = ElmSharp.ProgressBar;


[assembly: ExportRenderer(typeof(CircleProgress), typeof(CircleProgressRenderer))]
namespace UIComponents.Tizen.Wearable.Renderers
{
    public class CircleProgressRenderer : ViewRenderer<CircleProgress, EProgressBar>
    {
        /// <summary>
        /// Constructor of CircleProgressRenderer class
        /// </summary>
        public CircleProgressRenderer()
        {
        }

        /// <summary>
        /// Called when element is changed.
        /// </summary>
        /// <param name="e">Argument for ElementChangedEventArgs<CircleProgress></param>
        protected override void OnElementChanged(ElementChangedEventArgs<CircleProgress> e)
        {
            if (Control == null)
            {
                var progressBar = new EProgressBar(Forms.NativeParent);
                SetNativeControl(progressBar);
            }

            if (e.NewElement != null)
            {
                UpdateOption();
            }

            base.OnElementChanged(e);
        }

        /// <summary>
        /// Called when element property is changed.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">PropertyChangedEvent</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == CircleProgress.OptionProperty.PropertyName)
            {
                UpdateOption();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        /// <summary>
        /// Set style of pulsing animation
        /// </summary>
        void UpdateOption()
        {
            if (((CircleProgress)Element).Option == ProgressOptions.Large)
            {
                Control.Style = "process";
            }
            else
            {
                Control.Style = "process/popup/small";
            }

            Control.Show();
            Control.PlayPulse();
        }
    }
}
