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

[assembly: ExportEffect(typeof(TizenEventPropagationEffect), "TizenEventPropagationEffect")]

namespace VoiceMemo.Tizen.Wearable.Effects
{
    class TizenEventPropagationEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            DoEnable();
        }

        protected override void OnDetached()
        {
            DoEnable(false);
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
            if (args.PropertyName == VoiceMemo.Effects.TizenEventPropagationEffect.EnablePropagationProperty.PropertyName)
            {
                DoEnable();
            }
        }

        void DoEnable()
        {
            var enablePropagation = VoiceMemo.Effects.TizenEventPropagationEffect.GetEnablePropagation(Element);
            DoEnable(enablePropagation);
        }

        void DoEnable(bool enablePropagation)
        {
            Control.RepeatEvents = enablePropagation;
            Control.PropagateEvents = enablePropagation;
        }
    }
}
