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
 * @brief       This file contains example usage of Cipher class for Encrytpion / Decryption.
 */

namespace SecureRepository.Tizen
{
    using System;
    using System.Text;
    using global::Tizen;
    using global::Tizen.Security.SecureRepository;
    using global::Tizen.Security.SecureRepository.Crypto;

    /// <summary>
    /// Class for Encrypting, Decrypting data.
    /// <para>Also has static method responsible for converting byte[] to hexadecimal encoded string.</para>
    /// </summary>
    public class Cryptography
    {
        /// <summary>
        /// Lookup used for converting byte array to hexadecimal encoded string.
        /// </summary>
        private static readonly uint[] Lookup32 = CreateLookup32();

        /// <summary>
        /// Alias for Aes Key used for Encryption / Decryption.
        /// </summary>
        private string alias;

        /// <summary>
        /// Class for data Encryption / Decryption.
        /// </summary>
        private Cipher crypto;

        /// <summary>
        /// Class used to Remove Key from KeyManager if it is not an Aes Key.
        /// </summary>
        private Keys keys;

        /// <summary>
        /// Stores password used to access Key from KeyManager.
        /// </summary>
        private string password;

        /// <summary>
        /// Initializes a new instance of the Cryptography class .
        /// </summary>
        /// <param name="alias">The name of a Key.</param>
        /// <param name="password">The password that will be used to encrypt an Key value.</param>
        public Cryptography(string alias = "AES_ALIAS_FOR_ALL_MY_APPS", string password = null)
        {
            this.alias = alias;
            this.password = password;

            // Generates initialization vector for AesCbc, in your App change it to YOUR RANDOM string e.g. form /dev/random.
            byte[] random = Encoding.ASCII.GetBytes("3ivs1Uhl!KgdAAa1");

            // Cipher class suports different Algorythms check api for more information.
            AesCbcCipherParameters aes = new AesCbcCipherParameters();
            aes.IV = random; // assign Random bytes to initialization vector.
            this.crypto = new Cipher(aes); // Create instance of class responsible for encryption / decryption.
            this.keys = new Keys(); // Create instance of the class responsible for removing key from KeyManager if its not Aes key.
            // Keys.RemoveAllKeys(); // Use it in debuging / when you want to clear all keys form KeyManager.
            this.CreateAesKey();
        }

        /// <summary>
        /// Converts byte array to hexadecimal encoded string.
        /// </summary>
        /// <param name="bytes">Byte array that will be converted.</param>
        /// <returns>Hexadecimal string.</returns>
        public static string ByteArrayToHexViaLookup32(byte[] bytes)
        {
            var lookup32 = Lookup32;
            var result = new char[bytes.Length * 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                var val = lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[2 * i + 1] = (char)(val >> 16);
            }

            return new string(result);
        }

        /// <summary>
        /// Encrypt data using Aes Key from KeyManager.
        /// </summary>
        /// <param name="data">data to encrypt.</param>
        /// <returns>Encrypted data or null if error occurs.</returns>
        public byte[] Encrypt(byte[] data)
        {
            try
            {
                KeyManager.Get(this.alias, this.password); // Checks if Aes Key exists inside KeyManager, Alternative method of checking if item exists that I talked about in Secure class line 29.
                byte[] b;
                b = this.crypto.Encrypt(this.alias, this.password, data);
                return b;
            }
            catch (Exception ex)
            {
                Log.Error("SecureRepository_CRYPTOGRAPHY", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Decrypt data using Aes Key from KeyManager.
        /// </summary>
        /// <param name="data">Data to decrypt.</param>
        /// <returns>Decrypted data or null if any error occurs.</returns>
        public byte[] Decrypt(byte[] data)
        {
            try
            {
                KeyManager.Get(this.alias, this.password); // Checks if Aes Key exists inside KeyManager, Alternative method of checking if item exists that I talked about in Secure class line 29.
                byte[] b;
                b = this.crypto.Decrypt(this.alias, this.password, data);
                return b;
            }
            catch (Exception ex)
            {
                Log.Error("SecureRepository_CRYPTOGRAPHY", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Creates lookup used for converting byte array to hexadecimal encoded string.
        /// </summary>
        /// <returns>Lookup array.</returns>
        private static uint[] CreateLookup32()
        {
            var result = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                string s = i.ToString("X2");
                result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
            }

            return result;
        }

        /// <summary>
        /// Creates Aes Key if one does not already exists inside KeyManager under _alias.
        /// </summary>
        private void CreateAesKey()
        {
            bool hasKey = true;
            try
            {
                Key k;
                k = KeyManager.Get(this.alias, this.password); // Checks if Aes Key exists inside KeyManager.

                // Checks if Key is proper Aes Key, if not we will have to delete it and generate proper one.
                if (k.Type != KeyType.Aes)
                {
                    hasKey = false;
                    this.keys.Remove(this.alias);
                }
            }
            catch
            {
                // e.g. Key is not in KeyManager.
                hasKey = false;
            }

            // Generates new Aes key.
            if (!hasKey)
            {
                try
                {
                    KeyManager.CreateAesKey(256, this.alias, new Policy(this.password, true));
                }
                catch (Exception ex)
                {
                    Log.Error("SecureRepository_CRYPTOGRAPHY", ex.Message);
                }
            }
        }
    }
}
