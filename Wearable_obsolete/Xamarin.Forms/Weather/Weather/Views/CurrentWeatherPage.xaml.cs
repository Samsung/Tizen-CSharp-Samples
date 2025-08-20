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

using Weather.Models.Location;
using Xamarin.Forms;

namespace Weather.Views
{
    /// <summary>
    /// Interaction logic for CurrentWeatherPage.xaml.
    /// </summary>
    public partial class CurrentWeatherPage
    {
        #region properties

        /// <summary>
        /// Bindable property that allows to set city data selected by user.
        /// </summary>
        public static readonly BindableProperty CityDataProperty =
            BindableProperty.Create(nameof(CityData), typeof(City), typeof(CurrentWeatherPage), default(City));

        /// <summary>
        /// Gets or sets city data selected by user.
        /// </summary>
        public City CityData
        {
            get => (City)GetValue(CityDataProperty);
            set => SetValue(CityDataProperty, value);
        }

        #endregion

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public CurrentWeatherPage()
        {
            InitializeComponent();
        }
    }
}