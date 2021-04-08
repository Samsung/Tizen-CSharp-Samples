using VoiceMemo.Resx;
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
using VoiceMemo.ViewModels;
using Xamarin.Forms;

namespace VoiceMemo.Views
{
    /// <summary>
    /// DetailsPage class
    /// It shows the details of voice memo
    /// file name, recorded time, recording time
    /// </summary>
    public partial class DetailsPage : CirclePageEx
    {
        DetailsPageModel viewModel;

        public DetailsPage(DetailsPageModel _viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel = _viewModel;

            // Spare as high as an action button when a text is long
            mainLayout.Children.Add(new BoxView(), Constraint.RelativeToView(ContentLabel, (Parent, sibling) =>
            {
                return sibling.X;
            }), Constraint.RelativeToView(ContentLabel, (parent, sibling) =>
            {
                return sibling.Y + sibling.Height;
            }), Constraint.RelativeToParent((parent) =>
            {
                return 294;
            }), Constraint.RelativeToParent((parent) =>
            {
                return 78 + 7;
            }));
        }

        /// <summary>
        /// Request to play voice memo/record
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="args">EventArgs</param>
        async void OnPlayBackClicked(object sender, EventArgs args)
        {
            // Move to the recording page to keep recording
            Console.WriteLine("[DetailsPage.OnPlayBackClicked]     ");
            await Navigation.PushAsync(PageFactory.GetInstance(Pages.PlayBack, viewModel.Record/*new PlayBackPageModel(viewModel.Record)*/));
        }

        /// <summary>
        /// Request to delete the record
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="args">EventArgs</param>
        void OnCircleToolbarItemClicked_DeleteRecord(object sender, EventArgs args)
        {
            Console.WriteLine("[OnCircleToolbarItemClicked_DeleteRecord] sender : " + sender + ", record: " + ((DetailsPageModel)viewModel).Record);

            GraphicPopUp popup = new GraphicPopUp
            {
                Text = AppResources.Deleted,
            };
            popup.TimedOut += Popup_TimedOut;
            popup.Show();

            // Publish "DeleteVoiceMemo" message for a listener to remove this record from database.
            MessagingCenter.Send<Page, Record>(this, MessageKeys.DeleteVoiceMemo, ((DetailsPageModel)viewModel).Record);
        }

        private async void Popup_TimedOut(object sender, EventArgs e)
        {
            Console.WriteLine("[DetailsPage]  Popup_TimedOut() ...");
            await Navigation.PopToRootAsync();
        }

        /// <summary>
        /// Invoked immediately prior to the Page becoming visible.
        /// </summary>
        protected override void OnAppearing()
        {
            Console.WriteLine("[DetailsPage]  OnAppearing() ...");
        }

        /// <summary>
        /// Invoked when this page disappears.
        /// </summary>
        protected override void OnDisappearing()
        {
            Console.WriteLine("[DetailsPage]  OnDisappearing");
        }

        /// <summary>
        /// Invoked when backbutton is pressed
        /// VoiceMemoStandByPage will be shown.
        /// </summary>
        /// <returns>bool value</returns>
        protected override bool OnBackButtonPressed()
        {
            if (((DetailsPageModel)BindingContext).IsNew)
            {
                Console.WriteLine("[DetailsPage-OnBackButtonPressed]  PopToRootAsync");
                Navigation.PopToRootAsync();
            }
            else
            {
                Console.WriteLine("[DetailsPage-OnBackButtonPressed]  PopAsync");
                Navigation.PopAsync();
            }

            return true;
        }
    }
}
