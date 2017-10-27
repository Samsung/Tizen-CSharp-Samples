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
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Clock.Utils
{
    /// <summary>
    /// Utility class for data item of ListView in Worldclock Page
    /// </summary>
    public class CityRecordUtility
    {
        /// <summary>
        /// single data instance for presenting details for timezone
        /// </summary>
        public static BaseCityRecord detail = new BaseCityRecord();

        private struct TimezoneInfo
        {
            public DateTime newTime;
            public string relative;
        }
        /// <summary>
        /// Updates information about TimezoneDetails with the given GMT offset
        /// </summary>
        /// <param name="offset">int</param>
        public static void UpdateCityRecord(int offset)
        {
            TimezoneInfo result = GetTimezoneInformation(offset);
            detail.CityTime = GetFormattedTime(result.newTime);
            detail.CityAmPm = result.newTime.ToString("tt", CultureInfo.InvariantCulture);
            detail.RelativeToLocalCountry = result.relative;
            detail.Offset = offset;
        }

        /// <summary>
        /// Generate CityRecord instance related to the given GMT offset
        /// </summary>
        /// <param name="offset">int</param>
        /// <returns>CityRecord</returns>
        public static CityRecord GenerateCityRecord(int offset)
        {
            TimezoneInfo result = GetTimezoneInformation(offset);
            CityRecord record = new CityRecord
            {
                CityTime = GetFormattedTime(result.newTime),
                CityAmPm = result.newTime.ToString("tt", CultureInfo.InvariantCulture),
                CityDate = String.Format("{0:ddd, d MMM}", result.newTime),
                RelativeToLocalCountry = result.relative,
                Offset = offset,
                Delete = false,
            };
            return record;
        }

        /// <summary>
        /// Get formatted time text
        /// </summary>
        /// <param name="newTime">DateTime</param>
        /// <returns>string</returns>
        private static string GetFormattedTime(DateTime newTime)
        {
            string result;
            string h, m;

            if (DependencyService.Get<ISystemSetting>().Is24HourTimeFormatted())
            {
                h = String.Format("{0:HH}", newTime);
                m = String.Format("{0:MM}", newTime);
            }
            else
            {
                h = String.Format("{0:hh}", newTime);
                m = String.Format("{0:mm}", newTime);
            }

            if (h.IndexOf("0") == 0)
            {
                h = h.Substring(1);
            }

            result = String.Format("{0}:{1}", h, m);
            return result;
        }

        /// <summary>
        /// Get Timezone information related to the given GMT offset
        /// </summary>
        /// <param name="offset">int</param>
        /// <returns>TimezoneInfo</returns>
        private static TimezoneInfo GetTimezoneInformation(int offset)
        {
            TimezoneInfo info;

            int localTimezoneOffset = TimezoneUtility.GetLocalTimeGmtOffset();
            int offset_integer = (Math.Abs(offset - localTimezoneOffset)) / 60;
            int offset_remainder = (Math.Abs(offset - localTimezoneOffset)) % 60;

            DateTime dateNow = DateTime.Now;

            if (offset < localTimezoneOffset)
            {
                if (offset_remainder > 0)
                {
                    info.relative = String.Format("{0}h {1}m behind", offset_integer, offset_remainder);
                    info.newTime = dateNow.Add(new TimeSpan(-(offset_integer), -(offset_remainder), 0));
                }
                else
                {
                    info.relative = String.Format("{0}h behind", offset_integer);
                    info.newTime = dateNow.Add(new TimeSpan(-(offset_integer), 0, 0));
                }
            }
            else if (offset > localTimezoneOffset)
            {
                if (offset_remainder > 0)
                {
                    info.relative = String.Format("{0}h {1}m ahead", offset_integer, offset_remainder);

                    info.newTime = dateNow.Add(new TimeSpan(offset_integer, offset_remainder, 0));
                }
                else
                {
                    info.relative = String.Format("{0}h ahead", offset_integer);

                    info.newTime = dateNow.Add(new TimeSpan(offset_integer, 0, 0));
                }
            }
            else
            {
                info.relative = "Same as local time";
                info.newTime = dateNow;
            }

            return info;
        }
    }
}