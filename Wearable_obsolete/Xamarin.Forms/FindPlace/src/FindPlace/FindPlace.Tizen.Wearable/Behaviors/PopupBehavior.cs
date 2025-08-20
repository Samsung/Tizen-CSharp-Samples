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
using FindPlace.Interfaces;
using System;
using System.Windows.Input;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace FindPlace.Tizen.Wearable.Behaviors
{
    /// <summary>
    /// Class that provides functionality for popups.
    /// </summary>
    public class PopupBehavior : Behavior<CirclePage>
    {
        #region fields

        /// <summary>
        /// Information Popup Service obtained by Dependency Service.
        /// </summary>
        private readonly IInformationPopupService _popup;

        /// <summary>
        /// Field with text for no places found popup.
        /// </summary>
        private const string NoPlacesFound = "Service couldn't find such places.";

        /// <summary>
        /// Field with text for error popup.
        /// </summary>
        private const string SomethingWentWrong = "Something went wrong.";

        /// <summary>
        /// Field with text for popup button.
        /// </summary>
        public const string PopupButton = "OK";

        /// <summary>
        /// Field with text for loading popup.
        /// </summary>
        private const string Loading = "Loading";

        /// <summary>
        /// Field with text for GPS not available popup.
        /// </summary>
        private const string GpsNotAvailable = "GPS not available. The default location will be set.";

        /// <summary>
        /// Field with text for GPS access not granted popup.
        /// </summary>
        private const string GpsNotGranted = "GPS access not granted. The default location will be set.";

        #endregion

        #region properties

        /// <summary>
        /// Bindable property definition for HideLoadingPopupCommand.
        /// </summary>
        public static readonly BindableProperty HideLoadingPopupCommandProperty = BindableProperty.Create(
            nameof(HideLoadingPopupCommand),
            typeof(ICommand),
            typeof(PopupBehavior),
            null);

        /// <summary>
        /// Command for hiding loading popup.
        /// </summary>
        public ICommand HideLoadingPopupCommand
        {
            get => (ICommand)GetValue(HideLoadingPopupCommandProperty);
            set => SetValue(HideLoadingPopupCommandProperty, value);
        }

        /// <summary>
        /// Bindable property definition for ShowLoadingPopupCommand.
        /// </summary>
        public static readonly BindableProperty ShowLoadingPopupCommandProperty = BindableProperty.Create(
            nameof(ShowLoadingPopupCommand),
            typeof(ICommand),
            typeof(PopupBehavior),
            null);

        /// <summary>
        /// Command for showing loading popup.
        /// </summary>
        public ICommand ShowLoadingPopupCommand
        {
            get => (ICommand)GetValue(ShowLoadingPopupCommandProperty);
            set => SetValue(ShowLoadingPopupCommandProperty, value);
        }

        /// <summary>
        /// Bindable property definition for ShowErrorPopupCommand.
        /// </summary>
        public static readonly BindableProperty ShowErrorPopupCommandProperty = BindableProperty.Create(
            nameof(ShowErrorPopupCommand),
            typeof(ICommand),
            typeof(PopupBehavior),
            null);

        /// <summary>
        /// Command for showing error popup.
        /// </summary>
        public ICommand ShowErrorPopupCommand
        {
            get => (ICommand)GetValue(ShowErrorPopupCommandProperty);
            set => SetValue(ShowErrorPopupCommandProperty, value);
        }

        /// <summary>
        /// Bindable property definition for ShowNoPlacesPopupCommand.
        /// </summary>
        public static readonly BindableProperty ShowNoPlacesPopupCommandProperty = BindableProperty.Create(
            nameof(ShowNoPlacesPopupCommand),
            typeof(ICommand),
            typeof(PopupBehavior),
            null);

        /// <summary>
        /// Command for showing no places popup.
        /// </summary>
        public ICommand ShowNoPlacesPopupCommand
        {
            get => (ICommand)GetValue(ShowNoPlacesPopupCommandProperty);
            set => SetValue(ShowNoPlacesPopupCommandProperty, value);
        }

        /// <summary>
        /// Bindable property definition for ShowNotGrantedPopupCommand.
        /// </summary>
        public static readonly BindableProperty ShowNotGrantedPopupCommandProperty = BindableProperty.Create(
            nameof(ShowNotGrantedPopupCommand),
            typeof(ICommand),
            typeof(PopupBehavior),
            null);

        /// <summary>
        /// Command for GPS access not granted popup.
        /// </summary>
        public ICommand ShowNotGrantedPopupCommand
        {
            get => (ICommand)GetValue(ShowNotGrantedPopupCommandProperty);
            set => SetValue(ShowNotGrantedPopupCommandProperty, value);
        }

        /// <summary>
        /// Bindable property definition for ShowNotAvailablePopupCommand.
        /// </summary>
        public static readonly BindableProperty ShowNotAvailablePopupCommandProperty = BindableProperty.Create(
            nameof(ShowNotAvailablePopupCommand),
            typeof(ICommand),
            typeof(PopupBehavior),
            null);

        /// <summary>
        /// Command for showing GPS not available popup.
        /// </summary>
        public ICommand ShowNotAvailablePopupCommand
        {
            get => (ICommand)GetValue(ShowNotAvailablePopupCommandProperty);
            set => SetValue(ShowNotAvailablePopupCommandProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes popup behavior class.
        /// </summary>
        public PopupBehavior()
        {
            _popup = DependencyService.Get<IInformationPopupService>();
        }

        /// <summary>
        /// Overridden OnAttachedTo method which subscribes to BindingContextChanged in page.
        /// </summary>
        /// <param name="bindable">Bindable object.</param>
        protected override void OnAttachedTo(CirclePage bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.BindingContextChanged += OnBindableContextChanged;
        }

        /// <summary>
        /// Overridden OnDetachingFrom method which unsubscribes BindingContextChanged in page.
        /// </summary>
        /// <param name="bindable">Bindable object.</param>
        protected override void OnDetachingFrom(CirclePage bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindableContextChanged;
        }

        /// <summary>
        /// Handles BindingContextChange event.
        /// </summary>
        /// <param name="sender">Object which invoked the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnBindableContextChanged(object sender, EventArgs e)
        {
            ShowLoadingPopupCommand = new Command(ExecuteShowLoadingPopup);
            HideLoadingPopupCommand = new Command(ExecuteHideLoadingPopup);
            ShowErrorPopupCommand = new Command(ExecuteShowErrorPopup);
            ShowNoPlacesPopupCommand = new Command(ExecuteShowNoPlacesPopup);
            ShowNotAvailablePopupCommand = new Command(ExecuteShowNotAvailablePopup);
            ShowNotGrantedPopupCommand = new Command(ExecuteShowNotGrantedPopup);
        }

        /// <summary>
        /// Handles execution of ShowLoadingPopup.
        /// </summary>
        private void ExecuteShowLoadingPopup()
        {
            _popup.LoadingText = Loading;
            _popup.ShowLoadingPopup();
        }

        /// <summary>
        /// Handles execution of HideLoadingPopup.
        /// </summary>
        private void ExecuteHideLoadingPopup()
        {
            _popup.Dismiss();
        }

        /// <summary>
        /// Handles execution of ShowNoPlacesPopup.
        /// </summary>
        private void ExecuteShowNoPlacesPopup()
        {
            _popup.ErrorPopupText = NoPlacesFound;
            _popup.ErrorPopupButtonText = PopupButton;
            _popup.ShowErrorPopup();
        }

        /// <summary>
        /// Handles execution of ShowErrorPopup.
        /// </summary>
        private void ExecuteShowErrorPopup()
        {
            _popup.ErrorPopupText = SomethingWentWrong;
            _popup.ErrorPopupButtonText = PopupButton;
            _popup.ShowErrorPopup();
        }

        /// <summary>
        /// Handles execution of ShowNotAvailablePopup.
        /// </summary>
        private void ExecuteShowNotAvailablePopup()
        {
            _popup.ErrorPopupText = GpsNotAvailable;
            _popup.ErrorPopupButtonText = PopupButton;
            _popup.ShowErrorPopup();
        }

        /// <summary>
        /// Handles execution of ShowNotGrantedPopup.
        /// </summary>
        private void ExecuteShowNotGrantedPopup()
        {
            _popup.ErrorPopupText = GpsNotGranted;
            _popup.ErrorPopupButtonText = PopupButton;
            _popup.ShowErrorPopup();
        }

        #endregion
    }
}
