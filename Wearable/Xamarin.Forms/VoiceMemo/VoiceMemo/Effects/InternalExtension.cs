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

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace VoiceMemo.Effects
{
    internal static class InternalExtension
    {
        internal static void InternalPropertyChanged(BindableObject bindable, BindableProperty property, Func<bool> removalConditionFunc, IList<Type> supportedTypes = null)
        {
            if (supportedTypes != null && !supportedTypes.Contains(bindable.GetType()))
            {
                return;
            }

            var element = bindable as Element;
            string effectName = GetEffectName(property.PropertyName);
            Effect toRemove = null;

            foreach (var effect in element.Effects)
            {
                if (effect.ResolveId == effectName)
                {
                    toRemove = effect;
                    break;
                }
            }

            if (toRemove == null)
            {
                element.Effects.Add(Effect.Resolve(effectName));
            }
            else
            {
                if (removalConditionFunc())
                {
                    element.Effects.Remove(toRemove);
                }
            }
        }

        internal static string GetEffectName(string propertyName)
        {
            return string.Format("SEC.{0}Effect", propertyName);
        }
    }
}
