/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
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

using System;
using System.IO;
using System.Linq;
using Tizen.Applications;
using Tizen.System;
using VoiceMemo.Tizen.Wearable.Services;
using SQLitePCL;
using VoiceMemo.Services;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceInformation))]

namespace VoiceMemo.Tizen.Wearable.Services
{
    public static class Features
    {
        // Required features
        // check microphone feature to use a mic for voice recording
        public const string Microphone = "http://tizen.org/feature/microphone";
        // check speech.recognition feature to use stt service
        public const string STT = "http://tizen.org/feature/speech.recognition";
    }
    /// <summary>
    /// Class DeviceInformation
    /// It provides following functions
    /// - device capabilities
    /// - file path in internal storage
    /// - application's state(e.g. Foreground, Background)
    /// </summary>
    public class DeviceInformation : IDeviceInformation
    {
        const int OneKiloBytes = 1024;
        // indicate that speech-to-text feature is supported or not
        internal static bool isSpeechRecognitionSupported = false;
        // indicate that mic is supported or not
        internal static bool isMicrophoneSupported = false;
        internal static bool isFeatureSupported = false;

        public const string LastUsedID = "LAST_SAVED_ID_NUMBER";
        int _numbering;

        public int LastStoredFileIndex
        {
            get
            {
                return _numbering;
            }
        }

        public DeviceInformation()
        {
            // Check whether STT feature is supported or not
            Information.TryGetValue(Features.STT, out isSpeechRecognitionSupported);
            // Check whether mic is available or not
            Information.TryGetValue(Features.Microphone, out isMicrophoneSupported);
            isFeatureSupported = isSpeechRecognitionSupported && isMicrophoneSupported;

            bool existing = Preference.Contains(LastUsedID);
            if (existing)
            {
                _numbering = Preference.Get<int>(LastUsedID);
            }
            else
            {
                _numbering = 0;
            }
        }

        /// <summary>
        /// Indicate speech recognition feature is supported or not
        /// </summary>
        public bool SpeechRecognitionAvailable
        {
            get
            {
                return isFeatureSupported;
            }
        }

        ApplicationRunningContext _context;
        /// <summary>
        /// Application's execution state
        /// </summary>
        public AppState AppState
        {
            get
            {
                if (_context == null)
                {
                    var appInfo = Application.Current.ApplicationInfo;
                    _context = new ApplicationRunningContext(appInfo.ApplicationId);
                }

                if (_context.State == ApplicationRunningContext.AppState.Background)
                {
                    return AppState.Background;
                }
                else if (_context.State == ApplicationRunningContext.AppState.Foreground)
                {
                    return AppState.Foreground;
                }
                else if (_context.State == ApplicationRunningContext.AppState.Terminated)
                {
                    return AppState.Terminated;
                }
                else
                {
                    Console.WriteLine("App State : " + _context.State);
                    return AppState.Undefined;
                }
            }
        }

        /// <summary>
        /// Indicate that there is enough storage available to save audio file
        /// </summary>
        public bool StorageAvailable
        {
            get
            {
                // Check if internal storage has sufficient storage available or not
                Storage internalStorage = StorageManager.Storages.Where(s => s.StorageType == StorageArea.Internal).FirstOrDefault();
                // 'AvaliableSpace' is deprecated in Tizen 5.0
                // In Tizen 5.0, 'AvaliableSpace' is recommended instead of 'AvaliableSpace'.
                return ((internalStorage.AvaliableSpace / OneKiloBytes) > 105) ? true : false;
            }
        }

        /// <summary>
        /// Get the path of database file for this app
        /// </summary>
        /// <param name="filename">Database file name</param>
        /// <returns>The full path  of Database file</returns>
        public string GetLocalDBFilePath(string filename)
        {
            raw.SetProvider(new SQLite3Provider_sqlite3());
            raw.FreezeProvider(true);
            string path = Application.Current.DirectoryInfo.Data;
            return Path.Combine(path, filename);
        }
    }
}
