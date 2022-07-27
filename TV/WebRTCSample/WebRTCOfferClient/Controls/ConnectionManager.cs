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
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen;
using Tizen.Multimedia;
using Tizen.Multimedia.Remoting;

namespace WebRTCOfferClient
{
    internal static class WebRTCLog
    {
        internal const string Tag = "WebRTCOfferClient";
    }

    internal enum SourceType
        {
            None,
            AudioTest,
            VideoTest,
            Mic,
            Camera,
            Screen,
            File,
            MediaPacket
        }

    internal class ConnectionManager : IDisposable
    {
        private const string stunServerUrl = "stun://stun.l.google.com:19302";
        private const string externalSignalingServerUrl = "wss://www.testbd.ga:8443";

        private WebSocketClient webSocketClient;

        private TaskCompletionSource<bool> tcsSdpAnswer, tcsStateNegotiation, tcsStatePlaying, tcsDCCreated, tcsQuit, tcsIceGathering;
        private CancellationTokenSource ctsSdpReceiver;
        private List<string> iceCandidatesLocal, iceCandidatesRemote;

        private WebRTC webRtcClient;
        private WebRTCDataChannel webRTCSendDataChannel, webRTCReceiveDataChannel;

        private CameraController cameraController;

        Tizen.NUI.Window remoteView;

        private bool readyToPushMediaPacket;

        internal ConnectionManager()
        {
            Initialize();
        }

        private void Initialize()
        {
            cameraController = new CameraController(this);

            InitializeVariable();
        }

        private void InitializeVariable()
        {
            tcsSdpAnswer = new TaskCompletionSource<bool>();
            tcsStateNegotiation = new TaskCompletionSource<bool>();
            tcsStatePlaying = new TaskCompletionSource<bool>();
            tcsDCCreated = new TaskCompletionSource<bool>();
            tcsQuit = new TaskCompletionSource<bool>();
            tcsIceGathering = new TaskCompletionSource<bool>();

            ctsSdpReceiver = new CancellationTokenSource();

            iceCandidatesLocal = new List<string>();
            iceCandidatesRemote = new List<string>();
        }

        internal WebRTCState State => webRtcClient.State;

        internal SourceType MediaSourceType { get; set; } = SourceType.None;

        internal Size PreviewResolution
        {
            get => cameraController.PreviewResolution;
            set => cameraController.PreviewResolution = value;
        }

        internal TransceiverDirection TransceiverDirection {get; set;} = TransceiverDirection.SendRecv;

        internal IntPtr GetCameraHandle() => cameraController.GetCameraHandle();

        internal void SetMediaSourceType(SourceType sourceType) =>
            MediaSourceType = sourceType;

        internal void SetRemoteView(Tizen.NUI.Window window) =>
            remoteView = window;

        private Tizen.Multimedia.Remoting.MediaSource mediaSource;

        private MediaPacketBufferStatus mediaPacketBufferStatus = MediaPacketBufferStatus.Underrun;

        internal void PushMediaPacket(MediaPacket mediapacket)
        {
            if (readyToPushMediaPacket &&
                mediaPacketBufferStatus != MediaPacketBufferStatus.Overflow &&
                mediaSource is MediaPacketSource source)
            {
                source.Push(mediapacket);
            }
        }

        internal async void Connect(int remotePeerId)
        {
            webSocketClient = new WebSocketClient();

            int localPeerId = remotePeerId + 1;
            Log.Info(WebRTCLog.Tag, $"RID:{remotePeerId}, LID:{localPeerId}");

            // 1. Connect to signaling server
            await webSocketClient.ConnectAsync(externalSignalingServerUrl);

            // 2. send `HELLO` to signaling server
            await webSocketClient.SendAsync($"HELLO {localPeerId}");
            var rcvMessage = await webSocketClient.ReceiveAsync();

            // 3. If client receives 'HELLO' from signaling server,
            if (String.Compare(rcvMessage, "HELLO") == 0)
            {
                Log.Info(WebRTCLog.Tag, "Register done");

                // 4. Create local peer id and send it to signaling server
                await webSocketClient.SendAsync($"SESSION {remotePeerId}");
                rcvMessage = await webSocketClient.ReceiveAsync();

                if (String.Compare(rcvMessage, "SESSION_OK") == 0)
                {
                    Log.Info(WebRTCLog.Tag, "Session establised");
                }
                else
                {
                    throw new InvalidOperationException("Failed to establish session.");
                }
            }
            else
            {
                throw new InvalidOperationException("Connection failed.");
            }

            SDPReceiver(ctsSdpReceiver.Token);

            await StartNegotiation();
        }

