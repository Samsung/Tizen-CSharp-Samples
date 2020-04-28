/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xamarin.Forms;
using System.Collections.Generic;
using SNSUI.Extensions;

namespace SNSUI
{
    /// <summary>
    /// FriendsPage class declare.
    /// </summary>
    public class FriendsPage : ContentPage
    {
        private bool contentLoaded;
        private List<FriendListItemData> listItemData;

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendsPage"/> class.
        /// </summary>
        public FriendsPage()
        {
            InitializeData();
            InitializeComponent();
        }

        /// <summary>
        /// Initialize method for class FriendsPage.
        /// </summary>
        void InitializeData()
        {
            listItemData = new List<FriendListItemData>();
            for (int i = 0; i < 20; i++)
            {
                listItemData.Add(new FriendListItemData
                {
                    TextHeader = "Friend",
                    TextSub = "Will you add the friend?",
                    TextButton = "+",
                });
            }
        }

        /// <summary>
        /// Initial method for the class of FriendsPage
        /// </summary>
        void InitializeComponent()
        {
            IconImageSource = "00_controlbar_icon_artists.png";
            Title = "Friends";

            if (contentLoaded)
            {
                return;
            }

            contentLoaded = true;

            Content = CreateFriendsListView();
        }

        /// <summary>
        /// A method return ListView.
        /// </summary>
        /// <returns>Return a list view with friends info</returns>
        private ListView CreateFriendsListView()
        {
            ListView listView = new ListView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowHeight = (int)(App.ScreenHeight / 9),
                ItemsSource = listItemData,
                ItemTemplate = new DataTemplate(() =>
                {
                    var cell = new CustomCell { };
                    cell.SetBinding(CustomCell.TextProperty, "TextHeader");
                    cell.SetBinding(CustomCell.SubProperty, "TextSub");
                    cell.IsCheckVisible = true;
                    return cell;
                })
            };

            return listView;
        }

        /// <summary>
        /// Inside class of class FriendsPage.
        /// </summary>
        class FriendListItemData
        {
            public string TextHeader { get; set; }
            public string TextSub { get; set; }
            public string TextButton { get; set; }
        }
    }
}