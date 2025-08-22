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
    /// Page class for Tag Read
    /// </summary>
    public class TagReadPage : CirclePage
    {
        private readonly NfcApiManager nfc = new NfcApiManager();

        /// <summary>
        /// Initializes a new instance of the <see cref="TagReadPage"/> class.
        /// </summary>
        public TagReadPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            Title = "Read";
            string type = null;
            bool support = false;
            uint size = 0;
            uint maxsize = 0;

            nfc.GetTagInfo(ref type, ref support, ref size, ref maxsize);

            Label typeTitleLabel = CreateLabel("Type : ");
            Label typeDetailLabel = CreateDetailLabel(type);
            Label supportTitleLabel = CreateLabel("NDEF Support :");
            Label supportDetailLabel = CreateDetailLabel("Yes");
            if (support)
            {
                supportDetailLabel.Text = "Yes";
            }
            else
            {
                supportDetailLabel.Text = "No";
            }

            Label sizeTitleLabel = CreateLabel("Message Size :");
            Label sizeDetalLabel = CreateDetailLabel(size.ToString());
            Label maxTitleLabel = CreateLabel("Max Message Size :");
            Label maxDetailLabel = CreateDetailLabel(maxsize.ToString());

            StackLayout layout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(10),
                Children =
                {
                    typeTitleLabel,
                    typeDetailLabel,
                    supportTitleLabel,
                    supportDetailLabel,
                    sizeTitleLabel,
                    sizeDetalLabel,
                    maxTitleLabel,
                    maxDetailLabel,
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
                FontSize = 8,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }

        /// <summary>
        /// Creates a smaller label with details.
        /// </summary>
        /// <param name="text">The text to be displayed in label</param>
        /// <returns>The label</returns>
        private Label CreateDetailLabel(string text)
        {
            return new Label()
            {
                Text = text,
                TextColor = Color.White,
                FontSize = 6,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }
    }
}