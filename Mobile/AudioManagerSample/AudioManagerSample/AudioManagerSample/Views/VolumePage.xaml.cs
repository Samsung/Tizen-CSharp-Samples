/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AudioManagerSample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VolumePage : ContentPage
    {
		public VolumePage ()
		{
            SecurityProvider.Instance.CheckPrivilege();
            InitializeComponent ();
		}

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (sender == systemSlider)
            {
                VolumePageViewModel vm = new VolumePageViewModel();
                vm.SystemLabel = (int)e.NewValue;
            }
            else if (sender == mediaSlider)
            {
                VolumePageViewModel vm = new VolumePageViewModel();
                vm.MediaLabel = (int)e.NewValue;
            }
            else if (sender == notificationSlider)
            {
                VolumePageViewModel vm = new VolumePageViewModel();
                vm.NotificationLabel = (int)e.NewValue;
            }
            else if (sender == alarmSlider)
            {
                VolumePageViewModel vm = new VolumePageViewModel();
                vm.AlarmLabel = (int)e.NewValue;
            }
            else if (sender == voiceSlider)
            {
                VolumePageViewModel vm = new VolumePageViewModel();
                vm.VoiceLabel = (int)e.NewValue;
            }
        }
    }
}