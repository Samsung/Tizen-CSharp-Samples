/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using AppStatistics.Views;
using Xamarin.Forms;
using System.Windows.Input;
using AppStatistics.Utils;

namespace AppStatistics.ViewModels
{
    /// <summary>
    /// The application's main view model class (abstraction of the view).
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Reference to object handling navigation between pages obtained in constructor using DependencyService.
        /// </summary>
        private readonly IPageNavigation _navigation;

        #endregion

        #region properties

        /// <summary>
        /// Navigates to range selection page.
        /// </summary>
        public ICommand GoToRangeSelectionCommand { get; private set; }

        /// <summary>
        /// Navigates to battery page.
        /// </summary>
        public ICommand GoToBatteryCommand { get; private set; }

        /// <summary>
        /// Navigates to launch count page.
        /// </summary>
        public ICommand GoToLaunchCountCommand { get; private set; }

        /// <summary>
        /// Navigates to menu page.
        /// </summary>
        public ICommand GoToMenuCommand { get; private set; }

        /// <summary>
        /// Closes application.
        /// </summary>
        public ICommand CloseApplicationCommand { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public MainViewModel()
        {
            _navigation = DependencyService.Get<IPageNavigation>();

            InitCommands();
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        private void InitCommands()
        {
            GoToRangeSelectionCommand = new Command(ExecuteGoToRangeSelection);
            GoToBatteryCommand = new Command(ExecuteGoToBattery);
            GoToLaunchCountCommand = new Command<Range>(ExecuteGoToLaunchCount);
            GoToMenuCommand = new Command(ExecuteGoToMenu);
            CloseApplicationCommand = new Command(ExecuteCloseApplication);
        }

        /// <summary>
        /// Handles execution of "GoToRangeSelectionCommand".
        /// </summary>
        private void ExecuteGoToRangeSelection()
        {
            _navigation.CreateRangeSelectionPage();
        }

        /// <summary>
        /// Handles execution of "GoToBatteryCommand".
        /// </summary>
        private void ExecuteGoToBattery()
        {
            StatisticsViewModel batteryViewModel = new StatisticsViewModel(Range.Battery);
            _navigation.CreateBatteryPage(batteryViewModel);
        }

        /// <summary>
        /// Handles execution of "GoToLaunchCountCommand".
        /// </summary>
        /// <param name="option">Specifies which range should be displayed.</param>
        private void ExecuteGoToLaunchCount(Range option)
        {
            StatisticsViewModel launchCountViewModel = new StatisticsViewModel(option);
            _navigation.CreateLaunchCountPage(launchCountViewModel);
        }

        /// <summary>
        /// Handles execution of "GoToMenuCommand".
        /// </summary>
        private void ExecuteGoToMenu()
        {
            _navigation.CreateMenuPage();
        }

        /// <summary>
        /// Handles execution of "CloseApplicationCommand".
        /// </summary>
        private void ExecuteCloseApplication()
        {
            _navigation.Close();
        }

        #endregion
    }
}
