/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd
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
using Xamarin.Forms;
using Contacts.Models;

namespace Contacts.Views
{
    /// <summary>
    /// A custom layout for displaying the text and buttons to input event properties.
    /// </summary>
    public partial class ItemPage : ContentPage
    {
        /// <summary>
        /// A RecordItem property sets to or gets form the source.
        /// </summary>
        public RecordItem item;

        /// <summary>
        /// A event handler for the save or update button.
        /// <param name="sender">The object what got the event</param>
        /// <param name="e">Data of the event</param>
        /// </summary>
        public void OnLeftClicked(object sender, EventArgs e)
        {
            item.First = xFirst.Text;
            item.Last = xLast.Text;
            item.Number = xNumber.Text;
            item.Email = xEmail.Text;
            item.Url = xUrl.Text;
            item.Company = xCompany.Text;
            item.Note = xNote.Text;
            item.Event = xEvent.Date.Year * 10000 + xEvent.Date.Month * 100 + xEvent.Date.Day;

            if (item.Index == 0)
            {
                RecordItemProvider.Instance.Insert(item);
            }
            else
            {
                RecordItemProvider.Instance.Update(item);
            }
            Navigation.PopAsync();
        }

        /// <summary>
        /// A event handler for the Delete button.
        /// <param name="sender">The object what got the event</param>
        /// <param name="e">Data of the event</param>
        /// </summary>
        public void OnRightClicked(object sender, EventArgs e)
        {
            RecordItemProvider.Instance.Delete(item);
            Navigation.PopAsync();
        }

        /// <summary>
        /// A Constructor.
        /// Shows event properties to modify data.
        /// <param name="inItem">The item to be shown.</param>
        /// <param name="ButtonText">The name of button.</param>
        /// <param name="index">The selected event id if it exists.</param>
        /// </summary>
        public ItemPage(RecordItem inItem, string ButtonText, int index)
        {
            InitializeComponent();

            item = inItem;

            xFirst.Text = inItem.First;
            xLast.Text = inItem.Last;
            xNumber.Text = inItem.Number;
            xEmail.Text = inItem.Email;
            xUrl.Text = inItem.Url;
            xCompany.Text = inItem.Company;
            xNote.Text = inItem.Note;
            if (inItem.Event != 0)
            {
                xEvent.Date = new DateTime(inItem.Event / 10000, (inItem.Event % 10000) / 100,
                        inItem.Event % 100, 0, 0, 0, DateTimeKind.Local);
            }
            Title = (index == 0) ? "Create contact" : inItem.DisplayName;
            xRight.IsVisible = (index == 0) ? false : true;
            xLeft.Text = ButtonText;
        }
    }
}

