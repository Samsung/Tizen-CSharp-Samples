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
using System;
using Puzzle.Extensions;
using System.Threading;

namespace Puzzle
{
    public class PuzzleSquare : CustomImageButton
    {
        public static readonly BindableProperty PositionXProperty = BindableProperty.Create("PositionX", typeof(int), typeof(PuzzleSquare), default(int));
        public static readonly BindableProperty PositionYProperty = BindableProperty.Create("PositionY", typeof(int), typeof(PuzzleSquare), default(int));
        public static readonly BindableProperty OriginalXProperty = BindableProperty.Create("OriginalX", typeof(int), typeof(PuzzleSquare), default(int));
        public static readonly BindableProperty OriginalYProperty = BindableProperty.Create("OriginalY", typeof(int), typeof(PuzzleSquare), default(int));

        /// <summary>
        /// Initialize an PuzzleSquare
        /// </summary>
        public PuzzleSquare() : base()
        {
        }

        /// <summary>
        /// Current square's horizental position in Puzzle
        /// </summary>
        public int PositionX
        {
            get { return (int)GetValue(PositionXProperty); }
            set { SetValue(PositionXProperty, value); }
        }

        /// <summary>
        /// Current square's vertical position in Puzzle
        /// </summary>
        public int PositionY
        {
            get { return (int)GetValue(PositionYProperty); }
            set { SetValue(PositionYProperty, value); }
        }

        /// <summary>
        /// Original x position of image button in Puzzle
        /// This value is need to compare
        /// </summary>
        public int OriginalX
        {
            get { return (int)GetValue(OriginalXProperty); }
            set { SetValue(OriginalXProperty, value); }
        }

        /// <summary>
        /// Original y position of image button in Puzzle
        /// </summary>
        public int OriginalY
        {
            get { return (int)GetValue(OriginalYProperty); }
            set { SetValue(OriginalYProperty, value); }
        }
    }
}
