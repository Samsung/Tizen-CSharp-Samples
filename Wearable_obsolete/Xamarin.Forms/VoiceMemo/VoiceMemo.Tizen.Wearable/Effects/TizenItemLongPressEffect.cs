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

using VoiceMemo.Tizen.Wearable.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportEffect(typeof(TizenItemLongPressEffect), "ItemLongPressEffect")]

namespace VoiceMemo.Tizen.Wearable.Effects
{
    public class TizenItemLongPressEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var genlist = Control as ElmSharp.GenList;
            if (genlist != null)
            {
                genlist.ItemLongPressed += ItemLongPressed;
            }
        }

        protected override void OnDetached()
        {
            var genlist = this.Control as ElmSharp.GenList;
            if (genlist != null)
            {
                genlist.ItemLongPressed -= ItemLongPressed;
            }
        }

        void ItemLongPressed(object sender, ElmSharp.GenListItemEventArgs e)
        {
            var command = VoiceMemo.Effects.ItemLongPressEffect.GetCommand(Element);
            command?.Execute(VoiceMemo.Effects.ItemLongPressEffect.GetCommandParameter(Element));
        }
    }
}
