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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportEffect(typeof(UIComponents.Tizen.Wearable.Effects.TizenStyleEffect), "TizenStyleEffect")]
namespace UIComponents.Tizen.Wearable.Effects
{
    public class TizenStyleEffect : PlatformEffect
    {
        string oldStyle;

        /// <summary>
        /// Attach effect
        /// </summary>
        protected override void OnAttached()
        {
            DoSetStyle();
        }

        /// <summary>
        /// Detach effect
        /// </summary>
        protected override void OnDetached()
        {
            var view = Control as ElmSharp.Widget;
            if (view != null)
            {
                view.Style = oldStyle;
            }
        }

        /// <summary>
        /// Called when element property is changed.
        /// </summary>
        /// <param name="args">Argument for PropertyChangedEvent</param>
        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
            if (args.PropertyName == UIComponents.Extensions.Effects.TizenStyleEffect.StyleProperty.PropertyName)
            {
                DoSetStyle();
            }
        }

        /// <summary>
        /// Set style
        /// </summary>
        void DoSetStyle()
        {
            var view = Control as ElmSharp.Widget;
            if (view != null)
            {
                var style = UIComponents.Extensions.Effects.TizenStyleEffect.GetStyle(Element);
                oldStyle = view.Style;
                view.Style = style;
            }
        }
    }
}
