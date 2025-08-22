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
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HeartRateMonitor.Models;
using HeartRateMonitor.Navigation;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace HeartRateMonitor.ViewModels
{
    /// <summary>
    /// MainViewModel class.
    /// Provides commands and methods responsible for application view model state.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Flag indicating whether the application is in started state.
        /// </summary>
        private bool _isStarted = false;

        /// <summary>
        /// Flag indicating whether measurement process is in progress.
        /// </summary>
        private bool _isMeasuring = false;

        /// <summary>
        /// Flag indicating whether measurement process is finished.
        /// </summary>
        private bool _isFinished = false;

        /// <summary>
        /// Number indicating whether measurement process can be stopped automatically or not.
        /// If the value is equal to zero, the measurement process can be stopped automatically.
        /// Otherwise not.
        /// </summary>
        private int _measurementLock = 0;

        /// <summary>
        /// Number representing the range to which the heart rate value is classified,
        /// when the measurement is finished.
        /// If the value is equal to 1, the heart rate value is above the defined heart rate limit.
        /// If the value is equal to 0, the heart rate value is within the defined heart rate limit.
        /// If the value is equal to -1, the heart rate value is within the average resting rate.
        /// </summary>
        private int _measurementResultRange;

        /// <summary>
        /// String representing value of measurement countdown.
        /// </summary>
        private string _measurementCountdown;

        /// <summary>
        /// DateTime object representing starting time point of the measurement process.
        /// </summary>
        private DateTime _measurementStartTimestamp;

        /// <summary>
        /// Flag indicating whether heart rate limit is exceeded.
        /// </summary>
        private bool _isMeasurementResultAlert = false;

        /// <summary>
        /// Number representing current heart rate value.
        /// </summary>
        private int _currentHeartRate = 0;

        /// <summary>
        /// Number representing value of the first digit of the heart rate value.
        /// </summary>
        private int _currentHeartRateFirstNumber = 0;

        /// <summary>
        /// Number representing value of the second digit of the heart rate value.
        /// </summary>
        private int _currentHeartRateSecondNumber = 0;

        /// <summary>
        /// Number representing value of the third digit of the heart rate value.
        /// </summary>
        private int _currentHeartRateThirdNumber = 0;

        /// <summary>
        /// Number representing current heart rate progress bar value.
        /// </summary>
        private double _heartRateProgress;

        /// <summary>
        /// Number representing heart rate limit value.
        /// </summary>
        private int _heartRateLimitValue;

        /// <summary>
        /// Number representing temporary buffered heart rate limit value.
        /// </summary>
        private int _heartRateLimitBufferValue;

        /// <summary>
        /// Number representing heart rate limit slider value.
        /// </summary>
        private double _heartRateLimitSliderValue;

        /// <summary>
        /// Number representing temporary buffered heart rate limit slider value.
        /// </summary>
        private double _heartRateLimitSliderBufferValue;

        /// <summary>
        /// Rectangle object representing heart rate limit indicator position.
        /// </summary>
        private Rectangle _heartRateLimitIndicatorLayoutBounds = new Rectangle(0, 0, 50, 60);

        /// <summary>
        /// Number representing maximum value of the heart rate that can be displayed on the application screen.
        /// </summary>
        private const int MAX_HEART_RATE_VALUE = 999;

        /// <summary>
        /// Number representing maximum value of the heart rate that can be provided by Tizen Sensor API.
        /// </summary>
        private const int MAX_APP_HEART_RATE_VALUE = 220;

        /// <summary>
        /// Number representing lower limit of the average resting rate.
        /// </summary>
        private const int AVERAGE_HEART_RATE_VALUE_LOWER_LIMIT = 61;

        /// <summary>
        /// Number representing upper limit of the average resting rate.
        /// </summary>
        private const int AVERAGE_HEART_RATE_VALUE_UPPER_LIMIT = 76;

        /// <summary>
        /// Number representing default value of the heart rate limit.
        /// </summary>
        private const int HEART_RATE_DEFAULT_LIMIT_VALUE = 180;

        /// <summary>
        /// Number representing measurement time.
        /// </summary>
        private const int MEASUREMENT_TIME = 12;

        /// <summary>
        /// String representing key that is used by application's properties dictionary
        /// to save current heart rate limit value.
        /// </summary>
        private const string HEART_RATE_LIMIT_KEY = "heartRateLimit";

        /// <summary>
        /// Reference to the object of the HeartRateMonitorModel class.
        /// </summary>
        private HeartRateMonitorModel _heartRateMonitorModel;

        /// <summary>
        /// Reference to the IDictionary object that is used to store some data,
        /// so that they are available next time the applications runs.
        /// </summary>
        private IDictionary<string, object> _properties;

        /// <summary>
        /// List of numbers storing measurement values.
        /// It is used to calculate average value of the heart rate (at the end of the measurement process).
        /// </summary>
        private List<int> _measurementValues;

        #endregion

        #region properties

        /// <summary>
        /// An instance of PageNavigation class.
        /// </summary>
        public PageNavigation AppPageNavigation { private set; get; }

        /// <summary>
        /// Sets a property of the view model indicating that the application is in started state.
        /// </summary>
        public ICommand GetStartedCommand { private set; get; }

        /// <summary>
        /// Starts and stops measurement process.
        /// </summary>
        public ICommand ToggleMeasurementCommand { private set; get; }

        /// <summary>
        /// Prepares view model state before displaying SettingsPage.
        /// </summary>
        public ICommand ShowSettingsCommand { private set; get; }

        /// <summary>
        /// Updates view model properties responsible for UI representation of heart rate limit.
        /// </summary>
        public ICommand UpdateHeartRateLimitCommand { private set; get; }

        /// <summary>
        /// Command which shows message about denied privilege (application close).
        /// The command is injected into view model.
        /// </summary>
        public ICommand PrivilegeDeniedInfoCommand { set; get; }

        /// <summary>
        /// Command which handles confirmation of privilege denied dialog.
        /// </summary>
        public ICommand PrivilegeDeniedConfirmedCommand { set; get; }

        /// <summary>
        /// Command which shows message about lack of heart rate sensor (application close).
        /// The command is injected into view model.
        /// </summary>
        public ICommand NotSupportedInfoCommand { set; get; }

        /// <summary>
        /// Command which handles confirmation of lack of heart rate sensor.
        /// </summary>
        public ICommand NotSupportedConfirmedCommand { set; get; }

        /// <summary>
        /// MeasurementStarted event.
        /// It is fired when the measurement process starts.
        /// </summary>
        public event EventHandler MeasurementStarted;

        /// <summary>
        /// MeasurementFinished event.
        /// It is fired when the measurement finishes.
        /// </summary>
        public event EventHandler MeasurementFinished;

        /// <summary>
        /// Property indicating whether measurement process is in progress.
        /// </summary>
        public bool IsMeasuring
        {
            set { SetProperty(ref _isMeasuring, value); }
            get { return _isMeasuring; }
        }

        /// <summary>
        /// Property indicating whether measurement process is finished.
        /// </summary>
        public bool IsFinished
        {
            set { SetProperty(ref _isFinished, value); }
            get { return _isFinished; }
        }

        /// <summary>
        /// Property indicating whether heart rate limit is exceeded.
        /// </summary>
        public bool IsMeasurementResultAlert
        {
            set { SetProperty(ref _isMeasurementResultAlert, value); }
            get { return _isMeasurementResultAlert; }
        }

        /// <summary>
        /// Property indicating whether the application is in started state.
        /// </summary>
        public bool IsStarted
        {
            set
            {
                SetProperty(ref _isStarted, value);
                RestoreHeartRateLimitSliderValue();
            }

            get { return _isStarted; }
        }

        /// <summary>
        /// Property with value representing measurement countdown.
        /// </summary>
        public string MeasurementCountdown
        {
            set { SetProperty(ref _measurementCountdown, value); }
            get
            {
                return "(" + _measurementCountdown + " sec.)";
            }
        }

        /// <summary>
        /// Property with value representing current heart rate.
        /// </summary>
        public int CurrentHeartRate
        {
            set
            {
                SetProperty(ref _currentHeartRate, value);
                SetHeartRateNumbersValues(value);
                SetHeartRateProgress(value);
                UpdateMeasurementResultRange();
                UpdateMeasurementResultAlert();
            }

            get { return _currentHeartRate; }
        }

        /// <summary>
        /// Property with value representing current heart rate progress bar value.
        /// </summary>
        public double HeartRateProgress
        {
            set { SetProperty(ref _heartRateProgress, value); }
            get { return _heartRateProgress; }
        }

        /// <summary>
        /// Property with value representing heart rate limit value.
        /// </summary>
        public int HeartRateLimitValue
        {
            set { SetProperty(ref _heartRateLimitValue, value); }
            get { return _heartRateLimitValue; }
        }

        /// <summary>
        /// Property with value representing temporary buffered heart rate limit value.
        /// </summary>
        public int HeartRateLimitBufferValue
        {
            set { SetProperty(ref _heartRateLimitBufferValue, value); }
            get { return _heartRateLimitBufferValue; }
        }

        /// <summary>
        /// Property with value representing heart rate limit slider value.
        /// </summary>
        public double HeartRateLimitSliderValue
        {
            set
            {
                SetProperty(ref _heartRateLimitSliderValue, value);
                _properties[HEART_RATE_LIMIT_KEY] = value;
                UpdateHeartRateLimitIndicatorPosition(value);
                HeartRateLimitValue = (int)(value * MAX_APP_HEART_RATE_VALUE);
                UpdateMeasurementResultRange();
                UpdateMeasurementResultAlert();
            }

            get { return _heartRateLimitSliderValue; }
        }

        /// <summary>
        /// Property with value representing temporary buffered heart rate limit slider value.
        /// </summary>
        public double HeartRateLimitSliderBufferValue
        {
            set
            {
                SetProperty(ref _heartRateLimitSliderBufferValue, value);
                HeartRateLimitBufferValue = (int)(value * MAX_APP_HEART_RATE_VALUE);
            }

            get { return _heartRateLimitSliderBufferValue; }
        }

        /// <summary>
        /// Property with value representing heart rate limit indicator position.
        /// </summary>
        public Rectangle HeartRateLimitIndicatorLayoutBounds
        {
            set { SetProperty(ref _heartRateLimitIndicatorLayoutBounds, value); }
            get { return _heartRateLimitIndicatorLayoutBounds; }
        }

        /// <summary>
        /// Property representing value of the first digit of the heart rate value.
        /// </summary>
        public int CurrentHeartRateFirstNumber
        {
            set { SetProperty(ref _currentHeartRateFirstNumber, value); }
            get { return _currentHeartRateFirstNumber; }
        }

        /// <summary>
        /// Property representing value of the second digit of the heart rate value.
        /// </summary>
        public int CurrentHeartRateSecondNumber
        {
            set { SetProperty(ref _currentHeartRateSecondNumber, value); }
            get { return _currentHeartRateSecondNumber; }
        }

        /// <summary>
        /// Property representing value of the third digit of the heart rate value.
        /// </summary>
        public int CurrentHeartRateThirdNumber
        {
            set { SetProperty(ref _currentHeartRateThirdNumber, value); }
            get { return _currentHeartRateThirdNumber; }
        }

        /// <summary>
        /// Property representing the range to which the heart rate value is classified,
        /// when the measurement is finished.
        /// </summary>
        public int MeasurementResultRange
        {
            set { SetProperty(ref _measurementResultRange, value); }
            get { return _measurementResultRange; }
        }

        #endregion

        #region methods

        /// <summary>
        /// MainViewModel class constructor.
        /// </summary>
        /// <param name="properties">View model instance properties.</param>
        /// <param name="pageNavigation">Page navigation object.</param>
        public MainViewModel(IDictionary<string, object> properties, PageNavigation pageNavigation)
        {
            _properties = properties;
            AppPageNavigation = pageNavigation;

            GetStartedCommand = new Command(ExecuteGetStartedCommand);
            ToggleMeasurementCommand = new Command(ExecuteToggleMeasurementCommand);
            ShowSettingsCommand = new Command(ExecuteShowSettingsCommand, CanExecuteShowSettingsCommand);
            UpdateHeartRateLimitCommand = new Command(ExecuteUpdateHeartRateLimitCommand);
            PrivilegeDeniedConfirmedCommand = new Command(ExecutePrivilegeDeniedConfirmed);
            NotSupportedConfirmedCommand = new Command(ExecuteNotSupportedConfirmedCommand);
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <returns>The initialization task.</returns>
        public async Task Init()
        {
            _heartRateMonitorModel = new HeartRateMonitorModel();

            _heartRateMonitorModel.HeartRateMonitorDataChanged += ModelOnHeartRateMonitorDataChanged;
            _heartRateMonitorModel.HeartRateSensorNotSupported += ModelOnHeartRateSensorNotSupported;

            if (!await _heartRateMonitorModel.CheckPrivileges())
            {
                Device.StartTimer(TimeSpan.Zero, () =>
                {
                    PrivilegeDeniedInfoCommand?.Execute(null);
                    // return false to run the timer callback only once
                    return false;
                });

                return;
            }

            _heartRateMonitorModel.Init();
        }

        /// <summary>
        /// Updates value of the HeartRateProgress property.
        /// </summary>
        /// <param name="value">Value of heart rate.</param>
        private void SetHeartRateProgress(int value)
        {
            HeartRateProgress = (double)value / MAX_APP_HEART_RATE_VALUE;
        }

        /// <summary>
        /// Updates value of the HeartRateLimitIndicatorLayoutBounds property.
        /// </summary>
        /// <param name="value">Value of heart rate limit.</param>
        private void UpdateHeartRateLimitIndicatorPosition(double value)
        {
            HeartRateLimitIndicatorLayoutBounds = new Rectangle(value, _heartRateLimitIndicatorLayoutBounds.Top,
                _heartRateLimitIndicatorLayoutBounds.Width, _heartRateLimitIndicatorLayoutBounds.Height);
        }

        /// <summary>
        /// Updates values of the CurrentHeartRateFirstNumber, CurrentHeartRateSecondNumber
        /// and CurrentHeartRateThirdNumber properties.
        /// </summary>
        /// <param name="value">Current heart rate value.</param>
        private void SetHeartRateNumbersValues(int value)
        {
            if (value > MAX_HEART_RATE_VALUE)
            {
                return;
            }

            int[] listOfDigits = new int[] { 0, 0, 0 };
            int position = 2;

            while (value > 0)
            {
                listOfDigits[position] = value % 10;
                value = value / 10;
                position -= 1;
            }

            CurrentHeartRateThirdNumber = listOfDigits[2];
            CurrentHeartRateSecondNumber = listOfDigits[1];
            CurrentHeartRateFirstNumber = listOfDigits[0];
        }

        /// <summary>
        /// Handles "HeartRateMonitorDataChanged" event of the HeartRateMonitorModel object.
        /// Updates value of the MeasurementCountdown property.
        /// Updates value of the CurrentHeartRate property.
        /// Adds new value to the list of measurement values.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Arguments passed to the event.</param>
        private void ModelOnHeartRateMonitorDataChanged(object sender, EventArgs e)
        {
            int heartRateValue = _heartRateMonitorModel.GetHeartRate();

            CurrentHeartRate = heartRateValue;
            _measurementValues.Add(heartRateValue);
        }

        /// <summary>
        /// Handles "HeartRateSensorNotSupported" event of the HeartRateMonitorModel object.
        /// Executes NotSupportedInfoCommand command.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Arguments passed to the event.</param>
        private void ModelOnHeartRateSensorNotSupported(object sender, EventArgs e)
        {
            Device.StartTimer(TimeSpan.Zero, () =>
            {
                NotSupportedInfoCommand?.Execute(null);
                // return false to run the timer callback only once
                return false;
            });
        }

        /// <summary>
        /// Handles execution of command which occurs when user confirms privilege denied dialog.
        /// Closes the application.
        /// </summary>
        private void ExecutePrivilegeDeniedConfirmed()
        {
            DependencyService.Get<IApplicationService>()?.Close();
        }

        /// <summary>
        /// Handles execution of command which occurs when user confirms lack of heart rate sensor.
        /// Closes the application.
        /// </summary>
        private void ExecuteNotSupportedConfirmedCommand()
        {
            DependencyService.Get<IApplicationService>()?.Close();
        }

        /// <summary>
        /// Executes UpdateHeartRateLimitCommand command.
        /// Updates value of the HeartRateLimitSliderValue property.
        /// Navigates back to the measurement page by executing NavigateBackCommand command
        /// of the PageNavigation class.
        /// </summary>
        private void ExecuteUpdateHeartRateLimitCommand()
        {
            HeartRateLimitSliderValue = HeartRateLimitSliderBufferValue;
            AppPageNavigation.NavigateBackCommand.Execute(null);
        }

        /// <summary>
        /// Checks whether the ShowSettingsCommand command can be executed.
        /// </summary>
        /// <param name="arg">Command parameter.</param>
        /// <returns>Returns true if the ShowSettingsCommand can be executed, false otherwise.</returns>
        private bool CanExecuteShowSettingsCommand(object arg)
        {
            return !IsMeasuring;
        }

        /// <summary>
        /// Executes the ShowSettingsCommand command.
        /// Updates value of the HeartRateLimitSliderBufferValue property.
        /// Navigates to the settings page by executing NavigateToCommand command
        /// of the PageNavigation class.
        /// </summary>
        /// <param name="obj">Command parameter.</param>
        private void ExecuteShowSettingsCommand(object obj)
        {
            HeartRateLimitSliderBufferValue = HeartRateLimitSliderValue;
            AppPageNavigation.NavigateToCommand.Execute(obj);
        }

        /// <summary>
        /// Executes the GetStartedCommand command.
        /// Updates value of the IsStarted property.
        /// </summary>
        private void ExecuteGetStartedCommand()
        {
            IsStarted = true;
        }

        /// <summary>
        /// Executes the ToggleMeasurementCommand command.
        /// Depending on the IsMeasuring property state it starts or stops measurement process.
        /// Additionally it calls ChangeCanExecute method
        /// to update execution state of the ShowSettingsCommand command.
        /// </summary>
        private void ExecuteToggleMeasurementCommand()
        {
            if (IsMeasuring)
            {
                StopMeasurement(true);
                _measurementLock += 1;
            }
            else
            {
                StartMeasurement();
            }

            ((Command)ShowSettingsCommand).ChangeCanExecute();
        }

        /// <summary>
        /// Starts measurement process.
        /// Sets values of fields and properties
        /// responsible for the correct flow of the measurement process.
        /// Invokes "MeasurementStarted" event.
        /// Sets timer to let the application know
        /// when the measurement process should be stopped automatically.
        /// Executes the StartHeartRateMonitor method of the HeartRateMonitorModel object.
        /// </summary>
        private void StartMeasurement()
        {
            IsFinished = false;
            IsMeasuring = true;
            MeasurementCountdown = MEASUREMENT_TIME.ToString();
            _measurementStartTimestamp = DateTime.Now;
            _measurementValues = new List<int>();
            MeasurementStarted?.Invoke(this, new EventArgs());
            Device.StartTimer(TimeSpan.FromSeconds(MEASUREMENT_TIME), () =>
            {
                if (IsMeasuring && _measurementLock == 0)
                {
                    StopMeasurement();
                    ((Command)ShowSettingsCommand).ChangeCanExecute();
                }

                if (_measurementLock > 0)
                {
                    _measurementLock -= 1;
                }

                return false;
            });

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (!IsMeasuring)
                {
                    return false;
                }

                TimeSpan measurementElapsedTime = DateTime.Now - _measurementStartTimestamp;
                MeasurementCountdown = (MEASUREMENT_TIME - measurementElapsedTime.Seconds).ToString();
                return true;
            });

            _heartRateMonitorModel.StartHeartRateMonitor();
        }

        /// <summary>
        /// Stops measurement process.
        /// Sets values of properties responsible for indicating that the measurement process is finished.
        /// Invokes "MeasurementFinished" event.
        /// Executes the StopHeartRateMonitor method of the HeartRateMonitorModel object.
        /// Updates value of the CurrentHeartRate property by using GetAverageHeartRateValue method.
        /// </summary>
        /// <param name="canceled">Flag indicating if current heart rate value should be reset.</param>
        private void StopMeasurement(bool canceled = false)
        {
            IsMeasuring = false;
            MeasurementFinished?.Invoke(this, new EventArgs());
            _heartRateMonitorModel.StopHeartRateMonitor();

            if (canceled)
            {
                CurrentHeartRate = 0;
                return;
            }

            CurrentHeartRate = GetAverageHeartRateValue();
            IsFinished = true;
        }

        /// <summary>
        /// Calculates average value of the heart rate based on values stored in the _measurementValues list.
        /// </summary>
        /// <returns>Average value of the heart rate.</returns>
        private int GetAverageHeartRateValue()
        {
            int total = 0,
                count = _measurementValues.Count;

            if (count == 0)
            {
                return total;
            }

            total = _measurementValues.Sum();

            return total / count;
        }

        /// <summary>
        /// Updates value of the MeasurementResultRange property.
        /// </summary>
        private void UpdateMeasurementResultRange()
        {
            if (CurrentHeartRate > HeartRateLimitValue)
            {
                MeasurementResultRange = 1;
            }
            else if (CurrentHeartRate >= AVERAGE_HEART_RATE_VALUE_LOWER_LIMIT &&
                     CurrentHeartRate <= AVERAGE_HEART_RATE_VALUE_UPPER_LIMIT)
            {
                MeasurementResultRange = -1;
            }
            else
            {
                MeasurementResultRange = 0;
            }
        }

        /// <summary>
        /// Updates value of the IsMeasurementResultAlert property.
        /// </summary>
        private void UpdateMeasurementResultAlert()
        {
            IsMeasurementResultAlert = MeasurementResultRange == 1;
        }

        /// <summary>
        /// Restores heart rate limit slider value.
        /// It tries to obtain this value from application properties dictionary.
        /// If it is not available in the dictionary, the value is calculated based on app constants.
        /// </summary>
        private void RestoreHeartRateLimitSliderValue()
        {
            if (_properties.ContainsKey(HEART_RATE_LIMIT_KEY))
            {
                HeartRateLimitSliderValue = (double)_properties[HEART_RATE_LIMIT_KEY];
            }
            else
            {
                HeartRateLimitSliderValue = (double)HEART_RATE_DEFAULT_LIMIT_VALUE / MAX_APP_HEART_RATE_VALUE;
            }
        }

        #endregion
    }
}