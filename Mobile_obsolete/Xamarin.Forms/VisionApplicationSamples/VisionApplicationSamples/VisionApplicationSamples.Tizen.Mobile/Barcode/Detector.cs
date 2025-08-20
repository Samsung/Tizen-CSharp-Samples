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

using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Tizen.Multimedia.Util;
using Tizen.Multimedia.Vision;
using Xamarin.Forms;
using TizenMM = Tizen.Multimedia;
using VisionApplicationSamples.Barcode;
using VisionApplicationSamples.Tizen.Mobile.Barcode;
using System.Collections.Generic;

[assembly: Dependency(typeof(Detector))]
namespace VisionApplicationSamples.Tizen.Mobile.Barcode
{
    class Detector : IDetector
    {
        private MediaVisionSource _mvSource;
        private string _imagePath;

        private int _numberOfBarcodes;
        private List<string> _messages;

        TizenMM::Point _point;
        TizenMM::Size _size;

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
            }
            throw new InvalidDataException($"Unknow file extension: '{extension}'.");
        }

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
        /// Detects barcodes.
        /// </summary>
        /// <returns>A message.</returns>
        public async Task<string> Detect()
        {
            TizenMM::Rectangle roi = new TizenMM::Rectangle(_point, _size);

            var detectedBarcode = await BarcodeDetector.DetectAsync(_mvSource, roi);

            _numberOfBarcodes = detectedBarcode.Count();
            _messages = new List<string>();

            foreach (TizenMM.Vision.Barcode barcode in detectedBarcode)
            {
                _messages.Add(barcode.Message);
            }

            return _imagePath;
        }

        public int NumberOfBarcodes
        {
            get
            {
                return _numberOfBarcodes;
            }
        }

        public List<string> Messages
        {
            get
            {
                return _messages;
            }

        }
    }
}
