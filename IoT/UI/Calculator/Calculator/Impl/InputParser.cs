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

using Calculator.Models;

namespace Calculator.Impl
{
    /// <summary>
    /// A InputParser class which validates inputted elements(Operator, Operand)
    /// and provides separated elements list for the Formatter and the CalculatorImpl.
    /// This class keeps the inputted expression further use for validate purpose.
    /// </summary>
    public sealed class InputParser
    {
        /// <summary>
        /// A InputElement list contains lastly validated expression. </summary>
        private List<InputElement> expressionElements = new List<InputElement>();
        public IEnumerable<InputElement> ExpressionElements
        {
            get
            {
                return expressionElements;
            }
        }

        /// <summary>
        /// A flag represents whether the input elements is empty or not. </summary>
        public bool IsEmptyInputElements
        {
            get
            {
                return (expressionElements.Count == 0);
            }
        }

        /// <summary>
        /// A flag represents whether the last inputted element is the Literal or not. </summary>
        private bool IsLiteralAtLast
        {
            get
            {
                if (expressionElements.Count > 0 &&
                    expressionElements.Last() is Literal)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// A flag represents whether the last inputted element is the Operator or not. </summary>
        private bool IsOperatorAtLast
        {
            get
            {
                if (expressionElements.Count > 0 &&
                    expressionElements.Last() is IOperator)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// A flag indicates last validation status. </summary>
        private bool IsLastValidationSucceed;
        /// <summary>
        /// A flag represents whether equal key is pressed or not last time. </summary>
        private bool IsEqualUsed;

        public InputParser()
        {

        }

        /// <summary>
        /// A method adds inputted element to exists expression and validate the expression with adding a inputted element. </summary>
        /// <param name="input"> A inputted element </param>
        /// <param name="separated"> A infix notation expression InputElement list </param>
        /// <returns> A element adding result status </returns>
        public AddingElementResult GetseparatedPlainText(string input, out IEnumerable<InputElement> separated)
        {
            double inputNumber;
            separated = expressionElements;

            if (input.Length < 1)
            {
                IsLastValidationSucceed = false;
                return new InvalidFormatUsed();
            }

            if (Double.TryParse(input, out inputNumber))
            {
                Literal InputLiteral = new Literal(input);

                if (IsEmptyInputElements)
                {
                    expressionElements.Add(InputLiteral);
                    IsEqualUsed = false;
                    IsLastValidationSucceed = true;
                    return new AddingPossible();
                }
                else
                {
                    AddingElementResult res = expressionElements.Last().CheckPossibilityAddingElement(ref expressionElements,
                        ref IsEqualUsed, ref IsLastValidationSucceed, InputLiteral);
                    IsEqualUsed = false;
                    if (res is AddingPossible)
                    {
                        IsLastValidationSucceed = true;
                    }
                    else
                    {
                        IsLastValidationSucceed = false;
                    }

                    return res;
                }
            }

            InputElement inputElem = Operators.GetOperatorAsInputElement(input);
            IOperator inputOper = inputElem as IOperator;
            if (inputOper == null)
            {
                IsLastValidationSucceed = false;
                return new InvalidFormatUsed();
            }

            if (IsEmptyInputElements)
            {
                if (OperandType.LEFT == inputOper.OperandType ||
                    OperandType.BOTH == inputOper.OperandType)
                {
                    // Exceptional Case : '.' => '0.'
                    if (inputOper is Point)
                    {
                        expressionElements.Add(new Literal("0"));
                    }

                    else
                    {
                        return new InvalidFormatUsed();
                    }
                }

                expressionElements.Add(inputElem);
                Operators.GetOperatorAsInputElement(input)?.PostAddingWork(ref expressionElements);
                IsEqualUsed = false;

                IsLastValidationSucceed = true;
                return new AddingPossible();
            }

            if (inputElem.AlternativeWork(ref expressionElements, ref IsEqualUsed, ref IsLastValidationSucceed))
            {
                IsEqualUsed = false;
                IsLastValidationSucceed = true;
                return new AddingPossible();
            }

            // Exceptional case : Only one '.'
            if (input.CompareTo(".") == 0)
            {
                if (CheckDotExist())
                {
                    separated = expressionElements;
                    IsLastValidationSucceed = true;
                    return new AddingImpossible();
                }
            }
            else if (input.CompareTo("R") == 0)
            {
                if (ReverseSign(out separated))
                {
                    return new AddingPossible();
                }

                return new InvalidFormatUsed();
            }

            expressionElements.Last().CheckPossibilityAddingElement(ref expressionElements, ref IsEqualUsed, ref IsLastValidationSucceed, inputOper as InputElement);
            IsEqualUsed = false;

            IsLastValidationSucceed = true;
            return new AddingPossible();
        }

        /// <summary>
        /// A method provides status indicates that there is '.' exists or not </summary>
        /// <returns> A existence of '.' </returns>
        private bool CheckDotExist()
        {
            for (int i = expressionElements.Count - 1; i >= 0; i--)
            {
                if (expressionElements[i].CompareTo(".") == 0)
                {
                    return true;
                }

                if (expressionElements[i] is IOperator)
                {
                    break;
                }

            }

            return false;
        }

        /// <summary>
        /// A method sets the InputParser as initial status. </summary>
        public void Clear()
        {
            expressionElements.Clear();
            IsLastValidationSucceed = false;
            IsEqualUsed = false;
        }

        /// <summary>
        /// A method removes last inputted element from current expression. </summary>
        /// <param name="separated"> A current expression InputElement list </param>
        /// <returns> A result of deleting last element </returns>
        public bool DeleteLast(out IEnumerable<InputElement> separated)
        {
            separated = new List<InputElement>();

            if (expressionElements.Count == 0)
            {
                return false;
            }

            if (expressionElements.Last() is Literal &&
                expressionElements.Last().GetElement.Length > 1)
            {
                Literal literal = expressionElements.Last() as Literal;

                expressionElements[expressionElements.Count - 1] =
                    new Literal(literal.GetElement.Substring(0, literal.GetElement.Length - 1));
            }

            else
            {
                if (expressionElements.Last() is OpenBracket &&
                    expressionElements.Count > 1 &&
                    expressionElements.ElementAt(expressionElements.Count - 2) is IOperator)
                {
                    expressionElements.RemoveAt(expressionElements.Count - 1);
                }

                expressionElements.RemoveAt(expressionElements.Count - 1);
            }

            separated = expressionElements;
            return true;
        }

        /// <summary>
        /// A method adds a reverse sign or replace last inputted reverse sign. </summary>
        /// <param name="separated"> A current expression InputElement list </param>
        /// <returns> A element adding reverse sign </returns>
        public bool ReverseSign(out IEnumerable<InputElement> separated)
        {
            double lastNumber;

            separated = expressionElements;
            if (IsEmptyInputElements)
            {
                expressionElements.Add(new OpenBracket());
                expressionElements.Add(new Reverse());
                return true;
            }

            if (expressionElements.Last() is Point)
            {
                expressionElements.RemoveAt(expressionElements.Count - 1);
            }

            if (IsEmptyInputElements)
            {
                expressionElements.Add(new OpenBracket());
                expressionElements.Add(new Reverse());
                return true;
            }

            // (3 + R => ((-
            // ((-3 + R => (3
            // 1+-3 + R => 1+3
            // 1.1 + R => (-1.1

            if (Double.TryParse(expressionElements.Last(), out lastNumber))
            {
                if (lastNumber < 0)
                {
                    if (expressionElements.Count >= 2 &&
                        expressionElements[expressionElements.Count - 2] is OpenBracket)
                    {
                        expressionElements.RemoveAt(expressionElements.Count - 1);
                        expressionElements.RemoveAt(expressionElements.Count - 1);
                        expressionElements.Add(new Literal(lastNumber * -1));
                    }
                    else
                    {
                        expressionElements.RemoveAt(expressionElements.Count - 1);
                        expressionElements.Add(new Literal(lastNumber * -1));
                    }
                }
                else
                {
                    if (expressionElements.Count > 2)
                    {
                        if (expressionElements[expressionElements.Count - 2] is IOperator)
                        {
                            if (expressionElements[expressionElements.Count - 2] is Point)
                            {
                                Double postPoint;
                                if (Double.TryParse(expressionElements[expressionElements.Count - 3], out postPoint) == false)
                                {
                                    return false;
                                }

                                expressionElements.RemoveAt(expressionElements.Count - 1);
                                expressionElements.RemoveAt(expressionElements.Count - 1);
                                expressionElements.RemoveAt(expressionElements.Count - 1);

                                while (expressionElements.Count > 0 &&
                                    expressionElements.Last() is IOperator)
                                {
                                    if (expressionElements.Last() is OpenBracket)
                                    {
                                        if (postPoint < 0)
                                        {
                                            expressionElements.RemoveAt(expressionElements.Count - 1);
                                        }

                                        break;
                                    }
                                    else if (expressionElements.Last() is Reverse)
                                    {
                                        expressionElements.RemoveAt(expressionElements.Count - 1);
                                        postPoint = postPoint * -1;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                if ((postPoint * -1) < 0)
                                {
                                    expressionElements.Add(new OpenBracket());
                                }

                                expressionElements.Add(new Literal(postPoint * -1));
                                expressionElements.Add(new Point());
                                expressionElements.Add(new Literal(lastNumber));
                                return true;
                            }
                            else if (expressionElements[expressionElements.Count - 2] is OpenBracket)
                            {
                                expressionElements.RemoveAt(expressionElements.Count - 1);
                                expressionElements.Add(new OpenBracket());
                                expressionElements.Add(new Literal(lastNumber * -1));
                            }
                            else
                            {
                                expressionElements.RemoveAt(expressionElements.Count - 1);
                                Operators.GetOperatorAsInputElement(expressionElements.Last())?.CheckPossibilityAddingElement(
                                    ref expressionElements, ref IsEqualUsed, ref IsLastValidationSucceed, new OpenBracket());
                                expressionElements.Add(new Literal(lastNumber * -1));
                            }

                        }
                        else
                        {
                            return false;
                        }

                    }
                    else
                    {
                        expressionElements.RemoveAt(expressionElements.Count - 1);
                        expressionElements.Add(new OpenBracket());
                        expressionElements.Add(new Literal(lastNumber * -1));
                    }
                }

                return true;
            }

            // ( + R => ((-
            // ((R + R => (

            if (expressionElements.Count > 0 &&
                expressionElements[expressionElements.Count - 1].CompareTo("R") == 0)
            {
                if (expressionElements.Count > 1 &&
                expressionElements[expressionElements.Count - 2].CompareTo("(") == 0)
                {
                    expressionElements.RemoveAt(expressionElements.Count - 1);
                    expressionElements.RemoveAt(expressionElements.Count - 1);
                }
                else
                {
                    expressionElements.RemoveAt(expressionElements.Count - 1);
                }
            }
            else
            {
                if ((expressionElements.Last().CheckPossibilityAddingElement(ref expressionElements,
                        ref IsEqualUsed,
                        ref IsLastValidationSucceed,
                        new OpenBracket())
                    is AddingPossible) == false)
                {
                    return false;
                }

                expressionElements.Add(new Reverse());
            }

            return true;
        }

        /// <summary>
        /// A method manages equal key operation. </summary>
        /// <param name="separated"> A current expression InputElement list </param>
        /// <returns> A status of equal key operation </returns>
        public bool Equal(out IEnumerable<InputElement> separated)
        {
            List<InputElement> tempList = new List<InputElement>(expressionElements);
            separated = tempList;

            if (IsEqualUsed == false)
            {
                IsEqualUsed = true;
                return true;
            }

            if (IsLastValidationSucceed == false)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// A method sets Calculated result as expression.
        /// Usually this is used after equal key pressed situation. </summary>
        /// <param name="result"> A calculated result value </param>
        public void Set(string result)
        {
            expressionElements.Clear();
            expressionElements.Add(new Literal(result));
        }
    }
}
