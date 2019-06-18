/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
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

using ElmSharp;
using IMEManager;
using IMEManager.Tizen.Mobile;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(CustomCell), typeof(CustomCellRenderer))]
namespace IMEManager.Tizen.Mobile
{
    /// <summary>
    /// The renderer for CustomCellRenderer.
    /// </summary>
    class CustomCellRenderer : ViewCellRenderer
    {
        /// <summary>
        /// Return EvasObject which consists of a cell.
        /// </summary>
        /// <param name="cell"> Cell. </param>
        /// <param name="part"> Cell's style name. </param>
        /// <returns> EvasObject </returns>
        protected override EvasObject OnGetContent(Cell cell, string part)
        {
            var native = base.OnGetContent(cell, part);
            native.PropagateEvents = true;

            return native;
        }
    }
}
