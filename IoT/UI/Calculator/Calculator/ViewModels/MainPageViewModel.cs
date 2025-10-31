/*
 * Copyright (c) 2025 Samsung Electronics Co., Ltd
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

using Calculator.Impl;
using Calculator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tizen.Applications;
using Tizen.NUI;

namespace Calculator.ViewModels
{
    /// <summary>
    /// A Calculator ViewModel class which manages all pages (CalculatorMainPage, CalculatorMainPageLnadscape)
    /// by bindings and sending notifications.
    /// </summary>
    /// <seealso cref="Views.CalculatorMainPage">
    /// <seealso cref="Views.CalculatorMainPageLandscape">
    public class MainPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// A instance of MainPageViewModel which is wrapped as lazy<T> type.
        /// </summary>
        private static readonly Lazy<MainPageViewModel> lazy = new Lazy<MainPageViewModel>(() => new MainPageViewModel());

        /// <summary>
        /// A property to provide MainPageViewModel instance.
        /// </summary>
        public static MainPageViewModel Instance { get { return lazy.Value; } }

        /// <summary>
        /// Property changing event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public class ErrorEventArgs : EventArgs
        {
            public ErrorEventArgs(string value)
            {
                Message = value;
            }

            public string Message { get; }
        }

        /// <summary>
        /// Error message event handler
        /// </summary>
        public event EventHandler<ErrorEventArgs> ErrorOccured;

        /// <summary>
        /// A calculating expression text.
        /// </summary>
        public string ExpressionText
        {
            get;
            private set;
        }

        /// <summary>
        /// A calculating result text.
        /// </summary>
        public string ResultText
        {
            get;
            private set;
        }

        private static readonly Color RegularColor = new Color("#7F0000"); // originally "#7F000000"
        private static readonly UIColor ResultRegularColor = new UIColor(RegularColor.R, RegularColor.G, RegularColor.B, RegularColor.A);
        private static readonly Color SoloColor = new Color("#59B03A");
        private static readonly UIColor ResultSoloColor = new UIColor(SoloColor.R, SoloColor.G, SoloColor.B, SoloColor.A);

        /// <summary>
        /// A calculating result text color.
        /// </summary>
        public UIColor ResultColor
        {
            get;
            private set;
        }

        /// <summary>
        /// A image file name indicates degree metric can be changed.
        /// </summary>
        public string DegreeMetricImageFileName
        {
            get;
            private set;
        }

        /// <summary>
        /// A flag which indicates Radian metric is using for calculation
        /// </summary>
        public bool IsRadUsing
        {
            get;
            private set;
        }

        private static readonly string degreeImageFileName = Application.Current.DirectoryInfo.Resource + "calculator_button_l_24.png";
        private static readonly string radianImageFileName = Application.Current.DirectoryInfo.Resource + "calculator_button_l_25.png";

        /// <summary>
        /// A element button's command
        /// </summary>
        public ICommand PressButton { protected set; get; }
        /// <summary>
        /// A clear command button's command
        /// </summary>
        public ICommand Clear { protected set; get; }
        /// <summary>
        /// A <- command button's command
        /// </summary>
        public ICommand RemoveLast { protected set; get; }
        /// <summary>
        /// A = command button's command
        /// </summary>
        public ICommand Equal { protected set; get; }
        /// <summary>
        /// A +/- operator button's command
        /// </summary>
        public ICommand Reverse { protected set; get; }
        /// <summary>
        /// A +/- operator button's command
        /// </summary>
        public ICommand ChangeDegreeMetric { protected set; get; }

        /// <summary>
        /// MainPageViewModel constructor.
        /// In this constructor, The view initialization and commands bindings are completed.
        /// </summary>
        private MainPageViewModel()
        {
            SetDisplayEmpty();
            SetDegreeButton(true);

            if (Calculator.InputParserInstance.IsEmptyInputElements == false)
            {
                IEnumerable<InputElement> plainTexts = Calculator.InputParserInstance.ExpressionElements;
                string result;
                string expression;
                GetCalculatedResult(plainTexts, false, out expression, out result);
                ResultText = result;

                ExpressionText = expression;
                UpdateDisplay();
            }

            PressButton = new Command((value) =>
            {
                string input = value.ToString();

                IEnumerable<InputElement> plainTexts;
                AddingElementResult res = Calculator.InputParserInstance.GetseparatedPlainText(input, out plainTexts);
                if ((res is AddingPossible) == false)
                {
                    DisplayError(res.Message);
                    return;
                }

                string result;
                string expression;
                GetCalculatedResult(plainTexts, false, out expression, out result);

                ExpressionText = expression;
                ResultText = result;
                UpdateDisplay();
            });

            Clear = new Command(() =>
            {
                SetDisplayEmpty();
                Calculator.InputParserInstance.Clear();
            });

            RemoveLast = new Command(() =>
            {
                IEnumerable<InputElement> plainTexts;
                if (Calculator.InputParserInstance.DeleteLast(out plainTexts) == false)
                {
                    return;
                }

                string result;
                string expression;
                GetCalculatedResult(plainTexts, false, out expression, out result);

                ExpressionText = expression;
                ResultText = result;
                UpdateDisplay();
            });

            Equal = new Command(() =>
            {
                IEnumerable<InputElement> plainTexts;
                if (Calculator.InputParserInstance.Equal(out plainTexts) == false)
                {
                    return;
                }

                string result;
                string expression;
                if (GetEqualResult(plainTexts, out expression, out result) == false)
                {
                    return;
                }

                Calculator.InputParserInstance.Set(Calculator.CalculatorInstance.Result.ToString());
                ExpressionText = "";
                ResultText = result;

                UpdateDisplay(true);
            });

            Reverse = new Command(() =>
            {
                IEnumerable<InputElement> plainTexts;
                if (Calculator.InputParserInstance.ReverseSign(out plainTexts) == false)
                {
                    DisplayError("Invalid format used.");
                    return;
                }

                string result;
                string expression;
                GetCalculatedResult(plainTexts, false, out expression, out result);

                ExpressionText = expression;
                ResultText = result;
                UpdateDisplay();
            });

            ChangeDegreeMetric = new Command(() =>
            {
                SetDegreeButton();

                string result;
                string expression;
                GetCalculatedResult(Calculator.InputParserInstance.ExpressionElements, false, out expression, out result);

                ExpressionText = expression;
                ResultText = result;
                UpdateDisplay();
            });
        }

        /// <summary>
        /// A method provides calculated result and formatted expression for given expression.
        /// </summary>
        /// <param name="inputExpression"> A expression which consist of InputElements with IEnumerable interface. </param>
        /// <param name="isNeedCheckException"> A flag value indicates error displaying needs if error happen. </param>
        /// <param name="expression"> A formatted expression. </param>
        /// <param name="result"> A formatted calculation result. </param>
        /// <returns> A status of calculation </returns>
        private bool GetCalculatedResult(IEnumerable<InputElement> inputExpression, bool isNeedCheckException, out string expression, out string result)
        {
            expression = Calculator.FormatterInstance.GetFormattedExpressionText(inputExpression);
            result = "";

            CalculateResult resCal = Calculator.CalculatorInstance.SetExpression(inputExpression);
            if ((resCal is CalculateSuccess) == false)
            {
                if (isNeedCheckException)
                {
                    DisplayError(resCal.Message);
                    return false;
                }

                return true;
            }

            result = Calculator.FormatterInstance.GetFormattedOutputText(Calculator.CalculatorInstance.Result.ToString());
            return true;
        }

        /// <summary>
        /// A method provides calculated result and formatted expression for given inputExpression.
        /// </summary>
        /// <param name="inputExpression"> A expression which consist of InputElements with IEnumerable interface. </param>
        /// <param name="expression"> A formatted expression. </param>
        /// <param name="result"> A formatted calculation result. </param>
        /// <returns> A result status of equal key execution </returns>
        private bool GetEqualResult(IEnumerable<InputElement> inputExpression, out string expression, out string result)
        {
            expression = Calculator.FormatterInstance.GetFormattedExpressionText(inputExpression);
            result = "";

            CalculateResult resCal = Calculator.CalculatorInstance.Equal(inputExpression);
            if ((resCal is CalculateSuccess) == false)
            {
                DisplayError(resCal.Message);
                return false;
            }

            result = Calculator.FormatterInstance.GetFormattedOutputText(Calculator.CalculatorInstance.Result.ToString());
            return true;
        }

        /// <summary>
        /// A method updates the expression displaying text and the result displaying text.
        /// But Result color will be set for a Color meets for calculating situation.
        /// </summary>
        private void UpdateDisplay()
        {
            UpdateDisplay(false);
        }

        /// <summary>
        /// A method updates the expression displaying text and the result displaying text.
        /// </summary>
        /// <param name="isCalculateCompleted"> A flag value shows whether calculating completed or not. </param>
        private void UpdateDisplay(bool isCalculateCompleted)
        {
            if (isCalculateCompleted)
            {
                if (ResultColor != ResultSoloColor)
                {
                    ResultColor = ResultSoloColor;
                    OnPropertyChanged("ResultColor");
                }
            }
            else
            {
                if (ResultColor != ResultRegularColor)
                {
                    ResultColor = ResultRegularColor;
                    OnPropertyChanged("ResultColor");
                }
            }

            OnPropertyChanged("ExpressionText");
            OnPropertyChanged("ResultText");
        }

        /// <summary>
        /// A method makes the expression displaying text and the result displaying text as initial status.
        /// </summary>
        private void SetDisplayEmpty()
        {
            ExpressionText = "";
            ResultText = "";
            UpdateDisplay();
        }

        /// <summary>
        /// A method change degree metric button.
        /// </summary>
        /// <param name="isDegree"> A flag value indicates which metric is used (Degree, Radian) </param>
        private void SetDegreeButton(bool isDegree)
        {
            Operators.IsDegreeMetricUsed = isDegree;
            if (isDegree)
            {
                DegreeMetricImageFileName = radianImageFileName;
                IsRadUsing = false;
            }
            else
            {
                DegreeMetricImageFileName = degreeImageFileName;
                IsRadUsing = true;
            }

            OnPropertyChanged("DegreeMetricImageFileName");
            OnPropertyChanged("IsRadUsing");
            DebuggingUtils.Dbg("degree metric " + (Operators.IsDegreeMetricUsed ? "Degree" : "Radian"));
        }

        /// <summary>
        /// A method toggle degree metric button.
        /// </summary>
        private void SetDegreeButton()
        {
            SetDegreeButton(DegreeMetricImageFileName.Equals(degreeImageFileName));
        }

        /// <summary>
        /// A method displaying a message in a screen.
        /// </summary>
        /// <param name="message"> A displaying message. </param>
        private void DisplayError(string message)
        {
            if (message.Length > 0)
            {
                DebuggingUtils.Err($"Error occurs: {message}.");
                ErrorOccured?.Invoke(this, new ErrorEventArgs(message));
            }
        }

        /// <summary>
        /// A method notifying a property changing situation. </summary>
        /// <param name="propertyName"> A property name. </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
