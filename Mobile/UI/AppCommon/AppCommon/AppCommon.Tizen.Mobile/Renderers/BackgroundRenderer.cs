using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using AppCommon.Extensions;
using AppCommon.Tizen.Mobile.Renderers;
using EBackground = ElmSharp.Background;
using EBackgroundOptions = ElmSharp.BackgroundOptions;

[assembly: ExportRenderer(typeof(Background), typeof(BackgroundRenderer))]

namespace AppCommon.Tizen.Mobile.Renderers
{
    public class BackgroundRenderer : ViewRenderer<Background, EBackground>
    {
        public BackgroundRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Background> e)
        {
            if (Control == null)
            {
                var background = new EBackground(Forms.NativeParent);
                SetNativeControl(background);
            }

            if (e.NewElement != null)
            {
                UpdateImage();
                UpdateOption();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Background.ImageProperty.PropertyName)
            {
                UpdateImage();
            }
            else if (e.PropertyName == Background.OptionProperty.PropertyName)
            {
                UpdateOption();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        void UpdateImage()
        {
            if (Element.Image == null)
            {
                Control.File = "";
            }
            else
            {
                Control.File = ResourcePath.GetPath(Element.Image.File);
            }
        }

        void UpdateOption()
        {
            Control.BackgroundOption = ConvertToNativeBackgroundOptions(((Background)Element).Option);
        }

        EBackgroundOptions ConvertToNativeBackgroundOptions(BackgroundOptions option)
        {
            if (option == BackgroundOptions.Center)
            {
                return EBackgroundOptions.Center;
            }
            else if (option == BackgroundOptions.Stretch)
            {
                return EBackgroundOptions.Stretch;
            }
            else if (option == BackgroundOptions.Tile)
            {
                return EBackgroundOptions.Tile;
            }
            else
            {
                return EBackgroundOptions.Scale;
            }
        }
    }
}
