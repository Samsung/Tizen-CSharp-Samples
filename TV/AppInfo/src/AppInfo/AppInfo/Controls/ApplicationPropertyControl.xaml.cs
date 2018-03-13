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

namespace AppInfo.Controls
{
    /// <summary>
    /// ApplicationPropertyControl class.
    /// Provides logic for the ApplicationPropertyControl.
    /// </summary>
    public partial class ApplicationPropertyControl : ContentView
    {
        #region fields

        /// <summary>
        /// Backing field of the Label property.
        /// </summary>
        private string _label;

        /// <summary>
        /// Backing field of the HasBackground property.
        /// </summary>
        private bool _hasBackground;

        #endregion

        #region properties

        /// <summary>
        /// Label property.
        /// </summary>
        public string Label
        {
            set
            {
                _label = value;
                LabelStr.Text = _label;
            }

            get { return _label; }
        }

        /// <summary>
        /// HasBackground property.
        /// </summary>
        public bool HasBackground
        {
            set
            {
                _hasBackground = value;

                if (_hasBackground)
                {
                    PropertyBackground.Opacity = .1;
                }
            }

            get { return _hasBackground; }
        }

        /// <summary>
        /// Value bindable property.
        /// </summary>
        public static BindableProperty ValueProperty = BindableProperty.Create("Value", typeof(string),
            typeof(ApplicationPropertyControl), "", propertyChanged: OnValueChanged);

        /// <summary>
        /// Value property.
        /// </summary>
        public string Value
        {
            set
            {
                SetValue(ValueProperty, value);
            }

            get { return (string)GetValue(ValueProperty); }
        }

        #endregion

        #region methods

        /// <summary>
        /// ApplicationPropertyControl class constructor.
        /// </summary>
        public ApplicationPropertyControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Indicates when the Value bindable property has changed.
        /// Executes OnValueChanged method to update text displayed by the LabelValue label element.
        /// </summary>
        /// <param name="bindable">An instance of the ThumbnailsGridViewControl class containing changed property.</param>
        /// <param name="oldValue">Old value of the changed property.</param>
        /// <param name="newValue">New value of the changed property.</param>
        private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ApplicationPropertyControl)bindable).OnValueChanged((string)newValue);
        }

        /// <summary>
        /// Updated text property of the LabelValue label element.
        /// </summary>
        /// <param name="value">Label value.</param>
        private void OnValueChanged(string value)
        {
            LabelValue.Text = value;
        }

        #endregion
    }
}