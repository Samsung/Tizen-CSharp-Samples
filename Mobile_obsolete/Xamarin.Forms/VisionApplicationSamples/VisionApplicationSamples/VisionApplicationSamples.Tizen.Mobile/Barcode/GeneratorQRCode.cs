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
using Tizen.Multimedia.Vision;
using Xamarin.Forms;
using VisionApplicationSamples.Barcode;
using VisionApplicationSamples.Tizen.Mobile.Barcode;
using TizenApp = Tizen.Applications.Application;

[assembly: Dependency(typeof(GeneratorQRCode))]
namespace VisionApplicationSamples.Tizen.Mobile.Barcode
{
    class GeneratorQRCode : IGeneratorQRCode
    {
        private string _filePath = Path.Combine(TizenApp.Current.DirectoryInfo.Data, "barcode");
        private readonly VisionApplicationSamples.Barcode.BarcodeType _barcodeType = VisionApplicationSamples.Barcode.BarcodeType.QR;
        private QrModeType _qrModelType;
        private VisionApplicationSamples.Barcode.ErrorCorrectionLevel _eccLevel;
        private int _version;

        public GeneratorQRCode()
        {
            _qrModelType = QrModeType.Utf8;
            _eccLevel = VisionApplicationSamples.Barcode.ErrorCorrectionLevel.Medium;
        }

        public void SetQrModeType(QrModeType qrModeType)
        {
            _qrModelType = qrModeType;
        }

        public void SetErrorCorrectionLevel(VisionApplicationSamples.Barcode.ErrorCorrectionLevel eccLevel)
        {
            _eccLevel = eccLevel;
        }

        public void SetVersion(int version)
        {
            _version = version;
        }

        public string Generate(int width, int height, string message)
        {
            string targetFilePath = _filePath + "_" + _barcodeType.ToString();
            BarcodeImageConfiguration imageConfiguration = new BarcodeImageConfiguration(width, height, targetFilePath, BarcodeImageFormat.Jpeg);
            QrConfiguration qrConfiguration = new QrConfiguration(ConvertQrMode(_qrModelType), ConvertEccLevel(_eccLevel), _version);
            BarcodeGenerator.GenerateImage(message, qrConfiguration, imageConfiguration);

            return targetFilePath + ".jpg";
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

        private QrMode ConvertQrMode(QrModeType qrModeType)
        {
            QrMode convertedQrMode;
            switch (qrModeType)
            {
                case QrModeType.Numeric:
                    convertedQrMode = QrMode.Numeric;
                    break;
                case QrModeType.AlphaNumeric:
                    convertedQrMode = QrMode.AlphaNumeric;
                    break;
                case QrModeType.Byte:
                    convertedQrMode = QrMode.Byte;
                    break;
                default:
                    convertedQrMode = QrMode.Utf8;
                    break;
            }
            return convertedQrMode;
        }

        private global::Tizen.Multimedia.Vision.ErrorCorrectionLevel ConvertEccLevel(VisionApplicationSamples.Barcode.ErrorCorrectionLevel eccLevel)
        {
            global::Tizen.Multimedia.Vision.ErrorCorrectionLevel convertedEccLevel;
            switch (eccLevel)
            {
                case VisionApplicationSamples.Barcode.ErrorCorrectionLevel.Low:
                    convertedEccLevel = global::Tizen.Multimedia.Vision.ErrorCorrectionLevel.Low;
                    break;
                case VisionApplicationSamples.Barcode.ErrorCorrectionLevel.Medium:
                    convertedEccLevel = global::Tizen.Multimedia.Vision.ErrorCorrectionLevel.Medium;
                    break;
                case VisionApplicationSamples.Barcode.ErrorCorrectionLevel.Quartile:
                    convertedEccLevel = global::Tizen.Multimedia.Vision.ErrorCorrectionLevel.Quartile;
                    break;
                default:
                    convertedEccLevel = global::Tizen.Multimedia.Vision.ErrorCorrectionLevel.High;
                    break;
            }
            return convertedEccLevel;
        }
    }
}
