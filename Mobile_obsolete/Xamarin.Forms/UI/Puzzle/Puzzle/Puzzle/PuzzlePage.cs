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
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Puzzle
{
    /// <summary>
    /// The block's position information.
    /// </summary>
    class Position
    {
        public int X;
        public int Y;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    /// <summary>
    /// Puzzle app's Main page, logic part.
    /// </summary>
    public partial class PuzzlePage : ContentPage
    {
        // Maximum number of squares horizontally and vertically,
        static readonly int MAXNUM = 5;
        //Refresh factor, moving times to refresh the puzzle.
        private const int REFRESH_FACTOR = 50;
        private PuzzleLevel _level = PuzzleLevel.Level1;
        //PuzzleSquare initialize
        private PuzzleSquare[,] _puzzleSquares =  new PuzzleSquare[MAXNUM, MAXNUM];
        //blank position
        private Position _blankPos;

        private bool _started = false;
        private bool _isBusy = false;

        enum PuzzleLevel
        {
            Level1 = 4, //level1: 4X4
            Level2 = 5, //level2: 5X5
        }

        //The move direction of puzzle block
        enum Direction
        {
            LEFT,
            UP,
            RIGHT,
            DOWN,
            UNKNOW,
        }

        /// <summary>
        /// Move one square to a specified direction.
        /// If the direct == Direction.UNKNOW, it will choose an available direction automatically.
        /// </summary>
        /// <param name="square">Puzzle square object</param>
        /// <param name="direct">The direction will move to</param>
        private void MoveBlock(PuzzleSquare square, Direction direct)
        {
            //Get square's current position.
            Position pos = new Position(square.PositionX, square.PositionY);
            if (direct == Direction.UNKNOW)
            {
                if (pos.X + 1 == _blankPos.X && pos.Y == _blankPos.Y)
                {
                    direct = Direction.RIGHT;
                }
                else if (pos.X - 1 == _blankPos.X && pos.Y == _blankPos.Y)
                {
                    direct = Direction.LEFT;
                }
                else if (pos.X == _blankPos.X && pos.Y + 1 == _blankPos.Y)
                {
                    direct = Direction.DOWN;
                }
                else if (pos.X == _blankPos.X && pos.Y - 1 == _blankPos.Y)
                {
                    direct = Direction.UP;
                }
                else
                {
                    return;
                }
            }

            //Remove old image object from Grid.
            _puzzleGrid.Children.Remove(square);
            switch (direct)
            {
                case Direction.UP:
                    pos.Y--;
                    _blankPos.Y++;
                    break;
                case Direction.DOWN:
                    pos.Y++;
                    _blankPos.Y--;
                    break;
                case Direction.LEFT:
                    pos.X--;
                    _blankPos.X++;
                    break;
                case Direction.RIGHT:
                    pos.X++;
                    _blankPos.X--;
                    break;
            }

            _puzzleGrid.Children.Add(square, pos.X, pos.X + 1, pos.Y, pos.Y + 1);

            //replace blank to moved square.
            _puzzleSquares[_blankPos.Y, _blankPos.X] = null;
            _puzzleSquares[pos.Y, pos.X] = square;
            square.PositionX = pos.X;  //store current x position
            square.PositionY = pos.Y;  //store current y position

            if (_started)
            {
                //after block is moved, checking if puzzle is completed.
                //if puzzle is complete. display finish dialog.
                if (_blankPos.X == ((int)_level - 1) && _blankPos.Y == ((int)_level - 1))
                {
                    CheckFinished();
                }
            }
        }

        /// <summary>
        /// Check puzzle is finished.
        /// </summary>
        /// <returns>A Task to check finish of puzzle asynchrously</returns>
        Task CheckFinished()
        {
            return Task.Run(() =>
            {
                bool finish = true;
                //check all puzzle square has original position
                for (int row = 0; row < (int)_level; row++)
                {
                    for (int col = 0; col < (int)_level; col++)
                    {
                        PuzzleSquare square = _puzzleSquares[row, col];
                        if (square != null)
                        {
                            if (square.OriginalX != square.PositionX || square.OriginalY != square.PositionY)
                            {
                                //Puzzle is not completed.
                                finish = false;
                                break;
                            }
                        }
                    }

                    if (!finish)
                    {
                        break;
                    }
                }

                //if all puzzle is match. create finish popup.
                if (finish)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayFinishDialog();
                    });
                    _started = false;
                }
            });
        }

        /// <summary>
        /// Refresh squares of puzzle.
        /// Move squares to random direction.
        /// </summary>
        private async void RefreshPuzzle()
        {
            //Refreshing puzzle is progressing
            if (_isBusy)
            {
                return;
            }

            _isBusy = true;
            Random ran = new Random();
            //Refresh puzzle by moving blocks at random in REFRESH_FACTOR times.
            for (int i = 0; i < REFRESH_FACTOR; i++)
            {
                Direction direct = (Direction)(ran.Next(0, 1000) % 4);
                int directX, directY;
                switch (direct)
                {
                    case Direction.LEFT:
                        directX = 1;
                        directY = 0;
                        break;
                    case Direction.RIGHT:
                        directX = -1;
                        directY = 0;
                        break;
                    case Direction.UP:
                        directX = 0;
                        directY = 1;
                        break;
                    case Direction.DOWN:
                        directX = 0;
                        directY = -1;
                        break;
                    default:
                        directX = 0;
                        directY = 1;
                        break;

                }

                //checking move direction is in the puzzle area.
                if (_blankPos.X + directX < (int)_level && _blankPos.X + directX >= 0 &&
                _blankPos.Y + directY < (int)_level && _blankPos.Y + directY >= 0)
                {
                    if (_puzzleSquares[_blankPos.Y + directY, _blankPos.X + directX] != null)
                    {
                        MoveBlock(_puzzleSquares[_blankPos.Y + directY, _blankPos.X + directX], direct);
                    }
                }

                await RefreshAnimationEffect();
            }

            _started = true;
            _isBusy = false;
        }

        /// <summary>
        /// Add thread sleep for Animation effect when refresh puzzle
        /// </summary>
        /// <returns> Task return type </returns>
        private Task RefreshAnimationEffect()
        {
            return Task.Run(() =>
            {
               Thread.Sleep(5);
            });
        }

        /// <summary>
        /// The constructor of PuzzlePage class
        /// </summary>
        public PuzzlePage() : base()
        {
            Title = "Puzzle";
            InitializeComponent();
        }
    }
}

