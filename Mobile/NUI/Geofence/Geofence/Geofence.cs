using System;
using System.Collections.Generic;
using Tizen.Location.Geofence;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.Security;

namespace Geofence
{
    public class Program : NUIApplication
    {
        /// <summary>
        /// GeofenceManager object.
        /// </summary>
        static GeofenceManager geofence;

        /// <summary>
        /// VirtualPerimeter object.
        /// </summary>
        static VirtualPerimeter perimeter;

        static MainPage page;
        Navigator navigator;

        protected override void OnCreate()
        {
            base.OnCreate();
            Window window = Window.Instance;
            window.BackgroundColor = Color.Blue;
            window.KeyEvent += OnKeyEvent;

            navigator = window.GetDefaultNavigator();

            page = new MainPage();
            navigator.Push(page);

            PrivilegeCheck();
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
                geofence.GeofenceEventChanged -= GeofenceEventChanged;
                // Remove the handle for StateChanged
                geofence.StateChanged -= StateChanged;
                // Remove the handle for ProximityChanged
                geofence.ProximityChanged -= ProximityChanged;
            }

            // Dispose the GeofenceManager object
            if (geofence != null)
            {
                geofence.Dispose();
            }

            perimeter = null;
            geofence = null;

            // Set the value to the labels
            page.FirstTextLabel.Text = "GeofenceManager.Dispose";
            page.SecondTextLabel.Text = "Success";
        }

