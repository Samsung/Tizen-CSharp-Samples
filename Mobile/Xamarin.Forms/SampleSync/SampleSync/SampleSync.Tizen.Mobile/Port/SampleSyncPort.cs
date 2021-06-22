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
using SampleSync.Utils;
using System;
using System.Collections.Generic;
using TAS = Tizen.Account.SyncManager;
using Tizen.Security;

namespace SampleSync.Tizen.Port
{
    public class SampleSyncPort : ISyncAPIs
    {
        private TAS.SyncAdapter adapter;

        static IPlatformEvent pEvent;

        static PrivacyPrivilegeManager.ResponseContext context = null;

        // int variables to store each sync job ides
        private static int Periodic { get; set; }
        private static int Calendar { get; set; }
        private static int Contact { get; set; }
        private static int Image { get; set; }
        private static int Music { get; set; }
        private static int Sound { get; set; }
        private static int Video { get; set; }

        public SampleSyncPort()
        {
        }

        /// <summary>
        /// Used to be invoked by Sync Service.
        /// </summary>
        /// <returns>true for success, other values for failure</returns>
        static bool StartSyncCallback(TAS.SyncJobData request)
        {
            if (request.SyncJobName == "RequestOnDemand")
            {
                // Synchronize data with a server here
                // Update when the OnDemand sync job is completed
                pEvent.UpdateOnDemandDate(DateTime.Now.ToString());
            }
            else if (request.SyncJobName == "AddPeriodic")
            {
                // Synchronize data with a server here
                // Update when the Periodic sync job is completed
                pEvent.UpdatePeriodicDate(DateTime.Now.ToString());
            }
            else if (request.SyncJobName == TAS.SyncJobData.CalendarCapability)
            {
                // Synchronize data with a server here
                // Update when the Calendar data change sync job is completed
                pEvent.UpdateCalendarDate(DateTime.Now.ToString());
            }
            else if (request.SyncJobName == TAS.SyncJobData.ContactCapability)
            {
                // Synchronize data with a server here
                // Update when the Contact data change sync job is completed
                pEvent.UpdateContactDate(DateTime.Now.ToString());
            }
            else if (request.SyncJobName == TAS.SyncJobData.ImageCapability)
            {
                // Synchronize data with a server here
                // Update when the Image data change sync job is completed
                pEvent.UpdateImageDate(DateTime.Now.ToString());
            }
            else if (request.SyncJobName == TAS.SyncJobData.MusicCapability)
            {
                // Synchronize data with a server here
                // Update when the Music data change sync job is completed
                pEvent.UpdateMusicDate(DateTime.Now.ToString());
            }
            else if (request.SyncJobName == TAS.SyncJobData.SoundCapability)
            {
                // Synchronize data with a server here
                // Update when the Sound data change sync job is completed
                pEvent.UpdateSoundDate(DateTime.Now.ToString());
            }
            else if (request.SyncJobName == TAS.SyncJobData.VideoCapability)
            {
                // Synchronize data with a server here
                // Update when the Video data change sync job is completed
                pEvent.UpdateVideoDate(DateTime.Now.ToString());
            }

            return true;
        }

        public void CancelSyncCallback(TAS.SyncJobData data)
        {
        }

        /// <summary>
        /// Used to gain sync job ides for registered sync jobs.
        /// </summary>
        /// <returns>a number greater than 0 for success, 0 for failure</returns>
        static int GetSyncJobId(string syncJobName)
        {
            IEnumerable<KeyValuePair<int, TAS.SyncJobData>> syncJobs = TAS.SyncClient.GetAllSyncJobs();
            foreach (KeyValuePair<int, TAS.SyncJobData> item in syncJobs)
            {
                if (item.Value.SyncJobName == syncJobName)
                {
                    return item.Key;
                }
            }

            return 0;
        }

        /// <summary>
        /// Used to remove all the sync jobs and unset sync callbacks.
        /// </summary>
        public void RemoveAll()
        {
            IEnumerable<KeyValuePair<int, TAS.SyncJobData>> syncJobs = TAS.SyncClient.GetAllSyncJobs();
            foreach (KeyValuePair<int, TAS.SyncJobData> item in syncJobs)
            {
                TAS.SyncClient.RemoveSyncJob(item.Key);
            }

            adapter = new TAS.SyncAdapter();
            adapter.UnsetSyncEventCallbacks();
        }

        /// <summary>
        /// Used to set sync callbacks.
        /// </summary>
        public void SetCallbacks()
        {
            Periodic = Calendar = Contact = Image = Music = Sound = Video = 0;

            try
            {
                adapter = new TAS.SyncAdapter();
                adapter.SetSyncEventCallbacks(StartSyncCallback, CancelSyncCallback);
            }
            catch (Exception e)
            {
                Console.WriteLine("[SampleSync] Err message : " + e.ToString());
            }
        }

