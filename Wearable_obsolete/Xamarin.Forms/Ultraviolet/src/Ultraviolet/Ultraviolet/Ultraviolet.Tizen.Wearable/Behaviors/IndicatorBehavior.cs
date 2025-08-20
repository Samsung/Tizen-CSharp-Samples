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
    /// Class that provides functionality for uv level indicator image.
    /// </summary>
    public class IndicatorBehavior : Behavior<Image>
    {
        /// <summary>
        /// Field with animation length.
        /// </summary>
        private const uint AnimationLength = 800;

        /// <summary>
        /// Field with animation starting point value.
        /// </summary>
        private const int AnimationStartPoint = 0;

        /// <summary>
        /// Field with animation ending point value.
        /// </summary>
        private const int AnimationEndPoint = 10;

        /// <summary>
        /// Field with beginning point on animation timeline.
        /// </summary>
        private const double AnimationBeginning = 0;

        /// <summary>
        /// Field with midpoint on animation timeline.
        /// </summary>
        private const double AnimationMidpoint = 0.5;

        /// <summary>
        /// Field with finish point on animation timeline.
        /// </summary>
        private const double AnimationFinish = 1;

        /// <summary>
        /// Field with change animation length.
        /// </summary>
        private const uint AnimationChangeLength = 1000;

        /// <summary>
        /// Field with change animation starting point value.
        /// </summary>
        private const int AnimationShowPoint = 0;

        /// <summary>
        /// Field with change animation ending point value.
        /// </summary>
        private const int AnimationHidePoint = 300;

        /// <summary>
        /// Field with image to animate.
        /// </summary>
        private Image _image;

        /// <summary>
        /// Bindable property definition for uv level.
        /// </summary>
        public static readonly BindableProperty LevelProperty = BindableProperty.Create(
            nameof(Level),
            typeof(UvLevel),
            typeof(IndicatorBehavior),
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
            typeof(IndicatorBehavior),
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
                AnimateChange();
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
            _image = bindable;
            AnimateChange();
            base.OnAttachedTo(bindable);
        }

        /// <summary>
        /// Animates indicator, moving it up and down.
        /// </summary>
        private void AnimateUpDown()
        {
            if (_image != null)
            {
                var animationUp = new Animation(c => _image.TranslationY = c,
                    AnimationStartPoint, AnimationEndPoint, Easing.SinInOut);

                var animationDown = new Animation(c => _image.TranslationY = c,
                    AnimationEndPoint, AnimationStartPoint, Easing.SinInOut);

                var animation = new Animation();
                animation.Add(AnimationBeginning, AnimationMidpoint, animationUp);
                animation.Add(AnimationMidpoint, AnimationFinish, animationDown);
                animation.Commit(_image, nameof(AnimateUpDown), repeat: () => true, length: AnimationLength);
            }
        }

        /// <summary>
        /// Animates change of indicator.
        /// </summary>
        private void AnimateChange()
        {
            _image?.AbortAnimation(nameof(AnimateUpDown));
            if (_image != null)
            {
                var animationHide = new Animation(c => _image.TranslationY = c,
                    AnimationShowPoint, AnimationHidePoint, Easing.SinInOut,
                    finished: () => ImageSource = $"images/indicators/{Level.GetFilename()}");

                var animationShow = new Animation(c => _image.TranslationY = c,
                    AnimationHidePoint, AnimationShowPoint, Easing.SinInOut,
                    finished: () => AnimateUpDown());

                var animationChange = new Animation();
                animationChange.Add(AnimationBeginning, AnimationMidpoint, animationHide);
                animationChange.Add(AnimationMidpoint, AnimationFinish, animationShow);
                animationChange.Commit(_image, nameof(AnimateChange), repeat: () => false,
                    length: AnimationChangeLength);
            }
        }
    }
}