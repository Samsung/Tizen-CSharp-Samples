using Tizen.Applications;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Wearable.CircularUI.Forms.Renderer.Watchface;
using Xamarin.Forms;

namespace PhotoWatch
{
    class Program : FormsWatchface
    {
        ClockViewModel _viewModel;
        protected override void OnCreate()
        {
            base.OnCreate();
            var watchfaceApp = new PhotoWatchApp();
            var model = new ClockViewModel();

            model.LoadFromPreference();
            model.UpdateBackgroundImage();
            _viewModel = model;
            watchfaceApp.BindingContext = _viewModel;
            LoadWatchface(watchfaceApp);
        }

        protected override void OnPause()
        {
            _viewModel.UpdateBackgroundImage();
            base.OnPause();
        }

        protected override void OnTick(TimeEventArgs time)
        {
            base.OnTick(time);
            if (_viewModel != null)
            {
                _viewModel.Time = time.Time.UtcTimestamp;
            }
        }

        protected override void OnAmbientTick(TimeEventArgs time)
        {
            base.OnAmbientTick(time);
            if (_viewModel != null)
            {
                _viewModel.Time = time.Time.UtcTimestamp;
            }
        }

        protected override void OnAmbientChanged(AmbientEventArgs mode)
        {
            base.OnAmbientChanged(mode);
            if (_viewModel != null)
            {
                _viewModel.IsNormalMode = !mode.Enabled;
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
