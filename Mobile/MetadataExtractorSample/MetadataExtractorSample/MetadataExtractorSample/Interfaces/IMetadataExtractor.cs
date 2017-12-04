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

using System;
using System.Collections.Generic;

namespace MetadataExtractorSample
{
    /// <summary>
    /// Provides data for the <see cref="IMetadataExtractor.MetadataExtracted"/> event.
    /// </summary>
    public class MetadataExtractedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of MetadataExtractedEventArgs class with the specified items.
        /// </summary>
        /// <param name="items">The metadata items.</param>
        public MetadataExtractedEventArgs(IEnumerable<Item> items)
        {
            Items = items;
        }

        /// <summary>
        /// Gets the metadata items.
        /// </summary>
        public IEnumerable<Item> Items { get; }
    }

    /// <summary>
    /// Interface for metadata extactor.
    /// </summary>
    public interface IMetadataExtractor
    {
        /// <summary>
        /// Occurs when the metadata is extracted.
        /// </summary>
        event EventHandler<MetadataExtractedEventArgs> MetadataExtracted;

        /// <summary>
        /// Gets or sets the file path to extract metadata.
        /// </summary>
        string Path { get; set; }
    }
}
