//Copyright 2019 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
using System.Runtime.InteropServices;
using Tizen.Wearable.CircularUI.Forms;

[assembly: ExportRenderer(typeof(PlayerSample.TransparentView), typeof(PlayerSample.TransparentViewRenderer))]

namespace PlayerSample
{
    public class TransparentView : View
    {

    }

    public class TransparentViewRenderer : ViewRenderer<TransparentView, ElmSharp.Box>
    {
        ElmSharp.Rectangle _transparent;
        protected override void OnElementChanged(ElementChangedEventArgs<TransparentView> e)
        {
            if (Control == null)
            {
                SetNativeControl(new ElmSharp.Box(Forms.NativeParent));
                _transparent = new ElmSharp.Rectangle(Forms.NativeParent);
                _transparent.Color = ElmSharp.Color.Transparent;
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
                Toast.DisplayText(e.Message, 1000);
            }
        }
        [DllImport("libevas.so.1")]
        internal static extern void evas_object_render_op_set(IntPtr obj, int op);
    }
}
