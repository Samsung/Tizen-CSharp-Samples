/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace SystemInfo.Tizen.Wearable.View
{
    /// <summary>
    /// Interaction logic for DisplayPage.xaml
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayPage : CirclePage
    {
        /// <summary>
        /// Default class constructor.
        /// </summary>
        public DisplayPage()
        {
            InitializeComponent();
        }
    }
}