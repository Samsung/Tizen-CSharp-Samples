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

using ApplicationStoreUI.Extensions;
using Xamarin.Forms;

/// <summary>
/// ApplicationStoreUI is a sample app to provide a simple layout example of store app.
/// </summary>
namespace ApplicationStoreUI
{
    /// <summary>
    /// Main view of ApplicationStore
    /// </summary>
    public class MainView : ContentPage
    {
        //Height of top grid item
        private const int TOP_ITEM_HEIGHT = 90;
        //Height of content grid item
        private const int CONTENT_ITEM_HEIGHT = 360;
        //Space between columns
        private const int CONTENT_ITEM_COLUMN_SPACE = 20;

        //Device screen width
        private int _screenWidth;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        /// <param name="screenWidth">The screen width</param>
        /// <param name="screenHeight">The screen height</param>
        public MainView(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;

            // Add a label as view title
            Label header = new Label
            {
                Text = "Application Store",
                TextColor = Color.White,
                FontSize = 27,
                HeightRequest = 80,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
            };

            //Top grid with 6 buttons
            Grid topGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = TOP_ITEM_HEIGHT},
                    new RowDefinition { Height = TOP_ITEM_HEIGHT},
                    new RowDefinition { Height = TOP_ITEM_HEIGHT}
                },
                HorizontalOptions = LayoutOptions.StartAndExpand,
                ColumnSpacing = CONTENT_ITEM_COLUMN_SPACE,
                Padding = new Thickness(10, 0, 10, 0),
            };
            topGrid.Children.Add(CreateItem("apps", TOP_ITEM_HEIGHT), 0, 0);
            topGrid.Children.Add(CreateItem("games", TOP_ITEM_HEIGHT), 1, 0);
            topGrid.Children.Add(CreateItem("movies", TOP_ITEM_HEIGHT), 0, 1);
            topGrid.Children.Add(CreateItem("music", TOP_ITEM_HEIGHT), 1, 1);
            topGrid.Children.Add(CreateItem("books", TOP_ITEM_HEIGHT), 0, 2);
            topGrid.Children.Add(CreateItem("magazines", TOP_ITEM_HEIGHT), 1, 2);

            //Create movie grid with 3 buttons
            Grid movieGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = CONTENT_ITEM_HEIGHT}
                },
                ColumnSpacing = CONTENT_ITEM_COLUMN_SPACE,
                Padding = new Thickness(10, 0, 10, 0),
            };
            movieGrid.Children.Add(CreateItem("movie1", CONTENT_ITEM_HEIGHT), 0, 0);
            movieGrid.Children.Add(CreateItem("movie2", CONTENT_ITEM_HEIGHT), 1, 0);
            movieGrid.Children.Add(CreateItem("movie3", CONTENT_ITEM_HEIGHT), 2, 0);

            //Create game grid with 3 buttons
            Grid gameGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = CONTENT_ITEM_HEIGHT}
                },
                ColumnSpacing = CONTENT_ITEM_COLUMN_SPACE,
                Padding = new Thickness(10, 0, 10, 0),
            };
            gameGrid.Children.Add(CreateItem("game1", CONTENT_ITEM_HEIGHT), 0, 0);
            gameGrid.Children.Add(CreateItem("game2", CONTENT_ITEM_HEIGHT), 1, 0);
            gameGrid.Children.Add(CreateItem("game3", CONTENT_ITEM_HEIGHT), 2, 0);

            //Create music grid with 3 buttons
            Grid musicGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = CONTENT_ITEM_HEIGHT}
                },
                ColumnSpacing = CONTENT_ITEM_COLUMN_SPACE,
                Padding = new Thickness(10, 0, 10, 0),
            };
            musicGrid.Children.Add(CreateItem("music1", CONTENT_ITEM_HEIGHT), 0, 0);
            musicGrid.Children.Add(CreateItem("music2", CONTENT_ITEM_HEIGHT), 1, 0);
            musicGrid.Children.Add(CreateItem("music3", CONTENT_ITEM_HEIGHT), 2, 0);

            //Create book grid with 3 buttons
            Grid bookGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = CONTENT_ITEM_HEIGHT}
                },
                ColumnSpacing = CONTENT_ITEM_COLUMN_SPACE,
                Padding = new Thickness(10, 0, 10, 0),
            };
            bookGrid.Children.Add(CreateItem("book1", CONTENT_ITEM_HEIGHT), 0, 0);
            bookGrid.Children.Add(CreateItem("book2", CONTENT_ITEM_HEIGHT), 1, 0);
            bookGrid.Children.Add(CreateItem("book3", CONTENT_ITEM_HEIGHT), 2, 0);

            //Create magazine grid with 3 buttons
            Grid magazineGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = CONTENT_ITEM_HEIGHT}
                },
                ColumnSpacing = CONTENT_ITEM_COLUMN_SPACE,
                Padding = new Thickness(10, 0, 10, 0),
            };
            magazineGrid.Children.Add(CreateItem("magazine1", CONTENT_ITEM_HEIGHT), 0, 0);
            magazineGrid.Children.Add(CreateItem("magazine2", CONTENT_ITEM_HEIGHT), 1, 0);
            magazineGrid.Children.Add(CreateItem("magazine3", CONTENT_ITEM_HEIGHT), 2, 0);

            //Create app grid with 3 buttons
            Grid appGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = CONTENT_ITEM_HEIGHT}
                },
                ColumnSpacing = CONTENT_ITEM_COLUMN_SPACE,
                Padding = new Thickness(10, 0, 10, 0),
            };
            appGrid.Children.Add(CreateItem("app1", CONTENT_ITEM_HEIGHT), 0, 0);
            appGrid.Children.Add(CreateItem("app2", CONTENT_ITEM_HEIGHT), 1, 0);
            appGrid.Children.Add(CreateItem("app3", CONTENT_ITEM_HEIGHT), 2, 0);

            //Put grids and labels into stack layout
            StackLayout layout = new StackLayout
            {
                BackgroundColor = Color.Transparent,
                Children =
                {
                    topGrid,
                    CreateLabel("New Movie Releases"),
                    movieGrid,
                    CreateLabel("Top Grossing Games"),
                    gameGrid,
                    CreateLabel("Chart Buster Music"),
                    musicGrid,
                    CreateLabel("Books"),
                    bookGrid,
                    CreateLabel("Magazine"),
                    magazineGrid,
                    CreateLabel("App of the week"),
                    appGrid
                }
            };

            Appearing += (s, e) =>
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        header,
                        new ScrollView
                        {
                            Content = layout
                        }
                    }
                };
            };
        }

        /// <summary>
        /// The method to create a new item of grid.
        /// </summary>
        /// <param name="source">The item's image source name</param>
        /// <param name="itemHeight">The item height</param>
        /// <returns>Return the new ImageButton</returns>
        /// <seealso cref="ImageButton">
        private ApplicationStoreUI.Extensions.ImageButton CreateItem(string source, int itemHeight)
        {
            int horizentalItemCount = 3;
            if (itemHeight == TOP_ITEM_HEIGHT)
            {
                horizentalItemCount = 2;
            }

            int itemWidth = (_screenWidth - 20 - horizentalItemCount * CONTENT_ITEM_COLUMN_SPACE)  / horizentalItemCount;

            ApplicationStoreUI.Extensions.ImageButton item = new ApplicationStoreUI.Extensions.ImageButton
            {
                Source = source + ".jpg",
                MinWidth = itemWidth,
                MinHeight = itemHeight,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            return item;
        }

        /// <summary>
        /// The method to create a new label.
        /// </summary>
        /// <param name="text">The label's text</param>
        /// <returns>Return the new label</returns>
        /// <seealso cref="Label">
        private Label CreateLabel(string text)
        {
            Label label = new Label
            {
                Margin = new Thickness(0, 15, 0, 20),
                Text = text,
                FontSize = 18
            };

            return label;
        }
    }
}
