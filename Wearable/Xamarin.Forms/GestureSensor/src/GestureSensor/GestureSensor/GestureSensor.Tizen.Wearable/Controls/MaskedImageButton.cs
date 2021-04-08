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

﻿using System.Windows.Input;
using Xamarin.Forms;

namespace GestureSensor.Tizen.Wearable.Controls
{
    /// <summary>
    /// Masked image with press, release and tap handling.
    /// </summary>
    public class MaskedImageButton : MaskedImage
    {
        /// <summary>
        /// Backing store for the Command bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaskedImageButton), null);

        /// <summary>
        /// Backing store for the PressCommand bindable property.
        /// </summary>
        public static readonly BindableProperty PressCommandProperty =
            BindableProperty.Create(nameof(PressCommand), typeof(ICommand), typeof(MaskedImageButton), null);

        /// <summary>
        /// Backing store for the Command bindable property.
        /// </summary>
        public static readonly BindableProperty ReleaseCommandProperty =
            BindableProperty.Create(nameof(ReleaseCommand), typeof(ICommand), typeof(MaskedImageButton), null);

        /// <summary>
        /// Gets or sets command executed on click.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Gets or sets command executed on press.
        /// </summary>
        public ICommand PressCommand
        {
            get => (ICommand)GetValue(PressCommandProperty);
            set => SetValue(PressCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets command executed on release.
        /// </summary>
        public ICommand ReleaseCommand
        {
            get => (ICommand)GetValue(ReleaseCommandProperty);
            set => SetValue(ReleaseCommandProperty, value);
        }
    }
}
