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

using Puzzle.Extensions;
using System;
using Xamarin.Forms;

namespace Puzzle
{

    /// <summary>
    /// Puzzle app's Main page, UI part.
    /// Puzzle main page contain 3 views:
    /// 1. Menu view on top.
    /// 2. Puzzle view in the middle.
    /// 3. Control button view at the bottom.
    /// </summary>
    public partial class PuzzlePage : ContentPage
    {
        /// <summary>
        /// Grid view for menu
        /// </summary>
        private View _menuGrid;

        /// <summary>
        /// Grid layout for puzzle
        /// </summary>
        private Grid _puzzleGrid;

        /// <summary>
        /// Grid view for control button
        /// </summary>
        private View _controlBtnGrid;

        /// <summary>
        /// Dialog view for answer dialog
        /// </summary>
        private Dialog _ansDialog;

        /// <summary>
        /// Dialog view for level dialog
        /// </summary>
        private Dialog _levelDialog;

        private bool _isAnsDialogDismiss = false;
        private bool _islevelDialogDismiss = false;

        /// <summary>
        /// Create Menu View in the top of main layout.
        /// </summary>
        /// <returns>return a Grid layout</returns>
        private View CreateMenuGrid()
        {
            /// <summary>
            /// The main Grid contain 3 menu item.
            /// </summary>
            Grid menuGrid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(100, 0),
                ColumnSpacing = 100,
                HeightRequest = 120,
                BackgroundColor = Color.White
            };

            // Answer menu's Image.
            Image icon1 = new Image()
            {
                Source = "photo.png",
                WidthRequest = 100,
                HeightRequest = 120
            };

            /// <summary>
            /// Answer menu.
            /// </summary>
            Button ansBtn = CreateMenuBtn();

            menuGrid.Children.Add(icon1, 0, 1, 0, 1);
            menuGrid.Children.Add(ansBtn, 0, 1, 0, 1);

            //Register answer button click event
            //When the answer button is clicked. answer image dialog will be displayed.
            ansBtn.Clicked += (sender, e) =>
            {
                // check whether level dialog is already exist or not.
                // if dialog is not exist. create new dialog.
                if (_ansDialog == null)
                {
                    _ansDialog = CreateAnswerDialog();
                    _ansDialog.Show();
                    _isAnsDialogDismiss = false;
                }
                else
                {
                    if (_isAnsDialogDismiss == true)
                    {
                        _ansDialog = CreateAnswerDialog();
                        _isAnsDialogDismiss = false;
                    }

                    _ansDialog.Show();
                }

            };

            // Level menu's Image.
            Image icon2 = new Image()
            {
                Source = "level.png",
                WidthRequest = 100,
                HeightRequest = 120
            };
            /// <summary>
            /// Level menu.
            /// </summary>
            Button levBtn = CreateMenuBtn();
            menuGrid.Children.Add(icon2, 1, 2, 0, 1);
            menuGrid.Children.Add(levBtn, 1, 2, 0, 1);

            //Register level button click event
            //When the level button is clicked. level selection dialog will be displayed.
            levBtn.Clicked += (sender, e) =>
            {
                // check whether level dialog is already exist or not.
                // if dialog is not exist. create new dialog.
                if (_levelDialog == null)
                {
                    _levelDialog = CreateLevelDialog();
                    _levelDialog.Show();
                    _islevelDialogDismiss = false;
                }
                else
                {
                    if (_islevelDialogDismiss == true)
                    {
                        _levelDialog = CreateLevelDialog();
                        _islevelDialogDismiss = false;
                    }

                    _levelDialog.Show();
                }

                _started = false;
            };

            // Refresh menu's Image.
            Image icon3 = new Image()
            {
                Source = "refresh.png",
                WidthRequest = 100,
                HeightRequest = 120
            };
            /// <summary>
            /// Refresh menu.
            /// </summary>
            Button refBtn = CreateMenuBtn();

            //Add this view in first low, third column.
            menuGrid.Children.Add(icon3, 2, 3, 0, 1);
            menuGrid.Children.Add(refBtn, 2, 3, 0, 1);

