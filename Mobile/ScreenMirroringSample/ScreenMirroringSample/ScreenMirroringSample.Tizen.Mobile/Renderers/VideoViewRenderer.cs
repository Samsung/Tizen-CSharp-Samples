using ScreenMirroringSample;
using ScreenMirroringSample.Tizen.Mobile;
using System;
using Tizen.Multimedia;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
[assembly: ExportRenderer(typeof(VideoView), typeof(VideoViewRenderer))]
namespace ScreenMirroringSample.Tizen.Mobile
{
    /// <summary>
    /// Renderer for VideoView
    /// </summary>
    public class VideoViewRenderer : VisualElementRenderer<VideoView>
    {
        private MediaView _mediaView;
        private Display _display;
        public static string LogTag = "Tizen.Multimedia.ScreenMirroring";
        protected override void OnElementChanged(ElementChangedEventArgs<VideoView> e)
        {
            Log.Error(LogTag, "start OnElementChanged");
            if (_mediaView == null | _display == null)
            {
                _mediaView = new MediaView(Forms.NativeParent);
                SetNativeView(_mediaView);
                _display = new Display(Forms.NativeParent as MediaView);
                Log.Error(LogTag, "OnElementChanged is called.");
                ScreenMirroringController.Display = _display;
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
