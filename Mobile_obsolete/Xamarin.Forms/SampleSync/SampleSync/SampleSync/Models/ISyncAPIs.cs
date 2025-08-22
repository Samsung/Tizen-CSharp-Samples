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

namespace SampleSync.Models
{
    public interface ISyncAPIs
    {
        /// <summary>
        /// An interface to remove all sync jobs and unset sync callbacks.
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// An interface to set sync callbacks.
        /// </summary>
        void SetCallbacks();

        /// <summary>
        /// An interface to request On Demand sync job.
        /// </summary>
        void OnDemand();

        /// <summary>
        /// An interface to add Periodic sync job.
        /// </summary>
        void AddPeriodic();

        /// <summary>
        /// An interface to remove Periodic sync job.
        /// </summary>
        void RemovePeriodic();

        /// <summary>
        /// An interface to add Calendar data change sync job.
        /// </summary>
        void AddCalendarDataChange();

        /// <summary>
        /// An interface to add Contact data change sync job.
        /// </summary>
        void AddContactDataChange();

        /// <summary>
        /// An interface to add Image data change sync job.
        /// </summary>
        void AddImageDataChange();

        /// <summary>
        /// An interface to add Music data change sync job.
        /// </summary>
        void AddMusicDataChange();

        /// <summary>
        /// An interface to add Sound data change sync job.
        /// </summary>
        void AddSoundDataChange();

        /// <summary>
        /// An interface to add Video data change sync job.
        /// </summary>
        void AddVideoDataChange();

        /// <summary>
        /// An interface to remove Calendar data change sync job.
        /// </summary>
        void RemoveCalendarDataChange();

        /// <summary>
        /// An interface to remove Contact data change sync job.
        /// </summary>
        void RemoveContactDataChange();

        /// <summary>
        /// An interface to remove Image data change sync job.
        /// </summary>
        void RemoveImageDataChange();

        /// <summary>
        /// An interface to remove Music data change sync job.
        /// </summary>
        void RemoveMusicDataChange();

        /// <summary>
        /// An interface to remove Sound data change sync job.
        /// </summary>
        void RemoveSoundDataChange();

        /// <summary>
        /// An interface to remove Video data change sync job.
        /// </summary>
        void RemoveVideoDataChange();

        /// <summary>
        /// An interface to check calendar.read privilege.
        /// </summary>
        void CheckCalendarReadPrivileges();

        /// <summary>
        /// An interface to check contact.read privilege.
        /// </summary>
        void CheckContactReadPrivileges();
    }
}
