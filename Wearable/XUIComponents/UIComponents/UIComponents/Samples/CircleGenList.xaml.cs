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
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace UIComponents.Samples
{
    /// <summary>
    /// CircleGenList class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircleGenList : CirclePage
    {
        /// <summary>
        /// Constructor of CircleGenList class
        /// </summary>
        public CircleGenList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when item is tapped
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">ItemTappedEventArgs</param>
        public void OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            if (args.Item == null)
            {
                return;
            }

            var desc = args.Item as Sample;
            Console.WriteLine($"OnItemTapped desc.Class:{desc.Class}");
            if (desc != null && desc.Class != null)
            {
                Type pageType = desc.Class;

                // Create page and push it to navigation stack
                var page = Activator.CreateInstance(pageType) as Page;
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page as Page);
            }
        }
    }
}
