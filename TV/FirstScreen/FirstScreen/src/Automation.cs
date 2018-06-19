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

using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using System;
using System.Collections.Generic;

namespace FirstScreen
{
    /// <summary>
    /// Manager automation
    /// </summary>
    public class Automation
    {
        /// <summary>
        /// Base class of automation event
        /// </summary>
        public class AutomationEventBase : EventArgs
        {
            public uint duration;
            public uint repeat;
        }

        /// <summary>
        /// Event signal delegate
        /// </summary>
        public EventHandlerWithReturnType<object, AutomationEventBase, bool> EventSignal;

        private List<AutomationEventBase> _events;
        private int _currentEvent;
        private uint _eventRepeatCounter;
        private Timer _eventTimer;

        /// <summary>
        /// Automation's constructor
        /// </summary>
        public Automation()
        {
            _eventTimer = new Timer(1000);
            _eventTimer.Tick += EventTimerTick;
            _events = new List<AutomationEventBase>();
            _currentEvent = 0;
        }

        /// <summary>
        /// Add an automation event in event list
        /// </summary>
        /// <param name="automationEvent">automation event</param>
        public void Add(AutomationEventBase automationEvent)
        {
            _events.Add(automationEvent);
        }

        /// <summary>
        /// Start event timer 
        /// </summary>
        /// <param name="initialDelay">initial delay time</param>
        public void Start(uint initialDelay = 2000)
        {
            if (_events.Count > 0)
            {
                _eventRepeatCounter = _events[0].repeat;
                _eventTimer.Interval = initialDelay;
                _eventTimer.Start();
            }
        }

        /// <summary>
        /// This Function be invoked when event timer tick
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        /// <returns>Invoke state</returns>
        public bool EventTimerTick(object sender, EventArgs e)
        {
            // Have we completed all the automation events?
            // Note: We check that at the beginning of the handler rather than after we have executed an event
            // because we want to use the delay from the later event.
            if (_currentEvent >= _events.Count)
            {
                Console.WriteLine("Benchmark complete. Exiting...");
                //Application.Instance.Quit();
                // We must return here as the quit is not executed immediately, but queued.
                return false;
            }

            // Execute the users custom event handler code.
            bool returnValue = EventSignal.Invoke(this, _events[_currentEvent]);

            // Update the delay until the next automation event.
            _eventTimer.Interval = _events[_currentEvent].duration;

            // If we are repeating, wait until all repeats are complete before moving to the next event.
            if (--_eventRepeatCounter <= 0)
            {
                // Move to the next event. Check if it exists. If so we update the repeat counter now.
                // We don't exit yet as we want to use the delay specified in the last event.
                if (++_currentEvent < _events.Count)
                {
                    _eventRepeatCounter = _events[_currentEvent].repeat;
                }
            }

            return returnValue;
        }
    }

}
