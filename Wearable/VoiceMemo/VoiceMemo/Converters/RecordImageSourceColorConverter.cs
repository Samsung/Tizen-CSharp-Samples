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
using System.Globalization;
using VoiceMemo.Effects;
using Xamarin.Forms;

namespace VoiceMemo.Converters
{
    class RecordImageSourceColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string source = (string)value;
            Image imageOjb = (Image)parameter;

            if (source == "record_stop_icon.png")
            {
                ImageAttributes.SetBlendColor(imageOjb, Color.Red);
            }
            else if (source == "recording_icon_pause.png")
            {
                ImageAttributes.SetBlendColor(imageOjb, (Color)Application.Current.Resources["AO012L3"]);
            }

            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
