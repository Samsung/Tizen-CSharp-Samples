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
using System.Linq;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sensors
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : IndexPage
    {
        public Main()
        {
            InitializeComponent();
            var pages = AppDomain.CurrentDomain.GetAssemblies()
                                                .SelectMany(m => m.GetTypes())
                                                .Where(m => m.Namespace == this.GetType().Namespace + "." + nameof(Sensors.Pages));

            foreach (var page in pages)
            {
                Children.Add(Activator.CreateInstance(page) as ContentPage);
            }
        }
    }
}