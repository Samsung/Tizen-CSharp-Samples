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
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.Applications;
using System.Collections.Generic;
using Tizen.Uix.Sticker;

/// <summary>
/// Namespace for Sticker consumer sample.
/// </summary>
namespace StickerConsumerSample
{
    /// <summary>
    /// StickerPage class to show stickers.
    /// </summary>
    class StickerPage : Page
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
        /// The constructor for the sticker page.
        /// </summary>
        /// <param name="Navigator">The Navigator component</param>
        /// <param name="retrieveType">The RetrieveType</param>
        public StickerPage(Navigator Navigator, RetrieveType retrieveType) : base()
        {
            navigator = Navigator;

            // Create component to store custom views.
            View mainView = new View();

            // Create and setup linear layout. It defines children positions in main view.
            LinearLayout linearLayout = new LinearLayout();
            linearLayout.LinearOrientation = LinearLayout.Orientation.Vertical;

            mainView.Layout = linearLayout;
            mainView.BackgroundImage = Application.Current.DirectoryInfo.Resource + "/images/bg.png";
            mainView.Size = window.Size;
            Add(mainView);

            // Create component to set title text.
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
                    GoBack();
                }

                return true;
            };
            titleView.Add(backButton);

            // Method for setting title text.
            Func<RetrieveType, string> getTitleText = x =>
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

            // Create the component to set title text.
            TextLabel titleLabel = new TextLabel()
            {
                Text = getTitleText(retrieveType),
                TextColor = Color.White,
                PointSize = 9,
                Size = titleView.Size,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            titleView.Add(titleLabel);
            mainView.Add(titleView);

            // Create the component to show stickers.
            View stickerView = new View();
            stickerView.WidthSpecification = LayoutParamPolicies.MatchParent;
            stickerView.HeightSpecification = LayoutParamPolicies.MatchParent;

            FlexLayout flexLayout = new FlexLayout();
            flexLayout.WrapType = FlexLayout.FlexWrapType.Wrap;
            flexLayout.Direction = FlexLayout.FlexDirection.Row;
            stickerView.Layout = flexLayout;

            // Check that StickerConsumer class is initialized.
            if (!StickerConsumer.Initialized)
            {
                // Initialize the StickerConsumer class.
                StickerConsumer.Initialize();
            }

            // Add image views that contain sticker files to the main view.
            foreach (StickerData stickerData in GetStickerFiles(retrieveType))
            {
                ImageView imageView = new ImageView();
                // Set the URI of sticker file to ResourceUrl.
                imageView.ResourceUrl = stickerData.Uri;
                imageView.Size2D = new Size2D(100, 100);
                imageView.Margin = new Extents(20, 20, 20, 20);
                stickerView.Add(imageView);
            }

            mainView.Add(stickerView);
        }

        /// <summary>
        /// Get the information of the saved sticker files using RetrieveType.
        /// </summary>
        /// <param name="retrieveType">The RetrieveType</param>
        private IEnumerable<StickerData> GetStickerFiles(RetrieveType retrieveType)
        {
            switch (retrieveType)
            {
                case RetrieveType.All:
                    return StickerConsumer.GetAllStickers(0, 10);
                case RetrieveType.Keyword:
                    return StickerConsumer.GetStickersByKeyword(0, 10, "face");
                case RetrieveType.Group:
                    return StickerConsumer.GetStickersByGroup(0, 10, "animal");
                case RetrieveType.UriType:
                default:
                    return StickerConsumer.GetStickersByUriType(0, 10, UriType.LocalPath);
            }
        }

        /// <summary>
        /// Go back to the previous page.
        /// </summary>
        private void GoBack()
        {
            // Check that StickerConsumer class is initialized.
            if (StickerConsumer.Initialized)
            {
                // Deinitialize the StickerConsumer class.
                StickerConsumer.Deinitialize();
            }

            navigator.Pop();
        }
    }
}