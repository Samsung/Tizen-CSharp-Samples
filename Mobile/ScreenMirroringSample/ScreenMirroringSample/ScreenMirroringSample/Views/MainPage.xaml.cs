/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
namespace ScreenMirroringSample
{
    /// <summary>
    /// Main ContentPage.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        NavigationPage AppMainPage;

        public MainPage(NavigationPage page)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            AppMainPage = page;
        }
        protected override async void OnDisappearing()
        {
            await ((BindingContext as MainViewModel).OnDisappearing());
            var timer = Task.Run(async () => { await Task.Delay(2000); });
            base.OnDisappearing();
        }
    }
}
