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

using MediaToolSample.Views;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Multimedia;
using System.Collections.Generic;

namespace MediaToolSample.ViewModels
{
    /// <summary>
    /// ViewModel class for the Main Page
    /// </summary>
    class MainPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets command for pushing new page
        /// </summary>
        // A command to create audio format
        public ICommand CreateAudioFormatCommand { get; protected set; }
        // A command to create video format
        public ICommand CreateVideoFormatCommand { get; protected set; }
        // A command to create audio packet
        public ICommand CreateAudioPacketCommand { get; protected set; }
        // A command to create video format
        public ICommand CreateVideoPacketCommand { get; protected set; }
        // A command to destroy media packet and set format to null
        public ICommand DestroyAllCommand { get; protected set; }
        // A command to get the information of current format
        public ICommand GetFormatCommand { get; protected set; }
        public ICommand SamplePlayCommand { get; protected set; }

        /// <summary>
        /// Gets the Navigation instance to push new pages properly
        /// </summary>
        public INavigation Navigation { get; }

        private MediaFormat _audioformat;
        /// <summary>
        /// Sets and gets the audio format.
        /// </summary>
        public MediaFormat AudioFormat
        {
            get => _audioformat;
            protected set
            {
                if (_audioformat != value)
                {
                    _audioformat = value;

                    OnPropertyChanged(nameof(AudioFormat));
                }
            }
        }

        private MediaFormat _videoformat;
        /// <summary>
        /// Sets and gets the video format.
        /// </summary>
        public MediaFormat VideoFormat
        {
            get => _videoformat;
            protected set
            {
                if (_videoformat != value)
                {
                    _videoformat = value;

                    OnPropertyChanged(nameof(VideoFormat));
                }
            }
        }

        private MediaPacket _mediapacket;
        /// <summary>
        /// Sets and gets the MediaPacket.
        /// </summary>
        public MediaPacket MediaPacket
        {
            get => _mediapacket;
            protected set
            {
                if (_mediapacket != value)
                {
                    _mediapacket = value;

                    OnPropertyChanged(nameof(MediaPacket));
                }
            }
        }

