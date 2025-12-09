using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkApp // Changed namespace to match the project
{
    /// <summary>
    /// The Wi-Fi AP information
    /// </summary>
    public class APInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">ESSID of Wi-Fi AP</param>
        /// <param name="State">State of Wi-Fi AP</param>
        public APInfo(String Name, String State)
        {
            this.Name = Name;
            this.State = State;
        }

        /// <summary>
        /// ESSID of Wi-Fi AP
        /// </summary>
        public String Name;
        /// <summary>
        /// State of Wi-Fi AP
        /// </summary>
        public String State;
    }
}
