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
using Tizen.NUI;

namespace ImageSample
{

    /// <summary>
    /// To check the emul is a SR TV emul or VD TV emul.
    /// The DPI of the SR TV emul is (72, 72).
    /// The DPI of the VD TV emul is (314, 314).
    /// </summary>
    public class DeviceCheck
    {
        /// <summary>
        /// Whether it is a SR emul
        /// </summary>
        /// <returns>If it is a SR emul, then return true</returns>
        public static bool IsSREmul()
        {
            int widthDpi = (int)Window.Instance.Dpi.Width;
            int heightDpi = (int)Window.Instance.Dpi.Height;
            if(widthDpi == 314 && heightDpi == 314)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Return the point size of 6
        /// </summary>
        public static float PointSize6
        {
            get
            {
                if (IsSREmul())
                {
                    return 6.0f;
                }
                else
                {
                    return 26.0f;
                }
            }
        }

        /// <summary>
        /// Return the point size of 7
        /// </summary>
        public static float PointSize7
        {
            get
            {
                if (IsSREmul())
                {
                    return 7.0f;
                }
                else
                {
                    return 30.0f;
                }
            }
        }

        /// <summary>
        /// Return the point size of 8
        /// </summary>
        public static float PointSize8
        {
            get
            {
                if(IsSREmul())
                {
                    return 8.0f;
                }
                else
                {
                    return 34.0f;
                }
            }
        }

        /// <summary>
        /// Return the point size of 10
        /// </summary>
        public static float PointSize10
        {
            get
            {
                if (IsSREmul())
                {
                    return 10.0f;
                }
                else
                {
                    return 43.0f;
                }
            }
        }

        /// <summary>
        /// Return the point size of 15
        /// </summary>
        public static float PointSize15
        {
            get
            {
                if (IsSREmul())
                {
                    return 15.0f;
                }
                else
                {
                    return 65.0f;
                }
            }
        }
    }
}