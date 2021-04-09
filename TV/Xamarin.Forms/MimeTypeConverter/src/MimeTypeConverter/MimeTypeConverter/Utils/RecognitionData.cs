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
using System.Collections;

namespace MimeTypeConverter.Utils
{
    /// <summary>
    /// Recognition results structure.
    /// </summary>
    public struct RecognitionData
    {
        #region fields

        /// <summary>
        /// Type of input;
        /// </summary>
        public readonly InputType InputType;

        /// <summary>
        /// List of recognized items
        /// </summary>
        public readonly IEnumerable RecognizedItems;

        /// <summary>
        /// Recognized item type.
        /// </summary>
        public readonly string ItemType;

        #endregion

        #region methods

        /// <summary>
        /// Sets structure values.
        /// </summary>
        /// <param name="inputType">Initial value for InputType field.</param>
        /// <param name="recognizedItems">Initial value for RecognizedItems field.</param>
        /// <param name="itemType">Initial value for ItemType field.</param>
        public RecognitionData(InputType inputType, IEnumerable recognizedItems, string itemType)
        {
            InputType = inputType;
            RecognizedItems = recognizedItems;
            ItemType = itemType;
        }

        #endregion
    }
}
