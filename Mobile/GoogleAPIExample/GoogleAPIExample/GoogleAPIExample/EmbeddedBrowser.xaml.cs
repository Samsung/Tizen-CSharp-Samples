/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
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
using Xamarin.Forms.Xaml;

namespace GoogleAPIExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EmbeddedBrowser : ContentPage, IEmbeddedBrowser
    {
        public EmbeddedBrowser(string title)
        {
            Title = title;
            InitializeComponent();
        }

        public event EventHandler Cancelled;

        public void Start(string url)
        {
            Broswer.Source = url;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void OnCancel(object sender, EventArgs e)
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }
    }
}