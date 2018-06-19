using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceMemo.Models
{
    public class LatestRecord : Record
    {
        public Record Record
        {
            get;
            set;
        }

        public LatestRecord(Record record) : base()
        {
            Record = record;
        }
    }
}
