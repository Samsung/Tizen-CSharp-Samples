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

using UIComponents.Extensions;
using UIComponents.Tizen.Wearable.Renderers;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(MultilineCell), typeof(MultilineCellRenderer))]
namespace UIComponents.Tizen.Wearable.Renderers
{
    public class MultilineCellRenderer : CellRenderer
    {
        /// <summary>
        /// Constructor of MultilineCellRenderer class with style parameter
        /// </summary>
        /// <param name="style">Style of CellRenderer</param>
        protected MultilineCellRenderer(string style) : base(style)
        {
        }

        /// <summary>
        /// Default constructor of MultilineCellRenderer class
        /// </summary>
        public MultilineCellRenderer() : this("multiline")
        {
            MainPart = "elm.text";
        }

        /// <summary>
        /// Part name of theme
        /// </summary>
        protected string MainPart { get; set; }

        /// <summary>
        /// Getter for text in cell
        /// </summary>
        /// <param name="cell">Xamarin.Forms.Cell</param>
        /// <param name="part">part name of theme</param>
        /// <returns>Span</returns>
        protected override Span OnGetText(Cell cell, string part)
        {
            if (part == MainPart)
            {
                return new Span()
                {
                    Text = (cell as MultilineCell).Text
                };
            }

            return null;
        }
    }
}
