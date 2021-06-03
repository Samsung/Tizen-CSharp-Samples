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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

/// <summary>
/// namespace for channel list sample
/// </summary>
namespace ChannelList
{
    /// <summary>
    /// All channel list Item class.
    /// </summary>
    public class ListItem : View
    {
        /// <summary>
        /// The Constructor.
        /// </summary>
        public ListItem()
        {
            LoadItem();
        }

        /// <summary>
        /// Get/Set the program index of all channel list.
        /// </summary>
        public string ProgramIndex
        {
            get
            {
                return programIndexText.Text;
            }

            set
            {
                programIndexText.Text = value;
            }
        }

        /// <summary>
        /// Get/Set the program text of all channel list.
        /// </summary>
        public string ProgramText
        {
            get
            {
                return programText.Text;
            }

            set
            {
                programText.Text = value;
            }
        }

        /// <summary>
        /// Get/Set the channel text of all channal list.
        /// </summary>
        public string ChannelText
        {
            get
            {
                return channelText.Text;
            }

            set
            {
                channelText.Text = value;
            }
        }

        /// <summary>
        /// Set the all channel list focus.
        /// </summary>
        /// <param name="flagFocused">The focus flag.</param>
        public void Focus(bool flagFocused)
        {
            if (flagFocused)
            {
                // weight : 200
                programIndexText.Opacity = 0.95f;
                programText.Opacity = 0.95f;
                channelText.Opacity = 0.95f;
                focusImage.Opacity = 1;
                focusView.Opacity = 0.1f;
                if (programText.NaturalSize2D.Width > programText.SizeWidth)
                {
                    programText.EnableAutoScroll = true;
                }
            }
            else
            {
                programIndexText.Opacity = 0.85f;
                programText.Opacity = 0.6f;
                channelText.Opacity = 0.6f;
                Animation animation = new Animation(0);
                animation.AnimateTo(programIndexText, "colorAlpha", 0.85f);
                animation.AnimateTo(programText, "colorAlpha", 0.6f);
                animation.AnimateTo(channelText, "colorAlpha", 0.6f);
                animation.AnimateTo(focusView, "colorAlpha", 0);
                animation.AnimateTo(focusImage, "colorAlpha", 0);
                animation.EndAction = Animation.EndActions.StopFinal;
                animation.Play();
                programText.EnableAutoScroll = false;
                //animation.Finished += (obj, e) =>
                //{
                //    programIndexText.Opacity = 0.85f;
                //    programText.Opacity = 0.6f;
                //    channelText.Opacity = 0.6f;
                //    focusView.Opacity = 0;
                //    focusImage.Opacity = 0;
                //};
                //animation.ProgressReached += (obj, e) =>
                //{
                //};
            }
        }

        /// <summary>
        /// Set the playing program style of all channel list.
        /// </summary>
        /// <param name="flagPlay">The play flag.</param>
        public void Play(bool flagPlay)
        {
            if (flagPlay)
            {
                programIndexText.TextColor = new Color(62.0f / 255.0f, 174.0f / 255.0f, 254.0f / 255.0f, 1.0f);
                programText.TextColor = new Color(62.0f / 255.0f, 174.0f / 255.0f, 254.0f / 255.0f, 1.0f);
                channelText.TextColor = new Color(62.0f / 255.0f, 174.0f / 255.0f, 254.0f / 255.0f, 1.0f);
                ShowIcon(5);
                // CreateIcon(3, recordingImage);
            }
            else
            {
                programIndexText.TextColor = new Vector4(1, 1, 1, 1);
                programText.TextColor = new Vector4(1, 1, 1, 1);
                channelText.TextColor = new Vector4(1, 1, 1, 1);
                iconContentLayout.RemoveChildAt(new TableView.CellPosition(0, 5));
            }
        }

        /// <summary>
        /// Show the program icon at the specified index.
        /// </summary>
        /// <param name="index">The program index.</param>
        public void ShowIcon(uint index)
        {
            string imageurl = "";
            switch (index)
            {
                case 0: imageurl = onImage; break;
                case 1: imageurl = scrambledImage; break;
                case 2: imageurl = lockImage; break;
                case 3: imageurl = favoriteImage; break;
                case 4: imageurl = recordingImage; break;
                case 5: imageurl = nowplayingImage; break;
            }

            icons[index] = new ImageView(imageurl);
            icons[index].SizeWidth = windowSize.Width * 0.015625f;
            icons[index].SizeHeight = windowSize.Height * 0.027777f;
            iconContentLayout.AddChild(icons[index], new TableView.CellPosition(0, index));
        }

