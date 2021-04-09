/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Xamarin.Forms;

namespace PlayerSample
{
    public sealed class SecurityProvider
    {
        /// <summary>
        /// Instance for lazy initialization of SecurityProvider.
        /// </summary>
        private static readonly Lazy<SecurityProvider> lazy = new Lazy<SecurityProvider>(() => new SecurityProvider());

        /// <summary>
        /// A Record Item Provider class instance which provides Record Items.
        /// When it is called for the first time, SecurityProvider will be created.
        /// </summary>
        public static SecurityProvider Instance { get => lazy.Value; }

        /// <summary>
        /// Instance of IMediaPlayerSecurity for get the implementation of each platform.
        /// </summary>
        private static IMediaPlayerSecurity mediaplayerSecurity;

        /// <summary>
        /// media storage privilege.
        /// </summary>
        private const string privilegeMediastorage = "http://tizen.org/privilege/mediastorage";

        /// <summary>
        /// external storage privilege.
        /// </summary>
        private const string privilegeExternalstorage = "http://tizen.org/privilege/externalstorage";

        /// <summary>
        /// internet privilege.
        /// </summary>
        private const string privilegeInternet = "http://tizen.org/privilege/internet";

        /// <summary>
        /// SecurityProvider Constructor.
        /// A Constructor which will initialize the SecurityProvider instance.
        /// </summary>
        public void CheckPrivilege()
        {
            mediaplayerSecurity = DependencyService.Get<IMediaPlayerSecurity>();

            mediaplayerSecurity.CheckPrivilege(privilegeMediastorage);
            mediaplayerSecurity.CheckPrivilege(privilegeExternalstorage);
            mediaplayerSecurity.CheckPrivilege(privilegeInternet);
        }
    }
}