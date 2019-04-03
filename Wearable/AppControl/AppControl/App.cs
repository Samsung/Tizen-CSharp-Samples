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
                            Command = new Command(async () =>
                            {
                                try {
                                    // Launch with app control APIs
                                    Tizen.Applications.AppControl.SendLaunchRequest(
                                        new Tizen.Applications.AppControl
                                        {
                                            ApplicationId = "org.tizen.example.AppInformation",
                                            LaunchMode = Tizen.Applications.AppControlLaunchMode.Single,
                                        }, (launchRequest, replyRequest, result) =>
                                        {
                                            Console.WriteLine("Put your code for appcontrol callback method.");
                                        });
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error : " + e.Message +", " + e.Source +", " + e.StackTrace);
                                    await MainPage.DisplayAlert("Error", e.Message +"\nPlease satisfy the prerequisites(install AppInformation app to launch", "OK");
                                }
                            })
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Called when this application starts.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Called when the application enters the sleeping state.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Called when the application resumes from a sleeping state.
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
