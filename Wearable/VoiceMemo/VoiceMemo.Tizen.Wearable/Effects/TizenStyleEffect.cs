/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.ComponentModel;
using VoiceMemo.Tizen.Wearable.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportEffect(typeof(TizenStyleEffect), "TizenStyleEffect")]

namespace VoiceMemo.Tizen.Wearable.Effects
{
    public class TizenStyleEffect : PlatformEffect
    {
        string oldStyle;

        protected override void OnAttached()
        {
            DoSetStyle();
        }

        protected override void OnDetached()
        {
            var view = Control as ElmSharp.Widget;
            if (view != null)
            {
                view.Style = oldStyle;
            }
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
            if (args.PropertyName == VoiceMemo.Effects.TizenStyleEffect.StyleProperty.PropertyName)
            {
                DoSetStyle();
            }
        }

        void DoSetStyle()
        {
            var view = Control as ElmSharp.Widget;
            if (view != null)
            {
                var style = VoiceMemo.Effects.TizenStyleEffect.GetStyle(Element);
                oldStyle = view.Style;
                view.Style = style;
            }
        }
    }
}
