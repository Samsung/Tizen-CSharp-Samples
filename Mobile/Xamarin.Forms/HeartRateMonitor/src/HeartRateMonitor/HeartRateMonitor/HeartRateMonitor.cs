/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using HeartRateMonitor.Constants;
using HeartRateMonitor.Navigation;
using HeartRateMonitor.ViewModels;
using HeartRateMonitor.Views;
using Xamarin.Forms;

namespace HeartRateMonitor
{
    /// <summary>
    /// App class.
    /// </summary>
    public class App : Application
    {
        #region properties

        /// <summary>
        /// An instance of the MainViewModel class.
        /// </summary>
        public MainViewModel AppMainViewModel { private set; get; }

        #endregion

        #region methods

        /// <summary>
        /// App class constructor.
        /// </summary>
        public App()
        {
            MeasurementPage appMeasurementPage = new MeasurementPage();
            AppMainViewModel = new MainViewModel(Properties, new PageNavigation(appMeasurementPage.Navigation));
            appMeasurementPage.BindingContext = AppMainViewModel;

            MainPage = new NavigationPage(appMeasurementPage)
            {
                BarBackgroundColor = ColorConstants.BASE_APP_COLOR
            };

            appMeasurementPage.Init();
        }

        /// <summary>
        /// Handles the application start.
        /// Initializes view model.
        /// </summary>
        protected override async void OnStart()
        {
            base.OnStart();

            await AppMainViewModel.Init();
        }

        #endregion
    }
}