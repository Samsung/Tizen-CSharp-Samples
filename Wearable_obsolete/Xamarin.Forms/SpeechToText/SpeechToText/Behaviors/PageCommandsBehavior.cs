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
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechToText.Behaviors
{
    /// <summary>
    /// Behavior class which transforms page events into corresponding commands.
    /// </summary>
    class PageCommandsBehavior : Behavior<Page>
    {
        #region properties

        /// <summary>
        /// Property definition for appearing command.
        /// </summary>
        public static readonly BindableProperty AppearingCommandProperty =
            BindableProperty.Create("AppearingCommand", typeof(ICommand), typeof(PageCommandsBehavior));

        /// <summary>
        /// Command executed when page appears.
        /// </summary>
        public ICommand AppearingCommand
        {
            get => (ICommand)GetValue(AppearingCommandProperty);
            set => SetValue(AppearingCommandProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Called when the behavior is attached to the page.
        /// Registers callbacks for page events.
        /// </summary>
        /// <param name="bindable">The page to which the behavior is attached.</param>
        protected override void OnAttachedTo(Page bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.Appearing += PageOnAppearing;
        }

        /// <summary>
        /// Called when the behavior is removed from the attached page.
        /// Removes registered page events callbacks.
        /// </summary>
        /// <param name="bindable">The page to which the behavior is attached.</param>
        protected override void OnDetachingFrom(Page bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.Appearing -= PageOnAppearing;
        }

        /// <summary>
        /// Handles page appearing event.
        /// Executes corresponding command.
        /// </summary>
        /// <param name="sender">Event sender (page).</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void PageOnAppearing(object sender, EventArgs eventArgs)
        {
            AppearingCommand?.Execute(null);
        }

        #endregion
    }
}