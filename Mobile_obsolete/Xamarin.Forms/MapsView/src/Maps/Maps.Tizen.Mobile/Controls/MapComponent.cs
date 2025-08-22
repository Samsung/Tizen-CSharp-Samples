/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;

using Tizen.Maps;

using Xamarin.Forms;

namespace Maps.Tizen.Mobile.Controls
{
    public class MapComponent : View
    {
        public static readonly BindableProperty MapProperty = BindableProperty.Create(
            propertyName: nameof(Map),
            returnType: typeof(MapView),
            declaringType: typeof(MapComponent),
            defaultValue: default);

        public event EventHandler MapInitialized;

        public MapView Map
        {
            get => (MapView)GetValue(MapProperty);
            set => SetValue(MapProperty, value);
        }

        public void InvokeMapInitialized()
        {
            Map.Resize((int)Width, (int)Height);
            Map.Show();
            MapInitialized?.Invoke(this, EventArgs.Empty);
        }
    }
}
