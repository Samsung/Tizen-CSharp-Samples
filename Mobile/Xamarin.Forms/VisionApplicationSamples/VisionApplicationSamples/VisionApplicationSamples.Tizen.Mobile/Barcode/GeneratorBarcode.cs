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

using System.IO;
using Xamarin.Forms;
using Tizen.Multimedia.Vision;
using TizenApp = Tizen.Applications.Application;
using VisionApplicationSamples.Barcode;
using VisionApplicationSamples.Tizen.Mobile.Barcode;


[assembly: Dependency(typeof(GeneratorBarcode))]
namespace VisionApplicationSamples.Tizen.Mobile.Barcode
{
    class GeneratorBarcode : IGeneratorBarcode
    {
        private string _filePath = Path.Combine(TizenApp.Current.DirectoryInfo.Data, "barcode");
        private VisionApplicationSamples.Barcode.BarcodeType _barcodeType;

        public GeneratorBarcode()
        {
            _barcodeType = VisionApplicationSamples.Barcode.BarcodeType.Code128;
        }

        public void SetBarcodeType(VisionApplicationSamples.Barcode.BarcodeType barcodeType)
        {
            _barcodeType = barcodeType;
        }

        private global::Tizen.Multimedia.Vision.BarcodeType ConvertType(VisionApplicationSamples.Barcode.BarcodeType barcodeType)
        {
            global::Tizen.Multimedia.Vision.BarcodeType convertedBarcodeType;
            switch (barcodeType)
            {
                case VisionApplicationSamples.Barcode.BarcodeType.Upca:
                    convertedBarcodeType = global::Tizen.Multimedia.Vision.BarcodeType.UpcA;
                    break;
                case VisionApplicationSamples.Barcode.BarcodeType.Upce:
                    convertedBarcodeType = global::Tizen.Multimedia.Vision.BarcodeType.UpcE;
                    break;
                case VisionApplicationSamples.Barcode.BarcodeType.Ean8:
                    convertedBarcodeType = global::Tizen.Multimedia.Vision.BarcodeType.Ean8;
                    break;
                case VisionApplicationSamples.Barcode.BarcodeType.Ean13:
                    convertedBarcodeType = global::Tizen.Multimedia.Vision.BarcodeType.Ean13;
                    break;
                case VisionApplicationSamples.Barcode.BarcodeType.Code128:
                    convertedBarcodeType = global::Tizen.Multimedia.Vision.BarcodeType.Code128;
                    break;
                case VisionApplicationSamples.Barcode.BarcodeType.Code39:
                    convertedBarcodeType = global::Tizen.Multimedia.Vision.BarcodeType.Code39;
                    break;
                case VisionApplicationSamples.Barcode.BarcodeType.I25:
                    convertedBarcodeType = global::Tizen.Multimedia.Vision.BarcodeType.I25;
                    break;
                default:
                    convertedBarcodeType = global::Tizen.Multimedia.Vision.BarcodeType.Code128;
                    break;
            }
            return convertedBarcodeType;
        }
        public string Generate(int width, int height, string message)
        {
            string targetFilePath = _filePath + "_" + _barcodeType.ToString();
            BarcodeImageConfiguration imageConfiguration = new BarcodeImageConfiguration(width, height, targetFilePath, BarcodeImageFormat.Jpeg);
            BarcodeGenerator.GenerateImage(message, ConvertType(_barcodeType), imageConfiguration);

            return targetFilePath + ".jpg";
        }
    }
}
