/* 
  * Copyright (c) 2016 Samsung Electronics Co., Ltd 
  * 
  * Licensed under the Flora License, Version 1.1 (the "License"); 
  * you may not use this file except in compliance with the License. 
  * You may obtain a copy of the License at 
  * 
  * http://www.apache.org/licenses/LICENSE-2.0 
  * 
  * Unless required by applicable law or agreed to in writing, software 
  * distributed under the License is distributed on an "AS IS" BASIS, 
  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
  * See the License for the specific language governing permissions and 
  * limitations under the License. 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCardSampleApp
{
    /// <summary>
    /// Interface to use Tizen.Network.Smartcard namespace
    /// </summary>
    public interface ISmartCardImplementation
    {
        /// <summary>
        /// Check if the Secure element is present
        /// </summary>
        /// <returns>Returns true if secure element is present, false otherwise</returns>
        bool IsSecureElementPresent();
        /// <summary>
        /// Gets the list of readers
        /// </summary>
        /// <returns>list of readers</returns>
        List<string> GetReadersList();
        /// <summary>
        /// Initialize to use Smart card namespace
        /// </summary>
        /// <returns>Returns true if Initialize succeeded, false otherwise</returns>
        bool Initialize();
        /// <summary>
        /// Sends the command to SE
        /// </summary>
        /// <param name="readerName">The name of reader</param>
        /// <param name="cmd">The command</param>
        /// <returns>SE's Response to Command</returns>
        String SendCommand(String readerName, String cmd);
    }
}
