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
    public class CustomCell : ViewCell
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(CustomCell));

        private RelativeLayout listItemLayout;

        private Label menuLabel;

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public CustomCell(String title, ContentPage page)
        {
            listItemLayout = new RelativeLayout
            {
                HeightRequest = 120,
            };

            menuLabel = new Label
            {
                FontSize = 21,
                Text = title,
            };

            listItemLayout.Children.Add(menuLabel,
                    Constraint.RelativeToParent((parent) => (parent.X + 32)),
                    Constraint.RelativeToParent((parent) => (.3 * parent.Height)));

            View = listItemLayout;

            this.Tapped += (s, e) =>
            {
                if (Command == null)
                {
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
