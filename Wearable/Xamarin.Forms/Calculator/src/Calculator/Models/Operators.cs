
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
using System.Linq;
using Tizen;

namespace Calculator.Models
{
    /// <summary>
    /// This class includes all possible operators for the InputParser and the CalculatorImpl.
    /// Also this class includes helper methods for the input elements </summary>
    public static class Operators
    {
        /// <summary>
        /// Dictionary of the all possible operators 
        /// </summary>
        private static IReadOnlyDictionary<String, IOperator> operators = new Dictionary<string, IOperator>()
        {
            { Plus.Operator, new Plus() },
            { Minus.Operator, new Minus() },
            { Multiplication.Operator, new Multiplication() },
            { Division.Operator, new Division() },
            { OpenBracket.Operator, new OpenBracket() },
            { CloseBracket.Operator, new CloseBracket() },
            { Point.Operator, new Point() },
            { Reverse.Operator, new Reverse() },
        };

        /// <summary>
        /// The property returns operator dictionary.
        /// </summary>
        public static IReadOnlyDictionary<String, IOperator> OperatorMap
        {
            get => operators;
        }

        /// <summary>
        /// The method provides matched Operator's instance </summary>
        /// <param name="oper"> A operator's string. </param>
        /// <returns> Matched operator's instance.
        /// But NULL will be returned if there is no matched operator. </returns>
        public static IOperator GetOperator(string oper)
        {
            if (OperatorMap.ContainsKey(oper))
            {
                return OperatorMap[oper];
            }

            return null;
        }

        /// <summary>
        /// The method provides matched Operator's instance as InputElement type </summary>
        /// <param name="oper"> A operator's string. </param>
        /// <returns> Matched operator's InputElement type instance.
        /// But NULL will be returned if there is no matched operator. </returns>
        public static InputElement GetOperatorAsInputElement(string oper)
        {
            if (OperatorMap.ContainsKey(oper))
            {
                return OperatorMap[oper] as InputElement;
            }

            return null;
        }

