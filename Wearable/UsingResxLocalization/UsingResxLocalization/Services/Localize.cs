using System;
using Xamarin.Forms;
using System.Threading;
using System.Globalization;
using Tizen.System;
using UsingResxLocalization;

[assembly: Dependency(typeof(UsingResxLocalization.Services.Localize))]

namespace UsingResxLocalization.Services
{
    class Localize : UsingResxLocalization.ILocalize
    {
        CultureInfo _currentCultureInfo;

        public Localize()
        {
            _currentCultureInfo = GetCurrentCultureInfo();
            SystemSettings.LocaleLanguageChanged += SystemSettings_LocaleLanguageChanged;
        }

        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            var TizenLocale = SystemSettings.LocaleLanguage;
            netLanguage = TizenToDotnetLanguage(TizenLocale.ToString().Replace("_", "-"));

            // this gets called a lot - try/catch can be expensive so consider caching or something
            System.Globalization.CultureInfo ci = null;
            try
            {
                ci = new System.Globalization.CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException e1)
            {
                // iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
                // fallback to first characters, in this case "en"

                // iOS language not valid .NET culture, falling back to English
                Console.WriteLine(netLanguage + " could not be set, using 'en' (" + e1.Message + ")");
                ci = new System.Globalization.CultureInfo("en");

            }

            return ci;
        }

        /// <summary>
        /// Called when a language's changed
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">LocaleLanguageChangedEventArgs</param>

        private void SystemSettings_LocaleLanguageChanged(object sender, LocaleLanguageChangedEventArgs e)
        {
            CultureInfo info = GetCurrentCultureInfo();
            //Resx.AppResources.Culture = info;
            MessagingCenter.Send<ILocalize, CultureInfo>(this, "LanguageChanged", info);
            SetLocale(info);
            _currentCultureInfo = info;
        }

        string TizenToDotnetLanguage(string tizenLanguage)
        {
            var netLanguage = tizenLanguage;
            Console.WriteLine("Tizen Lang :" + tizenLanguage);
            //certain languages need to be converted to CultureInfo equivalent
            switch (tizenLanguage)
            {
                case "zh-CN":   // Chinese Simplified (People's Republic of China)
                    netLanguage = "zh-Hans"; // correct code for .NET
                    break;
                case "zh-HK":  // Chinese Traditional (Hong Kong)
                case "zh-hk":
                case "zh-tw":  // Chinese Traditional (Taiwan)
                case "zh-TW":
                    netLanguage = "zh-Hant"; // correct code for .NET
                    break;
                    //case "ar-ae":   // Arabic
                    //    netLanguage = "ar-sa"; // closest supported for .Net : Arabic (Saudi Arabia)
                    //    break;
                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }

            Console.WriteLine(".NET Language/Locale:" + netLanguage);
            return netLanguage;
        }
    }
}
