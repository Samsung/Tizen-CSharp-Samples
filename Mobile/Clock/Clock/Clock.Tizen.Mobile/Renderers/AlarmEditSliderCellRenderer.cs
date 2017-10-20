/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Clock.Alarm;
using Clock.Tizen.Mobile.Renderers;
using ElmSharp;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportCell(typeof(AlarmEditSliderCell), typeof(AlarmEditSliderCellRenderer))]

namespace Clock.Tizen.Mobile.Renderers
{
    /// <summary>
    /// The renderer for AlarmEditSliderCell
    /// It extends CellRenderer class but refers to the implementation of ViewCellRenderer
    /// </summary>
    public class AlarmEditSliderCellRenderer : CellRenderer
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        public AlarmEditSliderCellRenderer() : base("full")
        {
            MainContentPart = "elm.swallow.content";
        }

        protected string MainContentPart { get; set; }

        /// <summary>
        /// Return EvasObject which consists of a cell
        /// </summary>
        /// <param name="cell">Cell</param>
        /// <param name="part">Cell's style name</param>
        /// <returns>EvasObject</returns>
        protected override EvasObject OnGetContent(Cell cell, string part)
        {
            if (part == MainContentPart)
            {
                var viewCell = (AlarmEditSliderCell)cell;
                var listView = viewCell?.RealParent as TableView;

                Platform.GetRenderer(viewCell.View)?.Dispose();
                var renderer = Platform.GetOrCreateRenderer(viewCell.View);
                double height = viewCell.RenderHeight;
                height = height > 0 ? height : FindCellContentHeight(viewCell);

                renderer.NativeView.MinimumHeight = ConvertToScaledPixel(height);
                (renderer as LayoutRenderer)?.RegisterOnLayoutUpdated();

                return renderer.NativeView;
            }

            return null;
        }

        protected double FindCellContentHeight(AlarmEditSliderCell cell)
        {
            AlarmEditSliderCell viewCell = cell as AlarmEditSliderCell;
            if (viewCell != null)
            {
                var parentWidth = (cell.Parent as VisualElement).Width;
                var view = viewCell.View;
                return view.Measure(parentWidth, double.PositiveInfinity).Request.Height;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// The conversion of dp units to screen pixels
        /// </summary>
        /// <param name="dp">dp(density independent pixel)</param>
        /// <returns>pixels</returns>
        static int ConvertToScaledPixel(double dp)
        {
            return (int)Math.Round(dp * Device.Info.ScalingFactor);
        }

        /// <summary>
        /// Invoked when cell's property has been changed
        /// </summary>
        /// <param name="cell">Cell object</param>
        /// <param name="property">Changed property name</param>
        /// <param name="realizedView">dictionary</param>
        /// <returns>bool</returns>
        protected override bool OnCellPropertyChanged(Cell cell, string property, Dictionary<string, EvasObject> realizedView)
        {
            if (property == "View")
            {
                return true;
            }

            return base.OnCellPropertyChanged(cell, property, realizedView);
        }

        /// <summary>
        /// Return the reusable EvasObject which would be shown in a cell.
        /// </summary>
        /// <param name="cell">Cell</param>
        /// <param name="part">Cell's style name</param>
        /// <param name="old">EvasObject which has been already added in Cell</param>
        /// <returns>EvasObject</returns>
        protected override EvasObject OnReusableContent(Cell cell, string part, EvasObject old)
        {
            return old;
        }
    }
}
