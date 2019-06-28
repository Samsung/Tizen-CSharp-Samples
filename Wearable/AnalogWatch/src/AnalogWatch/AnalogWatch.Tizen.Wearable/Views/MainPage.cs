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
using System.Data;
using System.Reflection.Metadata;
using AnalogWatch.Tizen.Wearable.Controller;
using ElmSharp;
using Tizen.Applications;
using Window = ElmSharp.Window;

namespace AnalogWatch.Tizen.Wearable.Views
{
    /// <summary>
    /// Class describing the main view.
    /// </summary>
    public class MainPage
    {
        #region fields

        /// <summary>
        /// Application's main window.
        /// </summary>
        private readonly Window _win;

        /// <summary>
        /// Application's background image.
        /// </summary>
        private readonly Image _bgImage;

        /// <summary>
        /// Hour hand image.
        /// </summary>
        private readonly Image _hourImage;

        /// <summary>
        /// Minute hand image.
        /// </summary>
        private readonly Image _minute;

        /// <summary>
        /// Second hand image.
        /// </summary>
        private readonly Image _second;

        /// <summary>
        /// Visibility of the second hand image.
        /// </summary>
        private bool _secondsVisibility = true;

        /// <summary>
        /// Badge object displaying missed calls.
        /// </summary>
        private BadgeView _callBadge;

        /// <summary>
        /// Format of the displayed time.
        /// </summary>
        private const String FORMAT_WITH_SECONDS = "<align=0.5 font_size=96>{0:00}:{1:00}:{2:00}</align>";

        /// <summary>
        /// Format of the displayed time without the seconds field.
        /// </summary>
        private const String FORMAT_NO_SECONDS = "<align=0.5 font_size=96>{0:00}:{1:00}</align>";

        /// <summary>
        /// Format of the displayed date.
        /// </summary>
        private const string FORMAT_DATE = "<align=0.5 font_size=48>{0:00}.{1:00}.{2:0000}</align>";

        #endregion fields

        #region methods

        /// <summary>
        /// Method used to initialize and display an image widget.
        /// </summary>
        /// <param name="path">The path to the image file.</param>
        /// <param name="geometry">The initial geometry of the widget.</param>
        /// <returns>The created image object.</returns>
        private Image _createImage(String path, Rect geometry)
        {
            string dataDir = Application.Current.DirectoryInfo.Resource;

            Image img = new Image(this._win)
            {
                Geometry = geometry,
            };

            bool isLoaded = img.Load(dataDir + path);

            if (isLoaded == false)
            {
                throw new DataException("Failed to load file: " + path);
            }

            img.Show();
            return img;
        }

        /// <summary>
        /// Method used to display the application's background.
        /// </summary>
        private void _createBackground()
        {
            Background bg = new Background(_win)
            {
                Geometry = new Rect(0, 0, 360, 360),
                File = Application.Current.DirectoryInfo.Resource + "cipher_board_bg.png",
            };

            bg.Show();
        }

        /// <summary>
        /// Changes the rotation of the given hand image.
        /// </summary>
        /// <param name="img">The image to be modified.</param>
        /// <param name="time">The time value used to calculate the angle.</param>
        /// <param name="maxTime">The maximal value the <code>time</code>
        /// parameter (12 for hours, 60 for minutes and seconds).</param>
        private void _updateHand(Image img, int time, int maxTime)
        {
            double rotation = time / (float)maxTime * 360.0;

            EvasMap map = new EvasMap(4);
            map.PopulatePoints(img, 0);
            map.Rotate(rotation, 180, 180);
            img.EvasMap = map;
            img.IsMapEnabled = true;
        }

        /// <summary>
        /// The class constructor.
        /// The displayed widget geometries are set here.
        /// </summary>
        /// <param name="win">The application's main window.</param>
        public MainPage(Window win)
        {
            BadgeController badgeController = new Controller.BadgeController();
            this._win = win;
            Point point = new Point
            {
                Y = 208,
                X = 147
            };

            try
            {
                _createBackground();
                _callBadge = new BadgeView(_win, "icon_missed_calls.png", point);

                _createImage(@"hands_center.png", new Rect(360 / 2 - 9, 360 / 2 - 9, 18, 18));
                _hourImage = _createImage(@"hand_hour.png", new Rect(360 / 2 - 7, 360 / 2 - 83, 12, 83));
                _minute = _createImage(@"hand_minute.png", new Rect(360 / 2 - 5, 360 / 2 - 105, 12, 105));
                _second = _createImage(@"hand_second.png", new Rect(360 / 2 - 8, 65, 18, 9));
            }
            catch (Exception e)
            {
                global::Tizen.Log.Error(((Program)Application.Current).LogTag,
                    "Failed to load image. Exception: " + e.Message);
            }

            badgeController.BadgeStatusChanged += _onBadgeStatusChanged;
        }

        /// <summary>
        /// Handles the 'BadgeStatusChanged' BadgeController event.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="badgeEventArgs">Object containing the badge data.</param>
        private void _onBadgeStatusChanged(object sender, BadgeEventArgs badgeEventArgs)
        {
            if (badgeEventArgs.Badge.AppId == "org.tizen.w-phone")
            {
                _callBadge.SetCounter(badgeEventArgs.Badge.Count);
            }
        }

        /// <summary>
        /// Updates the displayed time.
        /// </summary>
        /// <param name="year">Current year.</param>
        /// <param name="month">Current month.</param>
        /// <param name="day">Current day.</param>
        /// <param name="hour">Current hour.</param>
        /// <param name="minute">Current minute.</param>
        /// <param name="second">Current second.</param>
        public void SetTime(int year, int month, int day, int hour, int minute, int second)
        {
            _updateHand(_hourImage, hour, 12);
            _updateHand(_minute, minute, 60);

            if (_second.IsVisible)
            {
                _updateHand(_second, second, 60);
            }
        }

        /// <summary>
        /// Sets the visibility of the seconds hand. Note that in the ambient mode the seconds hand is invisible.
        /// </summary>
        /// <param name="isVisible">True - the seconds hand will be visible.
        /// False - the seconds hand will be hidden.</param>
        public void SetSecondsVisibility(bool isVisible)
        {
            if (isVisible)
            {
                _second.Show();
            }
            else
            {
                _second.Hide();
            }
        }

        #endregion methods
    }
}
