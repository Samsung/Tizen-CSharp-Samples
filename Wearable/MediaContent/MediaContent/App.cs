﻿//Copyright 2018 Samsung Electronics Co., Ltd
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

using MediaContent.Views;
using Tizen.Security;
using Xamarin.Forms;

namespace MediaContent
{
    /// <summary>
    /// The App class
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class
        /// </summary>
        public App()
        {
            AskForPermissions();
            MainPage = new RootPage();
        }

        /// <summary>
        /// Triggers required permission requests.
        /// </summary>
        private void AskForPermissions()
        {
            PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/mediastorage");
            PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/externalstorage");
        }
    }
}
