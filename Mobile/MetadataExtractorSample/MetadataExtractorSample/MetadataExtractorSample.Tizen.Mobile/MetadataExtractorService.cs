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

using MetadataExtractorSample.Tizen.Mobile;
using System;
using System.Collections.Generic;
using System.Linq;
using Tizen.Multimedia;
using Xamarin.Forms;

[assembly: Dependency(typeof(MetadataExtractorService))]
namespace MetadataExtractorSample.Tizen.Mobile
{
    class MetadataExtractorService : IMetadataExtractor
    {
        private string _path;

        public string Path
        {
            get => _path;
            set
            {
                using (var extractor = new MetadataExtractor(value))
                {
                    MetadataExtracted?.Invoke(this,
                        new MetadataExtractedEventArgs(CreateItems(extractor.GetMetadata())));
                }

                _path = value;
            }
        }

        private IEnumerable<Item> CreateItems(Metadata metadata)
        {
            return CreateItemsFromProperties(metadata, "Video", "Audio").
                Concat(CreateItemsFromProperties(metadata.Video)).
                Concat(CreateItemsFromProperties(metadata.Audio));
        }

        private IEnumerable<Item> CreateItemsFromProperties(object obj, params string[] exclude)
        {
            if (obj != null)
            {
                foreach (var prop in obj.GetType().GetProperties())
                {
                    if (exclude.Contains(prop.Name) == true)
                    {
                        continue;
                    }

                    var value = prop.GetValue(obj);
                    if (value != null)
                    {
                        yield return new Item(prop.Name, value.ToString());
                    }
                }
            }
        }

        public event EventHandler<MetadataExtractedEventArgs> MetadataExtracted;
    }
}
