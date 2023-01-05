﻿/*
 * Copyright (c) 2023 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace Location
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// User needs to implement this, if required
        /// </summary>
        /// <param name="type">dispose type</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            ExitXaml();

            if (type == DisposeTypes.Explicit)
            {
                //Todo: Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.

            }

            //Todo: Release your own unmanaged resources here.
            //You should not access any managed member here except static instance.
            //because the execution order of Finalizes is non-deterministic.


            base.Dispose(type);
        }
    }
}
