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
    /// Page class for initial screen of application
    /// </summary>
    public class RootPage : ContentPage
    {
        INfcImplementation nfc = DependencyService.Get<INfcImplementation>();
        ILog log = DependencyService.Get<ILog>();

        /// <summary>
        /// Constructor of Root Page class
        /// </summary>
        public RootPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize function of Root Page class
        /// </summary>
        private void InitializeComponent()
        {
            this.Title = "NFC";
            

            if (nfc.IsSupported() == false)
            {
                StackLayout stackLayout = new StackLayout { };
                Label title = CreateLable("NFC is not supported");

                stackLayout.Children.Add(title);
                Content = stackLayout;
                return;
            }

            AbsoluteLayout layout = new AbsoluteLayout { };

            if (nfc.TagInit() == true)
            {
                Button Tag = CreateButton("NFC Tag");
                Tag.Clicked += TagClicked;
                AbsoluteLayout.SetLayoutFlags(Tag, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(Tag, new Rectangle(0.5f, 0.45f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                layout.Children.Add(Tag);
            }

            if (nfc.P2p_init() == true)
            {
                Button P2P = CreateButton("NFC P2P");
                P2P.Clicked += P2pClicked;
                AbsoluteLayout.SetLayoutFlags(P2P, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(P2P, new Rectangle(0.5f, 0.55f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                layout.Children.Add(P2P);
            }

            Content = layout;
        }

        /// <summary>
        /// Create a label
        /// </summary>
        /// <param name="text">The text to be displayed in label</param>
        /// <returns>The label</returns>
        private Label CreateLable(string text)
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

        /// <summary>
        /// Create a button
        /// </summary>
        /// <param name="text">The text to be displayed in button</param>
        /// <returns>The button</returns>
        private Button CreateButton(string text)
        {
            return new Button()
            {
                Text = text,
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
        }

        /// <summary>
        /// Callback function to be called when the Tag button is pressed
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Argument of Event</param>
        void TagClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TagPage());
        }

        /// <summary>
        /// Callback function to be called when the P2p button is pressed
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Argument of Event</param>
        void P2pClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new P2PPage());
        }
    }
}