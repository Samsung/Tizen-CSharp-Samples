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
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Calculator.Impl;
using Calculator.Models;
using System.Collections.Generic;

namespace CalculatorImplUnitTest
{
    [TestClass]
    public class CalculatorImplTests
    {
        private CalculatorImpl ci;
        public CalculatorImplTests()
        {
            ci = new CalculatorImpl();
        }

        [TestMethod]
        public void TestBasicOperations()
        {
            Dictionary<String, Double> tc = new Dictionary<String, Double>
                {
                    {"1 + 3", 4 },
                    {"1 - 3", -2 },
                    {"3 * 3", 9 },
                    {"3 / 3", 1 },
                    {"9 / 3", 3 },

                    {"1.3 + 3.3", 4.6 },
                    {"1.3 - 3.1", -1.8 },
                    {"3.1 * 3.3", 10.23 },
                    {"3.3 / 3", 1.1 },
                    {"9.3 / 3", 3.1 },

                    {"2 + 3 - 5 * 5 + 6 / 2", -17 },
                    {"23 * 5 / 2 + 4 * 6", 81.5 },
                    {"45 + 3 * 5 - 2 + 5 / 2 * 7", 75.5 },
                    {"( 1 + 2 ) * ( 3 + 4 )", 21 },
                    {"( 1 + 2 ) * 3", 9 },
                    {"1 + 2 * 3", 7 },
                    {"( 2 * ( 3 + 6 / 2 ) + 2 ) * 4 / 2", 28 },
                    {"10 + 3 * 5 / ( 16 - 4 )", 11.25 },
                    {"3 + 8 / 2 * 4 - 20 / 5 + ( ( 2 + 16 / 4 ) / ( 2 + 3 ) )", 16.2 },
                };

            foreach (var item in tc)
            {
                if ((ci.SetExpression(item.Key) is CalculateSuccess) == false)
                {
                    Assert.Fail();
                }

                Assert.AreEqual(item.Value, ci.Result, "Expression : " + item.Key);
            }
        }

        [TestMethod]
        public void TestPercentageOperations()
        {
            Dictionary<String, Double> tc = new Dictionary<String, Double>
                {
                    {"3 %", 0.03 },
                    {"3 % + 3 %", 0.0309 },
                    {"3 % - 3 %", 0.0291 },
                    {"3 % * 3 %", 0.0009 },
                    {"3 % / 3 %", 1 },
                };

            foreach (var item in tc)
            {
                if ((ci.SetExpression(item.Key) is CalculateSuccess) == false)
                {
                    Assert.Fail();
                }

                Assert.AreEqual(item.Value, ci.Result, "Expression : " + item.Key);
            }
        }

        [TestMethod]
        public void TestDevideByZeroException()
        {
            Dictionary<String, Double> tc = new Dictionary<String, Double>
                {
                    {"0 / 0", 0 },
                    {"4 + 4 / 0 + 4", 0 },
                    {"4 * 4 / 0 - 4", 0 },
                };

            foreach (var item in tc)
            {
                if (ci.SetExpression(item.Key) is CantDivideByZero)
                {
                    continue;
                }

                Assert.Fail("Expression : " + item.Key);
            }
        }

        [TestMethod]
        public void TestInvalidFormatException()
        {
            Dictionary<String, Double> tc = new Dictionary<String, Double>
                {
                    {"2 + 4 * 5 # 6", 0 },
                    {"2 ! 4 % 5 % 6", 0 },
                    {"1 /", 0 },
                    {"2 + ( 1 / )", 0 },
                };

            foreach (var item in tc)
            {
                if (ci.SetExpression(item.Key) is CantCalculateInvalidFormat)
                {
                    continue;
                }

                Assert.Fail("Expression : " + item.Key);
            }
        }

        [TestMethod]
        public void TestBinaryOperators()
        {
            Dictionary<String, Double> tc = new Dictionary<String, Double>
                {
                    {"1 + R 2", -1 },
                    {"1 + 2 R", -1 },
                    {"R 3 + R 3", -6 },
                    {"R 3 + 3 R", -6 },
                    {"3 R + 3 R", -6 },
                    {"3 % + 3 %", 0.0309 },
                };

            foreach (var item in tc)
            {
                if ((ci.SetExpression(item.Key) is CalculateSuccess) == false)
                {
                    Assert.Fail();
                }

                Assert.AreEqual(item.Value, ci.Result, "Expression : " + item.Key);
            }
        }

