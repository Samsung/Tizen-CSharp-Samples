/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace EmailUI
{
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;

    /// <summary>
    /// The main page of the EmailUI application.
    /// </summary>
    public class EmailPage : FlyoutPage
    {
        private MasterPageCS masterPage;
        private DetailPageCS detailPage;

        public EmailPage()
        {
            //Install the MasterPage
            this.IsPresentedChanged += PresentedChanged;
            this.masterPage = new MasterPageCS();
            this.masterPage.ListView.ItemTapped += this.MasterOnItemTapped;
            this.Flyout = this.masterPage;

            //Install the DetailPage
            this.detailPage = new DetailPageCS();
            this.detailPage.ListView.ItemSelected += this.DetailOnItemSelected;
            this.Detail = this.detailPage;

            // Title of this page
            this.Title = "In Box";

            // Create ToolbarItem and set the click callback
            Action rightToolbarClick = () =>
            {
                Navigation.PushModalAsync(new ComposePage());
            };
            Action leftToolbarClick = () =>
            {
                // set true to check current status
                Flyout.IsEnabled = true;

                // check if the page exists
                if (!IsPresented)
                {
                    Flyout.IsEnabled = true;
                    this.IsPresented = true;
                }
                else
                {
                    Flyout.IsEnabled = false;
                    this.IsPresented = false;
                }

            };

            // add 2 toolbar items
            ToolbarItems.Add(new ToolbarItem(null, null, leftToolbarClick, ToolbarItemOrder.Secondary, 1));
            ToolbarItems.Add(new ToolbarItem("new", null, rightToolbarClick, ToolbarItemOrder.Primary, 0));
        }

        /// <summary>
        /// Called right after the Detail list item is selected.
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void DetailOnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            this.IsPresented = false;
        }

        /// <summary>
        /// Called right after the Master list item is selected.
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void MasterOnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as MasterPageItem;

            // check if selected item is "In Box"
            if (item.Name == "In Box")
            {
                Title = "In Box";
            }
            else
            {
                Title = "Out Box";
            }

            this.Flyout.IsEnabled = false;
            this.IsPresented = false;
            this.Flyout.IsEnabled = true;
        }

        private void PresentedChanged(object sender, EventArgs e)
        {
            if (this.IsPresented)
            {
                this.Detail.IsEnabled = false;
            }
            else
            {
                this.Detail.IsEnabled = true;
            }
        }

    }
}
