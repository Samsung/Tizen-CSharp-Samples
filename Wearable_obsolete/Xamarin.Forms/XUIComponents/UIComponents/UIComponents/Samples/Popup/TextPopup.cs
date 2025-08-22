/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Tizen.Wearable.CircularUI.Forms;

namespace UIComponents.Samples.Popup
{
    public class TextPopup : InformationPopup
    {
        /// <summary>
        /// Constructor of TextPopup class
        /// </summary>
        public TextPopup()
        {
            //Set text of popup
            Text = "This has only texts. This is set by object";
            //Request to dismiss this popup when back button event occurs
            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}