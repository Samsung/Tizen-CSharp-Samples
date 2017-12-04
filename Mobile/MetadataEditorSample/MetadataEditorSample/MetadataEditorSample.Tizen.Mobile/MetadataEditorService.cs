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

using MetadataEditorSample.Tizen.Mobile;
using System;
using System.Collections.Generic;
using System.Linq;
using Tizen.Multimedia;
using Xamarin.Forms;

[assembly: Dependency(typeof(MetadataEditorService))]
namespace MetadataEditorSample.Tizen.Mobile
{
    /// <summary>
    /// Implementation of IMetadataEditor.
    /// </summary>
    class MetadataEditorService : IMetadataEditor
    {
        private MetadataEditor _editor;

        public event EventHandler Initialized;

        public IEnumerable<Item> Items { get; private set; }

        public void Initialize(string path)
        {
            _editor = new MetadataEditor(path);

            Items = CreateItems().ToList();

            Initialized?.Invoke(this, EventArgs.Empty);
        }

        private IEnumerable<Item> CreateItems()
        {
            yield return new Item("Album", _editor.Album);
            yield return new Item("Artist", _editor.Artist);
            yield return new Item("Author", _editor.Author);
            yield return new Item("Comment", _editor.Comment);
            yield return new Item("Conductor", _editor.Conductor);
            yield return new Item("Copyright", _editor.Copyright);
            yield return new Item("Date", _editor.Date);
            yield return new Item("Description", _editor.Description);
            yield return new Item("Genre", _editor.Genre);
            yield return new Item("Title", _editor.Title);
            yield return new Item("TrackNumber", _editor.TrackNumber);
            yield return new Item("UnsyncLyrics", _editor.UnsyncLyrics);
        }

        public void Save()
        {
            if (_editor == null)
            {
                return;
            }

            bool hasModified = false;

            foreach (var item in Items)
            {
                if (item.IsModified == false)
                {
                    continue;
                }

                typeof(MetadataEditor).GetProperty(item.Name).SetValue(_editor, item.Value);

                hasModified = true;
            }

            if (hasModified)
            {
                _editor.Commit();
            }
        }
    }
}
