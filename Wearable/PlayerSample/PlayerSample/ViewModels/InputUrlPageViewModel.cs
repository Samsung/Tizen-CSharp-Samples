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

using Xamarin.Forms;
using PlayerSample.Views;
using PlayerSample.Models;

namespace PlayerSample.ViewModels
{
    /// <summary>
    /// ViewModel for InputUrlPage.
    /// </summary>
    class InputUrlPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputUrlPageViewModel"/> class
        /// </summary>
        /// <param name="navigation">Navigation instance</param>
        public InputUrlPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        /// <summary>
        /// Gets the UrlText that a user inputs
        /// </summary>
        private string _urlText;
        public string UrlText
        {
            get => _urlText;
            set
            {
                if (_urlText != value)
                {
                    _urlText = value;

                    OnPropertyChanged(nameof(UrlText));

                    OpenCommand.ChangeCanExecute();
                }
            }
        }

        /// <summary>
        /// Gets the Navigation instance to push new pages properly
        /// </summary>
        public INavigation Navigation { get; protected set; }

        /// <summary>
        /// Gets or sets command for pushing new page
        /// </summary>
        public Command OpenCommand => new Command(PushPlayPage);

        /// <summary>
        /// Pushes new page for set streaming url
        /// </summary>
        private void PushPlayPage()
        {
            if (UrlText != null)
            {
                Navigation.PushModalAsync(new PlayPage(UrlText));
            }
            else
            {
                Logger.Log("Selected Item is null");
            }
        }
    }
}
