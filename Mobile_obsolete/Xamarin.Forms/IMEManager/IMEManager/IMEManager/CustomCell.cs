/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
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
using Label = Xamarin.Forms.Label;

namespace IMEManager
{
    /// <summary>
    /// Define a custom cell class.
    /// </summary>
    public class CustomCell : ViewCell
    {
        /// <summary>
        /// This property is used for sending command.
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(CustomCell));

        /// <summary>
        /// Item layout for custom ViewCell.
        /// </summary>
        private RelativeLayout listItemLayout;

        /// <summary>
        /// Title label of menu item.
        /// </summary>
        private Label menuLabel;

        /// <summary>
        /// Command to be used for page redirection.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomCell"/> class.
        /// </summary>
        /// <param name="title"> The title label in this view cell. </param>
        /// <param name="page"> The containing page for the table cell. </param>
        public CustomCell(string title, ContentPage page)
        {
            // Create new Relative layout for custom cell.
            listItemLayout = new RelativeLayout
            {
                HeightRequest = 120,
            };

            // Create new Label for item menu text.
            menuLabel = new Label
            {
                FontSize = 21,
                Text = title,
            };

            // Set x,y coordinates for aligning menu label.
            listItemLayout.Children.Add(menuLabel, Constraint.RelativeToParent((parent) => (parent.X + 32)), Constraint.RelativeToParent((parent) => (.3 * parent.Height)));

            View = listItemLayout;

            // Add tap gesture.
            this.Tapped += (s, e) =>
            {
                if (Command == null)
                {
                    // This command is used for push new page in async mode,
                    // the parameter title is title of the new page.
                    Command = new Command(async () =>
                    {
                        await page.Navigation.PushAsync(new ResultPage(title));
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
