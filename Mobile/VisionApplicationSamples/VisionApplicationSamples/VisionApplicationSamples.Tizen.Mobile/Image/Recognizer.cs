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

using VisionApplicationSamples.Tizen.Mobile.Image;
using VisionApplicationSamples.Image;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms;
using TizenMM = Tizen.Multimedia;
using Tizen.Multimedia.Vision;
using Tizen.Multimedia.Util;


[assembly: Dependency(typeof(Recognizer))]
namespace VisionApplicationSamples.Tizen.Mobile.Image
{
    class Recognizer  : IRecognizer
    {
        private MediaVisionSource _mvTargetSource;
        private MediaVisionSource _mvSceneSource;
        private string _targetImagePath;
        private string _sceneImagePath;

        private ImageObject _targObject;

        private bool _success;
        private Quadrangle _detectedTargetRegion;


        /// <summary>
        /// Converts an Quadrangle type to Points
        /// </summary>
        /// <param name="points"></param>
        /// <returns>A list of points</returns>
        private List<Point> ConvertToPoints(TizenMM::Point[] points)
        {
            List<Point> convertedPoints = new List<Point>();
            foreach (var point in points)
            {
                convertedPoints.Add(new Point(point.X, point.Y));
            }

            return convertedPoints;
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
        /// The result of recognition
        /// </summary>
        public bool Success
        {
            get
            {
                return _success;
            }
        }

        /// <summary>
        /// Set a target image path to be used for image recognition.
        /// </summary>
        public string TargetImagePath
        {
            set
            {
                if (_targetImagePath != value)
                {
                    _targetImagePath = value;
                }
            }
        }

        /// <summary>
        /// Set a scene image path to be used for image recognition.
        /// </summary>
        public string SceneImagePath
        {
            set
            {
                if (_sceneImagePath != value)
                {
                    _sceneImagePath = value;
                }
            }
        }

        /// <summary>
        /// Decodes images and creates MediaVisionSource instances.
        /// </summary>
        public async Task Decode()
        {
            using (var decoder = CreateDecoder(Path.GetExtension(_targetImagePath)))
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
                var result = (await decoder.DecodeAsync(_targetImagePath)).ElementAt(0);

                _mvTargetSource = new MediaVisionSource(result.Buffer, (uint)result.Size.Width, (uint)result.Size.Height, SupportedCSP);
            }

            using (var decoder = CreateDecoder(Path.GetExtension(_sceneImagePath)))
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
                var result = (await decoder.DecodeAsync(_sceneImagePath)).ElementAt(0);

                _mvSceneSource = new MediaVisionSource(result.Buffer, (uint)result.Size.Width, (uint)result.Size.Height, SupportedCSP);
            }
        }

        /// <summary>
        /// Set target object with a given target image.
        /// </summary>
        public void FillTarget()
        {
            _targObject = new ImageObject();
            _targObject.Fill(_mvTargetSource);
        }

        public async Task<string> Recognize()
        {
            ImageObject[] images = new ImageObject[1] { _targObject };

            var objLists = await ImageRecognizer.RecognizeAsync(_mvSceneSource, images);

            foreach (var ResultRecog in objLists)
            {
                _success = ResultRecog.Success;
                _detectedTargetRegion = ResultRecog.Region;
            }
            return _sceneImagePath;
        }

        public List<Point> RecognizedTarget
        {
            get
            {
                return ConvertToPoints(_detectedTargetRegion.Points);
            }
        }

        /*
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
        */

    }
}