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
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Models
{
    /// <summary>
    /// A class represents numbers. </summary>
    public class Literal : InputElement
    {
        /// <summary>
        /// Max digits for a number </summary>
        private const int MAX_DIGITS = 15;
        /// <summary>
        /// number value </summary>
        private string literal;

        /// <summary>
        /// Literal constructor for a text formatted number. </summary>
        /// <param name="value"> A number value string </param>
        public Literal(string value)
        {
            literal = (value != null) ? value : string.Empty;
        }

        /// <summary>
        /// Literal constructor for a double typed number. </summary>
        /// <param name="value"> A number value </param>
        public Literal(double value)
        {
            literal = value.ToString();
        }

        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return literal;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return literal;
            }
        }

        /// <summary>
        /// A flag value indicates literal is exceeded MAX digits. </summary>
        public bool IsMaxDigits
        {
            get
            {
                return literal.Length >= MAX_DIGITS;
            }
        }

        /// <summary>
        /// A method provides possibility of adding followingElement after this Literal.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="IsEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="IsLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="right"> A input element will be adding next of this element. </param>
        /// <returns> A status of adding element to current expression. </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements,
            ref bool IsEqualUsed,
            ref bool IsLastValidationSucceed,
            InputElement right)
        {
            if (right is Literal)
            {
                Literal lastLiteral = expressionElements.Last() as Literal;

                if (IsEqualUsed &&
                    IsLastValidationSucceed)
                {
                    expressionElements.Clear();
                    expressionElements.Add(right);
                }
                else
                {
                    // Exceptional case, prohibit 15 digits
                    if (lastLiteral.GetElement.Length > 14)
                    {
                        return new CantMoreThan15Digit();
                    }

                    // Exceptional case, prohibit 10 decimal places,
                    // OK - 0.0000000000 (10)
                    // NO - 0.00000000000 (11)
                    if (expressionElements.Count > 2 &&
                        lastLiteral.GetElement.Length == 10 &&
                        expressionElements[expressionElements.Count - 2] is Point)
                    {
                        return new CantMoreThan10Decimal();
                    }

                    if (lastLiteral.Append(right))
                    {
                        return new AddingPossible();
                    }

                    return new InvalidFormatUsed();
                }
            }
            else
            {
                IOperator rightOper = right as IOperator;

                if (right is INullary ||
                OperandType.RIGHT == rightOper.OperandType)
                {
                    expressionElements.Add(new Multiplication());
                }

                expressionElements.Add(right);

                right.PostAddingWork(ref expressionElements);
            }

            return new AddingPossible();
        }

        /// <summary>
        /// A methods appends a given literal.</summary>
        /// <param name="value"> A InputElement(Literal) </param>
        /// <returns> A result status of appending </returns>
        public bool Append(InputElement value)
        {
            double number;
            if (Double.TryParse(value.GetElement, out number) == false)
            {
                return false;
            }

            literal += value.GetElement;
            return true;
        }
    }
}
