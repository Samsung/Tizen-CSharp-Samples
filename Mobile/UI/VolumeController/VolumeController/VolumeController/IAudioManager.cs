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

namespace VolumeController
{
    public enum AudioVolumeTypeShare
    {
        System = 0,
        Notification = 1,
        Alarm = 2,
        Ringtone = 3,
        Media = 4,
        Voip = 5,
        Voice = 6,
        None = 7
    }



    public interface IAudioManager
    {
        int LevelType(AudioVolumeTypeShare type);
        int MaxLevel(AudioVolumeTypeShare type);
        void ApplyAudioType(AudioVolumeTypeShare type, int value);
       // event EventHandler Changed; TBD
    }
}

