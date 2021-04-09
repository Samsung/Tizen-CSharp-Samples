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

namespace SystemInfo.Control
{
    /// <summary>
    /// Interaction logic for ImageIndicatorControl.xaml
    /// </summary>
    public partial class ImageIndicatorControl
    {
        #region properties

        /// <summary>
        /// Bindable property that allows to set image source of footer's icon.
        /// </summary>
        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(ImageIndicatorControl), string.Empty);

        /// <summary>
        /// Bindable property that allows to set title of the footer.
        /// </summary>
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(ImageIndicatorControl), string.Empty);

        /// <summary>
        /// Bindable property that allows to set value that footer indicates.
        /// </summary>
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(string), typeof(ImageIndicatorControl), string.Empty);

        /// <summary>
        /// Gets or sets image source of footer's icon.
        /// </summary>
        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets title of the footer.
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets value that footer indicates.
        /// </summary>
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        #endregion

        #region methods

        /// <summary>
        /// Creates new instance of FooterControl class.
        /// </summary>
        public ImageIndicatorControl()
        {
            InitializeComponent();
        }

        #endregion
    }
}