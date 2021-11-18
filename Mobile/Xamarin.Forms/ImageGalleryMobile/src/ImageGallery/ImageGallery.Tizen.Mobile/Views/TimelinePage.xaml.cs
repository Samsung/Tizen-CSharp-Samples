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
using ImageGallery.Tizen.Mobile.Controls.PopupView;
using ImageGallery.Tizen.Mobile.Services;
using ImageGallery.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Xaml;

namespace ImageGallery.Tizen.Mobile.Views
{
    /// <summary>
    /// TimelinePage class.
    /// Provides logic for timeline page.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimelinePage : ContentPage
    {
        #region fields

        /// <summary>
        /// Key event service, which allows to listen to key events triggered by the remote control.
        /// </summary>
        private KeyEventService _keyEventService;

        /// <summary>
        /// Reference to the context menu popup, which is displayed when the hardware menu button is pressed.
        /// </summary>
        private ContextMenuControl _listPopupView;

        /// <summary>
        /// Reference to the cancel toolbar item.
        /// </summary>
        private ToolbarItem _cancelToolbarItem;

        /// <summary>
        /// Reference to the delete toolbar item.
        /// </summary>
        private ToolbarItem _deleteToolbarItem;

        /// <summary>
        /// Flag indicating whether delete state is active or not.
        /// </summary>
        private bool _isDeleteState = false;

        #endregion

        #region properties

        /// <summary>
        /// Provides commands and methods responsible for application view model state.
        /// </summary>
        public MainViewModel AppMainViewModel { private set; get; }

        #endregion

        #region methods

        /// <summary>
        /// TimelinePage class constructor.
        /// </summary>
        public TimelinePage()
        {
            AppMainViewModel = ((App)Application.Current).AppMainViewModel;
            InitializeComponent();
            Forms.ViewInitialized += OnViewInitialized;
            AppMainViewModel.DeleteStateChanged += OnDeleteStateChanged;
            AppMainViewModel.ImagesToRemoveUpdated += OnImagesToRemoveUpdated;
            AppMainViewModel.MultipleContentDeleted += OnMultipleContentDeleted;
        }

        /// <summary>
        /// Handles "MultipleContentDeleted" event of the MainViewModel object.
        /// Updates text displayed in the navigation bar title.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        private void OnMultipleContentDeleted(object e)
        {
            UpdateTitle();
        }

        /// <summary>
        /// Handles "ImagesToRemoveUpdated" event of the MainViewModel object.
        /// Updates text displayed in the navigation bar title.
        /// </summary>
        /// <param name="numberOfImages">Number of selected images.</param>
        private void OnImagesToRemoveUpdated(object numberOfImages)
        {
            UpdateTitle((int)numberOfImages);
        }

        /// <summary>
        /// Handles "DeleteStateChanged" event of the MainViewModel object.
        /// Provides information whether application's delete state is active or not.
        /// Updates appearance of the navigation toolbar by using UpdateNavigationBar method.
        /// If the application's delete state is inactive
        /// it sets IsSelectAll property of the MainViewModel to false.
        /// </summary>
        /// <param name="state">Boolean value describing delete state.</param>
        private void OnDeleteStateChanged(object state)
        {
            _isDeleteState = (bool)state;
            UpdateNavigationBar();
        }

        /// <summary>
        /// Updates page navigation bar.
        /// </summary>
        private void UpdateNavigationBar()
        {
            UpdateTitle();
        }

        /// <summary>
        /// Updates text displayed in the navigation bar title.
        /// </summary>
        /// <param name="count">Number of selected images.</param>
        private void UpdateTitle(int count = 0)
        {
            string text;

            if (_isDeleteState)
            {
                if (count == 0)
                {
                    text = "Select files";
                }
                else
                {
                    text = count + " selected";
                }

                CreateDeleteToolbarItem();
                CreateCancelToolbarItem();
            }
            else
            {
                text = "ImageGallery";
                DestroyDeleteToolbarItem();
                DestroyCancelToolbarItem();
            }

            Title = text;
        }

        /// <summary>
        /// Creates cancel toolbar item of the navigation bar.
        /// </summary>
        private void CreateCancelToolbarItem()
        {
            if (ToolbarItems.IndexOf(_cancelToolbarItem) == 1)
            {
                return;
            }

            _cancelToolbarItem = new ToolbarItem()
            {
                Order = ToolbarItemOrder.Secondary,
                Text = "CANCEL"
            };

            _cancelToolbarItem.SetBinding(MenuItem.CommandProperty, new Binding("UnsetDeleteStateCommand"));
            ToolbarItems.Insert(1, _cancelToolbarItem);
        }

        /// <summary>
        /// Creates delete toolbar item of the navigation bar.
        /// </summary>
        private void CreateDeleteToolbarItem()
        {
            if (ToolbarItems.IndexOf(_deleteToolbarItem) == 0)
            {
                return;
            }

            _deleteToolbarItem = new ToolbarItem()
            {
                Order = ToolbarItemOrder.Primary,
                Text = "DELETE"
            };

            _deleteToolbarItem.Clicked += (sender, item) =>
            {
                OnDeleteToolbarClick();
            };
            ToolbarItems.Insert(0, _deleteToolbarItem);
        }

        /// <summary>
        /// Handles "Clicked" event of the delete toolbar item.
        /// Displays delete confirmation popup.
        /// </summary>
        private async void OnDeleteToolbarClick()
        {
            int numberOfImages = AppMainViewModel.ImagesToRemove.Count;
            string title = "Delete image";
            string message = "This image will be deleted.";

            if (numberOfImages == 0)
            {
                return;
            }

            if (numberOfImages > 1)
            {
                title = "Delete images";
                message = "These images will be deleted.";
            }

            if (await DisplayAlert(title, message, "Delete", "Cancel"))
            {
                AppMainViewModel.DeleteImagesCommand.Execute(null);
            }
        }

        /// <summary>
        /// Removes cancel toolbar item from the navigation bar.
        /// </summary>
        private void DestroyCancelToolbarItem()
        {
            _cancelToolbarItem.RemoveBinding(MenuItem.CommandProperty);
            ToolbarItems.Remove(_cancelToolbarItem);
        }

        /// <summary>
        /// Removes delete toolbar item from the navigation bar.
        /// </summary>
        private void DestroyDeleteToolbarItem()
        {
            ToolbarItems.Remove(_deleteToolbarItem);
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
        /// Handles ViewInitialized event.
        /// Initializes key event services and context popup.
        /// </summary>
        /// <param name="sender">Instance of an object which invoked the event.</param>
        /// <param name="args">Event args.</param>
        private void OnViewInitialized(object sender, ViewInitializedEventArgs args)
        {
            _keyEventService = new KeyEventService();
            InitializeContextPopup();
            Forms.ViewInitialized -= OnViewInitialized;
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
        /// Sets application's delete state.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="selectedItemText">Text of the selected item.</param>
        private void OnItemSelected(object sender, string selectedItemText)
        {
            AppMainViewModel.SetDeleteStateCommand?.Execute(null);
        }

        /// <summary>
        /// Handles "MenuKeyPressed" event of the KeyEventService object.
        /// Toggles context menu popup.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">An instance of the EventArgs class providing detailed information about the event.</param>
        private void OnMenuKeyPressed(object sender, EventArgs e)
        {
            ToggleContextPopup();
        }

        /// <summary>
        /// Toggles context popup.
        /// Shows popup if it is not visible and the DayCollection contains at least one item.
        /// Hides popup otherwise.
        /// </summary>
        private void ToggleContextPopup()
        {
            if (!_listPopupView.IsShown() &&
                AppMainViewModel.DayCollection.Count > 0 &&
                !AppMainViewModel.IsDeleteState &&
                AppMainViewModel.IsStarted)
            {
                _listPopupView.Show();
            }
            else
            {
                _listPopupView.Hide();
            }
        }

        /// <summary>
        /// Overrides OnAppearing method.
        /// Registers "MenuKeyPressed" event handler.
        /// </summary>
        protected override void OnAppearing()
        {
            _keyEventService.MenuKeyPressed += OnMenuKeyPressed;
        }

        /// <summary>
        /// Overrides OnDisappearing method.
        /// Unregisters "MenuKeyPressed" event handler.
        /// </summary>
        protected override void OnDisappearing()
        {
            _keyEventService.MenuKeyPressed -= OnMenuKeyPressed;
        }

        #endregion
    }
}