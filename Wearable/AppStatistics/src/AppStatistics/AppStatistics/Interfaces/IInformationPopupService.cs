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
namespace AppStatistics.Interfaces
{
    /// <summary>
    /// Provides methods to use the information popup service.
    /// </summary>
    public interface IInformationPopupService
    {
        #region properties

        /// <summary>
        /// Gets or sets text displayed on the error popup.
        /// </summary>
        string ErrorPopupText { get; set; }

        /// <summary>
        /// Gets or sets text displayed on the error popup button.
        /// </summary>
        string ErrorPopupButtonText { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Shows error popup with action button.
        /// </summary>
        void ShowErrorPopup();

        #endregion
    }
}
