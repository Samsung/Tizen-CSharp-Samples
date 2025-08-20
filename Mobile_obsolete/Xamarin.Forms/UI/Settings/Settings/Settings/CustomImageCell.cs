/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Windows.Input;
using Xamarin.Forms;
using Image = Xamarin.Forms.Image;
using Label = Xamarin.Forms.Label;

namespace Settings
{
    /// <summary>
    /// Define a custom cell class
    /// </summary>
    public class CustomImageCell : ViewCell
    {
        /// <summary>
        /// This property is used for sending command (page push)
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(CustomImageCell));

        /// <summary>
        /// Item layout for custom viewcell
        /// </summary>
        private RelativeLayout listItemLayout;

        /// <summary>
        /// Icon image of menu item
        /// </summary>
        private Image menuImage;

        /// <summary>
        /// Title label of menu item
        /// </summary>
        private Label menuLabel;

        /// <summary>
        /// Command to be used for page redirection
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// CustomImageCell constructor to build UI controls
        /// <param name="title">The title label in this view cell</param>
        /// <param name="imageSource">The image source for the icon image in this view cell</param>
        /// <param name="page">The containing page for the table cell</param>
        /// <seealso cref="ContentPage">
        /// </summary>
        public CustomImageCell(String title, String imageSource, ContentPage page)
        {

            /// create new Relative layout for custom cell.
            listItemLayout = new RelativeLayout
            {
                HeightRequest = 120,
            };

            /// create new Image for left icon.
            menuImage = new Image
            {
                Source = imageSource,
                WidthRequest = 50,
                HeightRequest = 50,
            };

            /// create new Label for settings menu text.
            menuLabel = new Label
            {
                FontSize = 40,
                Text = title,
            };

            FontFormat.SetFontWeight(menuLabel, FontWeight.Light);

            ///set x,y coordinates for aligning menu label
            listItemLayout.Children.Add(menuLabel,
                    Constraint.RelativeToParent((parent) => (parent.X + 32 + 50 + 32)),
                    Constraint.RelativeToParent((parent) => (.5 * parent.Height - 30)));

            ///set x,y coordinates for aligning menu icon
            listItemLayout.Children.Add(menuImage,
                    Constraint.RelativeToParent((parent) => (parent.X + 32)),
                    Constraint.RelativeToParent((parent) => (.5 * parent.Height - 25)));

            View = listItemLayout;

            /// add tap gesture
            this.Tapped += (s, e) =>
            {
                if (Command == null)
                {
                    // This command is used for push new page in async mode,
                    // the parameter title is title of the new page.
                    Command = new Command(async () =>
                    {
                        await page.Navigation.PushAsync(new SecondPage(title));
                    });
                }

                if (Command != null)
                {
                    Command.Execute(null);
                }
            };
        }
    }
}