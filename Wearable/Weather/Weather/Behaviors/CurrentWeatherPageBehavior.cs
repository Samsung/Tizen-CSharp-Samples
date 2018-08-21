//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using Xamarin.Forms;

namespace Weather.Behaviors
{
    /// <summary>
    /// Content page behavior that allows to bind command to "Appearing" event.
    /// </summary>
    public class CurrentWeatherPageBehavior : Behavior<ContentPage>
    {
        #region properties

        /// <summary>
        /// Bindable property that allows to set command that will be executed during page appearing.
        /// </summary>
        public static readonly BindableProperty AppearingCommandProperty =
            BindableProperty.Create(nameof(AppearingCommand), typeof(Command), typeof(CurrentWeatherPageBehavior),
                default(Command));

        /// <summary>
        /// Command that will be executed during page appearing.
        /// </summary>
        public Command AppearingCommand
        {
            get => (Command)GetValue(AppearingCommandProperty);
            set => SetValue(AppearingCommandProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Called when behavior is attached to content page.
        /// </summary>
        /// <param name="bindable">Object to attach behavior.</param>
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Appearing += PageOnAppearing;
        }

        /// <summary>
        /// Called when behavior is detached from content page.
        /// </summary>
        /// <param name="bindable">Object to detach behavior.</param>
        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Appearing -= PageOnAppearing;
        }

        /// <summary>
        /// Called when content page is appearing.
        /// </summary>
        /// <param name="sender">Content page that sent event.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void PageOnAppearing(object sender, EventArgs eventArgs)
        {
            if (!((App)Application.Current).IsInitialized)
            {
                AppearingCommand?.Execute(sender);
            }
        }

        #endregion
    }
}