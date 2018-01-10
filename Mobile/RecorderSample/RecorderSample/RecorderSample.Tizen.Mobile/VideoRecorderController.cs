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

using RecorderSample.Tizen.Mobile;
using System.Linq;
using Tizen.Multimedia;
using Xamarin.Forms;

[assembly: Dependency(typeof(VideoRecorderController))]
namespace RecorderSample.Tizen.Mobile
{
    class VideoRecorderController : RecorderController, IVideoRecorderController
    {
        private readonly Camera _camera;

        public VideoRecorderController()
        {
            _camera = new Camera(CameraDevice.Rear);

            // Here, we avoid using H263 because there is a restriction on resolution.
            var videoCodec = VideoRecorder.GetSupportedVideoCodecs().First(
                codec => codec != RecorderVideoCodec.H263);

            Recorder = new VideoRecorder(_camera, videoCodec,
                Recorder.GetSupportedAudioCodecs().First(),
                videoCodec.GetSupportedFileFormats().First());
        }

        public override Recorder Recorder { get; }

        public void SetDisplay(object nativeView)
        {
            _camera.Display = new Display(nativeView as MediaView);
        }
    }
}
