/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Tizen.Xamarin.Forms.Extension;
using Xamarin.Forms;

namespace Clock.Controls
{
    /// <summary>
    /// Options when HW Menu key is pressed
    /// </summary>
    public enum MORE_MENU_OPTION
    {
        MORE_MENU_NONE,
        // in case that only one item is added to ListView
        MORE_MENU_DELETE,
        // in case that two or more items are added to ListView
        MORE_MENU_DELETE_AND_REORDER
    };

    /// <summary>
    /// MoreMenuDialog
    /// It's shown when one or more items are added to ListView and HW menu key is pressed
    /// An app user can select more menu
    /// </summary>
    public class MoreMenuDialog : Dialog
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="option">An option for more menu list</param>
        /// <param name="deleteFunc">a function called when "Delete" option is selected</param>
        /// <param name="reorderFunc">a function called when "Reorder" option is selected</param>
        public MoreMenuDialog(MORE_MENU_OPTION option, Action<string> deleteFunc, Action<string> reorderFunc = null) : base()
        {
            if (option == MORE_MENU_OPTION.MORE_MENU_DELETE_AND_REORDER && reorderFunc == null)
            {
                System.Diagnostics.Debug.WriteLine("## ERROR ##");
                Toast.DisplayText("MoreMenuDialog() you need to pass reorderFunc when more than two cities are added to listview.");
                return;
            }

            HorizontalOption = LayoutOptions.Fill;
            Grid grid = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,//Color.FromHex("0A3DB9CC"),
                RowSpacing = 5,
                ColumnDefinitions =
                {
                    new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                }
            };

            // Menu : Delete
            Command deleteCommand = new Command<string>(deleteFunc);
            MoreMenuItem itemDelete = new MoreMenuItem
            {
                Text = "Delete",
                Command = deleteCommand,
                CommandParameter = "Delete",
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(itemDelete, FontWeight.Light);

            switch (option)
            {
                case MORE_MENU_OPTION.MORE_MENU_DELETE:
                    grid.RowDefinitions = new RowDefinitionCollection
                    {
                        new RowDefinition { Height = GridLength.Auto },
                    };
                    grid.Children.Add(itemDelete, 0, 0);
                    break;
                case MORE_MENU_OPTION.MORE_MENU_DELETE_AND_REORDER:
                    BoxView box = new BoxView
                    {
                        Color = Color.LightGray,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest = 2,
                    };
                    // Menu : Reorder
                    Command reorderCommand = new Command<string>(reorderFunc);
                    MoreMenuItem itemReorder = new MoreMenuItem
                    {
                        Text = "Reorder",
                        Command = reorderCommand,
                        CommandParameter = "Reorder",
                    };
                    // to meet To meet thin attribute for font, need to use custom feature
                    FontFormat.SetFontWeight(itemReorder, FontWeight.Light);
                    grid.RowDefinitions = new RowDefinitionCollection
                    {
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto },
                    };
                    grid.Children.Add(itemDelete, 0, 0);
                    grid.Children.Add(box, 0, 1);
                    grid.Children.Add(itemReorder, 0, 2);
                    break;
                default:
                    // Error case
                    Toast.DisplayText("MoreMenuDialog() An error occurs");
                    break;
            }

            Content = new StackLayout
            {
                BackgroundColor = Color.FromHex("0A3DB9CC"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Spacing = 10,
                Children = { grid },
                WidthRequest = 720,
            };
        }
    }
}
