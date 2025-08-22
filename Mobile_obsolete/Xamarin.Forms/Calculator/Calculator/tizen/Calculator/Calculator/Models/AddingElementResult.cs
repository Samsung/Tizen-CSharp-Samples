/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

using System;

namespace Calculator.Models
{
    /// <summary>
    /// A AddingElementResult base class to notify of element submit. </summary>
    public class AddingElementResult
    {
        /// <summary>
        /// A message will be displayed in error pop up . </summary>
        public virtual String Message
        {
            get { return string.Empty; }
        }
    }

    /// <summary>
    /// A AddingElementResult class describes adding element is possible. </summary>
    public class AddingPossible : AddingElementResult
    {
    }

    /// <summary>
    /// A AddingElementResult class describes adding element is impossible. </summary>
    public class AddingImpossible : AddingElementResult
    {
    }

    /// <summary>
    /// A AddingElementResult class describes adding element is impossible
    /// due to invalid expression format. </summary>
    public class InvalidFormatUsed : AddingElementResult
    {
        /// <summary>
        /// A message will be displayed in error pop up . </summary>
        public override String Message
        {
            get { return "Invalid format used."; }
        }
    }

    /// <summary>
    /// A AddingElementResult class describes adding element is impossible
    /// due to a 15 digits exceeding number. </summary>
    public class CantMoreThan15Digit : AddingElementResult
    {
        /// <summary>
        /// A message will be displayed in error pop up . </summary>
        public override String Message
        {
            get { return "Can't enter more than 15 digits."; }
        }
    }

    /// <summary>
    /// A AddingElementResult class describes adding element is impossible
    /// due to a more than 10 number after decimal point. </summary>
    public class CantMoreThan10Decimal : AddingElementResult
    {
        /// <summary>
        /// A message will be displayed in error pop up . </summary>
        public override String Message
        {
            get { return "Can't enter more than 10 decimal places."; }
        }
    }
}