        /// <summary>
        /// Used to request On Demand sync job.
        /// </summary>
        public void OnDemand()
        {
            TAS.SyncJobData request = new TAS.SyncJobData();
            request.SyncJobName = "RequestOnDemand";
            // This id is used to remove pended On Demand sync job
			// Generally, it is processed immediately if the device has network connection
            int id = TAS.SyncClient.RequestOnDemandSyncJob(request, TAS.SyncOption.None);
        }

        /// <summary>
        /// Used to add Periodic sync job.
        /// </summary>
        public void AddPeriodic()
        {
            TAS.SyncJobData request = new TAS.SyncJobData();
            request.SyncJobName = "AddPeriodic";
            // This sync job has an interval as 30 minutes
            Periodic = TAS.SyncClient.AddPeriodicSyncJob(request, TAS.SyncPeriod.ThirtyMin, TAS.SyncOption.Expedited);
        }

        /// <summary>
        /// Used to remove Periodic sync job.
        /// </summary>
        public void RemovePeriodic()
        {
            if (Periodic > 0)
            {
                TAS.SyncClient.RemoveSyncJob(Periodic);
            }
            else
            {
                TAS.SyncClient.RemoveSyncJob(GetSyncJobId("AddPeriodic"));
            }
            Periodic = 0;
        }

        /// <summary>
        /// Used to add Calendar data change sync job.
        /// </summary>
        public void AddCalendarDataChange()
        {
            TAS.SyncJobData request = new TAS.SyncJobData();
            request.SyncJobName = TAS.SyncJobData.CalendarCapability;
            // If some data related with calendar is changed, you can know the time by using below API
            Calendar = TAS.SyncClient.AddDataChangeSyncJob(request, TAS.SyncOption.Expedited);
        }

        /// <summary>
        /// Used to add Contact data change sync job.
        /// </summary>
        public void AddContactDataChange()
        {
            TAS.SyncJobData request = new TAS.SyncJobData();
            request.SyncJobName = TAS.SyncJobData.ContactCapability;
            // If some data related with contact is changed, you can know the time by using below API
            Contact = TAS.SyncClient.AddDataChangeSyncJob(request, TAS.SyncOption.Expedited);
        }

        /// <summary>
        /// Used to add Image data change sync job.
        /// </summary>
        public void AddImageDataChange()
        {
            TAS.SyncJobData request = new TAS.SyncJobData();
            request.SyncJobName = TAS.SyncJobData.ImageCapability;
            // If some data related with image is changed, you can know the time by using below API
            Image = TAS.SyncClient.AddDataChangeSyncJob(request, TAS.SyncOption.Expedited);
        }

        /// <summary>
        /// Used to add Music data change sync job.
        /// </summary>
        public void AddMusicDataChange()
        {
            TAS.SyncJobData request = new TAS.SyncJobData();
            request.SyncJobName = TAS.SyncJobData.MusicCapability;
            // If some data related with music is changed, you can know the time by using below API
            Music = TAS.SyncClient.AddDataChangeSyncJob(request, TAS.SyncOption.Expedited);
        }

        /// <summary>
        /// Used to add Sound data change sync job.
        /// </summary>
        public void AddSoundDataChange()
        {
            TAS.SyncJobData request = new TAS.SyncJobData();
            request.SyncJobName = TAS.SyncJobData.SoundCapability;
            // If some data related with sound is changed, you can know the time by using below API
            Sound = TAS.SyncClient.AddDataChangeSyncJob(request, TAS.SyncOption.Expedited);
        }

        /// <summary>
        /// Used to add Video data change sync job.
        /// </summary>
        public void AddVideoDataChange()
        {
            TAS.SyncJobData request = new TAS.SyncJobData();
            request.SyncJobName = TAS.SyncJobData.VideoCapability;
            // If some data related with video is changed, you can know the time by using below API
            Video = TAS.SyncClient.AddDataChangeSyncJob(request, TAS.SyncOption.Expedited);
        }

        /// <summary>
        /// Used to remove Calendar data change sync job.
        /// </summary>
        public void RemoveCalendarDataChange()
        {
            if (Calendar > 0)
            {
                TAS.SyncClient.RemoveSyncJob(Calendar);
            }
            else
            {
                TAS.SyncClient.RemoveSyncJob(GetSyncJobId(TAS.SyncJobData.CalendarCapability));
            }
            Calendar = 0;
        }

        /// <summary>
        /// Used to remove Contact data change sync job.
        /// </summary>
        public void RemoveContactDataChange()
        {
            if (Contact > 0)
            {
                TAS.SyncClient.RemoveSyncJob(Contact);
            }
            else
            {
                TAS.SyncClient.RemoveSyncJob(GetSyncJobId(TAS.SyncJobData.ContactCapability));
            }
            Contact = 0;
        }

