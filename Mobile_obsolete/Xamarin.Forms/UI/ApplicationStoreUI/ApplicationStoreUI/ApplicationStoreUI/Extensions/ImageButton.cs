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
using Image = Xamarin.Forms.Image;
using System;
using System.Diagnostics;

namespace ApplicationStoreUI.Extensions
{
    class ImageButton : RelativeLayout, IButtonController
    {
        public static readonly BindableProperty SourceProperty = BindableProperty.Create("Source", typeof(ImageSource), typeof(ImageButton), default(ImageSource));
        public static readonly BindableProperty MinWidthProperty = BindableProperty.Create("MinWidth", typeof(double), typeof(ImageButton), default(double));
        public static readonly BindableProperty MinHeightProperty = BindableProperty.Create("MInHeigth", typeof(double), typeof(ImageButton), default(double));

        /// <summary>
        /// Initialize an image button layout
        /// Add LongTapGesture recognizer for displaying pressed color and invoke click event.
        /// </summary>
        public ImageButton() : base()
        {
            var image = new Image()
            {
                Source = Source,
            };
            image.BindingContext = this;
            image.SetBinding(Image.SourceProperty, new Binding("Source"));
            image.SetBinding(Image.MinimumWidthRequestProperty, new Binding("MinWidth"));
            image.SetBinding(Image.MinimumHeightRequestProperty, new Binding("MinHeight"));

            var gestureRecognizer = new LongTapGestureRecognizer();
            //When tap event is invoked. add pressed color to image.
            gestureRecognizer.TapStarted += (s, e) =>
            {
                //change forground blend color of image
                ImageAttributes.SetBlendColor(image, Color.FromRgb(213, 228, 240));
            };

            //If tap is released. set default color to image.
            gestureRecognizer.TapCanceled += (s, e) =>
            {
                //revert forground blend color of image
                ImageAttributes.SetBlendColor(image, Color.Default);
                SendClicked();
            };

            //If tap is completed. set default color to image.
            gestureRecognizer.TapCompleted += (s, e) =>
            {
                //revert forground blend color of image
                ImageAttributes.SetBlendColor(image, Color.Default);
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
        /// To broadcast a click event to subscribers
        /// </summary>
        public void SendClicked()
        {
            Debug.WriteLine("Click event invoked!");
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        public void SendReleased()
        {
        }

        public void SendPressed()
        {
        }

        public event EventHandler Clicked;
    }
}
