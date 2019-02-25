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

using System;
using System.IO;
using Tizen.Multimedia;

namespace MediaCodecSample
{
    /// <summary>
    /// PacketParser abstract class.
    /// </summary>
    public abstract class PacketParser
    {
        public delegate void PacketReadyDelegate(MediaPacket packet);

        public const int NumberOfFeed = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketParser"/> class.
        /// </summary>
        /// <param name="filePath">
        /// The file path of stored data.
        /// </param>
        protected PacketParser(string filePath)
        {
            FilePath = filePath;
        }

        /// <summary>
        /// Create MediaPacket data and then process packetAction.
        /// </summary>
        /// <param name="packetAction">
        /// The action to process after creating the packet data.
        /// </param>
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
}
