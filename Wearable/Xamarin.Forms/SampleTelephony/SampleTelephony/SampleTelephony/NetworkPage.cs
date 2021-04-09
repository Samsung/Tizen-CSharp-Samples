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
using System.Collections.Generic;
using Xamarin.Forms;
using Tizen;
using Tizen.Telephony;
using Tizen.Wearable.CircularUI.Forms;

namespace SampleTelephony
{
    /// <summary>
    /// This class provides APIs to obtain information about the current telephony service network.
    /// For getting the changed value immediately,
    /// should be set notification callbacks for each notification type
    /// </summary>
    public class NetworkPage : CirclePage
    {
        private static Network _network = null;
        private ChangeNotificationEventArgs.Notification[] notiArr =
        {
            ChangeNotificationEventArgs.Notification.NetworkBsId,
            ChangeNotificationEventArgs.Notification.NetworkBsLatitude,
            ChangeNotificationEventArgs.Notification.NetworkBsLongitude,
            ChangeNotificationEventArgs.Notification.NetworkCellid,
            ChangeNotificationEventArgs.Notification.NetworkDefaultDataSubscription,
            ChangeNotificationEventArgs.Notification.NetworkDefaultSubscription,
            ChangeNotificationEventArgs.Notification.NetworkId,
            ChangeNotificationEventArgs.Notification.NetworkLac,
            ChangeNotificationEventArgs.Notification.NetworkNetworkName,
            ChangeNotificationEventArgs.Notification.NetworkPsType,
            ChangeNotificationEventArgs.Notification.NetworkRoamingStatus,
            ChangeNotificationEventArgs.Notification.NetworkServiceState,
            ChangeNotificationEventArgs.Notification.NetworkSignalstrengthLevel,
            ChangeNotificationEventArgs.Notification.NetworkSystemId,
            ChangeNotificationEventArgs.Notification.NetworkTac
        };

        // It shows the result of executing API.
        // If an exception occurs, the result shows the message of exception.
        Label result;

