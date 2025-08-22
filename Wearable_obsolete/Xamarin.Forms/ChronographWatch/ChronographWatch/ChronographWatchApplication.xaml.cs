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

using ChronographWatch.Converters;
using System;
using System.Diagnostics;
using Tizen.System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChronographWatch
{
    /// <summary>
    /// ChronographWatch Application class
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChronographWatchApplication : Application
	{
        WatchViewModel _viewModel;
        Stopwatch _secondStopWatch;
        Stopwatch _elapsedStopWatch;
        DateTime _time;
        bool _moving;

        /// <summary>
        /// Constructor of ChronographWatch Application
        /// </summary>
        /// <param name="viewModel">WatchViewModel</param>
        public ChronographWatchApplication(WatchViewModel viewModel)
		{
            _viewModel = viewModel;
            InitializeComponent();
            BindingContext = viewModel;
        }

        /// <summary>
        /// Rotate Watch hands
        /// </summary>
        public void MoveHands()
        {
            //Set Rotation degree each hour/min/sec hand
            hand_hr.Rotation = _viewModel.HourRotation;
            hand_min.Rotation = _viewModel.MinuteRotation;

            //Set Rotation degree each hour/min/sec hand shadow
            hand_hr_shadow.Rotation = _viewModel.HourRotation;
            hand_min_shadow.Rotation = _viewModel.MinuteRotation;
        }

        /// <summary>
        /// Start rotation of second hand
        /// </summary>
        public void RunSecHand()
        {
            if (_secondStopWatch == null)
            {
                Console.WriteLine($"create new second stopwatch");
                _secondStopWatch = new Stopwatch();
            }

            _secondStopWatch.Reset();
            _time = _viewModel.Time;
            DateTime now ;
            // Callback function that is called every 200ms.
            Func<bool> callback = () =>
            {
                //Console.WriteLine($"callback _time:{_time}, _viewModel.Time{_viewModel.Time}");
                if (_viewModel.IsAmbientMode == false && _time != _viewModel.Time)
                {
                    _time = _viewModel.Time;
                    _secondStopWatch.Restart();
                }

                now = _time + TimeSpan.FromMilliseconds(_secondStopWatch.ElapsedMilliseconds);
                //Console.WriteLine($"callback now:{now},   _viewModel.Time{_viewModel.Time}");
                double sec = now.Second * 6  + now.Millisecond / 1000.0 * 6.0;

                hand_sec.Rotation = sec;
                hand_sec_shadow.Rotation = sec;

                if (_viewModel.IsAmbientMode)
                {
                    // In Ambient mode, OnTick is not called. 
                    // so rotate min/hour hand in this recurring callback.
                    _viewModel.Time = now;
                    MoveHands();   
                }

                return _moving;
            };
            Device.BeginInvokeOnMainThread(() =>
            {
                _moving = true;
                _secondStopWatch.Start();
                Console.WriteLine($"start second stopwatch ");
                if (callback())
                {
                    // timer starts for invoking callback every 200ms.
                    Device.StartTimer(TimeSpan.FromMilliseconds(200), callback);
                }
            });
        }

        /// <summary>
        /// Stop rotation of second hand
        /// </summary>
        public void StopSecHand()
        {
            _moving = false;
            _secondStopWatch.Stop();
        }

        /// <summary>
        /// Start measuring time in Chronograph mode.
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Start()");
            if (_elapsedStopWatch == null)
            {
                Console.WriteLine($"create new elapsed time stopwatch");
                _elapsedStopWatch = new Stopwatch();
            }

            //request cpu lock for prevent stop of timer.
            Device.BeginInvokeOnMainThread(() => Power.RequestLock(PowerLock.Cpu, 0));
            _viewModel.State = State.Started;
            _elapsedStopWatch.Start();


            // Synchronize the TimeSpan property on every 100 milliseconds.
            Device.StartTimer(TimeSpan.FromMilliseconds(100), OnTimeChanged);
        }

        /// <summary>
        /// Pause measuring time in Chronograph mode.
        /// </summary>
        public void Pause()
        {
            Console.WriteLine("Pause()");
            _viewModel.State = State.Paused;
            _elapsedStopWatch.Stop();
            //release cpu lock
            Device.BeginInvokeOnMainThread(() => Power.ReleaseLock(PowerLock.Cpu));
        }

        /// <summary>
        /// Stop measuring time in Chronograph mode.
        /// </summary>
        public void Stop()
        {
            Console.WriteLine("Stop()");
            _viewModel.State = State.Stopped;
            _elapsedStopWatch.Reset();

            ResetHands();
            //release cpu lock
            Device.BeginInvokeOnMainThread(() => Power.ReleaseLock(PowerLock.Cpu));
        }

        /// <summary>
        /// Reset postion of all hand
        /// </summary>
        public void ResetHands()
        {
            //reset stopwatch hand
            _viewModel.ElapsedTime = TimeSpan.Zero;
            hand_stopwatch_sec.Rotation = _viewModel.StopWatchSecRotation;
            hand_stopwatch_30_min.Rotation = _viewModel.StopWatchMin30Rotation;
            hand_stopwatch_12_hr.Rotation = _viewModel.StopWatchHr12Rotation;
        }
    

        /// <summary>
        /// Set the elapsed time to ElapsedTime property
        /// </summary>
        /// <returns>the timer will keep recurring if State is Started.</returns>
        bool OnTimeChanged()
        {
            _viewModel.ElapsedTime = _elapsedStopWatch.Elapsed;
            hand_stopwatch_sec.Rotation = _viewModel.StopWatchSecRotation;
            hand_stopwatch_30_min.Rotation = _viewModel.StopWatchMin30Rotation;
            hand_stopwatch_12_hr.Rotation = _viewModel.StopWatchHr12Rotation;

            //if _viewModel.State is started, keep recurring
            return _viewModel.State == State.Started;
        }

        /// <summary>
        /// Upward circle image button tapped event that can start chronograph mode and start measuring.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">An object that contains no event data.</param>
        void OnEnterEventTapped(object sender, EventArgs args)
        {
            Console.WriteLine("OnEnterEventTapped()");
            DoSpotAnimation(sender as Image);

            if (_viewModel.Mode == Mode.Chronograph)
            {
                Console.WriteLine("Mode is already Chronograph");
                return;
            }

            _viewModel.Mode = Mode.Chronograph;
            StopSecHand();
            Start();
        }

        /// <summary>
        /// Left side button tapped event that can pause/restart measuring
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">An object that contains no event data.</param>
        void OnLeftEventTapped(object sender, EventArgs args)
        {
            Console.WriteLine("OnLeftEventTapped()");
            DoSpotAnimation(sender as Image);

            if (_viewModel.Mode != Mode.Chronograph)
            {
                Console.WriteLine("Mode is not Chronograph");
                return;
            }

            if (_viewModel.State == State.Stopped || _viewModel.State == State.Paused)
            {
                Start();
            }
            else if (_viewModel.State == State.Started)
            {
                Pause();
            }
        }

        /// <summary>
        /// right side button tapped event that can stop the measuring and exit chronograph mode.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">An object that contains no event data.</param>
        void OnRightEventTapped(object sender, EventArgs args)
        {
            Console.WriteLine("OnRightEventTapped()");
            DoSpotAnimation(sender as Image);

            if (_viewModel.Mode != Mode.Chronograph)
            {
                Console.WriteLine("Mode is not Chronograph");
                return;
            }

            Stop();
            RunSecHand();
            _viewModel.Mode = Mode.Watch;
        }

        /// <summary>
        /// this method is used to show the button clicking effect
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
    }
}