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
    /// A sample of toggle.
    /// </summary>
    public class Toggle
    {
        private CheckBoxButton _toggleButton;
        private const string resources = "/home/owner/apps_rw/org.tizen.example.UIControlSample/res/images";
        private string normalOffImage = resources + "/Toggle/Unfocused_01.png";
        private string focusedOnImage = resources + "/Toggle/Focused_02.png";
        private string focusedOffImage = resources + "/Toggle/Focused_01.png";
        private string normalOnImage = resources + "/Toggle/Selected_01.png";

        /// <summary>
        /// Constructor to create new Toggle
        /// </summary>
        public Toggle()
        {
            OnIntialize();
        }

        /// <summary>
        /// Popups initialisation.
        /// </summary>
        private void OnIntialize()
        {
            _toggleButton = new CheckBoxButton();
            _toggleButton.Focusable = true;
            _toggleButton.ParentOrigin = ParentOrigin.TopLeft;
            _toggleButton.PivotPoint = PivotPoint.TopLeft;
            _toggleButton.Size2D = new Size2D(150, 80);

            PropertyMap unselectedMap = new PropertyMap();
            unselectedMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            unselectedMap.Add(ImageVisualProperty.URL, new PropertyValue(normalOffImage));

            PropertyMap selectedMap = new PropertyMap();
            selectedMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            selectedMap.Add(ImageVisualProperty.URL, new PropertyValue(normalOnImage));

            PropertyMap focusUnselectedMap = new PropertyMap();
            focusUnselectedMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            focusUnselectedMap.Add(ImageVisualProperty.URL, new PropertyValue(focusedOffImage));

            PropertyMap focusSelectedMap = new PropertyMap();
            focusSelectedMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            focusSelectedMap.Add(ImageVisualProperty.URL, new PropertyValue(focusedOnImage));

            _toggleButton.UnselectedVisual = unselectedMap;
            _toggleButton.SelectedVisual = selectedMap;

            _toggleButton.FocusGained += (obj, e) =>
            {
                _toggleButton.UnselectedVisual = focusUnselectedMap;
                _toggleButton.SelectedVisual = focusSelectedMap;
            };

            _toggleButton.FocusLost += (obj, e) =>
            {
                _toggleButton.UnselectedVisual = unselectedMap;
                _toggleButton.SelectedVisual = selectedMap;
            };
        }

        /// <summary>
        /// Get the initialized _toggleButton
        /// </summary>
        /// <returns>
        /// The _toggleButton which be created in this class
        /// </returns>
        public CheckBoxButton GetToggleButton()
        {
            return _toggleButton;
        }
    }
}
