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

using System.IO;
using Tizen.Multimedia;

namespace MediaCodecSample
{
    /// <summary>
    /// AudioDataFeeder class to feed test mediapacket data for this sample.
    /// </summary>
    internal class AudioDataFeeder : PacketParser
    {
        /// <summary>
        /// The path for audio pcm file.
        /// </summary>
        private const string _pcmFilePath = "/opt/usr/globalapps/org.tizen.example.MediaCodecSample/res/test.pcm";

        /// <summary>
        /// The pcm bit.
        /// </summary>
        private const int testPcmBit = 32;

        /// <summary>
        /// Represents audio media format.
        /// </summary>
        public static readonly MediaFormat Format =
            new AudioMediaFormat(MediaFormatAudioMimeType.Pcm, 2, 44100, testPcmBit, 128000);

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioDataFeeder"/> class
        /// </summary>
        public AudioDataFeeder() : base(_pcmFilePath)
        {
        }

        /// <summary>
        /// Creates MediaPacket from input file stream.
        /// </summary>
        /// <param name="file">
        /// The file stream.
        /// </param>
        /// <param name="frameCount">
        /// The count to control.
        /// </param>
        /// <returns>
        /// The MediaPacket that created based on <paramref name="file"/>.
        /// </returns>
        protected override MediaPacket CreatePacket(Stream file, int frameCount)
        {
            MediaPacket packet = MediaPacket.Create(Format);

            const int readSize = 1024 * 2 * (testPcmBit / 8) * 2;
            byte[] arr = new byte[readSize];

            file.Read(arr, 0, readSize);

            packet.Buffer.CopyFrom(arr, 0, readSize);
            packet.BufferWrittenLength = readSize;

            if (frameCount == 0)
            {
                packet.BufferFlags |= MediaPacketBufferFlags.CodecConfig;
            }
            else if (frameCount == NumberOfFeed - 1)
            {
                packet.BufferFlags |= MediaPacketBufferFlags.EndOfStream;
            }

            return packet;
        }
    }
}
