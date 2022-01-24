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
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Tizen.Location.Geofence;
using Tizen.Security;

namespace Geofence.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// GeofenceManager object.
        /// </summary>
        static GeofenceManager geofence;

        /// <summary>
        /// VirtualPerimeter object.
        /// </summary>
        static VirtualPerimeter perimeter;

        private string firstTextLabel;
        private string secondTextLabel;
        private string thirdTextLabel;
        private string fourthTextLabel;
        public ICommand AddPlace { get; private set; }
        public ICommand RemovePlace { get; private set; }
        public ICommand AddFence { get; private set; }
        public ICommand RemoveFence { get; private set; }
        public ICommand Start { get; private set; }
        public ICommand Stop { get; private set; }
        public ICommand UpdatePlace { get; private set; }
        public ICommand FenceStatus { get; private set; }

        public string FirstTextLabel
        {
            get => firstTextLabel;
            set
            {
                if (firstTextLabel != value)
                {
                    firstTextLabel = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string SecondTextLabel
        {
            get => secondTextLabel;
            set
            {
                if (secondTextLabel != value)
                {
                    secondTextLabel = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string ThirdTextLabel
        {
            get => thirdTextLabel;
            set
            {
                if (thirdTextLabel != value)
                {
                    thirdTextLabel = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string FourthTextLabel
        {
            get => fourthTextLabel;
            set
            {
                if (fourthTextLabel != value)
                {
                    fourthTextLabel = value;
                    RaisePropertyChanged();
                }
            }
        }

        public MainPageViewModel()
        {
            geofence = null;
            perimeter = null;
            firstTextLabel = "";
            PrivilegeCheck();
            /// Set the value to label
            FirstTextLabel = "new GeofenceManager and VirtualPerimeter";
            try
            {
                if (geofence == null)
                {
                    // Create the GeofenceManager object
                    geofence = new GeofenceManager();
                    // Create the VirtualPerimeter object
                    perimeter = new VirtualPerimeter(geofence);
                }

                // Set the value to label
                SecondTextLabel = "Success";

                // Check the permission for location privilege
                if (PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/location") == CheckResult.Allow)
                {
                    // Add a handle for GeofenceEventChanged
                    geofence.GeofenceEventChanged += GeofenceEventChanged;
                    // Add a handle for StateChanged
                    geofence.StateChanged += StateChanged;
                    // Add a handle for ProximityChanged
                    geofence.ProximityChanged += ProximityChanged;
                }
            }
            catch (Exception e)
            {
                // Set the value to label about occured exception
                SecondTextLabel = e.Message;
            }
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
                SecondTextLabel = "[Status] Privilege : " + ex.Message;
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
            FirstTextLabel = args.EventType.ToString();
            // Set the value to label about an occured error
            switch (args.ErrorCode)
            {
                case GeofenceError.None:
                    SecondTextLabel = "None";
                    break;
                case GeofenceError.OutOfMemory:
                    SecondTextLabel = "OutOfMemory";
                    break;
                case GeofenceError.InvalidParameter:
                    SecondTextLabel = "InvalidParameter";
                    break;
                case GeofenceError.PermissionDenied:
                    SecondTextLabel = "PermissionDenied";
                    break;
                case GeofenceError.NotSupported:
                    SecondTextLabel = "NotSupported";
                    break;
                case GeofenceError.NotInitialized:
                    SecondTextLabel = "NotInitialized";
                    break;
                case GeofenceError.InvalidID:
                    SecondTextLabel = "InvalidID";
                    break;
                case GeofenceError.Exception:
                    SecondTextLabel = "Exception";
                    break;
                case GeofenceError.AlreadyStarted:
                    SecondTextLabel = "AlreadyStarted";
                    break;
                case GeofenceError.TooManyGeofence:
                    SecondTextLabel = "TooManyGeofence";
                    break;
                case GeofenceError.IPC:
                    SecondTextLabel = "IPC Error";
                    break;
                case GeofenceError.DBFailed:
                    SecondTextLabel = "DBFailed";
                    break;
                case GeofenceError.PlaceAccessDenied:
                    SecondTextLabel = "PlaceAccessDenied";
                    break;
                case GeofenceError.GeofenceAccessDenied:
                    SecondTextLabel = "GeofenceAccessDenied";
                    break;
                default:
                    SecondTextLabel = "Unknown Error";
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
            FourthTextLabel = "FenceID: " + args.GeofenceId.ToString() + ", GeofenceState: ";
            switch (args.State)
            {
                case GeofenceState.In:
                    ThirdTextLabel += "In";
                    break;
                case GeofenceState.Out:
                    ThirdTextLabel += "Out";
                    break;
                default:
                    ThirdTextLabel += "Uncertain";
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
            FourthTextLabel = "FenceID: " + args.GeofenceId.ToString() + ", ProximityState: ";
            switch (args.State)
            {
                case ProximityState.Immediate:
                    FourthTextLabel += "Immediate";
                    break;
                case ProximityState.Near:
                    FourthTextLabel += "Near";
                    break;
                case ProximityState.Far:
                    FourthTextLabel += "Far";
                    break;
                default:
                    FourthTextLabel += "Uncertain";
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
