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

namespace Clock.Timer
{
    public class Timer : Counter
    {
        private int hour_;
        private int minute_;
        private int second_;

        public Timer()
        {
        }

        public void SetHour(int hour)
        {
            if (hour > 99)
            {
                hour = 0;
            }
            else if (hour < 0)
            {
                hour = 99;
            }

            hour_ = hour;
        }

        public void SetMinute(int minute)
        {
            if (minute > 59)
            {
                minute = 0;
            }
            else if (minute < 0)
            {
                minute = 59;
            }

            minute_ = minute;
        }

        public void SetSecond(int second)
        {
            if (second > 59)
            {
                second = 0;
            }
            else if (second < 0)
            {
                second = 59;
            }

            second_ = second;
        }

        public void SetTime(int hour, int minute, int second)
        {
            SetHour(hour);
            SetMinute(minute);
            SetSecond(second);
        }

        public string GetTime()
        {
            long remainingTime = GetRemainingTime();
            int h = (int)((remainingTime / 3600) % 100);
            int m = (int)((remainingTime - h * 3600) / 60);
            int s = (int)((remainingTime - h * 3600 - m * 60) % 60);
            var hmstime = string.Format("{0:00}:{1:00}:{2:00}", h, m, s);
            return hmstime;
        }

        public new int GetHour()
        {
            return hour_;
        }

        public new int GetMinute()
        {
            return minute_;
        }

        public new int GetSecond()
        {
            return second_;
        }

        public long GetRemainingTime()
        {
            long time = hour_ * 3600 + minute_ * 60 + second_;
            long elapsed = GetElapsedTime();
            return time - elapsed;
        }
    }
}
