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
namespace Geocoding.Views
{
    /// <summary>
    /// Provides methods which allow to navigate between pages.
    /// </summary>
    public interface IPageNavigation
    {
        #region methods

        /// <summary>
        /// Creates welcome page and sets it as active.
        /// </summary>
        void CreateWelcomePage();

        /// <summary>
        /// Creates range selection page and sets it as active.
        /// </summary>
        void CreateSearchPage();

        /// <summary>
        /// Creates map page and sets it as active.
        /// </summary>
        void CreateMapPage();

        /// <summary>
        /// Closes the application.
        /// </summary>
        void Close();

        #endregion
    }
}
