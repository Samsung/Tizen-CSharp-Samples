/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clock.Controls
{
    public class SwipeImage : Image
    {
        public static readonly BindableProperty OriginalSourceProperty = BindableProperty.Create("OriginalSource", typeof(ImageSource), typeof(SwipeImage), null);
        public static readonly BindableProperty PressSourceProperty = BindableProperty.Create("PressSource", typeof(ImageSource), typeof(SwipeImage), null);
        public static readonly BindableProperty DragSourceProperty = BindableProperty.Create("DragSource", typeof(ImageSource), typeof(SwipeImage), null);

        public ImageSource OriginalSource
        {
            get { return (ImageSource)GetValue(OriginalSourceProperty); }
            set { SetValue(OriginalSourceProperty, value); Source = value; OnPropertyChanged(); }
        }

        public Image DragImage { get; set; }

        ///<summary>
        ///Initializes a new instance of the <see cref="SwipeImage"/> class
        ///</summary>
        public SwipeImage()
        {
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        public static readonly BindableProperty TapStartCommandProperty =
            BindableProperty.Create(nameof(TapStartCommand), typeof(ICommand), typeof(SwipeImage), null, BindingMode.TwoWay);

        public static readonly BindableProperty TapEndCommandProperty =
            BindableProperty.Create(nameof(TapEndCommand), typeof(ICommand), typeof(SwipeImage), null, BindingMode.TwoWay);

        /// <summary>
        /// Command which can be called when SwipeButton is touched.
        /// </summary>
        public ICommand TapStartCommand
        {
            get { return (ICommand)GetValue(TapStartCommandProperty); }
            set { SetValue(TapStartCommandProperty, value); }
        }

        public ICommand TapEndCommand
        {
            get { return (ICommand)GetValue(TapEndCommandProperty); }
            set { SetValue(TapEndCommandProperty, value); }
        }
    }
}
