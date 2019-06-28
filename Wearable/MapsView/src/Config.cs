using Xamarin.Forms.Maps;

namespace MapsView
{
    public class Config
    {
        public const string MAPS_SERVICE_PROVIDER = "HERE";
        public const string MAPS_SERVICE_KEY = "<PUT YOUR KEY HERE>";
        // For more information see https://developer.tizen.org/development/guides/native-application/location-and-sensors/maps-and-maps-service/getting-here-maps-credentials

        /// <summary>
        /// How far from above do we start - its a distance in kilometers used for
        /// Distance.FromKilometers() method.
        /// </summary>
        public const double STARTING_ZOOM_LEVEL = 3;

        /// <summary>
        /// What type of Map should be used - we default to Satellite view
        /// </summary>
        public const MapType MAP_TYPE = MapType.Satellite;

        /// <summary>
        /// How often should a zoom happen if the user keeps on rotating the bezel
        /// </summary>
        public const int ZOOM_EVERY_X_MILLISECONDS = 500;
    }
}
