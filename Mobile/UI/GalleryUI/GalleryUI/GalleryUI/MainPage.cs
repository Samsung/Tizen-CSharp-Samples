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

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GalleryUI
{
    /// <summary>
    /// The main page of the gallery application.
    /// </summary>
    public class MainPage : ContentPage
    {
        // The portrait size of thumbnail.
        private int PORTRAIT_SIZE = (int)(0.126 * App.screenHeight);
        private double RATIO_GRID = 0.117;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        /// <param name="isLandscape">Is landscape or not.</param>
        public MainPage(bool isLandscape) : base()
        {
            // Create a relative layout.
            RelativeLayout relativeLayout = new RelativeLayout()
            {
                IsVisible = true,
            };

            // Create header label.
            Label headerLabel = CreateHeaderLabel();

            // Add header label into main page layout.
            relativeLayout.Children.Add(
                    headerLabel,
                    Constraint.Constant(0),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Width;
                    }),
                    Constraint.Constant(RATIO_GRID * App.screenHeight));

            Grid grid = null;

            if (isLandscape)
            {
                // Create a Grid object to show the thumbnail for image for landscape.
                grid = CreateLandscapeGrid();
            }
            else
            {
                // Create a Grid object to show the thumbnail for image for portrait.
                grid = CreatePortraitGrid();
            }

            // Add grid view into layout.
            relativeLayout.Children.Add(
                grid,
                Constraint.Constant(0),
                Constraint.Constant(RATIO_GRID * App.screenHeight),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height - (RATIO_GRID * App.screenHeight);
                }));

            // Build the page.
            Content = relativeLayout;
        }

        /// <summary>
        /// Create grid object for portrait.
        /// </summary>
        /// <returns>The grid for portrait.</returns>
        private Grid CreatePortraitGrid()
        {
            // Create a Grid object to show the thumbnail for image.
            Grid grid = new Grid()
            {
                Padding = 28,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    // 3 rows.
                    new RowDefinition { Height = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                },
                ColumnDefinitions =
                {
                    // 4 columns.
                    new ColumnDefinition { Width = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                },
            };

            // Put image into grid view one by one.

            // 1st row
            // 1st row 1 column
            grid.Children.Add(new Image
            {
                Source = "0.jpg",
                Aspect = Aspect.Fill,
            }, 0, 1, 0, 1);

            // 1st row 2 column
            grid.Children.Add(new Image
            {
                Source = "1.jpg",
                Aspect = Aspect.Fill,
            }, 1, 2, 0, 1);

            // 1st row 3 column
            grid.Children.Add(new Image
            {
                Source = "2.jpg",
                Aspect = Aspect.Fill,
            }, 2, 3, 0, 1);

            // 1st row 4 column
            grid.Children.Add(new Image
            {
                Source = "3.jpg",
                Aspect = Aspect.Fill,
            }, 3, 4, 0, 1);

            // 2nd row
            // 2nd row 1 column
            grid.Children.Add(new Image
            {
                Source = "4.jpg",
                Aspect = Aspect.Fill,
            }, 0, 1, 1, 2);

            // 2nd row 2 column
            grid.Children.Add(new Image
            {
                Source = "5.jpg",
                Aspect = Aspect.Fill,
            }, 1, 2, 1, 2);

            // 2nd row 3 column
            grid.Children.Add(new Image
            {
                Source = "6.jpg",
                Aspect = Aspect.Fill,
            }, 2, 3, 1, 2);

            // 2nd row 4 column
            grid.Children.Add(new Image
            {
                Source = "7.jpg",
                Aspect = Aspect.Fill,
            }, 3, 4, 1, 2);

            // 3rd row
            // 3rd row 1 column
            grid.Children.Add(new Image
            {
                Source = "8.jpg",
                Aspect = Aspect.Fill,
            }, 0, 1, 2, 3);

            // 3rd row 2 column
            grid.Children.Add(new Image
            {
                Source = "9.jpg",
                Aspect = Aspect.Fill,
            }, 1, 2, 2, 3);

            return grid;
        }

        /// <summary>
        /// Create grid object for landscape.
        /// </summary>
        /// <returns>The grid for landscape.</returns>
        private Grid CreateLandscapeGrid()
        {
            // Create a Grid object to show the thumbnail for image.
            Grid grid = new Grid()
            {
                Padding = 40,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    // 2 rows.
                    new RowDefinition { Height = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                },
                ColumnDefinitions =
                {
                    // 7 columns.
                    new ColumnDefinition { Width = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(PORTRAIT_SIZE, GridUnitType.Absolute) },
                },
            };

            // Put image into grid view one by one.
            // 1st row
            // 1st row 1 column
            grid.Children.Add(new Image
            {
                Source = "0.jpg",
                Aspect = Aspect.Fill,
            }, 0, 1, 0, 1);

            // 1st row 2 column
            grid.Children.Add(new Image
            {
                Source = "1.jpg",
                Aspect = Aspect.Fill,
            }, 1, 2, 0, 1);

            // 1st row 3 column
            grid.Children.Add(new Image
            {
                Source = "2.jpg",
                Aspect = Aspect.Fill,
            }, 2, 3, 0, 1);

            // 1st row 4 column
            grid.Children.Add(new Image
            {
                Source = "3.jpg",
                Aspect = Aspect.Fill,
            }, 3, 4, 0, 1);

            // 1st row 5 column
            grid.Children.Add(new Image
            {
                Source = "4.jpg",
                Aspect = Aspect.Fill,
            }, 4, 5, 0, 1);

            // 1st row 6 column
            grid.Children.Add(new Image
            {
                Source = "5.jpg",
                Aspect = Aspect.Fill,
            }, 5, 6, 0, 1);

            // 1st row 7 column
            grid.Children.Add(new Image
            {
                Source = "6.jpg",
                Aspect = Aspect.Fill,
            }, 6, 7, 0, 1);

            // 2nd row
            // 2nd row 1 column
            grid.Children.Add(new Image
            {
                Source = "7.jpg",
                Aspect = Aspect.Fill,
            }, 0, 1, 1, 2);

            // 2nd row 2 column
            grid.Children.Add(new Image
            {
                Source = "8.jpg",
                Aspect = Aspect.Fill,
            }, 1, 2, 1, 2);

            // 2nd row 3 column
            grid.Children.Add(new Image
            {
                Source = "9.jpg",
                Aspect = Aspect.Fill,
            }, 2, 3, 1, 2);

            return grid;
        }

        /// <summary>
        /// Create header label.
        /// </summary>
        /// <returns>Header label.</returns>
        private Label CreateHeaderLabel()
        {
            return new Label()
            {
                Text = "Gallery",
                FontSize = 28,
                TextColor = Color.White,
                FontAttributes = FontAttributes.None,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
        }
    }
}
