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

namespace Puzzle
{
    class PuzzleSquare : RelativeLayout, IButtonController
    {
        public static readonly BindableProperty SourceProperty = BindableProperty.Create("Source", typeof(ImageSource), typeof(PuzzleSquare), default(ImageSource));
        public static readonly BindableProperty MinWidthProperty = BindableProperty.Create("MinWidth", typeof(double), typeof(PuzzleSquare), default(double));
        public static readonly BindableProperty MinHeightProperty = BindableProperty.Create("MInHeigth", typeof(double), typeof(PuzzleSquare), default(double));
        public static readonly BindableProperty PositionXProperty = BindableProperty.Create("PositionX", typeof(int), typeof(PuzzleSquare), default(int));
        public static readonly BindableProperty PositionYProperty = BindableProperty.Create("PositionY", typeof(int), typeof(PuzzleSquare), default(int));
        public static readonly BindableProperty OriginalXProperty = BindableProperty.Create("OriginalX", typeof(int), typeof(PuzzleSquare), default(int));
        public static readonly BindableProperty OriginalYProperty = BindableProperty.Create("OriginalY", typeof(int), typeof(PuzzleSquare), default(int));

        /// <summary>
        /// Initialize an image button layout
        /// Add LongTapGesture recognizer for displaying pressed color and invoking click event.
        /// </summary>
        public PuzzleSquare() : base()
        {
            var image = new Image
            {
                Source = Source,
            };
            image.BindingContext = this;
            image.SetBinding(Image.SourceProperty, new Binding("Source"));
            image.SetBinding(Image.MinimumWidthRequestProperty, new Binding("MinWidth"));
            image.SetBinding(Image.MinimumHeightRequestProperty, new Binding("MinHeight"));

            var gestureRecognizer = new LongTapGestureRecognizer();

            //When tap event is invoked. add pressed color to square image.
            gestureRecognizer.TapStarted += (s, e) =>
            {
                //change foreground blend color of image
                ImageAttributes.SetBlendColor(image, Color.FromRgb(213, 228, 240));
            };

            //If tap is released. set default color to square image.
            gestureRecognizer.TapCanceled += (s, e) =>
            {
                //revert foreground blend color of image
                ImageAttributes.SetBlendColor(image, Color.Default);
                //Invoke click event to consumer.
                SendClicked();
            };

            //Set default color to square image.
            gestureRecognizer.TapCompleted += (s, e) =>
            {
                //revert foreground blend color of image
                ImageAttributes.SetBlendColor(image, Color.Default);
                //Invoke click event to consumer.
                SendClicked();
            };

            GestureRecognizers.Add(gestureRecognizer);


            Children.Add(
                image,
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height;
                }));
        }

        /// <summary>
        /// An Source will be filled in the square
        /// </summary>
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// Minimum width request value of square image
        /// </summary>
        public double MinWidth
        {
            get { return (double)GetValue(MinWidthProperty); }
            set { SetValue(MinWidthProperty, value); }
        }

        /// <summary>
        /// Minimum height request value of square image
        /// </summary>
        public double MinHeight
        {
            get { return (double)GetValue(MinHeightProperty); }
            set { SetValue(MinHeightProperty, value); }
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

        /// <summary>
        /// To broadcast a click event to subscribers
        /// </summary>
        public void SendClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        public void SendPressed()
        {
        }

        public void SendReleased()
        {
        }

        public event EventHandler Clicked;
    }
}
