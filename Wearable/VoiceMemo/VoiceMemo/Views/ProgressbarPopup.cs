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
using Xamarin.Forms;

namespace VoiceMemo.Views
{
    public class ProgressbarPopup : BindableObject
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(ProgressbarPopup), null);

        public static readonly BindableProperty DurationProperty = BindableProperty.Create(nameof(Duration), typeof(double), typeof(ProgressbarPopup), 1.5);

        public static readonly BindableProperty ProgressbarTextProperty = BindableProperty.Create(nameof(ProgressbarText), typeof(string), typeof(ProgressbarPopup), null);

        public static readonly BindableProperty ProgressbarDurationProperty = BindableProperty.Create(nameof(ProgressbarDuration), typeof(double), typeof(ProgressbarPopup), 0.8);

        IProgressbarPopup _popUp = null;
        public event EventHandler BackButtonPressed;
        public event EventHandler TimedOut;

        public ProgressbarPopup()
        {
            _popUp = DependencyService.Get<IProgressbarPopup>(DependencyFetchTarget.NewInstance);
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
            SetBinding(ProgressbarTextProperty, new Binding(nameof(ProgressbarText), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(ProgressbarDurationProperty, new Binding(nameof(ProgressbarDuration), mode: BindingMode.OneWayToSource, source: _popUp));
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
        /// Gets or sets text of the Popup.
        /// </summary>
        public string ProgressbarText
        {
            get { return (string)GetValue(ProgressbarTextProperty); }
            set { SetValue(ProgressbarTextProperty, value); }
        }

        /// <summary>
        /// Duration for Progressbar
        /// How long to display the text message with progressbar
        /// </summary>
        double ProgressbarDuration
        {
            get { return (double)GetValue(ProgressbarDurationProperty); }
            set { SetValue(ProgressbarDurationProperty, value); }
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
