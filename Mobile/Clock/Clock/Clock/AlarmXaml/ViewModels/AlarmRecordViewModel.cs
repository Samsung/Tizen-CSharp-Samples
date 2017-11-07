using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clock.Interfaces;
using Clock.Models;
using Xamarin.Forms;
using Clock.DataProvider;

namespace Clock.ViewModels
{
    public class AlarmRecordViewModel : BaseViewModel
    {
        private AlarmRecord _alarmRecord;

        public static List<AlarmRecordViewModel> GetItems()
        {
            var alarmRecordList = SQLiteDBAccessor.Instance.GetItems();
            List<AlarmRecordViewModel> viewModelList = new List<AlarmRecordViewModel>();
            foreach (var item in alarmRecordList)
            {
                viewModelList.Add(new AlarmRecordViewModel(item));
            }
            return viewModelList;
        }

        public AlarmRecordViewModel(AlarmRecord alarmRecord)
        {
            this._alarmRecord = alarmRecord;
        }

        public AlarmRecordViewModel(AlarmRecordViewModel alarmRecordViewModel)
        {
            this._alarmRecord = new AlarmRecord();
            DeepCopy(alarmRecordViewModel);
        }

        public DateTime ScheduledDateTime
        {
            get { return _alarmRecord.ScheduledDateTime; }
            set
            {
                if (_alarmRecord.ScheduledDateTime != value)
                {
                    _alarmRecord.ScheduledDateTime = value;
                    RaisePropertyChanged();
                }
            }
        }

        public AlarmWeekFlag WeekFlag
        {
            get { return _alarmRecord.WeekFlag; }
            set
            {
                if (_alarmRecord.WeekFlag != value)
                {
                    _alarmRecord.WeekFlag = value;
                    RaisePropertyChanged();
                }
            }
        }

        public AlarmTypes AlarmType
        {
            get { return _alarmRecord.AlarmType; }
            set
            {
                if (_alarmRecord.AlarmType != value)
                {
                    _alarmRecord.AlarmType = value;
                    RaisePropertyChanged();
                }
            }
        }

        public float Volume
        {
            get { return _alarmRecord.Volume; }
            set
            {
                if (_alarmRecord.Volume != value)
                {
                    _alarmRecord.Volume = value;
                    RaisePropertyChanged();
                }
            }
        }

        internal void UpdateOnOffToDb()
        {
            SQLiteDBAccessor.Instance.Update(_alarmRecord);
        }

        internal AlarmRecordViewModelEnum SaveToDb()
        {
            AlarmRecord existingRecord = SQLiteDBAccessor.Instance.Find(_alarmRecord.ScheduledDateTime);
            if (_alarmRecord.NativeAlarmId == null)
            {
                if (existingRecord != null)
                {
                    return AlarmRecordViewModelEnum.FailureNew;
                }
                //IAlarm iAlarm = DependencyService.Get<IAlarm>();
                _alarmRecord.NativeAlarmId = AlarmRecord.SaveAlarm(_alarmRecord);
                SQLiteDBAccessor.Instance.Insert(_alarmRecord);
                return AlarmRecordViewModelEnum.SuccessNew;
            }
            else
            {
                if (existingRecord != null)
                {
                    if (existingRecord.DateCreated != _alarmRecord.DateCreated)
                    {
                        // TODO: Critical!! Need to delete existingRecord's AlarmRecordViewModel from 
                        SQLiteDBAccessor.Instance.Update(_alarmRecord);
                        return AlarmRecordViewModelEnum.SuccessCancelAndUpdateExisting;
                    }
                    else
                    {
                        SQLiteDBAccessor.Instance.Update(_alarmRecord);
                        return AlarmRecordViewModelEnum.SuccessUpdateExceptTime;
                    }
                    // TODO: Update native alarm if week flag changed
                }
                else
                {
                    // TODO: Update native alarm since time has changed
                    SQLiteDBAccessor.Instance.Update(_alarmRecord);
                    return AlarmRecordViewModelEnum.SuccessUpdateNonexisting;
                }
            }
        }

        internal void DeepCopy(AlarmRecordViewModel copySource)
        {
            this.ScheduledDateTime = copySource.ScheduledDateTime;
            this.WeekFlag = copySource.WeekFlag;
            this.AlarmType = copySource.AlarmType;
            this.Volume = copySource.Volume;
            this.AlarmToneType = copySource.AlarmToneType;
            this.Snooze = copySource.Snooze;
            this.AlarmName = copySource.AlarmName;
            this.Repeat = copySource.Repeat;
            this.OnOff = copySource.OnOff;
            this.DateCreated = copySource.DateCreated;
            this.NativeAlarmId = copySource.NativeAlarmId;
        }

        public override string ToString()
        {
            string s = "";
            s += this.ScheduledDateTime + ", \n";
            s += this.WeekFlag + ", \n";
            s += this.AlarmType + ", \n";
            s += this.Volume + ", \n";
            s += this.AlarmToneType + ", \n";
            s += this.Snooze + ", \n";
            s += this.AlarmName + ", \n";
            s += this.Repeat + ", \n";
            s += this.OnOff + ", \n";
            s += this.DateCreated + ", \n";
            s += this._alarmRecord + ", \n";
            return s;
        }

        public AlarmToneTypes AlarmToneType
        {
            get { return _alarmRecord.AlarmToneType; }
            set
            {
                if (_alarmRecord.AlarmToneType != value)
                {
                    _alarmRecord.AlarmToneType = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool Snooze
        {
            get { return _alarmRecord.Snooze; }
            set
            {
                if (_alarmRecord.Snooze != value)
                {
                    _alarmRecord.Snooze = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string AlarmName
        {
            get
            {
                return _alarmRecord.AlarmName;
            }
            set
            {
                if (_alarmRecord.AlarmName != value)
                {
                    _alarmRecord.AlarmName = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool Repeat
        {
            get { return _alarmRecord.Repeat; }
            set
            {
                if (_alarmRecord.Repeat != value)
                {
                    _alarmRecord.Repeat = value;
                    RaisePropertyChanged();
                }
            }
        }


        public bool OnOff
        {
            get { return _alarmRecord.OnOff; }
            set
            {
                if (_alarmRecord.OnOff != value)
                {
                    _alarmRecord.OnOff = value;
                    RaisePropertyChanged();

                }
            }
        }

        public TimeSpan DateCreated
        {
            get { return _alarmRecord.DateCreated; }
            set
            {
                if (_alarmRecord.DateCreated != value)
                {
                    _alarmRecord.DateCreated = value;
                    RaisePropertyChanged();

                }
            }
        }

        public int? NativeAlarmId
        {
            get { return _alarmRecord.NativeAlarmId; }
            set
            {
                if (_alarmRecord.NativeAlarmId != value)
                {
                    _alarmRecord.NativeAlarmId = value;
                    RaisePropertyChanged();

                }
            }
        }
    }
}

