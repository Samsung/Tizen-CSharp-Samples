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
using System;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace SquatCounter.ViewModels
{
    /// <summary>
    /// Provides squat counter page view abstraction.
    /// </summary>
    public class SquatCounterPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Backing field of IsCounting property.
        /// </summary>
        private bool _isCounting;

        /// <summary>
        /// Backing field of SquatsCount property.
        /// </summary>
        private int _squatsCount;

        /// <summary>
        /// Backing field of Time property.
        /// </summary>
        private string _time;

        /// <summary>
        /// Field containing reference to squat counter service.
        /// </summary>
        private SquatCounterService _squatService;

        /// <summary>
        /// Field holding seconds count.
        /// </summary>
        private int _seconds;

        /// <summary>
        /// Field containing Timer object.
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// Changes squat counting service state depending on actual state.
        /// </summary>
        public ICommand ChangeServiceStateCommand { get; }

        /// <summary>
        /// Resets squat count.
        /// </summary>
        public ICommand ResetCommand { get; }

        /// <summary>
        /// Number of squats made.
        /// </summary>
        public int SquatsCount
        {
            get => _squatsCount;
            set => SetProperty(ref _squatsCount, value);
        }

        /// <summary>
        /// Property indicating if squat counting is enabled.
        /// </summary>
        public bool IsCounting
        {
            get => _isCounting;
            set => SetProperty(ref _isCounting, value);
        }

        /// <summary>
        /// Time in minutes and seconds.
        /// </summary>
        public string Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        /// <summary>
        /// Initializes SquatCounterPageViewModel class instance.
        /// </summary>
        public SquatCounterPageViewModel()
        {
            IsCounting = true;
            _seconds = 0;
            _time = "00:00";

            _squatService = new SquatCounterService();
            _squatService.SquatsUpdated += ExecuteSquatsUpdatedCallback;

            _timer = new Timer(1000);
            _timer.Elapsed += TimerTick;
            _timer.Start();

            ChangeServiceStateCommand = new Command(ExecuteChangeServiceStateCommand);
            ResetCommand = new Command(ExecuteResetCommand);

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                navigationPage.Popped += OnPagePopped;
            }
        }

        /// <summary>
        /// Handles execution of ChangeServiceStateCommand.
        /// </summary>
        private void ExecuteChangeServiceStateCommand()
        {
            if (IsCounting)
            {
                _timer.Stop();
                _squatService.Stop();
            }
            else
            {
                _timer.Start();
                _squatService.Start();
            }

            IsCounting = !IsCounting;
        }

        /// <summary>
        /// Handles execution of ResetCommand.
        /// </summary>
        private void ExecuteResetCommand()
        {
            _squatService.Reset();
            Time = "00:00";
            _seconds = 0;
        }

        /// <summary>
        /// Handles execution of Elapsed event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void TimerTick(object sender, EventArgs e)
        {
            _seconds++;
            Time = TimeSpan.FromSeconds(_seconds).ToString("mm\\:ss");
        }

        /// <summary>
        /// Handles execution of SquatsUpdated event callback.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="squatsCount">Squat count value.</param>
        private void ExecuteSquatsUpdatedCallback(object sender, int squatsCount)
        {
            SquatsCount = squatsCount;
        }

        private void OnPagePopped(object sender, NavigationEventArgs e)
        {
            _timer.Elapsed -= TimerTick;
            _timer.Close();
            _squatService.SquatsUpdated -= ExecuteSquatsUpdatedCallback;
            _squatService.Dispose();

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                navigationPage.Popped -= OnPagePopped;
            }
        }
    }
}