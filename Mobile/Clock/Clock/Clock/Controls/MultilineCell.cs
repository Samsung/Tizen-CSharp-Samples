/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

namespace Clock.Controls
{
    /// <summary>
    /// MultilineCell class
    /// </summary>
    public class MultilineCell : Cell
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(MultilineCell), default(string));
        public static readonly BindableProperty MultilineProperty = BindableProperty.Create("Multiline", typeof(string), typeof(TextCell), default(string));
        public static readonly BindableProperty IconProperty = BindableProperty.Create("Icon", typeof(ImageSource), typeof(MultilineCell), null,
            propertyChanged: (bindable, oldvalue, newvalue) => ((MultilineCell)bindable).OnSourcePropertyChanged((ImageSource)oldvalue, (ImageSource)newvalue));
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create("IsChecked", typeof(bool), typeof(MultilineCell), false,
            propertyChanged: (obj, oldValue, newValue) =>
            {
                var multilineCell = (MultilineCell)obj;
                multilineCell.Toggled?.Invoke(obj, new ToggledEventArgs((bool)newValue));
            }, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty IsCheckVisibleProperty = BindableProperty.Create("IsCheckVisible", typeof(bool), typeof(MultilineCell), false);
        public static readonly BindableProperty IconWidthProperty = BindableProperty.Create("IconWidth", typeof(int), typeof(MultilineCell), 0);
        public static readonly BindableProperty IconHeightProperty = BindableProperty.Create("IconHeight", typeof(int), typeof(MultilineCell), 0);


        /// <summary>
        /// Constructor
        /// </summary>
        public MultilineCell()
        {
            Disappearing += (sender, e) =>
            {
                Icon?.Cancel();
            };
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string Multiline
        {
            get { return (string)GetValue(MultilineProperty); }
            set { SetValue(MultilineProperty, value); }
        }

        [TypeConverter(typeof(ImageSourceConverter))]
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public bool IsCheckVisible
        {
            get { return (bool)GetValue(IsCheckVisibleProperty); }
            set { SetValue(IsCheckVisibleProperty, value); }
        }

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public int IconWidth
        {
            get { return (int)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public int IconHeight
        {
            get { return (int)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        public event EventHandler<ToggledEventArgs> Toggled;

        void OnSourcePropertyChanged(ImageSource oldvalue, ImageSource newvalue)
        {
            if (newvalue != null)
            {
                SetInheritedBindingContext(newvalue, BindingContext);
            }
        }
    }
}
