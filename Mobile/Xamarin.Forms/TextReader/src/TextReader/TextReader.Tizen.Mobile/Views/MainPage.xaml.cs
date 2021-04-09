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

using System.Linq;
using Xamarin.Forms;

namespace TextReader.Tizen.Mobile.Views
{
    /// <summary>
    /// Main application page class.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        #region methods

        /// <summary>
        /// Main page constructor.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles binding context change.
        /// Updates context in all found resources (bindable type).
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (Resources != null)
            {
                foreach (var resource in Resources.Values.OfType<BindableObject>())
                {
                    resource.BindingContext = BindingContext;
                }
            }
        }

        #endregion
    }
}
