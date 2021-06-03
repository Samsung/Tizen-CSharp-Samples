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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

/// <summary>
/// Namespace for Tizen.NUI.MediaHub package.
/// </summary>
namespace Tizen.NUI.MediaHub
{
    /// <summary>
    /// The TextItem class. 
    /// Text items are text format items that are selectable and part of a list or grid element.
    /// </summary>
    /// <code>
    /// TextItem textItem = new TextItem();
    /// textItem.MainText = "Hello world!";
    /// textItem.Size = new Size(400, 80, 0);
    /// textItem.Position = new Position(100, 100, 0);
    /// </code>
    public class TextItem : View
    {

        /// <summary>
        /// The constructor of the TextItem class with specified style string.
        /// </summary>
        public TextItem()
        {
            Initialize();
        }
        
        /// <summary>
        /// The property of the MainIcon's content.
        /// </summary>
        public string MainText
        {
            get
            {
                return mainTextContent;
            }

            set
            {
                Tizen.Log.Fatal("NUI", "value = " + value);
                mainTextContent = value;
                if (mainText != null)
                {
                    mainText.Text = mainTextContent;
                }

                UpdateMainTextScroll();
            }
        }

        /// <summary>
        /// The property of the state that the ListLine is enabled or not.
        /// </summary>
        public bool StateListLineEnabled
        {
            get
            {
                return isListLineEnabled;
            }

            set
            {
                Tizen.Log.Fatal("NUI", "value = " + value);
                isListLineEnabled = value;
                ApplyListLineView();
            }
        }

        /// <summary>
        /// The property of the focused state that set focus to the TextItem or move focus away from the TextItem.
        /// </summary>
        public bool StateFocused
        {
            get
            {
                return isFocused;
            }

            set
            {
                Tizen.Log.Fatal("NUI", "value = " + value);
                if (isFocused == value)
                {
                    return;
                }

                isFocused = value;
                UpdateFocusedState();
            }
        }

        /// <summary>
        /// The property of the selected state that selected the TextItem or not.
        /// </summary>
        public bool StateSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                Tizen.Log.Fatal("NUI", "value = " + value);
                if (isSelected == value)
                {
                    return;
                }

