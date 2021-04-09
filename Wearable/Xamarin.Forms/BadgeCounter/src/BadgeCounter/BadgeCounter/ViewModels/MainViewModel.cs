/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using BadgeCounter.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BadgeCounter.ViewModels
{
    /// <summary>
    /// Main view model class for the application.
    /// Encapsulates presentation logic and state of the application.
    /// </summary>
    class MainViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Instance of badge counter model.
        /// Allows to manage badge counter for current application.
        /// </summary>
        private BadgeCounterModel _badgeCounterModel;

        #endregion

        #region properties

        /// <summary>
        /// Current value of the badge counter.
        /// </summary>
        public int BadgeCounterValue
        {
            get => _badgeCounterModel.Value;
            set => _badgeCounterModel.Value = value;
        }

        /// <summary>
        /// Indicates if badge counter auto-increment feature in turned on.
        /// </summary>
        public bool AutoIncrement
        {
            get => _badgeCounterModel.AutoIncrement;
            set
            {
                if (value == AutoIncrement)
                {
                    return;
                }

                _badgeCounterModel.AutoIncrement = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Increases the badge counter value by one.
        /// </summary>
        public ICommand IncreaseCommand { get; }

        /// <summary>
        /// Decreases the badge counter value by one.
        /// </summary>
        public ICommand DecreaseCommand { get; }

        /// <summary>
        /// Resets the badge counter to default value.
        /// </summary>
        public ICommand ResetCommand { get; }

        #endregion

        #region methods

        /// <summary>
        /// Creates instance of the view model.
        /// </summary>
        public MainViewModel()
        {
            _badgeCounterModel = new BadgeCounterModel();
            _badgeCounterModel.Changed += OnBadgeCounterModelChanged;

            IncreaseCommand = new Command(ExecuteIncreaseCommand);
            DecreaseCommand = new Command(ExecuteDecreaseCommand, CanExecuteDecrease);
            ResetCommand = new Command(ExecuteResetCommand);
        }

        /// <summary>
        /// Handles "Changed" event of badge counter model.
        /// Notifies about update of badge counter value.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void OnBadgeCounterModelChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(BadgeCounterValue));
            ((Command)DecreaseCommand).ChangeCanExecute();
        }

        /// <summary>
        /// Handles execution of the "IncreaseCommand" command.
        /// Increases value of the badge counter by one.
        /// </summary>
        private void ExecuteIncreaseCommand()
        {
            BadgeCounterValue++;
        }

        /// <summary>
        /// Handles execution of the "DecreaseCommand" command.
        /// Decreases value of the badge counter by one.
        /// </summary>
        private void ExecuteDecreaseCommand()
        {
            if (BadgeCounterValue == 0)
            {
                return;
            }

            BadgeCounterValue--;
        }

        /// <summary>
        /// Returns true if "DecreaseCommand" can be executed, false otherwise.
        /// </summary>
        /// <returns>True if "DecreaseCommand" can be executed.</returns>
        private bool CanExecuteDecrease()
        {
            return BadgeCounterValue > 0;
        }

        /// <summary>
        /// Handles execution of the "ResetCommand" command.
        /// Calls model to reset badge counter to default value.
        /// </summary>
        private void ExecuteResetCommand()
        {
            _badgeCounterModel.Reset();
        }

        #endregion
    }
}
