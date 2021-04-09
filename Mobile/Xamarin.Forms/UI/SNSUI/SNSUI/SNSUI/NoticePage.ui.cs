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
    /// NoticePage partial class declare.
    /// </summary>
    public class NoticePage : ContentPage
    {
        private bool contentLoaded;
        private List<ListGroup> listGroups;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoticePage"/> class.
        /// </summary>
        public NoticePage()
        {
            InitializeData();
            InitializeComponent();
        }

        /// <summary>
        /// Initial method for the class of NoticePage
        /// </summary>
        void InitializeComponent()
        {
            IconImageSource = "00_controlbar_icon_more.png";
            Title = "Notice";

            if (contentLoaded)
            {
                return;
            }

            contentLoaded = true;

            Content = CreateSettingGroupsListView();
        }

        /// <summary>
        /// A method return ListView for the class of NoticePage
        /// </summary>
        /// <returns>Return a list view with group header</returns>
        private ListView CreateSettingGroupsListView()
        {
            ListView listView = new ListView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowHeight = (int)(App.ScreenHeight / 10),
                IsGroupingEnabled = true,
                GroupHeaderTemplate = MyGroupHeaderTemplate,
                ItemsSource = listGroups,
                ItemTemplate = new DataTemplate(() =>
                {
                    var cell = new Type1Cell();
                    cell.SetBinding(Type1Cell.TextProperty, "Name");
                    cell.SetBinding(Type1Cell.TextEndProperty, "On");
                    cell.IsCheckVisible = false;
                    return cell;
                })
            };
            return listView;
        }

        /// <summary>
        /// A method return DataTemplate for the group title of ListView
        /// </summary>
        private DataTemplate MyGroupHeaderTemplate = new DataTemplate(() =>
        {
            TextCell groupCell = new TextCell();
            groupCell.SetBinding(TextCell.TextProperty, "Title");

            return groupCell;
        });


        /// <summary>
        /// Initial method for the class of NoticePage
        /// </summary>
        void InitializeData()
        {
            listGroups = new List<ListGroup>();
            for (int i = 0; i < 3; ++i)
            {
                /// Add empty group
                listGroups.Add(new ListGroup("\n"));
                var group = new ListGroup("Setting");
                for (int j = 0; j < 4; ++j)
                {
                    group.Add(new ListItemData
                    {
                        Name = "Account setting",
                        On = "On",
                    });
                }

                listGroups.Add(group);
            }
        }
    }

    /// <summary>
    /// A class that represents a group.
    /// </summary>
    class ListGroup : List<ListItemData>
    {
        public string Title { get; set; }

        public ListGroup(string title)
        {
            Title = title;
        }
    }

    /// <summary>
    /// A class for an item for a group.
    /// </summary>
    class ListItemData
    {
        public string Name { get; set; }
        public string On { get; set; }
    }
}