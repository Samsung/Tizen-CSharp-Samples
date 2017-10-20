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

using Clock.Alarm;
using Clock.Interfaces;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clock.Common
{
    /// <summary>
    /// The enumeration of Ring type
    /// </summary>
    public enum RingType
    {
        /// <summary>
        /// Identifier for Alarm Ring.
        /// </summary>
        RING_TYPE_ALARM,
        /// <summary>
        /// Identifier for Timer Ring.
        /// </summary>
        RING_TYPE_TIMER,
    }

    /// <summary>
    /// The page shown when alarm or timer rings
    /// </summary>
    partial class RingPage : ContentPage
    {
        // It's for checking a time when an alarm rings.
        private static System.Diagnostics.Stopwatch stopWatch;
        private bool needsToRecur;
        private AlarmRecord _alarmRecord;

        Rectangle dismissOnlyRec = new Rectangle(360 - 90, 219 - 90, 180, 180);
        Rectangle dismissWithSnoozeRec = new Rectangle(36, 219 - 90, 180, 180);

        Rectangle snoozeRec = new Rectangle(720 - 180 - 36, 219 - 90, 180, 180);

        private ICommand _dismissTapStartCommmand;

        /// <summary>
        /// Command property when an user touches alarm dismiss image(red)
        /// </summary>
        public ICommand AlarmTapStartCommand
        {
            get
            {
                return _dismissTapStartCommmand ?? (_dismissTapStartCommmand = new Command(DismissTapStart));
            }
        }

        /// <summary>
        /// Command property when an user touches alarm snooze image(yellow)
        /// </summary>
        private ICommand _snoozeTapStartCommmand;
        public ICommand SnoozeTapStartCommand
        {
            get
            {
                return _snoozeTapStartCommmand ?? (_snoozeTapStartCommmand = new Command(SnoozeTapStart));
            }
        }

        /// <summary>
        /// Command property when an user touches timer dismiss image(red)
        /// </summary>
        private ICommand _timerTapStartCommmand;
        public ICommand TimerTapStartCommand
        {
            get
            {
                return _timerTapStartCommmand ?? (_timerTapStartCommmand = new Command(DismissTimerStart));
            }
        }

        public RingPage(RingType type)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            Content = CreateRingPage(type);
            if (type == RingType.RING_TYPE_TIMER)
            {
                CreateElapsedTimer();
            }
        }

        public RingPage(RingType type, AlarmRecord alarmRecord)
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                _alarmRecord = alarmRecord;
                SnoozeOn = _alarmRecord.Snooze;
                Content = CreateRingPage(type);
            }
            catch (Exception EX)
            {
                System.Diagnostics.Debug.WriteLine("RingPage() exception : " + EX.Message + " , " + EX.Source + ", " + EX.StackTrace);
            }
        }

        public void CreateElapsedTimer()
        {
            stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();
            needsToRecur = true;
            Device.StartTimer(new TimeSpan(0, 0, 0, 1, 0), UpdateElapsedTimeFunc);
            DependencyService.Get<IAlarm>().StartVibration();
            DependencyService.Get<IAlarm>().PlaySound();
        }

        public void StopElapsedTimer()
        {
            needsToRecur = false;
            stopWatch.Stop();
            DependencyService.Get<IAlarm>().StopVibration();
            DependencyService.Get<IAlarm>().StopSound();
        }

        private bool UpdateElapsedTimeFunc()
        {
            if (needsToRecur)
            {
                UpdatElapsedTime(stopWatch.Elapsed);
                return true;
            }
            else
            {
                return false;
            }
        }

        async void DismissTapStart()
        {
            ringImage.IsVisible = true;
            dismissImage.Source = "ring/alarm_btn_bg_press.png";
            await ringImage.ScaleTo(600 / 180, 200);
            backgroundRingImage.IsVisible = true;
            DismissTapEnd();
        }
        
        async void DismissTapEnd()
        {
            await backgroundRingImage.ScaleTo(3, 200);
            DependencyService.Get<IAlarm>().StopVibration();
            DependencyService.Get<IAlarm>().StopSound();
            await Navigation.PopAsync();
        }

        async void SnoozeTapStart()
        {
            snoozeRingImage.IsVisible = true;
            snoozeImage.Source = "ring/alarm_btn_circle_snooze_AO004P.png";
            await snoozeRingImage.ScaleTo(600 / 180, 200);
            snoozeBackgroundRingImage.IsVisible = true;
            SnoozeTapEnd();
        }

        async void SnoozeTapEnd()
        {
            await snoozeBackgroundRingImage.ScaleTo(3, 200);
            DependencyService.Get<IAlarm>().StopVibration();
            DependencyService.Get<IAlarm>().StopSound();
            await Navigation.PopAsync();
        }

        async void DismissTimerStart()
        {
            ringImage.IsVisible = true;
            dismissImage.Source = "ring/alarm_btn_bg_press.png";
            await ringImage.ScaleTo(600 / 180, 200);
            backgroundRingImage.IsVisible = true;
            DismissTimerEnd();
        }

        async void DismissTimerEnd()
        {
            await backgroundRingImage.ScaleTo(3, 200);
            StopElapsedTimer();
            await Navigation.PopAsync();
        }

        private void UpdatElapsedTime(TimeSpan showTS)
        {
            string hmsTime;
            if (showTS.Hours > 0)
            {
                hLabel.IsVisible = true;
                mLabel.IsVisible = true;
                sLabel.IsVisible = true;
                minusImage.IsVisible = true;
                _mLabel.IsVisible = false;
                _sLabel.IsVisible = false;
                _minusImage.IsVisible = false;
                hmsTime = String.Format("{0:00}:{1:00}:{2:00}", showTS.Hours, showTS.Minutes, showTS.Seconds);
            }
            else
            {
                hLabel.IsVisible = false;
                mLabel.IsVisible = false;
                sLabel.IsVisible = false;
                minusImage.IsVisible = false;
                _mLabel.IsVisible = true;
                _sLabel.IsVisible = true;
                _minusImage.IsVisible = true;
                hmsTime = String.Format("{0:00}:{1:00}", showTS.Minutes, showTS.Seconds);
            }

            counterview.DisplayTime(hmsTime);
        }
    }
}
