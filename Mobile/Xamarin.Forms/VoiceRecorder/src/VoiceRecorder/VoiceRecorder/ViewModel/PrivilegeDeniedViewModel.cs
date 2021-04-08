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
using System.Windows.Input;
using VoiceRecorder.Model;
using Xamarin.Forms;

namespace VoiceRecorder.ViewModel
{
    /// <summary>
    /// Provides abstraction for application privilege denied state.
    /// </summary>
    public class PrivilegeDeniedViewModel
    {
        #region

        /// <summary>
        /// Command which handles privilege denial confirmation (from user).
        /// </summary>
        public ICommand ConfirmCommand { get; }

        #endregion

        #region

        /// <summary>
        /// Initializes PrivilegeDeniedViewModel class instance.
        /// </summary>
        public PrivilegeDeniedViewModel()
        {
            ConfirmCommand = new Command(ExecuteConfirmCommand);
        }

        /// <summary>
        /// Handles execution of "ConfirmCommand".
        /// Closes application.
        /// </summary>
        private void ExecuteConfirmCommand()
        {
            DependencyService.Get<IApplicationService>().Close();
        }

        #endregion
    }
}
