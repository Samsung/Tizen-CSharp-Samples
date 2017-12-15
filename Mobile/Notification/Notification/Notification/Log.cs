/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;

namespace NotificationApp
{
    public static class Log
    {
        private const String LOGTAG = "NotificationTestApp";

        public static void Debug(string message)
        {
            global::Tizen.Log.Debug(LOGTAG, message);
        }

        public static void Error(string message)
        {
            global::Tizen.Log.Error(LOGTAG, message);
        }

        public static void Info(string message)
        {
            global::Tizen.Log.Info(LOGTAG, message);
        }

        public static void Warn(string message)
        {
            global::Tizen.Log.Warn(LOGTAG, message);
        }
    }
}
