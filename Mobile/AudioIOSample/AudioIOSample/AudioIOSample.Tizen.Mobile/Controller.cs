/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
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

using System.IO;
using Tizen.Multimedia;
using TizenApps = Tizen.Applications;

namespace AudioIOSample.Tizen.Mobile
{
    abstract class Controller
    {
        protected const int SampleRate = 8000;
        protected const AudioChannel Channel = AudioChannel.Stereo;
        protected const AudioSampleType SampleType = AudioSampleType.S16Le;

        public string SavePath =>
            Path.Combine(TizenApps.Application.Current.DirectoryInfo.Data, "captured.pcm");
    }
}
