/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.ComponentModel;
using Tizen.NUI;

namespace ApplicationControl.Models
{
    /// <summary>
    /// A class for an application list item
    /// </summary>
    public class ApplicationListItem : INotifyPropertyChanged
    {
        Color _blendColor;

        /// <summary>
        /// The application ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The icon path
        /// </summary>
        public string IconPath { get; set; }

        /// <summary>
        /// The color be blended with an image
        /// </summary>
        public Color BlendColor
        {
            get
            {
                return _blendColor;
            }

            set
            {
                if (_blendColor == value)
                {
                    return;
                }

                _blendColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BlendColor"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
