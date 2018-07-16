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
    /// Page class for client
    /// </summary>
    public class ClientPage : ContentPage
    {
        INfcImplementation nfc = DependencyService.Get<INfcImplementation>();
        ILog log = DependencyService.Get<ILog>();

        /// <summary>
        /// Constructor of Client Page class
        /// </summary>
        public ClientPage()
        {
            InitializeComponent();
        }

        Label reccount;

        /// <summary>
        /// Initialize function of Client Page class
        /// </summary>
        private void InitializeComponent()
        {

            Title = "Client";

            nfc.GetP2p();
            nfc.ReceiveNDEF(Received);

            reccount = CreateLabel("Record Count : 0");
            AbsoluteLayout layout = new AbsoluteLayout { };

            AbsoluteLayout.SetLayoutFlags(reccount, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(reccount, new Rectangle(0.5f, 0.25f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(reccount);
            
            Content = layout;
        }

        /// <summary>
        /// Callback function to be called when the NDEF Message received from NFC device
        /// </summary>
        /// <param name="count">The count of NDEF record</param>
        private void Received(int count)
        {           
            reccount.Text = String.Format("Record Count : {0}",count);            
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
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
        }
    }
}