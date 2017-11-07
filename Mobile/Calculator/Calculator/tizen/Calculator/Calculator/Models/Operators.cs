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
    /// A class including all possible operators for the InputParser and the CalculatorImpl.
    /// Also this class including helper methods for the input elements </summary>
    public static class Operators
    {
        /// <summary>
        /// A dictionary of the all possible operators </summary>
        private static IReadOnlyDictionary<String, IOperator> operators = new Dictionary<string, IOperator>()
        {
            { Plus.Operator, new Plus() },
            { Minus.Operator, new Minus() },
            { Multiplication.Operator, new Multiplication() },
            { Division.Operator, new Division() },
            { OpenBracket.Operator, new OpenBracket() },
            { CloseBracket.Operator, new CloseBracket() },
            { Percentage.Operator, new Percentage() },
            { Point.Operator, new Point() },
            { Reverse.Operator, new Reverse() },
            { Facto.Operator, new Facto() },
            { Sqrt.Operator, new Sqrt() },
            { Sin.Operator, new Sin() },
            { Cos.Operator, new Cos() },
            { Tan.Operator, new Tan() },
            { Log.Operator, new Log() },
            { Ln.Operator, new Ln() },
            { OneOver.Operator, new OneOver() },
            { Exp.Operator, new Exp() },
            { Square.Operator, new Square() },
            { TenPow.Operator, new TenPow() },
            { Pow.Operator, new Pow() },
            { Abs.Operator, new Abs() },
            { Pie.Operator, new Pie() },
            { Euler.Operator, new Euler() },
        };

        /// <summary>
        /// A property returns operator list.
        /// </summary>
        public static IReadOnlyDictionary<String, IOperator> OperatorList
        {
            get
            {
                return operators;
            }
        }

        /// <summary>
        /// A flag indicates the Degree metric is used for calculation.
        /// </summary>
        public static bool IsDegreeMetricUsed
        {
            get;
            set;
        }

        /// <summary>
        /// A method provides matched Operator's instance </summary>
        /// <param name="oper"> A operator's string. </param>
        /// <returns> Matched operator's instance.
        /// But NULL will be returned if there is no matched operator. </returns>
        public static IOperator GetOperator(string oper)
        {
            if (OperatorList.ContainsKey(oper))
            {
                return OperatorList[oper];
            }

            return null;
        }

        /// <summary>
        /// A method provides matched Operator's instance as InputElement type </summary>
        /// <param name="oper"> A operator's string. </param>
        /// <returns> Matched operator's InputElement type instance.
        /// But NULL will be returned if there is no matched operator. </returns>
        public static InputElement GetOperatorAsInputElement(string oper)
        {
            if (OperatorList.ContainsKey(oper))
            {
                return OperatorList[oper] as InputElement;
            }

            return null;
        }

        /// <summary>
        /// A method checks addingElement can be following of left argument receiving operator </summary>
        /// <param name="addingElement"> A adding InputElementthat can be a Literal or a Operator </param>
        /// /// <param name="isMultiplyOperatorRequired"> A flag indicates '*' is required to following addingElement if following is possible </param>
        /// <returns> A possibility of following </returns>
        internal static bool IsPossibleCombineWithOperatorLeft(InputElement addingElement,
            out bool isMultiplyOperatorRequired)
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
        /// A method checks addingElement can be following of both side argument receiving operator </summary>
        /// <param name="addingElement"> A adding InputElementthat can be a Literal or a Operator </param>
        /// <param name="isMultiplyOperatorRequired"> A flag indicates '*' is required to following addingElement if following is possible </param>
        /// <returns> A possibility of following </returns>
        internal static bool IsPossibleCombineWithOperatorRightBoth(InputElement addingElement,
            out bool isMultiplyOperatorRequired)
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
        /// A method checks addingElement can be following of left argument receiving operand type operator </summary>
        /// <param name="addingElement"> A adding InputElementthat can be a Literal or a Operator </param>
        /// <param name="isMultiplyOperatorRequired"> A flag indicates '*' is required to following addingElement if following is possible </param>
        /// <returns> A possibility of following </returns>
        internal static bool IsPossibleCombineWithOperandOperatorLeft(InputElement addingElement,
            out bool isMultiplyOperatorRequired)
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
        /// A method checks addingElement can be following of both side argument receiving operand type operator </summary>
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
        /// A method checks addingElement can be following of no argument receiving operand type operator </summary>
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
        /// A method checks whether addingElement can be following to current expression or
        /// add '*' if the addingElement required to adding</summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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
        /// A methods insert addingElement to current expression(expressionElements)</summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "+";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.BOTH;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = left + addingElement;
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
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "-";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.BOTH;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
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
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "×";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.BOTH;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = left * addingElement;
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
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "÷";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.BOTH;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = 0;
            if (addingElement == 0)
            {
                throw new DivideByZeroException();
            }

            result = left / addingElement;
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
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "(";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.RIGHT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            throw new Exception();
        }

        /// <summary>
        /// Return a number of unclosed parenthesis </summary>
        /// <param name="expressionElements"> A expression. </param>
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
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return ")";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.LEFT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            throw new Exception();
        }
    }

    /// <summary>
    /// A percentage operator </summary>
    public class Percentage : InputElement, IUnaryOperator
    {
        public static string Operator = "%";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "%";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.LEFT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = left / 100D;
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
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return ".";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.BOTH;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            double decimalPlace = addingElement;
            while (decimalPlace >= 1)
            {
                decimalPlace = decimalPlace / 10;
            }

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
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "-";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.RIGHT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = left * -1;
        }
    }


    /// <summary>
    /// A factorial operator </summary>
    public class Facto : InputElement, IUnaryOperator
    {
        public static string Operator = "F";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "!";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.LEFT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            double absValue = Math.Abs(left);
            if ((absValue - Math.Floor(absValue)) != 0.0)
            {
                throw new InvalidNumberException();
            }

            if (absValue > 170D)
            {
                throw new TooBigNumberException();
            }

            result = 1;
            int intValue = (int)absValue;
            for (int i = 1; i <= intValue; i++)
            {
                result = result * i;
            }

            if (left < 0)
            {
                result *= -1;
            }
        }
    }

    /// <summary>
    /// A square root operator </summary>
    public class Sqrt : InputElement, IUnaryOperator
    {
        public static string Operator = "Q";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "√";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.RIGHT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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
        /// A method doing additional work after added as addingElement.</summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        public override void PostAddingWork(ref List<InputElement> expressionElements)
        {
            expressionElements.Add(new OpenBracket());
        }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = Math.Sqrt(left);
        }
    }

    /// <summary>
    /// A sin() operator </summary>
    public class Sin : InputElement, IUnaryOperator
    {
        public static string Operator = "S";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "sin";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.RIGHT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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

        public override void PostAddingWork(ref List<InputElement> expressionElements)
        {
            expressionElements.Add(new OpenBracket());
        }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            if (Operators.IsDegreeMetricUsed)
            {
                result = Math.Sin(left * Math.PI / 180D);
            }
            else
            {
                result = Math.Sin(left);
            }

        }
    }

    /// <summary>
    /// A cos() operator </summary>
    public class Cos : InputElement, IUnaryOperator
    {
        public static string Operator = "C";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "cos";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.RIGHT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
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
        /// A method doing additional work after added as addingElement.</summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        public override void PostAddingWork(ref List<InputElement> expressionElements)
        {
            expressionElements.Add(new OpenBracket());
        }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            if (Operators.IsDegreeMetricUsed)
            {
                result = Math.Cos(left * Math.PI / 180D);
            }
            else
            {
                result = Math.Cos(left);
            }
        }
    }

    /// <summary>
    /// A tan() operator </summary>
    public class Tan : InputElement, IUnaryOperator
    {
        public static string Operator = "T";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "tan";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.RIGHT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements, ref bool isEqualUsed, ref bool isLastValidationSucceed, InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// A method doing additional work after added as addingElement.</summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        public override void PostAddingWork(ref List<InputElement> expressionElements)
        {
            expressionElements.Add(new OpenBracket());
        }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            if (Operators.IsDegreeMetricUsed)
            {
                result = Math.Tan(left * Math.PI / 180D);
            }
            else
            {
                if (Math.Round(left, 10) == Math.Round((Math.PI / 2), 10))
                {
                    result = Double.NaN;
                }
                else
                {
                    result = Math.Tan(left);
                }
            }
        }
    }

    /// <summary>
    /// A log(10) operator </summary>
    public class Log : InputElement, IUnaryOperator
    {
        public static string Operator = "G";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "log";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.RIGHT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements, ref bool isEqualUsed, ref bool isLastValidationSucceed, InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// A method doing additional work after added as addingElement.</summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        public override void PostAddingWork(ref List<InputElement> expressionElements)
        {
            expressionElements.Add(new OpenBracket());
        }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = Math.Log10(left);
        }
    }

    /// <summary>
    /// A log operator </summary>
    public class Ln : InputElement, IUnaryOperator
    {
        public static string Operator = "N";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "ln";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.RIGHT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements, ref bool isEqualUsed, ref bool isLastValidationSucceed, InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// A method doing additional work after added as addingElement.</summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        public override void PostAddingWork(ref List<InputElement> expressionElements)
        {
            expressionElements.Add(new OpenBracket());
        }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = Math.Log(left);
        }
    }

    /// <summary>
    /// A 1 / x operator </summary>
    public class OneOver : InputElement, IUnaryOperator
    {
        public static string Operator = "O";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "1÷";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.RIGHT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements, ref bool isEqualUsed, ref bool isLastValidationSucceed, InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = 1 / left;
        }
    }

    /// <summary>
    /// A exp operator </summary>
    public class Exp : InputElement, IUnaryOperator
    {
        public static string Operator = "X";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "e";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.RIGHT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements, ref bool isEqualUsed, ref bool isLastValidationSucceed, InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// A method doing additional work after added as addingElement.</summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        public override void PostAddingWork(ref List<InputElement> expressionElements)
        {
            expressionElements.Add(new OpenBracket());
        }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = Math.Exp(left);
        }
    }

    /// <summary>
    /// A square power operator </summary>
    public class Square : InputElement, IUnaryOperator
    {
        public static string Operator = "U";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "^(2)";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.LEFT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements, ref bool isEqualUsed, ref bool isLastValidationSucceed, InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = Math.Pow(left, 2);
        }
    }

    /// <summary>
    /// A power operator </summary>
    public class TenPow : InputElement, IUnaryOperator
    {
        public static string Operator = "P";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "10^";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.RIGHT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements, ref bool isEqualUsed, ref bool isLastValidationSucceed, InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// A method doing additional work after added as addingElement.</summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        public override void PostAddingWork(ref List<InputElement> expressionElements)
        {
            expressionElements.Add(new OpenBracket());
        }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = Math.Pow(10, left);
        }
    }

    /// <summary>
    /// A power operator </summary>
    public class Pow : InputElement, IBinaryOperator
    {
        public static string Operator = "W";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "^";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.BOTH;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements, ref bool isEqualUsed, ref bool isLastValidationSucceed, InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// A method doing additional work after added as addingElement.</summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        public override void PostAddingWork(ref List<InputElement> expressionElements)
        {
            expressionElements.Add(new OpenBracket());
        }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = Math.Pow(left, addingElement);
        }
    }

    /// <summary>
    /// A abs() operator </summary>
    public class Abs : InputElement, IUnaryOperator
    {
        public static string Operator = "A";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "abs";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.RIGHT;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements, ref bool isEqualUsed, ref bool isLastValidationSucceed, InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// A method doing additional work after added as addingElement.</summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        public override void PostAddingWork(ref List<InputElement> expressionElements)
        {
            expressionElements.Add(new OpenBracket());
        }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = Math.Abs(left);
        }
    }

    /// <summary>
    /// A Pie InputElement</summary>
    public class Pie : InputElement, INullary
    {
        private static double Value = 3.14159265358979;
        public static string Operator = "I";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }

        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "Π";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.NONE;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements, ref bool isEqualUsed, ref bool isLastValidationSucceed, InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = Value;
        }
    }

    /// <summary>
    /// A Euler's number InputElement</summary>
    public class Euler : InputElement, INullary
    {
        private static double Value = 2.71828182845905;
        public static string Operator = "E";
        /// <summary>
        /// A property provides element string.
        /// This element string is using for validation and calculation. </summary>
        public override string GetElement
        {
            get
            {
                return Operator;
            }
        }
        /// <summary>
        /// A property provides displaying element string.
        /// This displaying element is using for making formatted string. </summary>
        public override string GetDisplayElement
        {
            get
            {
                return "e";
            }
        }

        /// <summary>
        /// A operator priority </summary>
        public int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        public OperandType OperandType
        {
            get
            {
                return OperandType.NONE;
            }
        }

        /// <summary>
        /// A method provides possibility of that is it possible adding addingElement after this InputElement.
        /// This method can modify current expression by adding additional element or removing last element. </summary>
        /// <param name="expressionElements"> Current expression, a InputElement list </param>
        /// <param name="isEqualUsed"> A flag value whether equal key is pressed or not </param>
        /// <param name="isLastValidationSucceed"> A flag value whether last calculation is succeed or not </param>
        /// <param name="addingElement"> A InputElement will be adding next of this element. </param>
        /// <returns> A status of adding element to exist expression </returns>
        public override AddingElementResult CheckPossibilityAddingElement(ref List<InputElement> expressionElements, ref bool isEqualUsed, ref bool isLastValidationSucceed, InputElement addingElement)
        {
            if (Operators.InsertingWork(ref expressionElements, ref isEqualUsed, ref isLastValidationSucceed, addingElement))
            {
                return new AddingPossible();
            }

            return new InvalidFormatUsed();
        }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="addingElement"> A addingElement side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        public void GetResult(double left, double addingElement, out double result)
        {
            result = Value;
        }
    }
}

