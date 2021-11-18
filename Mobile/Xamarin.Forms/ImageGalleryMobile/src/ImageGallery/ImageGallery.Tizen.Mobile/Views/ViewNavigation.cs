/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using Xamarin.Forms;
using ImageGallery.Tizen.Mobile.Views;
using ImageGallery.Views;
using System.Linq;

[assembly: Dependency(typeof(ViewNavigation))]
namespace ImageGallery.Tizen.Mobile.Views
{
    /// <summary>
    /// Class handling navigation over application views.
    /// </summary>
    class ViewNavigation : IViewNavigation
    {
        #region methods

        /// <summary>
        /// Navigates to the view displaying image details.
        /// </summary>
        public void GoToDetailsView()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DetailsPage());
        }

        /// <summary>
        /// Navigates back to previously displayed view.
        /// </summary>
        public void GoToPreviousView()
        {
            Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault()?.Navigation.PopAsync();
        }

        #endregion
    }
}
