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

using System;
using Tizen;
using System.Runtime.InteropServices;
using Tizen.NUI;
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

namespace UIControlSample
{
    /// <summary>
    /// The ActivityIndicator component is a view gives a visual clue to the user
    /// that something is happening, without information about its progress.
    /// </summary>
    class ActivityIndicatorSample : IExample
    {
        private View activityIndicator;
        private TextLabel guide;
        /// <summary>
        /// The constructor
        /// </summary>
        public ActivityIndicatorSample()
        {
        }

        /// <summary>
        /// Text Sample Application initialisation.
        /// </summary>
        private void Initialize()
        {
            Window.Instance.BackgroundColor = Color.Black;

            guide = new TextLabel();
            guide.HorizontalAlignment = HorizontalAlignment.Center;
            guide.VerticalAlignment = VerticalAlignment.Center;
            guide.PositionUsesPivotPoint = true;
            guide.ParentOrigin = ParentOrigin.TopLeft;
            guide.PivotPoint = PivotPoint.TopLeft;
            guide.Size2D = new Size2D(1920, 96);
            guide.FontFamily = "Samsung One 600";
            guide.Position2D = new Position2D(0, 94);
            guide.MultiLine = false;
            //guide.PointSize = 15.0f;
            guide.PointSize = DeviceCheck.PointSize15;
            guide.Text = "ActivityIndicator Sample \n";
            guide.TextColor = Color.White;
            //guide.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            Window.Instance.GetDefaultLayer().Add(guide);

            activityIndicator = new ActivityIndicator();
            activityIndicator.Position = new Position(896, 476, 0);
            Window.Instance.GetDefaultLayer().Add(activityIndicator);
        }

        /// <summary>
        /// Dispose activityIndicator.
        /// </summary>
        public void Deactivate()
        {
            Window.Instance.GetDefaultLayer().Remove(guide);
            guide.Dispose();
            guide = null;

            Window.Instance.GetDefaultLayer().Remove(activityIndicator);
            activityIndicator.Dispose();
            activityIndicator = null;
        }

        /// <summary>
        /// Active activityIndicator.
        /// </summary>
        public void Activate()
        {
            Initialize();
        }
    }
}