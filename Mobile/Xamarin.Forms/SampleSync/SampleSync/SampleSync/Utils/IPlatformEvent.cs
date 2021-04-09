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

namespace SampleSync.Utils
{
    public interface IPlatformEvent
    {
        /// <summary>
        /// A method will be called when On Demand sync operation is finished.
        /// </summary>
        void UpdateOnDemandDate(string time);

        /// <summary>
        /// A method will be called when Periodic sync operation is finished.
        /// </summary>
        void UpdatePeriodicDate(string time);

        /// <summary>
        /// A method will be called when Calendar data change sync operation is finished.
        /// </summary>
        void UpdateCalendarDate(string time);

        /// <summary>
        /// A method will be called when Contact data change sync operation is finished.
        /// </summary>
        void UpdateContactDate(string time);

        /// <summary>
        /// A method will be called when Image data change sync operation is finished.
        /// </summary>
        void UpdateImageDate(string time);

        /// <summary>
        /// A method will be called when Music data change sync operation is finished.
        /// </summary>
        void UpdateMusicDate(string time);

        /// <summary>
        /// A method will be called when Sound data change sync operation is finished.
        /// </summary>
        void UpdateSoundDate(string time);

        /// <summary>
        /// A method will be called when Video data change sync operation is finished.
        /// </summary>
        void UpdateVideoDate(string time);

        /// <summary>
        /// A method will be called after allowing the calendar.read privilege.
        /// </summary>
        void AllowCalendarReadPrivilege();

        /// <summary>
        /// A method will be called after allowing the contact.read privilege.
        /// </summary>
        void AllowContactReadPrivilege();
    }
}
