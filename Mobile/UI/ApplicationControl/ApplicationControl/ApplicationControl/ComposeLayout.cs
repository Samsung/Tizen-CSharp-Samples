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

using Xamarin.Forms;
using ApplicationControl.Extensions;

namespace ApplicationControl
{
    /// <summary>
    /// A class for an compose layout as a part of the main layout
    /// </summary>
    public class ComposeLayout : RelativeLayout
    {
        /// <summary>
        /// A constructor of the ComposeLayout class
        /// </summary>
        public ComposeLayout() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// To initialize components of the compose layout
        /// </summary>
        void InitializeComponent()
        {
            Children.Add(
                new Image
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,

                    Source = "bar_compose.png",
                    Aspect = Aspect.Fill,
                },
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.2107;
                }));

            var message = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                TextColor = Color.Gray,
                BackgroundColor = Color.FromRgb(255, 255, 255),
            };
            BindingContextChanged += (s, e) =>
            {
                if (BindingContext == null)
                {
                    return;
                }

                message.Text = ((MainViewModel)BindingContext).Message.Text;
            };

            Children.Add(
                message,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.0417;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.2727;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.9166;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.4546;
                }));

            var address = new Entry
            {
                Placeholder = "mail to",
                BackgroundColor = Color.FromRgb(210, 210, 210),
            };
            address.Completed += (s, e) =>
            {
                if (BindingContext == null)
                {
                    return;
                }

                ((MainViewModel)BindingContext).Message.To = ((Entry)s).Text;
            };

            Children.Add(
                address,
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.7893;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.622;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.2107;
                }));


            var sendButton = new CustomImageButton
            {
                Source = "send_button.jpg",
            };
            sendButton.Clicked += (s, e) =>
            {
                if (BindingContext == null)
                {
                    return;
                }

                ((MainViewModel)BindingContext).SendMessage();
            };

            Children.Add(
                sendButton,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.622;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.773;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.378;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.227;
                }));
        }
    }
}