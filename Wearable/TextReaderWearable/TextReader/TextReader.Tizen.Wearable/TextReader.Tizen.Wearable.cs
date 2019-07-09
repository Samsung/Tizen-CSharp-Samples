namespace TextReader
{
    /// <summary>
    /// TextReader Xamarin.Forms application for Tizen Wearable profile.
    /// </summary>
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        #region methods

        /// <summary>
        /// Handles creation phase of the forms application.
        /// Loads Xamarin application.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        /// <summary>
        /// Application entry point.
        /// Initializes Xamarin.Forms application.
        /// </summary>
        /// <param name="args">Command line arguments. Not used.</param>
        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            global::Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }

        #endregion
    }
}