                isSelected = value;
                UpdateSelectedState();
            }
        }

        /// <summary>
        /// The property of the disabled state that disabled the TextItem or not.
        /// </summary>
        public bool StateEnabled
        {
            get
            {
                return isEnabled;
            }

            set
            {
                Tizen.Log.Fatal("NUI", "value = " + value);
                if (isEnabled == value)
                {
                    return;
                }

                isEnabled = value;
                UpdateEnabledState();
            }
        }

        /// <summary>
        /// Dispose TextItem.
        /// </summary>
        /// <param name="type">The DisposeTypes value.</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.
                //Dispose the focus in animation
                focusInAni?.Stop();
                focusInAni?.Clear();
                focusInAni?.Dispose();
                focusInAni = null;
                //Dispose the focus out animation
                focusOutAni?.Stop();
                focusOutAni?.Clear();
                focusOutAni?.Dispose();
                focusOutAni = null;
                //Dispose the selected in animation
                selectedInAni?.Stop();
                selectedInAni?.Clear();
                selectedInAni?.Dispose();
                selectedInAni = null;
                //Dispose the selected out animation
                selectedOutAni?.Stop();
                selectedOutAni?.Clear();
                selectedOutAni?.Dispose();
                selectedOutAni = null;
                //Dispose the dim in animation
                dimInAni?.Stop();
                dimInAni?.Clear();
                dimInAni?.Dispose();
                dimInAni = null;
                //Dispose the dim out animation
                dimOutAni?.Stop();
                dimOutAni?.Clear();
                dimOutAni?.Dispose();
                dimOutAni = null;
                //Dispose the main text
                if (mainText != null)
                {
                    rootView.Remove(mainText);
                    mainText.Dispose();
                    mainText = null;
                }
                //Dispose the checkIcon
                if (checkIcon != null)
                {
                    rootView.Remove(checkIcon);
                    checkIcon.Dispose();
                    checkIcon = null;
                }
                //Dispose the listLineView
                if (listLineView != null)
                {
                    rootView.Remove(listLineView);
                    listLineView.Dispose();
                    listLineView = null;
                }
                //Dispose the background view
                if (backgroundView != null)
                {
                    rootView.Remove(backgroundView);
                    backgroundView.Dispose();
                    backgroundView = null;
                }
                //Dispose the root view
                if (rootView != null)
                {
                    Remove(rootView);
                    rootView.Dispose();
                    rootView = null;
                }
            }

            //Release your own unmanaged resources here.
            //You should not access any managed member here except static instance.
            //because the execution order of Finalizes is non-deterministic.
            //Unreference this from if a static instance refer to this. 

            //You must call base.Dispose(type) just before exit.
            base.Dispose(type);
        }

        /// <summary>
        /// The method to update Attributes.
        /// </summary>
        public void OnUpdate()
        {

            ApplyAttributes();
        }

        /// <summary>
        /// The method to Initialize
        /// </summary>
        private void Initialize()
        {
            Relayout += OnRelayout;
            Tizen.Log.Fatal("NUI", "Create mandatory components");
            InitializeRootView();
            InitializeBGView();
        }

        /// <summary>
        /// Callback of OnRelayoutEvent
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event args</param>
        private void OnRelayout(object sender, EventArgs e)
        {
            OnUpdate();
        }

        /// <summary>
        /// The method to initialize the root view.
        /// </summary>
        private void InitializeRootView()
        {
            Tizen.Log.Fatal("NUI", "Create RootView");
            rootView = new View();
            rootView.WidthResizePolicy = ResizePolicyType.FillToParent;
            rootView.HeightResizePolicy = ResizePolicyType.FillToParent;
            rootView.PositionUsesPivotPoint = true;
            rootView.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            rootView.PivotPoint = Tizen.NUI.PivotPoint.Center;
            rootView.Name = "RootView";
            Add(rootView);
        }

        /// <summary>
        /// The method to initialize the bg view.
        /// </summary>
        private void InitializeBGView()
        {
            Tizen.Log.Fatal("NUI", "Create BGView");
            backgroundView = new ImageView();
            backgroundView.WidthResizePolicy = ResizePolicyType.Fixed;
            backgroundView.HeightResizePolicy = ResizePolicyType.Fixed;
            backgroundView.PositionUsesPivotPoint = true;
            backgroundView.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            backgroundView.PivotPoint = Tizen.NUI.PivotPoint.Center;
            backgroundView.Name = "BgView";
            rootView.Add(backgroundView);
        }

        /// <summary>
        /// The method to initialize the list line view.
        /// </summary>
        private void InitializeListLineView()
        {
            if (listLineView == null && isListLineEnabled)
            {
                Tizen.Log.Fatal("NUI", "Create ListLineView");
                listLineView = new ImageView();
                listLineView.WidthResizePolicy = ResizePolicyType.FillToParent;
                listLineView.HeightResizePolicy = ResizePolicyType.Fixed;
                listLineView.PositionUsesPivotPoint = true;
                listLineView.ParentOrigin = Tizen.NUI.ParentOrigin.BottomLeft;
                listLineView.PivotPoint = Tizen.NUI.PivotPoint.BottomLeft;
                listLineView.Name = "ListLineView";
                rootView.Add(listLineView);
            }
        }

        /// <summary>
        /// The method to initialize the main text.
        /// </summary>
        private void InitializeMainText()
        {
            if (mainText != null)
            {
                return;
            }

            Tizen.Log.Fatal("NUI", "Create MainText");
            mainText = new TextLabel();
            mainText.WidthResizePolicy = ResizePolicyType.Fixed;
            mainText.HeightResizePolicy = ResizePolicyType.Fixed;
            mainText.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            mainText.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            mainText.Name = "MainText";
            rootView.Add(mainText);
        }

        /// <summary>
        /// The method to initialize the check icon.
        /// </summary>
        private void InitializeCheckIcon()
        {
            if (checkIcon != null)
            {
                return;
            }

            Tizen.Log.Fatal("NUI", "Create CheckIcon");
            checkIcon = new ImageView();
            checkIcon.WidthResizePolicy = ResizePolicyType.Fixed;
            checkIcon.HeightResizePolicy = ResizePolicyType.Fixed;
            checkIcon.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            checkIcon.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            checkIcon.Name = "CheckIcon";
            rootView.Add(checkIcon);
            checkIcon.RaiseToTop();
        }

        /// <summary>
        /// The method to initialize the animation.
        /// </summary>
        private void InitializeAnimation()
        {
            if (focusInAni == null)
            {
                //Create focus in animation
                focusInAni = new Animation();
            }

            if (focusOutAni == null)
            {
                //Create focus out animation
                focusOutAni = new Animation();
            }

            if (dimInAni == null)
            {
                //Create dim in animation
                dimInAni = new Animation();
            }

            if (dimOutAni == null)
            {
                //Create dim out animation
                dimOutAni = new Animation();
            }

            if (selectedInAni == null)
            {
                //Create selected in animation
                selectedInAni = new Animation();
            }

            if (selectedOutAni == null)
            {
                //Create selected out animation
                selectedOutAni = new Animation();
            }
        }

        /// <summary>
        /// Set the properties
        /// </summary>
        private void ApplyAttributes()
        {
            Tizen.Log.Fatal("NUI", "Apply Attributes");
            ApplyBgView();
            ApplyListLineView();
            ApplyCheckIcon();
            ApplyMainText();
            ApplyAnimation();
        }
        
        /// <summary>
        /// Set the properties of the background view
        /// </summary>
        private void ApplyBgView()
        {
            Tizen.Log.Fatal("NUI", "Apply BGView");
 
            if (isFocused)
            {
                string url = CommonResource.GetLocalReosurceURL() + "highlight/r_highlight_bg_focus.9.png";
                backgroundView.SetImage(url);
            }
            else
            {
                string url = CommonResource.GetLocalReosurceURL() + "component/c_textitem/c_textitem_normal_transparentbg.png";
                backgroundView.SetImage(url);
            }

            float rootWidth = Size2D.Width;
            float rootHeight = Size2D.Height;
            float newWidth = rootWidth;
            float newHeight = rootHeight;

            float left = 0, right = 0, top = 0, bottom = 0;
            if (isFocused)
            {
                left = 7.0f;
                right = 7.0f;
                top = 12.0f;
                bottom = 12.0f;
            }

            newWidth = rootWidth + left + right;
            newHeight = rootHeight + top + bottom;

            Tizen.Log.Fatal("NUI", "width = " + rootWidth + ", height = " + rootHeight + ", newWidth = " + newWidth + ", newHeight = " + newHeight);
            backgroundView.Size2D = new Size2D((int)newWidth, (int)newHeight);
        }

        /// <summary>
        /// Set the properties of the list line view
        /// </summary>
        private void ApplyListLineView()
        {
            InitializeListLineView();
            if (listLineView == null)
            {
                return;
            }

            string url = CommonResource.GetLocalReosurceURL() + "component/c_textitem/c_textitem_white_single_line.png";
            listLineView.SetImage(url);
            listLineView.Size2D = new Size2D((int)SizeWidth, 2);
        }

        /// <summary>
        /// Set the properties of main text
        /// </summary>
        private void ApplyMainText()
        {
            InitializeMainText();
            ApplyMainTextSize();
            ApplyMainTextPosition();
            ApplyMainTextFontStyle();
            ApplyMainTextContent();
            ApplyMainTextScrollStyle();
            UpdateMainTextScroll();
        }

        /// <summary>
        /// The method to set the size of the main text.
        /// </summary>
        private void ApplyMainTextSize()
        {
            if (mainText == null)
            {
                return;
            }

            float width = SizeWidth - 20.0f * 2.0f - 26.0f - 10.0f;
            int height = 48;
            mainText.Size2D = new Size2D((int)width, height);
            mainTextWidth = width;
        }

        /// <summary>
        /// The method to set the position of the main text
        /// </summary>
        private void ApplyMainTextPosition()
        {
            if (mainText == null)
            {
                return;
            }
            
            float offsetX = 0;

            float x = 20.0f;
            float y = (SizeHeight - 48.0f) / 2.0f;
            x += offsetX;
            mainTextPosX = x;
            mainText.Position = new Position(x, y, 0);
        }

        /// <summary>
        /// The method to apply the font and style of the main text.
        /// </summary>
        private void ApplyMainTextFontStyle()
        {
            if (mainText == null)
            {
                return;
            }

            Tizen.Log.Fatal("NUI", "Apply MainText FontStyle");

            mainText.FontFamily = "SamsungOneUI_300";
            //mainText.PointSize = 8.0f; //enlargeEnabled == true PointSize = 36.0f;
            mainText.PointSize = DeviceCheck.PointSize8;
            mainText.HorizontalAlignment = HorizontalAlignment.Begin;
            mainText.VerticalAlignment = VerticalAlignment.Center;

            if (isFocused)
            {
                mainText.TextColor = Utility.Hex2Color(0x000000, 1.0f);
            }
            else if (isSelected)
            {
                mainText.TextColor = Utility.Hex2Color(0x000000, 1.0f);
            }
            else
            {
                mainText.TextColor = Utility.Hex2Color(0x262626, 1.0f);
            }
        }

        /// <summary>
        /// The method to apply the scroll style of the main text.
        /// </summary>
        private void ApplyMainTextScrollStyle()
        {
            if (mainText == null)
            {
                return;
            }

            Tizen.Log.Fatal("NUI", "Apply MainText ScrollStyle");

            mainText.AutoScrollSpeed = 50;
            mainText.AutoScrollLoopDelay = 1.0f;
            mainText.AutoScrollLoopCount = 0;
            mainText.AutoScrollGap = 50.0f;
            mainText.AutoScrollStopMode = AutoScrollStopMode.Immediate;
            enableScroll = true;
        }

        /// <summary>
        /// The method to update the scroll enable property of the main text
        /// </summary>
        private void UpdateMainTextScroll()
        {
            if (mainText == null || !enableScroll)
            {
                return;
            }

            if (isFocused)
            {
                // check the text's natural width is greater than user setting width or not. If yes, enable scroll; else, disable scroll.  
                float naturalWidth = mainText.NaturalSize2D.Width;
                float settingWidth = mainText.SizeWidth;
                Tizen.Log.Fatal("NUI", "naturalWidth = " + naturalWidth + ", settingWidth = " + settingWidth);
                if (naturalWidth > settingWidth)
                {
                    mainText.EnableAutoScroll = true;
                }
                else
                {
                    mainText.EnableAutoScroll = false;
                }
            }
            else
            {
                mainText.EnableAutoScroll = false;
            }
        }

        /// <summary>
        /// The method to apply the content of the main text
        /// </summary>
        private void ApplyMainTextContent()
        {
            if (mainText == null)
            {
                return;
            }

            if (mainTextContent != null)
            {
                Tizen.Log.Fatal("NUI", "mainTextContent = " + mainTextContent);
                mainText.Text = mainTextContent;
            }
        }

        /// <summary>
        /// Apply check icon.
        /// </summary>
        private void ApplyCheckIcon()
        {
            InitializeCheckIcon();
            string url = CommonResource.GetLocalReosurceURL() + "icon/r_icon_xs/r_icon_xs_check.png";
            checkIcon.SetImage(url);
            checkIcon.Size2D = new Size2D(26, 26);

            ApplyCheckIconPosition();

            if (isSelected)
            {
                checkIcon.Opacity = 1.0f;
            }
            else
            {
                checkIcon.Opacity = 0;
            }
        }

        /// <summary>
        /// The method to apply the animation.
        /// </summary>
        private void ApplyAnimation()
        {
            Tizen.Log.Fatal("NUI", "Apply Animation");
            InitializeAnimation();
            // focused
            int focusedInScaleDuration = 600;
            int focusedOutScaleDuration = 850;
            int focusedInOpacityDuration = 334;
            int focusedOutOpacityDuration = 334;
            float focusInOpacity = 1.0f;
            float focusOutOpacity = 0;
            focusInAni.Duration = focusedInScaleDuration;
            focusOutAni.Duration = focusedOutScaleDuration;
            if (backgroundView != null)
            {
                focusInAni.AnimateTo(backgroundView, "Opacity", focusInOpacity, 0, focusedInOpacityDuration);
                focusOutAni.AnimateTo(backgroundView, "Opacity", focusOutOpacity, 0, focusedOutOpacityDuration);
                Tizen.Log.Fatal("NUI", "isFocused = " + isFocused + ", focusInOpacity = " + focusInOpacity + ", focusOutOpacity = " + focusOutOpacity);
                if (isFocused)
                {
                    backgroundView.Opacity = focusInOpacity;
                }
                else
                {
                    backgroundView.Opacity = focusOutOpacity;
                }
            }

            // dim
            int dimOpacityDuration = 333;
            float dimInOpacity = 0.4f;
            float dimOutOpacity = 1.0f;
            dimInAni.Duration = dimOpacityDuration;
            dimOutAni.Duration = dimOpacityDuration;
            dimInAni.AnimateTo(this, "Opacity", dimInOpacity);
            dimOutAni.AnimateTo(this, "Opacity", dimOutOpacity);
            Tizen.Log.Fatal("NUI", "isEnabled = " + isEnabled + ", dimInOpacity = " + dimInOpacity + ", dimOutOpacity = " + dimOutOpacity);
            if (isEnabled)
            {
                Opacity = dimOutOpacity;
            }
            else
            {
                Opacity = dimInOpacity;
            }
            // selected
            int selectedDurationFirst = 167;
            int selectedDurationSecond = 417;
            int selectedDuration = 0;
            selectedDuration = selectedDurationFirst + selectedDurationSecond;
            Tizen.Log.Fatal("NUI", "selectedDuration = " + selectedDuration);
            selectedInAni.Duration = selectedDuration;
            selectedOutAni.Duration = selectedDuration;

            float selectedInOpacity = 1.0f;
            float selectedOutOpacity = 0;
            if (checkIcon != null)
            {
                Tizen.Log.Fatal("NUI", "checkIcon != null, bind checkIcon to selected in animation.");
                selectedInAni.AnimateTo(checkIcon, "Opacity", selectedInOpacity, selectedDurationFirst, selectedDuration);
                selectedOutAni.AnimateTo(checkIcon, "Opacity", selectedOutOpacity, 0, selectedDurationFirst);
                Tizen.Log.Fatal("NUI", "isSelected = " + isSelected + ", selectedInOpacity = " + selectedInOpacity + ", selectedOutOpacity = " + selectedOutOpacity);
                if (isSelected)
                {
                    checkIcon.Opacity = selectedInOpacity;
                }
                else
                {
                    checkIcon.Opacity = selectedOutOpacity;
                }
            }
        }

        /// <summary>
        /// The method to apply focus state
        /// </summary>
        private void UpdateFocusedState()
        {
            Tizen.Log.Fatal("NUI", "Update Focused state");
            ApplyBgView();
            UpdateMainTextFontStyleByState();
            PlayFocosedAnimation();
            UpdateMainTextScroll();
        }

        /// <summary>
        /// The method to play the focus animation.
        /// </summary>
        private void PlayFocosedAnimation()
        {
            Tizen.Log.Fatal("NUI", "Play Focused Animation");
            if (isFocused)
            {
                if (focusOutAni != null && focusOutAni.State == Animation.States.Playing)
                {
                    focusOutAni.Stop();
                }

                if (focusInAni != null)
                {
                    focusInAni.Play();
                }
            }
            else
            {
                if (focusInAni != null && focusInAni.State == Animation.States.Playing)
                {
                    focusInAni.Stop();
                }

                if (focusOutAni != null)
                {
                    focusOutAni.Play();
                }
            }
        }

        /// <summary>
        /// Update the selected state
        /// </summary>
        private void UpdateSelectedState()
        {
            Tizen.Log.Fatal("NUI", "Update Selected state");
            if (checkIcon != null)
            {
                if (isSelected)
                {
                    checkIcon.RaiseToTop();// temp add for checkicon is above mainIcon, like single02/single04
                    Tizen.Log.Fatal("NUI", "show CheckIcon");
                    checkIcon.Opacity = 0;
                }
                else
                {
                    Tizen.Log.Fatal("NUI", "Hide CheckIcon");
                    checkIcon.Opacity = 1.0f;
                }
            }

            UpdateMainTextFontStyleByState();
            PlaySelectedAnimation();
        }

        /// <summary>
        /// play selected animation
        /// </summary>
        private void PlaySelectedAnimation()
        {
            Tizen.Log.Fatal("NUI", "Play Selected Animation, isSelected = " + isSelected);
            if (isSelected)
            {
                if (selectedOutAni != null && selectedOutAni.State == Animation.States.Playing)
                {
                    selectedOutAni.Stop();
                }

                if (selectedInAni != null)
                {
                    selectedInAni.Play();
                }
            }
            else
            {
                if (selectedInAni != null && selectedInAni.State == Animation.States.Playing)
                {
                    selectedInAni.Stop();
                }

                if (selectedOutAni != null)
                {
                    selectedOutAni.Play();
                }
            }
        }

        /// <summary>
        /// update enabled state
        /// </summary>
        private void UpdateEnabledState()
        {
            Tizen.Log.Fatal("NUI", "Update Enabled state");
            UpdateMainTextFontStyleByState();
            PlayEnableAnimation();
        }

        /// <summary>
        /// Play Enable Animation
        /// </summary>
        private void PlayEnableAnimation()
        {
            Tizen.Log.Fatal("NUI", "Play Enabled Animation");
            if (isEnabled)
            {
                if (dimInAni != null && dimInAni.State == Animation.States.Playing)
                {
                    dimInAni.Stop();
                }

                if (dimOutAni != null)
                {
                    dimOutAni.Play();
                }
            }
            else
            {
                if (dimOutAni != null && dimOutAni.State == Animation.States.Playing)
                {
                    dimOutAni.Stop();
                }

                if (dimInAni != null)
                {
                    dimInAni.Play();
                }
            }
        }

        /// <summary>
        /// update the font style of the main text
        /// </summary>
        private void UpdateMainTextFontStyleByState()
        {
            if (mainText == null)
            {
                return;
            }

            Tizen.Log.Fatal("NUI", "Update MainText FontStyle");
            ApplyMainTextFontStyle();
        }

        /// <summary>
        /// Apply the position of the checkIcon
        /// </summary>
        private void ApplyCheckIconPosition()
        {
            if (checkIcon == null)
            {
                return;
            }

            Tizen.Log.Fatal("NUI", "Apply CheckIcon Position");

            float checkIconX = SizeWidth - 20.0f - 26.0f;
            float checkIconY = (SizeHeight - 26.0f) / 2.0f;
            checkIcon.Position = new Position(checkIconX, checkIconY, 0);
        }

        /// <summary>
        /// The focused state
        /// </summary>
        internal enum FocusedState
        {
            /// <summary>
            /// Normal
            /// </summary>
            Normal,
            /// <summary>
            /// Focused
            /// </summary>
            Focused
        }

        // The root view object.
        private View rootView = null;
        // The background view object.
        private ImageView backgroundView = null;
        // The main text object.
        private TextLabel mainText = null;
        // The check icon view object.
        private ImageView checkIcon = null;
        // The list line view object.
        private ImageView listLineView = null;
        // The focused state count.
        private const int focusedStateCount = 2;
        // The state that is focused.
        private bool isFocused = false;
        // The state that is selected.
        private bool isSelected = false;
        // The state that is enabled.
        private bool isEnabled = true;
        // The focusIn animation object.
        private Animation focusInAni = null;
        // The focusOut animation object.
        private Animation focusOutAni = null;
        // The dimIn animation object.
        private Animation dimInAni = null;
        // The dimOut animation object.
        private Animation dimOutAni = null;
        // The selectedIn animation object.
        private Animation selectedInAni = null;
        // The selectedOut animation object.
        private Animation selectedOutAni = null;
        // The content of the MainText.
        private string mainTextContent = null;
        // The current width of the MainText.
        private float mainTextWidth = 0;
        // The current Pos_x of the MainText.
        private float mainTextPosX = 0;
        // The state that ListLine is enabled or not.
        private bool isListLineEnabled = false;
        // The enable scroll flag.
        private bool enableScroll = false;
    }
}