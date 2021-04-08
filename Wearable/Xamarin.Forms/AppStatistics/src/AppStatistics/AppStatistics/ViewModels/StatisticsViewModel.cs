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
using AppStatistics.Interfaces;
using AppStatistics.Models;
using AppStatistics.Utils;
using AppStatistics.Views;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AppStatistics.ViewModels
{
    /// <summary>
    /// View model class responsible for information about the application statistics.
    /// </summary>
    public class StatisticsViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Reference to object handling navigation between pages obtained in constructor using DependencyService.
        /// </summary>
        private readonly IPageNavigation _navigation;

        /// <summary>
        /// Gives information about the applications and their statistics.
        /// </summary>
        private ApplicationStatisticsModel _applicationStatisticsModel;

        /// <summary>
        /// List of the applications and informations about them.
        /// </summary>
        private List<ApplicationStatisticsItem> _applications;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets list of the applications and informations about them.
        /// </summary>
        public List<ApplicationStatisticsItem> Applications
        {
            get => _applications;
            set => SetProperty(ref _applications, value);
        }

        /// <summary>
        /// Navigates to launch count details page.
        /// </summary>
        public Command GoToLaunchCountDetailsCommand { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        /// <param name="option">Specifies which range should be considered.</param>
        public StatisticsViewModel(Range option)
        {
            _navigation = DependencyService.Get<IPageNavigation>();

            _applicationStatisticsModel = new ApplicationStatisticsModel();

            Applications = _applicationStatisticsModel.GetApplicationStatistics(option);

            if (option == Range.Battery && Applications.Count == 0)
            {
                DependencyService.Get<IInformationPopupService>().ShowErrorPopup();
            }

            InitCommands();
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        private void InitCommands()
        {
            GoToLaunchCountDetailsCommand = new Command<int>(ExecuteGoToLaunchCountDetails);
        }

        /// <summary>
        /// Handles execution of "GoToLaunchCountDetailsCommand".
        /// </summary>
        /// <param name="id">Id of the application which is required to be displayed.</param>
        private void ExecuteGoToLaunchCountDetails(int id)
        {
            ApplicationStatisticsItem applicationItem = Applications.FirstOrDefault(x => x.ID == id);
            _navigation.CreateLaunchCountDetailsPage(new LaunchCountDetailsViewModel(applicationItem));
        }

        #endregion
    }
}
