/* 
  * Copyright (c) 2016 Samsung Electronics Co., Ltd 
  * 
  * Licensed under the Flora License, Version 1.1 (the "License"); 
  * you may not use this file except in compliance with the License. 
  * You may obtain a copy of the License at 
  * 
  * http://www.apache.org/licenses/LICENSE-2.0 
  * 
  * Unless required by applicable law or agreed to in writing, software 
  * distributed under the License is distributed on an "AS IS" BASIS, 
  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
  * See the License for the specific language governing permissions and 
  * limitations under the License. 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace NFCSampleApp
{
    /// <summary>
    /// Page class for Server
    /// </summary>
    public class ServerPage : ContentPage
    {
        INfcImplementation nfc = DependencyService.Get<INfcImplementation>();
        ILog log = DependencyService.Get<ILog>();

        /// <summary>
        /// Constructor of Server Page class
        /// </summary>
        public ServerPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize function of Server Page class
        /// </summary>
        private void InitializeComponent()
        {
            Title = "Server";
            
            byte[] type = { 0xAC, 0xFA };
            byte[] ID = { 0xAC, 0xFA };
            byte[] payload = { 0xAC, 0xFA };
            uint paloadlength = 20;

            bool sent = false;
            nfc.GetP2p();
            sent = nfc.SendNDEF(type, ID, payload, paloadlength);

            Label msgSent = CreateLabel("Yes");
            if (sent)
            {
                msgSent.Text = "Message Sent Successfully";
            }
            else
            {
                msgSent.Text = "Message not sent";
            }

            AbsoluteLayout layout = new AbsoluteLayout { };

            AbsoluteLayout.SetLayoutFlags(msgSent, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(msgSent, new Rectangle(0.5f, 0.25f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(msgSent);            
            Content = layout;
        }

        /// <summary>
        /// Create a label
        /// </summary>
        /// <param name="text">The text to be displayed in label</param>
        /// <returns>The label</returns>
        private Label CreateLabel(string text)
        {
            return new Label()
            {
                Text = text,
                TextColor = Color.White,
                FontSize = 28,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }        
    }
}