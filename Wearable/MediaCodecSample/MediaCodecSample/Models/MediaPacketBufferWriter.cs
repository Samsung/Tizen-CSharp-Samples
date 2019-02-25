using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Multimedia;

namespace MediaCodecSample.Models
{
    /// <summary>
    /// MediaPacketBufferWriter class to write and fill the data to the buffer.
    /// </summary>
    class MediaPacketBufferWriter
    {
        private readonly IMediaBuffer _buffer;
        private int _pos;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaPacketBufferWriter"/> class
        /// </summary>
        /// <param name="buffer">
        /// The input buffer.
        /// </param>
        public MediaPacketBufferWriter(IMediaBuffer buffer)
        {
            _buffer = buffer;
        }

        /// <summary>
        /// Writes data to the buffer.
        /// </summary>
        /// <param name="b">
        /// The input byte array.
        /// </param>
        public void Write(params byte[] b)
        {
            foreach (byte t in b)
            {
                _buffer[_pos++] = t;
            }
        }

        /// <summary>
        /// Fill <paramref name="value"/> data to the buffer as much as count number.
        /// </summary>
        /// <param name="value">
        /// The input value to fill.
        /// </param>
        /// <param name="count">
        /// The amount to fill.
        /// </param>
        public void Fill(byte value, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                _buffer[_pos++] = value;
            }
        }

        /// <summary>
        /// Gets the length of this buffer.
        /// </summary>
        public int Length => _pos;
    }
}
