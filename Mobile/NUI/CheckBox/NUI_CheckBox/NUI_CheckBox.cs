/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace NUI_CheckBox
{
    /// <summary>
    /// Custom style of the checkbox
    /// </summary>
    internal class CustomCheckBoxStyle : StyleBase
    {
        string ImageURL = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/";
        private Size2D CheckBoxSize = new Size2D(200,200);

        protected override ViewStyle GetViewStyle()
        {
            ButtonStyle Style = new ButtonStyle
            {
                /// Square area connected with click event
                Size = CheckBoxSize,
                Icon = new ImageViewStyle
                {
                    Size = CheckBoxSize,
                    ResourceUrl = new Selector<string>
                    {
                        Other = "",
                        Selected = ImageURL + "RoundCheckMark.png"
                    },
                    /// Round shadow, but the area connected with click event remains square
                    ImageShadow = new ImageShadow(ImageURL + "RoundShadow.png"),
                },
                ParentOrigin = ParentOrigin.BottomCenter,
                PositionUsesPivotPoint = true,
                PivotPoint = PivotPoint.BottomCenter
            };
            return Style;
        }
    }

    class Program : NUIApplication
    {
        /// <summary>
        /// Path to the directory with images
        /// </summary>
        static string ImageURL = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/";
        /// <summary>
        /// View with all CheckBoxes
        /// </summary>
        private View MainView;
        /// <summary>
        /// Used for a default CheckBox and CheckBox created with properties
        /// </summary>
        private CheckBox CheckBoxExample;
        /// <summary>
        /// Used for a custom style CheckBox
        /// </summary>
        private CheckBox CustomCheckBox;
        /// <summary>
        /// The side of the CheckBox
        /// </summary>
        static private int CheckBoxSide = 200;
        /// <summary>
        /// Size of each CheckBox
        /// </summary>
        private Size2D CheckBoxSize = new Size2D(CheckBoxSide, CheckBoxSide);
        /// <summary>
        /// Space between CheckBoxes inside the MainView
        /// </summary>
        private int Space = 50;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += WindowKeyEvent;

            MainView = new View();
            MainView.Size = new Size2D(CheckBoxSide + 100, 4 * CheckBoxSide + 5 * Space);
            MainView.PivotPoint = PivotPoint.Center;
            MainView.ParentOrigin = ParentOrigin.Center;
            MainView.PositionUsesPivotPoint = true;
            MainView.BackgroundColor = Color.White;
            Window.Instance.Add(MainView);

            // Default CheckBox
            CheckBoxExample = new CheckBox();
            CheckBoxExample.Size = CheckBoxSize;
            CheckBoxExample.ParentOrigin = ParentOrigin.TopCenter;
            CheckBoxExample.PositionUsesPivotPoint = true;
            CheckBoxExample.PivotPoint = PivotPoint.TopCenter;
            CheckBoxExample.Position = new Position(0, Space);
            MainView.Add(CheckBoxExample);

            // Create with properties
            CheckBoxExample = new CheckBox();
            CheckBoxExample.Size = CheckBoxSize;
            CheckBoxExample.ParentOrigin = ParentOrigin.Center;
            CheckBoxExample.PositionUsesPivotPoint = true;
            CheckBoxExample.PivotPoint = PivotPoint.Center;
            CheckBoxExample.Position = new Position(0, - (CheckBoxSide + Space) / 2);
            // Set the icon images for different check box states
            StringSelector IconURL = new StringSelector()
            {
                Normal           = ImageURL + "Blue.png",
                Selected         = ImageURL + "BlueCheckMark.png",
                Pressed          = ImageURL + "Red.png",
            };
            CheckBoxExample.IconURLSelector = IconURL;
            CheckBoxExample.Icon.Size = new Size2D(160,160);
            CheckBoxExample.BackgroundColor = new Color(0.57f, 0.7f, 1.0f, 0.8f);
            // CheckBox initial state set to be selected
            CheckBoxExample.IsSelected = true;
            // CheckBox can be selected
            CheckBoxExample.IsSelectable = true;
            // CheckBox is enabled
            CheckBoxExample.IsEnabled = true;
            MainView.Add(CheckBoxExample);

            // Create with style - since the CheckBox inherits after the Button class, the ButtonStyle is used
            ButtonStyle Style = new ButtonStyle
            {
                IsSelectable = true,
                ParentOrigin = ParentOrigin.Center,
                PositionUsesPivotPoint = true,
                PivotPoint = PivotPoint.Center,
                Position = new Position(0, (CheckBoxSide + Space) / 2),
                Size = CheckBoxSize,
                // Gray structural background
                BackgroundImage = ImageURL + "Struct.png",
                // Image overlaid on the background
                Icon = new ImageViewStyle
                {
                    Size =  CheckBoxSize,
                    // Different icon used depending on the status
                    ResourceUrl = new Selector<string>
                    {
                        Other = ImageURL + "XSign.png",
                        Selected = ImageURL + "CheckMark.png"
                    },
                    // Icon opacity set to 0.8 for all checkbox states
                    Opacity = 0.8f,
                    // Shadow visible for all states
                    ImageShadow = new ImageShadow(ImageURL + "Shadow.png")
                },
                // Style of the overlay image
                Overlay = new ImageViewStyle()
                {
                    ResourceUrl = new Selector<string>
                    {
                        Pressed = ImageURL + "Red.png",
                        Other   = ImageURL + "LightBlue.png"
                    },
                    Opacity = new Selector<float?> {Pressed = 0.3f, Other = 1.0f}
                }
            };
            CheckBoxExample = new CheckBox(Style);
            MainView.Add(CheckBoxExample);

            // Create with custom style
            // Custom style registration
            Tizen.NUI.Components.StyleManager.Instance.RegisterStyle("_CustomCheckBoxStyle", null, typeof(NUI_CheckBox.CustomCheckBoxStyle));
            CustomCheckBox = new CheckBox("_CustomCheckBoxStyle");
            CustomCheckBox.Position = new Position(0, -Space);
            // Click event handle
            CustomCheckBox.Clicked += OnClicked;
            MainView.Add(CustomCheckBox);
        }

        /// <summary>
        /// Event handler after the CheckBox is clicked
        /// </summary>
        /// <param name="sender"> Event sender </param>
        /// <param name="e"> Event argument </param>
        private void OnClicked(object sender, EventArgs e)
        {
            if (CustomCheckBox.IsSelected)
            {
                MainView.BackgroundColor = Color.Green;
            }
            else
            {
                MainView.BackgroundColor = Color.White;
            }
        }

        /// <summary>
        /// Called when the chosen key event is received.
        /// Use to exit the application
        /// </summary>
        /// <param name="sender"> Event sender </param>
        /// <param name="e"> Event argument </param>
        private void WindowKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "Escape" || e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "BackSpace")
                {
                    Exit();
                }
            }
        }

        /// <summary>
        /// Entry method of the program/application.
        /// </summary>
        /// <param name="args">Launch arguments.</param>
        static void Main(string[] args)
        {
            var App = new Program();
            App.Run(args);
            App.Dispose();
        }
    }
}
