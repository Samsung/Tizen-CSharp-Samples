/*
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

using Piano.Models;
using Xamarin.Forms;

namespace Piano.ViewModels
{
    /// <summary>
    /// ViewModel class for single key.
    /// </summary>
    public class KeyViewModel : BindableObject
    {
        #region properties

        /// <summary>
        /// Bindable property that allows to set sound index that will be played.
        /// </summary>
        public static BindableProperty SoundNumberProperty =
            BindableProperty.Create(nameof(SoundNumber), typeof(int), typeof(KeyViewModel), 0);

        /// <summary>
        /// Bindable property that allows to set command that plays sound.
        /// </summary>
        public static BindableProperty PlaySoundCommandProperty =
            BindableProperty.Create(nameof(PlaySoundCommand), typeof(Command), typeof(KeyViewModel));

        /// <summary>
        /// Gets or sets sound index that will be played.
        /// </summary>
        public int SoundNumber
        {
            get => (int)GetValue(SoundNumberProperty);
            set => SetValue(SoundNumberProperty, value);
        }

        /// <summary>
        /// Gets or sets command that plays sound.
        /// </summary>
        public Command PlaySoundCommand
        {
            get => (Command)GetValue(PlaySoundCommandProperty);
            set => SetValue(PlaySoundCommandProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public KeyViewModel()
        {
            PlaySoundCommand = new Command(PlaySound);
        }

        /// <summary>
        /// Plays sound.
        /// </summary>
        private async void PlaySound()
        {
            await Sound.Play(SoundNumber);
        }

        #endregion
    }
}