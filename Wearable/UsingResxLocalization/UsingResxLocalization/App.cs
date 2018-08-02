using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;
using System.Threading;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace UsingResxLocalization
{
    public class App : Application
    {
        ILocalize localize;
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
                    SetCultureInfo(culture);
                    MessagingCenter.Send<App>(this, "UpdateUIByLanguageChanges");
                });
            }
        }

        // Set the current culture
        // It will be used by the Resource Manager
        void SetCultureInfo(CultureInfo info)
        {
            Resx.AppResources.Culture = info; // set the RESX for resource localization
            Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = info;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
