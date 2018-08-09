
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

using Xamarin.Forms;
using Calculator.Views;
using Calculator.Impl;

namespace Calculator
{
    public class CalculatorApp : Application
    {
        /// <summary>
        /// InputParser class instance.
        /// The InputParser checks user input and validates calculated expression.
        /// </summary>
        /// <seealso cref="global::Calculator.Impl.InputParser">
        private static readonly Lazy<InputParser> inputParser = new Lazy<InputParser>(() => new InputParser());

        static public InputParser InputParserInstance
        {
            get
            {
                return inputParser.Value;
            }
        }

        /// <summary>
        /// A Formatter class instance.
        /// The Formatter class provides formatting for displaying text from validated expression.
        /// </summary>
        /// <seealso cref="global::Calculator.Impl.Formatter">
        private static readonly Lazy<Formatter> formatter = new Lazy<Formatter>(() => new Formatter());
        static public Formatter FormatterInstance
        {
            get
            {
                return formatter.Value;
            }
        }

        /// <summary>
        /// A CalculatorImpl class instance.
        /// The CalculatorImpl class calculates validated expression and provides result value.
        /// </summary>
        /// <seealso cref="global::Calculator.Impl.CalculatorImpl">
        private static readonly Lazy<CalculatorImpl> calculator = new Lazy<CalculatorImpl>(() => new CalculatorImpl());
        static public CalculatorImpl CalculatorInstance
        {
            get
            {
                return calculator.Value;
            }
        }

        public CalculatorApp()
        {
            // The root page of the application
            MainPage = new CalculatorMainPage();
        }
    }
}