        /// <summary>
        /// The method checks addingElement can be following of left argument receiving operator </summary>
        /// <param name="addingElement"> A adding InputElementthat can be a Literal or a Operator </param>
        /// /// <param name="isMultiplyOperatorRequired"> A flag indicates '*' is required to following addingElement if following is possible </param>
        /// <returns> A possibility of following </returns>
        internal static bool IsPossibleCombineWithOperatorLeft(InputElement addingElement, out bool isMultiplyOperatorRequired)
        {
            IOperator addingElementOper = null;

            isMultiplyOperatorRequired = false;
            if ((addingElementOper = addingElement as IOperator) == null ||
                addingElement is INullary)
            {
                isMultiplyOperatorRequired = true;
                return true;
            }

            if (OperandType.LEFT == addingElementOper.OperandType || OperandType.BOTH == addingElementOper.OperandType)
            {
                return false;
            }

            else if (OperandType.RIGHT == addingElementOper.OperandType)
            {
                isMultiplyOperatorRequired = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// The method checks addingElement can be following of both side argument receiving operator </summary>
        /// <param name="addingElement"> A adding InputElementthat can be a Literal or a Operator </param>
        /// <param name="isMultiplyOperatorRequired"> A flag indicates '*' is required to following addingElement if following is possible </param>
        /// <returns> A possibility of following </returns>
        internal static bool IsPossibleCombineWithOperatorRightBoth(InputElement addingElement, out bool isMultiplyOperatorRequired)
        {
            IOperator addingElementOper = null;

            isMultiplyOperatorRequired = false;

            if ((addingElementOper = addingElement as IOperator) == null ||
                addingElement is INullary)
            {
                return true;
            }

            if (addingElementOper is IUnaryOperator &&
                OperandType.RIGHT == addingElementOper.OperandType)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The method checks addingElement can be following of left argument receiving operand type operator </summary>
        /// <param name="addingElement"> A adding InputElementthat can be a Literal or a Operator </param>
        /// <param name="isMultiplyOperatorRequired"> A flag indicates '*' is required to following addingElement if following is possible </param>
        /// <returns> A possibility of following </returns>
        internal static bool IsPossibleCombineWithOperandOperatorLeft(InputElement addingElement, out bool isMultiplyOperatorRequired)
        {
            IOperator addingElementOper = null;

            isMultiplyOperatorRequired = false;

            if ((addingElementOper = addingElement as IOperator) == null ||
                addingElement is INullary)
            {
                isMultiplyOperatorRequired = true;
                return true;
            }

            if (OperandType.LEFT == addingElementOper.OperandType || OperandType.BOTH == addingElementOper.OperandType)
            {
                return true;   // ex ))
            }

            else if (OperandType.RIGHT == addingElementOper.OperandType)
            {
                isMultiplyOperatorRequired = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// The method checks addingElement can be following of both side argument receiving operand type operator </summary>
        /// <param name="addingElement"> A adding InputElementthat can be a Literal or a Operator </param>
        /// <param name="isMultiplyOperatorRequired"> A flag indicates '*' is required to following addingElement if following is possible </param>
        /// <returns> A possibility of following </returns>
        internal static bool IsPossibleCombineWithOperandOperatorRightBoth(InputElement addingElement, out bool isMultiplyOperatorRequired)
        {
            IOperator addingElementOper = null;

            isMultiplyOperatorRequired = false;

            if ((addingElementOper = addingElement as IOperator) == null ||
                addingElement is INullary)
            {
                return true;
            }

            if (addingElementOper is IUnaryOperator &&
                OperandType.RIGHT == addingElementOper.OperandType)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The method checks addingElement can be following of no argument receiving operand type operator </summary>
        /// <param name="addingElement"> A adding InputElementthat can be a Literal or a Operator </param>
        /// <param name="isMultiplyOperatorRequired"> A flag indicates '*' is required to following addingElement if following is possible </param>
        /// <returns> A possibility of following </returns>
        internal static bool IsPossibleCombineOperandOperatorWithNone(InputElement addingElement, out bool isMultiplyOperatorRequired)
        {
            IOperator addingElementOper = null;

            isMultiplyOperatorRequired = false;
            if ((addingElementOper = addingElement as IOperator) == null ||
                addingElement is INullary ||
                OperandType.RIGHT == addingElementOper.OperandType)
            {
                isMultiplyOperatorRequired = true;
                return true;
            }

            return true;
        }

        /// <summary>
        /// The method checks whether addingElement can be following to current expression or
        /// add '*' if the addingElement is required</summary>
        /// <param name="expressionElements"> Current expression, an InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be added after this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        private static bool PreInsertingWork(ref List<InputElement> expressionElements,
                                             ref bool isEqualUsed, 
                                             ref bool isLastValidationSucceed,
                                             InputElement addingElement)
        {
            if (expressionElements.Count == 0)
            {
                throw new Exception("This function should not be called if there is no inputed element");
            }

            if (expressionElements.Last() is Literal &&
                addingElement is IOperator)
            {
                IOperator inputOper = Operators.GetOperator(addingElement);
                if (inputOper is INullary ||
                    OperandType.RIGHT == inputOper?.OperandType)
                {
                    expressionElements.Add(new Multiplication());
                }

                return true;
            }

            IOperator lastOper = expressionElements.Last() as IOperator;
            bool isMultiplyRequird = false;
            bool res = false;

            if (lastOper is Point &&
                addingElement is IOperator)
            {
                IOperator inputOper = Operators.GetOperator(addingElement);
                expressionElements.RemoveAt(expressionElements.Count - 1);
                if (OperandType.RIGHT == inputOper.OperandType ||
                    OperandType.NONE == inputOper.OperandType)
                {
                    expressionElements.Add(new Multiplication());
                }

                return true;
            }

            if (OperandType.RIGHT == lastOper.OperandType ||
            OperandType.BOTH == lastOper.OperandType)
            {
                if (lastOper is IUnaryOperator)
                {
                    res = Operators.IsPossibleCombineWithOperandOperatorRightBoth(addingElement, out isMultiplyRequird);
                }

                else
                {
                    res = Operators.IsPossibleCombineWithOperatorRightBoth(addingElement, out isMultiplyRequird);
                }
            }
            else if (OperandType.LEFT == lastOper.OperandType)
            {
                if (lastOper is IUnaryOperator)
                {
                    res = Operators.IsPossibleCombineWithOperandOperatorLeft(addingElement, out isMultiplyRequird);
                }

                else
                {
                    res = Operators.IsPossibleCombineWithOperatorLeft(addingElement, out isMultiplyRequird);
                }
            }
            else if (OperandType.NONE == lastOper.OperandType)
            {
                res = Operators.IsPossibleCombineOperandOperatorWithNone(addingElement, out isMultiplyRequird);
            }

            if (isMultiplyRequird)
            {
                expressionElements.Add(new Multiplication());
            }

            return res;
        }

        /// <summary>
        /// The methods insert addingElement to current expression(expressionElements)</summary>
        /// <param name="expressionElements"> Current expression, an InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be added after this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        /// <seealso cref="PreInsertingWork(ref List{InputElement}, ref bool, ref bool, InputElement)"/>
        internal static bool InsertingWork(ref List<InputElement> expressionElements, 
                                           ref bool isEqualUsed,
                                           ref bool isLastValidationSucceed,
                                           InputElement addingElement)
        {
            IOperator inputOper = Operators.GetOperator(addingElement);


            if (PreInsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement) == false)
            {
                if (addingElement is IOperator &&
                    inputOper == null)
                {
                    return isLastValidationSucceed = false;
                }

                IOperator lastOper = expressionElements.Last() as IOperator;
                if (lastOper == null)
                {
                    return isLastValidationSucceed = false;
                }

                if (OperandType.BOTH == lastOper.OperandType &&
                           OperandType.BOTH == inputOper?.OperandType)
                {
                    expressionElements.RemoveAt(expressionElements.Count - 1);
                    expressionElements.Add(addingElement);
                    return true;
                }

                return isLastValidationSucceed = false;
            }

            expressionElements.Add(addingElement);

            addingElement.PostAddingWork(ref expressionElements);
            return true;
        }
    }

    /// <summary>
    /// A plus operator. </summary>
    public class Plus : InputElement, IBinaryOperator
    {
        public static string Operator = "+";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get => Operator;
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get => "+";
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get => 2;
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get => OperandType.BOTH;
        }

        /// <summary>
        /// The method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, an InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be added after this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements,
            ref bool isEqualUsed,
            ref bool isLastValidationSucceed,
            InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// The method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="right"> A rightside element. </param>
        /// <param name="result"> Calculation result. </param>
        public void GetResult(double left, double right, out double result)
        {
            result = left + right;
        }
    }

    /// <summary>
    /// A minus operator </summary>
    public class Minus : InputElement, IBinaryOperator
    {
        public static string Operator = "-";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get => Operator;
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get => "-";
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get => 2;
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get => OperandType.BOTH;
        }

        /// <summary>
        /// The method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, an InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be added after this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements,
            ref bool isEqualUsed,
            ref bool isLastValidationSucceed,
            InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// The method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="right"> A rightside element. </param>
        /// <param name="result"> Calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = left - addingElement;
        }
    }

