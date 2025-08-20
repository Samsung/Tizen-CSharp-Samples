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
using Xamarin.Forms;

namespace NFCSampleApp
{
    /// <summary>
    /// Page class for Server.
    /// </summary>
    public class P2PServerPage : ContentPage
    {
        private readonly NfcApiManager nfc = new NfcApiManager();

        /// <summary>
        /// Initializes a new instance of the <see cref="P2PServerPage"/> class.
        /// </summary>
        public P2PServerPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            byte[] type = { 0xAC, 0xFA };
            byte[] ID = { 0xAC, 0xFA };
            byte[] payload = { 0xAC, 0xFA };

            bool sent = false;
            nfc.GetP2P();
            sent = nfc.SendNdef(type, ID, payload);

            Label msgSent = CreateLabel("Yes");
            if (sent)
            {
                msgSent.Text = "Message Sent Successfully";
            }
            else
            {
                msgSent.Text = "Message not sent";
            }

            StackLayout layout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    msgSent,
                }
            };

            Content = layout;
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
                FontSize = 12,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }
    }
}