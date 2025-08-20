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
using System;

namespace VoiceMemo.Views
{
    /// <summary>
    /// Class GraphicPopUp
    /// </summary>
    public class GraphicPopUp : BindableObject
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(GraphicPopUp), null);

        public static readonly BindableProperty DurationProperty = BindableProperty.Create(nameof(Duration), typeof(double), typeof(GraphicPopUp), 1.5);

        IGraphicPopup _popUp = null;

        public event EventHandler BackButtonPressed;
        public event EventHandler TimedOut;

        // Constructor
        public GraphicPopUp()
        {
            _popUp = DependencyService.Get<IGraphicPopup>(DependencyFetchTarget.NewInstance);
            if (_popUp == null)
            {
                throw new Exception("Object reference not set to an instance of a Popup.");
            }

            _popUp.BackButtonPressed += (s, e) =>
            {
                BackButtonPressed?.Invoke(this, EventArgs.Empty);
            };

            _popUp.TimedOut += (s, e) =>
            {
                TimedOut?.Invoke(this, EventArgs.Empty);
            };

            SetBinding(TextProperty, new Binding(nameof(Text), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(DurationProperty, new Binding(nameof(Duration), mode: BindingMode.OneWayToSource, source: _popUp));
        }

        /// <summary>
        /// Gets or sets text of the Popup.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets duration of the Popup.
        /// </summary>
        public double Duration
        {
            get { return (double)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        /// <summary>
        /// Show Toast Popup
        /// </summary>
        public void Show()
        {
            _popUp.Show();
        }
    }
}
