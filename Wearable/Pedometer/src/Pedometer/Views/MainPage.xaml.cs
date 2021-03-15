/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd. All rights reserved.
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
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace Pedometer.Views
{
    /// <summary>
    /// Main page of the application
    /// </summary>
    public partial class MainPage : CirclePage
    {
        private const double SingleRotation = 46.2;
        private const int ScreenWidth = 200;
        private const int RotationTime = 450;
        private const int InitialPositionX = -118;
        private const int InitialPositionY = 75;

        private int _currentScreen = 1;
        private DateTime _lastMoveTime = DateTime.Now;
        private DateTime _currentMoveTime;

        /// <summary>
        /// Initializes MainPage class instance
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            ElmSharp.Wearable.RotaryEventManager.Rotated += RotaryEventManager_Rotated;
            GetLabelImage(0).FadeTo(0);
            GetLabelImage(2).FadeTo(0);
            GetLabelImage(3).FadeTo(0);
        }

        /// <summary>
        /// Switches view to selected screen
        /// </summary>
        /// <param name="screen">Screen's index</param>
        public void SwitchScreen(int screen)
        {
            if (_currentScreen == screen)
            {
                return;
            }

            GetLabelImage(_currentScreen).FadeTo(0);
            _currentScreen = screen;
            GetLabelImage(_currentScreen).FadeTo(1);

            double rotation = SingleRotation;
            int positionX = InitialPositionX;

            switch (screen)
            {
                case 0:
                    rotation -= SingleRotation;
                    positionX += ScreenWidth;
                    break;
                case 1:
                    break;
                case 2:
                    rotation += SingleRotation;
                    positionX -= ScreenWidth;
                    break;
                case 3:
                    rotation += SingleRotation * 2 + 0.5;
                    positionX -= ScreenWidth * 2;
                    break;
            }

            uint length = (uint)(Math.Abs(blueShape.Rotation - rotation) / SingleRotation * RotationTime);
            blueShape.RotateTo(rotation, length, Easing.SpringOut);
            grid.LayoutTo(new Rectangle(positionX, InitialPositionY, ScreenWidth * 4, ScreenWidth), (uint)(length * 0.6), Easing.SinInOut);
        }

        /// <summary>
        /// Returns image of particular label
        /// </summary>
        /// <param name="number">Label's index</param>
        /// <returns>Specified image</returns>
        private Image GetLabelImage(int number)
        {
            switch (number)
            {
                case 0:
                    return label0;
                case 1:
                    return label1;
                case 2:
                    return label2;
                case 3:
                    return label3;
            }

            return null;
        }

        /// <summary>
        /// Handles execution of Rotated event
        /// </summary>
        /// <param name="args">RotaryEvent Args</param>
        private void RotaryEventManager_Rotated(ElmSharp.Wearable.RotaryEventArgs args)
        {
            _currentMoveTime = DateTime.Now;
            var span = _currentMoveTime - _lastMoveTime;
            _lastMoveTime = _currentMoveTime;
            if (span.TotalMilliseconds <= 4)
            {
                return;
            }

            if (args.IsClockwise)
            {
                if (_currentScreen == 3)
                {
                    return;
                }

                SwitchScreen(_currentScreen + 1);
            }
            else
            {
                if (_currentScreen == 0)
                {
                    return;
                }

                SwitchScreen(_currentScreen - 1);
            }
        }
    }
}