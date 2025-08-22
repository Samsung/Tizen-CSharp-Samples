using System;
using Workout.Services;
using Workout.Services.Settings;

namespace Workout.Models
{
    /// <summary>
    /// Provides HRM related data.
    /// </summary>
    public class HeartRateMonitorModel
    {
        #region fields

        /// <summary>
        /// Constant from research of <see href="https://en.wikipedia.org/wiki/Heart_rate">Haskell & Fox</see>.
        /// </summary>
        private const int _haskellFoxConstant = 220;

        /// <summary>
        /// How many times maximum Bpm value is greater than minimum value.
        /// </summary>
        private const int _maxToMinRatio = 2;

        /// <summary>
        /// Number of Bpm ranges into which the scale is divided.
        /// </summary>
        private const int _bpmRanges = 6;

        /// <summary>
        /// An instance of the HeartRateMonitorService service.
        /// </summary>
        private readonly HeartRateMonitorService _service;

        /// <summary>
        /// Bpm scale start point value.
        /// </summary>
        private readonly int _maxBpm;

        /// <summary>
        /// Bpm scale end point value.
        /// </summary>
        private readonly int _minBpm;

        /// <summary>
        /// Flag indicating whether HRM measurement is paused or not.
        /// </summary>
        private bool _isMeasurementPaused;

        /// <summary>
        /// Array of Bpm range occurrences.
        /// </summary>
        private int[] _bpmRangeOccurrences;

        #endregion

        #region properties

        /// <summary>
        /// Updated event.
        /// Notifies about heart rate value update.
        /// </summary>
        public event EventHandler<HeartRateMonitorUpdatedEventArgs> Updated;

        /// <summary>
        /// NotSupported event.
        /// Notifies about lack of heart rate sensor.
        /// </summary>
        public event EventHandler NotSupported;

        #endregion

        #region methods

        /// <summary>
        /// Initializes value of _bpmRangeOccurrences field.
        /// </summary>
        private void InitializeBpmRangeOccurrences()
        {
            _bpmRangeOccurrences = new int[_bpmRanges];
        }

        /// <summary>
        /// Handles "DataUpdated" of the HeartRateMonitorService object.
        /// Invokes "Updated" to other application's modules.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="bpm">Heart rate value.</param>
        private void OnServiceDataUpdated(object sender, int bpm)
        {
            double normalizedBpm = Math.Clamp((bpm - _minBpm) / (double)(_maxBpm - _minBpm), 0, 1);
            int bpmRange = bpm < _minBpm ? 0 : Math.Min((int)((normalizedBpm * (_bpmRanges - 1)) + 1), _bpmRanges - 1);

            if (!_isMeasurementPaused)
            {
                _bpmRangeOccurrences[bpmRange]++;
            }

            Updated?.Invoke(this, new HeartRateMonitorUpdatedEventArgs(new HeartRateMonitorData
            {
                Bpm = bpm,
                BpmRange = bpmRange,
                BpmRangeOccurrences = _bpmRangeOccurrences,
                NormalizedBpm = normalizedBpm
            }));
        }

        /// <summary>
        /// Handles "NotSupported" of the HeartRateMonitorService object.
        /// Invokes "NotSupported" to other application's modules.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="args">Event arguments. Not used.</param>
        private void OnServiceNotSupported(object sender, EventArgs args)
        {
            NotSupported?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// HeartRateMonitorModel class constructor.
        /// </summary>
        public HeartRateMonitorModel()
        {
            _maxBpm = _haskellFoxConstant - SettingsService.Instance.Age;
            _minBpm = _maxBpm / _maxToMinRatio;

            _service = new HeartRateMonitorService();

            _service.DataUpdated += OnServiceDataUpdated;
            _service.NotSupported += OnServiceNotSupported;

            InitializeBpmRangeOccurrences();

            _service.Init();
        }

        /// <summary>
        /// Starts notification about changes of heart rate value.
        /// </summary>
        public void Start()
        {
            _isMeasurementPaused = false;

            _service.Start();
        }

        /// <summary>
        /// Stops notification about changes of heart rate value.
        /// </summary>
        public void Stop()
        {
            _service.Stop();
        }

        /// <summary>
        /// Pauses HRM measurement.
        /// </summary>
        public void Pause()
        {
            _isMeasurementPaused = true;
        }

        /// <summary>
        /// Resets HRM measurement.
        /// </summary>
        public void Reset()
        {
            InitializeBpmRangeOccurrences();
        }

        #endregion
    }
}
