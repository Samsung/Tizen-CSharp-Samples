//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.


using Weather.Utils;
using Xamarin.Forms;

namespace Weather.ViewModels
{
    /// <summary>
    /// ViewModel class for ApiErrorPage.
    /// </summary>
    public class ApiErrorViewModel
    {
        #region properties

        /// <summary>
        /// Gets HTTP status code.
        /// </summary>
        public int Code => ErrorHandler.Code;

        /// <summary>
        /// Gets message provided with exception.
        /// </summary>
        public string Message => ErrorHandler.Message ?? "None";

        /// <summary>
        /// Gets command that exits application.
        /// </summary>
        public Command ExitAppCommand { get; } = new Command(() => { Forms.Context.Exit();  });

        #endregion
    }
}