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
using VoiceMemo.Effects;
using VoiceMemo.Models;
using VoiceMemo.Resx;
using VoiceMemo.ViewModels;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace VoiceMemo.Views
{
    /// <summary>
    /// RecordListPage class
    /// It shows the list of recordings
    /// </summary>
    public partial class RecordListPage : CirclePageEx
    {
        MainPageModel ViewModel;
        public RecordListPage(MainPageModel viewModel)
        {
            BindingContext = ViewModel = viewModel;
            // Subscribe notification of locale changes to update text based on locale
            MessagingCenter.Subscribe<App>(this, MessageKeys.UpdateByLanguageChange, (obj) =>
            {
                UpdateUI();
            });
            InitializeComponent();
            ImageAttributes.SetBlendColor(NoImage, Color.FromRgba(94, 94, 94, 77));
            // Binding for ContextPopupEffectBehavior's properties
            // BindingContext is not inherited by Behavior and Effect.
            // Source of Binding should be specified explicitly.
            CheckedCounterBehavior.SetBinding(ContextPopupEffectBehavior.AcceptTextProperty, new Binding("SelectOptionMessage1", BindingMode.OneWay, source: BindingContext));
            CheckedCounterBehavior.SetBinding(ContextPopupEffectBehavior.AcceptCommandProperty, new Binding("SelectCommand1", BindingMode.OneWay, source: BindingContext));
            CheckedCounterBehavior.SetBinding(ContextPopupEffectBehavior.CancelTextProperty, new Binding("SelectOptionMessage2", BindingMode.OneWay, source: BindingContext));
            CheckedCounterBehavior.SetBinding(ContextPopupEffectBehavior.CancelCommandProperty, new Binding("SelectCommand2", BindingMode.OneWay, source: BindingContext));
            CheckedCounterBehavior.SetBinding(ContextPopupEffectBehavior.VisibilityProperty, new Binding("PopupVisibility", BindingMode.TwoWay, source: BindingContext));
        }

        void UpdateUI()
        {
            // Update text that has been translated into the current language.
            noRecordingsLabel.Text = AppResources.NoRecordings;
            recordingsLabel.Text = AppResources.Recordings;
            headerLabel.Text = AppResources.Recordings;
            deleteActionButton.Text = AppResources.DELETE;
            toolbarItem.Text = AppResources.DELETE;
        }

        /// <summary>
        /// Invoked immediately prior to the Page becoming visible.
        /// </summary>
        protected override void OnAppearing()
        {
            Console.WriteLine("[RecordListPage]  OnAppearing() ...");
            foreach (var record in ViewModel.Records)
            {
                record.Checked = false;
            }
        }

        public void Rotate(RotaryEventArgs args)
        {
            Console.WriteLine("[RecordListPage]  Rotate() IsClockwise ? " + args.IsClockwise);
            //DataListView.ScrollTo();
        }

        /// <summary>
        /// Invoked when this page disappears.
        /// </summary>
        protected override void OnDisappearing()
        {
            Console.WriteLine("[RecordListPage]  OnDisappearing");
            ViewModel.IsCheckable = false;
        }

        /*async*/
        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Record;
            if (item == null)
            {
                return;
            }

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item
            DataListView.SelectedItem = null;
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            if (((MainPageModel)BindingContext).IsCheckable)
            {
                Console.WriteLine(" RecordListPage.OnItemTapped :  Checkable mode so ignore tapped event");
                return;
            }

            Console.WriteLine(" RecordListPage.OnItemTapped :  " + args.Item);
            await Navigation.PushAsync(PageFactory.GetInstance(Pages.Details, args.Item));
        }

        void OnCircleToolbarItemClicked_DeleteRecord(object sender, EventArgs args)
        {
            Console.WriteLine("[RecordListPage.OnCircleToolbarItemClicked_DeleteRecord] sender : " + sender);

            //MessagingCenter.Send<Page, Record>(this, MessageKeys.DeleteVoiceMemo, ((DetailsPageModel)viewModel).Record);
            //await Navigation.PopToRootAsync();

            //MessagingCenter.Send<Page, Record>(this, MessageKeys.DeleteVoiceMemo, ((DetailsPageModel)viewModel).Record);
            //await Navigation.PushAsync(PageFactory.GetInstance(Pages.PlayBack, viewModel.Record/*new PlayBackPageModel(viewModel.Record)*/));
        }

        void DeleteActionButtonClicked(object sender, EventArgs args)
        {
            Console.WriteLine("[RecordListPage.DeleteActionButtonClicked] sender : " + sender + " CheckedNamesCount : " + ViewModel.CheckedNamesCount);
            if (ViewModel.CheckedNamesCount == 1)
            {
                GraphicPopUp popup = new GraphicPopUp
                {
                    Text = AppResources.Deleted,
                };
                popup.TimedOut += Popup_TimedOut;
                popup.Show();
            }
            else
            {
                ProgressbarPopup popup = new ProgressbarPopup
                {
                    Text = AppResources.Deleted,
                    ProgressbarText = AppResources.Deleting,
                };
                popup.TimedOut += Popup_TimedOut;
                popup.Show();
            }
        }

        private void Popup_TimedOut(object sender, EventArgs e)
        {
            Console.WriteLine("[RecordListPage.Popup_TimedOut] sender : " + sender);
            //await Navigation.PopToRootAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            if (ViewModel.IsCheckable)
            {
                Console.WriteLine("[RecordListPage.OnBackButtonPressed] IsCheckable = true");
                ViewModel.IsCheckable = false;
                return true;
            }
            else
            {
                Console.WriteLine("[RecordListPage.OnBackButtonPressed] IsCheckable = false");
                return false;
            }
        }
    }
}
