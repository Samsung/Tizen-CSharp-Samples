/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

﻿using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediaContent.Controls
{
    /// <summary>
    /// TableElementCentered class.
    /// Provides logic for TableElementCentered.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TableElementCentered : AbsoluteLayout
    {
        #region properties

        /// <summary>
        /// Text bindable property.
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(TableElementCentered), default(string));

        /// <summary>
        /// Text property.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        #region methods

        /// <summary>
        /// Updates table element with text from 'Text' property.
        /// </summary>
        private void TextPropertyChanged()
        {
            PropertyLabel.Text = Text;
        }

        /// <summary>
        /// Notifies the application about update of the property with name given as a parameter.
        /// </summary>
        /// <param name="propertyName">Name of the changed property.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                TextPropertyChanged();
            }
        }

        /// <summary>
        /// TableElementCentered class constructor.
        /// </summary>
        public TableElementCentered()
        {
            InitializeComponent();
        }

        #endregion
    }
}