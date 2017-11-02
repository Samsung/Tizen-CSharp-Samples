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
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

using Calculator.Impl;
using Calculator.Models;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Calculator.ViewModels
{
    /// <summary>
    /// A String to Color converter class.
    /// </summary>
    /// <remarks>
    /// Please refer to Xamarin Custom Renderer
    /// https://developer.xamarin.com/guides/xamarin-forms/custom-renderer/
    /// </remarks>
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.FromHex(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

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
        private static readonly Lazy<MainPageViewModel> lazy =
                new Lazy<MainPageViewModel>(() => new MainPageViewModel());

        /// <summary>
        /// A property to provide MainPageViewModel instance.
        /// </summary>
        public static MainPageViewModel Instance { get { return lazy.Value; } }

        /// <summary>
        /// Property changing event handler</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// A calculating expression text. </summary>
        public FormattedString ExpressionText
        {
            get;
            private set;
        }

        /// <summary>
        /// A calculating result text. </summary>
        public FormattedString ResultText
        {
            get;
            private set;
        }

        private static readonly String ResultRegularColor = "#7F000000";
        private static readonly String ResultSoloColor = "#59B03A";

        /// <summary>
        /// A calculating result text color. </summary>
        public String ResultColor
        {
            get;
            private set;
        }

        /// <summary>
        /// A image file name indicates degree metric can be changed. </summary>
        public String DegreeMetricImageFileName
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

        private static readonly String degreeImageFileName = "calculator_button_l_24.png";
        private static readonly String radianImageFileName = "calculator_button_l_25.png";

        /// <summary>
        /// A element button's command </summary>
        public ICommand PressButton { protected set; get; }
        /// <summary>
        /// A clear command button's command </summary>
        public ICommand Clear { protected set; get; }
        /// <summary>
        /// A <- command button's command </summary>
        public ICommand RemoveLast { protected set; get; }
        /// <summary>
        /// A = command button's command </summary>
        public ICommand Equal { protected set; get; }
        /// <summary>
        /// A +/- operator button's command </summary>
        public ICommand Reverse { protected set; get; }
        /// <summary>
        /// A +/- operator button's command </summary>
        public ICommand ChangeDegreeMetric { protected set; get; }

        /// <summary>
        /// MainPageViewModel constructor.
        /// In this constructor, The view initialization and commands bindings are completed. </summary>
        private MainPageViewModel()
        {
            SetDisplayEmpty();
            SetDegreeButton(true);

            if (Calculator.InputParserInstance.IsEmptyInputElements == false)
            {
                IEnumerable<InputElement> plainTexts = Calculator.InputParserInstance.ExpressionElements;
                FormattedString result;
                FormattedString expression;
                GetCalculatedResult(plainTexts, false, out expression, out result);
                ResultText = result;

                ExpressionText = expression;
                UpdateDisplay();
            }

            this.PressButton = new Command((value) =>
            {
                string input = value.ToString();

                IEnumerable<InputElement> plainTexts;
                AddingElementResult res = Calculator.InputParserInstance.GetseparatedPlainText(input, out plainTexts);
                if ((res is AddingPossible) == false)
                {
                    DisplayError(res.Message);
                    return;
                }

                FormattedString result;
                FormattedString expression;
                GetCalculatedResult(plainTexts, false, out expression, out result);

                ExpressionText = expression;
                ResultText = result;
                UpdateDisplay();
            });

            this.Clear = new Command(() =>
            {
                SetDisplayEmpty();
                Calculator.InputParserInstance.Clear();
            });

            this.RemoveLast = new Command(() =>
            {
                IEnumerable<InputElement> plainTexts;
                if (Calculator.InputParserInstance.DeleteLast(out plainTexts) == false)
                {
                    return;
                }

                FormattedString result;
                FormattedString expression;
                GetCalculatedResult(plainTexts, false, out expression, out result);

                ExpressionText = expression;
                ResultText = result;
                UpdateDisplay();
            });

            this.Equal = new Command(() =>
            {
                IEnumerable<InputElement> plainTexts;
                if (Calculator.InputParserInstance.Equal(out plainTexts) == false)
                {
                    return;
                }

                FormattedString result;
                FormattedString expression;
                if (GetEqualResult(plainTexts, out expression, out result) == false)
                {
                    return;
                }

                Calculator.InputParserInstance.Set(Calculator.CalculatorInstance.Result.ToString());
                ExpressionText = string.Empty;
                ResultText = result;

                UpdateDisplay(true);
            });

            this.Reverse = new Command(() =>
            {
                IEnumerable<InputElement> plainTexts;
                if (Calculator.InputParserInstance.ReverseSign(out plainTexts) == false)
                {
                    DisplayError("Invalid format used.");
                    return;
                }

                FormattedString result;
                FormattedString expression;
                GetCalculatedResult(plainTexts, false, out expression, out result);

                ExpressionText = expression;
                ResultText = result;
                UpdateDisplay();
            });

            this.ChangeDegreeMetric = new Command(() =>
            {
                SetDegreeButton();

                FormattedString result;
                FormattedString expression;
                GetCalculatedResult(Calculator.InputParserInstance.ExpressionElements, false, out expression, out result);

                ExpressionText = expression;
                ResultText = result;
                UpdateDisplay();
            });
        }

        /// <summary>
        /// A method provides calculated result and formatted expression for given expression. </summary>
        /// <param name="inputExpression"> A expression which consist of InputElements with IEnumerable interface. </param>
        /// <param name="isNeedCheckException"> A flag value indicates error displaying needs if error happen. </param>
        /// <param name="expression"> A formatted expression. </param>
        /// <param name="result"> A formatted calculation result. </param>
        /// <returns> A status of calculation </returns>
        private bool GetCalculatedResult(IEnumerable<InputElement> inputExpression,
            bool isNeedCheckException,
            out FormattedString expression,
            out FormattedString result)
        {
            expression = Calculator.FormatterInstance.GetFormattedExpressionText(inputExpression);
            result = string.Empty;

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
        /// A method provides calculated result and formatted expression for given inputExpression. </summary>
        /// <param name="inputExpression"> A expression which consist of InputElements with IEnumerable interface. </param>
        /// <param name="expression"> A formatted expression. </param>
        /// <param name="result"> A formatted calculation result. </param>
        /// <returns> A result status of equal key execution </returns>
        private bool GetEqualResult(IEnumerable<InputElement> inputExpression,
            out FormattedString expression,
            out FormattedString result)
        {
            expression = Calculator.FormatterInstance.GetFormattedExpressionText(inputExpression);
            result = string.Empty;

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
        /// But Result color will be set for a Color meets for calculating situation. </summary>
        private void UpdateDisplay()
        {
            UpdateDisplay(false);
        }

        /// <summary>
        /// A method updates the expression displaying text and the result displaying text. </summary>
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
        /// A method makes the expression displaying text and the result displaying text as initial status. </summary>
        private void SetDisplayEmpty()
        {
            ExpressionText = string.Empty;
            ResultText = string.Empty;
            UpdateDisplay();
        }

        /// <summary>
        /// A method change degree metric button. </summary>
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
        /// A method toggle degree metric button. </summary>
        private void SetDegreeButton()
        {
            SetDegreeButton(DegreeMetricImageFileName.Equals(degreeImageFileName));
        }

        /// <summary>
        /// A method displaying a message in a screen. </summary>
        /// <param name="message"> A displaying message. </param>
        private void DisplayError(String message)
        {
            if (message.Length > 0)
            {
                MessagingCenter.Send<MainPageViewModel, string>(this, "alert", message);
            }
        }

        /// <summary>
        /// A method notifying a property changing situation. </summary>
        /// <param name="propertyName"> A property name. </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
