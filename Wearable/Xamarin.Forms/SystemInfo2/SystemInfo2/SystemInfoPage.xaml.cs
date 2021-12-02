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

using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;
using SystemInfo.InfoPages;
using Xamarin.Forms;

namespace SystemInfo
{
    /// <summary>
    /// SystemInfoPage partial class
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SystemInfoPage : CarouselPage
    {

        public SystemInfoPage()
        {
            InitializeComponent();
            AddChildren();
        }

        /// <summary>
        /// Add all the sub pages to the main page Children collection
        /// </summary>    
        private void AddChildren()
        {
            this.Children.Add(new BatteryPage());
            this.Children.Add(new DisplayPage());
            this.Children.Add(new CapabilitiesPage());
            this.Children.Add(new SettingsPage());
        }
    }
}