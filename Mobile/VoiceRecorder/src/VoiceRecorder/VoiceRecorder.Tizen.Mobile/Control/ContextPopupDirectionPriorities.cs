/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
namespace VoiceRecorder.Tizen.Mobile.Control
{
    /// <summary>
    /// The direction priorities of a ContextPopup.
    /// </summary>
    public struct ContextPopupDirectionPriorities
    {
        #region properties

        /// <summary>
        /// Gets the first direction priority.
        /// </summary>
        public ContextPopupDirection First { get; private set; }

        /// <summary>
        /// Gets the second direction priority.
        /// </summary>
        public ContextPopupDirection Second { get; private set; }

        /// <summary>
        /// Gets the third direction priority.
        /// </summary>
        public ContextPopupDirection Third { get; private set; }

        /// <summary>
        /// Gets the fourth direction priority.
        /// </summary>
        public ContextPopupDirection Fourth { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Creates a ContextPopupDirectionPriorities structure.
        /// </summary>
        /// <param name="first">The first direction priority.</param>
        /// <param name="second">The second direction priority.</param>
        /// <param name="third">The third direction priority.</param>
        /// <param name="fourth">The fourth direction priority.</param>
        public ContextPopupDirectionPriorities(ContextPopupDirection first, ContextPopupDirection second, ContextPopupDirection third, ContextPopupDirection fourth)
        {
            First = first;
            Second = second;
            Third = third;
            Fourth = fourth;
        }

        #endregion
    }
}