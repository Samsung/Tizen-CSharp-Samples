/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Location;
using WeatherWatch.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherWatch.Pages
{
    /// <summary>
    /// WeatherWatchPage class
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WeatherWatchPage : ContentPage
	{
        public WeatherWatchPage()
		{
            InitializeComponent();
        }

        // Called when Battery Image or text is tapped
        void Battery_Tapped(object sender, EventArgs e)
        {
            ViewModel.BatteryTextIsVisible = !ViewModel.BatteryTextIsVisible;
        }

        // Called when Air pollution Quality Index(AQI) Image or text is tapped
        void AirPollution_Tapped(object sender, EventArgs e)
        {
            // Ignore when AQI Text information is not available
            if (ViewModel.AqiText == "")
            {
                return;
            }

            ViewModel.AqiTextIsVisible = !ViewModel.AqiTextIsVisible;
        }
    }
}