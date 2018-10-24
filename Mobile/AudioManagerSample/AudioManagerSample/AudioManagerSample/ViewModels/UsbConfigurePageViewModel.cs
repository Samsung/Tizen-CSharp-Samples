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

namespace AudioManagerSample
{
    /// <summary>
    /// ViewModel for UsbConfigurePage.
    /// </summary>
    class UsbConfigurePageViewModel : BaseViewModel
    {
        public UsbConfigurePageViewModel(DeviceItem selectedItem)
        {
            Log.Debug("USB configuration page for device(id:" + selectedItem.Id + ")");

            Id = selectedItem.Id;
            Type = selectedItem.Type;
            Name = selectedItem.Name;

            _mediaStreamOnly = AMController.GetMediaStreamOnly(selectedItem.Id);
            _avoidResampling = AMController.GetAvoidResampling(selectedItem.Id);

            AMController.DeviceConnectionChanged += OnConnectionChanged;
        }

        protected IAudioManagerController AMController => DependencyService.Get<IAudioManagerController>();

        private void OnConnectionChanged(object sender, DeviceConnectionChangedEventArgs e)
        {
            if (e.IsConnected == false && e.Device.Id == Id)
            {
                Log.Debug("This device(id:" + Id + ") is disconnected, pop this page");
                Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        public int Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        private bool _mediaStreamOnly;
        public bool MediaStreamOnly
        {
            get => _mediaStreamOnly;
            set
            {
                if (_mediaStreamOnly != value)
                {
                    AMController.SetMediaStreamOnly(Id, value);
                    _mediaStreamOnly = value;

                    OnPropertyChanged(nameof(MediaStreamOnly));
                }
            }
        }

        private bool _avoidResampling;
        public bool AvoidResampling
        {
            get => _avoidResampling;
            set
            {
                if (_avoidResampling != value)
                {
                    AMController.SetAvoidResampling(Id, value);
                    _avoidResampling = value;

                    OnPropertyChanged(nameof(AvoidResampling));
                }
            }
        }
        public override void OnPopped()
        {
            base.OnPopped();

            AMController.DeviceConnectionChanged -= OnConnectionChanged;
        }
    }
}