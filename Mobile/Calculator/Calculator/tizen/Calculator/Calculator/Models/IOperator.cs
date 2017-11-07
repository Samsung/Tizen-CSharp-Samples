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

namespace Calculator.Models
{
    /// <summary>
    /// A operand placing type of the operator
    /// LEFT : Operand is placed at Left side only
    /// RIGHT : Operand is placed at Right side only
    /// BOTH : Operand is placed at Both side
    /// NONE : Operand is placed at Both side
    /// </summary>
    public enum OperandType
    {
        LEFT,   // Operand is placed at Left side only
        RIGHT,  // Operand is placed at Right side only
        BOTH,   // Operand is placed at Both side
        NONE,   // Operand is placed at Both side
    };

    /// <summary>
    /// A interface for the Operator</summary>
    public interface IOperator
    {
        /// <summary>
        /// A operator priority </summary>
        int Priority { get; }

        /// <summary>
        /// A operator's operand type </summary>
        /// <seealso cref="OperandType">
        /// A operand type. </seealso>
        ///
        OperandType OperandType { get; }

        /// <summary>
        /// A method provides result of operator </summary>
        /// <param name="left"> A left side element. </param>
        /// <param name="right"> A right side element. </param>
        /// <param name="result"> A operator's calculation result. </param>
        void GetResult(double left, double right, out double result);
    }
}
