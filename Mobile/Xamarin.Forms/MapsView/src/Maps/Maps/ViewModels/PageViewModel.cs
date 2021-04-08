/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using Maps.Models;

using Xamarin.Forms;

namespace Maps.ViewModels
{
    /// <summary>
    /// Main application View Model.
    /// Manages data used by map and commands for user interaction.
    /// </summary>
    public class PageViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Map zoom level.
        /// </summary>
        private int _zoomLevel = Config.InitialMapZoom;

        /// <summary>
        /// GPS availability.
        /// </summary>
        private bool _isGPSAvailable;

        #endregion

        #region properties

        /// <summary>
        /// Collection of places.
        /// </summary>
        public ObservableCollection<Pin> PinPoints { get; } = DefinedPoints.GetCollection();

        /// <summary>
        /// Map initialization status.
        /// True if map is initialized.
        /// </summary>
        public bool IsMapInitialized { get; set; }

        /// <summary>
        /// Map zoom level.
        /// </summary>
        public int ZoomLevel
        {
            get => _zoomLevel;
            set => SetProperty(ref _zoomLevel, value);
        }

        /// <summary>
        ///  GPS availability.
        ///  </summary>
        public bool IsGPSAvailable
        {
            get => _isGPSAvailable;
            set => SetProperty(ref _isGPSAvailable, value);
        }

        /// <summary>
        /// Map zoom out command.
        /// </summary>
        public ICommand ZoomOutCommand { get; set; }

        /// <summary>
        /// Map zoom in command.
        /// </summary>
        public ICommand ZoomInCommand { get; set; }

        /// <summary>
        /// Event dispatched on Pin request.
        /// </summary>
        public event EventHandler<Pin> OnPinRequest;

        #endregion

        #region methods

        /// <summary>
        /// PageViewModel class constructor.
        /// Assigns commands.
        /// </summary>
        public PageViewModel()
        {
            ZoomOutCommand = new Command(ZoomOut);
            ZoomInCommand = new Command(ZoomIn);
        }

        /// <summary>
        /// Returns first Pin from Pins collection.
        /// </summary>
        /// <returns>First Pin from collection.</returns>
        public Pin GetFirstPin()
        {
            return PinPoints.FirstOrDefault();
        }

        /// <summary>
        /// Increases zoom level.
        /// </summary>
        public void ZoomIn()
        {
            if (ZoomLevel < Config.MaximumMapZoom)
            {
                ZoomLevel++;
            }

            ZoomChanged();
        }

        /// <summary>
        /// Decreases zoom level.
        /// </summary>
        private void ZoomOut()
        {
            if (ZoomLevel > Config.MinimumMapZoom)
            {
                ZoomLevel--;
            }

            ZoomChanged();
        }

        /// <summary>
        /// Dispatches "OnZoomChanged" event.
        /// </summary>
        private void ZoomChanged()
        {
            OnZoomChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handles zoom change.
        /// </summary>
        public event EventHandler OnZoomChanged;

        /// <summary>
        /// Invokes "OnPinRequest" event handler with selected Pin data.
        /// </summary>
        /// <param name="sender">Event sender. Not used.</param>
        /// <param name="e">Tapped item event arguments.</param>
        public void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            OnPinRequest?.Invoke(this, (e.Item as Pin));
        }

        #endregion
    }
}