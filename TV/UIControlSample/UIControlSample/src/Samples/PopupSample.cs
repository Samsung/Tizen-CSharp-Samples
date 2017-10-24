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
            guide = new TextLabel();
            guide.HorizontalAlignment = HorizontalAlignment.Begin;
            guide.VerticalAlignment = VerticalAlignment.Center;
            guide.PositionUsesPivotPoint = true;
            guide.ParentOrigin = ParentOrigin.TopLeft;
            guide.PivotPoint = PivotPoint.TopLeft;
            guide.Position = new Position(580, 200, 0);
            guide.MultiLine = true;
            guide.PointSize = 15.0f;
            guide.Text = "Popup Sample Guide\n" +
                            "1.Press Enter key to show the popup.\n";
            guide.TextColor = Color.White;
            Window.Instance.GetDefaultLayer().Add(guide);

            Popups popupSample = new Popups();
            popup = popupSample.GetPopup();
            Window.Instance.GetDefaultLayer().Add(popup);

            Button pushSample = new Button("PopupButton");
            popupButton = pushSample.GetPushButton();
            popupButton.Position = new Position(800, 550, 0); //300, 80
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