/*
 * Copyright (c) 2022 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using Tizen.Location.Geofence;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.Security;

namespace Geofence
{
    /// <summary>
    /// Represents NUI forms of tizen platform app
    /// </summary>
    public class Program : NUIApplication
    {
        /// <summary>
        /// GeofenceManager object.
        /// </summary>
        public static GeofenceManager Geofence { get; private set; } = null;

        /// <summary>
        /// VirtualPerimeter object.
        /// </summary>
        public static VirtualPerimeter Perimeter { get; private set; } = null;

        /// <summary>
        /// A navigation page.
        /// </summary>
        MainPage page = null;

        protected override void OnCreate()
        {
            base.OnCreate();
            Window window = Window.Instance;
            window.KeyEvent += OnKeyEvent;

            Navigator navigator = window.GetDefaultNavigator();

            if (Geofence == null)
            {
                // Create the GeofenceManager object
                Geofence = new GeofenceManager();
                // Create the VirtualPerimeter object
                Perimeter = new VirtualPerimeter(Geofence);
            }

            page = new MainPage();
            navigator.Push(page);

            // Check the privilege
            PrivilegeCheck();
        }

        /// <summary>
		/// Permission check 
		/// </summary>
		private void PrivilegeCheck()
        {
            try
            {
                /// Check location permission
                CheckResult result = PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/location");

                switch (result)
                {
                    case CheckResult.Allow:
                        break;
                    case CheckResult.Deny:
                        break;
                    case CheckResult.Ask:
                        /// Request to privacy popup
                        PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/location");
                        break;
                }
            }
            catch (Exception ex)
            {
                /// Exception handling
                page.GetInfoLabelList[1].Text = "[Status] Privilege : " + ex.Message;
            }
        }

        /// <summary>
        /// Handle when your app sleeps.
        /// </summary>
        protected override void OnPause()
        {
            // Check the permission for location privilege
            if (PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/location") == CheckResult.Allow)
            {
                // Remove the handle for GeofenceEventChanged
                Geofence.GeofenceEventChanged -= GeofenceEventChanged;
                // Remove the handle for StateChanged
                Geofence.StateChanged -= StateChanged;
                // Remove the handle for ProximityChanged
                Geofence.ProximityChanged -= ProximityChanged;
            }

            // Dispose the GeofenceManager object
            if (Geofence != null)
            {
                Geofence.Dispose();
            }

            Perimeter = null;
            Geofence = null;

            // Set the value to the labels
            page.GetInfoLabelList[0].Text = "GeofenceManager.Dispose";
            page.GetInfoLabelList[1].Text = "Success";
        }

        /// <summary>
        /// Handle when your app resumes.
        /// </summary>
        protected override void OnResume()
        {
            /// Set the value to label
            page.GetInfoLabelList[0].Text = "new GeofenceManager and VirtualPerimeter";
            try
            {
                if (Geofence == null)
                {
                    // Create the GeofenceManager object
                    Geofence = new GeofenceManager();
                    // Create the VirtualPerimeter object
                    Perimeter = new VirtualPerimeter(Geofence);
                }

                // Set the value to label
                page.GetInfoLabelList[1].Text = "Success";

                // Check the permission for location privilege
                if (PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/location") == CheckResult.Allow)
                {
                    // Add a handle for GeofenceEventChanged
                    Geofence.GeofenceEventChanged += GeofenceEventChanged;
                    // Add a handle for StateChanged
                    Geofence.StateChanged += StateChanged;
                    // Add a handle for ProximityChanged
                    Geofence.ProximityChanged += ProximityChanged;
                }
            }
            catch (Exception e)
            {
                // Set the value to label about occured exception
                page.GetInfoLabelList[1].Text = e.Message;
            }
        }

        /// <summary>
        /// Handle when GeofenceEventChanged event is occured.
        /// </summary>
        /// <param name="sender">Specifies the sender of this event</param>
        /// <param name="args">Specifies the information of this event</param>
        public void GeofenceEventChanged(object sender, GeofenceResponseEventArgs args)
        {
            // Set the value to label about an occured event
            page.GetInfoLabelList[0].Text = args.EventType.ToString();
            // Set the value to label about an occured error
            switch (args.ErrorCode)
            {
                case GeofenceError.None:
                    page.GetInfoLabelList[1].Text = "None";
                    break;
                case GeofenceError.OutOfMemory:
                    page.GetInfoLabelList[1].Text = "OutOfMemory";
                    break;
                case GeofenceError.InvalidParameter:
                    page.GetInfoLabelList[1].Text = "InvalidParameter";
                    break;
                case GeofenceError.PermissionDenied:
                    page.GetInfoLabelList[1].Text = "PermissionDenied";
                    break;
                case GeofenceError.NotSupported:
                    page.GetInfoLabelList[1].Text = "NotSupported";
                    break;
                case GeofenceError.NotInitialized:
                    page.GetInfoLabelList[1].Text = "NotInitialized";
                    break;
                case GeofenceError.InvalidID:
                    page.GetInfoLabelList[1].Text = "InvalidID";
                    break;
                case GeofenceError.Exception:
                    page.GetInfoLabelList[1].Text = "Exception";
                    break;
                case GeofenceError.AlreadyStarted:
                    page.GetInfoLabelList[1].Text = "AlreadyStarted";
                    break;
                case GeofenceError.TooManyGeofence:
                    page.GetInfoLabelList[1].Text = "TooManyGeofence";
                    break;
                case GeofenceError.IPC:
                    page.GetInfoLabelList[1].Text = "IPC Error";
                    break;
                case GeofenceError.DBFailed:
                    page.GetInfoLabelList[1].Text = "DBFailed";
                    break;
                case GeofenceError.PlaceAccessDenied:
                    page.GetInfoLabelList[1].Text = "PlaceAccessDenied";
                    break;
                case GeofenceError.GeofenceAccessDenied:
                    page.GetInfoLabelList[1].Text = "GeofenceAccessDenied";
                    break;
                default:
                    page.GetInfoLabelList[1].Text = "Unknown Error";
                    break;
            }
        }

        /// <summary>
        /// Handle when GeofenceStateEventArgs event is occured.
        /// </summary>
        /// <param name="sender">Specifies the sender of this event</param>
        /// <param name="args">Specifies the information of this event</param>
        public void StateChanged(object sender, GeofenceStateEventArgs args)
        {
            // Set the value to label about a changed geofence state
            page.GetInfoLabelList[2].Text = "FenceID: " + args.GeofenceId.ToString() + ", GeofenceState: ";
            switch (args.State)
            {
                case GeofenceState.In:
                    page.GetInfoLabelList[2].Text += "In";
                    break;
                case GeofenceState.Out:
                    page.GetInfoLabelList[2].Text += "Out";
                    break;
                default:
                    page.GetInfoLabelList[2].Text += "Uncertain";
                    break;
            }
        }

        /// <summary>
        /// Handle when ProximityChanged event is occured.
        /// </summary>
        /// <param name="sender">Specifies the sender of this event</param>
        /// <param name="args">Specifies the information of this event</param>
        public void ProximityChanged(object sender, ProximityStateEventArgs args)
        {
            // Set the value to label about a changed proximity state
            page.GetInfoLabelList[3].Text = "FenceID: " + args.GeofenceId.ToString() + ", ProximityState: ";
            switch (args.State)
            {
                case ProximityState.Immediate:
                    page.GetInfoLabelList[3].Text += "Immediate";
                    break;
                case ProximityState.Near:
                    page.GetInfoLabelList[3].Text += "Near";
                    break;
                case ProximityState.Far:
                    page.GetInfoLabelList[3].Text += "Far";
                    break;
                default:
                    page.GetInfoLabelList[3].Text += "Uncertain";
                    break;
            }
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
