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
using Xamarin.Forms;

namespace VisionApplicationSamples.Image
{
    public interface IRecognizer
    {
        /// <summary>
        /// Sets TargetImagePath.
        /// </summary>
        string TargetImagePath { set; }

        /// <summary>
        /// Sets SceneImagePath.
        /// </summary>
        string SceneImagePath { set; }

        /// <summary>
        /// Fill target with a given target image.
        /// </summary>
        bool FillTarget();

        /// <summary>
        /// Get value indicating whether a target is filled or not.
        /// </summary>
        bool IsTargetFilled { get; }

        /// <summary>
        /// Gets points of recognized targets.
        /// </summary>
        List<Point> RecognizedTarget { get; }

        /// <summary>
        /// The result of recognition
        /// </summary>
        bool Success { get;  }

        /// <summary>
        /// Recognizes the target image on the scene image.
        /// </summary>
        Task<string> Recognize();

        /// <summary>
        /// Decode an image at a path
        /// </summary>
        Task Decode();
    }
}
