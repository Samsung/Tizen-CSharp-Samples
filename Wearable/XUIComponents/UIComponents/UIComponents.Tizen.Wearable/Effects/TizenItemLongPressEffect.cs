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
using Xamarin.Forms.Platform.Tizen;

[assembly: ResolutionGroupName("SEC")]
[assembly: ExportEffect(typeof(UIComponents.Tizen.Wearable.Effects.TizenItemLongPressEffect), "ItemLongPressEffect")]
namespace UIComponents.Tizen.Wearable.Effects
{
    public class TizenItemLongPressEffect : PlatformEffect
    {
        /// <summary>
        /// Attach effect
        /// </summary>
        protected override void OnAttached()
        {
            var genlist = Control as ElmSharp.GenList;
            if (genlist != null)
            {
                genlist.ItemLongPressed += ItemLongPressed;
            }
        }

        /// <summary>
        /// Detach effect
        /// </summary>
        protected override void OnDetached()
        {
            var genlist = this.Control as ElmSharp.GenList;
            if (genlist != null)
            {
                genlist.ItemLongPressed -= ItemLongPressed;
            }
        }

        /// <summary>
        /// Called when the item is long pressed.
        /// </summary>
        /// <param name="sender">Object </param>
        /// <param name="e">Argument of ElmSharp.GenListItemEventArgs</param>
        void ItemLongPressed(object sender, ElmSharp.GenListItemEventArgs e)
        {
            var command = UIComponents.Extensions.Effects.ItemLongPressEffect.GetCommand(Element);
            command?.Execute(UIComponents.Extensions.Effects.ItemLongPressEffect.GetCommandParameter(Element));
        }
    }
}
