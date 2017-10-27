/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

namespace Clock.Alarm
{
    /// <summary>
    /// AlarmEditSliderCell class
    /// It's for a cell which has slider and volume image to change sound volume
    /// </summary>
    [ContentProperty("View")]
    public class AlarmEditSliderCell : Cell
    {
        View _view;

        public View View
        {
            get { return _view; }
            set
            {
                if (_view == value)
                {
                    return;
                }

                _view = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="record">AlarmRecord</param>
        public AlarmEditSliderCell(AlarmRecord record)
        {
            View = new AlarmEditSlider(record);
        }
    }
}
