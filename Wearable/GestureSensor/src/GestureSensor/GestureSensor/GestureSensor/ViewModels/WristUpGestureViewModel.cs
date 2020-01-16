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

﻿using GestureSensor.Enums;

namespace GestureSensor.ViewModels
{
    /// <summary>
    /// WristUp gesture view model.
    /// </summary>
    public sealed class WristUpGestureViewModel : SingleGestureViewModel
    {
        /// <summary>
        /// Creates new instance of <see cref="WristUpGestureViewModel"/>
        /// </summary>
        public WristUpGestureViewModel()
        {
            Title = "WristUp";
            Type = GestureType.WristUp;
            Count = GestureService.WristUpCounter;
        }

        /// <summary>
        /// Invoked when gesture was detected.
        /// </summary>
        protected override void OnGestureDetected()
        {
            Count = GestureService.WristUpCounter;
        }
    }
}
