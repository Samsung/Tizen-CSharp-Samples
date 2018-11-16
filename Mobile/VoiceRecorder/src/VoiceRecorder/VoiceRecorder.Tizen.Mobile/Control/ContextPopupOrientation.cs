/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;

namespace VoiceRecorder.Tizen.Mobile.Control
{
    /// <summary>
    /// Enumeration for the orientation of a ContextPopup.
    /// </summary>
    [Obsolete("ContextPopupOrientation is obsolete as of version 2.3.5-r256-001. ContextPopup does not support orientation. The orientation is always vertical.")]
    public enum ContextPopupOrientation
    {
        /// <summary>
        /// The horizontal ContextPopup direction.
        /// </summary>
        Horizontal,

        /// <summary>
        /// The vertical ContextPopup direction.
        /// </summary>
        Vertical,
    }
}