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

namespace NUI_Pagination
{
    /// <summary>
    /// Custom style of the pagination
    /// </summary>
    internal class CustomPaginationStyle : StyleBase
    {
        protected override ViewStyle GetViewStyle()
        {
            PaginationStyle Style = new PaginationStyle
            {
                /// size of the pagination indicator
                IndicatorSize = new Size(100, 100),
                /// size of the space between the indicators
                IndicatorSpacing = 50,
                /// images used for two states of the indicators
                IndicatorImageUrl = new Selector<string>
                {
                    Normal = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/gray.png",
                    Selected = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/blue.png"
                },
                Name = "Pagination",
                /// size of the pagination box
                Size = new Size(500, 200),
                BackgroundColor = new Color(1.0f, 1.0f, 1.0f, 1.0f),
                /// the reference point of the parent set on the top center
                ParentOrigin = ParentOrigin.TopCenter,
                /// the reference point of the pagination set to top center
                /// since no shift is defined, the two reference points will overlap
                PivotPoint = PivotPoint.TopCenter,
                /// if 'true' the set PivotPoint is used, otherwise the default value is used
                PositionUsesPivotPoint = true
            };
            return Style;
        }
    }

    class Program : NUIApplication
    {
        private View MainView;
        private VisualView MainVisualView;
        private Pagination PaginationExample;
        private int WindowWidth;
        private int WindowHeight;
        private Position2D WindowShift;

        /// <summary>
        /// Handles creation phase of the forms application.
        /// Sets up windows settings.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += WindowKeyEvent;
            WindowWidth = Window.Instance.WindowSize.Width;
            WindowHeight = Window.Instance.WindowSize.Height;
            WindowShift = new Position2D(WindowWidth, 0);

            // the view is parent for the MainVisualView and the PaginationExample
            MainView = new View();
            MainView.Size2D = new Size2D(WindowWidth, WindowHeight);
            Window.Instance.Add(MainView);

            // VisualView of the size = 3 * window_size; contains 3 images
            MainVisualView = new VisualView();
            MainVisualView.Size2D = new Size2D(3 * WindowWidth, WindowHeight);
            MainVisualView.PivotPoint = PivotPoint.TopLeft;
            MainVisualView.ParentOrigin = ParentOrigin.TopLeft;
            MainVisualView.PositionUsesPivotPoint = true;
            MainVisualView.BackgroundColor = Color.White;
            MainView.Add(MainVisualView);

            // adds images to the ImageVisuals
            ImageVisual ThisImageVisual = null;
            for (uint i = 1; i <= 3; ++i)
            {
                ThisImageVisual = CreateImageVisual(i);
                ThisImageVisual.Position = new Vector2((WindowWidth - ThisImageVisual.Size.Width) / 2 + (i - 1) * WindowWidth, 50);
                MainVisualView.AddVisual("image" + i.ToString(), ThisImageVisual);
            }

            // registers the pagination's custom style
            Tizen.NUI.Components.StyleManager.Instance.RegisterStyle("CustomPagination", null, typeof(NUI_Pagination.CustomPaginationStyle));

            PaginationExample = new Pagination("CustomPagination");
            PaginationExample.IndicatorCount = 3;
            PaginationExample.SelectedIndex = 0;

            MainView.Add(PaginationExample);
        }

        /// <summary>
        /// The method to create the image visual with the ImageNumber-th image
        /// </summary>
        /// <param name="ImageNumber"> The number of the image </param>
        /// <returns> The created ImageView </returns>
        private ImageVisual CreateImageVisual(uint ImageNumber)
        {
            ImageVisual ThisImageVisual = new ImageVisual();

            ThisImageVisual.URL = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/page" + ImageNumber.ToString() + ".png";
            ThisImageVisual.Size = new Vector2(600, 900);
            ThisImageVisual.Origin = Visual.AlignType.CenterBegin;
            ThisImageVisual.AnchorPoint = Visual.AlignType.CenterBegin;

            return ThisImageVisual;
        }

        /// <summary>
        /// Called when the chosen key event is received.
        /// Response on the pressing the left and right arrow is defined here:
        /// switching the selected pagination's indicator.
        /// Also used to exit the application.
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
                else if (e.Key.KeyPressedName == "Left")
                {
                    if (PaginationExample.SelectedIndex > 0)
                    {
                        MainVisualView.Position2D = MainVisualView.Position2D + WindowShift;
                        PaginationExample.SelectedIndex = PaginationExample.SelectedIndex - 1;
                    }
                }
                else if (e.Key.KeyPressedName == "Right")
                {
                    if (PaginationExample.SelectedIndex < PaginationExample.IndicatorCount - 1)
                    {
                        MainVisualView.Position2D = MainVisualView.Position2D - WindowShift;
                        PaginationExample.SelectedIndex = PaginationExample.SelectedIndex + 1;
                    }
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