            //Register refresh button click event
            //When the refresh button is clicked. refresh puzzle by moving blocks
            refBtn.Clicked += (sender, e) =>
            {
                RefreshPuzzle();
            };

            return menuGrid;
        }

        /// <summary>
        /// Create Menu Button
        /// </summary>
        /// <returns>return a Button</returns>
        /// <seealso cref="Button">
        private Button CreateMenuBtn()
        {
            return new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.FillAndExpand,
                WidthRequest = 100,
                HeightRequest = 110,
                BackgroundColor = Color.Transparent
            };
        }

        /// <summary>
        /// Add image blocks in puzzle view
        /// </summary>
        /// <param name="grid">The container Grid layout</param>
        /// <seealso cref="Grid">
        private void CreatePuzzle(Grid grid)
        {
            int level = (int)_level;
            int imageSize = 225;
            int size = imageSize / level;
            string folder = "";
            //Clear _puzzleSquares firstly
            int row = 0;
            int col = 0;
            //Clear puzzle grid
            grid.Children.Clear();
            //Initialize PuzzleSquare
            for (row = 0; row < MAXNUM; row++)
            {
                for (col = 0; col < MAXNUM ; col++)
                {
                    _puzzleSquares[row, col] = null;
                }
            }

            //The blank block's position at the right bottom corner.
            _blankPos = new Position(level - 1, level - 1);

            PuzzleSquare square = null;
            if (level == 4)
            {
                folder = "4X4";
            }
            else
            {
                folder = "5X5";
            }

            for (row = 0; row < level; row++)
            {
                for (col = 0; col < level; col++)
                {
                    //Does not add Image in blank block.
                    if (row == _blankPos.Y && col == _blankPos.X)
                    {
                        break;
                    }

                    //Create new square and then add puzzle grid.
                    square = new PuzzleSquare()
                    {
                        Source = folder + "/" + (row + 1).ToString() + "-" + (col + 1).ToString() + ".jpg",
                        MinimumWidthRequest = (720 - 2 * 2 * level) / level,
                        MinimumHeightRequest = (720 - 2 * 2 * level) / level,
                        PositionX = col,
                        PositionY = row,
                        OriginalX = col,
                        OriginalY = row,
                    };

                    //Add click event handler
                    square.Clicked += (s, e) =>
                    {
                        //Extract press square from sender.
                        PuzzleSquare clickedSquare = (PuzzleSquare)s;
                        MoveBlock(clickedSquare, Direction.UNKNOW);
                    };

                    //Add square to _puzzleSquares array
                    _puzzleSquares[row, col] = square;

                    //Add created Image object to Grid
                    grid.Children.Add(square, col, col + 1, row, row + 1);
                }
            }
        }

        /// <summary>
        /// Create puzzle view in middle of main layout
        /// </summary>
        /// <returns>return a Grid layout</returns>
        /// <seealso cref="Grid">
        private Grid CreatePuzzleGrid()
        {
            /// <summary>
            /// Create grid view for puzzle.
            /// </summary>
            Grid grid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ColumnSpacing = 2,
                RowSpacing = 2,
                HeightRequest = 680,
            };

            CreatePuzzle(grid);

            return grid;
        }

        /// <summary>
        /// Create control button view at the bottom of main layout
        /// </summary>
        /// <returns>return a Grid layout</returns>
        /// <seealso cref="View">
        private View CreateControlBtnGrid()
        {
            /// <summary>
            /// Create grid view for control button.
            /// </summary>
            Grid grid = new Grid()
            {
                Padding = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ColumnSpacing = 20,
                RowSpacing = 20,
                HeightRequest = 200,
                BackgroundColor = Color.White,
            };

            /// <summary>
            /// Create left button
            /// </summary>
            Button btn = new Button()
            {
                Text = "LEFT",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            //register left button click event
            btn.Clicked += (sender, e) =>
            {
                //Check which block can be moved to left.
                if (_blankPos.X < (int)_level - 1)
                {
                    if (_puzzleSquares[_blankPos.Y, _blankPos.X + 1] != null)
                    {
                        MoveBlock(_puzzleSquares[_blankPos.Y, _blankPos.X + 1], Direction.LEFT);
                    }
                }
            };
            //Layout button to Grid's 0~2 column, 0~2 row.
            grid.Children.Add(btn, 0, 2, 0, 2);

            /// <summary>
            /// Create up button
            /// </summary>
            btn = new Button()
            {
                Text = "UP",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            //register up button click event
            btn.Clicked += (sender, e) =>
            {
                //Check which block can be moved to top.
                if (_blankPos.Y < (int)_level - 1)
                {
                    if (_puzzleSquares[_blankPos.Y + 1, _blankPos.X] != null)
                    {
                        MoveBlock(_puzzleSquares[_blankPos.Y + 1, _blankPos.X], Direction.UP);
                    }
                }
            };
            //Layout button to Grid's 2~6 column, 0~1 row.
            grid.Children.Add(btn, 2, 6, 0, 1);

            /// <summary>
            /// Create down button
            /// </summary>
            btn = new Button()
            {
                Text = "DOWN",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            //register down button click event
            btn.Clicked += (sender, e) =>
            {
                //Check which block can be moved down.
                if (_blankPos.Y > 0)
                {
                    if (_puzzleSquares[_blankPos.Y - 1, _blankPos.X] != null)
                    {
                        MoveBlock(_puzzleSquares[_blankPos.Y - 1, _blankPos.X], Direction.DOWN);
                    }
                }
            };
            //Layout button to Grid's 2~6 column, 1~2 row.
            grid.Children.Add(btn, 2, 6, 1, 2);

            /// <summary>
            /// Create right button
            /// </summary>
            btn = new Button()
            {
                Text = "RIGHT",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            //register down button click event
            btn.Clicked += (sender, e) =>
            {
                //Check which block can be moved to right.
                if (_blankPos.X > 0)
                {
                    if (_puzzleSquares[_blankPos.Y, _blankPos.X - 1] != null)
                    {
                        MoveBlock(_puzzleSquares[_blankPos.Y, _blankPos.X - 1], Direction.RIGHT);
                    }
                }
            };
            //Layout button to Grid's 6~8 column, 0~2 row.
            grid.Children.Add(btn, 6, 8, 0, 2);
            return grid;
        }

        /// <summary>
        /// Create answer dialog. if answer button clicked, will push this dialog to front.
        /// Only contain a whole answer image in the center.
        /// </summary>
        /// <returns>return a Dailog</returns>
        /// <seealso cref="Dialog">
        Dialog CreateAnswerDialog()
        {
            var dialog = new Dialog();
            dialog.Title = "Answer";

            var layout = new StackLayout
            {
                Children =
                {
                    /// <summary>
                    /// Create answer image
                    /// </summary>
                    new Image()
                    {
                        HeightRequest = 300,
                        WidthRequest = 300,
                        Source = "smile.jpg",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                    },
                }
            };
            var closeBtn = new Button()
            {
                Text = "Close",
            };

            //Register close button click event
            closeBtn.Clicked += (s1, e1) =>
            {
                dialog.Hide();
                _isAnsDialogDismiss = true;
            };

            //Set content of dialog
            dialog.Content = layout;
            //set button of dialog
            dialog.Positive = closeBtn;

            //Register dialog back button click event
            dialog.BackButtonPressed += (s1, e1) =>
            {
                dialog.Hide();
                _isAnsDialogDismiss = true;
            };

            return dialog;
        }

        /// <summary>
        /// Create finish dialog. if puzzle is finished,  will push this dialog to front.
        /// Only contain a image in the center.
        /// </summary>
        void DisplayFinishDialog()
        {
            var dialog = new Dialog();
            dialog.Title = "Correct !!!";

            var layout = new StackLayout
            {
                Children =
                {
                    /// <summary>
                    /// Create answer image
                    /// </summary>
                    new Image()
                    {
                        HeightRequest = 300,
                        WidthRequest = 300,
                        Source = "correct.jpg",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                    },
                }
            };
            var closeBtn = new Button()
            {
                Text = "Close",
            };

            //register close button click event
            closeBtn.Clicked += (s1, e1) =>
            {
                dialog.Hide();
                _isAnsDialogDismiss = true;
            };

            //set content of dialog
            dialog.Content = layout;
            //set button of dialog
            dialog.Positive = closeBtn;

            //register dialog back button click event
            dialog.BackButtonPressed += (s1, e1) =>
            {
                dialog.Hide();
                _isAnsDialogDismiss = true;
            };

            dialog.Show(); ;
        }

        /// <summary>
        /// Create level dialog. if level button clicked, will push this dialog to front.
        /// Only contain radio button list for level.
        /// </summary>
        /// <returns>return a Dailog</returns>
        /// <seealso cref="Dialog">
        Dialog CreateLevelDialog()
        {
            var dialog = new Dialog();
            dialog.Title = "Level";

            var radiobtn1 = CreateRadioButton("4 x 4 Puzzle");

            //register radio button selected event
            radiobtn1.CheckedChanged += (s, e) =>
            {
                if (radiobtn1.IsChecked)
                {
                    _level = PuzzleLevel.Level1;
                }
            };
            radiobtn1.GroupName = "Group1";

            var radiobtn2 = CreateRadioButton("5 x 5 Puzzle");
            //register radio button selected event
            radiobtn2.CheckedChanged += (s, e) =>
            {
                if (radiobtn2.IsChecked)
                {
                    _level = PuzzleLevel.Level2;
                }
            };
            radiobtn2.GroupName = "Group1";

            if (_level == PuzzleLevel.Level1)
            {
                radiobtn1.IsChecked = true;
            }
            else
            {
                radiobtn2.IsChecked = true;
            }

            var layout = new StackLayout
            {
                //add padding value for margin
                Padding = new Thickness(30, 20, 0, 0),
                Children =
                {
                    radiobtn1,
                    radiobtn2
                }
            };
            //create ok button
            var okBtn = new Button()
            {
                Text = "OK",
            };

            //register ok button click event
            // When ok button clicked, update puzzle following to level.
            okBtn.Clicked += (s1, e1) =>
            {
                dialog.Hide();
                _islevelDialogDismiss = true;
                //update puzzle following selected level
                CreatePuzzle(_puzzleGrid);
            };

            //create close button
            var closeBtn = new Button()
            {
                Text = "Close",
            };

            //register close button click event
            // When close button clicked, dismiss level dialog.
            closeBtn.Clicked += (s1, e1) =>
            {
                dialog.Hide();
                _islevelDialogDismiss = true;
            };
            //set content of dialog
            dialog.Content = layout;
            // set two button for dialog. positive and neutral button
            dialog.Positive = okBtn;
            dialog.Neutral = closeBtn;
            // When button clicked, dismiss dialog.
            dialog.BackButtonPressed += (s1, e1) =>
            {
                dialog.Hide();
                _islevelDialogDismiss = true;
            };

            return dialog;
        }

        /// <summary>
        /// Create Radio Button of Dialog pop-up.
        /// Radio Button is Tizen forms extension API
        /// </summary>
        /// <param name="name">The text of radio button.</param>
        /// <returns>return a RadioButton</returns>
        /// <seealso cref="RadioButton">
        RadioButton CreateRadioButton(string name)
        {
            return new RadioButton
            {
                Content = name,
                TextColor = Color.Black,
                FontSize = 20,
                HeightRequest = 80,
            };
        }

        /// <summary>
        /// Create UI components.
        /// </summary>
        void InitializeComponent()
        {
            Title = "Puzzle";

            // create menu layout
            _menuGrid = CreateMenuGrid();

            // create puzzle layout
            _puzzleGrid = CreatePuzzleGrid();

            //create control button layout
            _controlBtnGrid = CreateControlBtnGrid();

            //Create main container
            StackLayout mainLayout = new StackLayout()
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(5),
                BackgroundColor = Color.White,
                Children =
                {
                    _menuGrid,
                    _puzzleGrid,
                    _controlBtnGrid
                },
            };

            Content = mainLayout;
        }

    }
}
