
using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceApp
{
    /// <summary>
    /// The class for device feature
    /// </summary>
    class FeatureItem
    {
        /// <summary>
        /// The name of feature
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The constructor of FeatureItem class
        /// </summary>
        /// <param name="feature">The name of feature</param>
        public FeatureItem(string feature)
        {
            Name = feature;
        }
    }
}
