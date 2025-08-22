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

using CalendarComponent.Tizen.Mobile.Views;
using Xamarin.Forms;
using CalendarComponent.Interfaces;
using CalendarComponent.Tizen.Mobile.Components;

[assembly: Dependency(typeof(CalendarPage))]
namespace CalendarComponent.Tizen.Mobile.Views
{
    /// <summary>
    /// CalendarPage class.
    /// Main application view.
    /// </summary>
    public partial class CalendarPage : IAppPage
    {
        #region methods

        /// <summary>
        /// Application main page constructor.
        /// Sets binding context and adds Calendar component.
        /// </summary>
        public CalendarPage()
        {
            InitializeComponent();

            BindingContext = ViewModels.ViewModelLocator.ViewModel;
            CalendarWrapper.Children.Add(new CalendarControl());
        }

        #endregion
    }
}