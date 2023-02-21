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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BasicCalculator.Models;
using Tizen.NUI;
using Tizen.NUI.Binding;

namespace BasicCalculator.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        #region fields

        /// <summary>
        /// Maximum length of the expression.
        /// </summary>
        private const int EXPRESSION_MAX_LENGTH = 24;

        /// <summary>
        /// Maximum length of the expression's calculated value.
        /// </summary>
        private const int RESULT_MAX_LENGTH = 12;

        /// <summary>
        /// Backing field for the <see cref="EnteredExpression"/> property.
        /// </summary>
        private string _enteredExpression = "0";

        /// <summary>
        /// Backing field for the <see cref="CurrentResult"/> property.
        /// </summary>
        private string _currentResult = "0";

        /// <summary>
        /// Event fired on property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion fields

        #region properties

        /// <summary>
        /// Current result of entered expression.
        /// </summary>
        public string CurrentResult
        {
            get => _currentResult;
            set
            {
                _currentResult = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Currently entered expression for calculation.
        /// Sets expression new value if it has valid length.
        /// Calls result recalculation when changed.
        /// </summary>
        public string EnteredExpression
        {
            get => _enteredExpression;
            set
            {
                if (value.Length > EXPRESSION_MAX_LENGTH || value.Equals(_enteredExpression))
                {
                    return;
                }

                _enteredExpression = value;

                // Update current result
                MathExpression e = new MathExpression(_enteredExpression);
                try
                {
                    string result = e.Evaluate(RESULT_MAX_LENGTH);
                    CurrentResult = result;
                }
                catch (MathExpressionEvaluationException)
                {
                    CurrentResult = "Expression error";
                }
                catch (MathExpressionResultLengthExceededException)
                {
                    CurrentResult = "Result length exceeded";
                }
                catch (Exception)
                {
                    CurrentResult = "Error";
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command for appending a character to the current expression.
        /// </summary>
        public ICommand AppendToExpressionCommand { get; set; }

        /// <summary>
        /// Command for clearing current expression.
        /// </summary>
        public ICommand ClearInputCommand { get; set; }

        /// <summary>
        /// Command for adding bracket to the current expression.
        /// </summary>
        public ICommand BracketCommand { get; set; }

        /// <summary>
        /// Command to delete last character from the current expression.
        /// </summary>
        public ICommand DeleteCommand { get; set; }

        /// <summary>
        /// Command for changing sign of the lastly entered value.
        /// </summary>
        public ICommand ChangeSignCommand { get; set; }

        /// <summary>
        /// Command to evaluate current expression.
        /// </summary>
        public ICommand EvaluateExpressionCommand { get; set; }

        /// <summary>
        /// Command for appending decimal point to the expression.
        /// </summary>
        public ICommand AppendDecimalPointCommand { get; set; }

        /// <summary>
        /// Command for appending an operator to the current expression.
        /// </summary>
        public ICommand AppendOperatorCommand { get; set; }

        #endregion properties

        #region methods

        /// <summary>
        /// Initializes view model.
        /// Creates and assigns view model commands.
        /// </summary>
        public CalculatorViewModel()
        {
            AppendToExpressionCommand = new Command(AppendToExpression);
            ClearInputCommand = new Command(ClearInput);
            BracketCommand = new Command(AppendBracket);
            DeleteCommand = new Command(DeleteLastCharacter);
            ChangeSignCommand = new Command(ChangeSign);
            EvaluateExpressionCommand = new Command(EvaluateExpression);
            AppendDecimalPointCommand = new Command(AppendDecimalPoint);
            AppendOperatorCommand = new Command(AppendOperator);
        }

        /// <summary>
        /// Invokes property changed with caller (property) name.
        /// </summary>
        /// <param name="propertyName">Name of the property changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Indicates if character passed in parameter is supported mathematic operator.
        /// </summary>
        /// <param name="c">Character</param>
        /// <returns>True if character is supported operator.</returns>
        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '×' || c == '÷';
        }

        /// <summary>
        /// Clears current input (entered expression).
        /// </summary>
        private void ClearInput()
        {
            EnteredExpression = "0";
        }

        /// <summary>
        /// Changes sign of the last value.
        /// </summary>
        private void ChangeSign()
        {
            char lastCharacter = EnteredExpression[EnteredExpression.Length - 1];

            if (!char.IsDigit(lastCharacter))
            {
                if (lastCharacter == '-' && EnteredExpression.Length > 1
                    && EnteredExpression[EnteredExpression.Length - 2] == '(')
                {
                    EnteredExpression = EnteredExpression.Remove(EnteredExpression.Length - 2, 2);
                    return;
                }

                if (!IsOperator(lastCharacter))
                {
                    EnteredExpression += "×";
                }

                EnteredExpression += "(-";
                return;
            }

            // Find last value start index
            int lastValueStart = EnteredExpression.Length - 1;
            while (lastValueStart > 0
                && (char.IsDigit(EnteredExpression[lastValueStart - 1]) || EnteredExpression[lastValueStart - 1] == '.'))
            {
                --lastValueStart;
            }

            // If it is already negative, make it positive
            if (lastValueStart >= 2 && EnteredExpression[lastValueStart - 2] == '('
                && EnteredExpression[lastValueStart - 1] == '-')
            {
                EnteredExpression = EnteredExpression.Remove(lastValueStart - 2, 2);
                return;
            }

            if (lastValueStart == 1 && EnteredExpression[0] == '-')
            {
                EnteredExpression = EnteredExpression.Remove(0, 1);
                return;
            }

            // Otherwise make it negative
            EnteredExpression = EnteredExpression.Insert(lastValueStart, "(-");
        }

        /// <summary>
        /// Compares number of the entered opening brackets to the closing brackets.
        /// </summary>
        /// <returns>0 if the number equals, more than 0 if there are more opening brackets,
        /// otherwise less than 0.</returns>
        private int BracketBalance()
        {
            int balance = 0;
            foreach (char t in EnteredExpression)
            {
                balance = t.Equals('(') ? balance + 1 : t.Equals(')') ? balance - 1 : balance;
            }

            return balance;
        }

        /// <summary>
        /// Appends bracket to the current expression.
        /// </summary>
        private void AppendBracket()
        {
            char lastCharacter = EnteredExpression[EnteredExpression.Length - 1];

            // remove last character if it's a '.'
            if (lastCharacter == '.')
            {
                EnteredExpression = EnteredExpression.Remove(EnteredExpression.Length - 1);
                lastCharacter = EnteredExpression[EnteredExpression.Length - 1];
            }

            if (EnteredExpression == "0")
            {
                EnteredExpression = "(";
            }
            else if (IsOperator(lastCharacter) || lastCharacter.Equals('('))
            {
                EnteredExpression += "(";
            }
            else if (BracketBalance() > 0)
            {
                EnteredExpression += ")";
            }
            else
            {
                EnteredExpression += "×(";
            }
        }

        /// <summary>
        /// Deletes last character from the current expression.
        /// </summary>
        private void DeleteLastCharacter()
        {
            EnteredExpression = EnteredExpression.Remove(EnteredExpression.Length - 1);
            if (EnteredExpression.Length == 0)
            {
                EnteredExpression = "0";
            }
        }

        /// <summary>
        /// Appends decimal point to the current expression.
        /// Point is added only when last entered character is a number
        /// and has no decimal point added already.
        /// </summary>
        private void AppendDecimalPoint()
        {
            int index = EnteredExpression.Length - 1;
            if (!char.IsDigit(EnteredExpression[index]))
            {
                return;
            }

            while (index > 0 && char.IsDigit(EnteredExpression[index]))
            {
                --index;
            }

            if (!EnteredExpression[index].Equals('.'))
            {
                EnteredExpression += ".";
            }
        }

        /// <summary>
        /// Appends and operator to the current expression.
        /// Operator is added when last expression character is not an opening bracket.
        /// If method is called with for '-' operator and expression has initial '0' value
        /// then current expression is replaced with '-'.
        /// If last expression character is a comma, it is removed before appending passed operator.
        /// </summary>
        /// <param name="operatorObject">The operator to be added.</param>
        private void AppendOperator(object operatorObject)
        {
            string operatorString = operatorObject as string;
            char lastCharacter = EnteredExpression[EnteredExpression.Length - 1];   // we always have it

            // ignore if last character was '('
            if (lastCharacter.Equals('(') && !operatorString.Equals("-"))
            {
                return;
            }

            // exchange if it's a starting '-' sign
            if (operatorString.Equals("-") && EnteredExpression.Equals("0"))
            {
                EnteredExpression = operatorString;
                return;
            }

            // if operator pressed after other operator, we exchange it
            if (IsOperator(lastCharacter) || lastCharacter.Equals('.'))
            {
                EnteredExpression = EnteredExpression.Remove(EnteredExpression.Length - 1) + operatorString;
                return;
            }

            EnteredExpression += operatorString;
        }

        /// <summary>
        /// Appends one character to currently entered expression.
        /// When adding digit to expression with default value (0) replaces 0 with provided digit.
        /// </summary>
        /// <param name="contentObject">String containing character to append.</param>
        public void AppendToExpression(object contentObject)
        {
            string contentToAppend = contentObject as string;
            if (contentToAppend == null || contentToAppend.Length > 1)
            {
                return;
            }

            if (!(char.IsDigit(contentToAppend[0]) || contentToAppend[0].Equals('%')))
            {
                return;
            }

            char lastCharacter = EnteredExpression[EnteredExpression.Length - 1];

            // digits exchange "0" if it's the only current input
            if (EnteredExpression.Equals("0") && char.IsDigit(contentToAppend[0]))
            {
                EnteredExpression = contentToAppend;
            }

            // insert '%' only after digits and ')', otherwise ignore it
            else if (contentToAppend.Equals("%"))
            {
                if (char.IsDigit(lastCharacter) || lastCharacter.Equals(')'))
                {
                    EnteredExpression += contentToAppend;
                }
            }
            else
            {
                if (lastCharacter.Equals('%'))
                {
                    EnteredExpression += '×';
                }

                EnteredExpression += contentToAppend;
            }
        }

        /// <summary>
        /// Evaluates current expression.
        /// Updates expression with result value if calculated successfully.
        /// </summary>
        private void EvaluateExpression()
        {
            MathExpression e = new MathExpression(EnteredExpression);

            try
            {
                EnteredExpression = e.Evaluate(RESULT_MAX_LENGTH);
            }
            catch (Exception)
            {
                // do nothing
            }

        }

        #endregion methods
    }
}
