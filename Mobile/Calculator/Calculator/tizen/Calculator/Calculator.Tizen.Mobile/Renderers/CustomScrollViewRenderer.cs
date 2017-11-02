/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

using Xamarin.Forms.Platform.Tizen;

using Calculator.Controls;
using Calculator.Tizen.Renderers;

using ElmSharp;
using System;

using Tizen;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CustomScrollView), typeof(CustomScrollViewRenderer))]
namespace Calculator.Tizen.Renderers
{
    /// <summary>
    /// Calculator scroll view custom renderer.
    /// To remove vertical scroll bar this custom renderer is made.
    /// </summary>
    /// <remarks>
    /// Please refer to Xamarin.Forms Custom Renderer
    /// https://developer.xamarin.com/guides/xamarin-forms/custom-renderer/
    /// </remarks>
    class CustomScrollViewRenderer : ScrollViewRenderer
    {
        /// <summary>
        /// To remove scrollbar, set scrollbar policies as invisible whenever the control is changed.
        /// </summary>
        /// <param name="e">Element changed event information</param>
        protected override void OnElementChanged(ElementChangedEventArgs<ScrollView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.HorizontalScrollBarVisiblePolicy = ScrollBarVisiblePolicy.Invisible;
                Control.VerticalScrollBarVisiblePolicy = ScrollBarVisiblePolicy.Invisible;
            }
        }

        /// <summary>
        /// If the orientation is changed, set scrollbar policies as invisible to remove scroll bar.
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">property changed event information</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Xamarin.Forms.ScrollView.OrientationProperty.PropertyName == e.PropertyName)
            {
                Control.HorizontalScrollBarVisiblePolicy = ScrollBarVisiblePolicy.Invisible;
                Control.VerticalScrollBarVisiblePolicy = ScrollBarVisiblePolicy.Invisible;
            }
        }
    }
}