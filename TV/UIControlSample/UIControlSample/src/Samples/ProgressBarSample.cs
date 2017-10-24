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
            Window.Instance.BackgroundColor = new Color(0.8f, 0.8f, 0.8f, 1.0f);

            guide = new TextLabel();
            guide.HorizontalAlignment = HorizontalAlignment.Begin;
            guide.VerticalAlignment = VerticalAlignment.Center;
            guide.PositionUsesPivotPoint = true;
            guide.ParentOrigin = ParentOrigin.TopLeft;
            guide.PivotPoint = PivotPoint.TopLeft;
            guide.Position = new Position(700, 300, 0);
            guide.MultiLine = true;
            guide.PointSize = 15.0f;
            guide.Text = "ProgressBar Sample\n";
            guide.TextColor = Color.White;
            Window.Instance.GetDefaultLayer().Add(guide);

            Progress progressSample = new Progress();
            progressBar = progressSample.GetProgressBar();
            progressBar.Position = new Position(40, 500, 0);
            Window.Instance.GetDefaultLayer().Add(progressBar);
            // Create timer. In this timer tick,
            // change the progressBar's value.
            Timer timer = new Timer(50);
            timer.Tick += (obj, e) =>
            {
                float progress = (float)Math.Round(progressBar.ProgressValue, 2);

                if (progress == 1.0f)
                {
                    progressBar.ProgressValue = 0.0f;
                    return false;
                }
                else
                {
                    progressBar.ProgressValue = progress + 0.01f;
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