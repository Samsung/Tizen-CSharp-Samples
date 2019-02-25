//Copyright 2019 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using MediaCodecSample.Models;
using System;
using System.Threading;
using System.Collections.Generic;
using Tizen.Multimedia;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Multimedia.MediaCodec;
using Xamarin.Forms;

namespace MediaCodecSample.ViewModels
{
    /// <summary>
    /// EncodingViewModel class for the media encoding
    /// </summary>
    public class EncodingViewModel : ViewModelBase
    {
        private MediaCodec _mediaCodec;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodingViewModel"/> class
        /// </summary>
        /// <param name="navigation">
        /// Navigation instance
        /// </param>
        /// <param name="type">
        /// The type of media format
        /// </param>
        public EncodingViewModel(INavigation navigation, MediaFormatType type)
        {
            MediaType = type;
            Navigation = navigation;

            Initialize(type);
        }

        /// <summary>
        /// Gets the Navigation instance to push new pages properly
        /// </summary>
        public INavigation Navigation { get; }

        /// <summary>
        /// The media type for encoding
        /// </summary>
        /// <seealso cref="MediaFormatType"/>
        public MediaFormatType MediaType { get; }

        /// <summary>
        /// Gets the supported codecs
        /// </summary>
        public List<string> SupportedCodecs { get; private set; }

        /// <summary>
        /// Gets or sets command for pushing new page with found files
        /// </summary>
        public Command StartEncodingCommand { get; set; }

        /// <summary>
        /// Initializes MediaCodec EncodingViewModel
        /// </summary>
        /// <param name="type">
        /// The type of media format.
        /// </param>
        private void Initialize(MediaFormatType type)
        {
            _mediaCodec = new MediaCodec();

            SupportedCodecs = new List<string>();

            if (type == MediaFormatType.Audio)
            {
                foreach (var codec in MediaCodec.SupportedAudioCodecs)
                {
                    // Workaround. 
                    // Even if target doesn't support both encoder and decoder,
                    // SupportedAudioCodecs property returns 'supported'.
                    // So we need to check it with below api, GetCodecType.
                    // 0 means it's not supported.
                    if (_mediaCodec.GetCodecType(true, codec) != 0)
                    {
                        SupportedCodecs.Add(codec.ToString());
                    }
                }
            }
            else
            {
                foreach (var codec in MediaCodec.SupportedVideoCodecs)
                {
                    if (_mediaCodec.GetCodecType(true, codec) != 0)
                    {
                        SupportedCodecs.Add(codec.ToString());
                    }
                }
            }

            RegisterCommands();
        }

        /// <summary>
        /// Registers command to start encoding.
        /// </summary>
        private void RegisterCommands()
        {
            StartEncodingCommand = new Command<string>((codec) =>
            {
                // Set codec
                if (MediaType == MediaFormatType.Audio)
                {
                    var selectedCodec = (MediaFormatAudioMimeType)Enum.Parse(typeof(MediaFormatAudioMimeType), codec);
                    ProcessEncoding(selectedCodec);
                }
                else if (MediaType == MediaFormatType.Video)
                {
                    var selectedCodec = (MediaFormatVideoMimeType)Enum.Parse(typeof(MediaFormatVideoMimeType), codec);
                    ProcessEncoding(selectedCodec);
                }
                else
                {
                    throw new ArgumentException(nameof(codec));
                }
            });
        }

        /// <summary>
        /// Gets icon path.
        /// </summary>
        private string _iconPath = "image/tw_ic_popup_btn_check.png";

