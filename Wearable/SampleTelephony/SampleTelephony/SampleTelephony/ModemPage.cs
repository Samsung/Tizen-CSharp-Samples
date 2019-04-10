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

using System;
using Xamarin.Forms;
using Tizen;
using Tizen.Telephony;
using Tizen.Wearable.CircularUI.Forms;

namespace SampleTelephony
{
    /// <summary>
    /// This class provides APIs to obtain information from the modem.
    /// </summary>
    public class ModemPage : CirclePage
    {
        private static Modem _modem = null;

        // It shows the result of executing API.
        // If an exception occurs, the result shows the message of exception.
        Label result;

        public ModemPage()
        {
            // Create components
            // Create Label
            Label title = CreateTitle();

            // Add button in screen for getting IMEI
            var imeiBtn = new Button
            {
                Text = "Get IMEI",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            imeiBtn.Clicked += ImeiBtn_Clicked;

            // Add button in screen for getting power status
            var powerBtn = new Button
            {
                Text = "Get power status",
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            powerBtn.Clicked += PowerBtn_Clicked;

            // Add button in screen for getting MEID
            var meidBtn = new Button
            {
                Text = "Get MEID",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            meidBtn.Clicked += MeidBtn_Clicked;

            // Create Label
            // Set attribute for result label
            result = new Label()
            {
                BackgroundColor = Color.White,
                FontSize = 10,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            result.Text = "Start Modem API test";

            // Create Label
            // Set attribute for start_temp label
            Label start_temp = new Label()
            {
                Text = "...",
                FontSize = 13,
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
            };

            // Create Label
            // Set attribute for temp label
            Label temp = new Label()
            {
                Text = "...",
                TextColor = Color.Black,
                FontSize = 10,
                LineHeight = 3,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Center,
            };

            // Put the StackLayout in a ScrollView.
            CircleScrollView sv = new CircleScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                // Add all controls on layout for displaying on screen
                Content = new StackLayout
                {
                    Children = { start_temp, title, imeiBtn, powerBtn, meidBtn, result, temp }
                }
            };
            Content = sv;
            RotaryFocusObject = sv;

            try
            {
                // If not set slotHandle, can't get any info from Telephony API.
                // Should be initialized before executing the menu on ModemPage.
                if (Globals.slotHandle == null)
                {
                    Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                    return;
                }

                // For getting and using modem information,
                // instance of Modem Class should be needed.
                _modem = new Modem(Globals.slotHandle);
            }

            catch (Exception ex)
            {
                Log.Debug(Globals.LogTag, "Exception in modem constructor: " + ex.ToString());
            }
        }

        /// <summary>
        /// Gets the MEID (Mobile Equipment Identifier) of a mobile phone (for CDMA).
        /// In case of GSM phone, MEID is set the blank.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void MeidBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using modem information,
            // instance of Modem Class should be needed.
            if (_modem == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the MEID on dlog
            Log.Debug(Globals.LogTag, "MEID: " + _modem.Meid);

            // Shows the MEID on result label
            result.Text = "MEID: " + _modem.Meid;
        }

        /// <summary>
        /// Gets the power status of the modem.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void PowerBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using modem information,
            // instance of Modem Class should be needed.
            if (_modem == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Power Status on dlog
            Log.Debug(Globals.LogTag, "Power Status: " + _modem.CurrentPowerStatus);

            // Shows the Power Status on result label
            result.Text = "Power Status: " + _modem.CurrentPowerStatus;
        }

        /// <summary>
        /// Gets the IMEI (International Mobile Station Equipment Identity) of a mobile phone.
        /// The IMEI number is used by a GSM network to identify valid devices and therefore,
        /// can be used for stopping a stolen phone from accessing that network.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void ImeiBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using modem information,
            // instance of Modem Class should be needed.
            if (_modem == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the IMEI on dlog
            Log.Debug(Globals.LogTag, "IMEI: " + _modem.Imei);

            // Shows the IMEI on result label
            result.Text = "IMEI: " + _modem.Imei;
        }

        /// <summary>
        /// Create a new Label component
        /// </summary>
        /// <returns>Label</returns>
        private Label CreateTitle()
        {
            return new Label()
            {
                Text = "Telephony/Modem Test",
                TextColor = Color.White,
                FontSize = 9,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }
    }
}
