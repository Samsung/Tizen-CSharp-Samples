
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
using System.Windows.Input;
using Tizen.Multimedia;
using Xamarin.Forms;

namespace VolumeController.ViewModels
{
    /// <summary>
    /// VolumeControlViewModel class.
    /// </summary>
    public class VolumeControlViewModel : ViewModelBase
    {

        #region fields

        /// <summary>
        /// List containing all possible AudioVolumeType values.
        /// </summary>
        private readonly List<AudioVolumeType> volumeTypes;

        /// <summary>
        /// Index of currently selected AudioVolumeType.
        /// </summary>
        private int volumeTypeIndex;

        /// <summary>
        /// Value representing maximum volume for current volume type.
        /// </summary>
        private int maxVolume;

        /// <summary>
        /// Value representing current volume.
        /// </summary>
        private int currentVolume;

        /// <summary>
        /// SliderUsable back-field.
        /// </summary>
        private bool sliderUsable;

        #endregion fields

        #region properties

        /// <summary>
        /// Property provides index of currently selected volume type.
        /// </summary>
        private int VolumeTypeIndex
        {
            set
            {
                volumeTypeIndex = Math.Clamp(value, 0, volumeTypes.Count);
                OnPropertyChanged("CanSelectNext");
                OnPropertyChanged("CanSelectPrevious");

                sliderUsable = false;

                OnPropertyChanged("VolumeTypeName");
                MaxVolume = AudioManager.VolumeController.MaxLevel[CurrentVolumeType];
                CurrentVolume = AudioManager.VolumeController.Level[CurrentVolumeType];
            }
            get { return volumeTypeIndex; }
        }

        /// <summary>
        /// Property provides currently selected AudioVolumeType.
        /// </summary>
        private AudioVolumeType CurrentVolumeType
        {
            get { return volumeTypes[VolumeTypeIndex]; }
        }

        /// <summary>
        /// Property provides name of AudioVolumeType.
        /// </summary>
        public string VolumeTypeName
        {
            get { return CurrentVolumeType.ToString(); }
        }

        /// <summary>
        /// Property provides maximum volume for current volume type.
        /// </summary>
        public int MaxVolume
        {
            private set { SetProperty(ref maxVolume, value); }
            get { return maxVolume; }
        }

        /// <summary>
        /// Property indicates whether previous volume type can be selected.
        /// </summary>
        public bool CanSelectNext => VolumeTypeIndex < volumeTypes.Count - 1;

        /// <summary>
        /// Property indicates whether next volume type can be selected.
        /// </summary>
        public bool CanSelectPrevious => VolumeTypeIndex > 0;

        /// <summary>
        /// Property provies current volume.
        /// </summary>
        public int CurrentVolume
        {
            set
            {
                value = Math.Clamp(value, 0, MaxVolume);

                if (value != currentVolume)
                {
                    SetProperty(ref currentVolume, value);

                    UpdateVolume();

                    // Slider is bound to SliderVolume which relays CurrentVolume so 
                    // it is necessary to raise OnPropertyChanged for SliderVolume.
                    OnPropertyChanged("SliderVolume");
                }

            }
            get { return currentVolume; }
        }

        /*
         * It is a workaround to prevent unwanted CurrentVolume change.
         * Avoids limiting CurrentVolume to default sliders maximum which can be lower than maximum volume.
         * This could occur after binding Value and before binding Maximum.
         */

        /// <summary>
        /// Property indicates whether it is safe to use values from slider.
        /// </summary>
        public bool SliderUsable
        {
            set
            {
                sliderUsable = value;
                if (value == true)
                    SliderVolume = CurrentVolume;
            }
            get { return sliderUsable; }
        }

        /// <summary>
        /// Property relaying CurrentVolume to slider.
        /// </summary>
        public int SliderVolume
        {
            set
            {
                // Update CurrentVolume with slider value only when it is safe.
                if (SliderUsable)
                {
                    CurrentVolume = value;
                    OnPropertyChanged("SliderVolume");
                }
                else
                {
                    // Unwanted write happened, from now CurrentVolume can be updated.
                    SliderUsable = true;
                }
            }
            get { return CurrentVolume; }
        }

        /// <summary>
        /// Command selects next AudioVolumeType.
        /// </summary>
        public ICommand NextVolumeTypeCommand { private set; get; }

        /// <summary>
        /// Command selects previous AudioVolumeType.
        /// </summary>
        public ICommand PreviousVolumeTypeCommand { private set; get; }

        #endregion properties

        #region methods

        /// <summary>
        /// Volume controller class constructor.
        /// </summary>
        public VolumeControlViewModel()
        {
            volumeTypes = new List<AudioVolumeType>();

            foreach (AudioVolumeType volumeType in Enum.GetValues(typeof(AudioVolumeType)))
            {
                // Skip type None as it cannot be used.
                if (volumeType == AudioVolumeType.None)
                    continue;

                volumeTypes.Add(volumeType);
            }

            NextVolumeTypeCommand = new Command(NextVolumeType);
            PreviousVolumeTypeCommand = new Command(PreviousVolumeType);

            VolumeTypeIndex = 0;
        }

        /// <summary>
        /// This function selects next volume type by increasing TypeIndex.
        /// </summary>
        private void NextVolumeType()
        {
            VolumeTypeIndex += 1;
        }

        /// <summary>
        /// This function selects previous volume type by decreasing TypeIndex.
        /// </summary>
        private void PreviousVolumeType()
        {
            VolumeTypeIndex -= 1;
        }

        /// <summary>
        /// This function sets volume level of currently selected volume type to CurrentVolume.
        /// </summary>
        private void UpdateVolume()
        {
            AudioManager.VolumeController.Level[CurrentVolumeType] = CurrentVolume;
        }

        #endregion methods
    }
}
