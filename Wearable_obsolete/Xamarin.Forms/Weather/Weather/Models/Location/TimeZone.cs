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

using Newtonsoft.Json;
using Xamarin.Forms;

namespace Weather.Models.Location
{
    /// <summary>
    /// Time zone model.
    /// </summary>
    public class TimeZone : BindableObject
    {
        #region properties

        /// <summary>
        /// Bindable property that allows to set timestamp offset of timezone.
        /// </summary>
        public static readonly BindableProperty OffsetProperty =
            BindableProperty.Create(nameof(Offset), typeof(int), typeof(TimeZone), default(int));

        /// <summary>
        /// Gets or sets timestamp offset of timezone.
        /// </summary>
        [JsonProperty(PropertyName = "rawOffset")]
        public int Offset
        {
            get => (int)GetValue(OffsetProperty);
            set
            {
                SetValue(OffsetProperty, value);
                OnPropertyChanged();
            }
        }

        #endregion
    }
}