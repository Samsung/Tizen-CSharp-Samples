/*
* Copyright (c) 2016 Samsung Electronics Co., Ltd.
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
    /// A sample of Sliders
    /// </summary>
    class Sliders
    {
        private View horizontalView;
        Slider horizontalSlider;
        private const string resources = "/home/owner/apps_rw/org.tizen.example.UIControlSample/res";
        string handleUnSelectedVisual = resources + "/images/Slider/img_slider_handler_h_unselected.png";
        string handleSelectedVisual = resources + "/images/Slider/img_slider_handler_h_selected.png";
        string trackVisual = resources + "/images/Slider/img_slider_track.png";
        string progressVisual = resources + "/images/Slider/img_slider_progress.png";
        const int sliderWidth = 392;

        /// <summary>
        /// Constructor to create new Sliders.
        /// </summary>
        public Sliders()
        {
            OnIntialize();
        }

        /// <summary>
        /// Create Normal background
        /// </summary>
        /// <returns>
        /// The PropertyMap which define the normal background.
        /// </returns>
        private PropertyMap CreateNormalBG()
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            map.Add(ImageVisualProperty.URL, new PropertyValue(handleUnSelectedVisual));
            return map;
        }

        /// <summary>
        /// Create selected background
        /// </summary>
        /// <returns>
        /// The PropertyMap which define the selected background.
        /// </returns>
        private PropertyMap CreateSelectedBG()
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            map.Add(ImageVisualProperty.URL, new PropertyValue(handleSelectedVisual));
            return map;
        }

        /// <summary>
        /// Slider initialisation.
        /// </summary>
        private void OnIntialize()
        {
            horizontalView = new View();
            horizontalView.ParentOrigin = ParentOrigin.TopLeft;
            horizontalView.Position2D = new Position2D(20, 50);
            horizontalView.Size2D = new Size2D(400, 100);
            InitializeHorizontalView();
        }

        /// <summary>
        /// Initial the horizontal view.
        /// </summary>
        private void InitializeHorizontalView()
        {
            TextLabel leftValueHor = new TextLabel();
            leftValueHor.TextColor = Color.White;
            leftValueHor.ParentOrigin = ParentOrigin.TopLeft;
            leftValueHor.Position2D = new Position2D(0, 15);
            leftValueHor.PointSize = 8.0f;

            TextLabel rightValueHor = new TextLabel();
            rightValueHor.TextColor = Color.White;
            rightValueHor.ParentOrigin = ParentOrigin.TopLeft;
            rightValueHor.Position2D = new Position2D((sliderWidth + 35), 15);
            rightValueHor.PointSize = 8.0f;

            TextLabel currentValueHor = new TextLabel();
            currentValueHor.TextColor = Color.White;
            currentValueHor.ParentOrigin = ParentOrigin.TopLeft;
            currentValueHor.PointSize = 8.0f;
            currentValueHor.Position2D = new Position2D(170, 50);

            horizontalView.Add(leftValueHor);
            horizontalView.Add(rightValueHor);
            horizontalView.Add(currentValueHor);

            horizontalSlider = new Slider();
            horizontalSlider.ParentOrigin = ParentOrigin.TopLeft;
            horizontalSlider.Position2D = new Position2D(25, 12);
            horizontalSlider.Size2D = new Size2D(sliderWidth, 30);
            horizontalSlider.ShowPopup = false;
            horizontalSlider.ShowValue = false;
            horizontalSlider.LowerBound = 0.0f;
            horizontalSlider.UpperBound = 100.0f;
            horizontalSlider.Value = 100;

            currentValueHor.Position2D = new Position2D((int)GetHorizontalSliderPosition(), currentValueHor.Position2D.Y);

            // Create selected handle visual.
            PropertyMap hanldeSelectedVisualMap = new PropertyMap();
            hanldeSelectedVisualMap.Add("visualType", new PropertyValue("IMAGE"));
            hanldeSelectedVisualMap.Add("size", new PropertyValue(new Vector2(4, 25)));
            hanldeSelectedVisualMap.Add("url", new PropertyValue(handleSelectedVisual));

            // Create unselected handle visual.
            PropertyMap hanldeUnSelectedVisualMap = new PropertyMap();
            hanldeUnSelectedVisualMap.Add("visualType", new PropertyValue("IMAGE"));
            hanldeUnSelectedVisualMap.Add("size", new PropertyValue(new Vector2(4, 25)));
            hanldeUnSelectedVisualMap.Add("url", new PropertyValue(handleUnSelectedVisual));
            horizontalSlider.HandleVisual = hanldeUnSelectedVisualMap;

            // Create progress visual map.
            PropertyMap progressVisualMap = new PropertyMap();
            progressVisualMap.Add("visualType", new PropertyValue("IMAGE"));
            progressVisualMap.Add("size", new PropertyValue(new Vector2(10, 4)));
            progressVisualMap.Add("url", new PropertyValue(progressVisual));
            horizontalSlider.ProgressVisual = progressVisualMap;

            // Create track visual map.
            PropertyMap trackVisualMap = new PropertyMap();
            trackVisualMap.Add("visualType", new PropertyValue("IMAGE"));
            trackVisualMap.Add("size", new PropertyValue(new Vector2(10, 4)));
            trackVisualMap.Add("url", new PropertyValue(trackVisual));
            horizontalSlider.TrackVisual = trackVisualMap;

            horizontalSlider.ValueChanged += (obj, e) =>
            {
                horizontalSlider.HandleVisual = hanldeSelectedVisualMap;

                float radio = horizontalSlider.Value / (horizontalSlider.UpperBound - horizontalSlider.LowerBound);
                float positionX = GetHorizontalSliderPosition();

                currentValueHor.Position2D = new Position2D((int)positionX, currentValueHor.Position2D.Y);
                currentValueHor.Text = ((int)horizontalSlider.Value).ToString();
                return true;
            };

            horizontalSlider.SlidingFinished += (obj, ee) =>
            {
                horizontalSlider.HandleVisual = hanldeUnSelectedVisualMap;
                return true;
            };


            leftValueHor.Text = ((int)horizontalSlider.LowerBound).ToString();
            rightValueHor.Text = ((int)horizontalSlider.UpperBound).ToString();
            currentValueHor.Text = "0";
            horizontalView.Add(horizontalSlider);

            horizontalSlider.Focusable = true;
            FocusManager.Instance.SetCurrentFocusView(horizontalSlider);

            horizontalSlider.KeyEvent += (obj, ee) =>
            {
                if ("Left" == ee.Key.KeyPressedName && Key.StateType.Up == ee.Key.State)
                {
                    if (horizontalSlider.Value > 0)
                    {
                        horizontalSlider.Value--;
                    }
                }
                else if ("Right" == ee.Key.KeyPressedName && Key.StateType.Up == ee.Key.State)
                {
                    if (horizontalSlider.Value < 100)
                    {
                        horizontalSlider.Value++;
                    }
                }
                else if ("Up" == ee.Key.KeyPressedName && Key.StateType.Up == ee.Key.State)
                {
                    MoveFocusTo(horizontalView.UpFocusableView);
                }

                return false;
            };

            horizontalSlider.FocusGained += (obj, ee) =>
            {
                horizontalSlider.HandleVisual = hanldeSelectedVisualMap;
                Tizen.Log.Fatal("NUI", "horizontalSlider got the focus!!!!!!!!");
            };

            horizontalSlider.FocusLost += (obj, ee) =>
            {
                horizontalSlider.HandleVisual = hanldeUnSelectedVisualMap;
                Tizen.Log.Fatal("NUI", "horizontalSlider lost the focus!!!!!!!!");
            };
        }

        /// <summary>
        /// Move focus the target.
        /// </summary>
        /// <param name="target">The view will be focused.</param>
        /// <returns>Move focus successfully or not</retrun>
        private bool MoveFocusTo(View target)
        {
            if (target == null)
            {
                return false;
            }

            return FocusManager.Instance.SetCurrentFocusView(target);
        }

        /// <summary>
        /// Get the positionX of horizontalSlider.
        /// </summary>
        /// <returns>The positionX of horizontalSlider.</retrun>
        private float GetHorizontalSliderPosition()
        {
            float radio = horizontalSlider.Value / (horizontalSlider.UpperBound - horizontalSlider.LowerBound);
            float positionX = sliderWidth * radio + horizontalSlider.Position2D.X - 10.0f;
            if(radio > 0.1)
            {
                positionX = positionX - 8;
            }
            return positionX;
        }

        /// <summary>
        /// Get the horizontalView
        /// </summary>
        /// <returns>
        /// The horizontalView which be created in this class
        /// </returns>
        public View GetHorizentalSlider()
        {
            return horizontalView;
        }

        /// <summary>
        /// Get the slider.
        /// </summary>
        /// <returns>
        /// The slider which be created in this class
        /// </returns>
        public Slider GetSlider()
        {
            return horizontalSlider;
        }
    }
}



