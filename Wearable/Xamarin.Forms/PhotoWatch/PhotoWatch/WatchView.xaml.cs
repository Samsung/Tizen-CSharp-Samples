using System;
using System.Globalization;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhotoWatch
{
    public class DayOfWeekConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToString("ddd", new CultureInfo("en-US").DateTimeFormat);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchView : ContentView
    {
        public static readonly BindableProperty TimeProperty = BindableProperty.Create("Time", typeof(DateTime), typeof(WatchView), default(DateTime), propertyChanged: (b, o, n) => ((WatchView)b).UpdateTime());

        int _totalMinute;
        int _minute;

        public WatchView ()
        {
            InitializeComponent();
        }

        public DateTime Time
        {
            get { return (DateTime)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }


        void UpdateTime()
        {
            int totalMinute = (Time.Hour % 12) * 60 + Time.Minute;
            
            if (totalMinute != _totalMinute)
                Hour.TimeHandTo(totalMinute);

            if (_minute != Time.Minute)
                Min.MinuteHandTo(Time.Minute);

            if (Second.IsVisible)
                Second.SecondHandTo(Time.Second);

            _totalMinute = totalMinute;
            _minute = Time.Minute;
        }
    }
    static class WatchHandExtensions
    {
        public static Task<bool> TimeHandTo(this VisualElement view, int totalMinute)
        {
            return view.RotateToClockwise(totalMinute / (12 * 60.0) * 360, 200, Easing.SpringOut);
        }
        public static Task<bool> MinuteHandTo(this VisualElement view, int minute)
        {
            return view.RotateToClockwise(minute / 60.0 * 360, 200, Easing.SpringOut);
        }
        public static Task<bool> SecondHandTo(this VisualElement view, int second)
        {
            return view.RotateToClockwise(second / 60.0 * 360, 200, Easing.SpringOut);
        }

        public static Task<bool> RotateToClockwise(this VisualElement view, double angle, uint length = 250, Easing easing = null)
        {
            double current = view.Rotation % 360;
            double diff = angle - current;
            if (diff <= 0)
                diff += 360;
            return view.RelRotateTo(diff, length, easing);
        }
    }
}