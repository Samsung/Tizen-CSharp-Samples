/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Xamarin.Forms;

namespace ApplicationControl.Extensions
{
    /// <summary>
    /// The RadioButton is a widget that allows one or more options to be displayed and have the user choose only one of them.
    /// </summary>
    /// <example>
    /// <code>
    /// var radioButton = new RadioButton
    /// {
    ///     Text = "Radio 1",
    ///     GroupName = "Group 1",
    ///     IsSelected = false,
    /// }
    /// radioButton.Selected += (s,e) => { ... };
    /// </code>
    /// </example>
    public class RadioButton : View
    {
        /// <summary>
        /// BindableProperty. Identifies the Text bindable property.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(RadioButton), default(string));

        /// <summary>
        /// BindableProperty. Identifies the TextColor bindable property.
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color), typeof(RadioButton), Color.Default);

        /// <summary>
        /// BindableProperty. Identifies the FontSize bindable property.
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create("FontSize", typeof(double), typeof(RadioButton), -1.0,
            defaultValueCreator: bindable => Device.GetNamedSize(NamedSize.Default, (RadioButton)bindable));

        /// <summary>
        /// BindableProperty. Identifies the IsSelected bindable property.
        /// </summary>
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create("IsSelected", typeof(bool), typeof(RadioButton), false,
             propertyChanged: IsSelectedPropertyChanged);

        /// <summary>
        /// BindableProperty. Identifies the GroupName bindable property.
        /// </summary>
        public static readonly BindableProperty GroupNameProperty = BindableProperty.Create("GroupName", typeof(string), typeof(RadioButton), default(string));

        /// <summary>
        /// Gets or sets the text displayed as the content of the RadioButton.
        /// This is a bindable property.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color for the text of the RadioButton.
        /// </summary>
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the size of the font of the RadioButton text.
        /// </summary>
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the name that specifies which RadioButton controls are mutually exclusive.
        /// It can be set to null.
        /// </summary>
        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether this RadioButton is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// Occurs when the RadioButton selection was changed.
        /// </summary>
        public event EventHandler<SelectedEventArgs> Selected;

        static void IsSelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var radioButton = (RadioButton)bindable;
            radioButton.Selected?.Invoke(radioButton, new SelectedEventArgs((bool)newValue));
        }
    }
}