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
namespace Geocoding.ViewModels
{
    /// <summary>
    /// Provides methods to use the information popup service.
    /// </summary>
    public interface IInformationPopupService
    {
        #region properties

        /// <summary>
        /// Gets or sets text displayed on the no results popup.
        /// </summary>
        string NoResultsPopupText { get; set; }

        /// <summary>
        /// Gets or sets text displayed on the no results popup button.
        /// </summary>
        string NoResultsPopupButtonText { get; set; }

        /// <summary>
        /// Gets or sets text displayed on the connection failed popup.
        /// </summary>
        string ConnectionFailedPopupText { get; set; }

        /// <summary>
        /// Gets or sets text displayed on the connection failed popup button.
        /// </summary>
        string ConnectionFailedPopupButtonText { get; set; }

        /// <summary>
        /// Gets or sets text displayed on the "Find more markers" popup.
        /// </summary>
        string FindMoreMarkersPopupText { get; set; }

        /// <summary>
        /// Gets or sets text displayed on the "Find more markers" popup button.
        /// </summary>
        string FindMoreMarkersPopupButtonText { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Dismisses the popup.
        /// </summary>
        void Dismiss();

        /// <summary>
        /// Shows no results popup.
        /// </summary>
        void ShowNoResultsPopup();

        /// <summary>
        /// Shows connection failed popup.
        /// </summary>
        void ShowConnectionFailedPopup();

        /// <summary>
        /// Shows "Find more markers" popup.
        /// </summary>
        void ShowFindMoreMarkersPopup();

        #endregion
    }
}
