//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapsView
{
    /// <summary>
    /// Converter for an item subTitle (GPS coordinates for a Position)
    /// </summary>
    public class PositionToStringValueConverter : IValueConverter
    {
        /// <summary>
        /// This is a very simple converter taking Position struct and turning it into a string
        /// </summary>        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Position && targetType == typeof(String))
            {
                return String.Format("GPS coords: {0:0.00}, {1:0.00}", ((Position)value).Latitude, ((Position)value).Longitude);
            }
            else
            {
                throw new NotImplementedException();
            }
        }       
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
