﻿/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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

using Tizen.NUI;
using Tizen.NUI.Binding;
using Tizen.NUI.BaseComponents;

namespace Piano.Controls
{
    /// <summary>
    /// Interaction logic for Key.xaml.
    /// </summary>
    public partial class Key : ImageView
    {
        #region fields

        /// <summary>
        /// Local storage of <see cref="SoundNumber"/>
        /// </summary>
        private int _soundNumber;
        private Timer mViewTimer;

        #endregion

        #region properties

        /// <summary>
        /// Bindable property that allows to set image source for pressed state.
        /// </summary>
        public static BindableProperty PressedImageSourceProperty =
            BindableProperty.Create(nameof(PressedImageSource), typeof(string), typeof(Key));

        /// <summary>
        /// Bindable property that allows to set image source for unpressed state.
        /// </summary>
        public static BindableProperty UnPressedImageSourceProperty =
            BindableProperty.Create(nameof(UnPressedImageSource), typeof(string), typeof(Key),
                propertyChanged: UnPressedImageSourcePropertyChanged);

        public Vector4 LayoutBounds
        {
            set
            {
                PositionX = value.X * Window.Instance.Size.Height;
                PositionY = value.Y * Window.Instance.Size.Width;
                SizeWidth = value.Z * Window.Instance.Size.Height;
                SizeHeight = value.W * Window.Instance.Size.Width;
            }
        }
        /// <summary>
        /// Gets or sets sound index that will be played.
        /// </summary>
        public int SoundNumber
        {
            get => _soundNumber;
            set
            {
                _soundNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets image source for pressed state.
        /// </summary>
        public string PressedImageSource
        {
            get => GetValue(PressedImageSourceProperty).ToString();
            set => SetValue(PressedImageSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets image source for unpressed state.
        /// </summary>
        public string UnPressedImageSource
        {
            get => GetValue(UnPressedImageSourceProperty).ToString();
            set => SetValue(UnPressedImageSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets command that is executed on click.
        /// </summary>
        public Command OnClickedCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public Key()
        {
            InitializeComponent();
            Root.TouchEvent += OnTouchEvent;
        }

        private bool OnTouchEvent(object source, View.TouchEventArgs e)
        {
            ExecuteClickCommand();
            return false;
        }

        /// <summary>
        /// Invoked on image source change.
        /// Updates image source.
        /// </summary>
        /// <param name="bindable">The bindable object that contains the property.</param>
        /// <param name="oldValue">Old value of the property.</param>
        /// <param name="newValue">New value of the property.</param>
        private static void UnPressedImageSourcePropertyChanged(BindableObject bindable, object oldValue,
            object newValue)
        {
            if (bindable is Key key)
            {
                key.UpdateImageSource(newValue.ToString());
            }
        }

        /// <summary>
        /// Executed on image click.
        /// </summary>
        private void ExecuteClickCommand()
        {
            UpdateImageSource(PressedImageSource);
            //OnClickedCommand?.Execute(null);

            mViewTimer = new Timer(300);
            mViewTimer.Tick += OnTimerTick;
            mViewTimer.Start();
        }

        private bool OnTimerTick(object sender, Timer.TickEventArgs args)
        {
            UpdateImageSource(UnPressedImageSource);

            mViewTimer.Stop();
            mViewTimer.Dispose();
            
            return false;
        }

        /// <summary>
        /// Updates image source.
        /// </summary>
        /// <param name="newSource">New image source.</param>
        private void UpdateImageSource(string newSource)
        {
            ResourceUrl = newSource;
        }

        #endregion
    }
}