/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Clock.Controls;
using Clock.Tizen.Mobile.Renderers;
using ElmSharp;
using System;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(CheckBox), typeof(CheckBoxRenderer))]

namespace Clock.Tizen.Mobile.Renderers
{
    /// <summary>
    /// The renderer of a CheckBox widget
    /// </summary>
    public class CheckBoxRenderer : ViewRenderer<CheckBox, Check>
    {
        /// <summary>
        /// The default constructor.
        /// </summary>
        public CheckBoxRenderer()
        {
            RegisterPropertyHandler(CheckBox.IsFavoriteStyleProperty, UpdateIsFavoriteStyle);
            RegisterPropertyHandler(CheckBox.IsCheckedProperty, UpdateIsChecked);
        }

        /// <summary>
        /// Invoked whenever the CheckBox element has been changed in Xamarin.
        /// </summary>
        /// <param name="e">Event parameters.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
        {
            if (Control == null)
            {
                var checkBox = new Check(Forms.Context.MainWindow)
                {
                    //PropagateEvents = false,
                };
                SetNativeControl(checkBox);
            }

            if (e.OldElement != null)
            {
                Control.StateChanged -= CheckChangedHandler;
            }

            if (e.NewElement != null)
            {
                Control.StateChanged += CheckChangedHandler;
            }

            base.OnElementChanged(e);
        }

        void CheckChangedHandler(object sender, EventArgs e)
        {
            Element.SetValue(CheckBox.IsCheckedProperty, Control.IsChecked);
        }

        void HandleToggled()
        {
            Control.IsChecked = Element.IsChecked;
        }

        void UpdateIsFavoriteStyle()
        {
            if (Element.IsFavoriteStyle)
            {
                Control.Style = "favorite";
            }
            else
            {
                Control.Style = "default";
            }
        }

        void UpdateIsChecked()
        {
            Control.IsChecked = Element.IsChecked;
        }
    }
}
