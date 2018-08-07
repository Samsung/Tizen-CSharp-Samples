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
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UIComponents.Extensions
{
    /// <summary>
    /// Types of Background Option
    /// </summary>
    public enum BackgroundOptions
    {
        /// <summary>
        /// Center the background image.
        /// </summary>
        Center,

        /// <summary>
        /// Scale the background image, retaining the aspect ratio.
        /// </summary>
        Scale,

        /// <summary>
        /// Stretch the background image to fill the widget's area.
        /// </summary>
        Stretch,

        /// <summary>
        /// Tile the background image at its original size.
        /// </summary>
        Tile
    }

    /// <summary>
    /// Background view
    /// </summary>
    public class Background : View
    {
        /// <summary>
        /// Image source property
        /// </summary>
        public static readonly BindableProperty ImageProperty = BindableProperty.Create("Image", typeof(FileImageSource), typeof(Background), default(FileImageSource));

        /// <summary>
        /// BackgroundOptions type property
        /// </summary>
        public static readonly BindableProperty OptionProperty = BindableProperty.Create("Option", typeof(BackgroundOptions), typeof(Background), BackgroundOptions.Scale);

        /// <summary>
        /// Image source
        /// </summary>
        public FileImageSource Image
        {
            get { return (FileImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        /// <summary>
        /// BackgroundOptions type
        /// </summary>
        public BackgroundOptions Option
        {
            get { return (BackgroundOptions)GetValue(OptionProperty); }
            set { SetValue(OptionProperty, value); }
        }
    }
}
