
//Copyright 2018 Samsung Electronics Co., Ltd
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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using HeartRateMonitor.Model;
using Tizen;
using HeartRateMonitor.Views;

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
        /// Flag indicating whether measurement process is in progress.
        /// </summary>
        private bool isMeasuring = false;

        /// <summary>
        /// Flag indicating whether measurement process is finished.
        /// </summary>
        private bool isFinished = false;

        /// <summary>
        /// Flag indicating whether measurements should be included in result.
        /// </summary>
        private bool isMeasurementCounted = false;

        /// <summary>
        /// Number indicating whether measurement process can be stopped automatically or not.
        /// If the value is equal to zero, the measurement process can be stopped automatically.
        /// Otherwise not.
        /// </summary>
        private int measurementLock = 0;

        /// <summary>
        /// Number representing the range to which the heart rate value is classified,
        /// when the measurement is finished.
        /// If the value is equal to 1, the heart rate value is above the defined heart rate limit.
        /// If the value is equal to 0, the heart rate value is within the defined heart rate limit.
        /// If the value is equal to -1, the heart rate value is within the average resting rate.
        /// </summary>
        private int measurementResultRange;

        /// <summary>
        /// String representing value of measurement countdown.
        /// </summary>
        private int measurementCountdown;

        /// <summary>
        /// DateTime object representing starting time point of the measurement process.
        /// </summary>
        private DateTime measurementStartTimestamp;

        /// <summary>
        /// Flag indicating whether heart rate limit is exceeded.
        /// </summary>
        private bool isMeasurementResultAlert = false;

        /// <summary>
        /// Number representing current heart rate value.
        /// </summary>
        private int currentHeartRate = 0;

        /// <summary>
        /// Number representing heart rate limit value.
        /// </summary>
        private int heartRateLimitValue;

        /// <summary>
        /// Number representing temporary buffered heart rate limit value.
        /// </summary>
        private int heartRateLimitBufferValue;

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
        private const int MEASUREMENT_TIME = 20;

        /// <summary>
        /// Number representing measurement time from which heartRateMonitor values are used to determine heart rate.
        /// </summary>
        private const int COUNTED_MEASUREMENT_TIME = 5;

        /// <summary>
        /// String representing key that is used by application's properties dictionary
        /// to save current heart rate limit value.
        /// </summary>
        private const string HEART_RATE_LIMIT_KEY = "heartRateLimit";

        /// <summary>
        /// Reference to the object of the HeartRateMonitorModel class.
        /// </summary>
        private HeartRateMonitorModel heartRateMonitorModel;

        /// <summary>
        /// Reference to the IDictionary object that is used to store some data,
        /// so that they are available next time the applications runs.
        /// </summary>
        private IDictionary<string, object> properties;

        /// <summary>
        /// List of numbers storing measurement values.
        /// It is used to calculate average value of the heart rate (at the end of the measurement process).
        /// </summary>
        private List<int> measurementValues;


        #endregion

        #region properties

        /// <summary>
        /// An instance of PageNavigation class.
        /// </summary>
        public INavigation AppPageNavigation { private set; get; }

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
        /// Command which shows message about lack of heart rate sensor (application close).
        /// The command is injected into view model.
        /// </summary>
        public ICommand NotSupportedInfoCommand { set; get; }

        /// <summary>
        /// Command which handles confirmation of privilege denied dialog.
        /// </summary>
        public ICommand AppUnusableConfirmedCommand { set; get; }

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
            set { SetProperty(ref isMeasuring, value); }
            get { return isMeasuring; }
        }

        /// <summary>
        /// Property indicating whether measurement process is finished.
        /// </summary>
        public bool IsFinished
        {
            set { SetProperty(ref isFinished, value); }
            get { return isFinished; }
        }

        public bool IsMeasurementCounted
        {
            set { SetProperty(ref isMeasurementCounted, value); }
            get { return isMeasurementCounted; }
        }

        /// <summary>
        /// Property indicating whether heart rate limit is exceeded.
        /// </summary>
        public bool IsMeasurementResultAlert
        {
            set { SetProperty(ref isMeasurementResultAlert, value); }
            get { return isMeasurementResultAlert; }
        }

        /// <summary>
        /// Property with value representing measurement countdown.
        /// </summary>
        public int MeasurementCountdown
        {
            set {
                SetProperty(ref measurementCountdown, value);
                if (value <= COUNTED_MEASUREMENT_TIME)
                    IsMeasurementCounted = true;
            }
            get { return measurementCountdown; }
        }

        /// <summary>
        /// Property with value representing current heart rate.
        /// </summary>
        public int CurrentHeartRate
        {
            set
            {
                SetProperty(ref currentHeartRate, value);
                UpdateMeasurementResultRange();
                UpdateMeasurementResultAlert();
            }

            get { return currentHeartRate; }
        }

        /// <summary>
        /// Property with value representing heart rate limit value.
        /// </summary>
        public int HeartRateLimitValue
        {
            set
            {
                SetProperty(ref heartRateLimitValue, value);
                properties[HEART_RATE_LIMIT_KEY] = value;
                UpdateMeasurementResultRange();
                UpdateMeasurementResultAlert();
            }
            get { return heartRateLimitValue; }
        }

        /// <summary>
        /// Property with value representing temporary buffered heart rate limit value.
        /// </summary>
        public int HeartRateLimitBufferValue
        {
            set { SetProperty(ref heartRateLimitBufferValue, value); }
            get { return heartRateLimitBufferValue; }
        }

        /// <summary>
        /// Property representing the range to which the heart rate value is classified,
        /// when the measurement is finished.
        /// </summary>
        public int MeasurementResultRange
        {
            set { SetProperty(ref measurementResultRange, value); }
            get { return measurementResultRange; }
        }
        #endregion

        #region methods

        /// <summary>
        /// MainViewModel class constructor.
        /// </summary>
        public MainViewModel()
        {
            ToggleMeasurementCommand = new Command(ExecuteToggleMeasurementCommand);
            ShowSettingsCommand = new Command(ExecuteShowSettingsCommand, CanExecuteShowSettingsCommand);
            UpdateHeartRateLimitCommand = new Command(ExecuteUpdateHeartRateLimitCommand);
            AppUnusableConfirmedCommand = new Command(ExecuteAppUnusableConfirmed);
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="properties">View model instance properties.</param>
        /// <param name="pageNavigation">Page navigation object.</param>
        /// <returns>The initialization task.</returns>
        public async Task Init(IDictionary<string, object> properties, INavigation pageMavigation)
        {
            this.properties = properties;
            AppPageNavigation = pageMavigation;

            heartRateMonitorModel = new HeartRateMonitorModel();

            heartRateMonitorModel.HeartRateMonitorDataChanged += ModelOnHeartRateMonitorDataChanged;
            heartRateMonitorModel.HeartRateSensorNotSupported += ModelOnHeartRateSensorNotSupported;

            if (!await heartRateMonitorModel.CheckPrivileges())
            {
                PrivilegeDeniedInfoCommand?.Execute(null);

                return;
            }

            heartRateMonitorModel.Init();

            RestoreHeartRateLimitSliderValue();
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
            int heartRateValue = heartRateMonitorModel.GetHeartRate();

            CurrentHeartRate = heartRateValue;

            if(IsMeasurementCounted)
                measurementValues.Add(heartRateValue);
        }

        /// <summary>
        /// Handles "HeartRateSensorNotSupported" event of the HeartRateMonitorModel object.
        /// Executes NotSupportedInfoCommand command.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Arguments passed to the event.</param>
        private void ModelOnHeartRateSensorNotSupported(object sender, EventArgs e)
        {
            NotSupportedInfoCommand?.Execute(null);
        }

        /// <summary>
        /// Handles execution of command which occurs when user confirms apps unability to use heart rate sensor.
        /// Closes the application.
        /// </summary>
        private void ExecuteAppUnusableConfirmed()
        {
            try
            {
                Tizen.Applications.Application.Current.Exit();
            }
            catch (Exception)
            {
                Log.Error("HeartRateMonitor", "Unable to close the application");
            }
        }

        /// <summary>
        /// Executes UpdateHeartRateLimitCommand command.
        /// Updates value of the HeartRateLimitSliderValue property.
        /// Navigates back to the measurement page by executing NavigateBackCommand command
        /// of the PageNavigation class.
        /// </summary>
        private void ExecuteUpdateHeartRateLimitCommand()
        {
            HeartRateLimitValue = HeartRateLimitBufferValue;
            AppPageNavigation.PopAsync();
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
            HeartRateLimitBufferValue = HeartRateLimitValue;
            AppPageNavigation.PushAsync(new SettingsPage(), false);
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
                measurementLock += 1;
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
            MeasurementCountdown = MEASUREMENT_TIME;
            measurementStartTimestamp = DateTime.Now;
            measurementValues = new List<int>();
            MeasurementStarted?.Invoke(this, new EventArgs());

            Device.StartTimer(TimeSpan.FromSeconds(MEASUREMENT_TIME), () =>
            {
                if (IsMeasuring && measurementLock == 0)
                {
                    StopMeasurement();
                    ((Command)ShowSettingsCommand).ChangeCanExecute();
                }

                if (measurementLock > 0)
                {
                    measurementLock -= 1;
                }

                return false;
            });

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (!IsMeasuring)
                {
                    return false;
                }

                TimeSpan measurementElapsedTime = DateTime.Now - measurementStartTimestamp;
                MeasurementCountdown = MEASUREMENT_TIME - measurementElapsedTime.Seconds;
                return true;
            });

            heartRateMonitorModel.StartHeartRateMonitor();
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
            IsMeasurementCounted = false;
            MeasurementFinished?.Invoke(this, new EventArgs());
            heartRateMonitorModel.StopHeartRateMonitor();

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
                count = measurementValues.Count;

            if (count == 0)
            {
                return total;
            }

            total = measurementValues.Sum();

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
        /// If it is not available in the dictionary, the value is set to app defined constant.
        /// </summary>
        private void RestoreHeartRateLimitSliderValue()
        {
            if (properties.ContainsKey(HEART_RATE_LIMIT_KEY))
            {
                HeartRateLimitValue = (int)properties[HEART_RATE_LIMIT_KEY];
            }
            else
            {
                HeartRateLimitValue = HEART_RATE_DEFAULT_LIMIT_VALUE;
            }
        }

        #endregion
    }
}