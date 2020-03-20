using Xamarin.Forms;

namespace PlayerSample
{
    /// <summary>
    /// Represents the view for player display.
    /// </summary>
    public class VideoView : View, IViewController
    {
        private static readonly BindablePropertyKey NativeViewPropertyKey =
            BindableProperty.CreateReadOnly(nameof(NativeView), typeof(object), typeof(VideoView), default(object));

        /// <summary>
        /// BindableProperty. Identifies the NativeView bindable property.
        /// </summary>
        public static readonly BindableProperty NativeViewProperty = NativeViewPropertyKey.BindableProperty;

        /// <summary>
        /// NativeView allows application developers to display the video output on screen.
        /// </summary>
        public object NativeView
        {
            get => GetValue(NativeViewProperty);
            set => SetValue(NativeViewPropertyKey, value);
        }
    }
}
