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

using Alarms.Controls;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Alarms.Tizen.Mobile.Views
{
    /// <summary>
    /// CountdownAlarmSettingsPage Xaml C# partial class code.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountdownAlarmSettingsPage : ContentPage
    {
        #region methods

        /// <summary>
        /// Method that is called when the binding context changes.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (Resources != null)
            {
                foreach (var resource in Resources.Values.OfType<DialogBase>())
                {
                    resource.BindingContext = BindingContext;
                }
            }
        }

        /// <summary>
        /// Class constructor with component initialization (Xaml partial).
        /// </summary>
        public CountdownAlarmSettingsPage()
        {
            InitializeComponent();
        }

        #endregion
    }
}