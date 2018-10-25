/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BadgeCounter.Controls
{
    /// <summary>
    /// The control which displays integer value and performs animation during value change.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Counter : ContentView
    {
        #region fields

        /// <summary>
        /// Value change animation time (in milliseconds).
        /// </summary>
        private const int ANIMATION_TIME = 600;

        /// <summary>
        /// Maximum value which the control can display.
        /// Everything above this value is displayed as "99+".
        /// </summary>
        private const int MAX_VALUE = 99;

        /// <summary>
        /// Text displayed when the counter value exceeds maximum value.
        /// </summary>
        private const string MAX_VALUE_EXCEEDED_TEXT = "99+";

        /// <summary>
        /// Snapshot of previous value (available till animation end).
        /// </summary>
        private int _valueSnapshot = 0;

        #endregion

        #region properties

        /// <summary>
        /// Bindable property definition for the counter value.
        /// </summary>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
            "Value", typeof(int), typeof(Counter), 0);

        /// <summary>
        /// Counter value.
        /// </summary>
        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Creates the control instance.
        /// </summary>
        public Counter()
        {
            InitializeComponent();
            SyncLabelsWithValue();
            _valueSnapshot = Value;
        }

        /// <summary>
        /// Formats text displayed in the control.
        /// </summary>
        /// <param name="value">Counter value.</param>
        /// <returns>Formatted value.</returns>
        private string FormatValue(int value)
        {
            if (value > MAX_VALUE)
            {
                return MAX_VALUE_EXCEEDED_TEXT;
            }

            // precede with 0 in case of 0-9
            return value.ToString("D2");
        }

        /// <summary>
        /// Synchronizes text displayed in control's labels with the counter value.
        /// </summary>
        private void SyncLabelsWithValue()
        {
            CurrentLabel.TranslationX = 0;
            CurrentLabel.Text = FormatValue(Value);
            NextLabel.TranslationX = 0;
            NextLabel.Text = FormatValue(Value);
        }

        /// <summary>
        /// Returns true if value change animation is running, false otherwise.
        /// </summary>
        /// <returns>True in case of running animation, false otherwise.</returns>
        private bool ValueChangeAnimationIsRunning()
        {
            return CurrentLabel.AnimationIsRunning("TranslateTo") ||
                NextLabel.AnimationIsRunning("TranslateTo");
        }

        /// <summary>
        /// Animates counter value change.
        /// The animation consists in smooth entering of next value
        /// from the left or from the right.
        /// </summary>
        /// <returns>A task that represents the animation operation.</returns>
        private async Task AnimateValueChange()
        {
            // both labels have the same width, take one of them
            double width = CurrentLabel.Width;

            // if animation is running, just update displayed values
            if (ValueChangeAnimationIsRunning())
            {
                NextLabel.Text = FormatValue(Value);
                CurrentLabel.Text = FormatValue(_valueSnapshot);
                return;
            }

            // make sure that labels are at proper start position
            NextLabel.TranslationX = Value > _valueSnapshot ? width : -width;
            NextLabel.Text = FormatValue(Value);
            CurrentLabel.TranslationX = 0;
            CurrentLabel.Text = FormatValue(_valueSnapshot);

            // start animation
            await Task.WhenAll(new List<Task>
            {
                CurrentLabel.TranslateTo(Value > _valueSnapshot ? -width : width, 0, ANIMATION_TIME),
                NextLabel.TranslateTo(0, 0, ANIMATION_TIME)
            });
        }

        /// <summary>
        /// Handles bound property change.
        /// If "Value" property was changed, it updates the control's appearance.
        /// Value change animation is performed if value does not exceed the maximum supported value.
        /// </summary>
        /// <param name="propertyName">Name of the property which was changed.</param>
        protected override async void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Value))
            {
                if (Value <= MAX_VALUE || _valueSnapshot <= MAX_VALUE)
                {
                    await AnimateValueChange();
                }

                _valueSnapshot = Value;
                SyncLabelsWithValue();
            }
        }

        #endregion
    }
}