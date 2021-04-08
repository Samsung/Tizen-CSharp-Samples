/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using ElmSharp;
using Tizen.Applications;

namespace AnalogWatch.Tizen.Wearable.Views
{
    /// <summary>
    /// Class displaying a badge object that is created from an image, bg and label widgets.
    /// </summary>
    internal class BadgeView
    {
        #region fields

        /// <summary>
        /// The parent window.
        /// </summary>
        private readonly Window _win;

        /// <summary>
        /// The badge's icon.
        /// </summary>
        private Image _icon;

        /// <summary>
        /// The badge's background.
        /// </summary>
        private Image _badgeBg;

        /// <summary>
        /// Label displaying the badge counter.
        /// </summary>
        private Label _counter;

        /// <summary>
        /// The badge label's text format.
        /// </summary>
        private String _counterFormat = "<font_size=20 align=center>{0}</font_size>";

        #endregion fields

        #region methods

        /// <summary>
        /// Creates an image.
        /// </summary>
        /// <param name="geometry">Image position and size.</param>
        /// <param name="imageFile">Image file name.</param>
        /// <returns>The created image.</returns>
        private Image _createImage(Rect geometry, String imageFile)
        {
            Image img = new Image(_win)
            {
                Geometry = geometry,
            };

            img.Load(Application.Current.DirectoryInfo.Resource + imageFile);
            img.Show();
            return img;
        }

        /// <summary>
        /// Class constructor. The method is used to initialize the displayed widgets.
        /// </summary>
        /// <param name="win">The application's main window.</param>
        /// <param name="icon">The icon to be displayed.</param>
        /// <param name="position">The icon's position on screen.</param>
        public BadgeView(Window win, string icon, Point position)
        {
            Rect counterBgPosition = new Rect(position.X + 45, position.Y - 12, 35, 35);
            Rect counterLabelPosition = new Rect(position.X + 45, position.Y - 8, 35, 35);

            this._win = win;
            _icon = _createImage(new Rect(position.X, position.Y, 65, 65), icon);
            _badgeBg = _createImage(counterBgPosition, "badge.png");

            _counter = new Label(_win)
            {
                Text = String.Format(_counterFormat, 0),
                Geometry = counterLabelPosition,
                AlignmentX = 0.5,
                AlignmentY = 0.5,
            };

            _counter.Show();
        }

        /// <summary>
        /// Updates the displayed counter.
        /// </summary>
        /// <param name="counter">New value to display.</param>
        public void SetCounter(int counter)
        {
            _counter.Text = counter > 99 ?
                String.Format(_counterFormat, "99+") : String.Format(_counterFormat, counter);
        }

        #endregion methods
    }
}