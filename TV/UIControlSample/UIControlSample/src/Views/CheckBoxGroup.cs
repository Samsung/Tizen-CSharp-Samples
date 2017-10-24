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
//using Tizen.Applications;

//------------------------------------------------------------------------------
// <manual-generated />
//
// This file can only run on Tizen target. You should compile it with DaliApplication.cs, and
// add tizen c# application related library as reference.
//------------------------------------------------------------------------------
namespace UIControlSample
{
    /// <summary>
    /// A sample of CheckBoxGroup.
    /// </summary>
    class CheckBoxGroup
    {
        private View checkBoxGroup;
        private CheckBoxButton checkBox1;
        private CheckBoxButton checkBox2;
        private CheckBoxButton checkBox3;

        /// <summary>
        /// Constructor to create new checkBoxGroup
        /// </summary>
        public CheckBoxGroup()
        {
            Initialize();
        }

        /// <summary>
        /// CheckBoxButtonGroup initialisation.
        /// </summary>
        private void Initialize()
        {
            checkBoxGroup = new View();
            checkBoxGroup.ParentOrigin = ParentOrigin.TopLeft;
            checkBoxGroup.PivotPoint = PivotPoint.TopLeft;
            checkBoxGroup.Size2D = new Size2D(300, 200);

            CheckBox radioSample1 = new CheckBox("Shadow");
            checkBox1 = radioSample1.GetCheckBoxButton();
            checkBox1.Position = new Position(0, 0, 0);

            CheckBox radioSample2 = new CheckBox("Color");
            checkBox2 = radioSample2.GetCheckBoxButton();
            checkBox2.Position = new Position(0, 50, 0);

            CheckBox radioSample3 = new CheckBox("Underline");
            checkBox3 = radioSample3.GetCheckBoxButton();
            checkBox3.Position = new Position(0, 100, 0);

            checkBoxGroup.Add(checkBox1);
            checkBoxGroup.Add(checkBox2);
            checkBoxGroup.Add(checkBox3);

            // Move focus from checkBox1.
            checkBox1.KeyEvent += (obj, e) =>
            {
                if (e.Key.KeyPressedName == "Down" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(checkBox2);
                }
                else if (e.Key.KeyPressedName == "Left" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(checkBoxGroup.LeftFocusableView);
                }
                else if (e.Key.KeyPressedName == "Right" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(checkBoxGroup.RightFocusableView);
                }

                return false;
            };

            // Move focus from checkBox1.
            checkBox2.KeyEvent += (obj, e) =>
            {
                if (e.Key.KeyPressedName == "Down" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(checkBox3);
                }
                else if (e.Key.KeyPressedName == "Up" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(checkBox1);
                }
                else if (e.Key.KeyPressedName == "Left" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(checkBoxGroup.LeftFocusableView);
                }
                else if (e.Key.KeyPressedName == "Right" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(checkBoxGroup.RightFocusableView);
                }

                return false;
            };

            // Move focus from checkBox3.
            checkBox3.KeyEvent += (obj, e) =>
            {
                if (e.Key.KeyPressedName == "Up" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(checkBox2);
                }
                else if (e.Key.KeyPressedName == "Left" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(checkBoxGroup.LeftFocusableView);
                }
                else if (e.Key.KeyPressedName == "Right" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(checkBoxGroup.RightFocusableView);
                }

                return false;
            };
        }

        /// <summary>
        /// Move focus the target.
        /// </summary>
        /// <param name="target">The view will be focused.</param>
        /// <returns>Move focus successfully or not</returns>
        private bool MoveFocusTo(View target)
        {
            if (target == null)
            {
                return false;
            }

            return FocusManager.Instance.SetCurrentFocusView(target);
        }

        /// <summary>
        /// Get CheckBoxGroup created in this class.
        /// </summary>
        /// <returns>The created checkBoxGroup</returns>
        public View GetCheckBoxGroup()
        {
            return checkBoxGroup;
        }

        /// <summary>
        /// Get checkBox1 created in this class.
        /// </summary>
        /// <returns>The created checkBox1</returns>
        public CheckBoxButton GetCheckBox1()
        {
            return checkBox1;
        }

        /// <summary>
        /// Get checkBox2 created in this class.
        /// </summary>
        /// <returns>The created checkBox2</returns>
        public CheckBoxButton GetCheckBox2()
        {
            return checkBox2;
        }

        /// <summary>
        /// Get checkBox3 created in this class.
        /// </summary>
        /// <returns>The created checkBox3</returns>
        public CheckBoxButton GetCheckBox3()
        {
            return checkBox3;
        }
    }
}
