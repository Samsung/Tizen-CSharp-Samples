using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock.Models
{
    public enum AlarmWeekFlag
    {
        /// <summary>
        /// Identifier for Sunday.
        /// </summary>
        Sunday = 0x01,

        /// <summary>
        /// Identifier for Monday.
        /// </summary>
        Monday = 0x02,

        /// <summary>
        /// Identifier for Tuesday.
        /// </summary>
        Tuesday = 0x04,

        /// <summary>
        /// Identifier for Wednesday.
        /// </summary>
        Wednesday = 0x08,

        /// <summary>
        /// Identifier for Thursday.
        /// </summary>
        Thursday = 0x10,

        /// <summary>
        /// Identifier for Friday.
        /// </summary>
        Friday = 0x20,

        /// <summary>
        /// Identifier for Saturday.
        /// </summary>
        Saturday = 0x40,

        /// <summary>
        /// All Days of the Week.
        /// </summary>
        AllDays = Sunday | Monday | Tuesday | Wednesday | Thursday | Friday | Saturday,

        /// <summary>
        /// Only Weekdays
        /// </summary>
        WeekDays = Monday | Tuesday | Wednesday | Thursday | Friday
    }
    /// <summary>
    /// The enumeration of AlarmStates
    /// </summary>
    public enum AlarmStates
    {
        /// <summary>
        /// Identifier for Active
        /// </summary>
        Active,
        /// <summary>
        /// Identifier for Snooze
        /// </summary>
        Snooze,
        /// <summary>
        /// Identifier for Inactive
        /// </summary>
        Inactive,
    }

    /// <summary>
    /// The enumeration of AlarmTone(media files to ring by playing)
    /// </summary>
    public enum AlarmToneTypes
    {
        /// <summary>
        /// Identifier for default alarm tone type
        /// </summary>
        Default,
        /// <summary>
        /// Identifier for alarm mp3 type
        /// </summary>
        AlarmMp3,
        /// <summary>
        /// Identifier for sdk ringtone type
        /// </summary>
        RingtoneSdk,
    }

    /// <summary>
    /// The enumeration of alarm type
    /// </summary>
    public enum AlarmTypes
    {
        /// <summary>
        /// Identifier for sound
        /// </summary>
        Sound,
        /// <summary>
        /// Identifier for vibration without making a sound
        /// </summary>
        Vibration,
        /// <summary>
        /// Identifier for vibration and sound
        /// </summary>
        SoundVibration
    }

    /// <summary>
    /// The enumeration of alarm editing type
    /// </summary>
    public enum AlarmEditTypes
    {
        /// <summary>
        /// Identifier for alarm edit
        /// </summary>
        Edit,
        /// <summary>
        /// Identifier for editing alarm repeat
        /// </summary>
        Repeat,
        /// <summary>
        /// Identifier for editing alarm type
        /// </summary>
        Type,
        /// <summary>
        /// Identifier for editing alarm sound
        /// </summary>
        Sound,
        /// <summary>
        /// Identifier for editing alarm tone
        /// </summary>
        Tone,
        /// <summary>
        /// Identifier for editing alarm snooze
        /// </summary>
        Snooze,
        /// <summary>
        /// Identifier for editing alarm name
        /// </summary>
        Name
    }
}
