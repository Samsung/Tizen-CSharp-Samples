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
using System.Linq;
using System.Text;
using Tizen.Telephony;
using Xamarin.Forms;

namespace SampleTelephony
{

    internal static class Globals
    {
        //It is used for printing the log on shell
        internal static string LogTag = "SampleTelephony";

        // For accessing the Telephony API,
        // should be gotten the Slot Handle corresponding the Modem Interface.
        // Slot Handle will be gotten when Tizen.Telephony.Manager is initialized.
        internal static SlotHandle slotHandle = null;
    }

    /// <summary>
    /// Main page of SampleTelephony application
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// There are 4 functionalities as Call, Modem, Network, SIM respectively
        /// can access each functional page through Menu page.
        /// </summary>
        public App()
        {
            // SampleTelephonyApp menu page
            Page Menu = new MenuPage();

            // The root page of your application
            MainPage = new TabbedPage
            {
                Children =
                {
                    Menu,
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