    /// <summary>
    /// A multiplication operator </summary>
    public class Multiplication : InputElement, IBinaryOperator
    {
        public static string Operator = "*";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get => Operator;
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get =>"×";
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get => 3;
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get => OperandType.BOTH;
        }

        /// <summary>
        /// The method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, an InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be added after this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements,
            ref bool isEqualUsed,
            ref bool isLastValidationSucceed,
            InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// The method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="right"> A rightside element. </param>
        /// <param name="result"> Calculation result. </param>
        public void GetResult(double left, double right, out double result)
        {
            result = left * right;
        }
    }

    /// <summary>
    /// A division operator </summary>
    public class Division : InputElement, IBinaryOperator
    {
        public static string Operator = "/";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get => Operator;
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get => "÷";
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get => 3;
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get => OperandType.BOTH;
        }

        /// <summary>
        /// The method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, an InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be added after this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements,
            ref bool isEqualUsed,
            ref bool isLastValidationSucceed,
            InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// The method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="right"> A rightside element. </param>
        /// <param name="result"> Calculation result. </param>
        public void GetResult(double left, double right, out double result)
        {
            result = 0;
            if (right == 0)
            {
                throw new DivideByZeroException();
            }

            result = left / right;
        }
    }

    /// <summary>
    /// A opening parenthesis operator </summary>
    public class OpenBracket : InputElement, IUnaryOperator
    {
        public static string Operator = "(";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get => Operator;
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get => "(";
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get => 1;
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get => OperandType.RIGHT;
        }

        /// <summary>
        /// The method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, an InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be added after this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements,
            ref bool isEqualUsed,
            ref bool isLastValidationSucceed,
            InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        public override bool AlternativeWork(ref List<InputElement> expressionElements,
            ref bool isEqualUsed,
            ref bool isLastValidationSucceed)
        {
            if (GetNumberOfOpenedBracket(expressionElements) > 0)
            {
                return Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, new CloseBracket());
            }

            return Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, new OpenBracket());
        }

        /// <summary>
        /// The method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="right"> A right side element. </param>
        /// <param name="result"> Calculation result. </param>
        public void GetResult(double left, double right, out double result)
        {
            throw new Exception();
        }

        /// <summary>
        /// Return a number of unclosed parenthesis </summary>
        /// <param name="expressionElements"> An expression. </param>
        /// <returns> A number of unclosed parenthesizes </returns>
        private int GetNumberOfOpenedBracket(List<InputElement> expressionElements)
        {
            int res = 0;
            foreach (var item in expressionElements)
            {
                if (item.CompareTo("(") == 0)
                {
                    res += 1;
                }

                else if (item.CompareTo(")") == 0)
                {
                    res -= 1;
                }
            }

            return res;
        }
    }


    /// <summary>
    /// A closing parenthesis operator </summary>
    public class CloseBracket : InputElement, IUnaryOperator
    {
        public static string Operator = ")";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get => Operator;
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get => ")";
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get => 1;
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get => OperandType.LEFT;
        }

        /// <summary>
        /// The method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, an InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be added after this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements,
            ref bool isEqualUsed,
            ref bool isLastValidationSucceed,
            InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// The method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="right"> A right side element. </param>
        /// <param name="result"> Calculation result. </param>
        public void GetResult(double left, double right, out double result)
        {
            throw new Exception();
        }
    }

    /// <summary>
    /// A point adding operator </summary>
    public class Point : InputElement, IBinaryOperator
    {
        public static string Operator = ".";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get => Operator;
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get => ".";
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get => 4;
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get => OperandType.BOTH;
        }

        /// <summary>
        /// The method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, an InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be added after this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements,
            ref bool isEqualUsed,
            ref bool isLastValidationSucceed,
            InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// The method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="right"> A rightside element. </param>
        /// <param name="result"> Calculation result. </param>
        public void GetResult(double left, double right, out double result)
        {
            double decimalPlace = right;
            while (decimalPlace >= 1)
            {
                decimalPlace = decimalPlace / 10;
            }

            if (left < 0)
                result = left - decimalPlace;
            else
                result = left + decimalPlace;
        }
    }

    /// <summary>
    /// A reverse sign operator </summary>
    public class Reverse : InputElement, IUnaryOperator
    {
        public static string Operator = "R";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get => Operator;
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get => "-";
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get => 4;
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get => OperandType.RIGHT;
        }

        /// <summary>
        /// The method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, an InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be added after this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements,
            ref bool isEqualUsed,
            ref bool isLastValidationSucceed,
            InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();

        }

        /// <summary>
        /// The method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="right"> A rightside element. </param>
        /// <param name="result"> Calculation result. </param>
        public void GetResult(double left, double right, out double result)
        {
            result = left * -1;
        }
    }

    
}

