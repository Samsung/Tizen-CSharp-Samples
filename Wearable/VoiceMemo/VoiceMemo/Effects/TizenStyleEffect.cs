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

using Xamarin.Forms;

namespace VoiceMemo.Effects
{
    public class TizenStyleEffect : RoutingEffect
    {
        public static readonly BindableProperty StyleProperty = BindableProperty.CreateAttached("Style", typeof(string), typeof(TizenStyleEffect), null);

        public static string GetStyle(BindableObject velement) => (string)velement.GetValue(StyleProperty);
        public static void SetStyle(BindableObject velement, string value) => velement.SetValue(StyleProperty, value);

        public TizenStyleEffect() : base("SEC.TizenStyleEffect")
        {
        }
    }
}
