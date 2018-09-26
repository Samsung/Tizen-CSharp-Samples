/*
 *  Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 *  Contact: Ernest Borowski <e.borowski@partner.samsung.com>
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License
 *
 *
 * @file        Certificates.cs
 * @author      Ernest Borowski (e.borowski@partner.samsung.com)
 * @version     1.0
 * @brief       This file contains implementation of UI
 */

namespace SecureRepository
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Class responsible for UI.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Holds App height. It is used when checking if height has changed.
        /// </summary>
        private double height;

        /// <summary>
        /// Contains ViewModel.
        /// </summary>
        private MainPageViewModel mv;

        /// <summary>
        /// Holds App width. It is used when checking if width has changed.
        /// </summary>
        private double width;

        /// <summary>
        /// Initializes a new instance of the MainPage class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            this.mv = new MainPageViewModel();
            this.BindingContext = this.mv;
        }

        /// <summary>
        /// Handles display orientation change (horizontal, vertical).
        /// </summary>
        /// <param name="width">New App width.</param>
        /// <param name="height">New App height.</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            // width or height has changed.
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;

                MainGrid.RowDefinitions.Clear();
                MainGrid.ColumnDefinitions.Clear();
                MainGrid.Children.Clear();

                optionsGrid.RowDefinitions.Clear();
                optionsGrid.ColumnDefinitions.Clear();
                optionsGrid.Children.Clear();

                // Wearable UI, it`s much simplified.
                // (Screen height may not equals screen width so it only checks pixel count).
                if (height < 500 && width < 500)
                {
                    this.MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.8, GridUnitType.Star) });
                    this.MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.8, GridUnitType.Star) });
                    this.MainGrid.Children.Add(this.listView, 0, 0);
                    this.MainGrid.Children.Add(this.appInfo, 0, 1);
                    appInfo.HorizontalOptions = LayoutOptions.Center;

                    this.mv.CallAdd();
                }
                else if (width > height)
                {
                    // Realigns UI for vertical mode.
                    MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.6, GridUnitType.Star) });
                    MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    optionsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    optionsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    optionsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    optionsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    optionsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                    optionsGrid.Children.Add(this.appInfo, 0, 0);
                    optionsGrid.Children.Add(this.contentPrefix, 0, 1);
                    optionsGrid.Children.Add(this.btnAdd, 0, 2);
                    optionsGrid.Children.Add(this.btnRemove, 0, 3);
                    optionsGrid.Children.Add(this.btnEncrypt, 0, 4);

                    MainGrid.Children.Add(this.listGrid, 0, 0);
                    MainGrid.Children.Add(this.optionsGrid, 1, 0);

                    // fix Listview size on TV emulator.
                    if (Device.Idiom == TargetIdiom.TV)
                    {
                        listView.Scale = 1.05;
                    }
                }
                else
                {
                    // Realigns UI for horizontal mode.
                    labelGrid.RowDefinitions.Clear();
                    labelGrid.ColumnDefinitions.Clear();
                    labelGrid.Children.Clear();

                    gridBtn.RowDefinitions.Clear();
                    gridBtn.ColumnDefinitions.Clear();
                    gridBtn.Children.Clear();

                    MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.6, GridUnitType.Star) });
                    MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                    optionsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.6, GridUnitType.Star) });
                    optionsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    labelGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    labelGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                    gridBtn.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    gridBtn.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    gridBtn.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                    labelGrid.Children.Add(this.appInfo, 0, 0);
                    labelGrid.Children.Add(this.contentPrefix, 0, 1);

                    gridBtn.Children.Add(this.btnAdd, 0, 0);
                    gridBtn.Children.Add(this.btnRemove, 0, 1);
                    gridBtn.Children.Add(this.btnEncrypt, 0, 2);

                    optionsGrid.Children.Add(this.labelGrid, 0, 0);
                    optionsGrid.Children.Add(this.gridBtn, 1, 0);

                    MainGrid.Children.Add(this.listGrid, 0, 0);
                    MainGrid.Children.Add(this.optionsGrid, 0, 1);
                }
                //// Changes Buttons and Labels size to fit in new UI.
                this.GridBtnSizeChanged(null, null);
            }
        }

        /// <summary>
        /// Adjusts margins of labels and buttons.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void GridBtnSizeChanged(object sender, EventArgs e)
        {
            // Changes buttons size to fit current UI.
            double margin, marginLeftRight;

            // Horizontal View.
            if (gridBtn.Children.Count > 0)
            {
                margin = gridBtn.Height * 0.02;
                marginLeftRight = gridBtn.Width * 0.15;
            }
            else
            {
                // Vertical Views.
                margin = optionsGrid.Height * 0.02;
                marginLeftRight = optionsGrid.Height * 0.15;
            }

            // Adjusts buttons margins.
            btnAdd.Margin = new Thickness(marginLeftRight, margin, marginLeftRight, margin);
            btnRemove.Margin = new Thickness(marginLeftRight, margin, marginLeftRight, margin);
            btnEncrypt.Margin = new Thickness(marginLeftRight, margin, marginLeftRight, margin);

            margin = appInfo.Height * 0.02;
            marginLeftRight = appInfo.Width * 0.02;

            // Adjusts labels margins.
            appInfo.Margin = new Thickness(marginLeftRight, margin, marginLeftRight, margin);
            contentPrefix.Margin = new Thickness(marginLeftRight, margin, marginLeftRight, margin);
        }
    }
}
