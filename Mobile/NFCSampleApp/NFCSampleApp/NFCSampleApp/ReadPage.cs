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
    /// Page class for Tag Read
    /// </summary>
    public class ReadPage : ContentPage
    {
        INfcImplementation nfc = DependencyService.Get<INfcImplementation>();
        ILog log = DependencyService.Get<ILog>();

        /// <summary>
        /// Constructor of Read Page class
        /// </summary>
        public ReadPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize function of Read Page class
        /// </summary>
        private void InitializeComponent()
        {
            Title = "Read";
            string type = null;
            bool support = false;
            uint size = 0;
            uint maxsize = 0;

            nfc.GetTagType(ref type, ref support, ref size, ref maxsize);
            Label TypeLabel = CreateLabel("Type : ");
            Label typelabel = CreateLabel(type);
            Label NDEFsupport = CreateLabel("NDEF Support :");
            Label supportlabel = CreateLabel("Yes");
            if (support)
            {
                supportlabel.Text = "Yes";
            }
            else
            {
                supportlabel.Text = "No";
            }

            Label SizeLabel = CreateLabel("Message Size :");
            Label sizelabel = CreateLabel(size.ToString());
            Label MaxLabel = CreateLabel("Max Message Size :");
            Label maxsizelabel = CreateLabel(maxsize.ToString());

            AbsoluteLayout layout = new AbsoluteLayout { };

            AbsoluteLayout.SetLayoutFlags(TypeLabel, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(TypeLabel, new Rectangle(0f, 0.25f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(TypeLabel);

            AbsoluteLayout.SetLayoutFlags(typelabel, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(typelabel, new Rectangle(0.7f, 0.25f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(typelabel);

            AbsoluteLayout.SetLayoutFlags(NDEFsupport, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(NDEFsupport, new Rectangle(0f, 0.35f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(NDEFsupport);

            AbsoluteLayout.SetLayoutFlags(supportlabel, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(supportlabel, new Rectangle(0.7f, 0.35f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(supportlabel);

            AbsoluteLayout.SetLayoutFlags(SizeLabel, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(SizeLabel, new Rectangle(0f, 0.45f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(SizeLabel);

            AbsoluteLayout.SetLayoutFlags(sizelabel, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(sizelabel, new Rectangle(0.7f, 0.45f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(sizelabel);

            AbsoluteLayout.SetLayoutFlags(MaxLabel, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(MaxLabel, new Rectangle(0f, 0.55f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(MaxLabel);

            AbsoluteLayout.SetLayoutFlags(maxsizelabel, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(maxsizelabel, new Rectangle(0.7f, 0.55f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(maxsizelabel);

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