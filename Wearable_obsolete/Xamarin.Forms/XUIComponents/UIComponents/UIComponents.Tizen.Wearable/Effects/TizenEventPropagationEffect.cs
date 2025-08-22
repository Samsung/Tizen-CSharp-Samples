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

[assembly: ExportEffect(typeof(UIComponents.Tizen.Wearable.Effects.TizenEventPropagationEffect), "TizenEventPropagationEffect")]
namespace UIComponents.Tizen.Wearable.Effects
{
    class TizenEventPropagationEffect : PlatformEffect
    {
        /// <summary>
        /// Attach effect
        /// </summary>
        protected override void OnAttached()
        {
            DoEnable();
        }

        /// <summary>
        /// Detach effect
        /// </summary>
        protected override void OnDetached()
        {
            DoEnable(false);
        }

        /// <summary>
        /// Called when element property is changed.
        /// </summary>
        /// <param name="args">Argument for PropertyChangedEvent</param>
        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
            if (args.PropertyName == UIComponents.Extensions.Effects.TizenEventPropagationEffect.EnablePropagationProperty.PropertyName)
            {
                DoEnable();
            }
        }

        /// <summary>
        /// Enable Propagation without parameter
        /// </summary>
        void DoEnable()
        {
            var enablePropagation = UIComponents.Extensions.Effects.TizenEventPropagationEffect.GetEnablePropagation(Element);
            DoEnable(enablePropagation);
        }

        /// <summary>
        /// Enable Propagation according to parameter
        /// </summary>
        /// <param name="enablePropagation">Boolean</param>
        void DoEnable(bool enablePropagation)
        {
            Control.RepeatEvents = enablePropagation;
            Control.PropagateEvents = enablePropagation;
        }
    }
}
