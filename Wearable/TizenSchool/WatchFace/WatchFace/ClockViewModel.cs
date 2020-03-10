using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tizen.System;

namespace WatchFace
{
    public class ClockViewModel : INotifyPropertyChanged
    {
        DateTime _time;

        public DateTime Time
        {
            get => _time;
            set
            {
                if (_time == value) return;
                _time = value;
                OnPropertyChanged();

                SecondsRotation = _time.Second * 6;
                OnPropertyChanged(nameof(SecondsRotation));
            }
        }

        public int SecondsRotation { get; private set; }
        public bool IsCharging { get; private set; }
        public int BatteryPercent { get; private set; }

        public ClockViewModel()
        {
            IsCharging = Battery.IsCharging;
            BatteryPercent = Battery.Percent;

            Battery.ChargingStateChanged += OnChargingStateChanged;
            Battery.PercentChanged += OnPercentChanged;
        }

        private void OnPercentChanged(object sender, BatteryPercentChangedEventArgs e)
        {
            BatteryPercent = e.Percent;
            OnPropertyChanged(nameof(BatteryPercent));
        }

        private void OnChargingStateChanged(object sender, BatteryChargingStateChangedEventArgs e)
        {
            IsCharging = e.IsCharging;
            OnPropertyChanged(nameof(IsCharging));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