        [TestMethod]
        public void TestLandscapeAdditionalOperators()
        {
            Dictionary<String, Double> tc = new Dictionary<String, Double>
                {
                    {"1 F", 1},
                    {"3 F", 6},
                    {"10 F", 3628800},
                    {"-1 F", -1},
                    {"-3 F", -6},
                    {"-10 F", -3628800},

                    {"Q 9", 3},
                    {"Q 25.1", Math.Sqrt(25.1) },
                    {"Q 1.1", Math.Sqrt(1.1) },

                    // TODO : deg
                    {"S 0", Math.Sin(0.0) },
                    {"S " + (Math.PI / 2), Math.Sin(Math.PI / 2) },
                    {"S " + (Math.PI / 3 * 2), Math.Sin(Math.PI / 3 * 2) },
                    {"S " + (Math.PI * 2), Math.Sin(Math.PI * 2) },

                    // TODO : deg
                    {"C 0", Math.Cos(0.0) },
                    {"C " + (Math.PI / 2), Math.Cos(Math.PI / 2) },
                    {"C " + (Math.PI / 3 * 2), Math.Cos(Math.PI / 3 * 2) },
                    {"C " + (Math.PI * 2), Math.Cos(Math.PI * 2) },

                    // TODO : deg
                    //{"T 0", Math.Tan(0.0)},
                    //{"T " + (Math.PI / 2), Math.Tan(Math.PI / 2)},
                    {"T " + (Math.PI / 3 * 2), Math.Tan(Math.PI / 3 * 2) },
                    {"T " + (Math.PI * 2), Math.Tan(Math.PI * 2) },

                    {"N 1", Math.Log(1) },
                    {"N 10", Math.Log(10) },
                    {"N 999", Math.Log(999) },

                    {"G 1", Math.Log10(1) },
                    {"G 10", Math.Log10(10) },
                    {"G 999", Math.Log10(999) },

                    {"O 1", 1 / 1.0},
                    {"O 3", 1 / 3.0},
                    {"O 100", 1 / 100.0},
                    {"O 1000000", 1 / 1000000.0},

                    {"1 U", 1},
                    {"2 U", Math.Pow(2,2) },
                    {"345 U", Math.Pow(345,2) },

                    {"P 1", Math.Pow(10,1) },
                    {"P 3", Math.Pow(10,3) },
                    {"P 34", Math.Pow(10,34) },

                    {"1 W 34", Math.Pow(1,34) },
                    {"1 W 3", Math.Pow(1,3) },
                    {"10 W 10", Math.Pow(10,10) },

                    {"A 0", Math.Abs(0) },
                    {"A -3", Math.Abs(-3) },
                    {"A -1.2", Math.Abs(-1.2) },

                    {"I", 3.14159265358979 },
                    {"I * 3", 3.14159265358979 * 3},
                    {"3 * I", 3.14159265358979 * 3},

                    {"E", 2.71828182845905},
                    {"E * 3", 2.71828182845905 * 3},
                    {"3 * E", 2.71828182845905 * 3},
                };

            foreach (var item in tc)
            {
                if ((ci.SetExpression(item.Key) is CalculateSuccess) == false)
                {
                    Assert.Fail();
                }

                Assert.AreEqual(item.Value, ci.Result, 0.00001, "Expression : " + item.Key);
            }
        }

        [TestMethod]
        public void TestDegreeRadianCases()
        {
            Operators.IsDegreeMetricUsed = false;

            Dictionary<String, Double> tc = new Dictionary<String, Double>
            {
                {"S 0", Math.Sin(0.0) },
                {"S " + (Math.PI / 2), Math.Sin(Math.PI / 2) },
                {"S " + (Math.PI / 3 * 2), Math.Sin(Math.PI / 3 * 2) },
                {"S " + (Math.PI * 2), Math.Sin(Math.PI * 2) },

                {"C 0", Math.Cos(0.0) },
                {"C " + (Math.PI / 2), Math.Cos(Math.PI / 2) },
                {"C " + (Math.PI / 3 * 2), Math.Cos(Math.PI / 3 * 2) },
                {"C " + (Math.PI * 2), Math.Cos(Math.PI * 2) },
                {"T 0", 0},
            };

            foreach (var item in tc)
            {
                if ((ci.SetExpression(item.Key) is CalculateSuccess) == false)
                {
                    Assert.Fail();
                }

                Assert.AreEqual(item.Value, ci.Result, 0.00001, "Expression : " + item.Key);
            }

            Operators.IsDegreeMetricUsed = true;

            // Deg
            tc = new Dictionary<String, Double>
            {
                {"S 0", Math.Sin(0.0) },
                {"S " + (Math.PI / 2), Math.Sin(Math.PI / 2 * Math.PI / 180) },
                {"S " + (Math.PI / 3 * 2), Math.Sin(Math.PI / 3 * 2 * Math.PI / 180) },
                {"S " + (Math.PI * 2), Math.Sin(Math.PI * 2 * Math.PI / 180) },

                {"C 0", Math.Cos(0.0) },
                {"C " + (Math.PI / 2), Math.Cos(Math.PI / 2 * Math.PI / 180) },
                {"C " + (Math.PI / 3 * 2), Math.Cos(Math.PI / 3 * 2 * Math.PI / 180) },
                {"C " + (Math.PI * 2), Math.Cos(Math.PI * 2 * Math.PI / 180) },

                {"T 0", Math.Tan(0.0) },
                {"T " + (Math.PI / 2), Math.Tan(Math.PI / 2 * Math.PI / 180) },
                {"T " + (Math.PI / 3 * 2), Math.Tan(Math.PI / 3 * 2 * Math.PI / 180) },
                {"T " + (Math.PI * 2), Math.Tan(Math.PI * 2 * Math.PI / 180) },
            };

            foreach (var item in tc)
            {
                if ((ci.SetExpression(item.Key) is CalculateSuccess) == false)
                {
                    Assert.Fail();
                }

                Assert.AreEqual(item.Value, ci.Result, 0.00001, "Expression : " + item.Key);
            }
        }

        [TestMethod]
        public void TestDegreeRadianErrorCase()
        {
            Operators.IsDegreeMetricUsed = false;

            Dictionary<String, Double> tc = new Dictionary<String, Double>
            {
                {"T " + (Math.PI / 2), Math.Tan(Math.PI / 2) },
            };

            foreach (var item in tc)
            {
                if (ci.SetExpression(item.Key) is CalculateSuccess)
                {
                    Assert.Fail();
                }
            }

        }

        [TestMethod]
        public void TestLandscapeAdditionalOperatorsException()
        {
            Dictionary<String, Double> tc = new Dictionary<String, Double>
            {
                {"1.4 F", 0},
                {"171 F", 0},
            };

            foreach (var item in tc)
            {
                if ((ci.SetExpression(item.Key) is CalculateSuccess) == false)
                {
                    continue;
                }

                Assert.Fail();
            }
        }
    }
}
