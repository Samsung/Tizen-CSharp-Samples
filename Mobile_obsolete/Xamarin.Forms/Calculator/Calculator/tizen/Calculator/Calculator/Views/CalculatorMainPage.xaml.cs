/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Threading;
using Calculator.ViewModels;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Calculator.Views
{
    /// <summary>
    /// A Calculator portrait layout class. </summary>
    public partial class CalculatorMainPage : ContentPage
    {
        private Mutex CounterMutex = new Mutex(false, "counter_mutex");
        private int counter;

        public CalculatorMainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = MainPageViewModel.Instance;

            MessagingCenter.Subscribe<MainPageViewModel, string>(this, "alert", (sender, arg) =>
            {
                CounterMutex.WaitOne();
                counter++;
                CounterMutex.ReleaseMutex();

                AlertToast.IsVisible = true;
                AlertToast.Text = arg.ToString();
                CloseAlertToast();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ExpressionLabelPropertyChanged(this, null);
        }

        /// <summary>
        /// A method close alert toast after 1.5 seconds later. </summary>
        async void CloseAlertToast()
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

        /// <summary>
        /// A event handler for updating scroll position of the expression label's scroll view
        /// </summary>
        /// <param name="sender">A expression label</param>
        /// <param name="e">A event handing argument</param>
        private void ExpressionLabelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (ExpressionScrollView == null ||
                ExpressionLabel == null)
            {
                return;
            }

            ExpressionScrollView.ScrollToAsync(0, ExpressionScrollView.ContentSize.Height, true);
        }
    }
}
