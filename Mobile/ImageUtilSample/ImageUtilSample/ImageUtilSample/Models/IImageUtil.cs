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

namespace ImageUtilSample
{
    /// <summary>
    /// Interface for image manipulation.
    /// </summary>
    public interface IImageUtil
    {
        /// <summary>
        /// Gets or sets the image path to transform.
        /// </summary>
        string ImagePath { get; set; }

        /// <summary>
        /// Gets the image width.
        /// </summary>
        int ImageWidth { get; }

        /// <summary>
        /// Gets the image height.
        /// </summary>
        int ImageHeight { get; }

        /// <summary>
        /// Gets the path for transformed image.
        /// </summary>
        string ResultPath { get; }

        /// <summary>
        /// Decodes the image to raw format.
        /// </summary>
        /// <remarks>
        /// It should be done first.
        /// </remarks>
        /// <returns>A task that represents the asynchronous decode operation.</returns>
        Task Decode();

        /// <summary>
        /// Rotates the image.
        /// </summary>
        /// <param name="rotate">Rotation value.</param>
        /// <returns>A task that represents the asynchronous rotate operation.</returns>
        Task Rotate(TransformRotation rotate);

        /// <summary>
        /// Flips the image.
        /// </summary>
        /// <param name="flip">Flip value.</param>
        /// <returns>A task that represents the asynchronous flip operation.</returns>
        Task Flip(TransformFlip flip);

        /// <summary>
        /// Changes the color-space of image.
        /// </summary>
        /// <param name="colorSpace">Color-space value.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task ChangeColorSpace(TransformColorSpace colorSpace);

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="width">Width value.</param>
        /// <param name="height">Height value.</param>
        /// <returns>A task that represents the asynchronous resize operation.</returns>
        Task Resize(int width, int height);

        /// <summary>
        /// Crops the image.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="top">Top value.</param>
        /// <param name="width">Width value.</param>
        /// <param name="height">Height value.</param>
        /// <returns>A task that represents the asynchronous crop operation.</returns>
        Task Crop(int left, int top, int width, int height);
    }
}
