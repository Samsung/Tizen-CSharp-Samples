using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

using Tizen.Sensor;

namespace SmartLevel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        private Accelerometer _accelerometer;
        private Queue<(float x, float y)> _positions = new Queue<(float, float)>();

        public MainPage()
        {
            InitializeComponent();
            InitializeAccelerometer();
        }

        private void InitializeAccelerometer()
        {
            _accelerometer = new Accelerometer();
            _accelerometer.Interval = 20;
            _accelerometer.DataUpdated += OnAccelerometerDataUpdated;
            _accelerometer.Start();
        }

        private void OnAccelerometerDataUpdated(object sender, AccelerometerDataUpdatedEventArgs e)
        {
            float x = (e.X + 10) / 20;
            float y = (e.Y + 10) / 20;

            _positions.Enqueue((x, y));
            if (_positions.Count > 25)
                _positions.Dequeue();

            float xAverage = _positions.Average(item => item.x);
            float yAverage = _positions.Average(item => item.y);

            var position = new Rectangle(xAverage, 1 - yAverage, 40, 40);
            AbsoluteLayout.SetLayoutBounds(ball, position);
        }
    }
}