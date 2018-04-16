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
    /// A sample of progressBar
    /// </summary>
    class ProgressBarSample : IExample
    {
        private ProgressBar progressBar;
        private TextLabel guide;
        private TextLabel percentage;
        // <summary>
        /// Constructor to create new ProgressBarSample
        /// </summary>
        public ProgressBarSample()
        {
        }

        /// <summary>
        /// ProgressBar initialisation.
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
            guide.Text = "ProgressBar Sample \n";
            guide.TextColor = Color.White;
            //guide.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            Window.Instance.GetDefaultLayer().Add(guide);

            Progress progressSample = new Progress();
            progressBar = progressSample.GetProgressBar();
            progressBar.Position = new Position(100, 540, 0);
            Window.Instance.GetDefaultLayer().Add(progressBar);

            percentage = new TextLabel();
            percentage.HorizontalAlignment = HorizontalAlignment.Center;
            percentage.VerticalAlignment = VerticalAlignment.Center;
            percentage.PositionUsesPivotPoint = true;
            percentage.ParentOrigin = ParentOrigin.TopLeft;
            percentage.PivotPoint = PivotPoint.TopLeft;
            percentage.Size2D = new Size2D(200, 80);
            percentage.FontFamily = "Samsung One 400";
            percentage.Position2D = new Position2D(1700, 440);
            percentage.MultiLine = false;
            percentage.PointSize = DeviceCheck.PointSize10;
            //percentage.PointSize = 10.0f;
            percentage.Text = (progressBar.ProgressValue * 100).ToString() + "%";
            percentage.TextColor = Color.White;
            Window.Instance.GetDefaultLayer().Add(percentage);

            // Create timer. In this timer tick,
            // change the progressBar's value.
            Timer timer = new Timer(50);
            timer.Tick += (obj, e) =>
            {
                if (progressBar != null)
                {
                    float progress = (float)Math.Round(progressBar.ProgressValue, 2);

                    if (progress == 1.0f)
                    {
                        progressBar.ProgressValue = 0.0f;
                        percentage.Text = (progressBar.ProgressValue * 100).ToString() + "%";
                        return false;
                    }
                    else
                    {
                        progressBar.ProgressValue = progress + 0.01f;
                        percentage.Text = (progressBar.ProgressValue * 100).ToString() + "%";
                        return true;

                    }

                }
                else
                {
                    return true;
                }
            };
            timer.Start();
        }

        /// <summary>
        /// Dispose progressBar
        /// </summary>
        public void Deactivate()
        {
            Window.Instance.GetDefaultLayer().Remove(guide);
            guide.Dispose();
            guide = null;
            Window.Instance.GetDefaultLayer().Remove(percentage);
            percentage.Dispose();
            percentage = null;
            Window.Instance.GetDefaultLayer().Remove(progressBar);
            progressBar.Dispose();
            progressBar = null;
        }

        /// <summary>
        /// Activate progressBar
        /// </summary>
        public void Activate()
        {
            Initialize();
        }
    }
}