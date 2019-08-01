/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoogleAPIExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLogin(object sender, EventArgs e)
        {
            // Client credential details
            // this information is for google API example, it is registered by google
            // You should register a new client id and issue a client secret
            var clientSecrets = new ClientSecrets
            {
                ClientId = "581786658708-elflankerquo1a6vsckabbhn25hclla0.apps.googleusercontent.com",
                ClientSecret = "3f6NggMbPtrmIBpgx-MK2xXK"
            };

            // It is a web browser, it is integrated in this app.
            // It is instance of Page, so it pushed on the Navigation Page
            // When a completed login process, you need to pop browser page on the Navigation Page
            var browser = new EmbeddedBrowser("Login to google");
            browser.Cancelled += (s, evt) => { Navigation.PopAsync(); };
            var unused = Navigation.PushAsync(browser);
            var userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets, new[] { DriveService.Scope.Drive }, "user", CancellationToken.None, 
                codeReceiver : new TizenLocalServerCodeReceiver { EmbeddedBrowser = browser });
            unused = Navigation.PopAsync();
            unused = Navigation.PushAsync(new FileListPage(userCredential));
        }
    }
}