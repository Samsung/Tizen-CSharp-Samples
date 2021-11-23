/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
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

using NotificationManager.Models;
using NotificationManager.TizenMobile.Service;
using Tizen.Applications;
using System.Linq;
using System;
using System.Collections.Generic;
using Log = Tizen.Log;

[assembly: Xamarin.Forms.Dependency(typeof(MyFileService))]

namespace NotificationManager.TizenMobile.Service
{
    /// <summary>
    /// MyFileService class.
    /// Provides method responsible for launching MyFile application.
    /// </summary>
    public class MyFileService : IMyFile
    {
        #region fields

        /// <summary>
        /// Sample log tag.
        /// </summary>
        private const string SAMPLE_LOG_TAG =
            "NotificationManagerSample";

        /// <summary>
        /// The message describing the 'Unknown path type' error.
        /// </summary>
        private const string UNKNOWN_PATH_TYPE_MSG =
            "Unknown path category to be updated after file selection.";

        #endregion

        #region properties

        /// <summary>
        /// Event fired when a file is selected.
        /// </summary>
        public event EventHandler<FileSelectedEventArgs> FileSelected;

        #endregion

        #region methods

        /// <summary>
        /// Callback executed after file selection.
        /// </summary>
        /// <param name="launchRequest">App control object used to launch the application.</param>
        /// <param name="replyRequest">App control object which contains the reply of launched application.</param>
        /// <param name="result">Application result.</param>
        private void MyFileSelectedCallback(AppControl launchRequest, AppControl replyRequest,
            AppControlReplyResult result)
        {
            var pathCategory = (PathCategory)Enum.Parse(typeof(PathCategory),
                (string)launchRequest.ExtraData.Get("PathCategory"));

            try
            {
                var path = ((List<string>)replyRequest.ExtraData.Get(AppControlData.Selected)).First();

                FileSelected?.Invoke(this, new FileSelectedEventArgs(path, pathCategory));
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException || e is KeyNotFoundException || e is ArgumentException)
                {
                    Log.Warn(SAMPLE_LOG_TAG, e.Message);
                    return;
                }

                throw;
            }
        }

        /// <summary>
        /// Launches application that allows 'pick' operation.
        /// </summary>
        /// <param name="pathCategory">Path which should be updated after file selection.</param>
        public void Launch(PathCategory pathCategory)
        {
            var pickAppcontrol = new AppControl
            {
                Operation = AppControlOperations.Pick
            };

            try
            {
                pickAppcontrol.ExtraData.Add("PathCategory", pathCategory.ToString());
                switch (pathCategory)
                {
                    case PathCategory.Background:
                    case PathCategory.Icon:
                    case PathCategory.Thumbnail:
                        pickAppcontrol.Mime = "image/*";
                        break;
                    case PathCategory.Sound:
                        pickAppcontrol.Mime = "audio/*";
                        break;
                    default:
                        Log.Warn(SAMPLE_LOG_TAG, UNKNOWN_PATH_TYPE_MSG);
                        return;
                }

                AppControl.SendLaunchRequest(pickAppcontrol, MyFileSelectedCallback);
            }
            catch (Exception e)
            {
                Log.Warn(SAMPLE_LOG_TAG, e.Message);
            }
        }

        #endregion
    }
}