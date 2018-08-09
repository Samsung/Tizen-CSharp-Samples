
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

namespace Calculator.Models
{
    /// <summary>
    /// CalculateResult base class to notify the calculate result. </summary>
    public class CalculationResult
    {
        /// <summary>
        /// A message will be displayed in error pop up . </summary>
        public virtual String Message
        {
            get { return string.Empty; }
        }
    }

    /// <summary>
    /// A CalculateResult class describes calculating is completed without error. </summary>
    public class CalculationSuccessful : CalculationResult
    {
    }

    /// <summary>
    /// A CalculateFailed class describes calculating is failed. </summary>
    public class CalculationFailed : CalculationResult
    {
    }

    /// <summary>
    /// A CalculateResult class describes calculating is failed
    /// due to invalid expression format. </summary>
    public class CantCalculateInvalidFormat : CalculationResult
    {
        /// <summary>
        /// A message will be displayed in error pop up . </summary>
        public override String Message
        {
            get { return "Invalid format used."; }
        }
    }

    /// <summary>
    /// A CalculateResult class describes calculating is failed
    /// due to big number. </summary>
    public class CantCalculateTooBigNumber : CalculationResult
    {
        /// <summary>
        /// A message will be displayed in error pop up . </summary>
        public override String Message
        {
            get { return "Couldn't display entire result. Result too long."; }
        }
    }

    /// <summary>
    /// A CalculateResult class describes calculating is failed
    /// due to dividing by zero. </summary>
    public class CantDivideByZero : CalculationResult
    {
        /// <summary>
        /// A message will be displayed in error pop up . </summary>
        public override String Message
        {
            get { return "Can't divide by zero."; }
        }
    }
}
