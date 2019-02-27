using System;
using System.IO;
using Tizen.Multimedia;
using MediaToolSample;
using MediaToolSample.Models;

namespace MediaToolSample
{
    /// <summary>
    /// VideoViewController to get window
    /// </summary>
    public static class VideoViewController
    {
        public static Func<ElmSharp.Window> MainWindowProvider { get; set; }
    }

    /// <summary>
    /// MediaPacketBufferWriter class to write buffers
    /// </summary>
    class MediaPacketBufferWriter
    {
        private readonly IMediaBuffer _buffer;
        private int _pos;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaPacketBufferWriter"/> class
        /// </summary>
        /// <param name="buffer">A media buffer to write</param>
        public MediaPacketBufferWriter(IMediaBuffer buffer)
        {
            _buffer = buffer;
        }

        /// <summary>
        /// Writes a buffer
        /// </summary>
        /// <param name="b"> A buffer </param>
        public void Write(params byte[] b)
        {
            foreach (byte t in b)
            {
                _buffer[_pos++] = t;
            }
        }

        /// <summary>
        /// Fills a buffer
        /// </summary>
        /// <param name="value"> A buffer to read </param>
        /// <param name="count"> the count for buffers </param>
        public void Fill(byte value, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                _buffer[_pos++] = value;
            }
        }

        public int Length => _pos;
    }

    /// <summary>
    /// PacketParser class to parse packets
    /// </summary>
    public abstract class PacketParser
    {

        public delegate void PacketReadyDelegate(MediaPacket packet);

        // the number of packets to feed
        public const int NumberOfFeed = 500;

        protected PacketParser(string filePath)
        {
            Logger.Log(filePath);
            FilePath = filePath;
        }

        /// <summary>
        /// Feed packets
        /// </summary>
        /// <param name="packetAction">A action with MediaPacket</param>
        public void Feed(Action<MediaPacket> packetAction)
        {
            int frameCount = 0;

            using (Stream file = File.OpenRead(FilePath))
            {
                while (frameCount < NumberOfFeed)
                {
                    FeedData(packetAction, file, frameCount++);
                }
            }
        }

        /// <summary>
        /// Feed a data
        /// </summary>
        /// <param name="packetAction">A action with MediaPacket</param>
        /// <param name="file"> A file </param>
        /// <param name="frameCount"> A count of frames</param>
        private void FeedData(Action<MediaPacket> packetAction, Stream file, int frameCount)
        {
            MediaPacket packet = CreatePacket(file, frameCount);

            if (packet == null)
            {
                return;
            }

            packetAction(packet);
        }

        protected string FilePath { get; }

        protected abstract MediaPacket CreatePacket(Stream file, int frameCount);
    }

    /// <summary>
    /// VideoDecoderParser class to decode video packet
    /// </summary>
    public class VideoDecoderParser : PacketParser
    {
        public static readonly VideoMediaFormat Format =
            new VideoMediaFormat(MediaFormatVideoMimeType.H264SP, 640, 480);

        private const int ES_DEFAULT_VIDEO_PTS_OFFSET = 20000000;

        private static ulong _pts;
        private static byte[] _sps;
        private static byte[] _pps;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoDecoderParser"/> class
        /// </summary>
        /// <param name="filePath">A path of the file</param>
        public VideoDecoderParser(string filePath)
            : base(filePath)
        {
        }

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


        private static int Extract(Stream file, IMediaBuffer buffer)
        {
            MediaPacketBufferWriter writer = new MediaPacketBufferWriter(buffer);
            int unitType = 0;

            while (file.ReadByte() == 0)
            {
            };

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
