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

using Clock.Data;
using Clock.Interfaces;
using System;
using Xamarin.Forms;

namespace Clock.Utils
{
    /// <summary>
    /// Direction to move the current timezone
    /// </summary>
    public enum Direction
    {
        // left direction
        LEFT,
        // right direction
        RIGHT,
    }

    /// <summary>
    /// Utility class for time zone
    /// </summary>
    public class TimezoneUtility
    {
        // timezone gmt offset
        private static int localTimezoneOffset;
        private static int currentTimezoneNo = 0;
        // the current timezone
        private static Timezone currentTimezone;

        // Get GMT offset of the current local timezone
        public static int GetLocalTimeGmtOffset()
        {
            return localTimezoneOffset;
        }

        /// <summary>
        /// Get index of current timezone
        /// </summary>
        /// <returns>int</returns>
        public static int GetCurrentTimezoneNo()
        {
            return currentTimezoneNo;
        }

        /// <summary>
        /// Get the current timezone
        /// </summary>
        /// <returns>Timezone</returns>
        public static Timezone GetCurrentTimezone()
        {
            return timezone_[currentTimezoneNo];
        }

        /// <summary>
        /// Get the time zone with GMT offset information
        /// </summary>
        /// <param name="offset">int</param>
        /// <returns>Timezone</returns>
        static public Timezone GetTimezoneByOffset(int offset)
        {
            foreach (Timezone tz in timezone_)
            {
                if (tz.gmtOffset == offset)
                {
                    return tz;
                }

            }

            return timezone_[0];
        }

        /// <summary>
        /// Set the current timezone
        /// </summary>
        /// <param name="no">int</param>
        public static void SetCurrentTimezone(int no/*Timezone no*/)
        {
            currentTimezoneNo = no;
            currentTimezone = timezone_[no];
        }

        /// <summary>
        /// Set the current timezone
        /// </summary>
        /// <param name="tz">Timezone</param>
        public static void SetCurrentTimezone(Timezone tz)
        {
            currentTimezone = tz;
            for (int i = 0; i < timezone_.Length; i++)
            {
                if (timezone_[i].Equals(tz))
                {
                    currentTimezoneNo = i;
                }
            }
        }

        /// <summary>
        /// Update the local time
        /// </summary>
        public static void UpdateLocalTime()
        {
            bool exist = DependencyService.Get<IPreference>().Exist("DOTNET_WORLDCLOCK_MAP_CURRENT_TIMEZONE");
            int storedTimezoneNo;
            if (exist)
            {
                storedTimezoneNo = DependencyService.Get<IPreference>().GetInt("DOTNET_WORLDCLOCK_MAP_CURRENT_TIMEZONE");
                SetCurrentTimezone(storedTimezoneNo);
            }

            string tz = DependencyService.Get<ISystemSetting>().GetString("LOCALE_TIMEZONE");
            string[] str = tz.Split('/');

            int _offset = 100000;
            foreach (Location l in L)
            {
                if (l.Name == str[1])
                {
                    _offset = l.GmtOffset;
                }
            }

            if (_offset != 100000)
            {
                localTimezoneOffset = _offset;
            }
            else
            {
                TimeZoneInfo localZone = TimeZoneInfo.Local;
                int local_hour = Math.Abs(localZone.BaseUtcOffset.Hours);
                int local_minute = Math.Abs(localZone.BaseUtcOffset.Minutes);

                if (localZone.BaseUtcOffset >= TimeSpan.Zero)
                {
                    localTimezoneOffset = (local_hour + (local_minute / 60)) * 60;
                }
                else
                {
                    localTimezoneOffset = -((local_hour + (local_minute / 60)) * 60);
                }
            }

            if (!exist)
            {
                SetCurrentTimezone(GetTimezoneByOffset(localTimezoneOffset));
            }
        }

        /// <summary>
        /// Move the current timezone depending on the direction
        /// left : move backward, decrease GMT offset
        /// right : move forward, increase GMT offset
        /// </summary>
        /// <param name="direction">Direction</param>
        public static void MoveCurrentTimezone(Direction direction)
        {
            int current = TimezoneUtility.GetCurrentTimezoneNo();
            if (direction == Direction.LEFT)
            {
                if (current == 0)
                {
                    TimezoneUtility.SetCurrentTimezone(TimezoneUtility.timezone_.Length - 1);
                }
                else
                {
                    TimezoneUtility.SetCurrentTimezone(current - 1);
                }
            }
            else
            {
                if (current == TimezoneUtility.timezone_.Length - 1)
                {
                    TimezoneUtility.SetCurrentTimezone(0);
                }
                else
                {
                    TimezoneUtility.SetCurrentTimezone(current + 1);
                }
            }
        }

