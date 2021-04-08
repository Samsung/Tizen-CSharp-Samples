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

namespace AppCommon.Extensions
{
    /// <summary>
    /// A class for an image enable to get click events.
    /// It can use as a button.
    /// </summary>
    public class ImageButton : Image, IButtonController
    {
        /// <summary>
        /// The constructor for an image button
        /// </summary>
        public ImageButton() : base()
        {
            var gestureRecognizer = new LongTapGestureRecognizer();
            gestureRecognizer.TapStarted += (s, e) =>
            {
                //change foreground blend color of image
                ImageAttributes.SetBlendColor(this, Color.FromRgb(213, 228, 240));
            };

            gestureRecognizer.TapCanceled += (s, e) =>
            {
                //revert foreground blend color of image
                ImageAttributes.SetBlendColor(this, Color.Default);
            };

            gestureRecognizer.TapCompleted += (s, e) =>
            {
                //revert foreground blend color of image
                ImageAttributes.SetBlendColor(this, Color.Default);
            };
            GestureRecognizers.Add(gestureRecognizer);
            this.Effects.Add(Effect.Resolve("Tizen.ImageClickEffect"));
        }

        /// <summary>
        /// To broadcast a click event to subscribers
        /// </summary>
        public void SendClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// To broadcast a press event to subscribers
        /// </summary>
        public void SendPressed()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// To broadcast a release event to subscribers
        /// </summary>
        public void SendReleased()
        {
            throw new NotImplementedException();
        }

        public event EventHandler Clicked;
    }
}