        /// <summary>
        /// Handle when your app resumes.
        /// </summary>
        protected override void OnResume()
        {
            /// Set the value to label
            page.FirstTextLabel.Text = "new GeofenceManager and VirtualPerimeter";
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
                page.SecondTextLabel.Text = "Success";

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
                page.SecondTextLabel.Text = e.Message;
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
            page.FirstTextLabel.Text = args.EventType.ToString();
            // Set the value to label about an occured error
            switch (args.ErrorCode)
            {
                case GeofenceError.None:
                    page.SecondTextLabel.Text = "None";
                    break;
                case GeofenceError.OutOfMemory:
                    page.SecondTextLabel.Text = "OutOfMemory";
                    break;
                case GeofenceError.InvalidParameter:
                    page.SecondTextLabel.Text = "InvalidParameter";
                    break;
                case GeofenceError.PermissionDenied:
                    page.SecondTextLabel.Text = "PermissionDenied";
                    break;
                case GeofenceError.NotSupported:
                    page.SecondTextLabel.Text = "NotSupported";
                    break;
                case GeofenceError.NotInitialized:
                    page.SecondTextLabel.Text = "NotInitialized";
                    break;
                case GeofenceError.InvalidID:
                    page.SecondTextLabel.Text = "InvalidID";
                    break;
                case GeofenceError.Exception:
                    page.SecondTextLabel.Text = "Exception";
                    break;
                case GeofenceError.AlreadyStarted:
                    page.SecondTextLabel.Text = "AlreadyStarted";
                    break;
                case GeofenceError.TooManyGeofence:
                    page.SecondTextLabel.Text = "TooManyGeofence";
                    break;
                case GeofenceError.IPC:
                    page.SecondTextLabel.Text = "IPC Error";
                    break;
                case GeofenceError.DBFailed:
                    page.SecondTextLabel.Text = "DBFailed";
                    break;
                case GeofenceError.PlaceAccessDenied:
                    page.SecondTextLabel.Text = "PlaceAccessDenied";
                    break;
                case GeofenceError.GeofenceAccessDenied:
                    page.SecondTextLabel.Text = "GeofenceAccessDenied";
                    break;
                default:
                    page.SecondTextLabel.Text = "Unknown Error";
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
            page.ThirdTextLabel.Text = "FenceID: " + args.GeofenceId.ToString() + ", GeofenceState: ";
            switch (args.State)
            {
                case GeofenceState.In:
                    page.ThirdTextLabel.Text += "In";
                    break;
                case GeofenceState.Out:
                    page.ThirdTextLabel.Text += "Out";
                    break;
                default:
                    page.ThirdTextLabel.Text += "Uncertain";
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
            page.FourthTextLabel.Text = "FenceID: " + args.GeofenceId.ToString() + ", ProximityState: ";
            switch (args.State)
            {
                case ProximityState.Immediate:
                    page.FourthTextLabel.Text += "Immediate";
                    break;
                case ProximityState.Near:
                    page.FourthTextLabel.Text += "Near";
                    break;
                case ProximityState.Far:
                    page.FourthTextLabel.Text += "Far";
                    break;
                default:
                    page.FourthTextLabel.Text += "Uncertain";
                    break;
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
                page.SecondTextLabel.Text = "[Status] Privilege : " + ex.Message;
            }
        }

        /// <summary>
        /// Geofence application sub class.
        /// </summary>
        private class SelectIDPage : ContentPage
        {
            /// <summary>
            /// Display a list for selecting place or fence.
            /// </summary>
            /// <param name="sender">Specifies the object of selected button in main page</param>
            public SelectIDPage(object sender)
            {
                if (PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/location") != CheckResult.Allow)
                {
                    var button = new Button()
                    {
                        Text = "OK",
                    };
                    button.Clicked += (object s, ClickedEventArgs a) =>
                    {
                        Navigator?.Pop();
                    };
                    DialogPage.ShowAlertDialog("Alert", "NoPermission", button);
                    return;
                }

                // Clear some labels if button4 is selected.
                if (sender != page.StartButton)
                {
                    page.ThirdTextLabel.Text = "";
                    page.FourthTextLabel.Text = "";
                }

                // Set a title of this page
                this.AppBar.Title = ((Button)sender).Text;

                // Create a list for the places or geofences
                List<string> myList = new List<string>();
                if (sender == page.RemovePlaceButton || sender == page.AddFenceButton || sender == page.UpdatePlaceButton)
                {
                    // Get the information for all the places
                    foreach (PlaceData data in perimeter.GetPlaceDataList())
                    {
                        // Add the place id to the list
                        myList.Add(data.PlaceId.ToString());
                    }
                }
                else if (sender == page.RemoveFenceButton || sender == page.StartButton || sender == page.StopButton || sender == page.FenceStatusButton)
                {
                    // Get the information for all the fences
                    foreach (FenceData data in perimeter.GetFenceDataList())
                    {
                        // Add the geofence id to the list
                        myList.Add(data.GeofenceId.ToString());
                    }
                }

                // Create a list view
                var listView = new Menu
                {
                    VerticalOptions = LayoutOptions.Center,
                    Items = myList
                };

                // Add a handler for list view
                listView.ItemSelected += async (o, e) =>
                {
                    if (sender == ButtonList[1])
                    {
                            // Remove a selected place
                            perimeter.RemovePlace(int.Parse(e.SelectedItem.ToString()));
                    }
                    else if (sender == ButtonList[2])
                    {
                            // Display a page for selecting a fence type
                            await Navigation.PushAsync(new SelectFenceTypePage(int.Parse(e.SelectedItem.ToString()), sender));
                    }
                    else if (sender == ButtonList[3])
                    {
                            // Remove a selected fence
                            perimeter.RemoveGeofence(int.Parse(e.SelectedItem.ToString()));
                    }
                    else if (sender == ButtonList[4])
                    {
                            // Start a selected fence
                            geofence.Start(int.Parse(e.SelectedItem.ToString()));
                    }
                    else if (sender == ButtonList[5])
                    {
                            // Stop a selected fence
                            geofence.Stop(int.Parse(e.SelectedItem.ToString()));
                    }
                    else if (sender == ButtonList[6])
                    {
                            // Display a page for inserting information
                            await Navigation.PushAsync(new InsertInfoPage(int.Parse(e.SelectedItem.ToString()), sender));
                    }
                    else if (sender == ButtonList[7])
                    {
                            // Create a fence status for sthe elected fence
                            FenceStatus status = new FenceStatus(int.Parse(e.SelectedItem.ToString()));

                            // Set the value to label about the fence status
                            InfoLabelList[2].Text = "Fence ID: " + int.Parse(e.SelectedItem.ToString()) + ", GeofenceState: ";
                        switch (status.State)
                        {
                            case GeofenceState.In:
                                InfoLabelList[2].Text += "In";
                                break;
                            case GeofenceState.Out:
                                InfoLabelList[2].Text += "Out";
                                break;
                            default:
                                InfoLabelList[2].Text += "Uncertain";
                                break;
                        }

                            // Add the duration to the label
                            InfoLabelList[2].Text += ", Duration: " + status.Duration.ToString();
                    }

                    if (sender != ButtonList[2] && sender != ButtonList[6])
                    {
                            // Move to the main page
                            await Navigation.PopToRootAsync();
                    }
                };

                // Create a layout for this page
                Content = new StackLayout
                {
                    Children = { listView },
                    // Set the margin of the layout
                    Margin = 10
                };

                // Check the count of the list is empty
                if (myList.Count < 1)
                {
                    // Displace an alert
                    var button = new Button()
                    {
                        Text = "OK",
                    };
                    button.Clicked += (object s, ClickedEventArgs a) =>
                    {
                        Navigator?.Pop();
                    };
                    DialogPage.ShowAlertDialog("Alert", "Empty", button);
                }
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
