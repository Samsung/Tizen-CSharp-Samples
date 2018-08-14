using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Tizen.Wearable.CircularUI.Forms.Renderer.Watchface;

namespace AmbientWatch
{
    public class ClockViewModel : INotifyPropertyChanged
    {
        public ClockViewModel(FormsWatchface p)
        {
            AmbientModeDisabled = true;
        }

        DateTime _Time;
        double _Hours, _Minutes, _Seconds;
        bool _AmbientModeDisabled;

        public double Hours
        {
            get
            {
                return _Hours;
            }

            set
            {
                SetProperty(ref _Hours, value, "Hours");
            }
        }

        public double Minutes
        {
            get
            {
                return _Minutes;
            }

            set
            {
                SetProperty(ref _Minutes, value, "Minutes");
            }
        }

        public double Seconds
        {
            get
            {
                return _Seconds;
            }

            set
            {
                SetProperty(ref _Seconds, value, "Seconds");
            }
        }

        public bool AmbientModeDisabled
        {
            get
            {
                return _AmbientModeDisabled;
            }

            set
            {
                SetProperty(ref _AmbientModeDisabled, value, "AmbientModeDisabled");
            }
        }
        

        public DateTime Time
        {
            get => _Time;
            set
            {
                if (_Time == value)
                {
                    return;
                }

                Hours = 30 * (_Time.Hour % 12) + 0.5f * _Time.Minute;
                Minutes = 6 * _Time.Minute + 0.1f * _Time.Second;
                Seconds = 6 * _Time.Second + (0.006f * _Time.Millisecond);
                SetProperty(ref _Time, value, "Time");
            }
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
