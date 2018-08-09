
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

namespace Calculator.Models
{
    /// <summary>
    /// AddingElementResult base class to notify of element submit. 
    /// </summary>
    public class AddingElementResult
    {
        /// <summary>
        /// Message to be displayed in error pop up.
        /// </summary>
        public virtual String Message
        {
            get { return string.Empty; }
        }
    }

    /// <summary>
    /// Class used to inform that adding element is possible. 
    /// </summary>
    public class AddingPossible : AddingElementResult
    {
    }

    /// <summary>
    /// Class used to inform that  adding element is impossible.
    /// </summary>
    public class AddingImpossible : AddingElementResult
    {
    }

    /// <summary>
    /// Class used to inform that adding element is impossible due to invalid expression format. 
    /// </summary>
    public class InvalidFormatUsed : AddingElementResult
    {
        /// <summary>
        /// Message to be displayed in error pop up.
        /// </summary>
        public override String Message
        {
            get { return "Invalid format used."; }
        }
    }

    /// <summary>
    /// Class used to inform that adding element is impossible due to a 15 digits exceeding number. 
    /// </summary>
    public class CantMoreThan15Digit : AddingElementResult
    {
        /// <summary>
        /// Message to be displayed in error pop up.
        /// </summary>
        public override String Message
        {
            get { return "Can't enter more than 15 digits."; }
        }
    }

    /// <summary>
    /// Class used to inform that adding element is impossible because number contains more than 10 digits after decimal point. 
    /// </summary>
    public class CantMoreThan10Decimal : AddingElementResult
    {
        /// <summary>
        /// Message to be displayed in error pop up.
        /// </summary>
        public override String Message
        {
            get { return "Can't enter more than 10 decimal places."; }
        }
    }
}
