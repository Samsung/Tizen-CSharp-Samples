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

namespace NFCSampleApp
{
    /// <summary>
    /// Interface to use Tizen.Network.Nfc namespace
    /// </summary>
    public interface INfcImplementation
    {
        /// <summary>
        /// Checks if NFC is supported
        /// </summary>
        /// <returns>Returns true if NFC is supported, false otherwise</returns>
        bool IsSupported();
        
        /// <summary>
        /// Initialize to enable NFC Tag operation
        /// </summary>
        /// <returns>Returns true if initialize succeeded, false otherwise</returns>
        bool TagInit();

        /// <summary>
        /// Retrieves NFC Tag information
        /// </summary>
        /// <param name="type">The Tag type</param>
        /// <param name="support">Whether or not to support NDEF messages</param>
        /// <param name="size">Stored NDEF message size in Tag</param>
        /// <param name="maxSize">Maximum NDEF message size that can be stored in Tag</param>
        /// <returns>Returns true if tag type was successfully obtained, false otherwise</returns>
        bool GetTagType(ref string type, ref bool support, ref uint size, ref uint maxSize);

        /// <summary>
        /// Initialize to enable NFC P2p operation
        /// </summary>
        /// <returns>Returns true if initialize succeeded, false otherwise</returns>
        bool P2p_init();

        /// <summary>
        /// Retrieves NFC P2p remote device
        /// </summary>
        /// <returns>Returns true if initialize succeeded, false otherwise</returns>
        bool GetP2p();

        /// <summary>
        /// Send an NDEF message to the remote device
        /// </summary>
        /// <param name="type">The type of NDEF Record</param>
        /// <param name="ID">The ID of NDEF Record</param>
        /// <param name="payload">The payload of NDEF Record</param>
        /// <param name="payloadLength">The length of payload</param>
        /// <returns>Returns true if initialize succeeded, false otherwise</returns>
        bool SendNDEF(byte[] type, byte[] ID, byte[] payload, uint payloadLength);

        /// <summary>
        /// Register callback to read NDEF message
        /// </summary>
        /// <param name="receiveCallback">Callbacks to be called when an NDEF message is received</param>
        /// <returns>Returns true if callback register succeeded, false otherwise</returns>
        bool ReceiveNDEF(Action<int> receiveCallback);
    }
}
