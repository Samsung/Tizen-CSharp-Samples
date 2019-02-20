//Copyright 2019 Samsung Electronics Co., Ltd
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

using PlayerSample.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace PlayerSample.ViewModels
{
    /// <summary>
    /// ViewModel class for the Main Page
    /// </summary>
    class MainPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets command for pushing new page
        /// </summary>
        public ICommand FileCommand { get; protected set; }
        public ICommand StreamCommand { get; protected set; }

        /// <summary>
        /// Gets the Navigation instance to push new pages properly
        /// </summary>
        public INavigation Navigation { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class
        /// </summary>
        /// <param name="navigation">Navigation instance</param>
        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            FileCommand = new Command(PushListPage);
            StreamCommand = new Command(PushInputUrlPage);
        }

        /// <summary>
        /// Pushes new page for getting the list of files.
        /// </summary>
        private void PushListPage()
        {
            Navigation.PushModalAsync(new ListPage());
        }

        /// <summary>
        /// Pushes new page for getting url.
        /// </summary>
        private void PushInputUrlPage()
        {
            Navigation.PushModalAsync(new InputUrlPage());
        }
    }
}
