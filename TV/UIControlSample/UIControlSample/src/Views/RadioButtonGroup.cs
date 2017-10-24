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
    /// A sample of RadioButtonGroup.
    /// </summary>
    class RadioButtonGroup
    {
        private View radioButtonGroup;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;

        /// <summary>
        /// Constructor to create new RadioButtonGroup
        /// </summary>
        public RadioButtonGroup()
        {
            Initialize();
        }

        /// <summary>
        /// RadioButtonGroup initialisation.
        /// </summary>
        private void Initialize()
        {
            radioButtonGroup = new View();
            radioButtonGroup.ParentOrigin = ParentOrigin.TopLeft;
            radioButtonGroup.PivotPoint = PivotPoint.TopLeft;
            radioButtonGroup.Size2D = new Size2D(300, 200);

            Radio radioSample1 = new Radio("Red");
            radioButton1 = radioSample1.GetRadioButton();
            radioButton1.Position = new Position(0, 0, 0);

            Radio radioSample2 = new Radio("Green");
            radioButton2 = radioSample2.GetRadioButton();
            radioButton2.Position = new Position(0, 50, 0);

            Radio radioSample3 = new Radio("Blue");
            radioButton3 = radioSample3.GetRadioButton();
            radioButton3.Position = new Position(0, 100, 0);

            radioButtonGroup.Add(radioButton1);
            radioButtonGroup.Add(radioButton2);
            radioButtonGroup.Add(radioButton3);

            // Move focus from radioButton1.
            radioButton1.KeyEvent += (obj, e) =>
            {
                if (e.Key.KeyPressedName == "Down" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(radioButton2);
                }
                else if (e.Key.KeyPressedName == "Left" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(radioButtonGroup.LeftFocusableView);
                }
                else if (e.Key.KeyPressedName == "Right" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(radioButtonGroup.RightFocusableView);
                }

                return false;
            };

            // Move focus from radioButton2.
            radioButton2.KeyEvent += (obj, e) =>
            {
                if (e.Key.KeyPressedName == "Down" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(radioButton3);
                }
                else if (e.Key.KeyPressedName == "Up" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(radioButton1);
                }
                else if (e.Key.KeyPressedName == "Left" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(radioButtonGroup.LeftFocusableView);
                }
                else if (e.Key.KeyPressedName == "Right" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(radioButtonGroup.RightFocusableView);
                }

                return false;
            };

            // Move focus from radioButton3.
            radioButton3.KeyEvent += (obj, e) =>
            {
                if (e.Key.KeyPressedName == "Up" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(radioButton2);
                }
                else if (e.Key.KeyPressedName == "Left" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(radioButtonGroup.LeftFocusableView);
                }
                else if (e.Key.KeyPressedName == "Right" && Key.StateType.Up == e.Key.State)
                {
                    MoveFocusTo(radioButtonGroup.RightFocusableView);
                }

                return false;
            };
        }

        /// <summary>
        /// Move focus the target.
        /// </summary>
        /// <param name="target">The view will be focused.</param>
        /// <returns>Move focus successfully or not<returns>
        private bool MoveFocusTo(View target)
        {
            if (target == null)
            {
                return false;
            }

            return FocusManager.Instance.SetCurrentFocusView(target);
        }

        /// <summary>
        /// Get radioButtonGroup created in this class.
        /// </summary>
        /// <returns>The created radioButtonGroup</returns>
        public View GetRadioButtonGroup()
        {
            return radioButtonGroup;
        }

        /// <summary>
        /// Get radioButton1 created in this class.
        /// </summary>
        /// <returns>The created radioButton1</returns>
        public RadioButton GetFocusableView()
        {
            return radioButton1;
        }

        /// <summary>
        /// Get radioButton1 created in this class.
        /// </summary>
        /// <returns>The created radioButton1</returns>
        public RadioButton GetRadioButton1()
        {
            return radioButton1;
        }

        /// <summary>
        /// Get radioButton2 created in this class.
        /// </summary>
        /// <returns>The created radioButton2</returns>
        public RadioButton GetRadioButton2()
        {
            return radioButton2;
        }

        /// <summary>
        /// Get radioButton2 created in this class.
        /// </summary>
        /// <returns>The created radioButton2</returns>
        public RadioButton GetRadioButton3()
        {
            return radioButton3;
        }
    }
}
