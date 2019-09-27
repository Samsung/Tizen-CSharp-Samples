using System;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
using System.Runtime.InteropServices;

[assembly: ExportRenderer(typeof(PlayerSample.TransparentView), typeof(PlayerSample.TransparentViewRenderer))]
namespace PlayerSample
{
    public class TransparentViewRenderer : ViewRenderer<TransparentView, Box>
    {
        Rectangle _transparent;
        protected override void OnElementChanged(ElementChangedEventArgs<TransparentView> e)
        {
            if (Control == null)
            {
                SetNativeControl(new Box(Forms.NativeParent));
                _transparent = new Rectangle(Forms.NativeParent);
                _transparent.Color = Color.Transparent;
                _transparent.Show();
                Control.SetLayoutCallback(OnLayout);
                Control.PackEnd(_transparent);
                MakeTransparent();
            }

            base.OnElementChanged(e);
        }

        void OnLayout()
        {
            _transparent.Geometry = Control.Geometry;
        }
        void MakeTransparent()
        {
            try
            {
                // Only for tizen 5.0 or higher
                _transparent.RenderOperation = RenderOp.Copy;
                // compatible tizen 4.0
                //evas_object_render_op_set(_transparent.RealHandle, 2);
            }
            catch (Exception e)
            {
            }
        }
        [DllImport("libevas.so.1")]
        internal static extern void evas_object_render_op_set(IntPtr obj, int op);
    }
}
