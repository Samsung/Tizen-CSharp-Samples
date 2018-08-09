
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
using Xamarin.Forms;

using Calculator.Models;

namespace Calculator.Impl
{
    /// <summary>
    /// A Formatter class that provides formatted displayable text.</summary>
    public sealed class Formatter
    {
        private const double displayMax = 9999999999D;
        private static readonly Color literalColor = Color.White;
        private static readonly Color operatorColor = Color.Pink;

        /// <summary>
        /// The method provides formatted text for a number. 
        /// </summary>
        /// <param name="value"> A number value </param>
        /// <returns> A formatted number text </returns>
        private String GetNumberText(Double value)
        {
            if (Math.Abs(value) > displayMax)
            {
                return String.Format("{0:0.########E+000}", value);
            }

            return String.Format("{0:#,##0.##########}", value);
        }

        /// <summary>
        /// The method provides formatted expression text for given expression. 
        /// </summary>
        /// <param name="separatedExpression"> An expression consist of InputElement list </param>
        /// <returns> A formatted expression text </returns>
        public FormattedString GetFormattedExpressionText(IEnumerable<InputElement> separatedExpression)
        {
            var result = new FormattedString();
            int oneLineLengthCount = 0;

            for (int i = 0; i < separatedExpression.Count(); i++)
            {
                InputElement s = separatedExpression.ElementAt(i);
                string element = s.GetDisplayElement;

                if (s is Literal)
                {
                    double value;
                    string displayNumberText = element;
                    if (Double.TryParse(element, out value))
                    {
                        displayNumberText = GetNumberText(value);
                    }

                    result.Spans.Add(new Span
                    {
                        Text = displayNumberText,
                        ForegroundColor = literalColor,
                    });
                }
                else if (s is Reverse)
                {
                    result.Spans.Add(new Span
                    {
                        Text = element,
                        ForegroundColor = literalColor,
                    });
                }
                else if (s is Models.Point && i < (separatedExpression.Count() - 1))
                {
                    if (separatedExpression.ElementAt(i + 1) is Literal)
                    {
                        string last = result.Spans.Last().Text;
                        string stringValue = last;
                        string next = separatedExpression.ElementAt(i + 1);
                        stringValue = last + "." + next;
                        double value;

                        if (Double.TryParse(stringValue, out value))
                        {
                            result.Spans.RemoveAt(result.Spans.Count - 1);
                            result.Spans.Add(new Span
                            {
                                Text = stringValue,
                                ForegroundColor = literalColor,
                            });
                            i += 1;
                        }
                    }
                }
                else
                {
                    result.Spans.Add(new Span
                    {
                        Text = element,
                        ForegroundColor = operatorColor,
                    });

                }


                oneLineLengthCount += result.Spans.Last().Text.Length;
            }

            return result;
        }

        /// <summary>
        /// The method provides formatted text for the calculated result. </summary>
        /// <param name="outputText"> A result text </param>
        /// <returns> A formatted result text </returns>
        public FormattedString GetFormattedOutputText(String outputText)
        {
            string displayNumber;

            double value;
            if (Double.TryParse(outputText, out value) == false)
            {
                return new FormattedString();
            }

            // Exceptional Case : 0.00000 => 0.00000 (0), 0.0 (X)
            if (value == 0 &&
                outputText.Length > 1)
            {
                displayNumber = outputText;
            }

            else
            {
                displayNumber = "=" + GetNumberText(value);
            }

            return new FormattedString()
            {
                Spans =
                {
                    new Span
                    {
                        Text = displayNumber,
                    },
                }
            };
        }
    }
}
