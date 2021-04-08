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
using SmartCardSampleApp.Tizen.Mobile;
using Tizen.Network.Smartcard;

[assembly: Xamarin.Forms.Dependency(implementorType: typeof(SmartCardImplementation))]

namespace SmartCardSampleApp.Tizen.Mobile
{
    /// <summary>
    /// The Smart card implementation class in mobile profile
    /// </summary>
    class SmartCardImplementation : ISmartCardImplementation
    {
        IEnumerable<SmartcardReader> SmartcardReader;

        /// <summary>
        /// Initialize to use Smart card namespace
        /// </summary>
        /// <returns>Returns true if Initialize succeeded, false otherwise</returns>
        public bool Initialize()
        {
            try
            {
                SmartcardReader = SmartcardManager.GetReaders();
                LogImplementation.DLog("Count is " + SmartcardReader.Count().ToString());
                if (SmartcardReader.Count() > 0)
                {
                    foreach (SmartcardReader readers in SmartcardReader)
                    {
                        LogImplementation.DLog("Reader " + readers.Name + " Is Secure " + readers.IsSecureElementPresent);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                LogImplementation.DLog("Failed " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Check if the Secure element is present
        /// </summary>
        /// <returns>Returns true if secure element is present, false otherwise</returns>
        public bool IsSecureElementPresent()
        {
            bool is_present = false;

            if (SmartcardReader.Count() > 0)
            {
                foreach (SmartcardReader readers in SmartcardReader)
                {
                    if (readers.IsSecureElementPresent)
                    {
                        is_present = true;
                    }
                }
            }

            return is_present;
        }

        /// <summary>
        /// Gets the list of readers
        /// </summary>
        /// <returns>list of readers</returns>
        public List<String> GetReadersList()
        {
            List<String> readersList = new List<String>();

            if (SmartcardReader.Count() > 0)
            {
                foreach (SmartcardReader readers in SmartcardReader)
                {
                    if (readers.IsSecureElementPresent)
                    {
                        readersList.Add(readers.Name);
                    }
                }
            }

            return readersList;
        }

        /// <summary>
        /// Sends the command to SE
        /// </summary>
        /// <param name="readerName">The name of reader</param>
        /// <param name="cmd">The command</param>
        /// <returns>SE's Response to Command</returns>
        public String SendCommand(String readerName, String cmd)
        {
            byte[] atrList = null;
            byte[] aid = {0xA0, 0x00, 0x00, 0x00, 0x63, 0x50, 0x4B, 0x43, 0x53, 0x2D, 0x31, 0x35};
            
            if (String.IsNullOrEmpty(cmd)) {
                LogImplementation.DLog("cmd should not be null or empty");
                return "6800";
            }

            if ((cmd.Length % 2) != 0)
            {
                LogImplementation.DLog("cmd should be even number");
                return "6800";
            }
            
            byte[] cmdBytes = new byte[cmd.Length / 2];
            try {
                for (int index = 0; index < cmdBytes.Length; index++)
                {
                    string byteValue = cmd.Substring(index * 2, 2);

                    cmdBytes[index] = byte.Parse(byteValue, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            catch (Exception e)
            {
                LogImplementation.DLog("Failed " + e.Message);
                return "6800";
            }

            if (SmartcardReader.Count() > 0)
            {
                foreach (SmartcardReader readers in SmartcardReader)
                {
                    if (readers.Name == readerName)
                    {
                        SmartcardSession session = readers.OpenSession();
                        SmartcardChannel channel = session.OpenBasicChannel(aid, 0x00);
                        atrList = channel.Transmit(cmdBytes);
                    }
                }
            }

            LogImplementation.DLog("CMD " + BitConverter.ToString(cmdBytes).Replace("-", string.Empty) + " Return " + BitConverter.ToString(atrList).Replace("-", string.Empty));
            return BitConverter.ToString(atrList).Replace("-", string.Empty);
        }
    }
}