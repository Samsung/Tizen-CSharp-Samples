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
    class ActivityIndicator : View
    {
        private ImageView _image1 = null;
        private Animation _animation1 = null;

        private ImageView _image2 = null;
        private Animation _animation2 = null;

        private const string resources = "/home/owner/apps_rw/org.tizen.example.UIControlSample/res/images";
        private string imagePath1 = resources + "/Loading/img_activity_indicator_126_1.png";
        private string imagePath2 = resources + "/Loading/img_activity_indicator_126_2.png";

        private int _duration = 1000;

        /// <summary>
        /// Restore the animation of _image1 and _image2.
        /// </summary>
        private void RestoreAnimation()
        {
            if (null == _animation1)
            {
                _animation1 = new Animation
                {
                    Duration = _duration
                };

                _animation1.LoopCount = 0;
                _animation1.AnimateTo(_image1, "Orientation", new Rotation(new Radian(new Degree(120.0f)), PositionAxis.Z), 0, _duration / 3);
                _animation1.AnimateTo(_image1, "Orientation", new Rotation(new Radian(new Degree(240.0f)), PositionAxis.Z), _duration / 3, _duration * 2 / 3);
                _animation1.AnimateTo(_image1, "Orientation", new Rotation(new Radian(new Degree(0.0f)), PositionAxis.Z), _duration * 2 / 3, _duration);
            }
            else
            {
                _animation1.Stop();
                _image1.Orientation = new Rotation(new Radian(new Degree(0.0f)), PositionAxis.Z);
            }

            if (null == _animation2)
            {
                _animation2 = new Animation
                {
                    Duration = _duration / 2
                };

                _animation2.LoopCount = 0;
                _animation2.AnimateTo(_image2, "Orientation", new Rotation(new Radian(new Degree(300.0f)), PositionAxis.Z), 0, _animation2.Duration / 3);
                _animation2.AnimateTo(_image2, "Orientation", new Rotation(new Radian(new Degree(60.0f)), PositionAxis.Z), _animation2.Duration / 3, _animation2.Duration * 2 / 3);
                _animation2.AnimateTo(_image2, "Orientation", new Rotation(new Radian(new Degree(180.0f)), PositionAxis.Z), _animation2.Duration * 2 / 3, _animation2.Duration);
            }
            else
            {
                _animation2.Stop();
                _image2.Orientation = new Rotation(new Radian(new Degree(180.0f)), PositionAxis.Z);
            }
        }

        /// <summary>
        /// Constructor to activityIndicator.
        /// </summary>
        public ActivityIndicator()
        {
            SetImageUrl(imagePath1, imagePath2);
        }

        /// <summary>
        /// Set the image url of _image1 and _image2.
        /// </summary>
        /// <param name="imgUrl1">The url of _image1</param>
        /// <param name="imgUrl2">The url of _image2</param>
        public void SetImageUrl(string imgUrl1, string imgUrl2)
        {
            if (null == _image1)
            {
                _image1 = new ImageView(imgUrl1);
                _image1.Size2D = new Size2D(126, 126);
                Add(_image1);
            }
            else
            {
                _image1.SetImage(imgUrl1);
            }

            if (null == _image2)
            {
                _image2 = new ImageView(imgUrl2);
                _image2.Size2D = new Size2D(126, 126);
                _image2.Orientation = new Rotation(new Radian(new Degree(180.0f)), PositionAxis.Z);
                Add(_image2);
            }
            else
            {
                _image2.SetImage(imgUrl2);
            }

            RestoreAnimation();

            Play();
        }

        /// <summary>
        /// Play the animation.
        /// </summary>
        public void Play()
        {
            _animation1.Play();
            _animation2.Play();
        }
    }
}
