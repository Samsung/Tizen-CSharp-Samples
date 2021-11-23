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

using Xamarin.Forms;

namespace NotificationManager.Controls.Slider
{
    /// <summary>
    /// MsSliderWithDescription class.
    /// Provides logic for MsSliderWithDescription.
    /// This class defines a slider item which contains a label at the top-left corner (title),
    /// a label at the top-right corner (describing slider value in milliseconds)
    /// and a slider at the bottom.
    /// </summary>
    public partial class MsSliderWithDescription
    {
        #region properties

        /// <summary>
        /// Slider value bindable property.
        /// </summary>
        public static readonly BindableProperty SliderValueProperty =
            BindableProperty.Create(nameof(SliderValue), typeof(double), typeof(MsSliderWithDescription),
                default(double),
                BindingMode.TwoWay, propertyChanged: OnSliderValuePropertyChanged);

        /// <summary>
        /// The value of the MsSliderWithDescription's slider.
        /// </summary>
        public double SliderValue
        {
            get => (double)GetValue(SliderValueProperty);
            set => SetValue(SliderValueProperty, value);
        }

        /// <summary>
        /// Title label text property.
        /// </summary>
        public string TitleLabelText
        {
            get => TitleLabel.Text;
            set => TitleLabel.Text = value;
        }

        /// <summary>
        /// Title label margin property.
        /// </summary>
        public Thickness TitleLabelMargin
        {
            get => TitleLabel.Margin;
            set => TitleLabel.Margin = value;
        }

        /// <summary>
        /// Status label margin property.
        /// </summary>
        public Thickness StatusLabelMargin
        {
            get => StatusLabel.Margin;
            set => StatusLabel.Margin = value;
        }

        /// <summary>
        /// Value slider margin property.
        /// </summary>
        public Thickness ValueSliderMargin
        {
            get => ValueSlider.Margin;
            set => ValueSlider.Margin = value;
        }

        #endregion

        #region methods

        /// <summary>
        /// MsSliderWithDescription class constructor.
        /// Initializes component and registers an event handler to respond
        /// to slider's 'ValueChanged' event.
        /// </summary>
        public MsSliderWithDescription()
        {
            InitializeComponent();

            ValueSlider.ValueChanged += (s, e) => { SliderValue = e.NewValue; };
        }

        /// <summary>
        /// Sets MsSliderWithDescription's slider value.
        /// </summary>
        /// <param name="sliderValue">Slider value which is to be set.</param>
        private void SetSliderValue(double sliderValue)
        {
            ValueSlider.Value = sliderValue;
        }

        /// <summary>
        /// Handles 'SliderValueProperty' property change event.
        /// </summary>
        /// <param name="bindable">Object that sent event.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnSliderValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MsSliderWithDescription button)
            {
                button.SetSliderValue((double)newValue);
            }
        }

        #endregion
    }
}