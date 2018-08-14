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

using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace NFCSampleApp
{
    /// <summary>
    /// Page class for P2P
    /// </summary>
    public class P2PPage : CirclePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="P2PPage"/> class.
        /// </summary>
        public P2PPage()
        {
            Title = "NFC P2P";

            StackLayout layout = new StackLayout { };
            layout.HorizontalOptions = LayoutOptions.CenterAndExpand;
            layout.VerticalOptions = LayoutOptions.CenterAndExpand;

            Button client = CreateButton("Client");
            client.Clicked += LoadClientPage;
            layout.Children.Add(client);

            Button server = CreateButton("Server");
            server.Clicked += LoadServerPage;
            layout.Children.Add(server);

            Content = layout;
        }

        /// <summary>
        /// Loads P2PClientPage.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Event arguments</param>
        private void LoadClientPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new P2PClientPage());
        }

        /// <summary>
        /// Loads P2PServerPage.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Event arguments</param>
        private void LoadServerPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new P2PServerPage());
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
    }
}