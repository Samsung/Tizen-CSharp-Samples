/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using SquatCounter.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace SquatCounter.ViewModels
{
    /// <summary>
    /// Guide page view model which is parent to all guide page view models.
    /// </summary>
    public class GuidePageViewModel : ViewModelBase
    {
        /// <summary>
        /// Background image path.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Navigates to squat counter page.
        /// </summary>
        public ICommand GoToSquatCounterPageCommand { get; set; }

        /// <summary>
        /// Initializes GuidePageViewModel instance.
        /// </summary>
        public GuidePageViewModel()
        {
            GoToSquatCounterPageCommand = new Command(ExecuteGoToSquatCounterPageCommand);
        }

        /// <summary>
        /// Handles execution of GoToSquatCounterPageCommand.
        /// </summary>
        public void ExecuteGoToSquatCounterPageCommand()
        {
            PageNavigationService.Instance.GoToSquatCounterPage();
        }
    }
}