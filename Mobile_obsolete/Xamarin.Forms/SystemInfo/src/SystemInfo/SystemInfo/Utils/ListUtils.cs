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

using System.Collections.ObjectModel;
using System.Windows.Input;
using SystemInfo.ViewModel.List;
using Xamarin.Forms;

namespace SystemInfo.Utils
{
    /// <summary>
    /// Class that contains methods for creating different list items.
    /// </summary>
    public static class ListUtils
    {
        #region fields

        /// <summary>
        /// Navigation context.
        /// </summary>
        private static readonly INavigation Navigation;

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        static ListUtils()
        {
            Navigation = Application.Current.MainPage?.Navigation;
        }

        /// <summary>
        /// Creates and populates observable collection of items. Each item navigate to another page.
        /// </summary>
        /// <param name="properties">Titles of items.</param>
        /// <returns>Collection of items with navigation.</returns>
        public static ObservableCollection<ItemViewModel> CreateItemsListWithNavigation(string[] properties)
        {
            var itemCollection = new ObservableCollection<ItemViewModel>();

            foreach (var property in properties)
            {
                itemCollection.Add(new ItemViewModel(property, CreateItemCommand(PageProvider.CreatePage(property))));
            }

            return itemCollection;
        }

        /// <summary>
        /// Creates observable, grouped collection of items with initial values.
        /// </summary>
        /// <param name="titles">Titles of items.</param>
        /// <param name="groupName">Group name.</param>
        /// <param name="initialValues">Initial values of items.</param>
        /// <returns>Collection of items with initial values.</returns>
        public static ListItem CreateGroupedItemsList(string[] titles, string groupName, string[] initialValues)
        {
            var list = CreateItemsList(titles, initialValues, ListItemType.Standard);
            list.GroupName = groupName;

            return list;
        }

        /// <summary>
        /// Creates observable collection of items with initial values. All items has common type.
        /// </summary>
        /// <param name="titles">Titles of items.</param>
        /// <param name="initialValues">Initial values of items.</param>
        /// <param name="itemType">Common type for all types.</param>
        /// <returns>Collection of items with initial values.</returns>
        public static ListItem CreateItemsList(string[] titles, string[] initialValues, ListItemType itemType)
        {
            var itemCollection = new ListItem();

            for (int i = 0; i < titles.Length; i++)
            {
                itemCollection.Add(new ItemViewModel(titles[i], new Command(() => { }, () => false), initialValues[i],
                    itemType));
            }

            return itemCollection;
        }

        /// <summary>
        /// Creates observable collection of items with initial values. The type must be specified for each item.
        /// </summary>
        /// <param name="titles">Titles of items.</param>
        /// <param name="initialValues">Initial values of items.</param>
        /// <param name="itemsType">Items' type.</param>
        /// <returns>Collection of list items.</returns>
        public static ListItem CreateItemsList(string[] titles, string[] initialValues, ListItemType[] itemsType)
        {
            var itemCollection = new ListItem();

            for (int i = 0; i < titles.Length; i++)
            {
                itemCollection.Add(new ItemViewModel(titles[i], new Command(() => { }, () => false), initialValues[i],
                    itemsType[i]));
            }

            return itemCollection;
        }

        /// <summary>
        /// Creates command for item, that navigate to another page.
        /// </summary>
        /// <param name="page">Page that will be pushed to navigation stack.</param>
        /// <returns>Command object.</returns>
        private static ICommand CreateItemCommand(Page page)
        {
            return new Command(() => { Navigation.PushAsync(page); }, () => true);
        }

        #endregion
    }
}