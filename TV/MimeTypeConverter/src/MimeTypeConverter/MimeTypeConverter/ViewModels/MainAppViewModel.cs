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
using System;
﻿using System.Collections.Generic;
using System.Windows.Input;
using MimeTypeConverter.Models;
using MimeTypeConverter.Utils;
using Xamarin.Forms;
using System.Linq;

namespace MimeTypeConverter.ViewModels
{
    /// <summary>
    /// MainAppViewModel class.
    /// Provides commands and methods responsible for application view model state.
    /// </summary>
    public class MainAppViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field for UserInput property.
        /// </summary>
        private String _userInput;

        /// <summary>
        /// Backing field for ResultsHeader property.
        /// </summary>
        private String _queryString;

        /// <summary>
        /// Instance of the main application model.
        /// </summary>
        private MimeUtilsModel _mimeUtilsModel;

        /// <summary>
        /// Private backing field for results list property.
        /// </summary>
        private IEnumerable<string> _resultsList;

        /// <summary>
        /// Private backing field forr NoResults property.
        /// </summary>
        private bool _resultsFound;

        /// <summary>
        /// Private backing field forr Convertedproperty.
        /// </summary>
        private bool _converted;

        /// <summary>
        /// Private backing field for ErrorMessage property.
        /// </summary>
        private string _errorMessage;

        /// <summary>
        /// Backing field for MediaType property.
        /// </summary>
        private string _mediaType;

        #endregion

        #region properties

        /// <summary>
        /// User input text.
        /// </summary>
        public String UserInput {
            get => _userInput;
            set => SetProperty(ref _userInput, value);
        }

        /// <summary>
        /// Query string.
        /// </summary>
        public String QueryString
        {
            get => _queryString;
            set => SetProperty(ref _queryString, value);
        }

        /// <summary>
        /// Command which shows message about occurred error.
        /// The command is injected into view model.
        /// </summary>
        public Command ErrorInfoCommand { get; set; }

        /// <summary>
        /// Command which converts input to MIME type or extensions.
        /// </summary>
        public ICommand ConvertCommand { get; }

        /// <summary>
        /// Media type of provided input.
        /// </summary>
        public String MediaType
        {
            get => _mediaType;
            set => SetProperty(ref _mediaType, value);
        }

        /// <summary>
        /// Conversion results.
        /// </summary>
        public IEnumerable<string> ResultsList
        {
            get => _resultsList;
            set => SetProperty(ref _resultsList, value);
        }

        /// <summary>
        /// Indicates that conversion resulted with no results.
        /// </summary>
        public bool NoResults
        {
            get => _resultsFound;
            set => SetProperty(ref _resultsFound, value);
        }

        /// <summary>
        /// Indicates that conversion has been made.
        /// </summary>
        public bool Converted
        {
            get => _converted;
            set => SetProperty(ref _converted, value);
        }

        /// <summary>
        /// Error message.
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// MainAppViewModel class constructor.
        /// Initializes the view model.
        /// </summary>
        public MainAppViewModel()
        {
            _mimeUtilsModel = new MimeUtilsModel();
            _mimeUtilsModel.ErrorOccured += OnErrorOccurred;

            ConvertCommand = new Command(ExecuteConvertCommand);
        }

        /// <summary>
        /// Handles execution of "ConvertCommand".
        /// Calls model to convert given string to MIME type or to list of file extensions.
        /// </summary>
        private void ExecuteConvertCommand()
        {
            QueryString = UserInput;
            InputType type = new InputRecognizer().GetType(QueryString);
            ResultsList = _mimeUtilsModel.GetConversionResults(QueryString);
            NoResults = !ResultsList.Any();

            string mediaType = "";

            if (type == InputType.MIMEType)
            {
                mediaType = _mimeUtilsModel.GetFileType(QueryString); ;

            } else if (type == InputType.Extension)
            {
                mediaType = _mimeUtilsModel.GetFileType(_mimeUtilsModel.GetMimeType(QueryString));
            }

            MediaType = mediaType;

            Converted = true;
        }

        /// <summary>
        /// Handles "ErrorOccured" event.
        /// Notifies user about error.
        /// </summary>
        /// <param name="sender">Instance of the class which invoked the event.</param>
        /// <param name="errorMessage">Error message.</param>
        private void OnErrorOccurred(object sender, string errorMessage)
        {
            ErrorMessage = errorMessage;

            Device.StartTimer(TimeSpan.Zero, () =>
            {
                ErrorInfoCommand?.Execute(null);
                // return false to run the timer callback only once
                return false;
            });
        }

        #endregion
    }
}
