 /*
 *  Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 *  Contact: Ernest Borowski <e.borowski@partner.samsung.com>
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License
 *
 *
 * @file        Certificates.cs
 * @author      Ernest Borowski (e.borowski@partner.samsung.com)
 * @version     1.0
 * @brief       This file contains example usage of DataManager class.
 */

namespace SecureRepository.Tizen
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using global::Tizen;
    using global::Tizen.Security.SecureRepository;

    /// <summary>
    /// Class for storing / getting items from DataManager. Also has static method to validate filePath.
    /// </summary>
    public class Data : Secure<byte[]>
    {
        /// <summary>
        /// Checks if provided file path is valid.
        /// </summary>
        /// <param name="filePath">File path to data.</param>
        /// <returns>True if file path is valid, false if not or if occurred error.</returns>
        public static bool CheckPath(string filePath)
        {
            if (filePath.Length == 0)
            {
                throw new System.ArgumentException("filePath lenght = 0");
            }

            // Checks if path does not contain invalid path characters.
            if (filePath.IndexOfAny(Path.GetInvalidPathChars()) == -1)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    if (fileInfo.Length == -1)
                    {
                        throw new System.ArgumentException("file does not exist or it is a directory");
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    // e.g. File IO exception.
                    Log.Error("SecureRepository_DATA", ex.Message);
                }
            }
            else
            {
                Log.Error("SecureRepository_DATA", "filePath contains invalid path characters");
            }

            return false;
        }

        #region Implement Interface

        /// <summary>
        /// Gets Data saved under specified alias from DataManager.
        /// </summary>
        /// <param name="alias">The name of a Data.</param>
        /// <param name="password">The password used to decrypt a Data. If password is provided when adding a Data to DataManager, the same password should be passed here.</param>
        /// <returns>Data if exists, else null.</returns>
        public override byte[] Get(string alias, string password = null)
        {
            byte[] data = null;
            if (this.Exists(alias, password))
            {
                try
                {
                    data = DataManager.Get(alias, password);
                    return data;
                }
                catch (Exception ex)
                {
                    Log.Error("SecureRepository_DATA", ex.Message);
                    return null;
                }
            }
            else
            {
                Log.Error("SecureRepository_DATA", "No Data stored under this alias");
                return null;
            }
        }

        /// <summary>
        /// Gets all available aliases from DataManager.
        /// </summary>
        /// <returns>Aliases List if exits, else null.</returns>
        public override IEnumerable<string> GetAliases()
        {
            IEnumerable<string> aliases;
            try
            {
                aliases = DataManager.GetAliases();
            }
            catch
            {
                return null;
            }

            return aliases;
        }

        /// <summary>
        /// Gets first chars of Data content no more than charCount.
        /// </summary>
        /// <param name="alias">The name of a Key.</param>
        /// <param name="password">The password used to decrypt a Data. If password is provided when adding a Data to DataManager, the same password should be passed here.</param>
        /// <param name="charCount">Maximum chars count that will be returned.</param>
        /// <returns>Key content prefix if exists, else null.</returns>
        public override string GetContentPrefix(string alias, string password = null, int charCount = 40)
        {
            byte[] data = null;
            string prefix = null;
            data = this.Get(alias, password); // Gets raw Data from DataManager.

            if (data != null)
            {
                prefix = System.Text.Encoding.UTF8.GetString(data);
                if (prefix.Length > charCount)
                {
                    prefix = prefix.Substring(0, charCount);
                }

                return prefix;
            }

            return null;
        }

        /// <summary>
        /// Gets Data type.
        /// </summary>
        /// <param name="alias">The name of a Data.</param>
        /// <param name="password">The password used to decrypt a Data. If password is provided when adding a Data to DataManager, the same password should be passed here.</param>
        /// <returns>Key Type if exists, else null.</returns>
        public override string GetType(string alias = null, string password = null)
        {
            return "byte[]"; // All Data saved using DataManger is saved as byte[]. Developer has to decode / encode it to their needs.
        }

        #endregion

        /// <summary>
        /// Adds data from file to DataManager.
        /// </summary>
        /// <param name="filePath">File path to data.</param>
        /// <param name="alias">The name of a Data.</param>
        /// <param name="password">The password used to encrypt a Data value.</param>
        public void Save(string filePath, string alias = "C#API_DATA_TEST", string password = null)
        {
            try
            {
                if (!this.Exists(alias, password))
                {
                    // Checks if filePath is valid.
                    if (CheckPath(filePath))
                    {
                        byte[] file = File.ReadAllBytes(filePath);
                        this.Save(file, alias, password); // Adds data to DataManager.
                    } // You can add else statement e.g. with some Log.Error()
                }
                else
                {
                    // You should log it because Exists() method does not log anything.
                    Log.Error("SecureRepository_DATA", "Data is already stored under this alias");
                }
            }
            catch (Exception ex)
            {
                // Log DataManager errors.
                Log.Error("SecureRepository_DATA", ex.Message);
            }
        }

        /// <summary>
        /// Adds data provided data to DataManager.
        /// </summary>
        /// <param name="data">Data that will be added to DataManager.</param>
        /// <param name="alias">The name of a Data.</param>
        /// <param name="password">The password used to encrypt an Data value.</param>
        public void Save(byte[] data, string alias, string password = null)
        {
            try
            {
                // Checks if data is already saved under this alias.
                if (!this.Exists(alias, password))
                {
                    DataManager.Save(alias, data, new Policy(password, true)); // If you do not want to use password protection just use DataManager.Save(alias,data,new Policy());
                }
                else
                {
                    // You should log it because Exists() method does not log anything.
                    Log.Error("SecureRepository_DATA", "Data is already stored under this alias");
                }
            }
            catch (Exception ex)
            {
                // Log DataManager errors.
                Log.Error("SecureRepository_DATA", ex.Message);
            }
        }
    }
}
