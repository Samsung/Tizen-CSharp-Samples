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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace AppInformation
{
    /// <summary>
    /// This is a main class of ApplicationInfo and DirectoryInfo.
    /// This class creates a simple list to show ApplicationInfo and DirectoryInfo of 
    /// this application.
    /// </summary>
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new CirclePage();
            StackLayout mainStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
            };

            Tizen.Applications.Application app = Tizen.Applications.Application.Current;
            CircleListView clView = new CircleListView();
            List<string> appInfoList = new List<string>();

            // ApplicationInfo
            appInfoList.Add("ApplicationInfo: ");

            appInfoList.Add("AppID: " + app.ApplicationInfo.ApplicationId);
            appInfoList.Add("AppType: " + app.ApplicationInfo.ApplicationType);
            appInfoList.Add("ExePath: " + app.ApplicationInfo.ExecutablePath);
            appInfoList.Add("PackageId: " + app.ApplicationInfo.IconPath);
            appInfoList.Add("SharedResourcePath: " + app.ApplicationInfo.SharedResourcePath);
            appInfoList.Add("SharedTrustedPath: " + app.ApplicationInfo.SharedTrustedPath);

            // DirectoryInfo
            appInfoList.Add("DirectoryInfo: ");

            appInfoList.Add("Resource: " + app.DirectoryInfo.Resource);
            appInfoList.Add("Cache: " + app.DirectoryInfo.Cache);
            appInfoList.Add("Data: " + app.DirectoryInfo.Data);
            appInfoList.Add("ExpansionPackageResource: " + app.DirectoryInfo.ExpansionPackageResource);
            appInfoList.Add("ExternalCache: " + app.DirectoryInfo.ExternalCache); 
            appInfoList.Add("ExternalData: " + app.DirectoryInfo.ExternalData); 
            appInfoList.Add("ExternalSharedData: " + app.DirectoryInfo.ExternalSharedData); 
            appInfoList.Add("SharedResource: " + app.DirectoryInfo.SharedResource);
            appInfoList.Add("SharedTrusted: " + app.DirectoryInfo.SharedTrusted);
            appInfoList.Add("SharedData: " + app.DirectoryInfo.SharedData);

            clView.ItemsSource = appInfoList;
            mainStack.Children.Add(clView);
            ((CirclePage)MainPage).Content = mainStack;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
