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
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace Alarm.Controls
{
    /// <summary>
    /// A element button which is implemented with a custom renderer based on a native image. 
    /// </summary>
    public class ImageButton : Image
    {
        public static readonly BindableProperty BlendColorProperty = BindableProperty.Create("BlendColor", typeof(Color), typeof(ImageButton), Color.Default);

        public event EventHandler Clicked;

        public event EventHandler Released;

        /// <summary>
        /// A color when the button is pressed. 
        /// </summary>
        public Color BlendColor
        {
            get { return (Color)GetValue(BlendColorProperty); }
            set { SetValue(BlendColorProperty, value); }
        }

        /// <summary>
        /// To broadcast a click event to subscribers
        /// </summary>
        public void SendClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// To broadcast a release event to subscribers
        /// </summary>
        public void SendReleased()
        {
            Released?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// ImageButton constructor
        /// </summary>
        public ImageButton()
        {
            BlendColor = Color.FromRgba(255, 255, 255, 100);
        }
    }
}
