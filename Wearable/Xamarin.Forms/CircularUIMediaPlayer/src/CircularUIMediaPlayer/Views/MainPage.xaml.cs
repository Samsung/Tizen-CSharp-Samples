/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
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

using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using CircularUIMediaPlayer.ViewModels;
using System.Threading.Tasks;

namespace CircularUIMediaPlayer.Views
{
    /// <summary>
    /// MainPage class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            ((MainPageModel)BindingContext).DisplayMessage += DisplayMessage;
        }

        /// <summary>
        /// Display the message
        /// </summary>
        /// <param name="method">method name</param>
        /// <param name="message">message</param>
        /// <returns>Task</returns>
        Task DisplayMessage(string method, string message)
        {
            return DisplayAlert(method, message, "OK");
        }

        /// <summary>
        /// Invoked when the button is clicked
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">EventArgs</param>
        private async void Clicked(object sender, System.EventArgs e)
        {
            // Show MediaPlayPage
            await Navigation.PushAsync(new MediaPlayPage((MainPageModel)BindingContext));
        }
    }
}