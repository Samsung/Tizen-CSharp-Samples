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
    /// A Calculator Implementation class that provides a calculate result for given expression.
    /// </summary>
    /// <remarks>
    /// The CalculatorImpl requires InputElement class based infix notation expression list.
    /// </remarks>
    public sealed class CalculatorImpl
    {
        /// <summary>
        /// A enum values represents Calculator working status.</summary>
        public enum WorkingStatus
        {
            WORKING,
            ERROR,
        }

        /// <summary>
        /// Calculator working status.
        /// If the status is ERROR, last calculating was failed. </summary>
        public WorkingStatus Status
        {
            get;
            private set;
        }

        /// <summary>
        /// All operators that can be used in calculation. </summary>
        private IReadOnlyDictionary<String, IOperator> OperatorsPriority;

        /// <summary>
        /// Last used operand and operator.
        /// These member variables are used when repeat calculation is required. </summary>
        private Literal LastOperand;
        private IOperator LastOperator;

        /// <summary>
        /// A flag value shows if equal key is pressed or not. </summary>
        private bool IsEqualUsed;

        /// <summary>
        /// Last calculated result value.</summary>
        public double Result
        {
            get;
            private set;
        }

        /// <summary>
        /// The CalculatorImpl's constructor which initializes calculator
        /// and set default calculate operators. </summary>
        public CalculatorImpl()
        {
            Status = WorkingStatus.WORKING;
            OperatorsPriority = Operators.OperatorList;
        }

        /// <summary>
        /// A method which calculates a given expression.</summary>
        /// <param name="infixNotation"> A infix notation expression text </param>
        /// <returns> A calculation result status </returns>
        public CalculateResult SetExpression(string infixNotation)
        {
            char[] delimiterChars = { ' ', '\t' };
            List<InputElement> separated = new List<InputElement>();
            foreach (string chunk in infixNotation.Split(delimiterChars))
            {
                double value;
                if (Double.TryParse(chunk, out value))
                {
                    separated.Add(new Literal(chunk));
                }
                else
                {
                    IOperator oper = Operators.GetOperator(chunk);
                    if (oper == null)
                    {
                        return new CantCalculateInvalidFormat();
                    }

                    separated.Add(oper as InputElement);
                }
            }

            return SetExpression(separated);
        }

        /// <summary>
        /// A method which calculates a given expression.</summary>
        /// <param name="separatedInfixNotation"> A infix notation expression list </param>
        /// <returns> A calculation result status </returns>
        public CalculateResult SetExpression(IEnumerable<InputElement> separatedInfixNotation)
        {
            IsEqualUsed = false;
            return Calculate(separatedInfixNotation);
        }

        /// <summary>
        /// A method which handling equal key pressed situation.
        /// If the equal key is pressed firstly, just provides a result,
        /// but if the equal key is pressed repeatedly, calculator calculates with lastly used operand and operator</summary>
        /// <param name="separated"> A infix notation expression list </param>
        /// <returns> A calculation result status </returns>
        public CalculateResult Equal(IEnumerable<InputElement> separated)
        {
            CalculateResult res;

            if (IsEqualUsed == false)
            {
                res = Calculate(separated);
                if (res is CalculateSuccess)
                {
                    IsEqualUsed = true;
                    return res;
                }

                LastOperand = null;
                LastOperator = null;
                return res;
            }

            if (separated.Count() < 1 ||
                LastOperator == null ||
                LastOperand == null)
            {
                return new CalculateFailed();
            }

            List<InputElement> tempList = new List<InputElement>(separated);
            tempList.Add(LastOperator as InputElement);
            tempList.Add(LastOperand);

            return Calculate(tempList);
        }

        /// <summary>
        /// A method which calculates a given expression.</summary>
        /// <param name="separated"> A infix notation expression InputElement list</param>
        /// <returns> A calculation result status </returns>
        private CalculateResult Calculate(IEnumerable<InputElement> separated)
        {
            Status = WorkingStatus.ERROR;

            if (separated.Count() < 1)
            {
                Result = 0;
                Status = WorkingStatus.WORKING;
                return new CalculateSuccess();
            }

            if (separated.Last() is Point)
            {
                return new CalculateFailed();
            }

            IList<InputElement> temp = new List<InputElement>(separated);
            {
                int res = 0;
                foreach (var item in temp)
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

                for (int i = 0; i < res; i++)
                {
                    temp.Add(new CloseBracket());
                }
            }

            Queue<InputElement> postfix = new Queue<InputElement>();
            if (ChangeToPostfix(temp, out postfix) == false)
            {
                return new CantCalculateInvalidFormat();
            }

            Stack<string> operand = new Stack<string>();

            try
            {
                while (postfix.Count > 0)
                {
                    InputElement token = postfix.Dequeue();

                    if (token is Literal)
                    {
                        operand.Push(token.GetElement);
                    }
                    else
                    {
                        IOperator op = OperatorsPriority[token];
                        string op1;
                        string op2;
                        double _op1 = 0;
                        double _op2 = 0;

                        if (op is IBinaryOperator)
                        {
                            op2 = operand.Pop();
                            op1 = operand.Pop();

                            if (GetDoubleValue(op2, out _op2) == false ||
                                GetDoubleValue(op1, out _op1) == false)
                            {
                                return new CantCalculateInvalidFormat();
                            }

                            if ((op is Models.Point) == false)
                            {
                                LastOperand = new Literal(op2);
                                LastOperator = op;
                            }
                        }
                        else if (op is IUnaryOperator)
                        {
                            op1 = operand.Pop();

                            if (GetDoubleValue(op1, out _op1) == false)
                            {
                                return new CantCalculateInvalidFormat();
                            }
                        }

                        double result = 0;
                        op.GetResult(_op1, _op2, out result);
                        if (Double.IsInfinity(result) ||
                            Double.IsNaN(result))
                        {
                            return new CantCalculateTooBigNumber();
                        }

                        // Exceptional Case : %
                        if (op.ToString().CompareTo("%") == 0)
                        {
                            if (operand.Count > 0)
                            {
                                if (postfix.Count == 0 ||
                                (postfix.Count > 0 &&
                                postfix.Peek().CompareTo("/") != 0 &&
                                postfix.Peek().CompareTo("*") != 0))
                                {
                                    double prevLiteral = Double.Parse(operand.Peek());
                                    result = prevLiteral * result;
                                }
                            }
                        }

                        operand.Push(result.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                if (e is InvalidOperationException ||
                    e is InvalidNumberException)
                {
                    return new CantCalculateInvalidFormat();
                }
                else if (e is DivideByZeroException)
                {
                    return new CantDivideByZero();
                }
                else if (e is TooBigNumberException)
                {
                    return new CantCalculateTooBigNumber();
                }

                return new CalculateFailed();
            }

            double ret;
            if (operand.Count == 0 ||
                Double.TryParse(operand.Peek(), out ret) == false)
            {
                return new CalculateFailed();
            }

            Result = ret;
            Status = WorkingStatus.WORKING;
            return new CalculateSuccess();
        }

        /// <summary>
        /// A method changes the infix expression to the postfix expression. </summary>
        /// <param name="separated"> A infix notation expression InputElement list </param>
        /// <param name="ret"> A postfix notation InputElement queue </param>
        /// <returns> A result status of expression's reorder </returns>
        private bool ChangeToPostfix(IEnumerable<InputElement> separated, out Queue<InputElement> ret)
        {
            Queue<InputElement> postfix = new Queue<InputElement>();
            Stack<InputElement> operators = new Stack<InputElement>();

            try
            {
                foreach (InputElement token in separated)
                {
                    if (token is Literal)
                    {
                        postfix.Enqueue(token);
                    }
                    else if (token.CompareTo("(") == 0)
                    {
                        operators.Push(token);
                    }
                    else if (token.CompareTo(")") == 0)
                    {
                        InputElement topToken = operators.Pop();
                        while (topToken.CompareTo("(") != 0)
                        {
                            postfix.Enqueue(topToken);
                            topToken = operators.Pop();
                        }
                    }
                    else
                    {
                        while ((operators.Count > 0) &&
                            (OperatorsPriority[operators.Peek()].Priority >= OperatorsPriority[token].Priority))
                        {
                            postfix.Enqueue(operators.Pop());
                        }

                        operators.Push(token);
                    }
                }

                while (operators.Count > 0)
                {
                    postfix.Enqueue(operators.Pop());
                }
            }
#pragma warning disable CS0168
            catch (Exception e)
#pragma warning restore CS0168
            {
                postfix.Clear();
                ret = postfix;
                return false;
            }

            ret = postfix;
            return true;
        }

        private bool GetDoubleValue(string literal, out double value)
        {
            string strValue = literal;

            if (literal.StartsWith("0") &&
                    literal.Contains(".") == false)
            {
                strValue = ("0." + literal);
            }

            return Double.TryParse(strValue, out value);
        }
    }
}
