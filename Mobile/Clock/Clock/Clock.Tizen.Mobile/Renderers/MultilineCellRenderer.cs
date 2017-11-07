/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

using Clock.Controls;
using Clock.Tizen.Mobile.Renderers;
using ElmSharp;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using NImage = Xamarin.Forms.Platform.Tizen.Native.Image;

[assembly: ExportCell(typeof(MultilineCell), typeof(MultilineCellRenderer))]

namespace Clock.Tizen.Mobile.Renderers
{
    /// <summary>
    /// The renderer for multiline cell
    /// </summary>
    public class MultilineCellRenderer : CellRenderer
    {
        string _iconPart = "elm.swallow.icon";
        string _endPart = "elm.swallow.end";

        public MultilineCellRenderer() : base("multiline")
        {
        }

        /// <summary>
        /// Return Cell's text part
        /// </summary>
        /// <param name="cell">Cell</param>
        /// <param name="part">text part's style name</param>
        /// <returns>Span</returns>
        protected override Span OnGetText(Cell cell, string part)
        {
            MultilineCell multilineCell = (MultilineCell)cell;
            if (part == "elm.text")
            {
                return new Span()
                {
                    Text = multilineCell.Text,
                    FontSize = -1
                };
            }
            else if (part == "elm.text.multiline")
            {
                return new Span()
                {
                    Text = multilineCell.Multiline,
                    FontSize = -1
                };
            }

            return null;
        }

        /// <summary>
        /// Returns EvasObject which constitutes the cell 
        /// </summary>
        /// <param name="cell">Cell</param>
        /// <param name="part">Cell's style name</param>
        /// <returns>EvasObject</returns>
        protected override EvasObject OnGetContent(Cell cell, string part)
        {
            MultilineCell multilineCell = (MultilineCell)cell;
            if (part == _iconPart && multilineCell.Icon != null)
            {
                var image = new NImage(Forms.Context.MainWindow);
                SetUpImage(multilineCell, image);
                return image;
            }
            else if (part == _endPart && multilineCell.IsCheckVisible)
            {
                var check = new Check(Forms.Context.MainWindow)
                {
                    Style = "toggle"
                };
                check.SetAlignment(-1, -1);
                check.SetWeight(1, 1);
                check.IsChecked = multilineCell.IsChecked;
                check.StateChanged += (sender, e) =>
                {
                    multilineCell.IsChecked = e.NewState;
                };
                return check;
            }

            return null;
        }

        protected override bool OnCellPropertyChanged(Cell cell, string property, Dictionary<string, EvasObject> realizedView)
        {
            MultilineCell multilineCell = (MultilineCell)cell;
            if (property == MultilineCell.IconProperty.PropertyName)
            {
                if (!realizedView.ContainsKey(_iconPart) || multilineCell.Icon == null)
                {
                    return true;
                }

                var image = realizedView[_iconPart] as NImage;
                SetUpImage(multilineCell, image);
                return false;
            }
            else if (property == MultilineCell.IsCheckedProperty.PropertyName && realizedView.ContainsKey(_endPart))
            {
                var check = realizedView[_endPart] as Check;
                check.IsChecked = multilineCell.IsChecked;
                return false;
            }
            else if (property == MultilineCell.IsCheckVisibleProperty.PropertyName ||
                property == MultilineCell.TextProperty.PropertyName ||
                property == MultilineCell.MultilineProperty.PropertyName)
            {
                return true;
            }

            return base.OnCellPropertyChanged(cell, property, realizedView);
        }

        void SetUpImage(MultilineCell cell, NImage image)
        {
            if (cell.IconHeight > 0 && cell.IconWidth > 0)
            {
                image.MinimumWidth = cell.IconWidth;
                image.MinimumHeight = cell.IconHeight;
            }
            else
            {
                image.LoadingCompleted += (sender, e) =>
                {
                    image.MinimumWidth = image.ObjectSize.Width;
                    image.MinimumHeight = image.ObjectSize.Height;
                };
            }

            image.LoadFromImageSourceAsync(cell.Icon);
        }
    }
}
