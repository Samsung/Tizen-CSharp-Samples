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
using System.Linq;
using Xamarin.Forms;
using Tizen;
using Tizen.Telephony;

namespace SampleTelephony
{
    /// <summary>
    /// This class allow you to get the voicecall states.
    /// It provides the list of CallHandle which can be used to get the information about call related actions.
    /// </summary>
    public class CallPage : ContentPage
    {
        // For getting and using call information,
        // instance of Call Class should be needed.
        private static Call _call = null;

        // For receiving call related information from Telephony,
        // should be set notification callbacks
        // For getting changed info of voicecall subscription preference,
        // should be set CallPreferredVoiceSubscription
        // For getting outgoing call status,
        // should be set VoiceCallStatusDialing
        // For receiving incoming call information
        // should be set VoiceCallStatusIncoming
        private ChangeNotificationEventArgs.Notification[] notiArr =
        {
            ChangeNotificationEventArgs.Notification.CallPreferredVoiceSubscription,
            ChangeNotificationEventArgs.Notification.VoiceCallStatusDialing,
            ChangeNotificationEventArgs.Notification.VoiceCallStatusIncoming
        };

        // It shows the result of executing API.
        // If an exception occurs, the result shows the message of exception.
        Label result;

        public CallPage()
        {
            // Create components
            // Create Label
            Label title = CreateTitle();

            // Add Button for getting preferred voice subscription
            var subsBtn = new Button
            {
                Text = "Get preferred voice subscription",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            subsBtn.Clicked += SubsBtn_Clicked;

            // Add Button for getting current call list
            var callListBtn = new Button
            {
                Text = "Get call list",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            callListBtn.Clicked += CallListBtn_Clicked;

            // Create Label
            // Set attribute for result label
            result = new Label()
            {
                BackgroundColor = Color.White,
                FontSize = 20,
            };
            result.Text = "Start Call API test";

            // Add all controls on layout for displaying on screen
            Content = new StackLayout
            {
                Children = {title, subsBtn, callListBtn, result}
            };

            try
            {
                // If not set slotHandle, can't get any info from Telephony API.
                // Should be initialized before executing the menu on CallPage.
                if (Globals.slotHandle == null)
                {
                    Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                    return;
                }

                Globals.slotHandle.ChangeNotification += SlotHandle_ChangeNotification;
                List<ChangeNotificationEventArgs.Notification> notiList = new List<ChangeNotificationEventArgs.Notification>();
                foreach (ChangeNotificationEventArgs.Notification noti in notiArr)
                {
                    notiList.Add(noti);
                }

                Globals.slotHandle.SetNotificationId(notiList);

                // For getting and using call information,
                // instance of Call Class should be needed.
                _call = new Call(Globals.slotHandle);
            }

            catch (Exception ex)
            {
                Log.Debug(Globals.LogTag, "Exception in call constructor: " + ex.ToString());
            }
        }

        private void SlotHandle_ChangeNotification(object sender, ChangeNotificationEventArgs e)
        {
            Log.Debug(Globals.LogTag, "Change notification: " + e.NotificationType);
        }

        /// <summary>
        /// Gets the list of the current existing calls.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void CallListBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                // For getting and using call information,
                // instance of Call Class should be needed.
                if (_call == null)
                {
                    Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                    return;
                }

                List<CallHandle> handleList = _call.GetCallHandleList().ToList();

                // If there is no established call included active calls and held calls,
                // GetCallHandleList() returns 0.
                if (handleList.Count == 0)
                {
                    result.Text = "Call handle list is empty";
                    return;
                }

                // From the call handle list,
                // can get some information alike below logs
                // HandleId is  an integer value of call handle index.
                // Number  is a phone number of other party.
                // Type is a call type to be used by applications (Voice, Data or Emergency).
                // Status is a call status, i.e Dialing, Active, Held, Incoming,
                // Direction is a direction of call whether the call is MO(Mobile Originated) call or MT(Mobile Terminated) call.
                // ConferenceStatus is whether the call is a conference call or not.
                foreach (CallHandle handle in handleList)
                {
                    // Shows the call information on dlog
                    Log.Debug(Globals.LogTag, "Handle Id: " + handle.HandleId + "\n Number: " + handle.Number + "\n Type: " + handle.Type);
                    Log.Debug(Globals.LogTag, "Status: " + handle.Status + "Direction: " + handle.Direction + "\n Conference Status: " + handle.ConferenceStatus);

                    // Shows the call information on result label
                    result.Text = "Handle Id: " + handle.HandleId + "\n Number: " + handle.Number + "\n Type: " + handle.Type + "\n Status: " 
                                  + handle.Status + "\n Direction: " + handle.Direction + "\n Conference Status: " + handle.ConferenceStatus + "\n";
                }
            }

            catch (Exception ex)
            {
                Log.Debug(Globals.LogTag, "Exception caught in getting call list: " + ex.ToString());
            }
        }
        /// <summary>
        /// Gets the current value for the preferred voice call subscription.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void SubsBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using call information,
            // instance of Call Class should be needed.
            if (_call == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }
            // Shows the Preferred voice subscription on dlog
            Log.Debug(Globals.LogTag, "Preferred voice subscription: " + _call.PreferredVoiceSubscription);

            // Shows the Preferred voice subscription on result label
            result.Text = "Preferred voice subscription: " + _call.PreferredVoiceSubscription;
        }

        /// <summary>
        /// Create a new Label component
        /// </summary>
        /// <returns>Label</returns>
        private Label CreateTitle()
        {
            return new Label()
            {
                Text = "Telephony/Call Test",
                TextColor = Color.White,
                FontSize = 28,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }
    }
}
