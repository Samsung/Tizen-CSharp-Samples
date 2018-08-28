/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xamarin.Forms;

namespace ApplicationControl
{
    /// <summary>
    /// A part of class for main page UI layout
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Initialize main page UI layout
        /// </summary>
        /// <param name="screenWidth">screen width</param>
        /// <param name="screenHeight">screen height</param>
        void InitializeComponent(int screenWidth, int screenHeight)
        {
            BackgroundColor = Color.FromRgb(255, 255, 255);

            var statusBarHeight = (int)((double)screenHeight * 0.03125);
            var adjustedScreenWidth = screenWidth;
            var adjustedScreenHeight = screenHeight - statusBarHeight;

            var page = new ContentPage
            {
                Title = "Application Control",
            };

            /// Instantiate a RelativeLayout for the mainLayout.
            /// the mainLayout is composed of an operationLayout, an applicationLayout and a composeLayout
            var mainLayout = new RelativeLayout
            {
                WidthRequest = adjustedScreenWidth,
                HeightRequest = adjustedScreenHeight,
            };

            /// Add an operationLayout to the mainLayout
            var operationLayout = new OperationLayout
            {
                WidthRequest = adjustedScreenWidth,
                HeightRequest = adjustedScreenHeight * 0.2614,
            };
            operationLayout.BindingContext = BindingContext;

            mainLayout.Children.Add(
                operationLayout,
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height - adjustedScreenHeight;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return adjustedScreenWidth;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return adjustedScreenHeight * 0.2614;
                }));

            /// Add an applicationLayout to the mainLayout
            var applicationLayout = new ApplicationLayout(screenWidth, screenHeight)
            {
                WidthRequest = adjustedScreenWidth,
                HeightRequest = adjustedScreenHeight * 0.3445,
            };
            applicationLayout.BindingContext = BindingContext;

            mainLayout.Children.Add(
                applicationLayout,
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height - (adjustedScreenHeight * (1 - 0.2614));
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return adjustedScreenWidth;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return adjustedScreenHeight * 0.3445;
                }));

            /// Add an composeLayout to the mainLayout
            var composeLayout = new ComposeLayout
            {
                WidthRequest = adjustedScreenWidth,
                HeightRequest = adjustedScreenHeight * 0.3941,
            };
            composeLayout.BindingContext = BindingContext;

            mainLayout.Children.Add(
                composeLayout,
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height - (adjustedScreenHeight * (1 - 0.6059));
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return adjustedScreenWidth;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return adjustedScreenHeight * 0.3941;
                }));

            Content = mainLayout;
        }
    }
}