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


using Xamarin.Forms;
using Calculator.Views;
using Calculator.Impl;
using System;

namespace Calculator
{
    /// <summary>
    /// Calculator implementation main class.
    /// This class has main model classes (InputParser, Formatter and Calculator).
    /// Also handling a making a Calculator view instance to display Calculator layout.
    /// </summary>
    public sealed class Calculator : Application, IAppConfigurationChanged
    {
        /// <summary>
        /// A InputParser class instance.
        /// The InputParser checking user input and validate expression for calculating.
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
        /// The Formatter class provides formatted displaying text from validated expression.
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
        /// The CalculatorImpl class calculate the validated expression and provides a result value.
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

        /// <summary>
        /// Calculator's constructor
        /// </summary>
        /// <param name="isLandscape"> A flag whether current display orientation is landscape or not. </param>
        public Calculator(bool isLandscape)
        {
            if (isLandscape)
            {
                OnOrientationChanged(AppOrientation.Landscape);
            }
            else
            {
                OnOrientationChanged(AppOrientation.Portrait);
            }
        }

        /// <summary>
        /// Device orientation changing notification interface.
        /// </summary>
        /// <param name="orientation"> A value which shows changed device orientation. </param>
        public void OnOrientationChanged(AppOrientation orientation)
        {
            switch (orientation)
            {
                case AppOrientation.Landscape:
                    FormatterInstance.IsLandscapeOrientation = true;
                    MainPage = new CalculatorMainPageLandscape();
                    break;

                case AppOrientation.Portrait:
                default:
                    FormatterInstance.IsLandscapeOrientation = false;
                    MainPage = new CalculatorMainPage();
                    break;
            }
        }
    }
}