        private void LoadItem()
        {
            // this.BackgroundColor = new Vector4(0, 8.0f / 255.0f, 12.0f / 255.0f, 0.95f);

            programIndexText = new TextLabel();
            programIndexText.SizeWidth = windowSize.Width * 0.072916f;
            programIndexText.SizeHeight = windowSize.Height * 0.061851f;//0.051851f
            programIndexText.PositionUsesPivotPoint = true;
            programIndexText.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
            programIndexText.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
            programIndexText.Position = new Position(windowSize.Width * 0.009375f, 0, 0);
            programIndexText.VerticalAlignment = VerticalAlignment.Center;
            programIndexText.HorizontalAlignment = HorizontalAlignment.Begin;
            //programIndexText.PointSize = 10.0f;
            programIndexText.PointSize = DeviceCheck.PointSize10;
            //programIndexText.PointSize = 42.0f;
            programIndexText.TextColor = new Vector4(1, 1, 1, 1);
            programIndexText.Opacity = 0.85f;
            programIndexText.FontFamily = "SamsungOne 300";

            programText = new TextLabel();
            programText.SizeWidth = windowSize.Width * (0.323958f - 0.093229f - 0.011458f);
            programText.SizeHeight = windowSize.Height * 0.051851f;
            programText.PositionUsesPivotPoint = true;
            programText.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            programText.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            programText.Position = new Position(windowSize.Width * 0.093229f, windowSize.Height * 0.017592f, 0);
            programText.VerticalAlignment = VerticalAlignment.Center;
            programText.HorizontalAlignment = HorizontalAlignment.Begin;
            //programText.PointSize = 8.0f;
            programText.PointSize = DeviceCheck.PointSize8;
            //programText.PointSize = 34.0f;
            programText.TextColor = new Vector4(1, 1, 1, 1);
            programText.Opacity = 0.6f;
            programText.FontFamily = "SamsungOne 300";
            programText.AutoScrollGap = 50;
            programText.AutoScrollSpeed = 100;
            programText.AutoScrollStopMode = AutoScrollStopMode.Immediate;

            channelText = new TextLabel();
            channelText.SizeWidth = windowSize.Width * (0.323958f - 0.093229f - 0.018229f - 0.015625f * 6) - 2 * 6;
            channelText.SizeHeight = windowSize.Height * 0.041481f; //0.031481f
            channelText.PositionUsesPivotPoint = true;
            channelText.PivotPoint = Tizen.NUI.PivotPoint.BottomLeft;
            channelText.ParentOrigin = Tizen.NUI.ParentOrigin.BottomLeft;
            channelText.Position = new Position(windowSize.Width * 0.093229f, -windowSize.Height * 0.017592f, 0);
            channelText.VerticalAlignment = VerticalAlignment.Center;
            channelText.HorizontalAlignment = HorizontalAlignment.Begin;
            //channelText.PointSize = 6.0f;
            channelText.PointSize = DeviceCheck.PointSize6;
            //channelText.PointSize = 26.0f;
            // channelText.TextColor = new Vector4(58.0f / 255.0f, 174.0f / 255.0f, 254.0f / 255.0f, 1);//#3aaef6
            channelText.TextColor = new Color(1, 1, 1, 1);
            channelText.Opacity = 0.6f;
            channelText.FontFamily = "SamsungOne 300";

            iconContentLayout = new TableView(1, 6);
            iconContentLayout.WidthResizePolicy = ResizePolicyType.FitToChildren;
            iconContentLayout.HeightResizePolicy = ResizePolicyType.FitToChildren;
            iconContentLayout.PositionUsesPivotPoint = true;
            iconContentLayout.PivotPoint = Tizen.NUI.PivotPoint.BottomRight;
            iconContentLayout.ParentOrigin = Tizen.NUI.PivotPoint.BottomRight;
            iconContentLayout.Position = new Position(-windowSize.Width * 0.018229f, -windowSize.Height * 0.024074f, 0);
            icons = new ImageView[6];
            for (uint i = 0; i < 6; i++)
            {
                iconContentLayout.SetFitWidth(i);
                // iconContentLayout.SetCellAlignment(new TableView.CellPosition(i, 0), HorizontalAlignmentType.Right,  VerticalAlignmentType.Center);
            }

            iconContentLayout.SetFitHeight(0);

            focusImage = new ImageView(highlightStrokeImage);
            focusImage.WidthResizePolicy = ResizePolicyType.FillToParent;
            focusImage.HeightResizePolicy = ResizePolicyType.FillToParent;
            focusImage.PositionUsesPivotPoint = true;
            focusImage.PivotPoint = Tizen.NUI.PivotPoint.Center;
            focusImage.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            focusImage.Opacity = 0;

            focusView = new View();
            focusView.BackgroundColor = new Color(1, 1, 1, 1f);
            focusView.WidthResizePolicy = ResizePolicyType.FillToParent;
            focusView.HeightResizePolicy = ResizePolicyType.FillToParent;
            focusView.Opacity = 0;

            this.Add(programIndexText);
            this.Add(programText);
            this.Add(channelText);
            this.Add(iconContentLayout);
            this.Add(focusImage);
            this.Add(focusView);
        }

