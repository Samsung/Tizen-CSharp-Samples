/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using Clock.Alarm;

namespace Clock.Interfaces
{
    /// <summary>
    /// The Deserializer Interface
    /// </summary>
    public interface IAlarmPersistentHandler
    {
        /// <summary>
        /// Deserializes an AlarmRecord object from XML document
        /// </summary>
        /// <returns> a collection of <string, AlarmRecord></string> </returns>
        IDictionary<string, AlarmRecord> DeserializeAlarmRecord();
        /// <summary>
        /// Serializes an AlarmRecord object to XML document
        /// </summary>
        /// <param name="properties">AlarmRecordDictionary object to serialize</param>
        /// <seealso cref="IDictionary<string, AlarmRecord>">
        /// <returns> Task return type </returns>
        Task SerializeAlarmRecordAsync(IDictionary<string, AlarmRecord> properties);
    }
}
