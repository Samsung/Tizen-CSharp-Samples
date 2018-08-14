//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace NFCSampleApp
{
    /// <summary>
    /// Page class for initial screen of application.
    /// </summary>
    public class RootPage : CirclePage
    {
        private readonly NfcApiManager nfc = new NfcApiManager();

        /// <summary>
        /// Initializes a new instance of the <see cref="RootPage"/> class.
        /// </summary>
        public RootPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            StackLayout layout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            Content = layout;

            if (!nfc.IsNfcSupported())
            {
                layout.Children.Add(CreateLabel("NFC is not supported"););
                return;
            }

            if (nfc.IsTagSupported() && nfc.TagInit())
            {
                Button Tag = CreateButton("NFC Tag");
                Tag.Clicked += TagClicked;
                layout.Children.Add(Tag);
            }
            else
            {
                layout.Children.Add(CreateLabel("NFC Tag is not supported"));
            }

            if (nfc.IsP2PSupported() && nfc.P2PInit())
            {
                Button P2P = CreateButton("NFC P2P");
                P2P.Clicked += P2pClicked;
                layout.Children.Add(P2P);
            }
            else
            {
                layout.Children.Add(CreateLabel("NFC P2P is not supported"));
            }
        }

        /// <summary>
        /// Creates a label.
        /// </summary>
        /// <param name="text">The text to be displayed in label</param>
        /// <returns>The label</returns>
        private Label CreateLabel(string text)
        {
            return new Label()
            {
                Text = text,
                TextColor = Color.White,
                FontSize = 8,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
        }

        /// <summary>
        /// Creates a button.
        /// </summary>
        /// <param name="text">The text to be displayed in button</param>
        /// <returns>The button</returns>
        private Button CreateButton(string text)
        {
            return new Button()
            {
                Text = text,
                WidthRequest = 200,
                Margin = new Thickness(10),
                Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
        }

        /// <summary>
        /// Event handler called when the Tag button is pressed.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Event arguments</param>
        void TagClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TagPage());
        }

        /// <summary>
        /// Event handler called when the P2P button is pressed.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Event arguments</param>
        void P2pClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new P2PPage());
        }
    }
}