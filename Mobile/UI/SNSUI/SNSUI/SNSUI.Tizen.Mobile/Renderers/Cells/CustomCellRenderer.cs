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
using ElmSharp;
using EButton = ElmSharp.Button;
using SNSUI.Extensions;
using SNSUI.Tizen.Renderers.Cells;

[assembly: ExportRenderer(typeof(CustomCell), typeof(CustomCellRenderer))]

namespace SNSUI.Tizen.Renderers.Cells
{
    /// <summary>
    /// A class that renders the CustomCell.
    /// </summary>
    public class CustomCellRenderer : BaseTypeCellRenderer
    {
        /// <summary>
        /// A constructor for the class.
        /// </summary>
        public CustomCellRenderer() : base("type1")
        {
        }

        /// <summary>
        /// To override OnGetContent to add button instead of a checkbox.
        /// </summary>
        /// <param name="cell">a cell element</param>
        /// <param name="part">a part that has to add</param>
        /// <returns>an EvasObject that rendered</returns>
        protected override EvasObject OnGetContent(Cell cell, string part)
        {
            if (part == "elm.swallow.end")
            {
                var button = new EButton(Forms.NativeParent)
                {
                    Text = "+",
                    MinimumWidth = 60,
                    MinimumHeight = 80,
                };
                return button;
            }

            return base.OnGetContent(cell, part);
        }
    }
}