/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
 */

using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
using Preference.Tizen.TV.Controls;
using Preference.Tizen.TV.Renderers;

using NImage = Xamarin.Forms.Platform.Tizen.Native.Image;

[assembly: ExportCell(typeof(MultilineCell), typeof(MultilineCellRenderer))]

namespace Preference.Tizen.TV.Renderers
{

    /// <summary>
    /// MultilineCell Component's renderer.
    /// </summary>
    public class MultilineCellRenderer : CellRenderer
    {
        #region fields

        /// <summary>
        /// Icon swallow part name.
        /// </summary>
        string _iconPart = "elm.swallow.icon";

        /// <summary>
        /// End swallow part name.
        /// </summary>
        string _endPart = "elm.swallow.end";

        #endregion

        #region methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public MultilineCellRenderer() : base("multiline")
        {
        }

        /// <summary>
        /// Returns new span component.
        /// </summary>
        /// <param name="cell">Cell element.</param>
        /// <param name="part">Source element.</param>
        /// <returns>Span component.</returns>
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
        /// Returns contents of specified part.
        /// </summary>
        /// <param name="cell">Multiline cell.</param>
        /// <param name="part">Part.</param>
        /// <returns>Eves object in specified part.</returns>
        protected override EvasObject OnGetContent(Cell cell, string part)
        {
            MultilineCell multilineCell = (MultilineCell)cell;
            if (part == _iconPart && multilineCell.Icon != null)
            {
                var image = new NImage(Forms.NativeParent);
                SetUpImage(multilineCell, image);
                return image;
            }
            else if (part == _endPart && multilineCell.IsCheckVisible)
            {
                var check = new Check(Forms.NativeParent);
                check.SetAlignment(-1, -1);
                check.SetWeight(1, 1);
                check.IsChecked = multilineCell.IsChecked;
                check.StateChanged += (sender, e) =>
                {
                    multilineCell.IsChecked = e.NewState;
                };

                //It is a temporary way to prevent that the check of the Cell gets focus until the UX about views in the Cell for TV is defined.
                if (Device.Idiom == TargetIdiom.TV)
                {
                    check.AllowFocus(false);
                }

                return check;
            }

            return null;
        }

        /// <summary>
        /// Handles cell's properties change.
        /// Updates view properties.
        /// </summary>
        /// <param name="cell">Actual multiline cell.</param>
        /// <param name="property">Property name.</param>
        /// <param name="realizedView">Set of corresponding properties names and Evas Objects.</param>
        /// <returns>False if propertiy has been changed. True otherwise.</returns>
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

        /// <summary>
        /// Loads image.
        /// Sets images dimensions.
        /// </summary>
        /// <param name="cell">Multiline cell element.</param>
        /// <param name="image">Image element.</param>
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

        #endregion
    }
}