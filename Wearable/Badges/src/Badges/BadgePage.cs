
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
using Tizen;
using Tizen.Applications;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Application = Tizen.Applications.Application;

namespace Badges
{
    public class BadgePage : CirclePage, IRotaryEventReceiver
    {
        /// <summary>
        /// PopupEntry from this Layout.
        /// </summary>
        private PopupEntry popupEntry;

        /// <summary>
        /// ApplicationId of this application.
        /// </summary>
        private readonly string applicationId;

        /// <summary>
        /// BadgeValue property back-field.
        /// </summary>
        private int badgeValue;

        /// <summary>
        /// Value to which badge count will be set.
        /// </summary>
        public int BadgeValue
        {
            set
            {
                badgeValue = Math.Clamp(value, 0, 10000);
                popupEntry.Text = badgeValue.ToString();
            }
            get { return badgeValue; }
        }

        /// <summary>
        /// BadgePage class constructor.
        /// </summary>
        public BadgePage()
        {
            applicationId = Application.Current.ApplicationInfo.ApplicationId;

            Content = CreateLayout();

            RotaryFocusObject = this;

            BadgeValue = GetBadge()?.Count ?? 0;
        }

        /// <summary>
        /// This function creates layout for current page.
        /// </summary>
        /// <returns> Layout for current page </returns>
        private View CreateLayout()
        {
            var label = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Set badge count:"
            };

            popupEntry = new PopupEntry();
            popupEntry.TextChanged += OnTextChanged;

            var applyButton = new Button
            {
                Text = "Apply",
                Command = new Command(ApplyBadge),
            };

            var resetButton = new Button
            {
                Text = "Reset",
                Command = new Command(ResetBadge),
            };

            var stack = new StackLayout
            {
                Children = {
                    label,
                    popupEntry,
                    applyButton,
                    resetButton,
                },
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(36, 0),
            };

            return stack;
        }
        
        /// <summary>
        /// Function executed before page becomes visible.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            popupEntry.HorizontalTextAlignment = TextAlignment.Center;
        }

        /// <summary>
        /// Event handler that is called when popupEntry's text changes.
        /// </summary>
        /// <param name="sender"> Object that raised event </param>
        /// <param name="e"> Event arguments </param>
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(popupEntry.Text, out int value))
            {
                BadgeValue = value;
            }
            popupEntry.Text = BadgeValue.ToString();
        }

        /// <summary>
        /// This function removes badge from current application.
        /// </summary>
        private void ResetBadge()
        {
            BadgeValue = 0;
            try
            {
                BadgeControl.Remove(applicationId);
            }
            catch (Exception e)
            {
                Log.Debug("Badges", $"Error when removing badge. {e.GetType()}, {e.Message}");
            }
        }

        /// <summary>
        /// This Function sets badge value of current application to BadgeValue.
        /// </summary>
        private void ApplyBadge()
        {
            Badge current = GetBadge();
            try
            {
                if (current == null)
                {
                    BadgeControl.Add(new Badge(applicationId, BadgeValue));
                }
                else
                {
                    current.Count = BadgeValue;
                    current.Visible = true;
                    BadgeControl.Update(current);
                }
            }
            catch (Exception e)
            {
                Log.Debug("Badges", $"Failed to set badge count. {e.GetType()}, {e.Message}");
            }
        }

        /// <summary>
        /// Returns badge for current application.
        /// If current application has no badge returns null.
        /// </summary>
        /// <returns> Found badge </returns>
        private Badge GetBadge()
        {
            Badge badge = null;
            try
            {
                badge = BadgeControl.Find(applicationId);
            }
            catch (Exception e)
            {
                Log.Debug("Badges", $"Failed to find badge. {e.GetType()}, {e.Message}");
            }
            return badge;
        }

        /// <summary>
        /// This function handles RotaryEvent.
        /// </summary>
        /// <param name="args"> Rotary event arguments </param>
        public void Rotate(RotaryEventArgs args)
        {
            BadgeValue = BadgeValue + ((args.IsClockwise) ? 1 : -1);
        }
    }
}