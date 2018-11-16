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
using Xamarin.Forms;

namespace VoiceRecorder.View
{
    /// <summary>
    /// IPageResolver interface to obtain pages for current device.
    /// </summary>
    public interface IPageResolver
    {
        #region properties

        /// <summary>
        /// Main application page property.
        /// </summary>
        /// <returns>Main page</returns>
        Page MainTabbedPage { get; }

        /// <summary>
        /// Page informing about privilege denied.
        /// </summary>
        /// <returns>Privilege denied page.</returns>
        Page PrivilegeDeniedPage { get; }

        /// <summary>
        /// Page with the list of recordings.
        /// </summary>
        /// <returns>List of recordings page</returns>
        Page RecordingsListPage { get; }

        /// <summary>
        /// Page with the recorder settings.
        /// </summary>
        /// <returns>Settings page</returns>
        Page SettingsPage { get; }

        /// <summary>
        /// Page with the player.
        /// </summary>
        /// <returns>Player page</returns>
        Page VoicePlayerPage { get; }

        /// <summary>
        /// Page with the voice recorder.
        /// </summary>
        /// <returns>Recorder page</returns>
        Page VoiceRecorderPage { get; }

        /// <summary>
        /// Welcome page.
        /// </summary>
        /// <returns>Welcome page.</returns>
        Page WelcomePage { get; }

        #endregion properties
    }
}
