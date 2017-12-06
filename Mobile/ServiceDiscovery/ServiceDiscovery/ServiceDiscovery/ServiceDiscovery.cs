/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
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

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ServiceDiscovery
{
    /// <summary>
    /// An example application for Tizen.Network.Nsd C# API.
    /// This application can announce (register) and discover a service within local network.
    /// Especially, the service discovery operations are provided by DNS-SD protocol.
    /// There are two operation.
    /// 1. DNS-SD Service Registration
    /// 2. DNS-SD Service Discovery
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Constructor to start an application for Mobile profile.
        /// Create menus for each operations
        /// </summary>
        public App()
        {
            // The root page of your application
            MainPage = CreateMainPage();
        }

        /// <summary>
        /// Create MainPage.
        /// It contains a ListView to show operations
        /// </summary>
        /// <returns>MainPage</returns>
        private Page CreateMainPage()
        {
            // Create components
            ListView list = CreateMenuList();

            // Add events on being tapped
            AddEvent(list);

            return new ContentPage
            {
                // Title of this application
                Title = "DNS-SD Service Discovery",

                // Layout
                Content = new StackLayout
                {
                    Children =
                    {
                        // Menus
                        list,
                    }
                }
            };
        }

        /// <summary>
        /// Create a ListView to show available menus
        /// </summary>
        /// <returns>ListView</returns>
        private ListView CreateMenuList()
        {
            // Possible operations
            // They are shown in a ListView component
            return new ListView()
            {
                ItemsSource = new List<string>()
                {
                    "DNS-SD Service Registration",
                    "DNS-SD Service Discovery",
                }
            };
        }

        /// <summary>
        /// Add ItemTapped event to ListView
        /// </summary>
        /// <param name="list">ListVIew to add event</param>
        private void AddEvent(ListView list)
        {
            try
            {
                list.ItemTapped += async (s, a) =>
                {
                    if (a.Item.ToString().Equals("DNS-SD Service Registration"))
                    {
                    // switch the current Page to RegisterPage
                    await MainPage.Navigation.PushModalAsync(new RegisterPage());
                    }
                    else if (a.Item.ToString().Equals("DNS-SD Service Discovery"))
                    {
                    // switch the current Page to DiscoveryPage
                    await MainPage.Navigation.PushModalAsync(new DiscoveryPage());
                    }
                };
            }
            catch (Exception ex)
            {
                MainPage.DisplayAlert("Unexpected Error", ex.Message.ToString(), "OK");
            }
        }
    }
}
