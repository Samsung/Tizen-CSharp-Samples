/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using ImageGallery.Models;
using ImageGallery.Tizen.Mobile.Constants;
using ImageGallery.Tizen.Mobile.Controls.PopupView;
using ImageGallery.Tizen.Mobile.Services;
using ImageGallery.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImageGallery.Tizen.Mobile.Views
{
    /// <summary>
    /// DetailsPage class.
    /// Provides logic for details page.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        #region fields

        /// <summary>
        /// An instance of KeyEventService class.
        /// </summary>
        private readonly KeyEventService _keyEventService;

        /// <summary>
        /// An instance of ContextMenuControl class.
        /// </summary>
        private ContextMenuControl _listPopupView;

        /// <summary>
        /// File name of the favorite on icon.
        /// </summary>
        private const string FAVORITE_ON = "favorite_on.png";

        /// <summary>
        /// File name of the favorite off icon.
        /// </summary>
        private const string FAVORITE_OFF = "favorite_off.png";

        #endregion

        #region properties

        /// <summary>
        /// An instance of the MainViewModel class.
        /// </summary>
        public MainViewModel AppMainViewModel { private set; get; }

        /// <summary>
        /// DeleteImageCommand bindable property.
        /// </summary>
        public static BindableProperty DeleteImageCommandProperty = BindableProperty.Create(nameof(DeleteImageCommand), typeof(Command),
            typeof(DetailsPage), default(Command));

        /// <summary>
        /// DeleteImageCommand property.
        /// </summary>
        public Command DeleteImageCommand
        {
            set { SetValue(DeleteImageCommandProperty, value); }
            get { return (Command)GetValue(DeleteImageCommandProperty); }
        }

        #endregion

        #region methods

        /// <summary>
        /// DetailsPage class constructor.
        /// </summary>
        public DetailsPage()
        {
            AppMainViewModel = ((App)Application.Current).AppMainViewModel;

            _keyEventService = new KeyEventService();

            InitializeComponent();

            InitializeContextPopup();

            if (AppMainViewModel.CurrentImageFilePathExists)
            {
                CreateIsFavoriteIcon();
            }

            AppMainViewModel.ContentUpdated += OnContentUpdated;
        }

        /// <summary>
        /// Handles "OnContentUpdated" event of the MainViewModel class.
        /// Executes CreateIsFavoriteIcon method.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">An instance of the ContentUpdatedArgs class providing detailed information about the event.</param>
        private void OnContentUpdated(object sender, IContentUpdatedArgs e)
        {
            if (AppMainViewModel.CurrentImageFilePathExists)
            {
                CreateIsFavoriteIcon();
            }
        }

        /// <summary>
        /// Creates IsFavorite icon.
        /// </summary>
        private void CreateIsFavoriteIcon()
        {
            if (ToolbarItems.Count == 1)
            {
                ToolbarItems.RemoveAt(0);
            }

            var toolbarItem = new ToolbarItem()
            {
                Order = ToolbarItemOrder.Primary,
                Icon = AppMainViewModel.CurrentImageIsFavorite ? FAVORITE_ON : FAVORITE_OFF
            };

            toolbarItem.SetBinding(MenuItem.CommandProperty, new Binding("ToggleIsFavoriteCommand"));

            ToolbarItems.Insert(0, toolbarItem);
        }

        /// <summary>
        /// Overrides OnBackButtonPressed method.
        /// Hides context menu if it is shown.
        /// Prevents any other action being result of pressing back button.
        /// </summary>
        /// <returns>Boolean value indicating whether default system back button action should happen or not.</returns>
        protected override bool OnBackButtonPressed()
        {
            if (_listPopupView.IsShown())
            {
                _listPopupView.Hide();
                return true;
            }

            return base.OnBackButtonPressed();
        }

        /// <summary>
        /// Creates an instance of ContextMenuControl class.
        /// Creates "ItemSelected" event handler.
        /// Initializes list of items.
        /// </summary>
        private void InitializeContextPopup()
        {
            _listPopupView = new ContextMenuControl();
            _listPopupView.ItemSelected += OnItemSelected;
            _listPopupView.Items = new List<string>()
            {
                "Delete"
            };
        }

        /// <summary>
        /// Handles "ItemSelected" event of the ContextMenuControl object.
        /// Executes command binded to the DeleteImageCommand property.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="item">Text of the selected item.</param>
        private async void OnItemSelected(object sender, string item)
        {
            if (await DisplayAlert("Delete image", "This image will be deleted.", "Delete", "Cancel"))
            {
                DeleteImageCommand?.Execute(item);
            }
        }

        /// <summary>
        /// Handles "MenuKeyPressed" event of the KeyEventService object.
        /// Executes ToggleContextPopup method.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">An instance of the EventArgs class providing detailed information about the event.</param>
        private void OnMenuKeyPressed(object sender, EventArgs e)
        {
            ToggleContextPopup();
        }

        /// <summary>
        /// Shows or hides context popup depending on whether it is visible or not.
        /// </summary>
        private void ToggleContextPopup()
        {
            if (_listPopupView.IsShown())
            {
                _listPopupView.Hide();
            }
            else
            {
                _listPopupView.Show();
            }
        }

        /// <summary>
        /// Overrides OnAppearing method.
        /// </summary>
        protected override void OnAppearing()
        {
            if (AppMainViewModel.CurrentImageFilePathExists)
            {
                _keyEventService.MenuKeyPressed += OnMenuKeyPressed;
            }

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor =
                ColorConstants.NAVIGATION_BAR_COLOR_DETAILS;
        }

        /// <summary>
        /// Overrides OnDisappearing method.
        /// </summary>
        protected override void OnDisappearing()
        {
            _keyEventService.MenuKeyPressed -= OnMenuKeyPressed;
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor =
                ColorConstants.NAVIGATION_BAR_COLOR_DEFAULT;
        }

        #endregion
    }
}