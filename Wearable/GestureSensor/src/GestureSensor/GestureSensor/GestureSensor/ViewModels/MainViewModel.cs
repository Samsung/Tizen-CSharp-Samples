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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using XFCommand = Xamarin.Forms.Command;

namespace GestureSensor.ViewModels
{
    /// <summary>
    /// The application's main view model class.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IGestureService _gestureService;

        /// <summary>
        /// Helper dictionary of gestures types and their values.
        /// </summary>
        private readonly IDictionary<GestureType, GestureState> _states;

        /// <summary>
        /// Backing field for <see cref="WristUpState"/>
        /// </summary>
        private GestureState _wristUpState;

        /// <summary>
        /// Backing field for <see cref="FaceDownState"/>
        /// </summary>
        private GestureState _faceDownState;

        /// <summary>
        /// Backing field for <see cref="PickUpState"/>
        /// </summary>
        private GestureState _pickUpState;

        /// <summary>
        /// Gets or sets WristUp gesture state.
        /// </summary>
        public GestureState WristUpState
        {
            get => _wristUpState;
            set => SetProperty(ref _wristUpState, value);
        }

        /// <summary>
        /// Gets or sets FaceDown gesture state.
        /// </summary>
        public GestureState FaceDownState
        {
            get => _faceDownState;
            set => SetProperty(ref _faceDownState, value);
        }

        /// <summary>
        /// Gets or sets PickUp gesture state.
        /// </summary>
        public GestureState PickUpState
        {
            get => _pickUpState;
            set => SetProperty(ref _pickUpState, value);
        }

        /// <summary>
        /// Executed on WristUp icon tapped.
        /// </summary>
        public ICommand WristUpTapped { get; private set; }

        /// <summary>
        /// Executed on WristUp icon pressed.
        /// </summary>
        public ICommand WristUpPressed { get; private set; }

        /// <summary>
        /// Executed on WristUp icon released.
        /// </summary>
        public ICommand WristUpReleased { get; private set; }

        /// <summary>
        /// Executed on FaceDown icon tapped.
        /// </summary>
        public ICommand FaceDownTapped { get; private set; }

        /// <summary>
        /// Executed on FaceDown icon pressed.
        /// </summary>
        public ICommand FaceDownPressed { get; private set; }

        /// <summary>
        /// Executed on FaceDown icon released.
        /// </summary>
        public ICommand FaceDownReleased { get; private set; }

        /// <summary>
        /// Executed on PickUp icon tapped.
        /// </summary>
        public ICommand PickUpTapped { get; private set; }

        /// <summary>
        /// Executed on PickUp icon pressed.
        /// </summary>
        public ICommand PickUpPressed { get; private set; }

        /// <summary>
        /// Executed on PickUp icon released.
        /// </summary>
        public ICommand PickUpReleased { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            _navigationService = Xamarin.Forms.DependencyService.Get<INavigationService>();
            _gestureService = Xamarin.Forms.DependencyService.Get<IGestureService>();

            _gestureService.Initialize();

            _states = new Dictionary<GestureType, GestureState>()
            {
                { GestureType.WristUp, WristUpState },
                { GestureType.FaceDown, FaceDownState },
                { GestureType.PickUp, PickUpState },
            };

            _gestureService.GestureUpdated += GestureUpdated;

            InitializeWristUpCommands();
            InitializeFaceDownCommands();
            InitializePickUpCommands();
        }

        /// <summary>
        /// Initializes WristUp gesture commands.
        /// </summary>
        private void InitializeWristUpCommands()
        {
            WristUpTapped = new XFCommand(() =>
            {
                if (WristUpState == GestureState.Pressed)
                {
                    _navigationService.NavigateTo<WristUpGestureViewModel>();
                }
            });

            WristUpPressed = new XFCommand(() =>
            {
                if (WristUpState == GestureState.Normal)
                {
                    WristUpState = GestureState.Pressed;
                }
            });

            WristUpReleased = new XFCommand(() =>
            {
                if (WristUpState == GestureState.Pressed)
                {
                    WristUpState = GestureState.Normal;
                }
            });
        }

        /// <summary>
        /// Initializes FaceDown gesture commands.
        /// </summary>
        private void InitializeFaceDownCommands()
        {
            FaceDownTapped = new XFCommand(() =>
            {
                if (FaceDownState == GestureState.Pressed)
                {
                    _navigationService.NavigateTo<FaceDownGestureViewModel>();
                }
            });

            FaceDownPressed = new XFCommand(() =>
            {
                if (FaceDownState == GestureState.Normal)
                {
                    FaceDownState = GestureState.Pressed;
                }
            });

            FaceDownReleased = new XFCommand(() =>
            {
                if (FaceDownState == GestureState.Pressed)
                {
                    FaceDownState = GestureState.Normal;
                }
            });
        }

        /// <summary>
        /// Initializes PickUp gesture commands.
        /// </summary>
        private void InitializePickUpCommands()
        {
            PickUpTapped = new XFCommand(() =>
            {
                if (PickUpState == GestureState.Pressed)
                {
                    _navigationService.NavigateTo<PickUpGestureViewModel>();
                }
            });

            PickUpPressed = new XFCommand(() =>
            {
                if (PickUpState == GestureState.Normal)
                {
                    PickUpState = GestureState.Pressed;
                }
            });

            PickUpReleased = new XFCommand(() =>
            {
                if (PickUpState == GestureState.Pressed)
                {
                    PickUpState = GestureState.Normal;
                }
            });
        }

        /// <summary>
        /// Gesture changed event handler.
        /// </summary>
        /// <param name="gestureType">Gesture type.</param>
        /// <param name="isDetected">Indicates if gesture was detected or not.</param>
        private void GestureUpdated(GestureType gestureType, bool isDetected)
        {
            var otherGestureStates = _states.Where(s => s.Key != gestureType);

            if (isDetected)
            {
                ChangeState(gestureType, GestureState.Active);
                if (!otherGestureStates.Any(s => s.Value == GestureState.Active))
                {
                    foreach (var s in otherGestureStates.ToList())
                    {
                        ChangeState(s.Key, GestureState.Disabled);
                    }
                }
            }
            else
            {
                if (otherGestureStates.Any(s => s.Value == GestureState.Active))
                {
                    ChangeState(gestureType, GestureState.Disabled);
                }
                else
                {
                    ChangeState(gestureType, GestureState.Normal);
                    foreach (var s in otherGestureStates.ToList())
                    {
                        ChangeState(s.Key, GestureState.Normal);
                    }
                }
            }
        }

        /// <summary>
        /// Updates state of the gesture.
        /// </summary>
        /// <param name="type">Type of the gesture.</param>
        /// <param name="newState">New state of the gesture.</param>
        private void ChangeState(GestureType type, GestureState newState)
        {
            _states[_states.FirstOrDefault(s => s.Key == type).Key] = newState;
            switch (type)
            {
                case GestureType.WristUp:
                    WristUpState = newState;
                    break;
                case GestureType.FaceDown:
                    FaceDownState = newState;
                    break;
                case GestureType.PickUp:
                    PickUpState = newState;
                    break;
            }
        }
    }
}
