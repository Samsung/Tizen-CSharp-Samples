using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Clock.Models
{
    public class AlarmRecordModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public string Name { get; set; }
        private DateTime _alarmDate;

        public DateTime AlarmDate
        {
            get
            {
                return _alarmDate;
            }
            set
            {
                if (_alarmDate != value)
                {
                    _alarmDate = value;
                    RaisePropertyChanged();
                }
            }
        }
        void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
