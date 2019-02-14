using System;
using System.Collections.Generic;
using System.Text;

namespace StorageApp
{
    /// <summary>
    /// Result page of Storage application
    /// </summary>
    class StoragePrint
    {
        /// <summary>
        /// Detail string to show
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// The constructor of StoragePrint
        /// </summary>
        /// <param name="str">Text to show</param>
        public StoragePrint(string str)
        {
            Detail = str;
        }
    }
}
