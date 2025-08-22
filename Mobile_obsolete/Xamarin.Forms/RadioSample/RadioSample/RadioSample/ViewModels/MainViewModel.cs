/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
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

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RadioSample
{
    /// <summary>
    /// Represents Main ViewModel.
    /// </summary>
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            InitializeCommands();

            RadioController.ScanUpdated += (s, e) => Frequencies.Add(e.Frequency);

            RadioController.ScanCompleted += (s, e) => OnPropertyChanged(nameof(RadioState));

            _selectedFrequency = RadioController.Frequency;

            RadioController.StartScan();
        }

        private IRadioController RadioController => DependencyService.Get<IRadioController>();

        /// <summary>
        /// Returns the text for the start button.
        /// </summary>
        public string StartText => IsPlaying ? "Stop" : "Start";

        /// <summary>
        /// Returns the current radio state.
        /// </summary>
        public RadioState RadioState => RadioController.State;

        /// <summary>
        /// Returns a bool value indicating whether the radio is scanning state.
        /// </summary>
        public bool IsScanning => RadioState == RadioState.Scanning;

        /// <summary>
        /// Returns a bool value indicating whether the radio is playing state.
        /// </summary>
        public bool IsPlaying => RadioState == RadioState.Playing;

        /// <summary>
        /// Returns a collection for the frequency list view.
        /// </summary>
        public ObservableCollection<int> Frequencies { get; }
            = new ObservableCollection<int>();

        private object _selectedFrequency;

        /// <summary>
        /// Gets or sets the selected frequency in the frequency list view.
        /// </summary>
        public object SelectedFrequency
        {
            // Always returns null to disable the selection effect.
            get => null;
            set
            {
                if (value != null && _selectedFrequency != value)
                {
                    var freq = (int)value;

                    // Sets radio frequency accordingly.
                    RadioController.Frequency = freq;
                    _selectedFrequency = value;

                    OnPropertyChanged(nameof(CurrentFrequencyText));
                }

                OnPropertyChanged(nameof(SelectedFrequency));
            }
        }

        /// <summary>
        /// Gets the display text of the current frequency.
        /// </summary>
        public string CurrentFrequencyText => $"Current frequency : {RadioController.Frequency} Hz";

        #region Commands

        // Commands for the buttons.
        public ICommand StartCommand { get; protected set; }
        public ICommand ScanCommand { get; protected set; }
        public ICommand SeekUpCommand { get; protected set; }
        public ICommand SeekDownCommand { get; protected set; }

        /// <summary>
        /// Initialize commands.
        /// </summary>
        private void InitializeCommands()
        {
            StartCommand = new Command(() =>
            {
                if (IsPlaying)
                {
                    RadioController.Stop();
                }
                else
                {
                    RadioController.Start();
                }

                OnPropertyChanged(nameof(StartText));
                OnPropertyChanged(nameof(RadioState));
            });

            ScanCommand = new Command(() =>
            {
                Frequencies.Clear();

                RadioController.StartScan();

                OnPropertyChanged(nameof(RadioState));
            });

            SeekUpCommand = new Command(
                async () => await Seek(RadioController.SeekUpAsync));

            SeekDownCommand = new Command(
                async () => await Seek(RadioController.SeekDownAsync));
        }
        #endregion

        private bool _seeking;

        /// <summary>
        /// Seeks next frequency.
        /// </summary>
        /// <param name="seekFunc">The function indicating the seek direction.</param>
        /// <returns>A task that represents the asynchronous seek operation.</returns>
        private async Task Seek(Func<Task> seekFunc)
        {
            // Returns if a previous seek operation is not done yet.
            if (_seeking)
            {
                return;
            }

            _seeking = true;
            await seekFunc();
            _seeking = false;

            OnPropertyChanged(nameof(CurrentFrequencyText));
        }

        /// <summary>
        /// Notifies change of a property.
        /// </summary>
        /// <param name="propertyName">A property name.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
