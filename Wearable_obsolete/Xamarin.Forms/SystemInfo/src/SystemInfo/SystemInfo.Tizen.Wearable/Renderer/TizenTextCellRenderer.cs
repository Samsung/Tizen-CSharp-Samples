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
using SystemInfo.Tizen.Wearable.Control;
using SystemInfo.Tizen.Wearable.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(TizenTextCell), typeof(TizenTextCellRenderer))]
namespace SystemInfo.Tizen.Wearable.Renderer
{
    /// <summary>
    /// TizenTextCellRenderer renderer class for the TizenTextCell control.
    /// </summary>
    public class TizenTextCellRenderer : CellRenderer
    {
        #region fields

        /// <summary>
        /// Part name for main text.
        /// </summary>
        private const string ELM_TEXT = "elm.text";

        /// <summary>
        /// Part name for sub text.
        /// </summary>
        private const string ELM_TEXT_SUB = "elm.text.1";

        #endregion

        #region methods

        /// <summary>
        /// TizenTextCellRenderer class constructor.
        /// </summary>
        /// <param name="style">Style name.</param>
        protected TizenTextCellRenderer(string style) : base(style)
        {
        }

        /// <summary>
        /// TizenTextCellRenderer class constructor.
        /// </summary>
        public TizenTextCellRenderer() : this("2text")
        {
        }

        /// <summary>
        /// Overrides OnGetText method for setting text contexts to TizenTextCellRenderer.
        /// </summary>
        /// <param name="cell">Cell object.</param>
        /// <param name="part">Part name.</param>
        /// <returns>Span element containing cell text.</returns>
        protected override Span OnGetText(Cell cell, string part)
        {
            TizenTextCell tizenTextCell = (cell as TizenTextCell);
            string spanText = "";

            switch (part)
            {
                case ELM_TEXT:
                    spanText = tizenTextCell.Text ?? spanText;
                    break;
                case ELM_TEXT_SUB:
                    spanText = tizenTextCell.Detail ?? spanText;
                    break;
            }

            if (spanText != "")
            {
                return new Span()
                {
                    Text = spanText,
                    FontSize = -1
                };
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