        internal async Task StartNegotiation()
        {
            webRtcClient = new WebRTC();

            Log.Info(WebRTCLog.Tag, "1. Set Source");
            switch (MediaSourceType)
            {
                case SourceType.Camera:
                    mediaSource = new MediaCameraSource();
                    break;
                case SourceType.Mic:
                    mediaSource = new MediaMicrophoneSource();
                    break;
                case SourceType.AudioTest:
                    mediaSource = new MediaTestSource(MediaType.Audio);
                    break;
                case SourceType.VideoTest:
                    mediaSource = new MediaTestSource(MediaType.Video);
                    break;
                case SourceType.MediaPacket:
                    mediaSource = new MediaPacketSource(
                        new VideoMediaFormat(cameraController.GetVideoMimeType(), PreviewResolution));

                    cameraController.EnableMediaPacket();
                    cameraController.StartPreview();

                    break;
                case SourceType.Screen:
                default:
                    break;
            }

            if (MediaSourceType != SourceType.None)
            {
                webRtcClient.AddSources(mediaSource);
                mediaSource.TransceiverDirection = TransceiverDirection;
                if (mediaSource is MediaPacketSource source)
                {
                    source.VideoConfiguration.BufferStatusChanged += (s, e) =>
                        mediaPacketBufferStatus = e.Status;
                }
            }

            Log.Info(WebRTCLog.Tag, "2. Create DataChannel");
            webRTCSendDataChannel = new WebRTCDataChannel(webRtcClient, "DataChannelForOfferClient", null);

            Log.Info(WebRTCLog.Tag, "3. Register all events");
            RegisterWebRTCEvents();
            RegisterWebRTCDataChannelEvents();

            Log.Info(WebRTCLog.Tag, "4. Set STUN server");
            if (webRtcClient.StunServer == null)
            {
                webRtcClient.StunServer = stunServerUrl;
            }

            Log.Info(WebRTCLog.Tag, "5. Start WebRTC");
            await webRtcClient.StartAsync();
            readyToPushMediaPacket = true;

            Log.Info(WebRTCLog.Tag, "6. Create and set offer");
            var offer = await webRtcClient.CreateOfferAsync();
            webRtcClient.SetLocalDescription(offer);

            Log.Info(WebRTCLog.Tag, "7. Send offer to remote");
            await webSocketClient.SendAsync(offer);

            Log.Info(WebRTCLog.Tag, "8. Wait StateChanged - Playing");
            await tcsStatePlaying.Task;
            Log.Info(WebRTCLog.Tag, $"Current state : {webRtcClient.State.ToString()}");

            Log.Info(WebRTCLog.Tag, "9. Send Message to DataChannel");
            await tcsDCCreated.Task;

            webRTCSendDataChannel.Send("HELLO_TIZEN");
            await tcsQuit.Task;
        }

        internal void Disconnect()
        {
            // stop pushing media packet
            readyToPushMediaPacket = false;

            cameraController.StopPreview();
            webRtcClient.Stop();

            InitializeVariable();
        }

        private void SDPReceiver(CancellationToken token)
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    Log.Info(WebRTCLog.Tag, "Waiting Web Socket Message");
                    var rcvMessage = await webSocketClient.ReceiveAsync(token);

