/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Clock.Alarm;
using Clock.Interfaces;
using Clock.Tizen.Mobile.Impls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;

[assembly: Xamarin.Forms.Dependency(typeof(AlarmPersistentHandler))]

namespace Clock.Tizen.Mobile.Impls
{
    /// <summary>
    /// The Deserializer class to deserialize an AlarmRecord object
    /// </summary>
    public class AlarmPersistentHandler : IAlarmPersistentHandler
    {
        const string AlarmRecordStoreFile = "AlarmRecordFile.xml";

        public AlarmPersistentHandler()
        {
        }

        /// <summary>
        /// Deserializes an AlarmRecord object from XML document
        /// </summary>
        /// <returns> a collection of <string, AlarmRecord></string> </returns>
        public IDictionary<string, AlarmRecord> DeserializeAlarmRecord()
        {
            Stream alarmFileStream = null;
            var fileOpener = new FileOpener();
            alarmFileStream = fileOpener.OpenFile(AlarmRecordStoreFile, System.IO.FileMode.OpenOrCreate);
            if (alarmFileStream.Length == 0)
            {
                return null;
            }

            using (XmlDictionaryReader alarmReader = XmlDictionaryReader.CreateBinaryReader(alarmFileStream, XmlDictionaryReaderQuotas.Max))
            {
                var dataContractSerializer = new DataContractSerializer(typeof(Dictionary<string, AlarmRecord>));
                Object ret1 = dataContractSerializer.ReadObject(alarmReader);
                alarmFileStream = null;
                return (IDictionary<string, AlarmRecord>)ret1;
            }
        }

        /// <summary>
        /// Serializes an AlarmRecord object to XML document
        /// </summary>
        /// <param name="properties">AlarmRecordDictionary object to serialize</param>
        /// <seealso cref="IDictionary<string, AlarmRecord>">
        /// <returns> Task return type </returns>
        public Task SerializeAlarmRecordAsync(IDictionary<string, AlarmRecord> properties)
        {
            //properties = new Dictionary<string, object>(properties);
            // Serialize property dictionary to local storage
            // Make sure to use Internal
            return Task.Run(() =>
            {
                Stream alarmFileStream = null;
                var fileOpener = new FileOpener();
                try
                {
                    alarmFileStream = fileOpener.OpenFile(AlarmRecordStoreFile, System.IO.FileMode.Create);
                    using (XmlDictionaryWriter alarmWriter = XmlDictionaryWriter.CreateBinaryWriter(alarmFileStream))
                    {
                        var dataContractSerializer = new DataContractSerializer(typeof(Dictionary<string, AlarmRecord>));
                        dataContractSerializer.WriteObject(alarmWriter, properties);
                        alarmFileStream = null;
                        alarmWriter.Flush();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("[Alarm serialization error] " + e.Message);
                }
                finally
                {
                    if (alarmFileStream != null)
                    {
                        alarmFileStream.Dispose();
                    }
                }

                return;
            });
        }
    }
}

