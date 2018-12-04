using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceApp
{
    class HapticInput
    {
        public int? Time { get; set; }

        public string StrTime
        {
            get
            {
                if (Time == null)
                    return "";
                else
                    return Time.ToString();
            }

            set
            {
                try
                {
                    Time = int.Parse(value);
                }
                catch
                {
                    Time = null;
                }
            }
        }
    }
}
