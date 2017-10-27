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

using System;

namespace Clock.Timer
{
    public class Counter
    {
        private long start_time_;
        private long break_time_;

        private long elapsed_time_;
        private long stopped_time_;

        //private DateTime startingTime;
        public Counter()
        {
            start_time_ = 0;
            break_time_ = 0;
            elapsed_time_ = 0;
            stopped_time_ = 0;
        }
        // called when startButton is clicked
        public void Run()
        {
            start_time_ = GetTimeSinceEpoch();
        }

        // called when resetButton is clicked
        public void Reset()
        {
            start_time_ = 0;
            break_time_ = 0;
            elapsed_time_ = 0;
            stopped_time_ = 0;
        }

        // called when resumButton is clicked
        public void Resume()
        {
            long now = GetTimeSinceEpoch();
            break_time_ += now - stopped_time_;
            stopped_time_ = 0;
        }

        // called when pauseButton is clicked
        public void Stop()
        {
            long now = GetTimeSinceEpoch();
            elapsed_time_ = now - start_time_ - break_time_;
            stopped_time_ = now;
        }

        // return elapsed time since timer started.
        public long GetElapsedTime()
        {
            long now = GetTimeSinceEpoch();
            return now - start_time_ - break_time_;
        }

        public long GetStartTime()
        {
            return start_time_;
        }

        public long GetTimeSinceEpoch()
        {
            long time = 24 * 60 * 60 * DateTime.Now.Day + 60 * 60 * DateTime.Now.Hour + 60 * DateTime.Now.Minute + DateTime.Now.Second;
            return time;
        }

        public int GetHour()
        {
            long time = GetElapsedTime();
            return (int)((time / 3600) /*% 99*/);
        }

        public int GetMinute()
        {
            long time = GetElapsedTime();
            return (int)((time % 3600) / 60 /*% 99*/);
        }

        public int GetSecond()
        {
            long time = GetElapsedTime();
            return (int)((time % 3600) % 60);
        }
    }
}
