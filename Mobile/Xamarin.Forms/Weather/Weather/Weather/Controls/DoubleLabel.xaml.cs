/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
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

namespace Weather.Controls
{
    /// <summary>
    /// Interaction logic for DoubleLabel.xaml.
    /// </summary>
    public partial class DoubleLabel
    {
        #region properties

        /// <summary>
        /// Bindable property that allows to set main text of control.
        /// </summary>
        public static readonly BindableProperty MainTextProperty =
            BindableProperty.Create(nameof(MainText), typeof(string), typeof(DoubleLabel), default(string));

        /// <summary>
        /// Bindable property that allows to set sub text of control.
        /// </summary>
        public static readonly BindableProperty SubTextProperty =
            BindableProperty.Create(nameof(SubText), typeof(string), typeof(DoubleLabel), default(string));

        /// <summary>
        /// Gets or sets main text of control.
        /// </summary>
        public string MainText
        {
            get => (string)GetValue(MainTextProperty);
            set => SetValue(MainTextProperty, value);
        }

        /// <summary>
        /// Gets or sets sub text of control.
        /// </summary>
        public string SubText
        {
            get => (string)GetValue(SubTextProperty);
            set => SetValue(SubTextProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DoubleLabel()
        {
            InitializeComponent();
        }

        #endregion
    }
}