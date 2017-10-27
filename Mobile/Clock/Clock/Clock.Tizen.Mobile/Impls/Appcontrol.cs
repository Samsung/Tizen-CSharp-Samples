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

using Clock.Data;
using Clock.Interfaces;
using Clock.Tizen.Mobile.Impls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Native = Tizen.Applications;

[assembly: Xamarin.Forms.Dependency(typeof(Appcontrol))]

namespace Clock.Tizen.Mobile.Impls
{
    /// <summary>
    /// The AppControl class for launching an application
    /// </summary>
    class Appcontrol : IAppControl
    {
        public Appcontrol()
        {

        }

        /// <summary>
        /// Request to launch an application
        /// </summary>
        /// <param name="appId">application's ID to launch</param>
        /// <seealso cref="string">
        /// <param name="op">AppControl operation type</param>
        /// <seealso cref="AppControlOperation">
        /// <param name="type">The launching mode to launch an application</param>
        /// <seealso cref="AppControlLaunchType">
        public void ApplicationLaunchRequest(string appId, AppControlOperation op, AppControlLaunchType type)
        {
            Native.AppControl appControl = new Native.AppControl()
            {
                ApplicationId = appId,
                Operation = Native.AppControlOperations.Default,
            };

            if (op == AppControlOperation.PICK)
            {
                appControl.Operation = Native.AppControlOperations.Pick;
            }

            if (type == AppControlLaunchType.SINGLE)
            {
                appControl.LaunchMode = Native.AppControlLaunchMode.Single;
            }
            else
            {
                appControl.LaunchMode = Native.AppControlLaunchMode.Group;
            }

            Native.AppControl.SendLaunchRequest(appControl, ReplyAfterLaunching);
        }

        /// <summary>
        /// Called when the replay is delivered
        /// </summary>
        /// <param name="launchRequest">
        /// AppControl for launch
        /// </param>
        /// <param name="replyRequest">
        /// AppControl for replay
        /// </param>
        /// <param name="result">
        /// The result of requesting an app launch
        /// </param>
        void ReplyAfterLaunching(Native.AppControl launchRequest, Native.AppControl replyRequest, Native.AppControlReplyResult result)
        {
            if (result != Native.AppControlReplyResult.Succeeded)
            {
                return;
            }

            try
            {
                if (replyRequest.ExtraData.Count() != 0)
                {
                    bool normal = false;

                    IEnumerable<string> stack1 = replyRequest.ExtraData.GetKeys();
                    foreach (var item in stack1)
                    {
                        if (item == "city_name")
                        {
                            normal = true;
                        }
                    }

                    if (!normal)
                    {
                        return;
                    }

                    string city = replyRequest.ExtraData.Get<string>("city_name");
                    string country_name = replyRequest.ExtraData.Get<string>("country_name");
                    string timezone = replyRequest.ExtraData.Get<string>("timezone");
                    string tzpath = replyRequest.ExtraData.Get<string>("tzpath");

                    string[] times = timezone.Split(':');
                    bool positive = true;

                    for (int i = 0; i < times.Length; i++)
                    {
                        if (i == 0 && times[i][0] == '-')
                        {
                            positive = false;
                        }
                    }

                    int h = 0, m = 0;
                    h = Int32.Parse(times[0]);
                    if (times.Length > 1)
                    {
                        m = Int32.Parse(times[1]);
                    }

                    int gmt_offset = 0;
                    if (positive)
                    {
                        gmt_offset = h * 60 + m;
                    }
                    else
                    {
                        gmt_offset = h * 60 - m;
                    }

                    Location l = new Location(city, country_name, gmt_offset, 0, 0);
                    App.ClockInfo.OnItemAdded(l);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(" [ReplyAfterLaunching] Exception - " + ex.Message);
            }
            
        }
    }
}