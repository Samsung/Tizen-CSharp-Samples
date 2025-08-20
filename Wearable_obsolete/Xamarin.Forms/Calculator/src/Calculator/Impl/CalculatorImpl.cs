
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

using Calculator.Models;
using Tizen;

namespace Calculator.Impl
{
    /// <summary>
    /// A Calculator Implementation class that calculates result for given expression.
    /// </summary>
    /// <remarks>
    /// The CalculatorImpl requires InputElement class based infix notation expression list.
    /// </remarks>
    public sealed class CalculatorImpl
    {
        /// <summary>
        /// An enum value representing Calculator working status.
        /// </summary>
        public enum WorkingStatus
        {
            WORKING,
            ERROR,
        }

        /// <summary>
        /// Calculator working status.
        /// Status is set to ERROR if last calculation failed.
        /// </summary>
        public WorkingStatus Status
        {
            get;
            private set;
        }

        /// <summary>
        /// All operators that can be used in calculation.
        /// </summary>
        private IReadOnlyDictionary<String, IOperator> OperatorDescriptions;

        /// <summary>
        /// Last used operand.
        /// This operand is used in repeated calculation.
        /// </summary>
        private Literal LastOperand;

        /// <summary>
        /// Last used operator.
        /// This operator is used in repeated calculation.
        /// </summary>
        private IOperator LastOperator;

        /// <summary>
        /// A flag value shows if the last pressed button is equal key.
        /// </summary>
        private bool IsEqualUsed;

        /// <summary>
        /// Last calculated result value.
        /// </summary>
        public double Result
        {
            get;
            private set;
        }

        /// <summary>
        /// The CalculatorImpl's constructor which initializes calculator
        /// and sets default calculation operators.
        /// </summary>
        public CalculatorImpl()
        {
            Status = WorkingStatus.WORKING;
            OperatorDescriptions = Operators.OperatorMap;
        }

        /// <summary>
        /// The method which calculates given expression.
        /// </summary>
        /// <param name="separatedInfixNotation"> An expression list in infix notation </param>
        /// <returns> Calculation result status </returns>
        public CalculationResult SetExpression(IEnumerable<InputElement> separatedInfixNotation)
        {
            IsEqualUsed = false;
            return Calculate(separatedInfixNotation);
        }

        /// <summary>
        /// This method is handling equal key press.
        /// If the equal key is pressed once, just provides a result,
        /// but if the equal key is pressed repeatedly, calculator calculates with lastly used operand and operator
        /// </summary>
        /// <param name="separated"> Expression list in infix notation </param>
        /// <returns> Calculation result status </returns>
        public CalculationResult Equal(IEnumerable<InputElement> separated)
        {
            CalculationResult res;

            if (IsEqualUsed == false)
            {
                res = Calculate(separated);
                if (res is CalculationSuccessful)
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
                return new CalculationFailed();
            }

            List<InputElement> tempList = new List<InputElement>(separated);
            tempList.Add(LastOperator as InputElement);
            tempList.Add(LastOperand);

            return Calculate(tempList);
        }

        /// <summary>
        /// The method calculates result for given expression.
        /// </summary>
        /// <param name="separated"> InputElement list in infix notation</param>
        /// <returns> Calculation result status </returns>
        private CalculationResult Calculate(IEnumerable<InputElement> separated)
        {
            Status = WorkingStatus.ERROR;

            if (!separated.Any())
            {
                Result = 0;
                Status = WorkingStatus.WORKING;
                return new CalculationSuccessful();
            }

            if (separated.Last() is Point)
            {
                return new CalculationFailed();
            }

            IList<InputElement> temp = new List<InputElement>(separated);
            // This section adds missing closing brackets
            {
                int depth = 0;
                foreach (var item in temp)
                {
                    if (item.CompareTo("(") == 0)
                    {
                        depth += 1;
                    }
                    else if (item.CompareTo(")") == 0)
                    {
                        depth -= 1;
                    }
                }

                for (int i = 0; i < depth; i++)
                {
                    temp.Add(new CloseBracket());
                }
            }


            Queue<InputElement> postfix;
            if (!ChangeToPostfix(temp, out postfix))
            {
                return new CantCalculateInvalidFormat();
            }
            
            Stack<string> operand = new Stack<string>();
            try
            {
                foreach(var token in postfix)
                {
                    if (token is Literal)
                    {
                        operand.Push(token.GetElement);
                    }
                    else
                    {
                        IOperator op = OperatorDescriptions[token];
                        string op1;
                        string op2;
                        double _op1 = 0;
                        double _op2 = 0;

                        // This section retrieves arguments needed by operator
                        if (op is IBinaryOperator)
                        {
                            op2 = operand.Pop();
                            op1 = operand.Pop();

                            if (!GetDoubleValue(op2, out _op2) || !GetDoubleValue(op1, out _op1))
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

                            if (!GetDoubleValue(op1, out _op1))
                            {
                                return new CantCalculateInvalidFormat();
                            }
                        }

                        op.GetResult(_op1, _op2, out double result);

                        if (Double.IsInfinity(result) || Double.IsNaN(result))
                        {
                            return new CantCalculateTooBigNumber();
                        }

                        operand.Push(result.ToString());
                    }
                }
            }
            catch (DivideByZeroException)
            {
                return new CantDivideByZero();
            }
            catch (InvalidOperationException)
            {
                return new CantCalculateInvalidFormat();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return new CalculationFailed();
            }

            double ret;
            if (operand.Count == 0 || Double.TryParse(operand.Peek(), out ret) == false)
            {
                return new CalculationFailed();
            }

            Result = ret;
            Status = WorkingStatus.WORKING;
            return new CalculationSuccessful();
        }

        /// <summary>
        /// The method changes expression format from infix to postfix.
        /// </summary>
        /// <param name="separated"> InputElement list in infix notation </param>
        /// <param name="ret"> InputElement queue in postfix notation </param>
        /// <returns> Result status of expression's reorder </returns>
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
                            (OperatorDescriptions[operators.Peek()].Priority >= OperatorDescriptions[token].Priority))
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
            catch (Exception)
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

            if (literal.StartsWith("0") && literal.Contains(".") == false)
            {
                strValue = ("0." + literal);
            }

            return Double.TryParse(strValue, out value);
        }
    }
}
