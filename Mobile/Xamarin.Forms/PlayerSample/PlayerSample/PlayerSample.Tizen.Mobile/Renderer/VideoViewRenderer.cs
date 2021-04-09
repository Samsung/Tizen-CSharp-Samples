using PlayerSample;
using PlayerSample.Tizen.Mobile;
using System;
using Tizen.Multimedia;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(VideoView), typeof(VideoViewRenderer))]
namespace PlayerSample.Tizen.Mobile
{
    /// <summary>
    /// Renderer for VideoView
    /// </summary>
    public class VideoViewRenderer : VisualElementRenderer<VideoView>
    {
        private MediaView _mediaView;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoView> e)
        {
            if (_mediaView == null)
            {
                _mediaView = new MediaView(Forms.NativeParent);
                SetNativeView(_mediaView);
            }

            if (e.OldElement != null)
            {
                _mediaView.Resized -= NatvieViewResized;
            }

            if (e.NewElement != null)
            {
                _mediaView.Resized += NatvieViewResized;
                Element.NativeView = _mediaView;
            }

            base.OnElementChanged(e);
        }

        private void NatvieViewResized(object sender, EventArgs e)
        {
            (Element as IViewController)?.NativeSizeChanged();
        }
    }
}
