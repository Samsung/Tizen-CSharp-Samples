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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFCSampleApp.Tizen.Mobile;
using Tizen.Network.Nfc;

[assembly: Xamarin.Forms.Dependency(implementorType: typeof(NFCImplementation))]

namespace NFCSampleApp.Tizen.Mobile
{
    /// <summary>
    /// The NFC implementation class in mobile profile
    /// </summary>
    class NFCImplementation : INfcImplementation
    {
        /// <summary>
        /// Adapter of NFC Tag operation
        /// </summary>
        NfcTagAdapter NfcTagAdapter;

        /// <summary>
        /// Adapter of NFC P2p operation
        /// </summary>
        NfcP2pAdapter NfcP2pAdapter;

        /// <summary>
        /// Checks if NFC is supported
        /// </summary>
        /// <returns>Returns true if NFC is supported, false otherwise</returns>
        public bool IsSupported()
        {
            try
            {
                return NfcManager.IsSupported;
            }
            catch (Exception e)
            {
                LogImplementation.DLog("Error : " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Initialize to enable NFC Tag operation.
        /// </summary>
        /// <returns>Returns true if initialize succeeded, false otherwise</returns>
        public bool TagInit()
        {
            try
            {
                NfcTagAdapter = NfcManager.GetTagAdapter();
                return true;
            }
            catch (Exception e)
            {
                LogImplementation.DLog("Failed initialize Tag " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Retrieves NFC Tag information.
        /// </summary>
        /// <param name="type">The Tag type</param>
        /// <param name="support">Whether or not to support NDEF messages</param>
        /// <param name="size">Stored NDEF message size in Tag</param>
        /// <param name="maxSize">Maximum NDEF message size that can be stored in Tag</param>
        /// <returns>Returns true if tag type was successfully obtained, false otherwise</returns>
        public bool GetTagType(ref string type, ref bool support, ref uint size, ref uint maxSize)
        {
            try
            {
                NfcTag nfcTag = NfcTagAdapter.GetConnectedTag();
                type = nfcTag.Type.ToString();
                support = nfcTag.IsSupportNdef;
                size = nfcTag.NdefSize;
                maxSize = nfcTag.MaximumNdefSize;
                return true;
            }
            catch (Exception e)
            {
                LogImplementation.DLog("Failed get Tag Type " + e.Message);
                return false;
            }
            
        }

        /// <summary>
        /// Initialize to enable NFC P2p operation.
        /// </summary>
        /// <returns>Returns true if initialize succeeded, false otherwise</returns>
        public bool P2p_init()
        {
            try
            {
                NfcP2pAdapter = NfcManager.GetP2pAdapter();
                return true;
            }
            catch (Exception e)
            {
                LogImplementation.DLog("Failed initialize P2P " + e.Message);
                return false;
            }
        }

        NfcP2p nfcP2P;

        /// <summary>
        /// Retrieves NFC P2p remote device.
        /// </summary>
        /// <returns>Returns true if initialize succeeded, false otherwise</returns>
        public bool GetP2p()
        {
            try
            {
                nfcP2P = NfcP2pAdapter.GetConnectedTarget();
                return true;
            }
            catch (Exception e)
            {
                LogImplementation.DLog("Failed get p2p device " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Send an NDEF message to the remote device
        /// </summary>
        /// <param name="type">The type of NDEF Record</param>
        /// <param name="ID">The ID of NDEF Record</param>
        /// <param name="payload">The payload of NDEF Record</param>
        /// <param name="payloadLength">The length of payload</param>
        /// <returns>Returns true if initialize succeeded, false otherwise</returns>
        public bool SendNDEF(byte[] type, byte[] ID, byte[] payload, uint payloadLength)
        {
            NfcNdefRecord NfcNdefRecord = new NfcNdefRecord(NfcRecordTypeNameFormat.WellKnown, type, ID, payload, payloadLength);
            NfcNdefMessage NfcNdefMessage = new NfcNdefMessage();
            try
            {
                NfcNdefMessage.AppendRecord(NfcNdefRecord);                
            }
            catch (Exception e)
            {
                LogImplementation.DLog("Failed append record " + e.Message);
                return false;
            }

            try
            {
                nfcP2P.SendNdefMessageAsync(NfcNdefMessage);
                return true;
            }
            catch (Exception e)
            {
                LogImplementation.DLog("Failed send message " + e.Message);
                return false;
            }
        }

        public int record_count = 0;
        public Action<int> callback;

        /// <summary>
        /// Register callback to read NDEF message
        /// </summary>
        /// <param name="receiveCallback">Callbacks to be called when an NDEF message is received</param>
        /// <returns>Returns true if callback register succeeded, false otherwise</returns>
        public bool ReceiveNDEF(Action<int> receiveCallback)
        {
            callback = receiveCallback;
            try
            {
                nfcP2P.P2pDataReceived += Received;
                return true;
            }
            catch (Exception e)
            {
                LogImplementation.DLog("Failed receive message " + e.Message);
                return false;
            }            
        }

        /// <summary>
        /// Callback function to be called when the NDEF message is received
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Argument of Event</param>
        void Received(object sender, P2pDataReceivedEventArgs e)
        {
            try
            {
                NfcNdefRecord record = e.NdefMessage.GetRecord(0);
            }
            catch (Exception exc)
            {
                LogImplementation.DLog("Failed getting record " + exc.Message);
            }
            
            record_count = e.NdefMessage.RecordCount;
                LogImplementation.DLog("Record Count " + e.NdefMessage.RecordCount);

            callback(record_count);
        }
    }
}