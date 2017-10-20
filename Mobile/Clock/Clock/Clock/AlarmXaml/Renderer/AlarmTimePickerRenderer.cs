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

using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;
using EColor = ElmSharp.Color;
using Customs = Clock.Pages.Customs;
using Clock.TizenMobile.CustomRenderers;

[assembly: ExportRenderer(typeof(Customs.AlarmTimePicker), typeof(AlarmTimePickerRenderer))]
namespace Clock.TizenMobile.CustomRenderers
{
    class AlarmTimePickerRenderer : ViewRenderer<Customs.AlarmTimePicker, TimePicker>
    {
        string _format;
        TimePicker _timePicker;

        public AlarmTimePickerRenderer()
        {
            RegisterPropertyHandler(Customs.AlarmTimePicker.TimeProperty, UpdateTimeToXamarinForms);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Customs.AlarmTimePicker> e)
        {
            if (Control == null)
            {
                _format = CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
                _timePicker = new TimePicker(Forms.Context.MainWindow)
                {
                    Time = Element.Time,
                    DateTimeFormat = _format,
                    // Added temporarily because of Tizen EFL limitation
                    Format = "%d/%b/%Y %I:%M %p"
                };
                _timePicker.DateTimeChanged += DateTimeChangedHandler;
                SetNativeControl(_timePicker);
            }

            base.OnElementChanged(e);
        }

        private void DateTimeChangedHandler(object sender, ElmSharp.DateChangedEventArgs e)
        {
            DateTime dt = e.NewDate;     // DateTime can be converted to TimeSpan via .TimeOfDay
            Control.Time = dt.TimeOfDay; // update Native wrapper and local variable
            Element.Time = Control.Time;
        }

        void UpdateTimeToXamarinForms()
        {
            Control.Time = Element.Time;
        }
    }
}
