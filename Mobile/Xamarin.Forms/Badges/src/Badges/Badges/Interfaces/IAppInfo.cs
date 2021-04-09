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

namespace Badges.Interfaces
{
    public struct AppInfoStruct
    {
        public string IconPath;
        public string AppName;
        public bool IsAvailable;
        public bool BadgeEnabled;
        public double BadgeCounter;

        public AppInfoStruct(string iconPath, string appName,
            bool isAvailable, bool badgeEnabled, double badgeCounter)
        {
            IconPath = iconPath;
            AppName = appName;
            IsAvailable = isAvailable;
            BadgeEnabled = badgeEnabled;
            BadgeCounter = badgeCounter;
        }
    }
}
