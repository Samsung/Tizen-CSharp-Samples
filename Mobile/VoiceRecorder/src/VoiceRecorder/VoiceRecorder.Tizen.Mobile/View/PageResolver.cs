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
using VoiceRecorder.Tizen.Mobile.View;
using VoiceRecorder.View;
using Xamarin.Forms;

[assembly: Dependency(typeof(PageResolver))]

namespace VoiceRecorder.Tizen.Mobile.View
{
    /// <summary>
    /// PageResolver class.
    /// Implements IPageResolver interface.
    /// </summary>
    public class PageResolver : IPageResolver
    {
        #region properties

        /// <summary>
        /// Application main page.
        /// </summary>
        /// <returns>Tizen Mobile main page.</returns>
        public Page MainTabbedPage { get => new MobileMainTabbedPage(); }

        /// <summary>
        /// Page informing about privilege denied.
        /// </summary>
        /// <returns>Privilege denied page.</returns>
        public Page PrivilegeDeniedPage { get => new MobilePrivilegeDeniedPage(); }

        /// <summary>
        /// Page with the list of recordings.
        /// </summary>
        /// <returns>Tizen Mobile page with the list of recordings.</returns>
        public Page RecordingsListPage { get => new MobileRecordingsListPage(); }

        /// <summary>
        /// Page with the recorder settings.
        /// </summary>
        /// <returns>Tizen Mobile settings page.</returns>
        public Page SettingsPage { get => new MobileSettingsPage(); }

        /// <summary>
        /// Page with the player.
        /// </summary>
        /// <returns>Tizen Mobile player page.</returns>
        public Page VoicePlayerPage { get => new MobileVoicePlayerPage(); }

        /// <summary>
        /// Page with the voice recorder.
        /// </summary>
        /// <returns>Tizen Mobile recorder page.</returns>
        public Page VoiceRecorderPage { get => new MobileVoiceRecorderPage(); }

        /// <summary>
        /// Welcome page.
        /// </summary>
        /// <returns>Tizen Mobile welcome page.</returns>
        public Page WelcomePage { get => new MobileWelcomePage(); }

        #endregion
    }
}
