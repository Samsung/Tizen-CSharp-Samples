using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Clock.Pages.Views
{
    public class AlarmRadioSwitch : Switch
    {
        private List<AlarmRadioSwitch> _alarmList;
        private string _group;
        public string Group
        {
            get { return _group; }
            set
            {
                _group = value;
                if (_groupMap.TryGetValue(_group, out _alarmList))
                {
                    // if this group is alrady exist, add this object
                    _alarmList.Add(this);
                }
                else
                {
                    List<AlarmRadioSwitch> newList = new List<AlarmRadioSwitch>();
                    newList.Add(this);
                    _groupMap.Add(_group, newList);
                }
            }
        }

        public int Value { get; set; }

        private static Dictionary<string, List<AlarmRadioSwitch>> _groupMap =
            new Dictionary<string, List<AlarmRadioSwitch>>();

        public AlarmRadioSwitch()
        {
        }

        public void TurnOffOthers()
        {
            if (_groupMap.TryGetValue(this.Group, out _alarmList))
            {
                foreach (var switchObj in _alarmList)
                {
                    if (switchObj != this)
                    {
                        switchObj.IsToggled = false;
                    }
                }
            }
        }
    }
}
