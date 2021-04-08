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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace UIComponents.Extensions
{
    /// <summary>
    /// View for Thumbnail item
    /// </summary>
    public class ThumbnailItem : View
    {
        /// <summary>
        /// Image source property
        /// </summary>
        public static readonly BindableProperty ImageProperty = BindableProperty.Create("Image", typeof(FileImageSource), typeof(ThumbnailItem), default(FileImageSource));

        /// <summary>
        /// Image source
        /// </summary>
        public FileImageSource Image
        {
            get { return (FileImageSource)GetValue(ImageProperty); }
            set
            {
                SetValue(ImageProperty, value);
            }
        }
    }
}