                    if (rcvMessage.Contains("sdp") && rcvMessage.Contains("answer"))
                    {
                        Log.Info(WebRTCLog.Tag, "[SDP] Answer from remote");
                        webRtcClient.SetRemoteDescription(rcvMessage);
                        tcsSdpAnswer.TrySetResult(true);
                    }
                    else if (rcvMessage.Contains("ice") && rcvMessage.Contains("candidate"))
                    {
                        Log.Info(WebRTCLog.Tag, "[SDP] Ice Candidate from remote");
                        webRtcClient.AddIceCandidate(rcvMessage);
                    }
                }
            }, token,
                TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }

        private void RegisterWebRTCEvents()
        {
            webRtcClient.StateChanged += WebRTCStateChanged;
            webRtcClient.NegotiationNeeded += WebRTCNegotiationNeeded;
            webRtcClient.IceCandidate += WebRTCIceCandidate;
            webRtcClient.TrackAdded += WebRTCTrackAdded;
            webRtcClient.DataChannel += WebRTCDataChannelCreated;
            webRtcClient.IceGatheringStateChanged += WebRTCICEGatheringStateChanged;
            // webRtcClient.VideoFrameEncoded += (s, e) =>
            // {
            //     Log.Info(WebRTCLog.Tag, $"Enter. length[{e.Packet.BufferWrittenLength}], format[{(e.Packet.Format as VideoMediaFormat).MimeType.ToString()}]");
            //     // just for CopyTo test
            //     var buffer = new byte[e.Packet.BufferWrittenLength];
            //     e.Packet.Buffer.CopyTo(buffer, 0, e.Packet.BufferWrittenLength);
            // };
        }

        private void RegisterWebRTCDataChannelEvents()
        {
            webRTCSendDataChannel.Opened += WebRTCSendDataChannelOpened;
            webRTCSendDataChannel.MessageReceived += WebRTCSendDataChannelMessageReceived;
        }

        private void RegisterWebRTCDataChannelRemoteEvents()
        {
            webRTCReceiveDataChannel.Opened += WebRTCReceiveDataChannelOpened;
            webRTCReceiveDataChannel.MessageReceived += WebRTCReceiveDataChannelMessageReceived;
        }

        void WebRTCStateChanged(object s, WebRTCStateChangedEventArgs e)
        {
            Log.Info(WebRTCLog.Tag, $"State changed : {e.Previous}->{e.Current}");
            if (e.Current == WebRTCState.Negotiating)
            {
                tcsStateNegotiation.TrySetResult(true);
            }
            else if (e.Current == WebRTCState.Playing)
            {
                tcsStatePlaying.TrySetResult(true);
            }
        }

        void WebRTCNegotiationNeeded(object s, EventArgs e)
        {
            Log.Info(WebRTCLog.Tag, $"NegotiationNeeded");
        }

        async void WebRTCIceCandidate(object s, WebRTCIceCandidateEventArgs e)
        {
            if (e.ICECandidate != null)
            {
                Log.Info(WebRTCLog.Tag, $"Local IceCandidate : {e}");
                await webSocketClient.SendAsync(e.ICECandidate);
            }
        }

        void WebRTCTrackAdded(object s, WebRTCTrackAddedEventArgs e)
        {
            if (e.MediaStreamTrack.Type == MediaType.Video && remoteView != null)
            {
                e.MediaStreamTrack.Display = new Display(remoteView);
            }
        }

        void WebRTCDataChannelCreated(object s, WebRTCDataChannelEventArgs e)
        {
            Log.Info(WebRTCLog.Tag, $"DataChannel created");
            webRTCReceiveDataChannel = e.DataChannel;
            RegisterWebRTCDataChannelRemoteEvents();

            tcsDCCreated.TrySetResult(true);
        }

        void WebRTCICEGatheringStateChanged(object s, WebRTCIceGatheringStateChangedEventArgs e)
        {
            if (e.State == WebRTCIceGatheringState.Completed)
            {
                tcsIceGathering.TrySetResult(true);
            }
        }

        void WebRTCSendDataChannelOpened(object s, EventArgs e)
        {
            Log.Info(WebRTCLog.Tag, $"DataChannel Opened");
        }

        void WebRTCSendDataChannelMessageReceived(object s, WebRTCDataChannelMessageReceivedEventArgs e)
        {
            Log.Info(WebRTCLog.Tag, $"Message received : {e.Message}");
        }

        void WebRTCReceiveDataChannelOpened(object s, EventArgs e)
        {
            Log.Info(WebRTCLog.Tag, $"DataChannel Opened");
        }

        void WebRTCReceiveDataChannelMessageReceived(object s, WebRTCDataChannelMessageReceivedEventArgs e)
        {
            Log.Info(WebRTCLog.Tag, $"Message received : {e.Message}");
            if (String.Compare(e.Message, "HELLO_TIZEN") == 0)
            {
                tcsQuit.TrySetResult(true);
            }
            else
            {
                // echo for test
                webRTCSendDataChannel.Send(e.Message);
            }
        }

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
                    // to be used if there are any other disposable objects\
                    cameraController?.Dispose();
                    webSocketClient?.Dispose();
                    webRtcClient?.Dispose();
                }

                _disposed = true;
            }
        }
        #endregion Dispose pattern support
    }
}
