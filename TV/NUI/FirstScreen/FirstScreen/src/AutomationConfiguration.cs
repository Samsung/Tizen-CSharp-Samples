/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
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
 *
 */

using Tizen.NUI.BaseComponents;

namespace FirstScreen
{
    public class AutomationConfiguration
    {
        /// <summary>
        /// The TVAutomationEvent is TV automation EventArgs
        /// </summary>
        public class TVAutomationEvent : Automation.AutomationEventBase
        {
            /// <summary>
            /// View focus direction
            /// </summary>
            public View.FocusDirection direction;

            /// <summary>
            /// Creates and initializes a new instance of the TVAutomationEvent class.
            /// </summary>
            /// <param name="constructorDirection">constructor direction</param>
            /// <param name="constructorDuration">constructor duration</param>
            /// <param name="constructorRepeat">constructor repeat</param>
            public TVAutomationEvent(View.FocusDirection constructorDirection, uint constructorDuration, uint constructorRepeat = 1)
            {
                // Duration & repeat are stored in the base automation class, as they are common to all automation events.
                duration = constructorDuration;
                repeat = constructorRepeat;
                // Direction is custom for our TV automation event.
                direction = constructorDirection;
            }
        }

        /// <summary>
        /// Add TVAutomation event to automation
        /// </summary>
        /// <param name="automation">the automation to configure</param>
        /// <param name="menuItemCount">menu item count</param>
        public void Configure(Automation automation, int menuItemCount)
        {
            // Test lower menu (including scroll end).
            automation.Add(new TVAutomationEvent(View.FocusDirection.Right, 700));
            automation.Add(new TVAutomationEvent(View.FocusDirection.Left, 700));
            automation.Add(new TVAutomationEvent(View.FocusDirection.Right, 200, 10));
            automation.Add(new TVAutomationEvent(View.FocusDirection.Left, 200, 8));
            automation.Add(new TVAutomationEvent(View.FocusDirection.Left, 800, 3));
            automation.Add(new TVAutomationEvent(View.FocusDirection.Right, 500, 3));

            // Test all poster menus (including scroll end).
            for (uint i = 0; i < menuItemCount; ++i)
            {
                automation.Add(new TVAutomationEvent(View.FocusDirection.Up, 600));
                automation.Add(new TVAutomationEvent(View.FocusDirection.Right, 150, 10));
                automation.Add(new TVAutomationEvent(View.FocusDirection.Left, 150, 12 + 1));
                automation.Add(new TVAutomationEvent(View.FocusDirection.Down, 600));
                automation.Add(new TVAutomationEvent(View.FocusDirection.Right, 400));
            }

            // Test system menu and focusing between items in system menu & poster menu.
            automation.Add(new TVAutomationEvent(View.FocusDirection.Left, 10, 12));
            automation.Add(new TVAutomationEvent(View.FocusDirection.Up, 600));
            automation.Add(new TVAutomationEvent(View.FocusDirection.Down, 600));
            automation.Add(new TVAutomationEvent(View.FocusDirection.Right, 100));
            automation.Add(new TVAutomationEvent(View.FocusDirection.Up, 600));
            automation.Add(new TVAutomationEvent(View.FocusDirection.Down, 600));
            automation.Add(new TVAutomationEvent(View.FocusDirection.Right, 100));
            automation.Add(new TVAutomationEvent(View.FocusDirection.Up, 600));
            automation.Add(new TVAutomationEvent(View.FocusDirection.Down, 600));

            // Test swapping focus between menu & poster-menu.
            automation.Add(new TVAutomationEvent(View.FocusDirection.Right, 100));
            for (uint i = 0; i < 10; ++i)
            {
                automation.Add(new TVAutomationEvent(View.FocusDirection.Up, 1 + 600 - (i * 60)));
                automation.Add(new TVAutomationEvent(View.FocusDirection.Down, 1 + 600 - (i * 60)));
            }

            // Test pulse and remote animation.
            automation.Add(new TVAutomationEvent(View.FocusDirection.Right, 500));
            automation.Add(new TVAutomationEvent(View.FocusDirection.Right, 25000));
        }
    }

}
