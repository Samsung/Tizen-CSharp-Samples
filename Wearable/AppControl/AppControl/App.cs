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
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace AppControl
{
    /// <summary>
    /// AppControl sample main class.
    /// This class creates a simple UI to send app launch request using app control APIs.
    /// </summary>
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new CirclePage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Launch other applications using app control"
                        },
                        new Button
                        {
                            Text = "Launch",
                            Command = new Command(() =>
                            {
                                // Launch with app control APIs
                                Tizen.Applications.AppControl.SendLaunchRequest(
                                    new Tizen.Applications.AppControl
                                    {
                                        ApplicationId = "org.tizen.alarm",
                                        LaunchMode = Tizen.Applications.AppControlLaunchMode.Single,
                                    }, (launchRequest, replyRequest, result) => 
                                    {
                                        System.Diagnostics.Debug.WriteLine("Put your code for appcontrol callback method.");
                                    });
                            })
                        }
                    }
                }
            };
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
