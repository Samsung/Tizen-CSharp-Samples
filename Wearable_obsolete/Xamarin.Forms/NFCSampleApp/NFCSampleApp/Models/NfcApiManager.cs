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
using Tizen.Network.Nfc;

namespace NFCSampleApp
{
    /// <summary>
    /// The NFC API manager class.
    /// </summary>
    class NfcApiManager
    {
        /// <summary>
        /// Adapter of NFC Tag operation.
        /// </summary>
        private NfcTagAdapter tagAdapter;

        /// <summary>
        /// Adapter of NFC P2P operation.
        /// </summary>
        private NfcP2pAdapter p2pAdapter;

        /// <summary>
        /// Connected NFC P2P remote device.
        /// </summary>
        private NfcP2p p2pDevice;

        /// <summary>
        /// Counter of received records by P2P connection.
        /// </summary>
        private int record_count = 0;

        /// <summary>
        /// Action called after NDEF Message receive.
        /// </summary>
        private Action<int> messageReceiveCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="NfcApiManager"/> class.
        /// </summary>
        public NfcApiManager()
        {
            if (!IsNfcSupported())
            {
                return;
            }

            if (IsP2PSupported())
            {
                P2PInit();
            }

            if (IsTagSupported())
            {
                TagInit();
            }
        }

        /// <summary>
        /// Checks if NFC is supported.
        /// </summary>
        /// <returns>Returns true if NFC is supported, false otherwise</returns>
        public bool IsNfcSupported()
        {
            Tizen.System.Information.TryGetValue("http://tizen.org/feature/network.nfc", out bool isNfcSupported);
            return isNfcSupported;
        }

        /// <summary>
        /// Checks if NFC P2P is supported.
        /// </summary>
        /// <returns>Returns true if NFC P2P is supported, false otherwise</returns>
        public bool IsP2PSupported()
        {
            Tizen.System.Information.TryGetValue("http://tizen.org/feature/network.nfc.p2p", out bool isP2PSupported);
            return isP2PSupported;
        }

        /// <summary>
        /// Checks if NFC Tag is supported.
        /// </summary>
        /// <returns>Returns true if NFC Tag is supported, false otherwise</returns>
        public bool IsTagSupported()
        {
            Tizen.System.Information.TryGetValue("http://tizen.org/feature/network.nfc.tag", out bool isTagSupported);
            return isTagSupported;
        }

        /// <summary>
        /// Initializes Tag Adapter.
        /// </summary>
        /// <returns>Returns true if initialize succeeded, false otherwise</returns>
        public bool TagInit()
        {
            try
            {
                tagAdapter = NfcManager.GetTagAdapter();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed initialize Tag: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Retrieves NFC Tag information.
        /// </summary>
        /// <param name="type">The Tag type</param>
        /// <param name="support">Whether NDEF messages are supported or not</param>
        /// <param name="size">Stored NDEF message size in Tag</param>
        /// <param name="maxSize">Maximum NDEF message size that can be stored in Tag</param>
        /// <returns>Returns true if tag type was successfully obtained, false otherwise</returns>
        public bool GetTagInfo(ref string type, ref bool support, ref uint size, ref uint maxSize)
        {
            try
            {
                NfcTag nfcTag = tagAdapter.GetConnectedTag();
                type = nfcTag.Type.ToString();
                support = nfcTag.IsSupportNdef;
                size = nfcTag.NdefSize;
                maxSize = nfcTag.MaximumNdefSize;
                return true;
            }
            catch (Exception e)
            {
                Tizen.Log.Debug("NFC_APP", "Failed get Tag Type " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Initializes P2P adapter.
        /// </summary>
        /// <returns>Returns true if initialize succeeded, false otherwise</returns>
        public bool P2PInit()
        {
            try
            {
                p2pAdapter = NfcManager.GetP2pAdapter();
                return true;
            }
            catch (Exception e)
            {
                Tizen.Log.Debug("NFC_APP", "Failed initialize P2P " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Retrieves NFC P2P remote device.
        /// </summary>
        /// <returns>Returns true if initialize succeeded, false otherwise</returns>
        public bool GetP2P()
        {
            try
            {
                p2pDevice = p2pAdapter.GetConnectedTarget();
                return true;
            }
            catch (Exception e)
            {
                Tizen.Log.Debug("NFC_APP", "Failed get p2p device " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Sends an NDEF message to the remote device.
        /// </summary>
        /// <param name="type">The type of NDEF Record</param>
        /// <param name="ID">The ID of NDEF Record</param>
        /// <param name="payload">The payload of NDEF Record</param>
        /// <param name="payloadLength">The length of payload</param>
        /// <returns>Returns true if initialize succeeded, false otherwise</returns>
        public bool SendNdef(byte[] type, byte[] ID, byte[] payload)
        {
            NfcNdefRecord ndefRecord = new NfcNdefRecord(NfcRecordTypeNameFormat.WellKnown, type, ID, payload, (uint)payload.Length);
            NfcNdefMessage ndefMessage = new NfcNdefMessage();
            try
            {
                ndefMessage.AppendRecord(ndefRecord);
            }
            catch (Exception e)
            {
                Tizen.Log.Debug("NFC_APP", "Failed append record " + e.Message);
                return false;
            }

            try
            {
                p2pDevice.SendNdefMessageAsync(ndefMessage);
                return true;
            }
            catch (Exception e)
            {
                Tizen.Log.Debug("NFC_APP", "Failed send message " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Registers event handler to receive NDEF message and register receiveCallback as 
        /// Action called after receiving NDEF message.
        /// </summary>
        /// <param name="receiveCallback">Action called after NDEF message receive</param>
        /// <returns>Returns true if event handler register succeeded, false otherwise</returns>
        public bool ReceiveNdef(Action<int> receiveCallback)
        {
            messageReceiveCallback = receiveCallback;
            try
            {
                p2pDevice.P2pDataReceived += Received;
                return true;
            }
            catch (Exception e)
            {
                Tizen.Log.Debug("NFC_APP", "Failed receive message " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Event handler called when the NDEF message is received
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Argument of Event</param>
        private void Received(object sender, P2pDataReceivedEventArgs e)
        {
            try
            {
                NfcNdefRecord record = e.NdefMessage.GetRecord(0);
                record_count = e.NdefMessage.RecordCount;
                messageReceiveCallback(record_count);
            }
            catch (Exception exc)
            {
                Tizen.Log.Debug("NFC_APP", "Failed getting record " + exc.Message);
            }
        }
    }
}
