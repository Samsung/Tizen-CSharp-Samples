/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
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

using SampleSync.Models;
using SampleSync.Views;
using SampleSync.Utils;
using System;
using Xamarin.Forms;

namespace SampleSync
{
    public class UpdateSyncDateListener
    {
        public string dateTime;
    }

    public class PrivacyPrivilegeListener
    {
    }

    public partial class App : Application, IPlatformEvent
    {
        private static ISyncAPIs ISA;

        public static event EventHandler<UpdateSyncDateListener> UpdateOnDemandDateListener;
        public static event EventHandler<UpdateSyncDateListener> UpdatePeriodicDateListener;
        public static event EventHandler<UpdateSyncDateListener> UpdateCalendarDateListener;
        public static event EventHandler<UpdateSyncDateListener> UpdateContactDateListener;
        public static event EventHandler<UpdateSyncDateListener> UpdateImageDateListener;
        public static event EventHandler<UpdateSyncDateListener> UpdateMusicDateListener;
        public static event EventHandler<UpdateSyncDateListener> UpdateSoundDateListener;
        public static event EventHandler<UpdateSyncDateListener> UpdateVideoDateListener;
        public static event EventHandler<PrivacyPrivilegeListener> CalendarReadPrivilegeListener;
        public static event EventHandler<PrivacyPrivilegeListener> ContactReadPrivilegeListener;

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new SyncMainPage());
            ISA = DependencyService.Get<ISyncAPIs>();
        }

        ~App()
        {
            ISA.RemoveAll();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            ISA.SetCallbacks();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        /// <summary>
        /// An interface to update the time when On Demand sync operation is finished.
        /// </summary>
        public void UpdateOnDemandDate(string time)
        {
            UpdateOnDemandDateListener?.Invoke(this, new UpdateSyncDateListener
            {
                dateTime = time
            });
        }

        /// <summary>
        /// An interface to update the time when Periodic sync operation is finished.
        /// </summary>
        public void UpdatePeriodicDate(string time)
        {
            UpdatePeriodicDateListener?.Invoke(this, new UpdateSyncDateListener
            {
                dateTime = time
            });
        }

        /// <summary>
        /// An interface to update the time when Calendar data change sync operation is finished.
        /// </summary>
        public void UpdateCalendarDate(string time)
        {
            UpdateCalendarDateListener?.Invoke(this, new UpdateSyncDateListener
            {
                dateTime = time
            });
        }

        /// <summary>
        /// An interface to update the time when Contact data change sync operation is finished.
        /// </summary>
        public void UpdateContactDate(string time)
        {
            UpdateContactDateListener?.Invoke(this, new UpdateSyncDateListener
            {
                dateTime = time
            });
        }

        /// <summary>
        /// An interface to update the time when Image data change sync operation is finished.
        /// </summary>
        public void UpdateImageDate(string time)
        {
            UpdateImageDateListener?.Invoke(this, new UpdateSyncDateListener
            {
                dateTime = time
            });
        }

        /// <summary>
        /// An interface to update the time when Music data change sync operation is finished.
        /// </summary>
        public void UpdateMusicDate(string time)
        {
            UpdateMusicDateListener?.Invoke(this, new UpdateSyncDateListener
            {
                dateTime = time
            });
        }

        /// <summary>
        /// An interface to update the time when Sound data change sync operation is finished.
        /// </summary>
        public void UpdateSoundDate(string time)
        {
            UpdateSoundDateListener?.Invoke(this, new UpdateSyncDateListener
            {
                dateTime = time
            });
        }

        /// <summary>
        /// An interface to update the time when Video data change sync operation is finished.
        /// </summary>
        public void UpdateVideoDate(string time)
        {
            UpdateVideoDateListener?.Invoke(this, new UpdateSyncDateListener
            {
                dateTime = time
            });
        }

        /// <summary>
        /// An interface to notify the privilege when API which is related to calendar.read.
        /// </summary>
        public void AllowCalendarReadPrivilege()
        {
            CalendarReadPrivilegeListener?.Invoke(this, new PrivacyPrivilegeListener{});
        }

        /// <summary>
        /// An interface to notify the privilege when API which is related to contact.read.
        /// </summary>
        public void AllowContactReadPrivilege()
        {
            ContactReadPrivilegeListener?.Invoke(this, new PrivacyPrivilegeListener{});
        }
    }
}
