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

using System;
using Xamarin.Forms;

namespace Preference.Tizen.TV.Controls
{
    /// <summary>
    /// The MultilineCell contains Icons, Text, Multiline, and Checkbox(IsCheckVisible).
    /// </summary>
    /// <remarks>
    /// The MultilineCell class is inherited from a cell.<br>
    /// Each property is displayed in the specified position.<br>
    /// The specified position is shown below.<br>
    /// <br>
    /// <table border=2 style="text-align:center;border-collapse:collapse;">
    ///        <tr>
    ///            <th height = 100 width=200 rowspan="2">Icon</th>
    ///            <th width = 200> Text </th>
    ///            <th width=200 rowspan="2">Checkbox</th>
    ///        </tr>
    ///        <tr>
    ///            <th>Multiline</th>
    ///        </tr>
    /// </table>
    /// </remarks>
    /// <example>
    /// <code>
    /// new DataTemplate(() =>
    /// {
    ///        return new MultilineCell
    ///        {
    ///            Text = "Test Text",
    ///            Multiline = "Test Multiline",
    ///            Icon = ImageSource.FromFile("icon.png"),
    ///            IsCheckVisible = true,
    ///            IconWidth = 80,
    ///            IconHeight = 100,
    ///        };
    ///    });
    /// </code>
    /// </example>
    public class MultilineCell : Cell
    {
        #region properties

        /// <summary>
        /// Bindable property with for main text.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(MultilineCell), default(string));

        /// <summary>
        /// Bindable property for secondary text.
        /// </summary>
        public static readonly BindableProperty MultilineProperty = BindableProperty.Create("Multiline", typeof(string), typeof(TextCell), default(string));

        /// <summary>
        /// Bindable property for icon.
        /// </summary>
        public static readonly BindableProperty IconProperty = BindableProperty.Create("Icon", typeof(ImageSource), typeof(MultilineCell), null,
            propertyChanged: (bindable, oldvalue, newvalue) => ((MultilineCell)bindable).OnSourcePropertyChanged((ImageSource)oldvalue, (ImageSource)newvalue));

        /// <summary>
        /// Checkbox state.
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create("IsChecked", typeof(bool), typeof(MultilineCell), false,
            propertyChanged: (obj, oldValue, newValue) =>
            {
                var multilineCell = (MultilineCell)obj;

                multilineCell.Toggled?.Invoke(obj, new ToggledEventArgs((bool)newValue));
            }, defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// Checkbox visibility.
        /// </summary>
        public static readonly BindableProperty IsCheckVisibleProperty = BindableProperty.Create("IsCheckVisible", typeof(bool), typeof(MultilineCell), false);

        /// <summary>
        /// Icon width.
        /// </summary>
        public static readonly BindableProperty IconWidthProperty = BindableProperty.Create("IconWidth", typeof(int), typeof(MultilineCell), 0);

        /// <summary>
        /// Icon height property.
        /// </summary>
        public static readonly BindableProperty IconHeightProperty = BindableProperty.Create("IconHeight", typeof(int), typeof(MultilineCell), 0);

        /// <summary>
        /// The MultilineCell's constructor.
        /// </summary>
        public MultilineCell()
        {
            Disappearing += (sender, e) =>
            {
                Icon?.Cancel();
            };
        }

        /// <summary>
        /// Gets or sets the text from the central top of the item.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Gets or sets the Multiline from the central bottom of the item.
        /// </summary>
        public string Multiline
        {
            get => (string)GetValue(MultilineProperty);
            set => SetValue(MultilineProperty, value);
        }

        /// <summary>
        /// Gets or sets the Image on the left side of the item.
        /// </summary>
        [TypeConverter(typeof(ImageSourceConverter))]
        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// True or False is used to indicate whether the checkbox is displayed on the right side of the item.
        /// </summary>
        public bool IsCheckVisible
        {
            get => (bool)GetValue(IsCheckVisibleProperty);
            set => SetValue(IsCheckVisibleProperty, value);
        }

        /// <summary>
        /// True or False is used to indicate whether the checkbox has been toggled.
        /// </summary>
        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        /// <summary>
        /// Gets or sets the Icon's width.
        /// </summary>
        public int IconWidth
        {
            get => (int)GetValue(IconWidthProperty);
            set => SetValue(IconWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets the Icon's height.
        /// </summary>
        public int IconHeight
        {
            get => (int)GetValue(IconHeightProperty);
            set => SetValue(IconHeightProperty, value);
        }

        /// <summary>
        /// The event is raised when checkbox is toggled.
        /// </summary>
        public event EventHandler<ToggledEventArgs> Toggled;

        #endregion

        #region methods

        /// <summary>
        /// Handles image source change event.
        /// </summary>
        /// <param name="oldvalue">Old value.</param>
        /// <param name="newvalue">New value.</param>
        void OnSourcePropertyChanged(ImageSource oldvalue, ImageSource newvalue)
        {
            if (newvalue != null)
            {
                SetInheritedBindingContext(newvalue, BindingContext);
            }
        }

        #endregion
    }
}
