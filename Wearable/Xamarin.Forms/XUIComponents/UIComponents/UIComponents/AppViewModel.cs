/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UIComponents.Samples;
using UIComponents.Samples.Background;
using UIComponents.Samples.Button;
using UIComponents.Samples.CircleDateTime;
using UIComponents.Samples.CircleList;
using UIComponents.Samples.CircleScroller;
using UIComponents.Samples.CircleSpinner;
using UIComponents.Samples.Entry;
using UIComponents.Samples.NoContent;
using UIComponents.Samples.PageControl;
using UIComponents.Samples.Popup;

namespace UIComponents
{
    /// <summary>
    /// The model Class for main page of App.xaml.
    /// </summary>
    public class AppViewModel
    {
        /// <summary>
        /// Constructor of AppViewModel class
        /// </summary>
        public AppViewModel()
        {
            Console.WriteLine("AppViewModel()");

            // 1st depth, add Sample instance to Samples ObservableCollection. 
            Samples = new ObservableCollection<Sample>();
            Samples.Add(new Sample{ Title = "Bg", Class = typeof(BackgroundList) });
            Samples.Add(new Sample { Title = "Button", Class = typeof(ButtonList) });
            Samples.Add(new Sample { Title = "Check", Class = typeof(Check) });
            Samples.Add(new Sample { Title = "Entry", Class = typeof(EntryList) });
            Samples.Add(new Sample { Title = "Image", Class = typeof(Image) });
            Samples.Add(new Sample { Title = "PageControl", Class = typeof(PageControlList) });
            Samples.Add(new Sample { Title = "Popup", Class = typeof(PopupList) });
            Samples.Add(new Sample { Title = "Progress", Class = typeof(ProgressBar) });
            Samples.Add(new Sample { Title = "Nocontents", Class = typeof(NoContentList) });
            Samples.Add(new Sample { Title = "Radio", Class = typeof(Radio) });

            Samples.Add(new Sample { Title = "(Circle) Datetime", Class = typeof(CircleDateTimeList) });
            Samples.Add(new Sample { Title = "(Circle) GenList", Class = typeof(CircleGenList) });
            Samples.Add(new Sample { Title = "(Circle) More Option", Class = typeof(MoreOption) });
            Samples.Add(new Sample { Title = "(Circle) ProgressBar", Class = typeof(CircleProgressBar) });
            //Samples.Add(new Sample { Title = "(Circle) Rotary Selector", Class = typeof(RotarySelector) });
            Samples.Add(new Sample { Title = "(Circle) Scroller", Class = typeof(CircleScrollerList) });
            Samples.Add(new Sample { Title = "(Circle) Slider", Class = typeof(CircleSlider) });
            Samples.Add(new Sample { Title = "(Circle) Spinner", Class = typeof(CircleSpinnerList) });

            // 2nd depth
            // Item count of this depth depends on characteristics of the control
            BgSamples = new ObservableCollection<Sample>();
            BgSamples.Add(new Sample { Title = "Solid Color", Class = typeof(SolidColor) });
            BgSamples.Add(new Sample { Title = "Image - CENTER", Class = typeof(ImageCenter) });
            BgSamples.Add(new Sample { Title = "Image - SCALE", Class = typeof(ImageScale) });
            BgSamples.Add(new Sample { Title = "Image - STRETCH", Class = typeof(ImageStretch) });
            BgSamples.Add(new Sample { Title = "Image - TILE", Class = typeof(ImageTile) });

            ButtonSamples = new ObservableCollection<Sample>();
            ButtonSamples.Add(new Sample { Title = "default", Class = typeof(DefaultButton) });
            ButtonSamples.Add(new Sample { Title = "bottom", Class = typeof(BottomButton) });

            EntrySamples = new ObservableCollection<Sample>();
            EntrySamples.Add(new Sample { Title = "Single line entry", Class = typeof(SingleLine) });
            EntrySamples.Add(new Sample { Title = "Password entry", Class = typeof(Password) });

            PageControlSamples = new ObservableCollection<Sample>();
            PageControlSamples.Add(new Sample { Title = "thumbnail", Class = typeof(Thumbnail) });
            PageControlSamples.Add(new Sample { Title = "Circle index", Class = typeof(CircleIndex) });

            PopupSamples = new ObservableCollection<Sample>();
            PopupSamples.Add(new Sample { Title = "text", Class = typeof(TextPopup) });
            PopupSamples.Add(new Sample { Title = "text scrollable", Class = typeof(TextScrollable) });
            PopupSamples.Add(new Sample { Title = "title text", Class = typeof(TitleText) });
            PopupSamples.Add(new Sample { Title = "text 1button", Class = typeof(TitleText1Button) });
            PopupSamples.Add(new Sample { Title = "title text 1button", Class = typeof(TitleText1Button) });
            PopupSamples.Add(new Sample { Title = "title text 2button", Class = typeof(TitleText2Button) });
            PopupSamples.Add(new Sample { Title = "title text check 2button", Class = typeof(TitleTextCheckButton) });
            PopupSamples.Add(new Sample { Title = "Toast", Class = typeof(TextPopup) });
            PopupSamples.Add(new Sample { Title = "Graphic Toast", Class = typeof(TextPopup) });
            PopupSamples.Add(new Sample { Title = "Process Small", Class = typeof(ProcessSmall) });

            NoContentSamples = new ObservableCollection<Sample>();
            NoContentSamples.Add(new Sample { Title = "Nocontent", Class = typeof(Nocontent) });
            NoContentSamples.Add(new Sample { Title = "Title Enabled", Class = typeof(TitleEnabled) });

            DateTimeSamples = new ObservableCollection<Sample>();
            DateTimeSamples.Add(new Sample { Title = "Set date", Class = typeof(CircleDate) });
            DateTimeSamples.Add(new Sample { Title = "Set time", Class = typeof(CircleTime) });

            GenListSamples = new ObservableCollection<Sample>();
            GenListSamples.Add(new Sample { Title = "title", Class = typeof(StyleTitle) });
            GenListSamples.Add(new Sample { Title = "title_with_groupindex", Class = typeof(StyleTitleGroupIndex) });
            GenListSamples.Add(new Sample { Title = "1text", Class = typeof(Style1Text) });
            GenListSamples.Add(new Sample { Title = "1text.1icon", Class = typeof(Style1Text1Icon) });
            GenListSamples.Add(new Sample { Title = "1text.1icon.1", Class = typeof(Style1Text1Icon1) });
            GenListSamples.Add(new Sample { Title = "1text.1icon.divider", Class = typeof(Style1text1iconDivider) });
            GenListSamples.Add(new Sample { Title = "2text", Class = typeof(Style2text) });
            GenListSamples.Add(new Sample { Title = "2text.1icon", Class = typeof(Style2text1icon) });
            GenListSamples.Add(new Sample { Title = "2text.1icon.1", Class = typeof(Style2text1icon1) });
            GenListSamples.Add(new Sample { Title = "2text.1icon.divider", Class = typeof(Style2text1iconDivider) });
            GenListSamples.Add(new Sample { Title = "3text", Class = typeof(Style3text) });
            GenListSamples.Add(new Sample { Title = "4text", Class = typeof(Style4text) });
            //GenListSamples.Add(new Sample { Title = "editfield", Class = typeof(StyleEditfield) });
            GenListSamples.Add(new Sample { Title = "multiline", Class = typeof(StyleMultiline) });
            GenListSamples.Add(new Sample { Title = "select_mode", Class = typeof(StyleSelectMode) });

            ScrollerSamples = new ObservableCollection<Sample>();
            ScrollerSamples.Add(new Sample { Title = "Vertical Scroll", Class = typeof(VerticalScroller) });
            ScrollerSamples.Add(new Sample { Title = "Horizontal Scroll", Class = typeof(HorizontalScroller) });

            SpinnerSamples = new ObservableCollection<Sample>();
            SpinnerSamples.Add(new Sample { Title = "Set timer", Class = typeof(SpinnerTimer) });
            SpinnerSamples.Add(new Sample { Title = "Default", Class = typeof(SpinnerDefault) });
        }

        public IList<Sample> Samples { get; private set; }
        public IList<Sample> BgSamples { get; private set; }
        public IList<Sample> ButtonSamples { get; private set; }
        public IList<Sample> EntrySamples { get; private set; }
        public IList<Sample> PageControlSamples { get; private set; }
        public IList<Sample> PopupSamples { get; private set; }
        public IList<Sample> NoContentSamples { get; private set; }
        public IList<Sample> DateTimeSamples { get; private set; }
        public IList<Sample> GenListSamples { get; private set; }
        public IList<Sample> ScrollerSamples { get; private set; }
        public IList<Sample> SpinnerSamples { get; private set; }
    }
}
