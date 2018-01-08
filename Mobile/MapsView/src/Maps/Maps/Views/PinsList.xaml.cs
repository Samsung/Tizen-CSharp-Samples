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

using Maps.ViewModels;

namespace Maps.Views
{
    /// <summary>
    /// Pins list component.
    /// </summary>
    public partial class PinsList
    {
        /// <summary>
        /// Stores common instance of PageViewModel.
        /// </summary>
        private PageViewModel _viewModel = ViewModelLocator.ViewModel;

        /// <summary>
        /// Initializes component.
        /// Adds handler for "ItemTapped" event.
        /// Sets "SelectedItem" to first Pin from collection.
        /// </summary>
        public PinsList()
        {
            InitializeComponent();
            BindingContext = _viewModel;

            ItemTapped += _viewModel.OnItemSelected;

            SelectedItem = _viewModel.GetFirstPin();
        }
    }
}