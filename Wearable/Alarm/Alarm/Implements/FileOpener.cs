/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


using System;
using System.IO;
using TApplication = Tizen.Applications.Application;

namespace Alarm.Implements
{
    /// <summary>
    /// The FileAccessor class
    /// </summary>
    class FileOpener
    {
        string _path;

        internal FileOpener()
        {
            _path = TApplication.Current.DirectoryInfo.Data;
        }

        public Stream OpenFile(string filePath, System.IO.FileMode fileMode)
        {
            if (filePath == null || filePath.Trim().Length == 0)
            {
                throw new ArgumentNullException("File path arguments are invalid.");
            }

            return new FileStream(Path.Combine(_path, filePath), fileMode, System.IO.FileAccess.ReadWrite, System.IO.FileShare.Read);
        }
    }
}
