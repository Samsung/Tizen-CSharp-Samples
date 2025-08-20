/*
 * Copyright (c) 2022 Samsung Electronics Co., Ltd. All rights reserved.
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
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.Applications;

/// <summary>
/// Namespace for Sticker consumer sample.
/// </summary>
namespace StickerConsumerSample
{
    /// <summary>
    /// An enumeration for retrieve type.
    /// </summary>
    public enum RetrieveType
    {
        All = 1,
        Keyword,
        Group,
        UriType
    }

    /// <summary>
    /// MenuPage class for menu list.
    /// </summary>
    class MenuPage : Page
    {
        /// <summary>
        ///Application window instance.
        /// </summary>
        private Window window = Window.Instance;

        /// <summary>
        /// Navigator component which navigates pages.
        /// </summary>
        private Navigator navigator = null;

        /// <summary>
        /// The constructor for the menu page.
        /// </summary>
        /// <param name="Navigator">The Navigator component</param>
        public MenuPage(Navigator Navigator) : base()
        {
            navigator = Navigator;

            // Create the component to store custom views.
            View mainView = new View();

            // Create and setup linear layout. It defines children positions in main view.
            LinearLayout linearLayout = new LinearLayout();
            linearLayout.LinearOrientation = LinearLayout.Orientation.Vertical;

            // Setup layout elements padding and layout margins.
            linearLayout.CellPadding = new Size2D(0, 13);
            linearLayout.Padding = new Extents(10, 10, 10, 10);

            mainView.Layout = linearLayout;
            mainView.BackgroundImage = Application.Current.DirectoryInfo.Resource + "/images/bg.png";
            mainView.Size = window.Size;
            Add(mainView);

            // Create the component to set title view.
            View titleView = new View();
            titleView.Size = new Size(window.Size.Width, window.Size.Height / 10);

            // Create the component to set back button.
            ImageView backButton = new ImageView()
            {
                Size = new Size(40, 40),
                Color = Color.White,
                Position = new Position(15, 0),
                PositionUsesPivotPoint = true,
                PivotPoint = Tizen.NUI.PivotPoint.CenterLeft,
                ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft,
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "/images/back.png",
            };
            
            /// <summary>
            /// Method which is called when the back button is touched.
            /// </summary>
            /// <param name="sender">Event sender</param>
            /// <param name="args">Event arguments</param>
            backButton.TouchEvent += (object sender, TouchEventArgs e) =>
            {
                backButton.BackgroundColor = new Vector4(0.0f, 0.0f, 0.0f, 0.2f);
                if (e.Touch.GetState(0) == PointStateType.Up)
                {
                    // Terminate application.
                    NUIApplication.Current.Exit();
                }

                return true;
            };
            titleView.Add(backButton);

            // Create the component to set title text.
            TextLabel titleLabel = new TextLabel()
            {
                Text = "StickerConsumer",
                TextColor = Color.White,
                PointSize = 12,
                Size = titleView.Size,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            titleView.Add(titleLabel);
            mainView.Add(titleView);

            // Create an item in the list.
            View retrieveAll = CreateMenuItem(RetrieveType.All);
            mainView.Add(retrieveAll);

            // Create an item in the list.
            View retrieveByKeyword = CreateMenuItem(RetrieveType.Keyword);
            mainView.Add(retrieveByKeyword);

            // Create an item in the list.
            View retrieveByGroup = CreateMenuItem(RetrieveType.Group);
            mainView.Add(retrieveByGroup);

            // Create an item in the list.
            View retrieveByUriType = CreateMenuItem(RetrieveType.UriType);
            mainView.Add(retrieveByUriType);
        }

        /// <summary>
        /// Touch Event Handler. It is used to verify that the back button is touched.
        /// </summary>
        /// <param name="retrieveType">The RetrieveType</param>
        private View CreateMenuItem(RetrieveType retrieveType)
        {
            // Create the component to set list item.
            View view = new View()
            {
                BackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.8f),
                Size2D = new Size2D(window.Size.Width - 20, window.Size.Height / 12),
            };

            /// <summary>
            /// Method which is called when the list item is touched.
            /// </summary>
            /// <param name="sender">Event sender</param>
            /// <param name="args">Event arguments</param>
            view.TouchEvent += (object sender, TouchEventArgs e) =>
            {
                if (e.Touch.GetState(0) == PointStateType.Up)
                {
                    // Display changes to StickerPage.
                    StickerPage stickerPage = new StickerPage(navigator, retrieveType);
                    navigator.Push(stickerPage);
                }

                return true;
            };

            // Method for setting title text.
            Func<RetrieveType, string> getLableText = x =>
            {
                switch (x)
                {
                    case RetrieveType.All: return "Retrieves all sticker data";
                    case RetrieveType.Keyword: return "Retrieves sticker data using keyword";
                    case RetrieveType.Group: return "Retrieves sticker data using group";
                    case RetrieveType.UriType: return "Retrieves sticker data using URI type";
                    default: return "Retrieves all sticker data";
                }
            };

            // Create the component to set title text on list item.
            TextLabel textLabel = new TextLabel()
            {
                Text = getLableText(retrieveType),
                TextColor = Color.Black,
                PointSize = 10,
                Size = view.Size,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Begin,
            };

            view.Add(textLabel);
            return view;
        }
    }
}