        private TextLabel programIndexText; //Program index text.
        private TextLabel programText; // Program text.
        private TextLabel channelText; // Channel text.
        private ImageView focusImage; //Focus image.
        private View focusView; //Focus view.
        private TableView iconContentLayout; // icon content layout.
        private ImageView[] icons; // icons.
        private Size2D windowSize = Window.Instance.Size; //window size.
        private static string resourcesPrinciple = "/home/owner/apps_rw/org.tizen.example.ChannelList/res/images/principle/"; // principle resource.
        private static string resourcesChannel = "/home/owner/apps_rw/org.tizen.example.ChannelList/res/images/channelList/"; // channel resource
        private string highlightStrokeImage = resourcesPrinciple + "COMMON/HIGHLIGHT_BLACK/stroke/highlight_stroke.9.png"; //high light stroke image
        private string nowplayingImage = resourcesChannel + "channel_list_icon_nowplaying_2.png"; // now playing image.
        private string recordingImage = resourcesChannel + "channel_list_icon_recording_2.png"; //recording image
        private string lockImage = resourcesChannel + "channel_list_icon_lock_2.png"; // lock image
        private string favoriteImage = resourcesChannel + "channel_list_icon_favorite_2.png"; //fovorite image
        private string scrambledImage = resourcesChannel + "channel_list_icon_scrambled_2.png"; //scrambled image
        private string onImage = resourcesChannel + "channel_list_icon_dynamic_on_2.png"; // on image.
        private string offImage = resourcesChannel + "channel_list_icon_dynamic_off_2.png"; // off image.
    }

    /// <summary>
    /// SubList item class.
    /// </summary>
    public class SubListItem : View
    {
        /// <summary>
        /// The Consturctor.
        /// </summary>
        public SubListItem()
        {
            LoadItem();
        }

        /// <summary>
        /// Get/Set the sublist icon url.
        /// </summary>
        public string IconUrl
        {
            set
            {
                icon.ResourceUrl = value;
            }

            get
            {
                return icon.ResourceUrl;
            }
        }

        /// <summary>
        /// Get/Set the sublist text.
        /// </summary>
        public string Text
        {
            set
            {
                text.Text = value;
            }

            get
            {
                return text.Text;
            }
        }

        /// <summary>
        /// Set the sub list focus.
        /// </summary>
        /// <param name="flagFocused">Focus flag.</param>
        public void Focus(bool flagFocused)
        {
            if (flagFocused)
            {
                focusImage.Opacity = 1;
                focusView.Opacity = 0.1f;
            }
            else
            {
                Animation animation = new Animation(0);
                animation.AnimateTo(focusView, "colorAlpha", 0);
                animation.AnimateTo(focusImage, "colorAlpha", 0);
                animation.EndAction = Animation.EndActions.StopFinal;
                animation.Play();
            }
        }

