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

namespace SmartCardSampleApp
{
    /// <summary>
    /// Page class for initial screen of application
    /// </summary>
    public class RootPage : ContentPage
    {
        ISmartCardImplementation smartcard = DependencyService.Get<ISmartCardImplementation>();
        ILog log = DependencyService.Get<ILog>();

        /// <summary>
        /// Constructor of RootPage class
        /// </summary>
        public RootPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize function of RootPage class
        /// </summary>
        private void InitializeComponent()
        {
            Title = "Smart Card";
            IsVisible = true;

            StackLayout layout = new StackLayout { };

            Label title = CreateLable("No Secure Element is present");
            
            Label title_found = CreateLable("Select Secure Element");
            title_found.VerticalOptions = LayoutOptions.Start;

            if (smartcard.Initialize() == false)
            {
                layout.Children.Add(title);
                Content = layout;
                return;
            }

            bool supportSE = smartcard.IsSecureElementPresent();
            log.Log("Secure Element Present : " + supportSE.ToString());

            List<String> readersList = smartcard.GetReadersList();

            if (!supportSE)
            {
                layout.Children.Add(title);
            }
            else
            {
                layout.Children.Add(title_found);
                for (int i = 0; i < readersList.Count(); i++)
                {
                    log.Log("Name : " + readersList.ElementAt(i));
                    Button button = CreateButton(readersList.ElementAt(i));
                    button.Clicked += DevClicked;
                    layout.Children.Add(button);
                }                
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
        /// Callback function to be called when the button is pressed
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Argument of Event</param>
        void DevClicked(object sender, EventArgs e)
        {
            string device = ((Button)sender).Text;
            Navigation.PushAsync(new CommandPage(device));
        }
    }
}