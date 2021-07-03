using System.Linq;
/*
* Copyright (c) 2021 Samsung Electronics Co., Ltd All Rights Reserved
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

using System;

using Tizen;
using Tizen.Multimedia;

namespace WebRTCOfferClient
{
    internal class CameraController : IDisposable
    {
        private ConnectionManager _connectionManager;
        private Camera _camera;

        internal CameraController(ConnectionManager connectionManager)
        {
            if (connectionManager == null)
            {
                throw new ArgumentNullException(nameof(connectionManager));
            }
            _connectionManager = connectionManager;

            Initialize();
        }

        private void Initialize()
        {
            _camera = new Camera(CameraDevice.Rear);
            _camera.Settings.PreviewFps = _camera.Capabilities.SupportedPreviewFps.FirstOrDefault();
            _camera.StateChanged += (s, e) => Log.Info(WebRTCLog.Tag, $"{e.Current.ToString()}");

            // Now, we just use preview format in ini or native default.
            // But, if you want to set preview format in app, you can do it too.
            //
            // e.g.)
            // var previewFormat = CameraPixelFormat.H264;
            // if (_camera.Capabilities.SupportedPreviewPixelFormats.Contains(previewFormat))
            // {
            //     _camera.Settings.PreviewPixelFormat = previewFormat;
            // }
        }

        internal Size PreviewResolution
        {
            get => _camera.Settings.PreviewResolution;
            set => _camera.Settings.PreviewResolution = value;
        }

        internal void EnableMediaPacket()
        {
            //uint count = 0;

            _camera.MediaPacketPreview += (s, e) =>
            {
                // if (count++ % 15 == 0)
                // {
                //     if (e.Packet.Format.Type == MediaFormatType.Video)
                //     {
                //         Log.Info(WebRTCLog.Tag, $"format:{(e.Packet.Format as VideoMediaFormat).MimeType.ToString()}");
                //     }

                //     Log.Info(WebRTCLog.Tag, $"pts:{e.Packet.Pts},duration:{e.Packet.Duration}");
                // }
                //Log.Info(WebRTCLog.Tag, $"pts:{e.Packet.Pts},duration:{e.Packet.Duration}");
                _connectionManager.PushMediaPacket(e.Packet);
            };
        }

        internal void StartPreview() => _camera.StartPreview();

        internal void StopPreview() => _camera.StopPreview();

        internal MediaFormatVideoMimeType GetVideoMimeType()
        {
            var previewFormat = MediaFormatVideoMimeType.I420;

            switch (_camera.Settings.PreviewPixelFormat)
            {
                case CameraPixelFormat.Nv12:
                    Log.Info(WebRTCLog.Tag, "NV12");
                    previewFormat = MediaFormatVideoMimeType.NV12;
                    break;
                case CameraPixelFormat.I420:
                Log.Info(WebRTCLog.Tag, "I420");
                    previewFormat = MediaFormatVideoMimeType.I420;
                    break;
                default:
                    break;
            }

            return previewFormat;
        }

        internal IntPtr GetCameraHandle() =>
            _camera != null ? _camera.Handle : IntPtr.Zero;

        #region Dispose pattern support
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // to be used if there are any other disposable objects
                    if (_camera.State == CameraState.Preview)
                    {
                        _camera.StopPreview();
                    }
                    _camera?.Dispose();
                }

                _disposed = true;
            }
        }
        #endregion Dispose pattern support
    }
}