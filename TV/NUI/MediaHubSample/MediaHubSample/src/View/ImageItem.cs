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
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;

/// <summary>
/// Namespace for Tizen.TV.NUI package.
/// </summary>
namespace Tizen.NUI.MediaHub
{
    /// <summary>
    /// The ImageItem class of tv nui component. 
    /// It deliver information in an image format and can be independently used with additional information
    /// </summary>
    /// <code>
    /// ImageItem imageItem = new NUI.ImageItem();
    /// imageItem.MainImageURL = @"/home/owner/apps_rw/org.tizen.example.MediaHubSample/res/images/gallery-3.jpg";
    /// imageItem.MainText = "MainText";
    /// imageItem.SubImageURL = @"/home/owner/apps_rw/org.tizen.example.MediaHubSample/res/images/image-1.jpg";
    /// string[] inforIconURLArray = new string[4];
    /// inforIconURLArray[0] = @"/home/owner/apps_rw/org.tizen.example.MediaHubSample/res/image/icon/r_icon_thumbnailinfo/r_icon_thumbnailinfo_ok.png";
    /// inforIconURLArray[1] = @"/home/owner/apps_rw/org.tizen.example.MediaHubSample/res/image/icon/r_icon_thumbnailinfo/r_icon_thumbnailinfo_lock.png";
    /// inforIconURLArray[2] = @"/home/owner/apps_rw/org.tizen.example.MediaHubSample/res/image/icon/r_icon_thumbnailinfo/r_icon_thumbnailinfo_sound.png";
    /// inforIconURLArray[3] = @"/home/owner/apps_rw/org.tizen.example.MediaHubSample/res/image/icon/r_icon_thumbnailinfo/r_icon_thumbnailinfo_play.png";
    /// imageItem.InformationIconURLArray = inforIconURLArray;
    /// imageItem.Size = new Size(350, 300, 0);
    /// imageItem.Position = new Position(300, 400, 0);
    /// imageItem.PivotPoint = PivotPoint.Center;
    /// </code>
    public class ImageItem : View
    {

        /// <summary>
        /// Constructor of the ImageItem class with special style.
        /// </summary>
        public ImageItem() : base()
        {
            Initialize();

            Relayout += OnRelayoutUpdate;
        }

        /// <summary>
        /// The property of the state that the ImageItem is focused or not.
        /// </summary>
        public bool StateFocused
        {
            get
            {
                return isFocused;
            }

            set
            {
                if (isFocused == value)
                {
                    return;
                }

                isFocused = value;
                UpdateFocusedState();
            }
        }

        /// <summary>
        /// The property of the state that the ImageItem is selected or not.
        /// </summary>
        public bool StateSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                if (isSelected == value)
                {
                    return;
                }