        /// <summary>
        /// Process audio encoding.
        /// </summary>
        /// <param name="codec">
        /// The type of audio encoder
        /// </param>
        private void ProcessEncoding(MediaFormatAudioMimeType codec)
        {
            var countdownEvent = new CountdownEvent(3);
            var audioInfoSet = new Dictionary<MediaFormatAudioMimeType, (int, int, int, int)>()
            {
                { MediaFormatAudioMimeType.Aac, (2, 48000, 32, 128) },
                { MediaFormatAudioMimeType.AacHE, (2, 48000, 32, 128) },
                { MediaFormatAudioMimeType.AacHEPS, (2, 48000, 32, 128) },
                { MediaFormatAudioMimeType.AmrNB, (1, 8000, 16, 102) },
                { MediaFormatAudioMimeType.MP3, (2, 48000, 16, 128) },
                { MediaFormatAudioMimeType.Vorbis, (2, 48000, 32, 128) },
                { MediaFormatAudioMimeType.Flac, (2, 48000, 32, 128) },
            };

            void outputAvailableHandler(object sender, OutputAvailableEventArgs e)
            {
                e.Packet.Dispose();
                if (countdownEvent.IsSet == false)
                {
                    countdownEvent.Signal();
                }
            }

            _mediaCodec.OutputAvailable += outputAvailableHandler;

            audioInfoSet.TryGetValue(codec, out (int channel, int sampleRate, int bit, int bitRate) aInfo);
            _mediaCodec.Configure(
                new AudioMediaFormat(codec, aInfo.channel, aInfo.sampleRate, aInfo.bit, aInfo.bitRate),
                true, MediaCodecTypes.Software);

            _mediaCodec.Prepare();

            new AudioDataFeeder().Feed(packet => _mediaCodec.ProcessInput(packet));

            if (countdownEvent.Wait(4000))
            {
                _mediaCodec.OutputAvailable -= outputAvailableHandler;

                Toast.DisplayIconText($"Success, {codec}",
                    new FileImageSource { File = _iconPath }, 1500);
            }
            else
            {
                throw new InvalidOperationException("Failed to encode.");
            }

            _mediaCodec.Unprepare();
        }

        /// <summary>
        /// Process video encoding.
        /// </summary>
        /// <param name="codec">
        /// The type of video encoder
        /// </param>
        private void ProcessEncoding(MediaFormatVideoMimeType codec)
        {
            CountdownEvent countdownEvent = new CountdownEvent(PacketParser.NumberOfFeed - 2);

            var resolutionSet = new Dictionary<MediaFormatVideoMimeType, Tizen.Multimedia.Size>()
            {
                {MediaFormatVideoMimeType.H261, new Tizen.Multimedia.Size(352, 288) },
                {MediaFormatVideoMimeType.H263, new Tizen.Multimedia.Size(128, 96) },
                {MediaFormatVideoMimeType.H263P, new Tizen.Multimedia.Size(640, 480) },
                {MediaFormatVideoMimeType.H264SP, new Tizen.Multimedia.Size(352, 288) },
                {MediaFormatVideoMimeType.H264MP, new Tizen.Multimedia.Size(640, 480) },
                {MediaFormatVideoMimeType.H264HP, new Tizen.Multimedia.Size(640, 480) },
                {MediaFormatVideoMimeType.Mpeg4Asp, new Tizen.Multimedia.Size(640, 480) },
                {MediaFormatVideoMimeType.Mpeg4SP, new Tizen.Multimedia.Size(640, 480) },
            };

            resolutionSet.TryGetValue(codec, out Tizen.Multimedia.Size size);

            void inputProcessedHandler(object sender, InputProcessedEventArgs e)
            {
                e.Packet.Dispose();
                countdownEvent.Signal();
            }

            _mediaCodec.InputProcessed += inputProcessedHandler;

            _mediaCodec.Configure(new VideoMediaFormat(codec, size.Width, size.Height),
                true, MediaCodecTypes.Software);
            _mediaCodec.Prepare();

            new VideoDataFeeder().Feed(packet => _mediaCodec.ProcessInput(packet));

            if (countdownEvent.Wait(4000))
            {
                _mediaCodec.InputProcessed -= inputProcessedHandler;
                Toast.DisplayIconText($"Success, {codec}",
                    new FileImageSource { File = _iconPath }, 1500);
            }
            else
            {
                throw new InvalidOperationException("Failed to encode.");
            }

            _mediaCodec.Unprepare();
        }

        /// <summary>
        /// Invoked when this view appears.
        /// </summary>
        public override void OnAppearing()
        {
            base.OnAppearing();
        }

        /// <summary>
        /// Invoked when this view disappears.
        /// </summary>
        public override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
