using System;

namespace AudioManagerSample
{
    /// <summary>
    /// Provides data for the <see cref="IVolumeController.LevelChanged"/> event.
    /// </summary>
    public class VolumeLevelChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the LevelChangedEventArgs class.
        /// </summary>
        /// <param name="newLevel">New level.</param>
        public VolumeLevelChangedEventArgs(string newType, int newLevel)
        {
            type = newType;
            level = newLevel;
        }

        public string type;
        public int level;
    }
}
