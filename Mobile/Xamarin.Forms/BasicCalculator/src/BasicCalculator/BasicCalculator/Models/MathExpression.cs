/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace BasicCalculator.Models
{
    /// <summary>
    /// Exception thrown when expression result length is exceeded.
    /// </summary>
    internal class MathExpressionResultLengthExceededException : Exception
    {

    }

    /// <summary>
    /// Exception thrown when expression evaluation fails.
    /// </summary>
    internal class MathExpressionEvaluationException : Exception
    {

    }

    internal class MathExpression
    {
        #region fields

        /// <summary>
        /// String holding whole current expression, which is to be calculated.
        /// </summary>
        private StringBuilder _expression;

        #endregion fields

        #region methods

        /// <summary>
        /// Constructor setting initial expression, which shall be calculated with the
        /// <see cref="Evaluate(int)"/> method.
        /// </summary>
        /// <param name="expression">Expression to be calculated.</param>
        public MathExpression(string expression)
        {
            _expression = new StringBuilder(expression);
        }

        /// <summary>
        /// Retrieves the value to the left of the operator finding it's start index.
        /// </summary>
        /// <param name="expression">Whole expression.</param>
        /// <param name="operatorPosition">Position of the operator.</param>
        /// <param name="valueStart">Returned start position of the value.</param>
        /// <returns>The value to the left of the operator.</returns>
        private decimal GetLeftValue(StringBuilder expression, int operatorPosition, out int valueStart)
        {
            valueStart = operatorPosition - 1;
            while (valueStart > 0 && (char.IsDigit(expression[valueStart - 1]) || expression[valueStart - 1] == '.'
                       || expression[valueStart - 1] == '-'))
            {
                --valueStart;
            }

            if (!decimal.TryParse(expression.ToString(valueStart, operatorPosition - valueStart), out decimal value))
            {
                throw new ArgumentException("Wrong value for left value of [" + expression.ToString(valueStart,
                    operatorPosition - valueStart + 1));
            }

            return value;
        }

        /// <summary>
        /// Retrieves the value to the right of the operator finding it's end index.
        /// </summary>
        /// <param name="expression">Whole expression.</param>
        /// <param name="operatorPosition">Position of the operator.</param>
        /// <param name="valueEnd">Returned end position of the value.</param>
        /// <returns>The value to the right of the operator.</returns>
        private decimal GetRightValue(StringBuilder expression, int operatorPosition, out int valueEnd)
        {
            valueEnd = operatorPosition + 1;
            while (valueEnd + 1 < expression.Length && (char.IsDigit(expression[valueEnd + 1])
                || expression[valueEnd + 1] == '.' || expression[valueEnd + 1] == '-'))
            {
                ++valueEnd;
            }

            decimal value;
            if (!decimal.TryParse(expression.ToString(operatorPosition + 1, valueEnd - operatorPosition), out value))
            {
                throw new ArgumentException("Wrong right value for expression " + expression.ToString(operatorPosition,
                                                valueEnd - operatorPosition + 1));
            }

            return value;
        }

        /// <summary>
        /// Evaluate string expression from _expression, starting at specified character.
        /// </summary>
        /// <param name="startPosition">Position start for evaluate.</param>
        /// <returns>Next position after evaluated expression.</returns>
        /// <exception cref="ArgumentException">Thrown when there is invalid expression in parsed string.</exception>
        /// <exception cref="DivideByZeroException">Thrown on divide by 0.</exception>
        /// <remarks>
        /// When the parser find "(" character it start itself recursively parsing from that character. When it finds
        /// ")" it ends recursive instance and substitutes parsed expression together with brackets (if any) with it's
        /// result.
        /// </remarks>
        private int EvaluateSubExpression(int startPosition)
        {
            int endPosition = startPosition;

            // Expression operators order: () % ×/÷ +/-

            // ()
            for (; endPosition < _expression.Length; ++endPosition)
            {
                if (_expression[endPosition] == '(')
                {
                    endPosition = EvaluateSubExpression(endPosition + 1);
                }

                if (endPosition < _expression.Length && _expression[endPosition] == ')')
                {
                    break;
                }
            }

            if (endPosition > _expression.Length)
            {
                endPosition = _expression.Length;
            }

            StringBuilder thisExpression = new StringBuilder(
                _expression.ToString(startPosition, endPosition - startPosition));

            // %
            for (int position = 0; position < thisExpression.Length; ++position)
            {
                if (thisExpression[position] == '%')
                {
                    int percentStart;
                    string newValue = (GetLeftValue(thisExpression, position, out percentStart) / 100).ToString();
                    thisExpression.Remove(percentStart, position - percentStart + 1);
                    thisExpression.Insert(percentStart, newValue);
                    position = percentStart;
                }
            }

            // "--" convert to positive
            // End parsing bound is -2 as we don't accept "--" at the end of expression
            for (int position = 0; position < thisExpression.Length - 2; ++position)
            {
                if (thisExpression[position] == '-' && thisExpression[position + 1] == '-')
                {
                    thisExpression.Remove(position, 2);
                    if (position > 0 && char.IsDigit(thisExpression[position - 1]))
                    {
                        thisExpression.Insert(position, '+');
                    }
                }
            }

            // "-" when should be treated as decrease we convert it to "+<negative value>"
            for (int position = 1; position < thisExpression.Length; ++position)
            {
                if (thisExpression[position] == '-' && char.IsDigit(thisExpression[position - 1]))
                {
                    thisExpression.Insert(position, '+');
                }
            }

            // ×/÷
            for (int position = 0; position < thisExpression.Length; ++position)
            {
                if (thisExpression[position] == '×')
                {
                    decimal lValue, rValue;
                    lValue = GetLeftValue(thisExpression, position, out var leftValueStart);
                    rValue = GetRightValue(thisExpression, position, out var rightValueEnd);
                    thisExpression.Remove(leftValueStart, rightValueEnd - leftValueStart + 1);
                    thisExpression.Insert(leftValueStart, (lValue * rValue).ToString());
                    position = leftValueStart;
                }
                else if (thisExpression[position] == '÷')
                {
                    decimal lValue, rValue;
                    lValue = GetLeftValue(thisExpression, position, out var leftValueStart);
                    rValue = GetRightValue(thisExpression, position, out var rightValueEnd);
                    thisExpression.Remove(leftValueStart, rightValueEnd - leftValueStart + 1);
                    thisExpression.Insert(leftValueStart, (lValue / rValue).ToString());
                    position = leftValueStart;
                }
            }

            // +
            for (int position = 0; position < thisExpression.Length; ++position)
            {
                if (thisExpression[position] == '+')
                {
                    decimal lValue, rValue;
                    lValue = GetLeftValue(thisExpression, position, out var leftValueStart);
                    rValue = GetRightValue(thisExpression, position, out var rightValueEnd);
                    thisExpression.Remove(leftValueStart, rightValueEnd - leftValueStart + 1);
                    thisExpression.Insert(leftValueStart, (lValue + rValue).ToString());
                    position = leftValueStart;
                }
            }

            if (startPosition > 0 && _expression[startPosition - 1] == '(')
            {
                --startPosition;
            }

            if (endPosition < _expression.Length && _expression[endPosition] == ')')
            {
                ++endPosition;
            }

            _expression.Remove(startPosition, endPosition - startPosition);
            _expression.Insert(startPosition, thisExpression);

            return startPosition + thisExpression.Length;
        }

        /// <summary>
        /// Returns string with formatted <paramref name="value"/> rounded to fit maximum length
        /// (<paramref name="maxLength"/> parameter)
        /// </summary>
        /// <param name="value">Value to convert to string.</param>
        /// <param name="maxLength">Maximum resulting string length.</param>
        /// <returns>Resulting string.</returns>
        private string GetValueString(decimal value, int maxLength)
        {
            int dotIndex = value.ToString(CultureInfo.InvariantCulture).IndexOf('.');
            if (dotIndex == -1)
            {
                dotIndex = value.ToString(CultureInfo.InvariantCulture).Length;
            }

            if (dotIndex > maxLength)
            {
                throw new MathExpressionResultLengthExceededException();
            }

            value = (dotIndex >= maxLength - 1) ? Math.Round(value) : Math.Round(value, maxLength - dotIndex - 1);
            value /= 1.000000000000000000000000000000000M;
            return value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Validates brackets count.
        /// When opening and closing brackets count differs, does nothing.
        /// Evaluates expression passed by constructor.
        /// If successful evaluated result is returned as string with dots replaced with commas.
        /// </summary>
        /// <param name="maxLength">Maximum returned result string length.</param>
        /// <returns>Result string or null if there are any expression errors.</returns>
        public string Evaluate(int maxLength)
        {
            if (new Regex(Regex.Escape("(")).Matches(_expression.ToString()).Count -
                new Regex(Regex.Escape(")")).Matches(_expression.ToString()).Count < 0)
            {
                throw new MathExpressionEvaluationException();
            }

            int position;
            try
            {
                position = EvaluateSubExpression(0);
            }
            catch (Exception e)
            {
                if (e is ArgumentException || e is DivideByZeroException)
                {
                    throw new MathExpressionEvaluationException();
                }

                throw;
            }

            if (position < _expression.Length)
            {
                throw new MathExpressionEvaluationException();
            }

            if (!decimal.TryParse(_expression.ToString(), out decimal value))
            {
                throw new MathExpressionEvaluationException();
            }

            return GetValueString(value, maxLength);
        }

        #endregion methods
    }
}
