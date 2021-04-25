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
    /// A sample of toggle.
    /// </summary>
    public class Toggle
    {
        private Tizen.NUI.Components.CheckBox _toggleButton;
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
            _toggleButton = new Tizen.NUI.Components.CheckBox();
            _toggleButton.Focusable = true;
            _toggleButton.ParentOrigin = ParentOrigin.TopLeft;
            _toggleButton.PivotPoint = PivotPoint.TopLeft;
            _toggleButton.Size2D = new Size2D(150, 80);
            _toggleButton.CheckImageURLSelector = new StringSelector()
            {
                Normal = normalOffImage,
                Selected = normalOnImage,
                Focused = focusedOffImage,
                SelectedFocused = focusedOnImage
            };
        }

        /// <summary>
        /// Get the initialized _toggleButton
        /// </summary>
        /// <returns>
        /// The _toggleButton which be created in this class
        /// </returns>
        public Tizen.NUI.Components.CheckBox GetToggleButton()
        {
            return _toggleButton;
        }
    }
}
