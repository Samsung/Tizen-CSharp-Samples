/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using ElmSharp;
using Geocoding.Tizen.Wearable.Controls;
using Geocoding.Tizen.Wearable.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using EProgressBar = ElmSharp.ProgressBar;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CircleProgress), typeof(CircleProgressRenderer))]
namespace Geocoding.Tizen.Wearable.Renderers
{
    /// <summary>
    /// Circle progress renderer.
    /// </summary>
    public class CircleProgressRenderer : ViewRenderer<CircleProgress, EProgressBar>
    {
        #region methods

        /// <summary>
        /// Overridden OnElementChanged method for initializing new control.
        /// </summary>
        /// <param name="e">Event argument.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<CircleProgress> e)
        {
            if (Control == null)
            {
                var progressBar = new EProgressBar(Forms.NativeParent);
                SetNativeControl(progressBar);
            }

            if (e.NewElement != null)
            {
                Control.Style = "process/popup/small";
                Control.Color = ElmSharp.Color.FromHex("#197847");
                Control.Show();
                Control.PlayPulse();
            }

            base.OnElementChanged(e);
        }

        #endregion
    }
}
