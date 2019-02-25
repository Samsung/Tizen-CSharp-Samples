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

namespace MediaCodecSample.Models
{
    /// <summary>
    /// VideoDataFeeder class to feed test mediapacket data for this sample.
    /// </summary>
    public class VideoDataFeeder : PacketParser
    {
        /// <summary>
        /// The path for video h264 file.
        /// </summary>
        private const string _videoFilePath = "/opt/usr/globalapps/org.tizen.example.MediaCodecSample/res/test.h264";

        /// <summary>
        /// Represents video media format.
        /// </summary>
        public static readonly VideoMediaFormat Format =
            new VideoMediaFormat(MediaFormatVideoMimeType.H264SP, 640, 480);

        private const int ES_DEFAULT_VIDEO_PTS_OFFSET = 20000000;

        private static ulong _pts;
        private static byte[] _sps;
        private static byte[] _pps;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoDataFeeder"/> class
        /// </summary>
        public VideoDataFeeder()
            : base(_videoFilePath)
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

            packet.Pts = _pts / 1000000;

            packet.BufferWrittenLength = Extract(file, packet.Buffer);

            if (packet.BufferWrittenLength == 0)
            {
                packet.Dispose();
                return null;
            }

            if (frameCount == 2)
            {
                packet.BufferFlags |= MediaPacketBufferFlags.CodecConfig;
            }
            else if (frameCount == NumberOfFeed - 1)
            {
                packet.BufferFlags |= MediaPacketBufferFlags.EndOfStream;
            }

            _pts += ES_DEFAULT_VIDEO_PTS_OFFSET;

            return packet;
        }

        /// <summary>
        /// Extract <paramref name="file"/> stream to <paramref name="buffer"/>.
        /// </summary>
        /// <param name="file">
        /// The input file stream.
        /// </param>
        /// <param name="buffer">
        /// The output buffer.
        /// </param>
        /// <returns>
        /// The length of buffer.
        /// </returns>
        private static int Extract(Stream file, IMediaBuffer buffer)
        {
            MediaPacketBufferWriter writer = new MediaPacketBufferWriter(buffer);
            int unitType = 0;

            while (file.ReadByte() == 0)
            {
                ;
            }

            writer.Write(0, 0, 0, 1);

            int zeroCount = 0;

            bool init = true;
            while (true)
            {
                if (file.Length == file.Position)
                {
                    return writer.Length;
                }

                int val = file.ReadByte();
                if (val < 0)
                {
                    break;
                }

                if (init)
                {
                    unitType = val & 0xf;
                    init = false;
                }

                if (val == 0)
                {
                    zeroCount++;
                }
                else
                {
                    if ((zeroCount == 2 || zeroCount == 3 || zeroCount == 4) && val == 1)
                    {
                        break;
                    }

                    writer.Fill(0, zeroCount);
                    writer.Write((byte)val);
                    zeroCount = 0;
                }
            }

            file.Position -= zeroCount + 1;

            if (unitType == 0x7)
            {
                _sps = new byte[writer.Length];
                buffer.CopyTo(_sps, 0, writer.Length);
                return 0;
            }
            else if (unitType == 0x8)
            {
                _pps = new byte[writer.Length];
                buffer.CopyTo(_pps, 0, writer.Length);
                return 0;
            }
            else if (unitType == 0x5)
            {
                byte[] tmpBuf = new byte[writer.Length];
                buffer.CopyTo(tmpBuf, 0, writer.Length);
                int offset = 0;
                if (_sps != null)
                {
                    buffer.CopyFrom(_sps, 0, _sps.Length);
                    offset += _sps.Length;
                }

                if (_pps != null)
                {
                    buffer.CopyFrom(_pps, 0, _pps.Length, offset);
                    offset += _pps.Length;
                }

                buffer.CopyFrom(tmpBuf, 0, writer.Length, offset);

                return writer.Length + offset;
            }

            return writer.Length;
        }
    }    
}