        public static Location[] L =
            {
            //  city, country,  , offset, x, y
            new Location("Midway Atoll", "USA", -11 * 60, 43, 218),               // GMT -11
            new Location("Pago Pago", "American Samoa", -11 * 60, 54, 230),       // GMT -11

            new Location("Honolulu", "USA", -10 * 60, 57, 223),                   // GMT -10

            new Location("Anchorage", "USA", -9 * 60, 74, 144),                   // GMT -9

            new Location("Los Angeles", "USA", -8 * 60, 131, 196),                // GMT -8
            new Location("San Francisco", "USA", -8 * 60, 126, 191),              // GMT -8
            new Location("Vancouver", "Canada", -8 * 60, 122, 166),               // GMT -8

            new Location("Denver", "USA", -7 * 60, 156, 185),                     // GMT -7

            new Location("Austin", "USA", -6 * 60, 168, 199),                     // GMT -6
            new Location("Chicago", "USA", -6 * 60, 187, 181),                    // GMT -6
            new Location("Guatemala City", "Guatemala", -6 * 60, 181, 233),       // GMT -6
            new Location("Mexico City", "Mexico", -6 * 60, 167, 244),             // GMT -6

            new Location("Bogota", "Colombia", -5 * 60, 212, 253),                // GMT -5
            new Location("Lima", "Peru", -5 * 60, 206, 286),                     // GMT -5
            new Location("Miami", "USA", -5 * 60, 200, 211),                      // GMT -5
            new Location("New York", "USA", -5 * 60, 211, 184),                   // GMT -5
            new Location("Ottawa", "Canada", -5 * 60, 208, 173),                  // GMT -5
            new Location("Washington DC", "USA", -5 * 60, 205, 189),              // GMT -5

            new Location("Santiago", "Chile", -4 * 60, 217, 327),                 // GMT -4
            new Location("Santo Domingo", "Dominican Republic", -4 * 60, 218, 226),   // GMT -4

            new Location("Brasilia", "Brazil", -3 * 60, 257, 290),                    // GMT -3
            new Location("Buenos Aires", "Argentina", -3 * 60, 239, 329),             // GMT -3
            new Location("Nuuk", "Greenland", -3 * 60, 256, 143),                     // GMT -3
            new Location("Sao Paulo", "Brazil", -3 * 60, 260, 308),                   // GMT -3

            new Location("Mid-Atlantic", "", -2 * 60, 296, 266),                       // GMT -2
            new Location("Grytvieken", "South Georgia", -2 * 60, 276, 364),

            new Location("Azores", "Portugal", -1 * 60, 308, 188),                    // GMT -1
            new Location("Ponta Delaga", "Portugal", -1 * 60, 300, 190),

            new Location("Accra", "Ghana", 0, 343, 250),                               // GMT 0
            new Location("Dakar", "Senegal", 0, 314, 234),
            new Location("Lisbon", "Portugal", 0, 327, 187),
            new Location("London", "United Kingdom", 0, 343, 162),
            new Location("Reykjavik", "Iceland", 0, 306, 138),

            new Location("Algiers", "Algeria", 1 * 60, 349, 190),                       // GMT +1
            new Location("Bracelona", "Spain", 1 * 60, 346, 181),
            new Location("Berlin", "Germany", 1 * 60, 367, 162),
            new Location("Luanda", "Angola", 1 * 60, 367, 278),
            new Location("Madrid", "Spain", 1 * 60, 337, 185),
            new Location("Paris", "France", 1 * 60, 349, 168),
            new Location("Rome", "Italy", 1 * 60, 365, 180),
            new Location("Stockholm", "Sweden", 1 * 60, 375, 147),

            new Location("Athens", "Greece", 2 * 60, 385, 188),                       // GMT +2
            new Location("Cairo", "Egypt", 2 * 60, 400, 205),
            new Location("Cape Town", "South Africa", 2 * 60, 376, 326),
            new Location("Harare", "Zimbabwe", 2 * 60, 403, 293),
            new Location("Istanbul", "Turkey", 2 * 60, 395, 183),

            new Location("Doha", "Qatar", 3 * 60, 437, 213),                       // GMT +3
            new Location("Nairobi", "Kenia", 3 * 60, 409, 263),
            new Location("Moscow", "Russia", 3 * 60, 410, 155),
            new Location("Tehran", "Iran", 3 * 60 + 30, 435, 192),


            new Location("Izhevsk", "Russia", 4 * 60, 437, 152),                       // GMT +4
            new Location("Dubai", "United Arab Emirates", 4 * 60, 441, 213),
            new Location("Kabul", "Afghanistan", 4 * 60 + 30, 464, 194),


            new Location("Islamabad", "Pakistan", 5 * 60, 467, 196),                       // GMT +5
            new Location("Tashkent", "Uzbekistan", 5 * 60, 467, 182),
            new Location("Delhi", "India", 5 * 60 + 30, 481, 203),
            new Location("Kathmandu", "Nepal", 5 * 60 + 45, 494, 204),


            new Location("Almaty", "Kazakhstan", 6 * 60, 481, 177),                       // GMT +6
            new Location("Bishkek", "Kyrgyzstan", 6 * 60, 477, 179),
            new Location("Dhaka", "Bangladesh", 6 * 60, 504, 213),
            new Location("Omsk", "Russia", 6 * 60, 474, 156),


            new Location("Bangkok", "Thailand", 7 * 60, 522, 235),                       // GMT +7
            new Location("Hanoi", "Vietnam", 7 * 60, 532, 220),
            new Location("Jakarta", "Indonesia", 7 * 60, 532, 274),


            new Location("Beijing", "China", 8 * 60, 550, 185),                       // GMT +8
            new Location("Kuala Lumpur", "Malaysia", 8 * 60, 523, 255),
            new Location("Manila", "Philipines", 8 * 60, 559, 234),
            new Location("Perth", "Australia", 8 * 60, 550, 322),
            new Location("Singapore", "Republic of Singapore", 8 * 60, 528, 258),
            new Location("Ulan Bator", "Mongolia", 8 * 60, 535, 168),


            new Location("Seoul", "South Korea", 9 * 60, 570, 190),                       // GMT +9
            new Location("Tokyo", "Japan", 9 * 60, 592, 193),

            new Location("Sydney", "Australia", 10 * 60, 613, 326),                       // GMT +10

            new Location("Noumea", "New Caledonia", 11 * 60, 639, 304),                       // GMT +11
            new Location("Khabarowsk", "Russia", 11 * 60, 584, 169),

            new Location("Auckland", "New Zealand", 12 * 60, 654, 328),                       // GMT +12

            new Location("Nuku'alofa", "Tonga", 13 * 60, 668, 304),
            new Location("Caracas", "Venezuela", -4 * 60 - 30, 219, 247),
            new Location("St John's", "Newfoundland", -3 * 60 - 30, 249, 166),
            new Location("Yangon", "Myanmar", 6 * 60 + 30, 516, 232),
            new Location("Adelaide", "Australia", 9 * 60 + 30, 603, 332),
        };