        /// <summary>
        /// Set the sublist icon opacity.
        /// </summary>
        /// <param name="flagShow">The sublist icon opacity flag.</param>
        public void IconOpacity(bool flagShow)
        {
            if (flagShow)
            {
                icon.Opacity = 1;
            }
            else
            {
                icon.Opacity = 0.7f;
            }
        }

        /// <summary>
        /// Set the selected text style of the sublist.
        /// </summary>
        /// <param name="flagSelected">The selected flag.</param>
        public void SelectedText(bool flagSelected)
        {
            if (flagSelected)
            {
                text.TextColor = new Vector4(58.0f / 255.0f, 174.0f / 255.0f, 254.0f / 255.0f, 1);
            }
            else
            {
                text.TextColor = new Vector4(1, 1, 1, 1);
            }
        }

        private void LoadItem()
        {
            // this.BackgroundColor = new Vector4(8.0f / 255.0f, 12.0f / 255.0f, 15.0f / 255.0f, 0.95f);

            icon = new ImageView();
            icon.SizeWidth = windowSize.Width * 0.041666f;
            icon.SizeHeight = windowSize.Height * 0.074074f;
            icon.PositionUsesPivotPoint = true;
            icon.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
            icon.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
            icon.Position = new Position(windowSize.Width * 0.010416f, 0, 0);
            icon.Opacity = 0.7f; //Close: 0.7 Open:1

            text = new TextLabel();
            text.SizeWidth = windowSize.Width * 0.135416f;
            text.SizeHeight = windowSize.Height * 0.069444f;
            text.PositionUsesPivotPoint = true;
            text.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
            text.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
            text.Position = new Position(windowSize.Width * (0.010416f * 2 + 0.041666f), 0, 0);
            text.VerticalAlignment = VerticalAlignment.Center;
            text.HorizontalAlignment = HorizontalAlignment.Begin;
            //text.PointSize = 8.0f;
            text.PointSize = DeviceCheck.PointSize8;
            //text.PointSize = 38.0f;
            text.TextColor = new Vector4(1, 1, 1, 1);
            text.FontFamily = "SamsungOne 300";

            focusImage = new ImageView(highlightStrokeImage);
            focusImage.WidthResizePolicy = ResizePolicyType.FillToParent;
            focusImage.HeightResizePolicy = ResizePolicyType.FillToParent;
            focusImage.PositionUsesPivotPoint = true;
            focusImage.PivotPoint = Tizen.NUI.PivotPoint.Center;
            focusImage.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            focusImage.Opacity = 0;

            focusView = new View();
            focusView.BackgroundColor = new Color(1, 1, 1, 1f);
            focusView.WidthResizePolicy = ResizePolicyType.FillToParent;
            focusView.HeightResizePolicy = ResizePolicyType.FillToParent;
            focusView.Opacity = 0;

            this.Add(icon);
            this.Add(text);
            this.Add(focusImage);
            this.Add(focusView);
        }

        private ImageView icon; //sublist item icon
        private TextLabel text; //sublist item text.
        private View focusView; //sublist item focus view.
        private ImageView focusImage; //sublist item focus image.
        private Size2D windowSize = Window.Instance.Size; //window size.
        private static string resourcesPrinciple = "/home/owner/apps_rw/org.tizen.example.ChannelList/res/images/principle/"; //principle resource path.
        private string highlightStrokeImage = resourcesPrinciple + "COMMON/HIGHLIGHT_BLACK/stroke/highlight_stroke.9.png"; //high light stroke image.
    }

    /// <summary>
    /// Favorite list item class.
    /// </summary>
    public class SelectListItem : View
    {
        /// <summary>
        /// The Constructor
        /// </summary>
        public SelectListItem()
        {
            LoadItem();
        }

        /// <summary>
        /// Get/Set the favorite list text.
        /// </summary>
        public string FavoriteText
        {
            set
            {
                favoriteText.Text = value;
            }

            get
            {
                return favoriteText.Text;
            }
        }

        /// <summary>
        /// Get/Set the favorite list num.
        /// </summary>
        public int FavoriteNum
        {
            set
            {
                favoriteNum = value;
                if (favoriteNum < 1)
                {
                    channelNumText.Text = "Empty";
                }
                else if (favoriteNum == 1)
                {
                    channelNumText.Text = favoriteNum + " channels";
                }
                else
                {
                    channelNumText.Text = favoriteNum + " channel";
                }

            }

            get
            {
                return favoriteNum;
            }
        }

