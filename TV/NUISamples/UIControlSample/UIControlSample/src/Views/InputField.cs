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
    /// A sample of TextField.
    /// </summary>
    public class InputField
    {
        private InputMethodContext imf;
        private bool isImfVisible;
        private TextField _textField;
        private View bgView;
        private const string resources = "/home/owner/apps_rw/org.tizen.example.UIControlSample/res/images";
        private string textFieldBG = resources + "/TextField/bg_text_input_box_255_255_255_90.9.png";
        private string BG = resources + "/TextField/r_highlight_bg_focus.9.png";

        /// <summary>
        /// Get/Set _textField.text.
        /// </summary>
        public string Text
        {
            get
            {
                return _textField.Text;
            }

            set
            {
                _textField.Text = value;
            }
        }

        /// <summary>
        /// Constructor to create new InputField
        /// </summary>
        public InputField()
        {
            OnIntialize();
        }

        /// <summary>
        /// InputField initialisation.
        /// </summary>
        private void OnIntialize()
        {
            imf = new InputMethodContext();
            imf.AutoEnableInputPanel(true);
            isImfVisible = false;

            // Add a _text label to the window
            _textField = new TextField();
            PropertyMap map = new PropertyMap();
            map.Insert(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            map.Insert((int)ImageVisualProperty.URL, new PropertyValue(textFieldBG));
            _textField.Background = map;
            _textField.FontFamily = "Samsung One 400";
            _textField.PrimaryCursorColor = new Color(0, 129, 198, 100);
            _textField.SecondaryCursorColor = new Color(0, 129, 198, 100);
            _textField.CursorWidth = 2;
            _textField.PlaceholderText = "Input your Name";
            _textField.PlaceholderTextFocused = "Input your Name";
            _textField.ParentOrigin = ParentOrigin.TopLeft;
            _textField.PivotPoint = PivotPoint.TopLeft;
            _textField.Position = new Position(30, 18, 0);
            _textField.Size2D = new Size2D(1000, 80);
            _textField.HorizontalAlignment = HorizontalAlignment.Begin;
            _textField.VerticalAlignment = VerticalAlignment.Center;
            _textField.PointSize = DeviceCheck.PointSize10;
            //_textField.PointSize = 10.0f;
            _textField.Focusable = true;
            _textField.FocusGained += OnFocusGained;
            _textField.FocusLost += OnFocusLost;
            //_textField.KeyEvent += OnIMEKeyEvent;

            // Create TextField's BackgroundView.
            PropertyMap bgMap = new PropertyMap();
            bgMap.Insert(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            bgMap.Insert((int)ImageVisualProperty.URL, new PropertyValue(textFieldBG));
            bgView = new View();
            bgView.Background = bgMap;
            bgView.ParentOrigin = ParentOrigin.TopLeft;
            bgView.PivotPoint = PivotPoint.TopLeft;
            bgView.Size2D = new Size2D(1060, 116);
            bgView.Add(_textField);
        }

        /// <summary>
        /// Get the initialized bgView
        /// </summary>
        /// <returns>
        /// The bgView which be created in this class
        /// </returns>
        public View GetView()
        {
            return bgView;
        }

        /// <summary>
        /// Callback by _textField when it get focus.
        /// </summary>
        /// <param name="source">_textField</param>
        /// <param name="e">event</param>
        private void OnFocusGained(object source, EventArgs e)
        {
           PropertyMap map = new PropertyMap();
           map.Insert(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
           map.Insert((int)ImageVisualProperty.URL, new PropertyValue(BG));
           bgView.Background = map;
           ShowImf();
        }

        /// <summary>
        /// Callback by _textField when it lost focus.
        /// </summary>
        /// <param name="source">_textField</param>
        /// <param name="e">event</param>
        private void OnFocusLost(object source, EventArgs e)
        {
           PropertyMap map = new PropertyMap();
           map.Insert(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
           map.Insert((int)ImageVisualProperty.URL, new PropertyValue(textFieldBG));
           bgView.Background = map;
        }

        /// <summary>
        /// Get the initialized _textField
        /// </summary>
        /// <returns>
        /// The _textField which be created in this class
        /// </returns>
        public TextField GetTextField()
        {
            return _textField;
        }

        /// <summary>
        /// Callback by _textField when have key event.
        /// </summary>
        /// <param name="source">_textField</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private bool OnIMEKeyEvent(object source, View.KeyEventArgs e)
        {
            Tizen.Log.Fatal("NUI", "!!!!!OnIMEKeyEvent!!!!! + KeyName: " + e.Key.KeyPressedName);

            if (e.Key.State == Key.StateType.Up)
            {
                switch (e.Key.KeyPressedName)
                {
                    case "Select":
                        Tizen.Log.Fatal("NUI", "!!!!!OnIMEKeyEvent!!!!! + KeyName == Select ");
                        HideImf();
                        return false;
                    case "Cancel":
                        Text = "";
                        Tizen.Log.Fatal("NUI", "!!!!!OnIMEKeyEvent!!!!! + KeyName == Cancel ");
                        HideImf();
                        return false;
                    case "XF86Back":
                        Tizen.Log.Fatal("NUI", "!!!!!OnIMEKeyEvent!!!!! + KeyName == XF86Back ");
                        HideImf();
                        return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Move focus the target.
        /// </summary>
        /// <param name="target">The view will be focused.</param>
        /// <returns>Move focus successfully or not</returns>
        private bool MoveFocusTo(View target)
        {
            Tizen.Log.Fatal("NUI", "Move Focus To");
            if (isImfVisible == true || target == null)
            {
                Tizen.Log.Fatal("NUI", "Move Focus Failed For TextField!!!!!");
                return false;
            }

            return FocusManager.Instance.SetCurrentFocusView(target);
        }

        /// <summary>
        /// Hide imfManager.
        /// </summary>
        private void HideImf()
        {
            imf.Deactivate();
            imf.HideInputPanel();
            isImfVisible = false;
            Tizen.Log.Fatal("NUI", "Move Focus FbgView!!!!!");
            //FocusManager.Instance.SetCurrentFocusView(bgView);
        }

        /// <summary>
        /// Show imfManager.
        /// </summary>
        private void ShowImf()
        {
            imf.Activate();
            imf.ShowInputPanel();
            isImfVisible = true;
        }
    }
}
