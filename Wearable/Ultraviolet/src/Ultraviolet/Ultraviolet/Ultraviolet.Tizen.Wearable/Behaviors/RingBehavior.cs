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
using Ultraviolet.Enums;
using Xamarin.Forms;

namespace Ultraviolet.Tizen.Wearable.Behaviors
{
    /// <summary>
    /// Class that provides functionality for uv level ring image.
    /// </summary>
    public class RingBehavior : Behavior<Image>
    {
        /// <summary>
        /// Bindable property definition for uv level.
        /// </summary>
        public static readonly BindableProperty LevelProperty = BindableProperty.Create(
            nameof(Level),
            typeof(UvLevel),
            typeof(RingBehavior),
            UvLevel.None);

        /// <summary>
        /// Enum representing uv level.
        /// </summary>
        public UvLevel Level
        {
            get => (UvLevel)GetValue(LevelProperty);
            set => SetValue(LevelProperty, value);
        }

        /// <summary>
        /// Bindable property definition for image source.
        /// </summary>
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource),
            typeof(ImageSource),
            typeof(RingBehavior),
            null);

        /// <summary>
        /// Source of the image indicating uv level.
        /// </summary>
        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        /// <summary>
        /// Overridden OnPropertyChanged method which refreshes properties values.
        /// </summary>
        /// <param name="propertyName">Name of property which changes.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == LevelProperty.PropertyName && Level != UvLevel.None)
            {
                ImageSource = $"images/rings/{Level.GetFilename()}";
            }

            base.OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Overriden OnAttachedTo method which sets BindingContext of behavior.
        /// </summary>
        /// <param name="bindable">Bindable object.</param>
        protected override void OnAttachedTo(Image bindable)
        {
            BindingContext = bindable;
            base.OnAttachedTo(bindable);
        }
    }
}
