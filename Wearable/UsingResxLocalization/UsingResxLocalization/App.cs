using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;
using System.Threading;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace UsingResxLocalization
{
    /// <summary>
    /// UsingResxLocalization app
    /// According to system languages, localized texts and images are shown.
    /// Currently English, Korean, Japanese are supported.
    /// </summary>
    public class App : Application
    {
        ILocalize localize;
        /// <summary>
        /// Constructor
        /// </summary>
        public App()
        {
            localize = DependencyService.Get<ILocalize>();
            // Get locale information
            UpdateLocale();
            MainPage main = new MainPage();
            MainPage = new NavigationPage(main);
        }

        /// <summary>
        /// Get the current locale and apply it.
        /// </summary>
        public void UpdateLocale()
        {
            // determine the correct, supported .NET culture
            var ci = localize.GetCurrentCultureInfo();
            
            SetCultureInfo(ci);
            if (Device.RuntimePlatform == Device.Tizen)
            {
                // Whenever language has been changed, CurrentCulture will be updated.
                MessagingCenter.Subscribe<ILocalize, CultureInfo>(this, "LanguageChanged", (obj, culture) =>
                {
                    // Update CultureInfo with the new language
                    SetCultureInfo(culture);
                    // Send "UpdateUIByLanguageChanges" message to update UI
                    MessagingCenter.Send<App>(this, "UpdateUIByLanguageChanges");
                });
            }
        }

        /// <summary>
        /// Set the current culture
        /// It will be used by the Resource Manager
        /// </summary>
        /// <param name="info">CultureInfo</param>
        void SetCultureInfo(CultureInfo info)
        {
            // set the RESX for resource localization
            Resx.AppResources.Culture = info;
            Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = info;
        }

        /// <summary>
        /// Called when the application starts.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Called when the application enters the sleeping state.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// when the application resumes from a sleeping state.
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
