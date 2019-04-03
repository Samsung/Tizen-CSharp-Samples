/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
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

using System;
using Xamarin.Forms;

namespace VisionApplicationSamples
{
    /// <summary>
    /// NavigationCommands to Create Page.
    /// </summary>
    /// <typeparam name="TPage">A type of Page.</typeparam>
    class NavigationCommands<TPage> : Command
        where TPage : ContentPage, new()
    {
        public NavigationCommands()
            : base(async () => await Application.Current.MainPage.Navigation.PushAsync(new TPage()))
        {

        }

        public NavigationCommands(Func<object> bindingContextCreator)
            : base(async () => await Application.Current.MainPage.Navigation.PushAsync(
                new TPage() { BindingContext = bindingContextCreator?.Invoke() }))
        {

        }

        public NavigationCommands(Func<object> bindingContextCreator, Func<bool> canExecute)
            : base(async () => await Application.Current.MainPage.Navigation.PushAsync(
                new TPage() { BindingContext = bindingContextCreator?.Invoke() }), canExecute)
        {

        }
    }
}
