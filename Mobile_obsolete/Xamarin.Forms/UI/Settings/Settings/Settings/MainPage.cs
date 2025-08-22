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

using Xamarin.Forms;

namespace Settings
{
    /// <summary>
    /// The main page of the settings application.
    /// </summary>
    public class MainPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {

            // Title of this page.
            this.Title = "Settings";
            // Content view of this page.
            this.Content = new TableView
            {
                // A table intended to be used as a menu for selections.
                Intent = TableIntent.Menu,
                Root = new TableRoot
                {
                    // Create new TableSection and set the sub title.
                    new TableSection("Connections")
                    {
                        // Create new custom cell and set the title and image.
                        new CustomImageCell("Wi-Fi", "settings_wifi.png", this),
                        new CustomImageCell("Bluetooth", "settings_bluetooth.png", this),
                        new CustomImageCell("Airplane mode", "settings_flight.png", this),
                        new CustomImageCell("Tethering", "settings_tethering.png", this),
                        new CustomImageCell("Mobile Networks", "settings_mobile_networks.png", this),
                        new CustomImageCell("Tizen Connect", "settings_more_connections.png", this)
                    },

                    new TableSection("Device")
                    {
                        new CustomImageCell("Sound", "settings_sound_and_notifications.png", this),
                        new CustomImageCell("Display", "settings_display.png", this),
                        new CustomImageCell("Apps", "settings_applications.png", this)
                    },

                    new TableSection("Personalization")
                    {
                        new CustomImageCell("Wallpapers", "settings_wallpapers.png", this),
                        new CustomImageCell("Lock screen", "settings_lock_screen_and_security.png", this),
                        new CustomImageCell("Accessibility", "settings_accessibility.png", this),
                        new CustomImageCell("Privacy and security", "settings_privacy_and_safety.png", this),
                        new CustomImageCell("Accounts", "settings_account.png", this)
                    },

                    new TableSection("System")
                    {
                        new CustomImageCell("Language and input", "settings_language_and_input.png", this),
                        new CustomImageCell("Storage", "settings_storage.png", this),
                        new CustomImageCell("Date and time", "settings_date_and_time.png", this),
                        new CustomImageCell("About Device", "settings_about_device.png", this)
                    },
                }
            };
        }
    }
}