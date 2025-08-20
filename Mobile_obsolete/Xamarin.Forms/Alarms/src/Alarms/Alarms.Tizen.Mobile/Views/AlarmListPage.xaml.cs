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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Alarms.ViewModels;
using System;

namespace Alarms.Tizen.Mobile.Views
{
    /// <summary>
    /// AlarmListPage Xaml C# partial class code.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmListPage : ContentPage
    {
        #region fields

        /// <summary>
        /// Flag indicating if page is visible.
        /// </summary>
        private bool _visible;

        #endregion

        #region methods

        /// <summary>
        /// Class constructor with component initialization (Xaml partial).
        /// </summary>
        public AlarmListPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Overrides OnAppearing method.
        /// Starts timer which refreshes alarm list.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _visible = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(1000), () =>
            {
                ((AlarmListViewModel)BindingContext).RefreshCommand.Execute(null);
                return _visible;
            });
        }

        /// <summary>
        /// Overrides OnDisappearing method.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _visible = false;
        }

        #endregion
    }
}