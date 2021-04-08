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
using Tizen.Content.MediaContent;
using VoiceMemo.Models;
using VoiceMemo.Services;
using VoiceMemo.Tizen.Wearable.Services;
using VoiceMemo.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(MediaContentService))]

namespace VoiceMemo.Tizen.Wearable.Services
{
    class MediaContentService : IMediaContentService
    {
        MediaDatabase mediaDB;
        MediaInfoCommand mediaInfoCmd;

        public MediaContentService()
        {
            try
            {
                mediaDB = new MediaDatabase();
#if media_svc_get_storage_id_failed_return_minus_2
                //mediaDB.Connect();
                //MediaDatabase.FolderUpdated += MediaDatabase_FolderUpdated;
                //MediaDatabase.MediaInfoUpdated += MediaDatabase_MediaInfoUpdated;
                //mediaInfoCmd = new MediaInfoCommand(mediaDB);
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine("    FAILED MediaContentService() : " + e.Message);
                MessagingCenter.Send(this, MessageKeys.ErrorOccur, e);
            }
        }

        /// <summary>
        /// Destroy MediaContentService
        /// </summary>
        public void Destroy()
        {
            if (mediaDB != null)
            {
                Console.WriteLine("MediaContentService.Destroy()   ....");
#if media_svc_get_storage_id_failed_return_minus_2
                mediaDB.Disconnect();
#endif
                mediaDB.Dispose();
            }
        }

        /// <summary>
        /// Get Record from media file
        /// </summary>
        /// <param name="path">recorded audio file</param>
        /// <param name="SttOn">stt on/off mode</param>
        /// <returns>Record</returns>
        public Record GetMediaInfo(string path, bool SttOn)
        {
            Record record = null;
            try
            {
                // TODO : scan 
#if media_svc_get_storage_id_failed_return_minus_2
#else
                mediaDB.Connect();
#endif
                mediaDB.ScanFile(path);
#if media_svc_get_storage_id_failed_return_minus_2
#else
                mediaInfoCmd = new MediaInfoCommand(mediaDB);
#endif
                AudioInfo info = mediaInfoCmd.Add(path) as AudioInfo;
#if media_svc_get_storage_id_failed_return_minus_2
#else
                mediaDB.Disconnect();
#endif

                //int minutes = info.Duration / 60000;
                //int seconds = (info.Duration - minutes * 60000) / 1000;
                //string duration = String.Format("{0:00}:{1:00}", minutes, seconds);
                record = new Record
                {
                    _id = AudioRecordService.numbering,
                    Title = info.Title.Substring(0, info.Title.IndexOf("_T_")),
                    Date = info.DateModified.ToLocalTime().ToString("t"),
                    Duration = info.Duration,
                    Path = path,
                    SttEnabled = SttOn,
                };
                Console.WriteLine(" ** Record :" + record);
            }
            catch (Exception e)
            {
                Console.WriteLine("###########################################");
                Console.WriteLine("    FAILED GetMediaInfo : " + e.Message + ", " + e.Source);
                Console.WriteLine("###########################################");
                MessagingCenter.Send(this, MessageKeys.ErrorOccur, e);
            }

            return record;
        }

        /// <summary>
        /// Delete audio file from internal storage
        /// </summary>
        /// <param name="Path">file path to delete</param>
        /// <returns>true if the file is successfully deleted.</returns>
        public bool RemoveMediaFile(string Path)
        {
            try
            {
                File.Delete(Path);
            }
            catch (Exception e)
            {
                Console.WriteLine("[MediaContentService.RemoveMediaFile] Path(" + Path + ") error :" + e.Message);
                return false;
            }

            return true;
        }
    }
}
