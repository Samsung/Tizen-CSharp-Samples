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
    /// Page class for command
    /// </summary>
    public class CommandPage : ContentPage
    {
        ISmartCardImplementation smartcard = DependencyService.Get<ISmartCardImplementation>();
        ILog log = DependencyService.Get<ILog>();

        /// <summary>
        /// Constructor of CommandPage class
        /// </summary>
        /// <param name="device">The device</param>
        public CommandPage(string device)
        {
            InitializeComponent(device);
        }

        /// <summary>
        /// The name of reader
        /// </summary>
        string devicename = "";

        /// <summary>
        /// Command to send to the Secure element
        /// </summary>
        string command = "";

        /// <summary>
        /// Responses to sent commands
        /// </summary>
        Label backResponse;

        /// <summary>
        /// Initialize function of CommandPage class
        /// </summary>
        /// <param name="device">The device</param>
        private void InitializeComponent(string device)
        {
            Title = "Smart Card";
            IsVisible = true;
            devicename = device;
            Label ReaderName = CreateLabel("Reader Name : ");
            Label DeviceName = CreateLabel(device);
            Label Command = CreateLabel("Command :");
            Label Response = CreateLabel("Response :");
            backResponse = CreateLabel("");
            backResponse.HorizontalOptions = LayoutOptions.Start;
            var commandEntry = new Entry { Placeholder = "" , FontSize = 10, BackgroundColor = Color.White,
                TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Start};

            Button SendButton = CreateButton("Send");
           
            AbsoluteLayout layout = new AbsoluteLayout { };
            
            AbsoluteLayout.SetLayoutFlags(ReaderName, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(ReaderName, new Rectangle(0.1f,0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(ReaderName);

            AbsoluteLayout.SetLayoutFlags(DeviceName, AbsoluteLayoutFlags.YProportional);
            AbsoluteLayout.SetLayoutBounds(DeviceName, new Rectangle(200f, 0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(DeviceName);

            AbsoluteLayout.SetLayoutFlags(Command, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(Command, new Rectangle(0.1f, 0.20f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(Command);

            commandEntry.HorizontalTextAlignment = TextAlignment.Center;
            AbsoluteLayout.SetLayoutFlags(commandEntry, AbsoluteLayoutFlags.YProportional);
            AbsoluteLayout.SetLayoutBounds(commandEntry, new Rectangle(150f, 0.20f, 200f, AbsoluteLayout.AutoSize));
            layout.Children.Add(commandEntry);

            AbsoluteLayout.SetLayoutFlags(Response, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(Response, new Rectangle(0.1f, 0.40f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(Response);

            AbsoluteLayout.SetLayoutFlags(backResponse, AbsoluteLayoutFlags.YProportional);
            AbsoluteLayout.SetLayoutBounds(backResponse, new Rectangle(150f, 0.40f, 150f, AbsoluteLayout.AutoSize));
            layout.Children.Add(backResponse);

            AbsoluteLayout.SetLayoutFlags(SendButton, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(SendButton, new Rectangle(0.5f, 0.70f, 200f, AbsoluteLayout.AutoSize));
            layout.Children.Add(SendButton);

            commandEntry.TextChanged += CommandEntered;
            SendButton.Clicked += SendClicked;

            Content = layout;
          
        }

        /// <summary>
        /// Callback function to be called when the commandEntry text is changed
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Argument of Event</param>
        void CommandEntered(object sender, EventArgs e)
        {
            command = ((Entry)sender).Text;           
        }

        /// <summary>
        /// Callback function to be called when the "Send" button is clicked
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Argument of Event</param>
        void SendClicked(object sender, EventArgs e)
        {
            backResponse.Text = smartcard.SendCommand(devicename, command);
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
                FontSize = 8,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Start,
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
                Font = Font.SystemFontOfSize(NamedSize.Small),
                BorderWidth = 1,
                WidthRequest = 200,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
        }      
    }
}