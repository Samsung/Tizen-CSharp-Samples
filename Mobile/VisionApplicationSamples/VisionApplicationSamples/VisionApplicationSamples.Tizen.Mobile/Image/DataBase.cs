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

using VisionApplicationSamples.Tizen.Mobile.Image;
using VisionApplicationSamples.Image;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tizen.Content.MediaContent;
using Xamarin.Forms;
using TizenApp = Tizen.Applications.Application;

[assembly: Dependency(typeof(DataBase))]
namespace VisionApplicationSamples.Tizen.Mobile.Image
{
    class DataBase : IDataBase
    {
        private readonly MediaInfoCommand _mediaInfoCmd;
        private string _filePath = TizenApp.Current.DirectoryInfo.Resource + "/image";
        private List<string> _targetFiles;
        private List<string> _sceneFiles;

        public DataBase()
        {
            var database = new MediaDatabase();
            database.Connect();

            _mediaInfoCmd = new MediaInfoCommand(database);
            _targetFiles = Directory.GetFiles(_filePath + "/target", "*.jpg").Select(Path.GetFullPath).ToList();
            _sceneFiles = Directory.GetFiles(_filePath + "/scene", "*.jpg").Select(Path.GetFullPath).ToList();
        }

        /// <summary>
        /// Selects a given type's media.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>A MediaDataReader</returns>
        private MediaDataReader<MediaInfo> Select(string filter)
        {
            return _mediaInfoCmd.SelectMedia(new SelectArguments()
            {
                FilterExpression = filter
            });
        }

        /// <summary>
        /// Select a scene image.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> SelectSceneImages()
        {
            using (var reader = Select($"{MediaInfoColumns.MediaType}={(int)MediaType.Image}"))
            {
                while (reader.Read())
                {
                    _sceneFiles.Add(reader.Current.Path);
                }
            }
            return _sceneFiles;
        }

        /// <summary>
        /// Select a target image.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> SelectTargetImages()
        {
            using (var reader = Select($"{MediaInfoColumns.MediaType}={(int)MediaType.Image}"))
            {
                while (reader.Read())
                {
                    _targetFiles.Add(reader.Current.Path);
                }
            }
            return _targetFiles;
        }
    }
}
