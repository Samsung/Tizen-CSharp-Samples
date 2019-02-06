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
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Tizen;
using Tizen.Telephony;

namespace SampleTelephony
{
    public class MenuPage : CirclePage
    {
        // It shows the result of executing API.
        // If an exception occurs, the result shows the message of exception.
        Label result;

        public MenuPage()
        {
            // Set title message
            Title = "SampleTelephony";
            IsVisible = true;

            // Create components
            // Create Label
            Label title = CreateTitle();

            /// <summary>
            /// The Button for initializing Telephony C# API library and getting the SlotHandle
            /// It should be called at first before running any functions
            /// </summary>
            var initBtn = new Button
            {
                Text = "Initialize",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Fill,
            };

            // Add Clicked events
            initBtn.Clicked += InitBtn_Clicked;

            /// <summary>
            /// The Button for executing call functionality of Tizen.Telephony C# API
            /// </summary>
            var callBtn = new Button
            {
                Text = "Call",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Fill,
            };

            // Add Clicked events
            callBtn.Clicked += CallBtn_Clicked;

            /// <summary>
            /// The Button for executing Modem functionality of Tizen.Telephony C# API
            /// </summary>
            var ModemBtn = new Button
            {
                Text = "Modem",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Fill
            };

            // Add Clicked events
            ModemBtn.Clicked += ModemBtn_Clicked;


            /// <summary>
            /// The Button for executing Network functionality of Tizen.Telephony C# API
            /// </summary>
            var NetworkBtn = new Button
            {
                Text = "Network",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Fill
            };

            // Add Clicked events
            NetworkBtn.Clicked += NetworkBtn_Clicked;

            /// <summary>
            /// The Button for executing SIM functionality of Tizen.Telephony C# API
            /// </summary>
            var simBtn = new Button
            {
                Text = "SIM",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Fill
            };

            // Add Clicked events
            simBtn.Clicked += SimBtn_Clicked;

            /// <summary>
            /// The Button for de-initializing Telephony C# API library and freeing the SlotHandle
            /// It should be called before exiting sample Telephony Application
            /// </summary>
            var deinitBtn = new Button
            {
                Text = "De-initialize",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Fill
            };

            // Add Clicked events
            deinitBtn.Clicked += DeinitBtn_Clicked;

            // Create Label
            // Set attribute for result label
            result = new Label()
            {
                BackgroundColor = Color.White,
                FontSize = 10,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Fill,
            };
            result.Text = "Start Sample Telephony Application";

            // Create Label
            // Set attribute for temp label
            Label temp = new Label()
            {
                Text = "...",
                FontSize = 10,
                LineHeight = 3,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Fill,
            };

            // Put the StackLayout in a ScrollView.
            CircleScrollView sv = new CircleScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                Content = new StackLayout
                {
                    Children = { title, initBtn, callBtn, ModemBtn, NetworkBtn, simBtn, deinitBtn, result, temp }
                }
            };
            Content = sv;
            RotaryFocusObject = sv;

            try
            {
                // For getting the Telephony State changed status,
                // should be set Manager State Changed callback.
                Manager.StateChanged += Manager_StateChanged;
            }

            catch (Exception ex)
            {
                Log.Debug(Globals.LogTag, "Exception in registering for state changed event: " + ex.ToString());
            }
        }

        /// <summary>
        /// Whenever Telephony State is changed, this function will be called.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void Manager_StateChanged(object sender, StateEventArgs e)
        {
            Log.Debug(Globals.LogTag, "Telephony state changed: " + e.CurrentState);
        }

        /// <summary>
        /// Before the finishing Telephony API testing,
        /// should be called de-initialization procedure of Telephony API's interface.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void DeinitBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                // When Manger.Deinit() is called,
                // Will be released the resource related Telephony interface internally.
                Manager.Deinit();
                Globals.slotHandle = null;
                result.Text = "De-init successful";
            }

            catch (Exception ex)
            {
                Log.Debug(Globals.LogTag, "Telephony de-init exception: " + ex.ToString());
            }
        }

        /// <summary>
        /// When user selects Call from the Main Page, then move to CallPage
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private async void CallBtn_Clicked(object sender, EventArgs e)
        {
            // If not set slotHandle, can't get any info from Telephony API.
            // Should be initialized before executing the menu on CallPage.
            if (Globals.slotHandle == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                result.Text = "Telephony is not initialized/there are no SIM slot handles";
                return;
            }

            await Navigation.PushModalAsync(new CallPage());
        }

        /// <summary>
        /// When user selects Modem from the Main Page, then move to ModemPage
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private async void ModemBtn_Clicked(object sender, EventArgs e)
        {
            // If not set slotHandle, can't get any info from Telephony API.
            // Should be initialized before executing the menu on CallPage.
            if (Globals.slotHandle == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                result.Text = "Telephony is not initialized/there are no SIM slot handles";
                return;
            }

            await Navigation.PushModalAsync(new ModemPage());
        }

        /// <summary>
        /// When user selects Network from the Main Page, then move to NetworkPage
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private async void NetworkBtn_Clicked(object sender, EventArgs e)
        {
            // If not set slotHandle, can't get any info from Telephony API.
            // Should be initialized before executing the menu on CallPage.
            if (Globals.slotHandle == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                result.Text = "Telephony is not initialized/there are no SIM slot handles";
                return;
            }

            await Navigation.PushModalAsync(new NetworkPage());
        }

        /// <summary>
        /// When user selects SIM from the Main Page, then move to SIMPage
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private async void SimBtn_Clicked(object sender, EventArgs e)
        {
            // If not set slotHandle, can't get any info from Telephony API.
            // Should be initialized before executing the menu on CallPage.
            if (Globals.slotHandle == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                result.Text = "Telephony is not initialized/there are no SIM slot handles";
                return;
            }

            await Navigation.PushModalAsync(new SimPage());
        }

        /// <summary>
        /// Before the starting Telephony API testing,
        /// should be called initialization procedure of Telephony API's interface.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void InitBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                // For executing Telephony functionality, 
                // should be loaded more than 1 Modem Interface
                // simList.Count means the number of Modem Interface.
                // In case of simList.Count is zero, 
                // it means can't use the Telephony Interface. 
                List<SlotHandle> simList = Manager.Init().ToList();
                if (simList.Count == 0)
                {
                    Log.Debug(Globals.LogTag, "SIM list count is zero");
                    return;
                }

                // After getting simList.count,
                // Should keep the index as slotHandle.
                result.Text = "Telephony initialized successfully";
                Globals.slotHandle = simList.ElementAt(0);
            }

            catch (Exception ex)
            {
                Log.Debug(Globals.LogTag, "Telephony initialize exception: " + ex.ToString());
            }
        }

        /// <summary>
        /// Create a new Label component
        /// </summary>
        /// <returns>Label</returns>
        private Label CreateTitle()
        {
            return new Label()
            {
                Text = "Telephony Test",
                TextColor = Color.White,
                FontSize = 10,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }

    }
}
