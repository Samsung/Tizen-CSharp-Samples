//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SpeechToText.Behaviors
{
    /// <summary>
    /// ImageActiveBehavior class.
    /// Adds behavior to image component which allows to set separate source for active state
    /// (tapped image).
    /// </summary>
    public class ImageActiveBehavior : Behavior<Image>
    {
        /// <summary>
        /// Structure which holds behavior's private data.
        /// </summary>
        struct TargetStorage
        {
            /// <summary>
            /// Reference to the tap recognizer which was added to the target component
            /// by the behavior.
            /// </summary>
            public TapGestureRecognizer TapRecognizer;

            /// <summary>
            /// Flag indicating if target component is currently active (tapped).
            /// </summary>
            public bool Active;
        }

        #region fields

        /// <summary>
        /// Default timeout to restore original image source (active state).
        /// </summary>
        public const int DEFAULT_TIMEOUT_MILLISECONDS = 300;

        /// <summary>
        /// Converter used to transform string source to ImageSource class instance.
        /// </summary>
        private readonly ImageSourceConverter _converter = new ImageSourceConverter();

        /// <summary>
        /// Structure used to hold private data for each target component (bindable object).
        /// </summary>
        private readonly Dictionary<BindableObject, TargetStorage> _storage =
            new Dictionary<BindableObject, TargetStorage>();

        #endregion

        #region properties

        /// <summary>
        /// ActiveSource property definition.
        /// It defines image source used for the active state of the component.
        /// </summary>
        public static readonly BindableProperty ActiveSourceProperty =
            BindableProperty.Create("ActiveSource", typeof(string), typeof(ImageActiveBehavior));

        /// <summary>
        /// OriginalSource property definition.
        /// It defines image source for the default state of the component.
        /// </summary>
        public static readonly BindableProperty OriginalSourceProperty =
            BindableProperty.Create("OriginalSource", typeof(string), typeof(ImageActiveBehavior));

        /// <summary>
        /// Timeout property definition.
        /// It defines time (in milliseconds) for the active state of the component
        /// (after that time, original image source will be restored).
        /// </summary>
        public static readonly BindableProperty TimeoutProperty =
            BindableProperty.Create("Timeout", typeof(int), typeof(ImageActiveBehavior),
                DEFAULT_TIMEOUT_MILLISECONDS);

        /// <summary>
        /// Image source for the active state of the component.
        /// </summary>
        public string ActiveSource
        {
            get { return (string)GetValue(ActiveSourceProperty); }
            set { SetValue(ActiveSourceProperty, value); }
        }

        /// <summary>
        /// Image source for the default state of the component.
        /// </summary>
        public string OriginalSource
        {
            get { return (string)GetValue(OriginalSourceProperty); }
            set { SetValue(OriginalSourceProperty, value); }
        }

        /// <summary>
        /// Active state timeout (in milliseconds).
        /// </summary>
        public int Timeout
        {
            get { return (int)GetValue(TimeoutProperty); }
            set { SetValue(TimeoutProperty, value); }
        }

        #endregion

        #region methods

        /// <summary>
        /// Converts image source (string) to ImageSource class instance.
        /// </summary>
        /// <param name="source">Image path as string.</param>
        /// <returns>ImageSoruce class instance created from specified string.</returns>
        private ImageSource SourceToImageSource(string source)
        {
            return source == null
                ? null :
                (ImageSource)_converter.ConvertFromInvariantString(source);
        }

        /// <summary>
        /// Called when the behavior is attached to the view.
        /// Adds tap recognizer (to the image) which change the source for a specified time
        /// (active state).
        /// </summary>
        /// <param name="bindable">The object to which the behavior is being binded.</param>
        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
            Image target = (Image)bindable;

            target.Source = SourceToImageSource(OriginalSource);

            TargetStorage targetStorage = new TargetStorage
            {
                TapRecognizer = new TapGestureRecognizer(),
            };

            targetStorage.TapRecognizer.Tapped += (sender, args) =>
            {
                if (targetStorage.Active)
                {
                    return;
                }

                target.Source = SourceToImageSource(ActiveSource);
                targetStorage.Active = true;

                Device.StartTimer(TimeSpan.FromMilliseconds(Timeout), () =>
                {
                    if (_storage.ContainsKey(bindable))
                    {
                        target.Source = SourceToImageSource(OriginalSource);
                    }

                    targetStorage.Active = false;
                    return false;
                });
            };

            target.GestureRecognizers.Add(targetStorage.TapRecognizer);
            _storage[bindable] = targetStorage;
        }

        /// <summary>
        /// Called when the behavior is removed from the attached control.
        /// Used to remove tap recognizer and perform cleanup.
        /// </summary>
        /// <param name="bindable">The object to which the behavior is binded.</param>
        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);
            if (!_storage.ContainsKey(bindable))
            {
                return;
            }

            TargetStorage targetStorage = _storage[bindable];
            Image target = (Image)bindable;
            target.GestureRecognizers.Remove(targetStorage.TapRecognizer);

            _storage.Remove(bindable);
        }

        /// <summary>
        /// Called when one of the behavior's properties was changed.
        /// Used to update original source value in all image components.
        /// </summary>
        /// <param name="propertyName">Changed property name.</param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == OriginalSourceProperty.PropertyName)
            {
                foreach (KeyValuePair<BindableObject, TargetStorage> entry in _storage)
                {
                    if (entry.Value.Active)
                    {
                        continue;
                    }

                    ((Image)entry.Key).Source = SourceToImageSource(OriginalSource);
                }
            }
        }

        #endregion
    }
}
