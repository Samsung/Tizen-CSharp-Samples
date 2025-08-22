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

using VisionApplicationSamples.Tizen.Mobile.Face;
using VisionApplicationSamples.Face;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms;
using TizenMM = Tizen.Multimedia;
using Tizen.Multimedia.Vision;
using Tizen.Multimedia.Util;


[assembly: Dependency(typeof(Detector))]
namespace VisionApplicationSamples.Tizen.Mobile.Face
{
    class Detector : IDetector
    {
        private MediaVisionSource _mvSource;
        private string _imagePath;

        private int _numberOfFaces = 0;
        private TizenMM::Rectangle[] _detectedFaces;

        TizenMM::Point _point;
        TizenMM::Size _size;

        /// <summary>
        /// Converts an Multimedia Rectangle type to Rentangle
        /// </summary>
        /// <param name="rects"></param>
        /// <returns>A list of Rectangle</returns>
        private List<Rectangle> ConvertToRectangle(List<TizenMM::Rectangle> rects)
        {
            List<Rectangle> convertedRect = new List<Rectangle>();
            foreach (var rect in rects)
            {
                convertedRect.Add(new Rectangle(rect.X, rect.Y, rect.Width, rect.Height));
            }

            return convertedRect;
        }

        /// <summary>
        /// Creates an image decoder based on extension.
        /// </summary>
        /// <param name="extension">The file extension.</param>
        /// <returns>A decoder.</returns>
        private ImageDecoder CreateDecoder(string extension)
        {
            switch (extension.ToLower())
            {
                case ".jpg":
                    return new JpegDecoder();
            }

            throw new InvalidDataException($"Unknow file extension: '{extension}'.");
        }

        /// <summary>
        /// Set a image path to be used for face detection.
        /// </summary>
        public string ImagePath
        {
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                }
            }
        }

        /// <summary>
        /// Decodes an image and creates MediaVisionSource instance.
        /// </summary>
        public async Task Decode()
        {
            using (var decoder = CreateDecoder(Path.GetExtension(_imagePath)))
            {
                TizenMM::ColorSpace SupportedCSP = TizenMM::ColorSpace.Y800;
                foreach (var csp in ImageUtil.GetSupportedColorSpaces(ImageFormat.Jpeg))
                {
                    if (csp == TizenMM::ColorSpace.Rgb888 || csp == TizenMM::ColorSpace.Rgba8888)
                    {
                        SupportedCSP = csp;
                        break;
                    }
                }
                decoder.SetColorSpace(SupportedCSP);
                var result = (await decoder.DecodeAsync(_imagePath)).ElementAt(0);

                _mvSource = new MediaVisionSource(result.Buffer, (uint)result.Size.Width, (uint)result.Size.Height, SupportedCSP);

                _point = new TizenMM::Point(0, 0);
                _size = new TizenMM.Size(result.Size.Width, result.Size.Height);
            }
        }

        /// <summary>
        /// Detects faces.
        /// </summary>
        /// <returns>An image path</returns>
        public async Task<string> Detect()
        {
            _detectedFaces = await FaceDetector.DetectAsync(_mvSource);
            _numberOfFaces = _detectedFaces.Length;

            return _imagePath;
        }

        /// <summary>
        /// The number of the detected faces.
        /// </summary>
        public int NumberOfFace
        {
            get
            {
                return _numberOfFaces;
            }
        }

        /// <summary>
        /// The list of the detected faces' regions.
        /// </summary>
        public List<Rectangle> DetectedFaces
        {
            get
            {
                return ConvertToRectangle(_detectedFaces.ToList());
            }
        }

    }
}
