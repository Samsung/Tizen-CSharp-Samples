/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using ErrorCodeConverter.Interfaces;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ErrorCodeConverter.ViewModels
{
    /// <summary>
    /// Main application view model.
    /// </summary>
    public class ErrorCodeConverterViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Minimum error code value.
        /// </summary>
        const int ERROR_CODE_MIN = -130;

        /// <summary>
        /// Maximum error code value.
        /// </summary>
        const int ERROR_CODE_MAX = 0;

        /// <summary>
        /// Platform specific error codes converting service. See <see cref="IErrorCodeConverterService"/>.
        /// </summary>
        private readonly IErrorCodeConverterService _service;

        /// <summary>
        /// Backing field for ErrorCode property.
        /// </summary>
        private int _errorCode;

        /// <summary>
        /// Backing field for ErrorMessage property.
        /// </summary>
        private string _errorMessage;

        #endregion

        #region properties

        /// <summary>
        /// Error code value.
        /// </summary>
        public int ErrorCode
        {
            get => _errorCode;
            set
            {
                SetProperty(ref _errorCode, Math.Min(ERROR_CODE_MAX, Math.Max(ERROR_CODE_MIN, value)));
                UpdateErrorMessage();
            }
        }

        /// <summary>
        /// Stores error message.
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        /// <summary>
        /// Stores command for decreasing error code value.
        /// </summary>
        public ICommand DecreaseErrorCode { get; }

        /// <summary>
        /// Stores command for increasing error code value.
        /// </summary>
        public ICommand IncreaseErrorCode { get; }

        #endregion

        #region methods

        /// <summary>
        /// Class constructor.
        /// Initializes commands.
        /// </summary>
        public ErrorCodeConverterViewModel()
        {
            _service = DependencyService.Get<IErrorCodeConverterService>();

            DecreaseErrorCode = new Command(() =>
            {
                ErrorCode -= 1;
            });

            IncreaseErrorCode = new Command(() =>
            {
                ErrorCode += 1;
            });
        }

        /// <summary>
        /// Updates ErrorMessage property.
        /// </summary>
        private void UpdateErrorMessage()
        {
            ErrorMessage = _service.GetMessageFromCode(_errorCode);
        }

        #endregion
    }
}
