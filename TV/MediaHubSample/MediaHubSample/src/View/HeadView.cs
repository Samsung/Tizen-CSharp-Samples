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

using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;
using System;

namespace Tizen.NUI.MediaHub
{
    /// <summary>
    /// The Head of the Media hub sample.
    /// </summary>
    class HeadView : View
    {
        private View titleView;
        private View parent = null;
        private TextLabel guide;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parent">The parent view of the head</param>
        public HeadView(View parent = null)
        {
            this.parent = parent;           
        }

        /// <summary>
        /// Create the head view
        /// </summary>
        public void RenderView()
        {
            //Add view title
            this.titleView = new View();
            this.titleView.Size2D = new Size2D(1920, 120);
            this.titleView.Position = new Position(0, 0, 0);
            this.titleView.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            this.titleView.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;

            this.parent.Add(titleView);

            guide = new TextLabel();
            guide.HorizontalAlignment = HorizontalAlignment.Center;
            guide.VerticalAlignment = VerticalAlignment.Center;
            //guide.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            guide.TextColor = Color.White;
            guide.PositionUsesPivotPoint = true;
            guide.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            guide.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            guide.Size2D = new Size2D(1920, 96);
            guide.FontFamily = "Samsung One 600";
            guide.Position2D = new Position2D(0, 94);
            guide.MultiLine = false;
            //guide.PointSize = 15.0f;
            guide.PointSize = DeviceCheck.PointSize15;
            guide.Text = "Media Hub Sample";
            this.titleView.Add(guide);
        }

        /// <summary>
        /// Set the text of the head
        /// </summary>
        public void Update()
        {
            guide.Text = CommonResource.DeviceName;
        }

    }
}
