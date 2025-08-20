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
    /// The compose page of the EmailUI application.
    /// </summary>
    public class ComposePage : ContentPage
    {
        private const int FONT_SIZE = 24;

        private string toText;
        private string subjectText;
        private string composeText;
        private Editor composeEditor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComposePage"/> class.
        /// </summary>
        public ComposePage()
        {
            // The title of this page
            Title = "Compose";

            #region "Title area"
            // Add cancel button
            Button CancelBtn = new Button()
            {
                ImageSource = "icon_cancel.png",
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 50,
                HeightRequest = 50,
            };
            CancelBtn.Clicked += OnCancelBtnClicked;

            // Add title label
            Label TitleArea = new Label()
            {
                Text = Title,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = FONT_SIZE + 2,
            };

            // Add ok button
            Button OkBtn = new Button()
            {
                ImageSource = "icon_done.png",
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 50,
                HeightRequest = 50,
            };
            OkBtn.Clicked += OnOkBtnClickedAsync;
            #endregion

            #region "Body area"
            // Add email address entry
            Entry emailEntry = new Entry()
            {
                HeightRequest = 0.07 * App.screenHeight,
                Text = "example@tizen.com",
                FontSize = FONT_SIZE
            };

            // Add To address entry
            Entry ToEntry = new Entry()
            {
                HeightRequest = 0.07 * App.screenHeight,
                Placeholder = "To",
                PlaceholderColor = Color.Gray,
                FontSize = FONT_SIZE
            };
            ToEntry.TextChanged += ToEntry_TextChanged;

            // Add subject entry
            Entry subjectEntry = new Entry()
            {
                HeightRequest = 0.07 * App.screenHeight,
                Placeholder = "Subject",
                PlaceholderColor = Color.Gray,
                FontSize = FONT_SIZE
            };
            subjectEntry.TextChanged += SubjectEntry_TextChanged;

            // Add compose editor
            composeEditor = new Editor() {
                HeightRequest = 0.07 * App.screenHeight,
                Text = "Compose email",
                TextColor = Color.Gray,
                FontSize = FONT_SIZE
            };
            composeEditor.TextChanged += ComposeEditor_TextChanged;

            // reset editor window
            composeEditor.Focused += ComposeEditor_Focused;
            composeEditor.Unfocused += ComposeEditor_Focused;
            #endregion

            // Content view of this page.
            Content = new StackLayout
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,

                // Set the Orientations of the StackLayout, the layout is vertical
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    // Add title area
                    new StackLayout
                    {
                        Padding = new Thickness(0,10,0,0),
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            CancelBtn,
                            TitleArea,
                            OkBtn
                        }

                    },

                    // Add body area
                    emailEntry,
                    ToEntry,
                    subjectEntry,
                    composeEditor
                }
            };
        }

        private void ComposeEditor_Focused(object sender, FocusEventArgs e)
        {
            // remove initial text
            if (composeEditor.Text.CompareTo("Compose email") == 0)
            {
                composeEditor.Text = "";
                composeEditor.TextColor = Color.Black;
            }

            // add initial text on empty case
            if (e.IsFocused == false && composeEditor.Text.CompareTo("") == 0)
            {
                composeEditor.Text = "Compose email";
                composeEditor.TextColor = Color.Gray;
            }
        }

        /// <summary>
        /// TextChanged event function of compose editor
        /// </summary>
        /// <param name="sender">sender of object type</param>
        /// <param name="e">event of TextChangedEventArgs type</param>
        private void ComposeEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            composeText = e.NewTextValue;
        }

        /// <summary>
        /// TextChanged event function of subject entry
        /// </summary>
        /// <param name="sender">sender of object type</param>
        /// <param name="e">event of TextChangedEventArgs type</param>
        private void SubjectEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            subjectText = e.NewTextValue;
        }

        /// <summary>
        /// TextChanged event function of to entry
        /// </summary>
        /// <param name="sender">sender of object type</param>
        /// <param name="e">event of TextChangedEventArgs type</param>
        private void ToEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            toText = e.NewTextValue;
        }

        /// <summary>
        /// clicked event function of Ok button
        /// </summary>
        /// <param name="sender">sender of object type</param>
        /// <param name="e">event of EventArgs type</param>
        //private async void OnOkBtnClicked(object sender, EventArgs e)
        private async void OnOkBtnClickedAsync(object sender, EventArgs e)
        {
            // check 'to' text
            if (toText.Equals(""))
            {
                await DisplayAlert("Warning","Add at least one recipient.","ok");
                return;
            }
            // check 'subject' text
            else if (subjectText.Equals("")) 
            {
                await DisplayAlert("Error", "There's no text in the message subject.", "ok", "Cancel");
            }
            // check 'compose' text
            else if (composeText == null || composeText.Equals(""))
            {
                await DisplayAlert("Error", "There's no text in the message body.", "ok", "Cancel");
            }
            // no issue case
            else
            {
                SendMessageAsync();
                return;
            }
        }

        /// <summary>
        /// clicked event function of Cancel button
        /// </summary>
        /// <param name="sender">sender of object type</param>
        /// <param name="e">event of EventArgs type</param>
        private void OnCancelBtnClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        /// <summary>
        /// sending message function
        /// </summary>
        private async void SendMessageAsync()
        {
            await Navigation.PopModalAsync();
            await DisplayAlert("Alert", "Sending message...", "ok");
        }
    }
}
