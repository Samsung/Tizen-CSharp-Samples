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
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoogleAPIExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FileListPage : ContentPage
	{
        public IEnumerable<File> Items { get; set; }
        public FileListPage(UserCredential credential)
		{
			InitializeComponent();

            var initializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive Sample Application",
            };

            var service = new DriveService(initializer);

            Device.BeginInvokeOnMainThread(async () =>
            {
                var list = await service.Files.List().ExecuteAsync();

                Items = list.Items.Where(d => !string.IsNullOrEmpty(d.OriginalFilename)).Take(50);

                BindingContext = this;
            });

        }
	}
}