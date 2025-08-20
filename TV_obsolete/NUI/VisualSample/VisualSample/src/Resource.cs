/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI.BaseComponents;

namespace VisualSample
{
    /// <summary>
    /// A interface of all image samples.
    /// </summary>
    public class Resource
    {
        private static VisualView visualView = null;

        public static VisualView VisualVisualInstance
        {
            get
            {
                return visualView;
            }

            set
            {
                visualView = value;
            }
        }
    }
}