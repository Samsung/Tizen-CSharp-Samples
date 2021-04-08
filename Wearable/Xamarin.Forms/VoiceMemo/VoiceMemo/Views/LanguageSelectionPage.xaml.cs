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
using VoiceMemo.Models;
using VoiceMemo.Resx;
using VoiceMemo.ViewModels;
using Xamarin.Forms;

namespace VoiceMemo.Views
{
    /// <summary>
    /// LanguageSelectionPage class
    /// </summary>
    public partial class LanguageSelectionPage : CirclePageEx
    {
        MainPageModel ViewModel;
        public bool ignoreRadioSelection = true;

        public LanguageSelectionPage(MainPageModel viewModel)
        {
            BindingContext = ViewModel = viewModel;
            // Subscribe notification of locale changes to update text based on locale
            MessagingCenter.Subscribe<App>(this, MessageKeys.UpdateByLanguageChange, (obj) =>
            {
                // Update text that has been translated into the current language.
                headerLabel.Text = AppResources.Languages;
            });
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            Console.WriteLine("[LanguageSelectionPage.Init()] LangListView.SelectedItem :" + LangListView.SelectedItem 
                + " VS. ViewModel.SelectedItemIndex :" + ViewModel.SelectedItemIndex);
            ignoreRadioSelection = true;

            // Scroll to the previously selected language if it exists.
            if (ViewModel.SelectedItemIndex != null)
            {
                LangListView.ScrollTo(ViewModel.SelectedItemIndex, ScrollToPosition.Center, false);
            }
        }

        /// <summary>
        /// Invoked when an item is tapped
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="args">ItemTappedEventArgs</param>
        void OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            ignoreRadioSelection = false;

            ViewModel.CurrentLanguage = ((SttLanguage)args.Item).Lang;
            SttLanguage item = args.Item as SttLanguage;
            for (int i = 0; i < ((MainPageModel)BindingContext).Languages.Count; i++)
            {
                if (((MainPageModel)BindingContext).Languages[i].Combo == item.Combo)
                {
                    Console.WriteLine("OnItemTapped : *" + i + " )  FOUND!!  IsOn will be true! ");
                    // The following code makes the Radio button of SttLanguageViewCell is selected.
                    ((MainPageModel)BindingContext).Languages[i].IsOn = true;
                }
            }
        }

        /// <summary>
        /// Invoked immediately prior to the Page becoming visible.
        /// </summary>
        protected override void OnAppearing()
        {
            Console.WriteLine("[LanguageSelectionPage]  OnAppearing() ...");
        }

        protected override void OnDisappearing()
        {
            Console.WriteLine("[LanguageSelectionPage]  OnDisappearing");
        }
    }
}