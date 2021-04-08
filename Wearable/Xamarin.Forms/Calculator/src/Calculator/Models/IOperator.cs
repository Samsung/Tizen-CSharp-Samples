
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

namespace Calculator.Models
{
    /// <summary>
    /// A operand placing type of the operator
    /// LEFT : Operand is placed on Left side only
    /// RIGHT : Operand is placed on Right side only
    /// BOTH : Operand is placed on Both side
    /// NONE : Operand is placed on Both side
    /// </summary>
    public enum OperandType
    {
        LEFT,   // Operand is placed on Left side only
        RIGHT,  // Operand is placed on Right side only
        BOTH,   // Operand is placed on Both side
        NONE,   // Operand is placed on Both side
    };

    /// <summary>
    /// An interface for the Operator</summary>
    public interface IOperator
    {
        /// <summary>
        /// Operator priority 
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// An operator's operand type 
        /// </summary>
        /// <seealso cref="OperandType">
        /// Operand type. 
        /// </seealso>
        OperandType OperandType { get; }

        /// <summary>
        /// The method provides result of operator 
        /// </summary>
        /// <param name="left"> Left side element. </param>
        /// <param name="right"> Light side element. </param>
        /// <param name="result"> Operator's calculation result. </param>
        void GetResult(double left, double right, out double result);
    }
}
