/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Stopwatch.Models;
using Xamarin.Forms;

namespace Stopwatch.ViewModels
{
    /// <summary>
    /// The view model class of the stopwatch.
    /// </summary>
    class StopwatchViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Time properties update interval (in milliseconds).
        /// </summary>
        private const int TIME_UPDATE_INTERVAL_MS = 50;

        /// <summary>
        /// Stopwatch model class instance.
        /// </summary>
        private StopwatchModel _model;

        /// <summary>
        /// Current stopwatch elapsed time.
        /// Backing field for ElapsedTime property.
        /// </summary>
        private TimeSpan _elapsedTime;

        /// <summary>
        /// Flag indicating if stopwatch is paused.
        /// Backing field for Paused property.
        /// </summary>
        private bool _paused;

        #endregion

        #region properties

        /// <summary>
        /// Command which toggles the stopwatch state (paused, running).
        /// </summary>
        public ICommand ToggleStartPauseCommand { get; private set; }

        /// <summary>
        /// Command which resets the stopwatch to initial state.
        /// </summary>
        public ICommand ResetCommand { get; private set; }

        /// <summary>
        /// Command which registers new lap.
        /// </summary>
        public ICommand LapCommand { get; private set; }

        /// <summary>
        /// Stopwatch elapsed time.
        /// Property updated by timer with defined interval.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            private set => SetProperty(ref _elapsedTime, value);
        }

        /// <summary>
        /// Flag indicating if stopwatch is paused.
        /// </summary>
        public bool Paused
        {
            get => _paused;
            private set => SetProperty(ref _paused, value);
        }

        /// <summary>
        /// A collection of stopwatch laps (view models).
        /// </summary>
        public ObservableCollection<LapViewModel> Laps { get; private set; }

        /// <summary>
        /// Active lap view model.
        /// No lap is ever active so property getter always returns null.
        /// Used to block selecting laps.
        /// </summary>
        public LapViewModel ActiveLap
        {
            get => null;
            set => OnPropertyChanged();
        }

        #endregion

        #region methods

        /// <summary>
        /// The view model constructor.
        /// </summary>
        public StopwatchViewModel()
        {
            _model = new StopwatchModel();

            _elapsedTime = _model.ElapsedTime;
            _paused = _model.Paused;
            Laps = new ObservableCollection<LapViewModel>();

            InitCommands();
            _model.LapsAdded += ModelOnLapsAdded;
        }

        /// <summary>
        /// Handles laps added event from the stopwatch model.
        /// Updates collection of laps.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="lapsAddedEventArgs">Event arguments.</param>
        private void ModelOnLapsAdded(object sender, LapsAddedEventArgs lapsAddedEventArgs)
        {
            foreach (var lap in lapsAddedEventArgs.Laps)
            {
                Laps.Insert(0, new LapViewModel(lap));
            }
        }

        /// <summary>
        /// Initializes view model commands.
        /// </summary>
        private void InitCommands()
        {
            ToggleStartPauseCommand = new Command(ExecuteToggleStartPause);
            ResetCommand = new Command(ExecuteReset, CanExecuteReset);
            LapCommand = new Command(ExecuteLap, CanExecuteLap);
        }

        /// <summary>
        /// Handles execution of toggle start/pause command.
        /// Updates Paused property value and starts timer which updates time properties.
        /// Updates execution permission of stopwatch state dependent commands.
        /// </summary>
        private void ExecuteToggleStartPause()
        {
            _model.ToggleStartPause();

            Paused = _model.Paused;

            if (!Paused)
            {
                Device.StartTimer(TimeSpan.FromMilliseconds(TIME_UPDATE_INTERVAL_MS), OnTimerTick);
            }

            ElapsedTime = _model.ElapsedTime;
            ((Command)ResetCommand).ChangeCanExecute();
            ((Command)LapCommand).ChangeCanExecute();
        }

        /// <summary>
        /// Handles tick of the timer.
        /// Updates time properties (elapsed time) of stopwatch and its laps.
        /// </summary>
        /// <returns>False if timer should be stopped, true otherwise.</returns>
        private bool OnTimerTick()
        {
            ElapsedTime = _model.ElapsedTime;
            foreach (var lap in Laps)
            {
                lap.Update();
            }

            return !_model.Paused;
        }

        /// <summary>
        /// Handles execution of reset command.
        /// Calls model to reset itself.
        /// Clears laps collection.
        /// Updates execution permission of stopwatch state dependent commands.
        /// </summary>
        private void ExecuteReset()
        {
            _model.Reset();
            ElapsedTime = _model.ElapsedTime;
            Paused = _model.Paused;
            Laps.Clear();
            ((Command)ResetCommand).ChangeCanExecute();
            ((Command)LapCommand).ChangeCanExecute();
        }

        /// <summary>
        /// Returns true if reset command can be executed (stopwatch is running),
        /// false otherwise.
        /// </summary>
        /// <returns>True if reset command can be executed, false otherwise</returns>
        private bool CanExecuteReset()
        {
            return _model.Running;
        }

        /// <summary>
        /// Handles execution of lap command.
        /// Calls model to register new lap.
        /// </summary>
        private void ExecuteLap()
        {
            _model.Lap();
        }

        /// <summary>
        /// Returns true if lap command can be executed (stopwatch is running and not paused),
        /// false otherwise.
        /// </summary>
        /// <returns>True if lap command can be executed, false otherwise.</returns>
        private bool CanExecuteLap()
        {
            return _model.Running && !_model.Paused;
        }

        #endregion
    }
}
