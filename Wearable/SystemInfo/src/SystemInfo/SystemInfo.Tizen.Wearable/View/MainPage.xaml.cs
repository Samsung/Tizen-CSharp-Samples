/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System.Collections.ObjectModel;
using SystemInfo.ViewModel;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SystemInfo.Tizen.Wearable.View
{
    /// <summary>
    /// Main page of the application. Shows application's main menu.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        #region methods

        /// <summary>
        /// Default class constructor.
        /// Initializes page component.
        /// </summary>
        public MainPage()
        {
            BindingContext = PrepareMainMenuItems();

            InitializeComponent();
        }

        /// <summary>
        /// Returns main menu items grouped by their type.
        /// </summary>
        /// <returns>Main menu items.</returns>
        private ObservableCollection<MenuItemsCollection> PrepareMainMenuItems()
        {
            return new ObservableCollection<MenuItemsCollection>
            {
                new MenuItemsCollection(new PropertyViewModel().PropertiesCollection)
                {
                    Name = "Properties"
                },
                new MenuItemsCollection(new OtherViewModel().PropertiesCollection)
                {
                    Name = "Other"
                }
            };
        }

        #endregion
    }
}
