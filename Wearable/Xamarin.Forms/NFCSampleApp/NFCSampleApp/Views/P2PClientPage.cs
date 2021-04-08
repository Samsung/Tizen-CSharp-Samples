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
    /// Page class for client
    /// </summary>
    public class P2PClientPage : CirclePage
    {
        private NfcApiManager nfc = new NfcApiManager();

        private Label recordCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="P2PClientPage"/> class.
        /// </summary>
        public P2PClientPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            nfc.GetP2P();
            nfc.ReceiveNdef(Received);

            recordCount = CreateLabel("Record Count : 0");
            StackLayout layout = new StackLayout
            {
                Children =
                {
                    recordCount,
                }
            };

            Content = layout;
        }

        /// <summary>
        /// Event handler called when the NDEF Message received from NFC device.
        /// </summary>
        /// <param name="count">The count of NDEF record</param>
        private void Received(int count)
        {           
            recordCount.Text = String.Format("Record Count : {0}",count);            
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
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
        }
    }
}