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

using Calculator.Models;
using Calculator.Tizen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Impl
{
    internal struct Span
    {
        public Span()
        {
            Text = "";
            ForegroundColor = "";
            FontSize = 0;
        }

        public string Text;
        public string ForegroundColor;
        public int FontSize;
    }

    internal struct FormattedString
    {
        public FormattedString()
        {
        }

        public List<Span> Spans = new List<Span>();

        public override string ToString()
        {
            string result = "";
            foreach (Span span in Spans)
            {
                result += "<span";
                if (span.FontSize != 0)
                {
                    result += $" font-size='{span.FontSize}'";
                }
                if (!string.IsNullOrEmpty(span.ForegroundColor))
                {
                    result += $" text-color='{span.ForegroundColor}'";
                }
                result += $">{span.Text}</span>";
            }
            return result;
        }
    };

    /// <summary>
    /// A Formatter class that provides formatted displayable text.</summary>
    public sealed class Formatter
    {
        private const double displayMax = 999999999999999D;

        // color format, please refer to https://docs.tizen.org/application/dotnet/guides/user-interface/nui/text/#use-markup-to-style-textlabel
        private static readonly string literalColor = "#FF000000"; // originally "#000000"
        private static readonly string operatorColor = "#AD7F4E61"; // originally "#7F4E61AD"

        private readonly int[] maxLengthOfOneLine = new int[] { 50, 10 };
        /// <summary>
        /// Expression text size for Portrait and Landscape </summary>
        private readonly int[,] expressionTextSize = new int[,] { { 18, 28 }, { 20, 30 }, { 22, 45 } };
        /// <summary>
        /// Result text size for Portrait and Landscape </summary>
        private readonly int[] resultTextSize = new int[] { 28, 34 };

        /// <summary>
        /// A flag value indicates current device orientation. </summary>
        public bool IsLandscapeOrientation { get; set; }

        public Formatter()
        {
        }

        /// <summary>
        /// A method provides formatted text for a number. </summary>
        /// <param name="value"> A number value </param>
        /// <returns> A formatted number text </returns>
        private string GetNumberText(double value)
        {
            if (Math.Abs(value) > displayMax)
            {
                return string.Format("{0:0.########E+000}", value);
            }

            return string.Format("{0:#,##0.##########}", value);
        }

        /// <summary>
        /// A method provides formatted expression text for given expression. </summary>
        /// <param name="separatedExpression"> A expression consist of InputElement list </param>
        /// <returns> A formatted expression text </returns>
        public string GetFormattedExpressionText(IEnumerable<InputElement> separatedExpression)
        {
            FormattedString result = new FormattedString();
            int oneLineLengthCount = 0;

            for (int i = 0; i < separatedExpression.Count(); i++)
            {
                InputElement s = separatedExpression.ElementAt(i);
                string element = s.GetDisplayElement;

                if (s is Literal)
                {
                    double value;
                    string displayNumberText = element;
                    if (double.TryParse(element, out value))
                    {
                        displayNumberText = GetNumberText(value);
                    }

                    result.Spans.Add(new Span
                    {
                        Text = displayNumberText,
                        ForegroundColor = literalColor,
                    });
                }
                else
                {
                    int maxLineLength = (IsLandscapeOrientation) ? maxLengthOfOneLine[0] : maxLengthOfOneLine[1];
                    if (oneLineLengthCount >= maxLineLength)
                    {
                        oneLineLengthCount = 0;
                        result.Spans.Add(new Span
                        {
                            Text = "\n",
                        });
                    }

                    if (s is Reverse)
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

                            if (double.TryParse(stringValue, out value))
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
                }

                oneLineLengthCount += result.Spans.Last().Text.Length;
            }

            int TextLength = result.ToString().Length;
            if (TextLength >= 18)
            {
                foreach (Span item in result.Spans)
                {
                    var span = item;
                    span.FontSize = IsLandscapeOrientation ? expressionTextSize[0, 0] : expressionTextSize[0, 1]; // Simple : 58, Scientific : 38
                }
            }
            else if (TextLength >= 15)
            {
                foreach (Span item in result.Spans)
                {
                    var span = item;
                    span.FontSize = IsLandscapeOrientation ? expressionTextSize[1, 0] : expressionTextSize[1, 1]; // Simple : 68, Scientific : 48
                }
            }
            else
            {
                foreach (Span item in result.Spans)
                {
                    var span = item;
                    span.FontSize = (IsLandscapeOrientation) ? expressionTextSize[2, 0] : expressionTextSize[2, 1]; // Simple : 90, Scientific : 58
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// A method provides formatted text for the calculated result. </summary>
        /// <param name="outputText"> A result text </param>
        /// <returns> A formatted result text </returns>
        public string GetFormattedOutputText(String outputText)
        {
            string displayNumber;

            double value;
            if (double.TryParse(outputText, out value) == false)
            {
                return "";
            }

            // Exceptional Case : 0.00000 => 0.00000 (0), 0.0 (X)
            if (value == 0 && outputText.Length > 1)
            {
                displayNumber = outputText;
            }
            else
            {
                displayNumber = GetNumberText(value);
            }

            var formattedString = new FormattedString()
            {
                Spans =
                {
                    new Span
                    {
                        Text = displayNumber,
                        FontSize = (IsLandscapeOrientation) ? resultTextSize[0] : resultTextSize[1],
                    },
                }
            };

            return formattedString.ToString();
        }
    }
}
