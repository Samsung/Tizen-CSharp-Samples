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
    /// Page class for Tag Write
    /// </summary>
    public class WritePage : ContentPage
    {
        /// <summary>
        /// Constructor of Write Page class
        /// </summary>
        public WritePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize function of Write Page class
        /// </summary>
        private void InitializeComponent()
        {
            Title = "Write";

            StackLayout layout = new StackLayout { };

            Label title = CreateLabel("Write Tag.");

            layout.Children.Add(title);

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
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
        }
    }
}