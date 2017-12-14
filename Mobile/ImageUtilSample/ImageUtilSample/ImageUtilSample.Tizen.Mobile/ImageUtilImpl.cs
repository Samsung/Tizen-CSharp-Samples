/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
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

using ImageUtilSample.Tizen.Mobile;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tizen.Multimedia;
using Tizen.Multimedia.Util;
using Xamarin.Forms;
using TizenApp = Tizen.Applications.Application;

[assembly: Dependency(typeof(ImageUtilImpl))]
namespace ImageUtilSample.Tizen.Mobile
{
    class ImageUtilImpl : IImageUtil
    {
        public string ImagePath { get; set; }

        public string ResultPath => Path.Combine(TizenApp.Current.DirectoryInfo.Data, "transformed");

        public int ImageWidth => _imageSize.Width;

        public int ImageHeight => _imageSize.Height;

        private MediaPacket _decodedPacket;
        private global::Tizen.Multimedia.Size _imageSize;

        /// <summary>
        /// Creates an image decoder based on extension.
        /// </summary>
        /// <param name="extension">The file extension.</param>
        /// <returns>A decoder.</returns>
        private ImageDecoder CreateDecoder(string extension)
        {
            switch (extension.ToLower())
            {
                case ".png":
                    return new PngDecoder();
                case ".jpg":
                    return new JpegDecoder();
                case ".gif":
                    return new GifDecoder();
                case ".bmp":
                    return new BmpDecoder();
            }

            throw new InvalidDataException($"Unknown file extension : '{extension}'.");
        }

        public async Task Decode()
        {
            using (var decoder = CreateDecoder(Path.GetExtension(ImagePath)))
            {
                var result = (await decoder.DecodeAsync(ImagePath)).ElementAt(0);
                _imageSize = result.Size;

                // Creates a media packet.
                _decodedPacket =
                    MediaPacket.Create(new VideoMediaFormat(MediaFormatVideoMimeType.Rgba, result.Size));

                // Fills the buffer with decoded data.
                _decodedPacket.VideoPlanes[0].Buffer.CopyFrom(result.Buffer, 0, result.Buffer.Length);
            }
        }

        private byte[] CopyBuffer(MediaPacketVideoPlane[] planes)
        {
            var buffer = new byte[planes.Aggregate(0, (sum, plane) => sum + plane.Buffer.Length)];

            var bufferPos = 0;

            foreach (var plane in planes)
            {
                plane.Buffer.CopyTo(buffer, bufferPos, plane.Buffer.Length);
                bufferPos += plane.Buffer.Length;
            }

            return buffer;
        }

        private async Task Transform(ImageTransform item, ColorSpace resultColorSpace = ColorSpace.Rgba8888)
        {
            using (var transformer = new ImageTransformer())
            {
                // If the item is not a ColorSpaceTransform,
                // the result color space is always rgba as the _decodedPacket is.
                var resultPacket = await transformer.TransformAsync(_decodedPacket, item);

                using (var encoder = new JpegEncoder())
                using (var fs = File.OpenWrite(ResultPath))
                {
                    encoder.SetResolution((resultPacket.Format as VideoMediaFormat).Size);

                    // We set input color space as the color space of the result.
                    encoder.SetColorSpace(resultColorSpace);

                    // Encodes the result(which is raw format) as a jpeg image.
                    await encoder.EncodeAsync(CopyBuffer(resultPacket.VideoPlanes), fs);
                }
            }
        }

        public Task Rotate(TransformRotation rotate)
        {
            return Transform(new RotateTransform(Enum.Parse<Rotation>(rotate.ToString())));
        }

        public Task Flip(TransformFlip flip)
        {
            return Transform(new FlipTransform(Enum.Parse<Flips>(flip.ToString())));
        }

        public Task ChangeColorSpace(TransformColorSpace transformColorSpace)
        {
            var colorSpace = Enum.Parse<ColorSpace>(transformColorSpace.ToString());

            return Transform(new ColorSpaceTransform(colorSpace), colorSpace);
        }

        public Task Resize(int width, int height)
        {
            return Transform(new ResizeTransform(new global::Tizen.Multimedia.Size(width, height)));
        }

        public Task Crop(int left, int top, int width, int height)
        {
            return Transform(
                new CropTransform(new global::Tizen.Multimedia.Rectangle(left, top, width, height)));
        }
    }
}
