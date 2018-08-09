
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
using System.Collections.Generic;

namespace Calculator.Models
{
    /// <summary>
    /// A base class for the calculating elements(Number, Operators).
    /// The InputElemet can be a Literal and any other operators that inherit the InputElement and IOperator. </summary>
    public abstract class InputElement : IComparable<string>
    {
        /// <summary>
        /// A property provides element string.
        /// This property is used for validation and calculation </summary>
        public abstract string GetElement { get; }

        /// <summary>
        /// A property provides displaying element string.
        /// This property is used for building formatted string. </summary>
        public abstract string GetDisplayElement { get; }

        /// <summary>
        /// The method provides whether it is possible to add followingElement after this InputElement(is placed at the end of expressionElements).
        /// This method can modify current expression by adding new element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, an InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="followingElement"> An input element will be added after this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        /// <seealso cref="AlternativeWork(ref List{InputElement}, ref bool, ref bool)"/>
        public abstract AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements,
            ref bool isEqualUsed,
            ref bool isLastValidationSucceed,
            InputElement followingElement);

        /// <summary>
        /// The method doing some exceptional or an independent work instead of adding this InputElement.
        /// If this methods return true, calculator do not call the CheckPossibilityAddingElement method.</summary>
        /// <param name="expressionElements"> Current expression, an InputElement list </param>
        /// <param name="IsEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="IsLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <returns> A status of operator's alternative work  </returns>
        /// <seealso cref="AddingElementResult"/>
        public virtual bool AlternativeWork(ref List<InputElement> expressionElements,
            ref bool IsEqualUsed,
            ref bool IsLastValidationSucceed)
        {
            return false;
        }

        /// <summary>
        /// The method doing additional work after added as followingElement.</summary>
        /// <param name="expressionElements"> Current expression, an InputElement list </param>
        public virtual void PostAddingWork(ref List<InputElement> expressionElements)
        {
        }

        public static implicit operator string(InputElement ie)
        {
            return ie.GetElement;
        }

        public override string ToString()
        {
            return GetElement;
        }

        public int CompareTo(string other)
        {
            return other.CompareTo(GetElement);
        }
    }
}
