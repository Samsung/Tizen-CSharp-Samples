using Xamarin.Forms;

namespace ImageReader
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        /// <summary>
        /// Called when the application is launched.
        /// If base.OnCreated() is not called, the event 'Created' will not be emitted.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();

            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            // Initialize to use Tizen.Wearable.CircularUI.Forms APIs
            Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
