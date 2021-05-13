/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using ImageViewer.Views;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;


[assembly: ExportRenderer(typeof(CircleContentView), typeof(CircleContentViewRenderer))]
namespace ImageViewer.Views
{
    class CircleContentViewRenderer : LayoutRenderer
    {
        ElmSharp.EvasImage _circleImage;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            _circleImage = new ElmSharp.EvasImage(Forms.NativeParent);
            _circleImage.IsFilled = true;
            _circleImage.File = ResourcePath.GetPath("circle.png");
            _circleImage.Show();

            if (Control != null)
            {
                _circleImage.Geometry = Control.Geometry;
                Control.SetClip(_circleImage);
                Control.LayoutUpdated += OnLayoutUpdated;
                Control.Moved += Control_Moved;
                Control.Deleted += Control_Deleted;
            }
        }

        private void Control_Deleted(object sender, EventArgs e)
        {
            _circleImage?.Unrealize();
        }

        private void Control_Moved(object sender, EventArgs e)
        {
            if (Control != null)
            {
                _circleImage.Geometry = Control.Geometry;
            }
        }

        private void OnLayoutUpdated(object sender, Xamarin.Forms.Platform.Tizen.Native.LayoutEventArgs e)
        {
            if (Control != null)
            {
                _circleImage.Geometry = Control.Geometry;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _circleImage?.Unrealize();
            }

            base.Dispose(disposing);
        }
    }
}
