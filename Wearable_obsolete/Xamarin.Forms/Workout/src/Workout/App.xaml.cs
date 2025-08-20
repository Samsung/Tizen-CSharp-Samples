using Tizen.System;
using Workout.Services;
using Workout.Services.Privilege;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Workout
{
    /// <summary>
    /// Main application class.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        #region fields

        /// <summary>
        /// Font size multiplier.
        /// </summary>
        private const double _fontSizeMultiplier = 94;

        #endregion

        #region methods

        /// <summary>
        /// Initializes application.
        /// </summary>
        public App()
        {
            InitializeComponent();

            if (!Information.TryGetValue("http://tizen.org/feature/screen.dpi", out int dpi))
            {
                dpi = 301;
            }

            if (Resources != null)
            {
                Resources["FontSizeXXXXS"] = 19 * _fontSizeMultiplier / dpi;
                Resources["FontSizeXXXS"] = 22 * _fontSizeMultiplier / dpi;
                Resources["FontSizeXXS"] = 24 * _fontSizeMultiplier / dpi;
                Resources["FontSizeXS"] = 27 * _fontSizeMultiplier / dpi;
                Resources["FontSizeS"] = 30 * _fontSizeMultiplier / dpi;
                Resources["FontSizeM"] = 36 * _fontSizeMultiplier / dpi;
                Resources["FontSizeL"] = 38 * _fontSizeMultiplier / dpi;
                Resources["FontSizeXL"] = 42 * _fontSizeMultiplier / dpi;
                Resources["FontSizeXXL"] = 46 * _fontSizeMultiplier / dpi;
                Resources["FontSizeXXXL"] = 62 * _fontSizeMultiplier / dpi;
            }

            if (PrivilegeService.AllPermissionsGranted())
            {
                PageNavigationService.Instance.GoToWelcomePage();
            }
            else
            {
                PageNavigationService.Instance.GoToPrivilegeDeniedPage();
            }
        }

        #endregion
    }
}
