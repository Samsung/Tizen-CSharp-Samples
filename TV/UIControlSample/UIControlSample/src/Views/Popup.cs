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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.NUI;
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

namespace UIControlSample
{
    /// <summary>
    /// A sample of popup.
    /// </summary>
    public class Popups
    {
        Tizen.NUI.UIComponents.Popup _popup;
        PushButton cancelButton, okayButton;
        private const string resources = "/home/owner/apps_rw/org.tizen.example.UIControlSample/res";
        private string popupBGImage = resources + "/images/Popup/bg_popup_220_220_220_100.9.png";
        private string barImage = resources + "/images/Popup/img_popup_bar_line.png";
        private string shadowImage = resources + "/images/Popup/img_popup_bar_shadow.png";

        /// <summary>
        /// Constructor to create new Popups
        /// </summary>
        public Popups()
        {
            OnIntialize();
        }

        /// <summary>
        /// Popups initialisation.
        /// </summary>
        private void OnIntialize()
        {
            _popup = CreatePopup();
            _popup.SetContent(CreateContent());
            _popup.Focusable = (true);
            _popup.SetDisplayState(Tizen.NUI.UIComponents.Popup.DisplayStateType.Hidden);
        }

        /// <summary>
        /// Get the initialized _popup
        /// </summary>
        /// <returns>
        /// The _popup which be created in this class
        /// </returns>
        public Tizen.NUI.UIComponents.Popup GetPopup()
        {
            return _popup;
        }

        /// <summary>
        /// Create popup
        /// </summary>
        /// <returns>
        /// The _popup which be created in this function
        /// </returns>
        Tizen.NUI.UIComponents.Popup CreatePopup()
        {
            Tizen.NUI.UIComponents.Popup confirmationPopup = new Tizen.NUI.UIComponents.Popup();

            confirmationPopup.ParentOrigin = ParentOrigin.TopLeft;
            confirmationPopup.PivotPoint = PivotPoint.TopLeft;
            confirmationPopup.WidthResizePolicy = ResizePolicyType.FitToChildren;
            confirmationPopup.HeightResizePolicy = ResizePolicyType.FitToChildren;

            confirmationPopup.Position = new Position(530, 306, 0);
            confirmationPopup.PopupBackgroundImage = popupBGImage;
            return confirmationPopup;
        }

        /// <summary>
        /// Create the view be used to set popup.content.
        /// </summary>
        /// <returns>
        /// The created contentView.
        /// </returns>
        View CreateContent()
        {
            View contentView = new View();
            contentView.Size2D = new Size2D(860, 486);
            contentView.ParentOrigin = ParentOrigin.TopLeft;
            contentView.PivotPoint = PivotPoint.TopLeft;
            contentView.Position = new Position(0, 0, 0);

            TextLabel titleView = new TextLabel("Information");
            titleView.FontFamily = "Samsung One 600";
            titleView.TextColor = Color.Black;
            titleView.ParentOrigin = ParentOrigin.TopLeft;
            titleView.PivotPoint = PivotPoint.TopLeft;
            titleView.Position = new Position(70, 65, 0);
            titleView.Size2D = new Size2D(720, 70);
            titleView.MultiLine = false;
            titleView.PointSize = 10;
            titleView.HorizontalAlignment = HorizontalAlignment.Center;

            ImageView imageView = new ImageView(barImage);
            imageView.ParentOrigin = ParentOrigin.TopLeft;
            imageView.PivotPoint = PivotPoint.TopLeft;
            imageView.Position = new Position(70, 143, 0);
            imageView.Size2D = new Size2D(720, 3);

            ImageView shadow = new ImageView(shadowImage);
            shadow.ParentOrigin = ParentOrigin.TopLeft;
            shadow.PivotPoint = PivotPoint.TopLeft;
            shadow.Position = new Position(70, 146, 0);
            shadow.Size2D = new Size2D(720, 9);


            TextLabel contentLabel = new TextLabel("Do you want to erase this file Permanently?");
            contentLabel.FontFamily = "Samsung One 400";
            contentLabel.TextColor = Color.Black;
            contentLabel.ParentOrigin = ParentOrigin.TopLeft;
            contentLabel.PivotPoint = PivotPoint.TopLeft;
            contentLabel.Position = new Position(70, 204, 0);
            contentLabel.Size2D = new Size2D(720, 120);
            contentLabel.MultiLine = true;
            contentLabel.PointSize = 8;
            contentLabel.HorizontalAlignment = HorizontalAlignment.Center;

            PushButton okButton = CreateOKButton();
            okButton.Position = new Position(120, 316, 0); //300, 80

            PushButton cancelButton = CreateCancelButton();
            cancelButton.Position = new Position(440, 316, 0); //300, 80

            contentView.Add(titleView);
            contentView.Add(imageView);
            contentView.Add(shadow);
            contentView.Add(contentLabel);
            contentView.Add(okButton);
            contentView.Add(cancelButton);
            return contentView;
        }

        /// <summary>
        /// Create the okButton
        /// </summary>
        /// <returns>
        /// The created okButton.
        /// </returns>
        PushButton CreateOKButton()
        {
            Button okSample = new Button("OK");
            okayButton = okSample.GetPushButton();
            okayButton.Name = "OKButton";

            // Hide popup.
            okayButton.Clicked += (obj, ee) =>
            {
                _popup.SetDisplayState(Tizen.NUI.UIComponents.Popup.DisplayStateType.Hidden);
                return false;
            };

            // Move focus to cancelButton.
            okayButton.KeyEvent += (obj, e) =>
            {
                if (e.Key.KeyPressedName == "Right" && Key.StateType.Down == e.Key.State)
                {
                    FocusManager.Instance.SetCurrentFocusView(cancelButton);
                }

                return false;
            };
            return okayButton;
        }

        /// <summary>
        /// Create the okButton
        /// </summary>
        /// <returns>
        /// The created okButton.
        /// </returns>
        PushButton CreateCancelButton()
        {
            Button cancelSample = new Button("Cancel");
            cancelButton = cancelSample.GetPushButton();

            // Hide popup.
            cancelButton.Clicked += (obj, ee) =>
            {
                _popup.SetDisplayState(Tizen.NUI.UIComponents.Popup.DisplayStateType.Hidden);
                return false;
            };

            // Move focus to okayButton.
            cancelButton.KeyEvent += (obj, e) =>
            {
                if (e.Key.KeyPressedName == "Left" && Key.StateType.Down == e.Key.State)
                {
                    FocusManager.Instance.SetCurrentFocusView(okayButton);
                }

                return false;
            };

            return cancelButton;
        }
    }
}
