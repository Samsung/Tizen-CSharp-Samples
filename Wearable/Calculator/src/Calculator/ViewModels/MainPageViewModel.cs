
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
    /// A Calculator ViewModel class which manages bindings and sending notifications.
    /// </summary>
    /// <seealso cref="Views.CalculatorMainPage">
    public class MainPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Instance of MainPageViewModel which is wrapped in lazy<T> type.
        /// </summary>
        private static readonly Lazy<MainPageViewModel> lazy =
                new Lazy<MainPageViewModel>(() => new MainPageViewModel());

        /// <summary>
        /// This property provides MainPageViewModel instance.
        /// </summary>
        public static MainPageViewModel Instance => lazy.Value;
        
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// This property provides formatted expression text. 
        /// </summary>
        public FormattedString ExpressionText
        {
            get;
            private set;
        }

        /// <summary>
        /// A calculating result text.
        /// </summary>
        public FormattedString ResultText
        {
            get;
            private set;
        }

        private static readonly String ResultRegularColor = "#7F000000";
        private static readonly String ResultSoloColor = "#59C03A";

        /// <summary>
        /// Calculation result text color.
        /// </summary>
        public String ResultColor
        {
            get;
            private set;
        }
        
        /// <summary>
        /// An element button's command 
        /// </summary>
        public ICommand PressButton { protected set; get; }
        /// <summary>
        /// A clear command button's command 
        /// </summary>
        public ICommand Clear { protected set; get; }
        /// <summary>
        /// A remove last button's command 
        /// </summary>
        public ICommand RemoveLast { protected set; get; }
        /// <summary>
        /// A = command button's command 
        /// </summary>
        public ICommand Calculate { protected set; get; }
        /// <summary>
        /// A +/- operator button's command 
        /// </summary>
        public ICommand Reverse { protected set; get; }

        /// <summary>
        /// MainPageViewModel constructor.
        /// In this constructor, view initialization and commands bindings are completed. 
        /// </summary>
        private MainPageViewModel()
        {
            SetDisplayEmpty();

            if (CalculatorApp.InputParserInstance.IsEmptyInputElements == false)
            {
                IEnumerable<InputElement> plainTexts = CalculatorApp.InputParserInstance.ExpressionElements;
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
                AddingElementResult res = CalculatorApp.InputParserInstance.GetSeparatedPlainText(input, out plainTexts);
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
                CalculatorApp.InputParserInstance.Clear();
            });

            this.RemoveLast = new Command(() =>
            {
                IEnumerable<InputElement> plainTexts;
                if (CalculatorApp.InputParserInstance.DeleteLast(out plainTexts) == false)
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

            this.Calculate = new Command(() =>
            {
                IEnumerable<InputElement> plainTexts;
                if (CalculatorApp.InputParserInstance.Equal(out plainTexts) == false)
                {
                    return;
                }

                FormattedString result;
                FormattedString expression;
                if (GetEqualResult(plainTexts, out expression, out result) == false)
                {
                    return;
                }

                CalculatorApp.InputParserInstance.Set(CalculatorApp.CalculatorInstance.Result.ToString());
                ResultText = result;

                UpdateDisplay(true);
            });

            this.Reverse = new Command(() =>
            {
                IEnumerable<InputElement> plainTexts;
                if (CalculatorApp.InputParserInstance.ReverseSign(out plainTexts) == false)
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
        }

        /// <summary>
        /// The method provides calculated result and formatted expression for given expression. </summary>
        /// <param name="inputExpression"> An expression which consist of InputElements with IEnumerable interface. </param>
        /// <param name="isNeedCheckException"> Value indicates whether error displaying is needed. </param>
        /// <param name="expression"> A formatted expression. </param>
        /// <param name="result"> A formatted calculation result. </param>
        /// <returns> A status of calculation </returns>
        private bool GetCalculatedResult(IEnumerable<InputElement> inputExpression,
            bool isNeedCheckException,
            out FormattedString expression,
            out FormattedString result)
        {
            expression = CalculatorApp.FormatterInstance.GetFormattedExpressionText(inputExpression);
            result = string.Empty;

            CalculationResult resCal = CalculatorApp.CalculatorInstance.SetExpression(inputExpression);
            if ((resCal is CalculationSuccessful) == false)
            {
                if (isNeedCheckException)
                {
                    DisplayError(resCal.Message);
                    return false;
                }

                return true;
            }

            result = CalculatorApp.FormatterInstance.GetFormattedOutputText(CalculatorApp.CalculatorInstance.Result.ToString());
            return true;
        }

        /// <summary>
        /// The method provides calculated result and formatted expression for given inputExpression. </summary>
        /// <param name="inputExpression"> An expression which consist of InputElements with IEnumerable interface. </param>
        /// <param name="expression"> A formatted expression. </param>
        /// <param name="result"> A formatted calculation result. </param>
        /// <returns> A result status of equal key execution </returns>
        private bool GetEqualResult(IEnumerable<InputElement> inputExpression,
            out FormattedString expression,
            out FormattedString result)
        {
            expression = CalculatorApp.FormatterInstance.GetFormattedExpressionText(inputExpression);
            result = string.Empty;

            CalculationResult resCal = CalculatorApp.CalculatorInstance.Equal(inputExpression);
            if ((resCal is CalculationSuccessful) == false)
            {
                DisplayError(resCal.Message);
                return false;
            }

            result = CalculatorApp.FormatterInstance.GetFormattedOutputText(CalculatorApp.CalculatorInstance.Result.ToString());
            return true;
        }

        /// <summary>
        /// This method updates expression and result texts assuming calculation has not completed.
        /// </summary>
        private void UpdateDisplay()
        {
            UpdateDisplay(false);
        }

        /// <summary>
        /// This method updates expression and result texts. 
        /// </summary>
        /// <param name="isCalculationCompleted"> Flag value indicating whether calculation has completed or not. </param>
        private void UpdateDisplay(bool isCalculationCompleted)
        {
            UpdateResultTextColor(isCalculationCompleted);

            OnPropertyChanged("ExpressionText");
            OnPropertyChanged("ResultText");
        }

        /// <summary>
        /// This method updates result text color. 
        /// </summary>
        /// <param name="isCalculationCompleted"> Flag value indicating whether calculation has completed or not. </param>
        private void UpdateResultTextColor(bool isCalculationCompleted)
        {
            if (isCalculationCompleted)
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
        }
        

        /// <summary>
        /// This method clears Expression and result texts. 
        /// </summary>
        private void SetDisplayEmpty()
        {
            ExpressionText = string.Empty;
            ResultText = string.Empty;
            UpdateDisplay();
        }

        /// <summary>
        /// This method displaying error message on the screen.
        /// </summary>
        /// <param name="message"> Message to be displayed </param>
        private void DisplayError(String message)
        {
            if (message.Length > 0)
            {
                MessagingCenter.Send(this, "alert", message);
            }
        }

        /// <summary>
        /// The method is used to notify that property has changed. </summary>
        /// <param name="propertyName"> A property name. </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