        /// <summary>
        /// Sets information of a packet.
        /// </summary>
        /// <param name="mediaPacket">MediaPacket to set information</param>
        public void SetPacketInformation(MediaPacket mediaPacket)
        {
            MediaPacketBufferFlags flags = MediaPacketBufferFlags.CodecConfig | MediaPacketBufferFlags.EndOfStream;
            mediaPacket.BufferFlags = flags;
            mediaPacket.Rotation = 0;
            mediaPacket.Flip = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class
        /// </summary>
        /// <param name="navigation">Navigation instance</param>
        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            // A command to create audio format
            CreateAudioFormatCommand = new Command(() =>
            {
                try
                {
                    // checks if all audio format can be made
                    foreach (MediaFormat f in GetFormat(false) ?? throw new Exception("A format is not created."))
                    {
                        AudioFormat = f;
                        Models.Logger.Log(AudioFormat.ToString() + "is created.");
                    }

                    InformationPopup("AudioFormat is created.");
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });
            // A command to create video format
            CreateVideoFormatCommand = new Command(() =>
            {
                try
                {
                    // checks if all video format can be made
                    foreach (MediaFormat f in GetFormat(true) ?? throw new Exception("A format is not created."))
                    {
                        VideoFormat = f;
                        Models.Logger.Log(VideoFormat.ToString() + "is created.");
                    }

                    InformationPopup("VideoFormat is created.");
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });
            // A command to create audio packet
            CreateAudioPacketCommand = new Command(() =>
            {
                try
                {
                    // throws exception if audio format is null
                    if (AudioFormat == null)
                    {
                        throw new Exception("MediaFormat is not created.");
                    }
                    
                    // makes packet with audio format that is made before
                    MediaPacket = MediaPacket.Create(AudioFormat);
                    SetPacketInformation(MediaPacket);
                    InformationPopup("MediaPacket is set with AudioFormat.");
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });
            // A command to create video packet
            CreateVideoPacketCommand = new Command(() =>
            {
                try
                {
                    // throws exception if video format is null
                    if (VideoFormat == null)
                    {
                        throw new Exception("MediaFormat is not created.");
                    }

                    // makes packet with video format that is made before
                    MediaPacket = MediaPacket.Create(VideoFormat);
                    SetPacketInformation(MediaPacket);
                    InformationPopup("MediaPacket is set with VideoFormat.");
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });
            // A command to destroy media packet
            DestroyAllCommand = new Command(() =>
            {
                // initializes formats
                AudioFormat = VideoFormat = null;
                var text = "Formats are initialized. but ";

                try
                {
                    // throws exception if mediapacket is disposed already
                    if (MediaPacket.IsDisposed)
                    {
                        throw new Exception("MediaPacket is already destroyed.");
                    }

                    MediaPacket.Dispose();
                    InformationPopup("MediaFormats and MediaPacket are initialized.");
                }
                catch (Exception e)
                {
                    text += e.Message;
                    Toast.DisplayText(text, 1000);
                }
            });
            // A command to get the information of current format
            GetFormatCommand = new Command(() =>
            {
                try
                {
                    InformationPopup(MediaPacket.Format.ToString());
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });
            SamplePlayCommand = new Command(PushPlayPage);
        }

        /// <summary>
        /// Get a MediaFormat
        /// </summary>
        /// <param name="video">Checks whether or not it is video format</param>
        /// <returns>New MediaFormat</returns>
        public static IEnumerable<MediaFormat> GetFormat(bool video)
        {
            if (video == true)
            {
                // makes new argb format having width 640, height 480.
                yield return new VideoMediaFormat(MediaFormatVideoMimeType.Argb, 640, 480);
                // makes new argb format having size(160, 120).
                yield return new VideoMediaFormat(MediaFormatVideoMimeType.Argb, new Tizen.Multimedia.Size(160, 120));
                // makes new argb format having width 1280, height 720 and frameRate 10000.
                yield return new VideoMediaFormat(MediaFormatVideoMimeType.Argb, 1280, 720, 10000);
                // makes new argb format having width 320, height 240, frameRate 20000 and bitRate 300000.
                yield return new VideoMediaFormat(MediaFormatVideoMimeType.Argb, 320, 240, 20000, 300000);
                // makes new argb format having size(320, 240), frameRate 50000 and bitRate 300000.
                yield return new VideoMediaFormat(MediaFormatVideoMimeType.Argb, new Tizen.Multimedia.Size(320, 240), 50000, 300000);
            }
            else
            {
                // makes new aac format having channel 4, sampleRate 3, bit 2 and bitRate 1.
                yield return new AudioMediaFormat(MediaFormatAudioMimeType.Aac, 4, 3, 2, 1);
                // makes new aac format having channel 1, sampleRate 2, bit 3, bitRate 4 and MediaFormatAacType.None.
                yield return new AudioMediaFormat(MediaFormatAudioMimeType.Aac, 1, 2, 3, 4, MediaFormatAacType.None);
            }
        }

        /// <summary>
        /// Makes information popup.
        /// </summary>
        /// <param name="text">A text to show on popup</param>
        public static void InformationPopup(string text)
        {
            var infoPopUp = new InformationPopup();
            if (infoPopUp != null)
            {
                infoPopUp.Text = text;
                infoPopUp.Show();
            }

            infoPopUp.BackButtonPressed += (s, e) =>
            {
                infoPopUp.Dismiss();
            };
        }

        /// <summary>
        /// Pushes new page for playing sample.
        /// </summary>
        private void PushPlayPage()
        {
            Navigation.PushModalAsync(new PlayPage());
        }

        /// <summary>
        /// Invoked when this view disappears.
        /// </summary>
        public override void OnDisappearing()
        {
            base.OnDisappearing();

            AudioFormat = VideoFormat = null;

            if (MediaPacket != null)
            {
                MediaPacket.Dispose();
            }
        }
    }
}
