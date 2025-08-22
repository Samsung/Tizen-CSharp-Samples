﻿/*
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

namespace VisionApplicationSamples.Barcode
{
    /// <summary>
    /// Interface for OneBarcodeGenerator
    /// </summary>
    public interface IGeneratorBarcode
    {
        /// <summary>
        /// Sets BarcodeType <see cref="BarcodeType"/>
        /// </summary>
        /// <param name="barcodeType">A BarcodeType</param>
        void SetBarcodeType(BarcodeType barcodeType);

        /// <summary>
        /// Generates Barcode.
        /// </summary>
        /// <param name="width">A width.</param>
        /// <param name="height">A height.</param>
        /// <param name="message">A message.</param>
        /// <returns></returns>
        string Generate(int width, int height, string message);
    }
}
