/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using Camera.Model;
using Xamarin.Forms;

namespace Camera.ViewModel
{
    /// <summary>
    /// Provides commands and methods to handle presented pop-up.
    /// </summary>
    public class PopupViewModel : ViewModelBase
    {
        #region properties

        /// <summary>
        /// Command which handles privilege denial confirmation (from user).
        /// </summary>
        public Command ConfirmCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes PopupViewModel class instance.
        /// </summary>
        public PopupViewModel()
        {
            ConfirmCommand = new Command(ExecuteConfirmCommand);
        }

        /// <summary>
        /// Handles execution of ConfirmCommand.
        /// Closes the application.
        /// </summary>
        private void ExecuteConfirmCommand()
        {
            DependencyService.Get<IApplicationService>().Close();
        }

        #endregion
    }
}
