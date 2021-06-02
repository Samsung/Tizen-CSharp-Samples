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
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms; // we added this namespace as a reference

namespace VolumeController
{
    public class MainPage : ContentPage
    {
        private Dictionary<AudioVolumeTypeShare, Label> controllerLabelDictionary;
        private Dictionary<AudioVolumeTypeShare, Slider> controllerSliderDictionary;
        private double[] LableNum = new double[8];

        private bool _contentLoaded;
        void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }

            _contentLoaded = true;

            // UI Logic Development
            this.Content = CreateAllControllers();
        }

        /// <summary>
        /// created Initialization and user logic
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create main Layout
        /// </summary>
        /// <returns>main Layout</returns>
        private ScrollView CreateAllControllers()
        {
            controllerLabelDictionary = new Dictionary<AudioVolumeTypeShare, Label>();
            controllerSliderDictionary = new Dictionary<AudioVolumeTypeShare, Slider>();

            StackLayout controllers = new StackLayout();
            controllers.BackgroundColor = Color.White;
            controllers.HorizontalOptions = LayoutOptions.FillAndExpand;

            foreach (AudioVolumeTypeShare type in Enum.GetValues(typeof(AudioVolumeTypeShare)))
            {
                if (type != AudioVolumeTypeShare.None && type != AudioVolumeTypeShare.Voip)
                {
                    //create child layout by AudioVolumeTypeShare's type
                    controllers.Children.Add(CreateController(type));
                }
            }

            ScrollView scrollView = new ScrollView
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = ScrollOrientation.Vertical,
                Content = controllers,
            };

            return scrollView;
        }

        /// <summary>
        /// create child layout by AudioVolumeTypeShare's type
        /// </summary>
        /// <param name="type">AudioVolumeTypeShare's type</param>
        /// <returns>child layout</returns>
        private StackLayout CreateController(AudioVolumeTypeShare type)
        {
            Label soundType = new Label();
            soundType.Text = type.ToString();
            soundType.FontSize = 40;
            soundType.TextColor = new Color(0, 0, 0);
            soundType.HorizontalOptions = LayoutOptions.StartAndExpand;

            Label soundLevel = new Label();

            LableNum[(int)type] = DependencyService.Get<IAudioManager>().LevelType(type);
            soundLevel.Text = LableNum[(int)type].ToString();
            soundLevel.FontSize = 40;
            soundLevel.HorizontalOptions = LayoutOptions.EndAndExpand;
            soundLevel.TextColor = new Color(0, 0, 0);
            soundLevel.LineBreakMode = LineBreakMode.NoWrap;
            controllerLabelDictionary.Add(type, soundLevel);

            Layout typeAndLevel = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(30, 0, 30, 0),
                Children =
                {
                    soundType,
                    soundLevel
                }
            };

            Slider slider = new Slider();
            slider.Margin = new Thickness(30, 0, 30, 0);
            slider.HorizontalOptions = LayoutOptions.FillAndExpand;
            slider.Minimum = 0;
            slider.Maximum = DependencyService.Get<IAudioManager>().MaxLevel(type);
            slider.Value = LableNum[(int)type];
            switch (type)
            {
                case AudioVolumeTypeShare.System:
                    slider.ValueChanged += SystemSliderDragged;
                    break;
                case AudioVolumeTypeShare.Notification:
                    slider.ValueChanged += NotificationSliderDragged;
                    break;
                case AudioVolumeTypeShare.Alarm:
                    slider.ValueChanged += AlarmSliderDragged;
                    break;
                case AudioVolumeTypeShare.Ringtone:
                    slider.ValueChanged += RingtoneSliderDragged;
                    break;
                case AudioVolumeTypeShare.Media:
                    slider.ValueChanged += MediaSliderDragged;
                    break;
                case AudioVolumeTypeShare.Voice:
                    slider.ValueChanged += VoiceSliderDragged;
                    break;
                default:
                    break;
            }

            controllerSliderDictionary.Add(type, slider);

            StackLayout controller = new StackLayout();
            controller.HorizontalOptions = LayoutOptions.FillAndExpand;
            controller.VerticalOptions = LayoutOptions.StartAndExpand;
            controller.BackgroundColor = Color.White;
            controller.Children.Add(typeAndLevel);
            controller.Children.Add(slider);

            return controller;
        }

        /// <summary>
        /// Invoked when SystemSlider Dragged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">ValueChangedEventArgs</param>
        private void SystemSliderDragged(object sender, ValueChangedEventArgs e)
        {
            UpdateVolumeAndLabelFromDrag(AudioVolumeTypeShare.System, sender, e);
        }

        /// <summary>
        /// Invoked when NotificationSlider Dragged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">ValueChangedEventArgs</param>
        private void NotificationSliderDragged(object sender, ValueChangedEventArgs e)
        {
            UpdateVolumeAndLabelFromDrag(AudioVolumeTypeShare.Notification, sender, e);
        }

        /// <summary>
        /// Invoked when AlarmSlider Dragged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">ValueChangedEventArgs</param>
        private void AlarmSliderDragged(object sender, ValueChangedEventArgs e)
        {
            UpdateVolumeAndLabelFromDrag(AudioVolumeTypeShare.Alarm, sender, e);
        }

        /// <summary>
        /// Invoked when RingtoneSlider Dragged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">ValueChangedEventArgs</param>
        private void RingtoneSliderDragged(object sender, ValueChangedEventArgs e)
        {
            UpdateVolumeAndLabelFromDrag(AudioVolumeTypeShare.Ringtone, sender, e);
        }

        /// <summary>
        /// Invoked when MediaSlider Dragged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">ValueChangedEventArgs</param>
        private void MediaSliderDragged(object sender, ValueChangedEventArgs e)
        {
            UpdateVolumeAndLabelFromDrag(AudioVolumeTypeShare.Media, sender, e);
        }

        /// <summary>
        /// Invoked when VoiceSlider Dragged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">ValueChangedEventArgs</param>
        private void VoiceSliderDragged(object sender, ValueChangedEventArgs e)
        {
            UpdateVolumeAndLabelFromDrag(AudioVolumeTypeShare.Voice, sender, e);
        }

        /// <summary>
        /// Update Volume value And Label text
        /// </summary>
        /// <param name="type">AudioVolumeTypeShare's type</param>
        /// <param name="sliderObject">sliderObject</param>
        /// <param name="e">ValueChangedEventArgs</param>
        private async void UpdateVolumeAndLabelFromDrag(AudioVolumeTypeShare type, object sliderObject, ValueChangedEventArgs e)
        {
            Slider slider = (Slider)sliderObject;
            if (Math.Abs(slider.Value - LableNum[(int)type]) >= 1)
            {
                int tempValue = (int)Math.Floor(slider.Value);
                LableNum[(int)type] = tempValue;
                Label sliderLabel = controllerLabelDictionary[type];
                sliderLabel.Text = tempValue.ToString();
                await SetAudioType(type, tempValue);
            }
        }

        /// <summary>
        /// Update Volume value
        /// </summary>
        /// <param name="type">AudioVolumeTypeShare's type</param>
        /// <param name="value">Volume value</param>
        /// <returns>async Task</returns>
        private Task SetAudioType(AudioVolumeTypeShare type, int value)
        {
            return Task.Run(() =>
            {
                DependencyService.Get<IAudioManager>().ApplyAudioType(type, value);
            });
        }
    }
}