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

using System.Windows.Input;
using Xamarin.Forms;

namespace VoiceMemo.Effects
{
    /// <summary>
    /// ItemLongPressEffect class
    /// </summary>
    public class ItemLongPressEffect : RoutingEffect
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached("Command", typeof(ICommand), typeof(ItemLongPressEffect), null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached("CommandParameter", typeof(object), typeof(ItemLongPressEffect), null);

        public static Command GetCommand(BindableObject view) => (Command)view.GetValue(CommandProperty);
        public static void SetCommand(BindableObject view, ICommand value) => view.SetValue(CommandProperty, value);

        public static object GetCommandParameter(BindableObject view) => view.GetValue(CommandParameterProperty);
        public static void SetCommandParameter(BindableObject view, object value) => view.SetValue(CommandParameterProperty, value);

        public ItemLongPressEffect() : base("SEC.ItemLongPressEffect")
        {
        }
    }
}
