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
using Xamarin.Forms.Xaml;
using VoiceMemo.ViewModels;
using VoiceMemo.Resx;
using Xamarin.Forms;

namespace VoiceMemo.Views
{
    /// <summary>
    /// The MainPage of VoiceMemo application
    /// </summary>
    public partial class MainPage : CirclePageEx
    {
        /// <summary>
        /// Constructor of StandByPage()
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            // Subscribe notification of locale changes to update text based on locale
            MessagingCenter.Subscribe<App>(this, MessageKeys.UpdateByLanguageChange, (obj) =>
            {
                // After Toolbar items are added, any properties of the toolbar item cannot be changed.
                // So, first of all, remove all toolbar items and update localized texts and then add items to Toolbar.
                var count = ToolbarItems.Count;
                for (int i = ToolbarItems.Count - 1; i >= 0; i--)
                {
                    ToolbarItems.RemoveAt(i);
                }

                // Update text that has been translated into the current language.
                recordsMenu.Text = AppResources.Recordings;
                sttOnOfMenu.Text = AppResources.SpeechToText;
                sttMenu.Text = AppResources.Languages;

                // Add items to Toolbar
                ToolbarItems.Add(recordsMenu);
                ToolbarItems.Add(sttOnOfMenu);
                if (count == 3)
                {
                    ToolbarItems.Add(sttMenu);
                }

                // Update Main label's text
                ((MainPageModel)BindingContext).UpdateText();
            });
        }

        /// <summary>
        /// Invoked when recording(red) button is tapped.
        /// RecordingPage will be shown.
        /// </summary>
        /// <param name="sender"> Sender object</param>
        /// <param name="e">EventArgs</param>
        async void LaunchRecordingPage_Tapped(object sender, EventArgs e)
        {
            // While STT is in progress, a new recording cannot be started.
            if (((App)App.Current).mainPageModel.availableToRecord)
            {
                // can record
                await Navigation.PushAsync(PageFactory.GetInstance(Pages.Recording, ((MainPageModel)ViewModel).SttEnabled));

            }
            else
            {
                // cannot record
                await DisplayAlert("Recording", "Stt service is in progress. Please wait a moment.", "OK");
            }
        }

        /// <summary>
        /// Standby > More options > Recordings
        /// A list of Recordings will be shown if there's recordings.
        /// </summary>
        /// <param name="sender"> Sender object</param>
        /// <param name="e">EventArgs</param>
        async void OnCircleToolbarItemClicked_DisplayRecordings(object sender, EventArgs e)
        {
            Console.WriteLine(" MainPage.OnCircleToolbarItemClicked_DisplayRecordings()");
            await Navigation.PushAsync(PageFactory.GetInstance(Pages.RecordList, ViewModel));
        }

        /// <summary>
        /// Standby > More options > Speech to text
        /// Invoked when "Speech to text" more option is selected to turn Speech to text feature on/off
        /// </summary>
        /// <param name="sender">sender object </param>
        /// <param name="e">EventArgs</param>
        void OnCircleToolbarItemClicked_OnOffStt(object sender, EventArgs e)
        {
            Console.WriteLine(" MainPage.OnCircleToolbarItemClicked_OnOffStt()");

            // As mentioned in API spec (https://developer.xamarin.com/api/type/Xamarin.Forms.ToolbarItem/),
            // Any changes made to the properties of the toolbar item after it has been added will be ignored.
            // So according to the circumstances, ToolbarItems are added or removed.
            if (ToolbarItems.Count == 3)
            {
                // When STT is off, "Language" more menu should be hidden.
                ToolbarItems.RemoveAt(2);
                ToolbarItems.RemoveAt(1);
                // When STT is off, Icon image & SubText should be changed.
                sttOnOfMenu.IconImageSource = "more_option_icon_stt_off.png";
                sttOnOfMenu.SubText = AppResources.SttOff;
                ToolbarItems.Add(sttOnOfMenu);
            }
            else
            {
                ToolbarItems.RemoveAt(1);
                // When STT is on, Icon image & SubText should be changed.
                sttOnOfMenu.IconImageSource = "more_option_icon_stt_on.png";
                sttOnOfMenu.SubText = AppResources.SttOn;
                ToolbarItems.Add(sttOnOfMenu);
                // When STT is on, "Language" more menu should be shown.
                ToolbarItems.Add(sttMenu);
            }
        }

        /// <summary>
        /// Called when Language is selected in More option
        /// Standby > More options > Language
        /// </summary>
        /// <param name="sender">sender object </param>
        /// <param name="e">EventArgs</param>
        async void OnCircleToolbarItemClicked_SelectLanguage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(PageFactory.GetInstance(Pages.Languages, ViewModel));
        }

        /// <summary>
        /// Invoked immediately prior to the Page becoming visible.
        /// </summary>
        protected override void OnAppearing()
        {
        }

        /// <summary>
        /// Invoked when this page disappears.
        /// </summary>
        protected override void OnDisappearing()
        {
        }

        public void Dispose()
        {
            ((MainPageModel)BindingContext).Dispose();
        }
    }
}