                isSelected = value;
                UpdateSelectedState();
            }
        }

        /// <summary>
        /// The property of the state that the ImageItem is enabled or not.
        /// </summary>
        public bool StateEnabled
        {
            get
            {
                return isEnabled;
            }

            set
            {
                if (isEnabled == value)
                {
                    return;
                }

                isEnabled = value;
                UpdateEnabledState();
            }
        }

        /// <summary>
        /// The property of the state that the ImageItem is editabled or not.
        /// </summary>
        public bool StateEditabled
        {
            get
            {
                return isEditabled;
            }

            set
            {
                if (isEditabled == value)
                {
                    return;
                }

                isEditabled = value;
            }
        }

        /// <summary>
        /// The property of main image url.
        /// </summary>
        public string MainImageURL
        {
            get
            {
                return mainImageURL;
            }

            set
            {
                mainImageURL = value;
                if (mainImage != null)
                {
                    mainImage.SetImage(mainImageURL);
                }
            }
        }

        /// <summary>
        /// The property of main text value.
        /// </summary>
        public string MainText
        {
            get
            {
                return mainTextContent;
            }

            set
            {
                mainTextContent = value;
                if (mainText != null)
                {
                    mainText.Text = mainTextContent;
                }

                UpdateMainTextScroll();
            }
        }

        /// <summary>
        /// The property of the Information icon URL array.
        /// </summary>
        public string[] InformationIconURLArray
        {
            get
            {
                return informationIconURLArray;
            }

            set
            {
                if (value == null)
                {
                    if (informationIconArray != null)
                    {
                        int iconArrayLength = informationIconArray.Length;
                        if (iconArrayLength > 0)
                        {
                            for (int i = 0; i < iconArrayLength; i++)
                            {
                                rootView.Remove(informationIconArray[i]);
                                informationIconArray[i].Dispose();
                            }
                        }

                        informationIconArray = null;
                    }
                }
                else
                {
                    int length = value.Length;
                    if (length > informationIconArrayMaxCount)
                    {
                        length = informationIconArrayMaxCount;
                    }

                    informationIconURLArray = new string[length];
                    for (int i = 0; i < length; i++)
                    {
                        informationIconURLArray[i] = value[i];
                    }

                    InitializeInformationIcon();
                    ApplyInformationIcon();
                }
            }
        }

        /// <summary>
        /// Dispose ImageItem.
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
                    rootView?.Remove(mainText);
                    mainText.Dispose();
                    mainText = null;
                }
                //Dispose the main image
                if (mainImage != null)
                {
                    rootView?.Remove(mainImage);
                    mainImage.Dispose();
                    mainImage = null;
                }
                //Dispose the information icons
                if (informationIconArray != null)
                {
                    for (int i = 0; i < informationIconArray.Length; i++)
                    {
                        rootView?.Remove(informationIconArray[i]);
                        informationIconArray[i].Dispose();
                        informationIconArray[i] = null;
                    }

                    informationIconArray = null;
                }
                //Dispose the root view
                if (rootView != null)
                {
                    Remove(rootView);
                    rootView.Dispose();
                    rootView = null;
                }
                //Dispose the background image
                if (backgroundImage != null)
                {
                    Remove(backgroundImage);
                    backgroundImage.Dispose();
                    backgroundImage = null;
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
        /// Callback of Relayout event
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event args</param>
        protected void OnRelayoutUpdate(object sender, EventArgs e)
        {
            float width = Size2D.Width;
            float height = Size2D.Height;
            ApplyAttributes();
        }

        /// <summary>
        /// Initialize
        /// </summary>
        private void Initialize()
        {
            InitializeBGView();
            InitializeRootView();
            InitializeMainImage();
            InitializeMainText();
            InitializeAnimation();
        }

        /// <summary>
        /// The method is to initialize the background image
        /// </summary>
        private void InitializeBGView()
        {
            backgroundImage = new ImageView();
            backgroundImage.WidthResizePolicy = ResizePolicyType.Fixed;
            backgroundImage.HeightResizePolicy = ResizePolicyType.Fixed;
            backgroundImage.PositionUsesPivotPoint = true;
            backgroundImage.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            backgroundImage.PivotPoint = Tizen.NUI.PivotPoint.Center;
            Add(backgroundImage);
            backgroundImage.LowerToBottom();
        }

        /// <summary>
        /// The method is to initialize the root view
        /// </summary>
        private void InitializeRootView()
        {
            rootView = new View();
            rootView.WidthResizePolicy = ResizePolicyType.Fixed;
            rootView.HeightResizePolicy = ResizePolicyType.Fixed;
            rootView.PositionUsesPivotPoint = true;
            rootView.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            rootView.PivotPoint = Tizen.NUI.PivotPoint.Center;
            Add(rootView);
            rootSize = new Size(0, 0, 0);
        }

        /// <summary>
        /// The method is to initialize the main text
        /// </summary>
        private void InitializeMainText()
        {
            mainText = new TextLabel();
            mainText.Size2D = new Size2D(10, 10);
            mainText.WidthResizePolicy = ResizePolicyType.Fixed;
            mainText.HeightResizePolicy = ResizePolicyType.Fixed;
            mainText.PositionUsesPivotPoint = true;
            mainText.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            mainText.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            mainText.RaiseToTop();
            rootView.Add(mainText);
        }

        /// <summary>
        /// The method is to initialize the main image
        /// </summary>
        private void InitializeMainImage()
        {
            if (mainImage == null)
            {
                mainImage = new ImageView();
                mainImage.WidthResizePolicy = ResizePolicyType.Fixed;
                mainImage.HeightResizePolicy = ResizePolicyType.Fixed;
                mainImage.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
                mainImage.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
                rootView.Add(mainImage);
            }
        }

        /// <summary>
        /// The method is to initialize the information icons
        /// </summary>
        private void InitializeInformationIcon()
        {
            if (informationIconURLArray == null)
            {
                return;
            }

            int urlArrayLength = informationIconURLArray.Length;
            if (urlArrayLength < 0 || urlArrayLength > informationIconArrayMaxCount)
            {
                return;
            }

            int newLength = 0;
            if (informationIconArray == null)
            {
                newLength = urlArrayLength;
            }
            else
            {
                int iconArrayLength = informationIconArray.Length;
                if (iconArrayLength != urlArrayLength)
                {
                    for (int i = 0; i < iconArrayLength; i++)
                    {
                        rootView.Remove(informationIconArray[i]);
                        informationIconArray[i].Dispose();
                    }

                    informationIconArray = null;
                    newLength = urlArrayLength;
                }
            }

            if (newLength <= 0)
            {
                return;
            }

            informationIconArray = new ImageView[newLength];
            for (int i = 0; i < newLength; i++)
            {
                informationIconArray[i] = new ImageView();
                informationIconArray[i].WidthResizePolicy = ResizePolicyType.Fixed;
                informationIconArray[i].HeightResizePolicy = ResizePolicyType.Fixed;
                informationIconArray[i].ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
                informationIconArray[i].PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
                informationIconArray[i].RaiseToTop();
                rootView.Add(informationIconArray[i]);
            }
        }

        /// <summary>
        /// The method is to initialize the animations
        /// </summary>
        private void InitializeAnimation()
        {
            dimInAni = new Animation();
            dimOutAni = new Animation();
        }

        /// <summary>
        /// The method is to apply the properties
        /// </summary>
        private void ApplyAttributes()
        {
            ApplyRootView();
            ApplyBGImage();
            ApplyMainImage();
            ApplyMainText();
            ApplyInformationIcon();
            ApplyAnimation();
        }

        /// <summary>
        /// The method is to apply the properties of root view
        /// </summary>
        private void ApplyRootView()
        {
            if (rootView == null)
            {
                return;
            }

            float thisWidth = Size2D.Width;
            float thisHeight = Size2D.Height;
            float left = 2.0f, top = 2.0f, right = 2.0f, bottom = 2.0f;
            float rootWidth = thisWidth - left - right;
            float rootHeight = thisHeight - top - bottom;
            rootSize = new Size(rootWidth, rootHeight, 0);
            rootView.Size2D = new Size2D((int)rootWidth, (int)rootHeight);
        }

        /// <summary>
        /// The method is to apply the properties of bg view
        /// </summary>
        private void ApplyBGImage()
        {
            if (backgroundImage == null)
            {
                return;
            }
            //Set this according to the ImageItemStyle(should be "C_ImageItem_WhiteSingleline01" style)
            float rootWidth = Size2D.Width;
            float rootHeight = Size2D.Height;
            float left = 0;
            float top = 0;
            float right = 0;
            float bottom = 0;
            string bgImagePath = "";
            if (isFocused)
            {
                left = 5.0f;
                top = 10.0f;
                right = 5.0f;
                bottom = 10.0f;
                bgImagePath = CommonResource.GetLocalReosurceURL() + "component/c_imageitem/c_imageitem_white_bg_focus_singleline_9patch.9.png";
            }
            else
            {
                bgImagePath = CommonResource.GetLocalReosurceURL() + "component/c_imageitem/c_imageitem_white_bg_normal_singleline_9patch.9.png";
                
            }

            backgroundImage.Size2D = new Size2D((int)(rootWidth + left + right), (int)(rootHeight + top + bottom));
            backgroundImage.SetImage(bgImagePath);
        }

        /// <summary>
        /// The method is to apply the properties of main image
        /// </summary>
        private void ApplyMainImage()
        {
            if (mainImage == null)
            {
                return;
            }

            mainImage.Size2D = new Size2D((int)(Size2D.Width - 2.0f * 2), (int)(Size2D.Height - 2.0f * 2 - 60.0f));
            mainImage.Position = new Position(0, 0, 0);

            if (mainImageURL != null)
            {
                mainImage.SetImage(mainImageURL);
            }
        }

        /// <summary>
        /// The method is to apply the properties of main text
        /// </summary>
        private void ApplyMainText()
        {
            if (mainText == null)
            {
                return;
            }

            float rootWidth = rootSize.Width;
            float rootHeight = rootSize.Height;

            mainText.HorizontalAlignment = HorizontalAlignment.Begin;
            mainText.VerticalAlignment = VerticalAlignment.Center;
            Size2D size = new Size2D((int)(Size2D.Width - 2.0f * 2), (int)(Size2D.Height - 2.0f * 2));
            mainText.Size2D = new Size2D((int)(size.Width - 20.0f * 2.0f), 40);
            Tizen.Log.Fatal("NUI", "size, mainTextWidth= " + mainText.Size2D.Width + ", mainTextHeight = " + mainText.Size2D.Height);

            mainText.Position = new Position(20.0f, size.Height - 60.0f + (60.0f - 40.0f) / 2.0f, 0);
            Tizen.Log.Fatal("NUI", "position, x= " + mainText.Position.X + ", y = " + mainText.Position.Y);
            
            if (mainTextContent != null)
            {
                Tizen.Log.Fatal("NUI",  "mainTextContent = " + mainTextContent);
                mainText.Text = mainTextContent;
            }

            UpdateMainTextStyleByState();
        }

        /// <summary>
        /// The method is to apply the properties of information icons
        /// </summary>
        private void ApplyInformationIcon()
        {
            if (informationIconArray == null || informationIconURLArray == null)
            {
                Tizen.Log.Fatal("NUI",  "informationIconArray/informationIconURLArray is null , return");
                return;
            }

            int iconArrayLength = informationIconArray.Length;
            for (int i = 0; i < iconArrayLength; i++)
            {
                if (informationIconArray[i] != null)
                {
                    informationIconArray[i].Size2D = new Size2D(38, 38);
                    //TODO maybe the rootSize is incorrect here:Size rootSize = new Size(newWidth - 2.0f * 2, newHeight - 2.0f * 2, 0);
                    Size2D rootsize = new Size2D(Size2D.Width - 2 * 2, Size2D.Height - 2 * 2);
                    informationIconArray[i].Position = new Position(rootsize.Width - (38.0f + 4.0f) * (i + 1), 4.0f, 0);
                    
                    if (informationIconURLArray[i] != null)
                    {
                        string url = informationIconURLArray[i];
                        if (url != null)
                        {
                            informationIconArray[i].SetImage(url);
                        }
                    }

                    informationIconArray[i].RaiseToTop();
                }
            }
        }

        /// <summary>
        /// The method is to apply the properties of animation
        /// </summary>
        private void ApplyAnimation()
        {
            // focusIn
            if (focusInAni != null)
            {
                focusInAni.Duration = 1100;
                float focusInScale = 1.2f;
                focusInAni.AnimateTo(this, "Scale", new Size(focusInScale, focusInScale, 0));
            }
            // focusOut
            if (focusOutAni != null)
            {
                focusOutAni.Duration = 850;
                float focusOutScale = 1.0f;
                focusOutAni.AnimateTo(this, "Scale", new Size(focusOutScale, focusOutScale, 0));
            }


            // selectedIn
            if (selectedInAni != null)
            {
                selectedInAni.Duration = 850;
                float selectedInScale = 1.0f;
                selectedInAni.AnimateTo(this, "Scale", new Size(selectedInScale, selectedInScale, 0));
            }
            // selectedOut
            if (selectedOutAni != null)
            {
                selectedOutAni.Duration = 1000;
                float selectedOutScale = 1.2f;
                selectedOutAni.AnimateTo(this, "Scale", new Size(selectedOutScale, selectedOutScale, 0));
            }

            // dimIn
            if (dimInAni != null)
            {
                dimInAni.Duration = 333;
                float dimInOpacity = 0.4f;
                dimInAni.AnimateTo(this, "Opacity", dimInOpacity);
                if (!isEnabled)
                {
                    Opacity = dimInOpacity;
                }
            }
            
            // dimout
            if (dimOutAni != null)
            {
                dimOutAni.Duration = 333;
                float dimOutOpacity = 1.0f;
                dimOutAni.AnimateTo(this, "Opacity", dimOutOpacity);
                if (isEnabled)
                {
                    Opacity = dimOutOpacity;
                }
            }
        }

        /// <summary>
        /// Update focused state
        /// </summary>
        private void UpdateFocusedState()
        {
            Tizen.Log.Fatal("NUI",  "isFocused = " + isFocused);
            UpdateTextStyleByState();
            ApplyBGImage();
        }

        /// <summary>
        /// Update selected state
        /// </summary>
        private void UpdateSelectedState()
        {
            Tizen.Log.Fatal("NUI",  "isSelected = " + isSelected);
            UpdateTextStyleByState();
        }

        /// <summary>
        /// Update enabled state
        /// </summary>
        private void UpdateEnabledState()
        {
            Tizen.Log.Fatal("NUI",  "isEnabled = " + isEnabled);
            UpdateTextStyleByState();
            PlayDimAnimation();
        }

        /// <summary>
        /// play focused animation
        /// </summary>
        private void PlayFocosedAnimation()
        {
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
        /// play selected animation
        /// </summary>
        private void PlaySelectedAnimation()
        {
            Tizen.Log.Fatal("NUI",  "isSelected = " + isSelected);
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
        /// Play dim Animation
        /// </summary>
        private void PlayDimAnimation()
        {
            Tizen.Log.Fatal("NUI",  "isEnabled = " + isEnabled);
            if (!isEnabled)
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
            else
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
        }

        private void UpdateSubImageForStyle()
        {
            ApplyMainText();
        }

        private void UpdateTextStyleByState()
        {
            UpdateMainTextStyleByState();
        }

        private void UpdateMainTextStyleByState()
        {
            if (mainText == null)
            {
                return;
            }

            Tizen.Log.Fatal("NUI",  "isFocused = " + isFocused + ", isSelected = "  + isSelected + ", isEnabled = " + isEnabled);
            if (isFocused)
            {
                mainText.TextColor = Utility.Hex2Color(0x000000, 1.0f);
            }
            else if (isFocused && !isEnabled)
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

            mainText.FontFamily = "SamsungOneUI_400";
            //mainText.PointSize = 5;
            mainText.PointSize = DeviceCheck.PointSize5;

            // scroll
            mainText.EnableAutoScroll = false; // enable scroll
            mainText.AutoScrollSpeed = 50; // 50px/1s
            mainText.AutoScrollLoopDelay = 1.0f; // 1s
            mainText.AutoScrollStopMode = AutoScrollStopMode.Immediate; // stop immediate
            mainText.AutoScrollGap = 50.0f; // 50px
            mainText.AutoScrollLoopCount = 0; // infinite count
            UpdateMainTextScroll();
        }

        private void UpdateMainTextScroll()
        {
            if (mainText == null)
            {
                return;
            }

            if (enableScroll)
            {
                if (isFocused)
                {
                    // check the text's natural width is greater than user setting width or not. If yes, enable scroll; else, disable scroll.  
                    float naturalWidth = mainText.NaturalSize2D.Width;
                    float settingWidth = mainText.SizeWidth;
                    Tizen.Log.Fatal("NUI",  "naturalWidth = " + naturalWidth + ", settingWidth = " + settingWidth + ", enableScroll = " + enableScroll);
                    mainText.EnableAutoScroll = true;
                }
                else
                {
                    mainText.EnableAutoScroll = false;
                }
            }
        }

        /// <summary>
        /// Focus state
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

        /// <summary>
        /// Dim image state
        /// </summary>
        internal enum DimImageState
        {
            /// <summary>
            /// Normal
            /// </summary>
            Normal,
            /// <summary>
            /// NormalDim
            /// </summary>
            NormalDim,
            /// <summary>
            /// Focused
            /// </summary>
            Focused
        }
    
        // The root view
        private View rootView = null;           
        // The backgound image view
        private ImageView backgroundImage = null;       
        // The main image.
        private ImageView mainImage = null;
        // The main text
        private TextLabel mainText = null;

        // The information icon array
        private ImageView[] informationIconArray = null;
        // The CheckBox.
        //private CheckBoxButton checkBox = null;
        // The count of the focusedstate
        private const int focusedStateCount = 2;
        // The max count of the information icon
        private const int informationIconArrayMaxCount = 4;

        // The state that is focused.
        private bool isFocused = false;
        // The state that is selected.
        private bool isSelected = false;
        // The state that is disabled.
        private bool isEnabled = true;
        // The state that is editabled.me
        private bool isEditabled = false;
        // The current root view size.
        private Size rootSize = null;
        // The main image url.
        private string mainImageURL = null;
        // The main text content.
        private string mainTextContent = null;
        // The information icon url array.
        private string[] informationIconURLArray = null;
        // The focusIn animation.
        private Animation focusInAni = null;
        // The focusOut animation.
        private Animation focusOutAni = null;
        // The selectedIn animation.
        private Animation selectedInAni = null;
        // The selectedOut animation.
        private Animation selectedOutAni = null;
        // The dimIn animation.
        private Animation dimInAni = null;
        // The dimOut animation.
        private Animation dimOutAni = null;
        // The enable scroll flag.
        private bool enableScroll = true;
    }
}