        // total 33 timezones
        internal static Timezone[] timezone_ =
        {
            //x_coord, zone_width(DOT), gmt_offset, places
            new Timezone(44, 24, -11 * 60, new Location[] { L[0], L[1] }),                                         // GMT -11
            new Timezone(44, 24, -10 * 60, new Location[] { L[2] }),                                               // GMT -10
            new Timezone(68, 24, -9 * 60, new Location[] { L[3] }),                                                // GMT -9
            new Timezone(116, 30, -8 * 60, new Location[] { L[4], L[5], L[6] }),                                     // GMT -8
            new Timezone(146, 30, -7 * 60, new Location[] { L[7] }),                                              // GMT -7
            new Timezone(146, 47, -6 * 60, new Location[] { L[8], L[9], L[10], L[11] }),                           // GMT -6
            new Timezone(193, 32, -5 * 60, new Location[] { L[12], L[13], L[14], L[15], L[16], L[17] }),
            new Timezone(193, 61, -270, new Location[] { L[77] }),
            new Timezone(193, 61, -4 * 60, new Location[] { L[18], L[19] }),
            new Timezone(225, 59, -210, new Location[] { L[78] }),
            new Timezone(225, 59, -3 * 60, new Location[] { L[20], L[21], L[22], L[23] }),
            new Timezone(254, 54, -2 * 60, new Location[] { L[24], L[25] }),
            new Timezone(284, 24, -1 * 60, new Location[] { L[26], L[27] }),
            new Timezone(308, 41, 0 * 60, new Location[] { L[28], L[29], L[30], L[31], L[32] }),
            new Timezone(332, 45, 1 * 60, new Location[] { L[33], L[34], L[35], L[36], L[37], L[38], L[39], L[40] }),
            new Timezone(349, 85, 2 * 60, new Location[] { L[41], L[42], L[43], L[44], L[45] }),
            new Timezone(377, 81, 3 * 60, new Location[] { L[46], L[47], L[48] }),
            new Timezone(402, 56, 210 /*(3.5 * 60)*/, new Location[] { L[49] }),
            new Timezone(402, 56, 4 * 60, new Location[] { L[50], L[51] }),
            new Timezone(458, 28, 270 /* (4.5 * 60) */, new Location[] { L[52] }),
            new Timezone(458, 28, 5 * 60, new Location[] { L[53], L[54] }),
            new Timezone(458, 28, 330 /*(5.5 * 60)*/, new Location[] { L[55] }),
            new Timezone(486, 28, 345 /* (5.75 * 60) */, new Location[] { L[56] }),
            new Timezone(458, 56, 6 * 60, new Location[] { L[57], L[58], L[59], L[60] }),
            new Timezone(498, 24, 390, new Location[] { L[79] }),
            new Timezone(514, 24, 7 * 60, new Location[] { L[61], L[62], L[63] }),
            new Timezone(514, 54, 8 * 60, new Location[] { L[64], L[65], L[66], L[67], L[68], L[69] }),
            new Timezone(538, 84, 9 * 60, new Location[] { L[70], L[71] }),
            new Timezone(538, 84, 570, new Location[] { L[80] }),
            new Timezone(595, 27, 10 * 60, new Location[] { L[72] }),
            new Timezone(568, 76, 11 * 60, new Location[] { L[73], L[74] }),
            new Timezone(644, 24, 12 * 60, new Location[] { L[75] }),
            new Timezone(655, 24, 13 * 60, new Location[] { L[76] })
        };
    }
}
