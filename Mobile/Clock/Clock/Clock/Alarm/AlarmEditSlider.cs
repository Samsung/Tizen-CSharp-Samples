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

using Clock.Controls;
using Clock.Converters;
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// This class defines alarm edit sound row UI.
    /// This row is only for sound row
    /// </summary>
    class AlarmEditSlider : RelativeLayout
    {
        internal Slider slider;
        internal Image volumeImage;

        /// <summary>
        /// Constructor for this class
        /// It defines UIs for this row
        /// </summary>
        /// <param name="alarmRecord">AlarmRecord</param>
        public AlarmEditSlider(AlarmRecord alarmRecord)
        {
            BindingContext = alarmRecord;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            //VerticalOptions = LayoutOptions.Start;
            HeightRequest = 120;
            /// volume image beside slider
            volumeImage = new Image()
            {
                HeightRequest = 50,
                WidthRequest = 50,
                Source = alarmRecord.IsMute ? "alarm/01_volume_vibration.png" : "alarm/00_volume_icon.png",
            };
            ImageAttributes.SetBlendColor(volumeImage, Color.FromRgba(50, 150, 166, 204));
            // volume image depends on 'IsMute' value of AlarmModel.BindableAlarmRecord
            // When volume is mute, volume image changes to vibration image.
            volumeImage.SetBinding(Image.SourceProperty, new Binding("IsMute", BindingMode.Default, new MuteToImageSourceConverter(), source:AlarmModel.BindableAlarmRecord));

            /// slider to change volume
            slider = new Slider()
            {
                WidthRequest = 720 - (32 + 50 + 32 + 32),
                Value = AlarmModel.BindableAlarmRecord.Volume,
                HeightRequest = 50,
            };
            // Slider's Value affect volume of AlarmModel.BindableAlarmRecord
            //slider.SetBinding(Slider.ValueProperty, new Binding("Volume", BindingMode.OneWayToSource, source: AlarmModel.BindableAlarmRecord));
            slider.SetBinding(Slider.ValueProperty, new Binding("Volume", BindingMode.TwoWay, source: AlarmModel.BindableAlarmRecord));

            Children.Add(volumeImage,
                Constraint.RelativeToParent((parent) => { return 32; }),
                Constraint.RelativeToParent((parent) => { return (120 - 50) / 2; }));

            Children.Add(slider,
                Constraint.RelativeToParent((parent) => { return 32 + 50 + 20; }),
                Constraint.RelativeToParent((parent) => { return (120 - 50) / 2; }));
        }
    }
}
