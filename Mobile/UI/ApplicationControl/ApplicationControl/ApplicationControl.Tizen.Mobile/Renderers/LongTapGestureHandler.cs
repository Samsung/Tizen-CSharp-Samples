/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using ElmSharp;
using ApplicationControl.Extensions;
using ApplicationControl.Tizen.Mobile.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportHandler(typeof(LongTapGestureRecognizer), typeof(LongTapGestureHandler))]

namespace ApplicationControl.Tizen.Mobile.Renderers
{
    public class LongTapGestureHandler : GestureHandler
    {
        public LongTapGestureHandler(IGestureRecognizer recognizer) : base(recognizer)
        {
        }

        public override GestureLayer.GestureType Type
        {
            get
            {
                return GestureLayer.GestureType.LongTap;
            }
        }

        public override double Timeout
        {
            get
            {
                return (Recognizer as LongTapGestureRecognizer).Timeout;
            }
        }

        protected override void OnCanceled(View sender, object data)
        {
            (Recognizer as ILongTapGestureController).SendLongTapCanceled(sender, ((GestureLayer.TapData)data).Timestamp);
        }

        protected override void OnCompleted(View sender, object data)
        {
            (Recognizer as ILongTapGestureController).SendLongTapCompleted(sender, ((GestureLayer.TapData)data).Timestamp);
        }

        protected override void OnMoved(View sender, object data)
        {
        }

        protected override void OnStarted(View sender, object data)
        {
            (Recognizer as ILongTapGestureController).SendLongTapStarted(sender, ((GestureLayer.TapData)data).Timestamp);
        }
    }
}