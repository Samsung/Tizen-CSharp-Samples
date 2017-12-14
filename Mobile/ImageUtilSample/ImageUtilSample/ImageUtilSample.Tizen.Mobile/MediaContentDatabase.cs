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

using ImageUtilSample.Tizen.Mobile;
using System.Collections.Generic;
using Tizen.Content.MediaContent;
using Xamarin.Forms;

[assembly: Dependency(typeof(MediaContentDatabase))]
namespace ImageUtilSample.Tizen.Mobile
{
    class MediaContentDatabase : IMediaContentDatabase
    {
        private readonly MediaInfoCommand _mediaInfoCmd;

        public MediaContentDatabase()
        {
            var database = new MediaDatabase();
            database.Connect();

            _mediaInfoCmd = new MediaInfoCommand(database);
        }

        private MediaDataReader<MediaInfo> Select(string filter)
        {
            return _mediaInfoCmd.SelectMedia(new SelectArguments()
            {
                FilterExpression = filter
            });
        }

        public IEnumerable<string> SelectImages()
        {
            using (var reader = Select($"{MediaInfoColumns.MediaType}={(int)MediaType.Image}"))
            {
                while (reader.Read())
                {
                    yield return reader.Current.Path;
                }
            }
        }
    }
}