        /// <summary>
        /// Set the favorite list focus.
        /// </summary>
        /// <param name="flagFocused">The focus flag.</param>
        public void Focus(bool flagFocused)
        {
            if (flagFocused)
            {
                focusImage.Opacity = 1;
                focusView.Opacity = 0.1f;
            }
            else
            {
                Animation animation = new Animation(166);//166
                animation.AnimateTo(focusView, "colorAlpha", 0);
                animation.AnimateTo(focusImage, "colorAlpha", 0);
                animation.EndAction = Animation.EndActions.StopFinal;
                animation.Play();
            }
        }

        /// <summary>
        /// Set the select text style of favorite list.
        /// </summary>
        /// <param name="flagSelected">The selected flag.</param>
        public void Select(bool flagSelected)
        {
            if (flagSelected)
            {
                favoriteText.TextColor = new Color(62.0f / 255.0f, 174.0f / 255.0f, 254.0f / 255.0f, 1.0f);
                channelNumText.TextColor = new Color(62.0f / 255.0f, 174.0f / 255.0f, 254.0f / 255.0f, 1.0f);
            }
            else
            {
                favoriteText.TextColor = new Color(1, 1, 1, 1);
                channelNumText.TextColor = new Color(1, 1, 1, 1);
            }
        }

        private void LoadItem()
        {
            this.Opacity = 0.95f;

            favoriteText = new TextLabel();
            favoriteText.SizeWidth = windowSize.Width * 0.302083f;
            favoriteText.SizeHeight = windowSize.Height * 0.060925f;//0.050925f
            favoriteText.PositionUsesPivotPoint = true;
            favoriteText.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            favoriteText.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            favoriteText.Position = new Position(windowSize.Width * 0.021875f, windowSize.Height * 0.010416f, 0);
            favoriteText.VerticalAlignment = VerticalAlignment.Center;
            favoriteText.HorizontalAlignment = HorizontalAlignment.Begin;
            //favoriteText.PointSize = 8.0f;
            favoriteText.PointSize = DeviceCheck.PointSize8;
            //favoriteText.PointSize = 38.0f;
            favoriteText.TextColor = new Vector4(1, 1, 1, 1);
            favoriteText.FontFamily = "SamsungOne 300";

            channelNumText = new TextLabel();
            channelNumText.SizeWidth = windowSize.Width * 0.302083f;
            channelNumText.SizeHeight = windowSize.Height * 0.047037f;//0.037037f
            channelNumText.PositionUsesPivotPoint = true;
            channelNumText.PivotPoint = Tizen.NUI.PivotPoint.BottomLeft;
            channelNumText.ParentOrigin = Tizen.NUI.ParentOrigin.BottomLeft;
            channelNumText.Position = new Position(windowSize.Width * 0.021875f, -windowSize.Height * 0.010416f, 0);
            channelNumText.VerticalAlignment = VerticalAlignment.Center;
            channelNumText.HorizontalAlignment = HorizontalAlignment.Begin;
            //channelNumText.PointSize = 6.0f;
            channelNumText.PointSize = DeviceCheck.PointSize6;
            //channelNumText.PointSize = 30.0f;
            channelNumText.TextColor = new Vector4(1, 1, 1, 1);
            channelNumText.Opacity = 0.5f;
            channelNumText.FontFamily = "SamsungOne 300";

            focusImage = new ImageView(highlightStrokeImage);
            focusImage.WidthResizePolicy = ResizePolicyType.FillToParent;
            focusImage.HeightResizePolicy = ResizePolicyType.FillToParent;
            focusImage.PositionUsesPivotPoint = true;
            focusImage.PivotPoint = Tizen.NUI.PivotPoint.Center;
            focusImage.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            focusImage.Opacity = 0;

            focusView = new View();
            focusView.BackgroundColor = new Color(1, 1, 1, 1f);
            focusView.WidthResizePolicy = ResizePolicyType.FillToParent;
            focusView.HeightResizePolicy = ResizePolicyType.FillToParent;
            focusView.Opacity = 0;

            this.Add(favoriteText);
            this.Add(channelNumText);
            this.Add(focusImage);
            this.Add(focusView);
        }

