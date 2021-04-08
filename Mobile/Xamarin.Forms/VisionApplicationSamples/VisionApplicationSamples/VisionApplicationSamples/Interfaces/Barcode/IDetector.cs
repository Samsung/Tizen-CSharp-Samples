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

using System.Collections.Generic;
using System.Threading.Tasks;

namespace VisionApplicationSamples.Barcode
{
    /// <summary>
    /// Interface for Detector.
    /// </summary>
    public interface IDetector
    {
        /// <summary>
        /// Sets ImagePath.
        /// </summary>
        string ImagePath { set; }

        /// <summary>
        /// Gets the number of detected Barcodes.
        /// </summary>
        int NumberOfBarcodes { get; }

        /// <summary>
        /// Gets regions of detected faces.
        /// </summary>
        List<string> Messages { get; }

        /// <summary>
        /// Detects Barcode from a path.
        /// </summary>
        /// <returns>A message</returns>
        Task<string> Detect();

        /// <summary>
        /// Decode an image at a path.
        /// </summary>
        Task Decode();

    }
}
