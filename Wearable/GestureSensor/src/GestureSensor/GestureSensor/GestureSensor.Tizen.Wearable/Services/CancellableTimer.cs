/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace GestureSensor.Tizen.Wearable.Services
{
    /// <summary>
    /// Provides timer that can be cancelled in any time.
    /// </summary>
    public class CancellableTimer : IDisposable
    {
        private readonly TimeSpan _interval;
        private readonly Action _callback;
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Indicates if task is running.
        /// </summary>
        public bool IsRunning { get; private set; } = false;

        /// <summary>
        /// Creates new instance of <see cref="CancellableTimer"/>
        /// </summary>
        /// <param name="interval">Timer delay.</param>
        /// <param name="callback">Function delegate that will be invoke after interval.</param>
        public CancellableTimer(TimeSpan interval, Action callback)
        {
            _interval = interval;
            _callback = callback;
        }

        /// <summary>
        /// Starts timer.
        /// </summary>
        public void Start()
        {
            IsRunning = true;
            _cancellationTokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(async () =>
            {
                var cancellationToken = _cancellationTokenSource.Token;
                await Task.Delay(_interval, cancellationToken);
                if (_cancellationTokenSource.IsCancellationRequested)
                {
                    return;
                }

                _callback.Invoke();
            },
            _cancellationTokenSource.Token);
        }

        /// <summary>
        /// Stops timer.
        /// </summary>
        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            IsRunning = false;
        }

        /// <summary>
        /// Disposes used resources.
        /// </summary>
        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }
    }
}
