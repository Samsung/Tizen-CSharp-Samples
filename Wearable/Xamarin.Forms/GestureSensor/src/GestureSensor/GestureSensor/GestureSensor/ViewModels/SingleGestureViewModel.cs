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
using GestureSensor.Interfaces;

namespace GestureSensor.ViewModels
{
    /// <summary>
    /// Abstraction of single gesture view model.
    /// </summary>
    public abstract class SingleGestureViewModel : BaseViewModel
    {
        protected readonly IGestureService GestureService;

        /// <summary>
        /// Backing field for <see cref="Count"/>
        /// </summary>
        private int _count;

        /// <summary>
        /// Backing field for <see cref="Title"/>
        /// </summary>
        private string _title;

        /// <summary>
        /// Backing field for <see cref="Type"/>
        /// </summary>
        private GestureType _type;

        /// <summary>
        /// Gets or sets number indicating how many times gesture was detected.
        /// </summary>
        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

        /// <summary>
        /// Gets or sets page's title.
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// Gets or sets gesture type.
        /// </summary>
        public GestureType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        /// <summary>
        /// Creates new instance of <see cref="SingleGestureViewModel"/>
        /// </summary>
        public SingleGestureViewModel()
        {
            GestureService = Xamarin.Forms.DependencyService.Get<IGestureService>();
            GestureService.GestureUpdated += GestureUpdated;
        }

        /// <summary>
        /// Gesture changed event handler.
        /// </summary>
        /// <param name="type">Gesture type.</param>
        /// <param name="isDetected">Indicates if gesture was detected or not.</param>
        private void GestureUpdated(GestureType type, bool isDetected)
        {
            if (isDetected && type == Type)
            {
                OnGestureDetected();
            }
        }

        /// <summary>
        /// Invoked when gesture was detected.
        /// </summary>
        protected abstract void OnGestureDetected();
    }
}