        /// <summary>
        /// Used to remove Image data change sync job.
        /// </summary>
        public void RemoveImageDataChange()
        {
            if (Image > 0)
            {
                TAS.SyncClient.RemoveSyncJob(Image);
            }
            else
            {
                TAS.SyncClient.RemoveSyncJob(GetSyncJobId(TAS.SyncJobData.ImageCapability));
            }
            Image = 0;
        }

        /// <summary>
        /// Used to remove Music data change sync job.
        /// </summary>
        public void RemoveMusicDataChange()
        {
            if (Music > 0)
            {
                TAS.SyncClient.RemoveSyncJob(Music);
            }
            else
            {
                TAS.SyncClient.RemoveSyncJob(GetSyncJobId(TAS.SyncJobData.MusicCapability));
            }
            Music = 0;
        }

        /// <summary>
        /// Used to remove Sound data change sync job.
        /// </summary>
        public void RemoveSoundDataChange()
        {
            if (Sound > 0)
            {
                TAS.SyncClient.RemoveSyncJob(Sound);
            }
            else
            {
                TAS.SyncClient.RemoveSyncJob(GetSyncJobId(TAS.SyncJobData.SoundCapability));
            }
            Sound = 0;
        }

        /// <summary>
        /// Used to remove Video data change sync job.
        /// </summary>
        public void RemoveVideoDataChange()
        {
            if (Video > 0)
            {
                TAS.SyncClient.RemoveSyncJob(Video);
            }
            else
            {
                TAS.SyncClient.RemoveSyncJob(GetSyncJobId(TAS.SyncJobData.VideoCapability));
            }
            Video = 0;
        }

        /// <summary>
        /// Used to set privilege event listener and request the privilege.
        /// </summary>
        private static void PPM_RequestResponse(object sender, RequestResponseEventArgs e)
        {
            if (e.cause == CallCause.Answer)
            {
                switch (e.result)
                {
                    // Allow clicked case
                    case RequestResult.AllowForever:
                        if (e.privilege == "http://tizen.org/privilege/calendar.read")
                        {
                            pEvent.AllowCalendarReadPrivilege();
                        }
                        else if (e.privilege == "http://tizen.org/privilege/contact.read")
                        {
                            pEvent.AllowContactReadPrivilege();
                        }
                        break;
                    case RequestResult.DenyForever:
                        break;
                    case RequestResult.DenyOnce:
                        break;
                };

                // Unset event listener for privilege
                PrivacyPrivilegeManager.GetResponseContext(e.privilege).TryGetTarget(out context);
                if (context != null)
                {
                    context.ResponseFetched -= PPM_RequestResponse;
                }
            }
            else
            {
                Console.WriteLine("Error occurs during requesting permission for {0}", e.privilege);
            }
        }

        /// <summary>
        /// Used to set privilege event listener and request the privilege.
        /// </summary>
        void AskPrivacyPrivilege(string privilege)
        {
            // Set event listener for privilege
            PrivacyPrivilegeManager.GetResponseContext(privilege).TryGetTarget(out context);
            if (context != null)
            {
                context.ResponseFetched += PPM_RequestResponse;
            }

            // Request pop-up message for privileges
            PrivacyPrivilegeManager.RequestPermission(privilege);
        }

        /// <summary>
        /// Used to check calendar.read privilege.
        /// </summary>
        public void CheckCalendarReadPrivileges()
        {
            CheckResult result = PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/calendar.read");
            switch (result)
            {
                case CheckResult.Allow:
                    // Privilege can be used
                    pEvent.AllowCalendarReadPrivilege();
                    break;
                case CheckResult.Deny:
                    // Privilege can't be used
                    break;
                case CheckResult.Ask:
                    // User permission request required
                    AskPrivacyPrivilege("http://tizen.org/privilege/calendar.read");
		    PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/contact.read");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Used to check contact.read privilege.
        /// </summary>
        public void CheckContactReadPrivileges()
        {
            CheckResult result = PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/contact.read");
            switch (result)
            {
                case CheckResult.Allow:
                    // Privilege can be used
                    pEvent.AllowContactReadPrivilege();
                    break;
                case CheckResult.Deny:
                    // Privilege can't be used
                    break;
                case CheckResult.Ask:
                    // User permission request required
                    AskPrivacyPrivilege("http://tizen.org/privilege/contact.read");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Used to register platform event.
        /// </summary>
        /// <param name="_pEvent">platform event</param>
        public static void EventRegister(IPlatformEvent _pEvent)
        {
            pEvent = _pEvent;
        }
    }
}
