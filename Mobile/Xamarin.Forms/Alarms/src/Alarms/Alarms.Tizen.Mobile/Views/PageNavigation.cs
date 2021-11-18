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

using Alarms.Tizen.Mobile.Views;
using Alarms.Views;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(PageNavigation))]

namespace Alarms.Tizen.Mobile.Views
{
    /// <summary>
    /// Page resolver implementation for Tizen.Mobile.
    /// </summary>
    internal class PageNavigation : IPageNavigation
    {
        #region methods

        /// <summary>
        /// Creates and sets main (welcome) page.
        /// </summary>
        public void CreateMainPage()
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        /// <summary>
        /// Goes to page with application choice for new alarm.
        /// </summary>
        public void GoToAppSelectPage()
        {
            Application.Current.MainPage.Navigation.PushAsync(new AppSelectPage());
        }

        /// <summary>
        /// Goes to page with new alarm type choice.
        /// </summary>
        /// <param name="bindingContext">BindingContext.</param>
        public void GoToAlarmTypeSelectPage(object bindingContext)
        {
            Application.Current.MainPage.Navigation.PushAsync(new AlarmTypeSelectPage()
            {
                BindingContext = bindingContext
            });
        }

        /// <summary>
        /// Goes to page with new alarm details settings for delay type alarm.
        /// </summary>
        /// <param name="bindingContext">BindingContext.</param>
        public void GoToDelayAlarmSettingsPage(object bindingContext)
        {
            Application.Current.MainPage.Navigation.PushAsync(new CountdownAlarmSettingsPage()
            {
                BindingContext = bindingContext
            });
        }

        /// <summary>
        /// Goes to page with new alarm details settings for exact date type alarm.
        /// </summary>
        /// <param name="bindingContext">BindingContext.</param>
        public void GoToDateAlarmSettingsPage(object bindingContext)
        {
            Application.Current.MainPage.Navigation.PushAsync(new DateAlarmSettingsPage()
            {
                BindingContext = bindingContext
            });
        }

        /// <summary>
        /// Goes to page with alarms list property.
        /// </summary>
        public void GoToAlarmListPage()
        {
            Application.Current.MainPage = new NavigationPage(new AlarmListPage());
        }

        /// <summary>
        /// Goes to page with "No Alarms" information.
        /// </summary>
        public void GoToNoAlarmsPage()
        {
            Application.Current.MainPage = new NavigationPage(new NoAlarmsPage());
        }

        #endregion
    }
}
