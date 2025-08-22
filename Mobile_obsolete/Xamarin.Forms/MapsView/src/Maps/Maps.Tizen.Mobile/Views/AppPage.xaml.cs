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

using Maps.Interfaces;
using Maps.Tizen.Mobile.Views;
using Maps.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppPage))]
namespace Maps.Tizen.Mobile.Views
{
    /// <summary>
    /// Application view.
    /// </summary>
    public partial class AppPage : IAppPage
    {
        #region methods

        /// <summary>
        /// AppPage constructor.
        /// Initializes application view.
        /// Add platform specific map component.
        /// </summary>
        public AppPage()
        {
            BindingContext = ViewModelLocator.ViewModel;
            InitializeComponent();

            if (ViewModelLocator.ViewModel.IsMapInitialized)
            {
                ShowMap();
            }
            else
            {
                ShowMapNotConfiguredWarningNotification();
            }
        }

        /// <summary>
        /// Adds map element to the page.
        /// </summary>
        private void ShowMap()
        {
            MapContainerWrapper.Children.Add(new MapWrapper());
        }

        /// <summary>
        /// Shows notification text if map is not configured.
        /// </summary>
        private void ShowMapNotConfiguredWarningNotification()
        {
            MapContainerWrapper.Children.Add(new Label
            {
                Text = Config.MAP_NOT_CONFIGURED_MSG,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 22,
                HorizontalTextAlignment = TextAlignment.Center
            });
        }

        #endregion
    }
}