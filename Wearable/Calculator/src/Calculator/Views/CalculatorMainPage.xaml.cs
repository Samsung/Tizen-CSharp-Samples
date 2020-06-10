
//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System.Threading;
using System.Threading.Tasks;

using Calculator.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculator.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculatorMainPage : ContentPage
    {
        private Mutex CounterMutex = new Mutex(false, "counter_mutex");
        private int counter;

        public CalculatorMainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = MainPageViewModel.Instance;

            MessagingCenter.Subscribe<MainPageViewModel, string>(this, "alert", async (sender, arg) =>
            {
                CounterMutex.WaitOne();
                counter++;
                CounterMutex.ReleaseMutex();

                AlertToast.IsVisible = true;
                AlertToast.Text = arg.ToString();
                await CloseAlertToast();
            });
        }

        /// <summary>
        /// This method closes alert toast after 1.5 seconds.
        /// </summary>
        private async Task CloseAlertToast()
        {
            await Task.Delay(1500);
            CounterMutex.WaitOne();
            if (--counter <= 0)
            {
                counter = 0;
                AlertToast.IsVisible = false;
            }

            CounterMutex.ReleaseMutex();
        }
    }
}