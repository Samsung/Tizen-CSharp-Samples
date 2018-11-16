/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
 */
using QRCodeGenerator.Tizen.Wearable.Services;
using QRCodeGenerator.Services;
using Xamarin.Forms;
using Tizen.Multimedia.Vision;
using TizenApp = Tizen.Applications;
using Tizen.Multimedia;

[assembly: Dependency(typeof(QRService))]

namespace QRCodeGenerator.Tizen.Wearable.Services
{
    /// <summary>
    /// Application QR service class.
    /// </summary>
    class QRService : IQRService
    {
        #region methods

        /// <summary>
        /// Generates QR code image with Tizen API.
        /// </summary>
        /// <param name="textToEncode">String value that will be encoded intro QR code.</param>
        /// <returns>Path to QR code image.</returns>
        public string Generate(string textToEncode)
        {
            string path = TizenApp.Application.Current.DirectoryInfo.SharedTrusted + "/QRCode";

            QrConfiguration qrConfig = new QrConfiguration(QrMode.Utf8, ErrorCorrectionLevel.High, 6);
            BarcodeGenerationConfiguration barConfig = new BarcodeGenerationConfiguration
            {
                TextVisibility = Visibility.Invisible
            };

            BarcodeImageConfiguration imageConfig = new BarcodeImageConfiguration
                (192, 192, path, BarcodeImageFormat.Jpeg);

            BarcodeGenerator.GenerateImage(textToEncode, qrConfig, imageConfig, barConfig);

            return path + ".jpg";
        }

        #endregion
    }
}