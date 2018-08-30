using System;
using Tizen.Applications;
using Tizen.Wearable.CircularUI.Forms.Renderer.Watchface;

namespace AmbientWatch
{
    class Program : FormsWatchface
    {
        ClockViewModel ViewModel;

        protected override void OnCreate()
        {
            base.OnCreate();
            var watchfaceApp = new AmbientWatchApplication();
            ViewModel = new ClockViewModel(this);
            ViewModel.Time = GetCurrentTime().UtcTimestamp;
            watchfaceApp.BindingContext = ViewModel;
            LoadWatchface(watchfaceApp);
        }

        protected override void OnTick(TimeEventArgs time)
        {
            base.OnTick(time);
            if (ViewModel != null)
            {
                ViewModel.Time = time.Time.UtcTimestamp + TimeSpan.FromMilliseconds(time.Time.Millisecond);
            }
        }

        //
        // Summary:
        //     Overrides this method if want to handle behavior when the ambient mode is changed.
        //     If base.OnAmbientChanged() is not called, the event 'AmbientChanged' will not
        //     be emitted.
        //
        // Parameters:
        //   mode:
        //     The received AmbientEventArgs
        protected override void OnAmbientChanged(AmbientEventArgs mode)
        {
            base.OnAmbientChanged(mode);
            ViewModel.AmbientModeDisabled = !mode.Enabled;
        }
        //
        // Summary:
        //     Overrides this method if want to handle behavior when the time tick event comes
        //     in ambient mode. If base.OnAmbientTick() is not called, the event 'AmbientTick'
        //     will not be emitted.
        //
        // Parameters:
        //   time:
        //     The received TimeEventArgs to get time information.
        protected override void OnAmbientTick(TimeEventArgs time)
        {
            base.OnAmbientTick(time);
            ViewModel.Time = time.Time.UtcTimestamp + TimeSpan.FromMilliseconds(time.Time.Millisecond);
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