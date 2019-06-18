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
using Xamarin.Forms;
using Tizen;
using Tizen.Uix.InputMethodManager;

namespace IMEManager
{
    internal class ResultPage : ContentPage
    {
        public ResultPage(string detail)
        {
            Label label = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 36,
                Text = detail
            };

            this.Title = "IMEManager Sample";
            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    label
                }
            };

            switch (detail)
            {
                case "ShowIMEList":
                    Manager.ShowIMEList();
                    break;
                case "ShowIMESelector":
                    Manager.ShowIMESelector();
                    break;
                case "IsIMEEnabled":
                    string appId = "ise-default";
                    if (Manager.IsIMEEnabled(appId))
                    {
                        label.Text = "IME state : On";
                    }
                    else
                    {
                        label.Text = "IME state : Off";
                    }

                    break;
                case "GetActiveIME":
                    label.Text = "Active IME : " + Manager.GetActiveIME();
                    break;
                case "GetEnabledIMECount":
                    label.Text = "IME count : " + Manager.GetEnabledIMECount().ToString();
                    break;
                default:
                    break;
            }
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
