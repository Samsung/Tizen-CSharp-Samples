/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
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
 *
 */

using System;
using Tizen;
using System.Runtime.InteropServices;
using Tizen.NUI;
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

namespace UIControlSample
{
    /// <summary>
    /// A sample of Popup
    /// </summary>
    class PopupSample : IExample
    {
        private Popup popup;
        private PushButton popupButton;
        private TextLabel guide;
        private TextLabel userGuide;
        // <summary>
        /// Constructor to create new PopupSample
        /// </summary>
        public PopupSample()
        {
        }

        /// <summary>
        /// Popup initialisation.
        /// </summary>
        private void Initialize()
        {
            Window.Instance.BackgroundColor = Color.Black;

            guide = new TextLabel();
            guide.HorizontalAlignment = HorizontalAlignment.Center;
            guide.VerticalAlignment = VerticalAlignment.Center;
            guide.PositionUsesPivotPoint = true;
            guide.ParentOrigin = ParentOrigin.TopLeft;
            guide.PivotPoint = PivotPoint.TopLeft;
            guide.Size2D = new Size2D(1920, 96);
            guide.FontFamily = "Samsung One 600";
            guide.Position2D = new Position2D(0, 94);
            guide.MultiLine = false;
            //guide.PointSize = 15.0f;
            guide.PointSize = DeviceCheck.PointSize15;
            guide.Text = "Popup Sample \n";
            guide.TextColor = Color.White;
            //guide.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            Window.Instance.GetDefaultLayer().Add(guide);

            userGuide = new TextLabel();
            userGuide.HorizontalAlignment = HorizontalAlignment.Begin;
            userGuide.VerticalAlignment = VerticalAlignment.Top;
            userGuide.PositionUsesPivotPoint = true;
            userGuide.ParentOrigin = ParentOrigin.TopLeft;
            userGuide.PivotPoint = PivotPoint.TopLeft;
            userGuide.FontFamily = "Samsung One 400";
            userGuide.Position = new Position(200, 200, 0);
            userGuide.MultiLine = true;
            //userGuide.PointSize = 10.0f;
            userGuide.PointSize = DeviceCheck.PointSize10;
            userGuide.Text = "Press Enter key to show the popup.\n";
            userGuide.TextColor = Color.White;
            Window.Instance.GetDefaultLayer().Add(userGuide);

            Popups popupSample = new Popups();
            popup = popupSample.GetPopup();
            popup.Shown += (obj, e) =>
            {
                Tizen.Log.Fatal("UISample", "Ungrab key: ");
                bool flag = Window.Instance.UngrabKey(166);
                Tizen.Log.Fatal("UISample", "Ungrab key: " + flag);
            };
            popup.Hidden += (obj, e) =>
            {
                Tizen.Log.Fatal("UISample", "Grab key: ");
                Window.Instance.GrabKey(166, Window.KeyGrabMode.Topmost);
            };
            Window.Instance.GetDefaultLayer().Add(popup);

            Button pushSample = new Button("PopupButton");
            popupButton = pushSample.GetPushButton();
            popupButton.Position = new Position(810, 500, 0); //300, 80
            Window.Instance.GetDefaultLayer().Add(popupButton);
            popupButton.Released += (obj, e) =>
            {
                popup.SetDisplayState(Popup.DisplayStateType.Shown);
                FocusManager.Instance.SetCurrentFocusView(popup.FindChildByName("OKButton"));
                return true;
            };

            FocusManager.Instance.SetCurrentFocusView(popupButton);
        }

        /// <summary>
        /// Dispose popup
        /// </summary>
        public void Deactivate()
        {
            Window.Instance.GetDefaultLayer().Remove(guide);
            guide.Dispose();
            guide = null;

            Window.Instance.GetDefaultLayer().Remove(userGuide);
            userGuide.Dispose();
            userGuide = null;

            Window.Instance.GetDefaultLayer().Remove(popupButton);
            popupButton.Dispose();
            popupButton = null;
        }

        /// <summary>
        /// Activate this class.
        /// </summary>
        public void Activate()
        {
            Initialize();
        }
    }
}