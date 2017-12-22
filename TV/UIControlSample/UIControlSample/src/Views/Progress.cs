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
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

namespace UIControlSample
{
    /// <summary>
    /// A sample of progressBar
    /// </summary>
    public class Progress
    {
        private ProgressBar _progressBar;
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
            _progressBar = new ProgressBar();
            _progressBar.ParentOrigin = ParentOrigin.TopLeft;
            _progressBar.PivotPoint = PivotPoint.TopLeft;
            _progressBar.Focusable = true;
            _progressBar.Size2D = new Size2D(1728, 4);

            PropertyMap progressMap = new PropertyMap();
            progressMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            progressMap.Add(ImageVisualProperty.URL, new PropertyValue(progressImage));
            _progressBar.ProgressVisual = progressMap;

            PropertyMap trackMap = new PropertyMap();
            trackMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            trackMap.Add(ImageVisualProperty.URL, new PropertyValue(trackImage));
            _progressBar.TrackVisual = trackMap;

            _progressBar.Indeterminate = false;
            _progressBar.IndeterminateVisual = trackMap;
            _progressBar.IndeterminateVisualAnimation = CreateIndeterminateVisualAnimation();

            PropertyMap labelVisual = new PropertyMap();
            labelVisual.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            labelVisual.Add(TextVisualProperty.Text, new PropertyValue("progressBar"));
            labelVisual.Add(TextVisualProperty.TextColor, new PropertyValue(Color.White));
            labelVisual.Add(TextVisualProperty.PointSize, new PropertyValue(10));
            labelVisual.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
            _progressBar.LabelVisual = labelVisual;

            // Change _progressBar's Indeterminate.
            _progressBar.KeyEvent += (obj, e) =>
            {
                if (e.Key.KeyPressedName == "Return")
                {
                    _progressBar.Indeterminate = !_progressBar.Indeterminate;
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
        public ProgressBar GetProgressBar()
        {
            return _progressBar;
        }

        /// <summary>
        /// Create Indeterminate Visual Animation
        /// </summary>
        /// <returns>
        /// The PropertyArray which define Indeterminate Visual Animation.
        /// </returns>
        private PropertyArray CreateIndeterminateVisualAnimation()
        {
            PropertyArray indeterminateVisualAnimation = new PropertyArray();

            PropertyMap transitionMap = new PropertyMap();
            transitionMap.Add("target", new PropertyValue("indeterminateVisual"));
            transitionMap.Add("property", new PropertyValue("offset"));
            transitionMap.Add("initialValue", new PropertyValue(new Vector2(0.0f, 0.0f)));
            transitionMap.Add("targetValue", new PropertyValue(new Vector2(100.0f, 0.0f)));
            PropertyMap animator = new PropertyMap();
            animator.Add("alphaFunction", new PropertyValue("EASE_IN_OUT_BACK"));
            PropertyMap timePeriod = new PropertyMap();
            timePeriod.Add("delay", new PropertyValue(0.5f));
            timePeriod.Add("duration", new PropertyValue(100.0f));
            animator.Add("timePeriod", new PropertyValue(timePeriod));
            transitionMap.Add("animator", new PropertyValue(animator));
            indeterminateVisualAnimation.PushBack(new PropertyValue(transitionMap));

            return indeterminateVisualAnimation;
        }
    }
}
