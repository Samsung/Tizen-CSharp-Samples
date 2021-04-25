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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

namespace UIControlSample
{
    /// <summary>
    /// A sample of progressBar
    /// </summary>
    public class Progress
    {
        private Tizen.NUI.Components.Progress _progressBar;
        private const string resources = "/home/owner/apps_rw/org.tizen.example.UIControlSample/res/images";
        private string progressImage = resources + "/ProgressBar/img_viewer_progress_0_129_198_100.9.png";
        //private string trackImage = resources + "/ProgressBar/img_viewer_progress_0_0_0_100.9.png";
        private string trackImage = resources + "/ProgressBar/img_viewer_progress_255_255_255_100.9.png";

        /// <summary>
        /// Constructor to create new Progress
        /// </summary>
        public Progress()
        {
            OnIntialize();
        }

        /// <summary>
        /// _progressBar initialisation.
        /// </summary>
        private void OnIntialize()
        {
            _progressBar = new Tizen.NUI.Components.Progress();
            _progressBar.ParentOrigin = ParentOrigin.TopLeft;
            _progressBar.PivotPoint = PivotPoint.TopLeft;
            _progressBar.Focusable = true;
            _progressBar.Size2D = new Size2D(1728, 4);
            _progressBar.ProgressImageURL = progressImage;
            _progressBar.TrackImageURL = trackImage;
            _progressBar.MinValue = 0.0f;
            _progressBar.MaxValue = 100.0f;
            _progressBar.ProgressState = Tizen.NUI.Components.Progress.ProgressStatusType.Indeterminate;
            _progressBar.TooltipText = "progressBar";
            // Change _progressBar's Indeterminate.
            _progressBar.KeyEvent += (obj, e) =>
            {
                if (e.Key.KeyPressedName == "Return")
                {
                    if (_progressBar.ProgressState == Tizen.NUI.Components.Progress.ProgressStatusType.Indeterminate)
                    {
                        _progressBar.ProgressState = Tizen.NUI.Components.Progress.ProgressStatusType.Determinate;
                    }
                    else
                    {
                        _progressBar.ProgressState = Tizen.NUI.Components.Progress.ProgressStatusType.Indeterminate;
                    }
                }

                return false;
            };
        }

        /// <summary>
        /// Get the initialized _progressBar
        /// </summary>
        /// <returns>
        /// The _progressBar which be created in this class
        /// </returns>
        public Tizen.NUI.Components.Progress GetProgressBar()
        {
            return _progressBar;
        }
    }
}
