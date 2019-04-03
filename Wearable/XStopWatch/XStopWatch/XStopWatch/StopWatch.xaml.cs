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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NStopWatch = System.Diagnostics.Stopwatch;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.System;

namespace XStopWatch
{
    /// <summary>
    /// StopWatch is a Page that present stopwatch main screen
    /// This page has Time label and the color bar for present measuring time.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StopWatch : CirclePage
    {
        public static BindableProperty StateProperty = BindableProperty.Create(nameof(State), typeof(State), typeof(StopWatch), State.Stopped);
        public static BindableProperty AllTimeProperty = BindableProperty.Create(nameof(AllTime), typeof(TimeSpan), typeof(StopWatch), TimeSpan.Zero);

        // stop watch for measuring elapsed time
        NStopWatch _mainStopWatch;
        // stop watch for measuring lap time
        NStopWatch _subStopWatch;

        // this State is used in the text of buttons and stopwatch processing.
        public State State { get => (State)GetValue(StateProperty); set => SetValue(StateProperty, value); }

        // this TimeSpan property is elapsed time that present in the center of the page.
        public TimeSpan AllTime { get => (TimeSpan)GetValue(AllTimeProperty); set => SetValue(AllTimeProperty, value); }

        // event for Lap Add button pressed
        public event EventHandler<(TimeSpan, TimeSpan) > LapPressed;

        // event for stop button pressed
        public event EventHandler StopPressed;

        public StopWatch()
		{
            _mainStopWatch = new NStopWatch();
            _subStopWatch = new NStopWatch();

            InitializeComponent();

            Stop();
        }

        /// <summary>
        /// upward button tapped event that can start/pause/restart measuring
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">An object that contains no event data.</param>
        void OnTopEventTapped(object sender, EventArgs args)
        {
            DoSpotAnimation(sender as Image);
            if (State == State.Stopped)
            {
                Start();
            }
            else if (State == State.Started)
            {
                Pause();
            }
            else if (State == State.Paused)
            {
                Start();
            }
        }

        /// <summary>
        /// downward button tapped event that can stop the measuring or add lap time measuring.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">An object that contains no event data.</param>
        void OnBottomEventTapped(object sender, EventArgs args)
        {
            DoSpotAnimation(sender as Image);
            if (State == State.Started)
            {
                OnLapPressed();
            }
            else if (State == State.Paused)
            {
                Stop();
            }
        }

        /// <summary>
        /// this method is used to show the upward or downward button clicking effect
        /// </summary>
        /// <param name="spot">The button clicking effect image</param>
        void DoSpotAnimation(Image spot)
        {
            spot.Opacity = 1;
            Device.StartTimer(TimeSpan.FromMilliseconds(80), () =>
            {
                spot.Opacity = 0;
                return false;
            });
        }

        /// <summary>
        /// this method is used to add current lap time
        /// </summary>
        void OnLapPressed()
        {
            if (!BlueBar.IsVisible)
            {
                BlueBar.IsVisible = true;
            }

            _subStopWatch.Reset();
            _subStopWatch.Start();
            BlueBar.Rotation = 0;

            if (!CueBtn.AnimationIsRunning("CueAnimation"))
            {
                DoShowCueButton();
            }

            LapPressed?.Invoke(this, (_mainStopWatch.Elapsed, _subStopWatch.IsRunning ? _subStopWatch.Elapsed : _mainStopWatch.Elapsed));
        }

        /// <summary>
        /// this method is used to show small bracket and animation
        /// </summary>
        void DoShowCueButton()
        {
            CueBtn.IsVisible = true;

            var X = CueBtn.TranslationX;
            var TX = CueBtn.TranslationX - CueBtn.Width;
            var anim = new Animation();
            var opacityAnim = new Animation(f => CueBtn.Opacity = f, 1, 0.2);
            var transfAnim = new Animation(f => CueBtn.TranslationX = f, X, TX);
            anim.Add(0, 1, opacityAnim);
            anim.Add(0, 1, transfAnim);

            anim.Commit(CueBtn, "CueAnimation", 16, 1000, finished: (f, b) =>
            {
                CueBtn.Opacity = 1;
                CueBtn.TranslationX = X;
            });
        }

        /// <summary>
        /// start measuring time
        /// </summary>
        void Start()
        {
            State = State.Started;
            // Power.RequestCpuLock(Int32) is deperecated but there are no alternative on TizenFX 4.0
            // Use RequestLock(PowerLock, Int32) if you use version after TizenFX 5.0.
            Device.BeginInvokeOnMainThread(() => Power.RequestCpuLock(0));

            _mainStopWatch.Start();
            if (_subStopWatch.ElapsedMilliseconds > 0)
            {
                _subStopWatch.Start();
            }

            Timebar.IsVisible = true;

            // Synchronize the TimeSpan property on every 10 milliseconds.
            // Minimum time that presented in Central Label is 10 milliseconds.
            Device.StartTimer(TimeSpan.FromMilliseconds(10), OnTimeChanged);

            // Rotate Red and Blue Bar on every 16 milliseconds to fit 60 fps.
            Device.StartTimer(TimeSpan.FromMilliseconds(16), OnTimeBarAnimate);
        }

        /// <summary>
        /// pause measuring time
        /// </summary>
        void Pause()
        {
            State = State.Paused;

            // Power.ReleaseCpuLock() is deperecated but there are no alternative on TizenFX 4.0
            // Use ReleaseLock(PowerLock) if you use version after TizenFX 5.0.
            Device.BeginInvokeOnMainThread(() => Power.ReleaseCpuLock());

            _mainStopWatch.Stop();
            _subStopWatch.Stop();
        }

        /// <summary>
        /// reset measuring time
        /// </summary>
        void Stop()
        {
            State = State.Stopped;
            _mainStopWatch.Reset();
            _subStopWatch.Reset();
            BlueBar.IsVisible = false;
            CueBtn.IsVisible = false;
            Timebar.IsVisible = false;
            OnTimeChanged();
            OnTimeBarAnimate();
            StopPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// set the elapsed time to TimeSpan property
        /// </summary>
        /// <returns>the timer will keep recurring if return true.</returns>
        bool OnTimeChanged()
        {
            AllTime = _mainStopWatch.Elapsed;
            return State == State.Started;
        }

        /// <summary>
        /// the method present the Red, Blue Bar and the circular progressbar animation.
        /// </summary>
        /// <returns>the timer will keep recurring if return true.</returns>
        bool OnTimeBarAnimate()
        {
            double sec = TimeToRotation(_mainStopWatch.Elapsed);
            double lap = TimeToRotation(_subStopWatch.Elapsed);
            double timeBarValue = (_mainStopWatch.ElapsedMilliseconds / 600000.0) % 1;

            RedBar.Rotation = sec;
            BlueBar.Rotation = lap;
            Timebar.Value = timeBarValue;

            return State == State.Started;
        }

        /// <summary>
        /// To calculate the rotation, use the seconds and the milliseconds.
        /// </summary>
        /// <param name="ts">The TimeSpan to change to angle of rotation.</param>
        /// <returns>The angle of rotation.</returns>
        static double TimeToRotation(TimeSpan ts) => ts.Seconds * 6 + ts.Milliseconds / 1000.0 * 6.0;
    }

    /// <summary>
    /// State is present a current status of StopWatch
    /// </summary>
    public enum State
    {
        Started,
        Stopped,
        Paused
    }
}
