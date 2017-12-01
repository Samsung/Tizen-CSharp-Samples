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
using Xamarin.Forms;
using Tizen;
using Tizen.Telephony;

namespace SampleTelephony
{
    /// <summary>
    /// This class provides APIs that allow you to extract the information stored on a SIM card.
    /// </summary>
    public class SimPage : ContentPage
    {
        private static Sim _sim = null;
        private ChangeNotificationEventArgs.Notification[] notiArr =
        {
            ChangeNotificationEventArgs.Notification.SimStatus,
            ChangeNotificationEventArgs.Notification.SimCallForwardingIndicatorState
        };

        // It shows the result of executing API.
        // If an exception occurs, the result shows the message of exception.
        Label result;

        public SimPage()
        {
            // Create components
            // Create Label
            Label title = CreateTitle();

            // Add Button for getting SIM status
            var changedBtn = new Button
            {
                Text = "Is SIM changed",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            changedBtn.Clicked += ChangedBtn_Clicked;

            // Add Button for getting Operator information on SIM
            var operatorBtn = new Button
            {
                Text = "Get operator",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            operatorBtn.Clicked += OperatorBtn_Clicked;

            // Add Button for getting ICCID
            var iccIdBtn = new Button
            {
                Text = "Get ICCID",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            iccIdBtn.Clicked += IccIdBtn_Clicked;

            // Add Button for getting MSIN
            var msinBtn = new Button
            {
                Text = "Get MSIN",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            msinBtn.Clicked += MsinBtn_Clicked;

            // Add Button for getting SPN
            var spnBtn = new Button
            {
                Text = "Get SPN",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            spnBtn.Clicked += SpnBtn_Clicked;

            // Add Button for getting SIM State
            var stateBtn = new Button
            {
                Text = "Get SIM state",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            stateBtn.Clicked += StateBtn_Clicked;

            // Add Button for getting application list offered by SIM
            var appListBtn = new Button
            {
                Text = "Get SIM application list",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            appListBtn.Clicked += AppListBtn_Clicked;

            // Add Button for getting subscriber number
            var subscriberBtn = new Button
            {
                Text = "Get subscriber number",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            subscriberBtn.Clicked += SubscriberBtn_Clicked;

            // Add Button for getting subscriber ID
            var subscriberIdBtn = new Button
            {
                Text = "Get subscriber ID",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            subscriberIdBtn.Clicked += SubscriberIdBtn_Clicked;

            // Add Button for getting SIM Lock State
            var lockStateBtn = new Button
            {
                Text = "Get lock state",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            lockStateBtn.Clicked += LockStateBtn_Clicked;

            // Add Button for getting group ID
            var groupIdBtn = new Button
            {
                Text = "Get group ID1",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            groupIdBtn.Clicked += GroupIdBtn_Clicked;

            // Add Button for getting call forwarding indicator state
            var cfIndiStateBtn = new Button
            {
                Text = "Get call forwarding indicator state",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            cfIndiStateBtn.Clicked += CfIndiStateBtn_Clicked;

            // Create Label
            // Set attribute for result label
            result = new Label()
            {
                BackgroundColor = Color.White,
                FontSize = 20,
            };
            result.Text = "Start SIM API test";

            //add all buttons on layout
            Content = new StackLayout
            {
                Children =
                {
                        title, changedBtn, operatorBtn, iccIdBtn, msinBtn, spnBtn, stateBtn, appListBtn, subscriberBtn,
                        subscriberIdBtn, lockStateBtn, groupIdBtn, cfIndiStateBtn, result
                }
            };

            try
            {
                // If not set slotHandle, can't get any info from Telephony API.
                // Should be initialized before executing the menu on SIMPage.
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

                // For getting and using SIM information,
                // instance of SIM Class should be needed.
                _sim = new Sim(Globals.slotHandle);
            }

            catch (Exception ex)
            {
                Log.Debug(Globals.LogTag, "Exception in SIM constructor: " + ex.ToString());
            }
        }

        private void SlotHandle_ChangeNotification(object sender, ChangeNotificationEventArgs e)
        {
            Log.Debug(Globals.LogTag, "Change notification: " + e.NotificationType);
        }

        /// <summary>
        /// Gets the call forwarding indicator state of the SIM.
        /// If the state is true, the incoming call will be forwarded to the selected number. 
        /// State indicates the CFU (Call Forwarding Unconditional) indicator status - Voice (3GPP TS 31.102 4.2.64 EF CFIS).
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void CfIndiStateBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using SIM information,
            // instance of SIM Class should be needed.
            if (_sim == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the call forwarding indicator state on dlog
            Log.Debug(Globals.LogTag, "Call forwarding indicator state: " + _sim.CallForwardingIndicatorState);

            // Shows the call forwarding indicator state on result label
            result.Text = "Call forwarding indicator state: " + _sim.CallForwardingIndicatorState;

        }

        /// <summary>
        /// Gets the GID1 (Group Identifier Level 1).
        /// Gets Group Identifier Level 1 (GID1) embedded in the SIM card.
        /// If this value is not stored in SIM card, an empty string will be returned.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void GroupIdBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using SIM information,
            // instance of SIM Class should be needed.
            if (_sim == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the group ID level1 on dlog
            Log.Debug(Globals.LogTag, "Group ID level1: " + _sim.GroupId1);

            // Shows the group ID level1 on result label
            result.Text = "Group ID level1: " + _sim.GroupId1;
        }

        /// <summary>
        /// Gets the lock state of the SIM.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void LockStateBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using SIM information,
            // instance of SIM Class should be needed.
            if (_sim == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the SIM lock state on dlog
            Log.Debug(Globals.LogTag, "SIM lock state: " + _sim.CurrentLockState);

            // Shows the SIM lock state on result label
            result.Text = "SIM lock state: " + _sim.CurrentLockState;
        }

        /// <summary>
        /// Gets the subscriber ID.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void SubscriberIdBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using SIM information,
            // instance of SIM Class should be needed.
            if (_sim == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the subscriber ID on dlog
            Log.Debug(Globals.LogTag, "Subscriber ID: " + _sim.SubscriberId);

            // Shows the subscriber ID on result label
            result.Text = "Subscriber ID: " + _sim.SubscriberId;
        }

        /// <summary>
        /// Gets the subscriber number embedded in the SIM card.
        /// This value contains the MSISDN related to the subscriber.
        /// If this value is not stored in SIM card, an empty string will be returned.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void SubscriberBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using SIM information,
            // instance of SIM Class should be needed.
            if (_sim == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the subscriber number on dlog
            Log.Debug(Globals.LogTag, "Subscriber Number: " + _sim.SubscriberNumber);

            // Shows the subscriber number on result label
            result.Text = "Subscriber Number: " + _sim.SubscriberNumber;
        }

        /// <summary>
        /// Gets the count of an application on the UICC.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void AppListBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using SIM information,
            // instance of SIM Class should be needed.
            if (_sim == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the application list on dlog
            Log.Debug(Globals.LogTag, "Application list: " + _sim.ApplicationList);

            // Shows the application list on result label
            result.Text = "Application list: " + _sim.ApplicationList;
        }

        /// <summary>
        /// Gets the state of the SIM.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void StateBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using SIM information,
            // instance of SIM Class should be needed.
            if (_sim == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the SIM state on dlog
            Log.Debug(Globals.LogTag, "SIM state: " + _sim.CurrentState);

            // Shows the SIM state on result label
            result.Text = "SIM state: " + _sim.CurrentState;
        }

        /// <summary>
        /// Gets the Service Provider Name (SPN) of the SIM card.
        /// Gets Service Provider Name embedded in the SIM card.
        /// If this value is not stored in the SIM card, an empty string will be returned.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void SpnBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using SIM information,
            // instance of SIM Class should be needed.
            if (_sim == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the SPN on dlog
            Log.Debug(Globals.LogTag, "SPN: " + _sim.Spn);

            // Shows the SPN on result label
            result.Text = "SPN: " + _sim.Spn;
        }

        /// <summary>
        /// Gets the Mobile Subscription Identification Number (MSIN [9~10 digits]) of the SIM provider.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void MsinBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using SIM information,
            // instance of SIM Class should be needed.
            if (_sim == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the MSIN on dlog
            Log.Debug(Globals.LogTag, "MSIN: " + _sim.Msin);

            // Shows the MSIN on result label
            result.Text = "MSIN: " + _sim.Msin;
        }

        /// <summary>
        /// Gets the Integrated Circuit Card IDentification (ICC-ID).
        /// The Integrated Circuit Card Identification number internationally identifies SIM cards.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void IccIdBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using SIM information,
            // instance of SIM Class should be needed.
            if (_sim == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the ICCID on dlog
            Log.Debug(Globals.LogTag, "ICCID: " + _sim.IccId);

            // Shows the ICCID on result label
            result.Text = "ICCID: " + _sim.IccId;
        }

        /// <summary>
        /// Gets the SIM Operator (MCC [3 digits] + MNC [2~3 digits]).
        /// The Operator is embedded in the SIM card.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void OperatorBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using SIM information,
            // instance of SIM Class should be needed.
            if (_sim == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the network operator on dlog
            Log.Debug(Globals.LogTag, "Operator: " + _sim.Operator);

            // Shows the network operator on result label
            result.Text = "Operator: " + _sim.Operator;
        }

        /// <summary>
        /// Checks whether the current SIM card is different from the previous SIM card.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void ChangedBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using SIM information,
            // instance of SIM Class should be needed.
            if (_sim == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Is SIM changed flag on dlog
            Log.Debug(Globals.LogTag, "Is SIM changed: " + _sim.IsChanged);

            // Shows the Is SIM changed flag on result label
            result.Text = "Is SIM changed: " + _sim.IsChanged;
        }

        /// <summary>
        /// Create a new Label component
        /// </summary>
        /// <returns>Label</returns>
        private Label CreateTitle()
        {
            return new Label()
            {
                Text = "Telephony/SIM Test",
                TextColor = Color.White,
                FontSize = 28,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }
    }
}
