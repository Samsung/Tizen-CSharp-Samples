using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using TizenResourceManager = Tizen.Applications.ResourceManager;
using Tizen.System;
using System;

[assembly: ExportRenderer(typeof(Image), typeof(UsingResxLocalization.Renderers.LocalizedImageRenderer))]

namespace UsingResxLocalization.Renderers
{
    /// <summary>
    /// Custom renderer class for localized image
    /// </summary>
    class LocalizedImageRenderer : ImageRenderer
    {
        string fileName;
        public LocalizedImageRenderer() : base()
        {
            SystemSettings.LocaleLanguageChanged += SystemSettings_LocaleLanguageChanged;
        }

        // Invoked every time the language setting has been changed
        private void SystemSettings_LocaleLanguageChanged(object sender, LocaleLanguageChangedEventArgs e)
        {
            // Get the path of a proper image based on locale and update the source of an image
            Element.Source = TizenResourceManager.TryGetPath(TizenResourceManager.Category.Image, fileName);
        }

        /// <summary>
        /// Called when Image element is changed
        /// </summary>
        /// <param name="e">ElementChangedEventArgs<Image></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var s = e.NewElement.Source as FileImageSource;
                if (s != null)
                {
                    fileName = s.File;
                    var ResourcePath = "";
                    try
                    {
                        ResourcePath = TizenResourceManager.TryGetPath(TizenResourceManager.Category.Image, fileName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("[LocalizedImageRenderer] Error : " + ex.Message);
                    }

                    e.NewElement.Source = ResourcePath;
                }
            }
        }
    }
}