        private TextLabel favoriteText; // favorite item text.
        private TextLabel channelNumText; // favorite channel number text.
        private View focusView; // favorite item focus view.
        private ImageView focusImage; // favorite item focus image.
        private int favoriteNum; //favorite number.
        private Size2D windowSize = Window.Instance.Size; //window size.
        private static string resourcesPrinciple = "/home/owner/apps_rw/org.tizen.example.ChannelList/res/images/principle/"; //principle resource path
        private string highlightStrokeImage = resourcesPrinciple + "COMMON/HIGHLIGHT_BLACK/stroke/highlight_stroke.9.png"; // high light stroke image.
    }

    /// <summary>
    /// Genre List class.
    /// </summary>
    public class GenreListItem : View
    {
        /// <summary>
        /// The Constructor
        /// </summary>
        public GenreListItem()
        {
            LoadItem();
        }

        /// <summary>
        /// Get/Set the genre text.
        /// </summary>
        public string Text
        {
            set
            {
                text.Text = value;
            }

            get
            {
                return text.Text;
            }
        }

        /// <summary>
        /// Set the focus effect of genre list.
        /// </summary>
        /// <param name="flagFocused">The focus flag.</param>
        public void Focus(bool flagFocused)
        {
            if (flagFocused)
            {
                focusImage.Opacity = 1;
                focusView.Opacity = 0.1f;
            }
            else
            {
                Animation animation = new Animation(166);//166
                animation.AnimateTo(focusView, "colorAlpha", 0);
                animation.AnimateTo(focusImage, "colorAlpha", 0);
                animation.EndAction = Animation.EndActions.StopFinal;
                animation.Play();
            }
        }

        /// <summary>
        /// Set the select style of genre list.
        /// </summary>
        /// <param name="flagSelected">The selected flag.</param>
        public void Select(bool flagSelected)
        {
            if (flagSelected)
            {
                text.TextColor = new Color(62.0f / 255.0f, 174.0f / 255.0f, 254.0f / 255.0f, 1.0f);
            }
            else
            {
                text.TextColor = new Color(1, 1, 1, 1);
            }
        }

        private void LoadItem()
        {
            this.Opacity = 0.95f;

            text = new TextLabel();
            text.SizeWidth = windowSize.Width * 0.092592f;
            text.SizeHeight = windowSize.Height * 0.092592f;//0.050925f
            text.PositionUsesPivotPoint = true;
            text.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            text.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            text.Position = new Position(windowSize.Width * 0.021875f, 0, 0);
            text.VerticalAlignment = VerticalAlignment.Center;
            text.HorizontalAlignment = HorizontalAlignment.Begin;
            //text.PointSize = 8.0f;
            text.PointSize = DeviceCheck.PointSize8;
            //text.PointSize = 38.0f;
            text.TextColor = new Vector4(1, 1, 1, 1);
            text.FontFamily = "SamsungOne 300";

            focusImage = new ImageView(highlightStrokeImage);
            focusImage.WidthResizePolicy = ResizePolicyType.FillToParent;
            focusImage.HeightResizePolicy = ResizePolicyType.FillToParent;
            focusImage.PositionUsesPivotPoint = true;
            focusImage.PivotPoint = Tizen.NUI.PivotPoint.Center;
            focusImage.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            focusImage.Opacity = 0;

            focusView = new View();
            focusView.BackgroundColor = new Color(1, 1, 1, 1f);
            focusView.WidthResizePolicy = ResizePolicyType.FillToParent;
            focusView.HeightResizePolicy = ResizePolicyType.FillToParent;
            focusView.Opacity = 0;

            this.Add(text);
            this.Add(focusImage);
            this.Add(focusView);
        }

        private TextLabel text; //Genre item text.
        private View focusView; //Genre item focus view.
        private ImageView focusImage; //Genre item focus image.
        private Size2D windowSize = Window.Instance.Size; //window size.
        private static string resourcesPrinciple = "/home/owner/apps_rw/org.tizen.example.ChannelList/res/images/principle/"; //principle resource path.
        private string highlightStrokeImage = resourcesPrinciple + "COMMON/HIGHLIGHT_BLACK/stroke/highlight_stroke.9.png"; // high light stroke image.
    }
}