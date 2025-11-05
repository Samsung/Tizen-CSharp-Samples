/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace ApplicationControl.Models
{
    /// <summary>
    /// A class for a composed message will be sent
    /// </summary>
    public class Message
    {
        /// <summary>
        /// A constructor for the Message class
        /// </summary>
        public Message()
        {
            Initialize();
        }

        /// <summary>
        /// An email address to send
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// An Subject for the message
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// An text content to send
        /// </summary>
        public string Text { get; set; }

        void Initialize()
        {
            Subject = "Message from appcontrol";
            Text = "Dear Developer,\n\nThis is the default message sent from\nappcontrol sample application.\nFeel free to modify this text message in email composer.\n\nBest Regards.";
        }
    }
}