        public NetworkPage()
        {
            // Create components
            // Create Label
            Label title = CreateTitle();

            // Add Button for getting Cell ID
            var cellBtn = new Button
            {
                Text = "Get cell ID",
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            cellBtn.Clicked += CellBtn_Clicked;

            // Add Button for getting LAC
            var lacBtn = new Button
            {
                Text = "Get LAC",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            lacBtn.Clicked += LacBtn_Clicked;

            // Add Button for getting MCC
            var mccBtn = new Button
            {
                Text = "Get MCC",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            mccBtn.Clicked += MccBtn_Clicked;

            // Add Button for getting MNC
            var mncBtn = new Button
            {
                Text = "Get MNC",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            mncBtn.Clicked += MncBtn_Clicked;

            // Add Button for getting Network Name
            var nwNameBtn = new Button
            {
                Text = "Get network name",
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            nwNameBtn.Clicked += NwNameBtn_Clicked;

            // Add Button for getting Network Name Option
            var nwNameOptionBtn = new Button
            {
                Text = "Get network name option",
                FontSize = 9,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            nwNameOptionBtn.Clicked += NwNameOptionBtn_Clicked;

            // Add Button for getting roaming status
            var roamingStatusBtn = new Button
            {
                Text = "Get roaming status",
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            roamingStatusBtn.Clicked += RoamingStatusBtn_Clicked;

            // Add Button for getting RSSI
            var rssiBtn = new Button
            {
                Text = "Get RSSI",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            rssiBtn.Clicked += RssiBtn_Clicked;

            // Add Button for getting Service State
            var serviceStateBtn = new Button
            {
                Text = "Get service state",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            serviceStateBtn.Clicked += ServiceStateBtn_Clicked;

            // Add Button for getting Network Type
            var typeBtn = new Button
            {
                Text = "Get network type",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            typeBtn.Clicked += TypeBtn_Clicked;

            // Add Button for getting Packet Service Type
            var psTypeBtn = new Button
            {
                Text = "Get PS type",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            psTypeBtn.Clicked += PsTypeBtn_Clicked;

            // Add Button for getting default data subscription type
            var dataSubsBtn = new Button
            {
                Text = "Get default data subscription",
                FontSize = 8,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            dataSubsBtn.Clicked += DataSubsBtn_Clicked;

            // Add Button for getting default subscription type
            var defaultSubsBtn = new Button
            {
                Text = "Get default subscription",
                FontSize = 10,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            defaultSubsBtn.Clicked += DefaultSubsBtn_Clicked;

            // Add Button for getting Network Selection Mode
            var selectionModeBtn = new Button
            {
                Text = "Get selection mode",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            selectionModeBtn.Clicked += SelectionModeBtn_Clicked;

            // Add Button for getting TAC
            var tacBtn = new Button
            {
                Text = "Get TAC",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            tacBtn.Clicked += TacBtn_Clicked;

            // Add Button for getting System ID
            var systemIdBtn = new Button
            {
                Text = "Get system ID",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            systemIdBtn.Clicked += SystemIdBtn_Clicked;

            // Add Button for getting Network ID
            var nwIdBtn = new Button
            {
                Text = "Get network ID",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            nwIdBtn.Clicked += NwIdBtn_Clicked;

            // Add Button for getting base station ID
            var baseStationIdBtn = new Button
            {
                Text = "Get base station ID",
                FontSize = 10,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            baseStationIdBtn.Clicked += BaseStationIdBtn_Clicked;

            // Add Button for getting base station Longitude
            var baseStationLongitudeBtn = new Button
            {
                Text = "Get base station longitude",
                FontSize = 9,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            baseStationLongitudeBtn.Clicked += BaseStationLongitudeBtn_Clicked;

            // Add Button for getting base station Latitude
            var baseStationLatitudeBtn = new Button
            {
                Text = "Get base station latitude",
                FontSize = 9,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Add Clicked events
            baseStationLatitudeBtn.Clicked += BaseStationLatitudeBtn_Clicked;

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
            result.Text = "Start Network API test";

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
                    Children =
                    {
                        start_temp, title, cellBtn, lacBtn, mccBtn, mncBtn, nwNameBtn, nwNameOptionBtn, roamingStatusBtn, rssiBtn,
                        serviceStateBtn, typeBtn, psTypeBtn, dataSubsBtn, defaultSubsBtn, selectionModeBtn, tacBtn,
                        systemIdBtn, nwIdBtn, baseStationIdBtn, baseStationLatitudeBtn, baseStationLongitudeBtn, result, temp,
                    }
                }
            };
            Content = sv;
            RotaryFocusObject = sv;

            try
            {
                // If not set slotHandle, can't get any info from Telephony API.
                // Should be initialized before executing the menu on NetworkPage.
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

                // For getting and using Network information,
                // instance of Network Class should be needed.
                _network = new Network(Globals.slotHandle);
            }

            catch (Exception ex)
            {
                Log.Debug(Globals.LogTag, "Exception in network constructor: " + ex.ToString());
            }
        }

        private void SlotHandle_ChangeNotification(object sender, ChangeNotificationEventArgs e)
        {
            Log.Debug(Globals.LogTag, "Change notification: " + e.NotificationType);
        }

        /// <summary>
        /// Gets the base station latitude of the current location.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void BaseStationLatitudeBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the base station latitude on dlog
            Log.Debug(Globals.LogTag, "Base station latitude: " + _network.BaseStationLatitude);

            // Shows the the base station latitude on result label
            result.Text = "Base station latitude: " + _network.BaseStationLatitude;
        }

        /// <summary>
        /// Gets the base station longitude of the current location.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void BaseStationLongitudeBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the base station longitude on dlog
            Log.Debug(Globals.LogTag, "Base Station longitude: " + _network.BaseStationLongitude);

            // Shows the the base station longitude on result label
            result.Text = "Base Station longitude: " + _network.BaseStationLongitude;
        }

        /// <summary>
        /// Gets the base station ID of the current location.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void BaseStationIdBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Base station ID on dlog
            Log.Debug(Globals.LogTag, "Base station ID: " + _network.BaseStationId);

            // Shows the the Base station ID on result label
            result.Text = "Base station ID: " + _network.BaseStationId;
        }

        /// <summary>
        /// Gets the network ID of the current location.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void NwIdBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Network ID on dlog
            Log.Debug(Globals.LogTag, "Network ID: " + _network.NetworkId);

            // Shows the Network ID on result label
            result.Text = "Network ID: " + _network.NetworkId;
        }

        /// <summary>
        /// Gets the system ID of the current location.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void SystemIdBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Network System ID on dlog
            Log.Debug(Globals.LogTag, "System ID: " + _network.SystemId);

            // Shows the Network System ID on result label
            result.Text = "System ID: " + _network.SystemId;
        }

        /// <summary>
        /// Gets the TAC (Tracking Area Code) of the current location.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void TacBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the TAC on dlog
            Log.Debug(Globals.LogTag, "TAC: " + _network.Tac);

            // Shows the TAC(Tracking Area Code) on result label
            result.Text = "Tracking Area Code: " + _network.Tac;
        }

        /// <summary>
        /// Gets the network selection mode.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void SelectionModeBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Network Selection Mode on dlog
            Log.Debug(Globals.LogTag, "Network Selection mode: " + _network.NetworkSelectionMode);

            // Shows the Network Selection Mode on result label
            result.Text = "Network Selection mode: " + _network.NetworkSelectionMode;
        }

        /// <summary>
        /// Gets the current default subscription for the voice service (Circuit Switched).
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void DefaultSubsBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Network default subscription mode on dlog
            Log.Debug(Globals.LogTag, "Network default subscription: " + _network.NetworkDefaultSubscription);

            // Shows the Network default subscription mode on result label
            result.Text = "Network default subscription: " + _network.NetworkDefaultSubscription;
        }

        /// <summary>
        /// Gets the current default subscription for the data service (Packet Switched).
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void DataSubsBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Network default data subscription mode on dlog
            Log.Debug(Globals.LogTag, "Default data subscription: " + _network.NetworkDefaultDataSubscription);

            // Shows the Network default data subscription mode on result label
            result.Text = "Default data subscription: " + _network.NetworkDefaultDataSubscription;
        }

        /// <summary>
        /// Gets the packet service type of the current registered network.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void PsTypeBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Network packet service type on dlog
            Log.Debug(Globals.LogTag, "Network PS type: " + _network.NetworkPsType);

            // Shows the Network packet service type on result label
            result.Text = "Network PS type: " + _network.NetworkPsType;
        }

        /// <summary>
        /// Gets the network service type of the current registered network.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void TypeBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Network service type on dlog
            Log.Debug(Globals.LogTag, "Network type: " + _network.NetworkType);

            // Shows the Network service type on result label
            result.Text = "Network type: " + _network.NetworkType;
        }

        /// <summary>
        /// Gets the current network state of the telephony service.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void ServiceStateBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Network service state on dlog
            Log.Debug(Globals.LogTag, "Network service state: " + _network.NetworkServiceState);

            // Shows the  Network service state on result label
            result.Text = "Network service state: " + _network.NetworkServiceState;
        }

        /// <summary>
        /// Gets the RSSI (Received Signal Strength Indicator).
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void RssiBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the current RSSI level on dlog
            Log.Debug(Globals.LogTag, "Current RSSI level: " + _network.CurrentRssi);

            // Shows the  current RSSI level on result label
            result.Text = "Current RSSI level: " + _network.CurrentRssi;
        }

        /// <summary>
        /// Gets the roaming state of the current registered network.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void RoamingStatusBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Roaming status on dlog
            Log.Debug(Globals.LogTag, "Roaming status: " + _network.RoamingStatus);

            // Shows the Roaming status on result label
            result.Text = "Roaming status: " + _network.RoamingStatus;
        }

        /// <summary>
        /// Gets the network name option of the current registered network.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void NwNameOptionBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Network name option on dlog
            Log.Debug(Globals.LogTag, "Network name option: " + _network.NetworkNameOption);

            // Shows the Network name option on result label
            result.Text = "Network name option: " + _network.NetworkNameOption;
        }

        /// <summary>
        /// Gets the name of the current registered network.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void NwNameBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Network name on dlog
            Log.Debug(Globals.LogTag, "Network name: " + _network.NetworkName);

            // Shows the  Network name on result label
            result.Text = "Network name: " + _network.NetworkName;
        }

        /// <summary>
        /// Gets the MNC (Mobile Network Code) of the current registered network.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void MncBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the MNC on dlog
            Log.Debug(Globals.LogTag, "MNC: " + _network.Mnc);

            // Shows the MNC on result label
            result.Text = "MNC: " + _network.Mnc;
        }

        /// <summary>
        /// Gets the MCC (Mobile Country Code) of the current registered network.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void MccBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the MCC on dlog
            Log.Debug(Globals.LogTag, "MCC: " + _network.Mcc);

            // Shows the MCC on result label
            result.Text = "MCC: " + _network.Mcc;
        }

        /// <summary>
        /// Gets the LAC (Location Area Code) of the current location.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void LacBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the LAC on dlog
            Log.Debug(Globals.LogTag, "LAC: " + _network.Lac);

            // Shows the LAC on result label
            result.Text = "LAC: " + _network.Lac;
        }

        /// <summary>
        /// Gets the cell ID of the current location.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void CellBtn_Clicked(object sender, EventArgs e)
        {
            // For getting and using Network information,
            // instance of Network Class should be needed.
            if (_network == null)
            {
                Log.Debug(Globals.LogTag, "Telephony is not initialized/there are no SIM slot handles");
                return;
            }

            // Shows the Cell ID on dlog
            Log.Debug(Globals.LogTag, "Cell ID: " + _network.CellId);

            // Shows the cell ID on result label
            result.Text = "Cell ID: " + _network.CellId;
        }

        /// <summary>
        /// Create a new Label component
        /// </summary>
        /// <returns>Label</returns>
        private Label CreateTitle()
        {
            return new Label()
            {
                Text = "Telephony/Network Test",
                TextColor = Color.White,
                FontSize = 9,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }
    }
}
