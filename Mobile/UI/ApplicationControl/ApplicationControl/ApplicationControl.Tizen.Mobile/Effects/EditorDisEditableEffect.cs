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

using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using NEntry = Xamarin.Forms.Platform.Tizen.Native.Entry;
using ApplicationControl.Extensions;
using ApplicationControl.Tizen.Mobile.Effects;

[assembly: ExportEffect(typeof(EditorDisEditableEffect), "EditorDisEditableEffect")]

namespace ApplicationControl.Tizen.Mobile.Effects
{
    /// <summary>
    /// A class to enable to make an ediator diseditable
    /// </summary>
    public class EditorDisEditableEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var entry = (NEntry)Control;
            entry.IsEditable = ((DisEditableEditor)Element).IsEditable;
        }

        protected override void OnDetached()
        {
            var entry = (NEntry)Control;
            entry.IsEditable = true;
        }
    }
}