/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Clock.Controls
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
            return string.Format("Tizen.{0}Effect", propertyName);
        }
    }
}