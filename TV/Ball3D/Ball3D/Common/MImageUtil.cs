/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using System.IO;

namespace Ball3D
{
    public static class MImageUtil
    {
        /// <summary>
        /// Load bmp information from the image. 
        /// </summary>
        /// <param name="path">path of image</param>
        /// <returns>Image information per pixel</returns>
        public static MBitmap LoadImage(string path)
        {
            MBitmap bitmap = new MBitmap();

            // Read bitmap image from path.
            Stream imageStream = File.OpenRead(path);
            BinaryReader reader = new BinaryReader(imageStream);

            // Parse the bitmap information
            bitmap.header.bfType = reader.ReadUInt16();
            bitmap.header.bfSize = reader.ReadUInt32();
            bitmap.header.bfReserved1 = reader.ReadUInt16();
            bitmap.header.bfReserved2 = reader.ReadUInt16();
            bitmap.header.bfOffBytes = reader.ReadUInt32();

            bitmap.inforHeader.biSize = reader.ReadUInt32();
            bitmap.inforHeader.biWidth = reader.ReadUInt32();
            bitmap.inforHeader.biHeight = reader.ReadUInt32();
            bitmap.inforHeader.biPlanes = reader.ReadUInt16();
            bitmap.inforHeader.biBitCount = reader.ReadUInt16();
            bitmap.inforHeader.biCompression = reader.ReadUInt32();
            bitmap.inforHeader.biSizeImage = reader.ReadUInt32();
            bitmap.inforHeader.biXPelsPerMeter = reader.ReadUInt32();
            bitmap.inforHeader.biYPelsPerMeter = reader.ReadUInt32();
            bitmap.inforHeader.biClrUsed = reader.ReadUInt32();
            bitmap.inforHeader.biClrImportant = reader.ReadUInt32();
            byte[] temp = reader.ReadBytes((int)reader.BaseStream.Length);
            bitmap.byteBuffer = new byte[temp.Length];
            for (int i = 2; i < temp.Length; i += 3)
            {
                bitmap.byteBuffer[i - 2] = temp[i];
                bitmap.byteBuffer[i - 1] = temp[i - 1];
                bitmap.byteBuffer[i] = temp[i - 2];
            }

            return bitmap;
        }
    }

    public class MBitmap
    {
        public BitMapPicHeader header;
        public BitMapPicInfoHeader inforHeader;
        public byte[] byteBuffer;
        /// <summary>
        /// Constructor of MBitmap
        /// </summary>
        public MBitmap()
        {
            header = new BitMapPicHeader();
            inforHeader = new BitMapPicInfoHeader();
        }
    }

    /// <summary>
    /// Define the head of bitmap
    /// </summary>
    public class BitMapPicHeader
    {
        // file type, it must be 0x4d42 for bitmap
        public ushort bfType;
        // file size
        public uint bfSize;
        // reserved byte
        public ushort bfReserved1;
        public ushort bfReserved2;
        // offset of pixel data
        public uint bfOffBytes;
    };

    /* 位图文件的信息头 */
    /// <summary>
    /// Define the pixel information of bitmap
    /// </summary>
    public class BitMapPicInfoHeader
    {
        // size of this struct
        public uint biSize;
        // width of the bitmap
        public uint biWidth;
        // height of the bitmap
        public uint biHeight;
        public ushort biPlanes;
        // count of one pixel
        public ushort biBitCount;
        public uint biCompression;
        // size of the picture
        public uint biSizeImage;
        // x resolution
        public uint biXPelsPerMeter;
        // y resolution
        public uint biYPelsPerMeter;
        public uint biClrUsed;
        public uint biClrImportant;
    };
}
