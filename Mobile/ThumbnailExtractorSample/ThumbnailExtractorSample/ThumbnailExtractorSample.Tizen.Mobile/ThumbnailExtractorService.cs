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
using System.IO;
using System.Threading.Tasks;
using ThumbnailExtractorSample.Tizen.Mobile;
using Tizen.Multimedia;
using Tizen.Multimedia.Util;
using Xamarin.Forms;
using TizenApp = Tizen.Applications.Application;

[assembly: Dependency(typeof(ThumbnailExtractorService))]
namespace ThumbnailExtractorSample.Tizen.Mobile
{
    /// <summary>
    /// Implementation of IThumbnailExtractor.
    /// </summary>
    class ThumbnailExtractorService : IThumbnailExtractor
    {
        // Source is embedded image file.
        public string ImagePath => Path.Combine(TizenApp.Current.DirectoryInfo.Resource, "image.png");

        private string ThumbnailPath = Path.Combine(TizenApp.Current.DirectoryInfo.Data, "thumbnail");

        private async Task<string> EncodeAsync(Func<Task<ThumbnailExtractionResult>> extractFunc)
        {
            var result = await extractFunc();

            File.Delete(ThumbnailPath);

            using (var encoder = new JpegEncoder())
            using (var fs = File.Create(ThumbnailPath))
            {
                encoder.SetColorSpace(ColorSpace.Bgra8888);
                encoder.SetResolution(result.Size);

                await encoder.EncodeAsync(result.RawData, fs);

                return ThumbnailPath;
            }
        }

        public Task<string> ExtractAsync()
        {
            return EncodeAsync(() => ThumbnailExtractor.ExtractAsync(ImagePath));
        }

        public Task<string> ExtractAsync(int width, int height)
        {
            return EncodeAsync(() => ThumbnailExtractor.ExtractAsync(ImagePath,
                new global::Tizen.Multimedia.Size(width, height)));
        }
    }
}
