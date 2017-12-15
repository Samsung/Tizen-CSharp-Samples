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

namespace NotificationApp
{
    /// <summary>
    /// The ContentPage for testing Tizen.Application.Notification C# API
    /// </summary>
    class NotificationPage : ContentPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NotificationPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize NotificationPage. Add components and events.
        /// </summary>
        private void InitializeComponent()
        {
            // Set tile
            Title = "Notification";
            IsVisible = true;

            // Create Label
            Label title = CreateTitle();
            // Create ListView
            ListView list = CreateListView();
            
            // Add events
            AddEvent(list);
            
            // Create a layout
            Content = new StackLayout
            {
                Children =
                {
                   title,
                   list,
                }
            };
        }

        /// <summary>
        /// Create a new Label component
        /// </summary>
        /// <returns>Label</returns>
        private Label CreateTitle()
        {
            return new Label()
            {
                Text = "Notification Test",
                TextColor = Color.White,
                FontSize = 28,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }

        /// <summary>
        /// Create a new ListView to show Tizen.Application.Notification operations
        /// </summary>
        /// <returns>ListView</returns>
        private ListView CreateListView()
        {
            // Create ListView
            ListView ListView = new ListView()
            {
                // Set the ItemSource of ListView
                ItemsSource = new List<String>()
                {
                    // Show the normal notification
                    "Normal Notification",
                    // Show the progress notification
                    "Progress Notification",
                    // Show the active style notification
                    "Active Notification",
                    // Show the reply notification
                    "Reply Notification",
                },
            };
            return ListView;
        }

        /// <summary>
        /// Add ItemTapped event to ListView
        /// </summary>
        /// <param name="list">ListView to add event</param>
        private void AddEvent(ListView list)
        {
            list.ItemTapped += (s, e) =>
            {
                INotificationOperation operation;
                // Item is "Normal Notification"
                if (e.Item.ToString().Equals("Normal Notification"))
                {
                    // Set operation as NormalNotificaiton
                    operation = new NormalNotification();
                }
                // Item is "Active Notification"
                else if (e.Item.ToString().Equals("Active Notification"))
                {
                    // Set operation as ActiveNotification
                    operation = new ActiveNotification();
                }
                // Item is "Progress Notification"
                else if (e.Item.ToString().Equals("Progress Notification"))
                {
                    // Set operation as ProgressNotification
                    operation = new ProgressNotification();
                }
                // Item is "ReplyNotification"
                else
                {
                    // Set operation as ReplyNotification
                    operation = new ReplyNotification();
                }

                operation.Operate();
                list.SelectedItem = null;
            };
        }
    }
}
