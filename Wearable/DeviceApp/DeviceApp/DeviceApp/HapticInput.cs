using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceApp
{
    /// <summary>
    /// The class for user input 
    /// </summary>
    class HapticInput
    {
        /// <summary>
        /// Time duration for vibration
        /// </summary>
        public int? Time { get; set; }

        /// <summary>
        /// Time duration for vibration on string type
        /// </summary>
        public string StrTime
        {
            get
            {
                if (Time == null)
                {
                    return "";
                }
                else
                {
                    return Time.ToString();
                }
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
