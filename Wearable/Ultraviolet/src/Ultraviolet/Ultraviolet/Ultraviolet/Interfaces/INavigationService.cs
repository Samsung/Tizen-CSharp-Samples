/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

﻿using Ultraviolet.ViewModels;

namespace Ultraviolet.Interfaces
{
    /// <summary>
    /// Interface for NavigationService.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigate to a Page based on a ViewModel type.
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel type.</typeparam>
        /// <param name="viewModel">Instance of view model.</param>
        /// <param name="clearStack">After navigating to a new page, remove all previous pages from NavigationStack.</param>
        void NavigateTo<TViewModel>(TViewModel viewModel = null, bool clearStack = false) where TViewModel : BaseViewModel;
    }
}
