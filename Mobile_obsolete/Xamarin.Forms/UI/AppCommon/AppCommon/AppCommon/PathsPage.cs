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

using System.Diagnostics;

namespace AppCommon
{
    /// <summary>
    /// A class to define each path information
    /// </summary>
    public class PathInformation
    {
        public string Title { get; set; }

        public string Path { get; set; }
    }

    /// <summary>
    /// A class for PathsPage
    /// </summary>
    public partial class PathsPage
    {
        bool _contentLoaded;

        double _horizontalScale;

        /// <summary>
        /// A constructor for PathsPage
        /// </summary>
        /// <param name="screenWidth">Screen width</param>
        /// <param name="screenHeight">Screen height</param>
        public PathsPage(int screenWidth, int screenHeight)
        {
            if (_contentLoaded)
            {
                return;
            }

            _contentLoaded = true;
            _horizontalScale = (double)screenWidth / 720.0;

            InitializeComponent();
        }
    }
}