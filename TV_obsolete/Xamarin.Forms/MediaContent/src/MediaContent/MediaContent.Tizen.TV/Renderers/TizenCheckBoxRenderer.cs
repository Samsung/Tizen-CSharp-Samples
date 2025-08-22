/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using MediaContent.Tizen.Mobile.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using MediaContent.Views.Renderers;

[assembly: ExportRenderer(typeof(TizenCheckBox), typeof(TizenCheckBoxRenderer))]
namespace MediaContent.Tizen.Mobile.Renderers
{
    /// <summary>
    /// Tizen TizenCheckBoxRenderer control.
    /// </summary>
    public class TizenCheckBoxRenderer : SwitchRenderer
    {
        #region methods

        /// <summary>
        /// Overridden OnElementChanged method for setting new control style to TizenCheckBoxRenderer.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);
            Control.Style = "default";
        }

        #endregion methods
    }
}
