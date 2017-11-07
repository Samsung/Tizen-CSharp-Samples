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

using System.Collections.Generic;

using Calculator.Impl;
using Calculator.Models;

namespace CalculatorImplUnitTest
{
    [TestClass]
    public class InputParserTest
    {
        private InputParser ip;
        public InputParserTest()
        {
            ip = new InputParser();
        }

        [TestMethod]
        public void TestBasicOperators()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("-", out output);
            ip.GetseparatedPlainText("1", out output);

            expected = "1 + 1 - 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);

            expected = "1 . 1 + 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);

            expected = "1 . 11 + 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));


            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);

            expected = "1 + 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
        }

        [TestMethod]
        public void TestNullaryOperatorCombine()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();

            ip.Clear();
            ip.GetseparatedPlainText("I", out output);

            expected = "I";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("I", out output);

            expected = "3 * I";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("I", out output);
            ip.GetseparatedPlainText("3", out output);

            expected = "I * 3";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("E", out output);

            expected = "E";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("E", out output);
            ip.GetseparatedPlainText("3", out output);

            expected = "E * 3";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("E", out output);

            expected = "3 * E";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
        }

        [TestMethod]
        public void TestOperatorsCombination()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();

            // Empty - Literal
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);

            expected = "1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            // Empty - operand & operator (Right)
            ip.Clear();
            ip.GetseparatedPlainText("P", out output);

            expected = "P (";
            Assert.AreEqual<String>(expected, String.Join(" ", output));


            // Literal - Literal
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("1", out output);

            expected = "1111";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("9", out output);

            expected = "11 + 19";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            // Literal - operator (Left)/operator (Both)
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(")", out output);

            expected = "1 * ( 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            // Literal - operand & operator (Left)
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("%", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("%", out output);

            expected = "1 % + 1 %";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            // Literal - operand & operator (Left)
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("R", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "( -11 + 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            // operator (Left), operator (Right)

            // operator (Both) - operand & operator (Right)
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("-", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "1 + ( 1 - 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            // operator (Both) - nullary
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("E", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("I", out output);
            ip.GetseparatedPlainText("-", out output);
            ip.GetseparatedPlainText("1", out output);

            expected = "1 + E * 1 * I - 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            // operand & operator (Left) - Literal
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("%", out output);
            ip.GetseparatedPlainText("1", out output);

            expected = "1 % * 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            // operand & operator (Left) - nullary
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("%", out output);
            ip.GetseparatedPlainText("E", out output);

            expected = "1 % * E";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            // operand & operator (Left) - operand & operator (Left)/operand & operator (Both)
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("%", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);

            expected = "1 % + 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            // operand & operator (Left) - operand & operator (Right)
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("%", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(")", out output);

            expected = "1 % * ( 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            // operand & operator (Left) - nullary
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("%", out output);
            ip.GetseparatedPlainText("I", out output);
            ip.GetseparatedPlainText("1", out output);

            expected = "1 % * I * 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            // operand & operator (Right) - Literal
            ip.Clear();
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "( 1 + 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            // operand & operator (Right) - operand & operator (Right)
            ip.Clear();
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "( ( ( 1 + 1 ) ) )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "( 1 + 1 ) * ( ( 3 ) )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            // operand & operator (Right) - nullary
            ip.Clear();
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("I", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("I", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "( I ) * ( 1 + 1 ) * ( I )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
        }

        [TestMethod]
        public void TestOperatorsCombinationException()
        {
            IEnumerable<InputElement> output = new List<InputElement>();

            // empty - operand & operator (Left)
            ip.Clear();
            if (ip.GetseparatedPlainText("!", out output) is AddingPossible)
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void TestOperatorDuplication()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("-", out output);

            expected = "1 -";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("-", out output);
            ip.GetseparatedPlainText("*", out output);

            expected = "1 *";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("-", out output);
            ip.GetseparatedPlainText("/", out output);

            expected = "1 /";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("/", out output);
            ip.GetseparatedPlainText("+", out output);

            expected = "1 +";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("*", out output);
            ip.GetseparatedPlainText("-", out output);

            expected = "1 -";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
        }

        [TestMethod]
        public void TestBracketOpertor()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "1 + ( ( 3 ) )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "1 * ( 3 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("%", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "1 % * ( 3 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("%", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "( 1 % ) * ( 3 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("%", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "( 1 + 1 % ) * ( 3 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("*", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("%", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "( 1 * 1 % ) * 3 * ( 3 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("*", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("2", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("(", out output);


            expected = "1 * ( ( 2 ) ) * ( 3 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

        }

        [TestMethod]
        public void TestReverseOperators()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.ReverseSign(out output);

            expected = "( -1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.ReverseSign(out output);

            expected = "1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.ReverseSign(out output);

            expected = "( R";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.ReverseSign(out output);

            expected = "";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.ReverseSign(out output);

            expected = "1 + ( -1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.ReverseSign(out output);

            expected = "1 + 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
        }

        [TestMethod]
        public void TestCombinationOfBracketReverseOperators()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.ReverseSign(out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "1 * ( ( R 3 ) )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.ReverseSign(out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "1 * ( ( -3 ) )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.ReverseSign(out output);
            ip.ReverseSign(out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "1 * ( 3 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.ReverseSign(out output);
            ip.ReverseSign(out output);
            ip.ReverseSign(out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "1 * ( ( -3 ) )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("-1", out output);
            ip.ReverseSign(out output);

            expected = "1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.ReverseSign(out output);

            expected = "( -1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
            ip.ReverseSign(out output);

            expected = "1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.ReverseSign(out output);

            expected = "( -1 . 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.ReverseSign(out output);

            expected = "1 . 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("R", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.ReverseSign(out output);

            expected = "1 . 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.ReverseSign(out output);

            expected = "( -1 . 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("R", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.ReverseSign(out output);

            expected = "( 1 . 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.ReverseSign(out output);

            expected = "( ( -1 . 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("R", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.ReverseSign(out output);

            expected = "( 1 . 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.ReverseSign(out output);

            expected = "( ( -1 . 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("R", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.ReverseSign(out output);

            expected = "1 + 1 . 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.ReverseSign(out output);

            expected = "1 + ( -1 . 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("R", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.ReverseSign(out output);

            expected = "1 + 1 . 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.ReverseSign(out output);

            expected = "1 + ( -1 . 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
        }

        [TestMethod]
        public void TestCombinationOfBracketMinusOperators()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();
            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("R", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "1 * ( ( R 3 ) )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("R", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "1 * ( ( -3 ) )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("R", out output);
            ip.GetseparatedPlainText("R", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "1 * ( 3 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("R", out output);
            ip.GetseparatedPlainText("R", out output);
            ip.GetseparatedPlainText("R", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.GetseparatedPlainText("(", out output);

            expected = "1 * ( ( -3 ) )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
        }

        [TestMethod]
        public void TestEqual()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.Equal(out output);

            expected = "1 + 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.GetseparatedPlainText("1", out output);

            expected = "1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.GetseparatedPlainText("1", out output);

            expected = "11";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.GetseparatedPlainText("1", out output);

            expected = "111";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
        }

        [TestMethod]
        public void TestAdditionalBracketAdding()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();

            ip.Clear();
            ip.GetseparatedPlainText("S", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.Equal(out output);

            expected = "S ( 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("C", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.Equal(out output);

            expected = "C ( 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("T", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.Equal(out output);

            expected = "T ( 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("G", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.Equal(out output);

            expected = "G ( 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("N", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.Equal(out output);

            expected = "N ( 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("X", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.Equal(out output);

            expected = "X ( 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("A", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.Equal(out output);

            expected = "A ( 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("A", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.Equal(out output);

            expected = "A ( 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("A", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.Equal(out output);

            expected = "1 * A ( 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("W", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.Equal(out output);

            expected = "1 W ( 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("P", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.Equal(out output);

            expected = "1 * P ( 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("Q", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("(", out output);
            ip.Equal(out output);

            expected = "1 * Q ( 1 )";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
        }

        [TestMethod]
        public void TestRemovingOperatorwithBracket()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();

            ip.Clear();
            ip.GetseparatedPlainText("(", out output);

            expected = "(";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.DeleteLast(out output);
            expected = "";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("F", out output);

            expected = "1 F";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.DeleteLast(out output);
            expected = "1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("T", out output);

            expected = "1 * T (";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.DeleteLast(out output);
            expected = "1 *";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("Q", out output);

            expected = "1 * Q (";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.DeleteLast(out output);
            expected = "1 *";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("A", out output);

            expected = "1 * A (";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.DeleteLast(out output);
            expected = "1 *";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
        }

        [TestMethod]
        public void TestBinaryOperatorCase()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();

            ip.Clear();
            ip.GetseparatedPlainText("+", out output);
            expected = "";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("-", out output);
            expected = "";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("*", out output);
            expected = "";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("/", out output);
            expected = "";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("W", out output);
            expected = "";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
        }

        [TestMethod]
        public void TestDeleteLast()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("2", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.DeleteLast(out output);
            ip.DeleteLast(out output);
            ip.DeleteLast(out output);
            expected = "";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("2", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.DeleteLast(out output);
            ip.DeleteLast(out output);
            ip.DeleteLast(out output);
            ip.DeleteLast(out output);
            expected = "";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.DeleteLast(out output);
            ip.DeleteLast(out output);
            ip.DeleteLast(out output);
            expected = "";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("+", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.DeleteLast(out output);
            ip.DeleteLast(out output);
            ip.DeleteLast(out output);
            ip.DeleteLast(out output);
            expected = "";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
        }

        [TestMethod]
        public void TestExceptionalCase()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();

            ip.Clear();
            ip.GetseparatedPlainText(".", out output);
            expected = "0 .";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            expected = "1 .";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("1", out output);
            expected = "11 . 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            expected = "11 . 1";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("(", out output);
            expected = "1 * (";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("+", out output);
            expected = "1 +";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("E", out output);
            expected = "1 * E";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("F", out output);
            expected = "1 F";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
        }

        [TestMethod]
        public void TestNumberCombination()
        {
            string expected;
            IEnumerable<InputElement> output = new List<InputElement>();

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("2", out output);
            ip.GetseparatedPlainText("3", out output);
            expected = "123";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("2", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("1", out output);
            expected = "123 . 001";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("2", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("4", out output);
            ip.GetseparatedPlainText("5", out output);
            expected = "123 . 45";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            expected = "0 . 00000";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            expected = "0 . 0000000000";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText(".", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("1", out output);
            expected = "0 . 0000000000";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("2", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("4", out output);
            ip.GetseparatedPlainText("5", out output);
            ip.GetseparatedPlainText("6", out output);
            ip.GetseparatedPlainText("7", out output);
            ip.GetseparatedPlainText("8", out output);
            ip.GetseparatedPlainText("9", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("2", out output);
            ip.GetseparatedPlainText("3", out output);
            ip.GetseparatedPlainText("4", out output);
            ip.GetseparatedPlainText("5", out output);
            ip.GetseparatedPlainText("6", out output);
            ip.GetseparatedPlainText("7", out output);
            expected = "123456789012345";
            Assert.AreEqual<String>(expected, String.Join(" ", output));

            ip.Clear();
            ip.GetseparatedPlainText("1", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("0", out output);
            ip.GetseparatedPlainText("4", out output);
            ip.GetseparatedPlainText("5", out output);
            ip.GetseparatedPlainText("6", out output);
            ip.GetseparatedPlainText("7", out output);
            expected = "100000000000045";
            Assert.AreEqual<String>(expected, String.Join(" ", output));
        }
    }
}
