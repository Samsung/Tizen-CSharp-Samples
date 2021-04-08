namespace AppCommon.Extensions
{
    /// <summary>
    /// Enumeration for identifiers on how a background widget displays its image, if it is set to use an image file.
    /// </summary>
    public enum BackgroundOptions
    {
        /// <summary>
        /// Center the background image.
        /// </summary>
        Center,

        /// <summary>
        /// Scale the background image, retaining the aspect ratio.
        /// </summary>
        Scale,

        /// <summary>
        /// Stretch the background image to fill the widget's area.
        /// </summary>
        Stretch,

        /// <summary>
        /// Tile the background image at its original size.
        /// </summary>
        Tile
    }
}