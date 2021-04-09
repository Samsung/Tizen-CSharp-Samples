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
 * @brief       This file contains Model that implements IMainInterface
 */

[assembly: Xamarin.Forms.Dependency(typeof(SecureRepository.Tizen.MainModel))]

namespace SecureRepository.Tizen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using global::Tizen;
    using global::Tizen.Security.SecureRepository;

    /// <summary>
    /// Class contains Implementation of IMainInterface.
    /// </summary>
    public class MainModel : IMainInterface
    {
        /// <summary>
        /// Used for accessing CertificateManager.
        /// </summary>
        private Certificates certificates;

        /// <summary>
        /// Used for storing data in DataManager and for checking file paths.
        /// </summary>
        private Data data;

        /// <summary>
        /// Used for accessing KeyManager.
        /// </summary>
        private Keys keys;

        /// <summary>
        /// Initializes a new instance of the MainModel class.
        /// </summary>
        public MainModel()
        {
            this.certificates = new Certificates();
            this.data = new Data();
            this.keys = new Keys();
            this.ListItems();
        }

        /// <summary>
        /// Gets decrypted text.
        /// </summary>
        public string DecryptedText
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets encrypted text.
        /// </summary>
        public string EncryptedText
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets Item list stored in Managers.
        /// </summary>
        public List<ListViewGroup> ItemList
        {
            get;
            private set;
        }

        /// <summary>
        /// Adds Items to Managers.
        /// </summary>
        public void Add()
        {
            this.AddCertificates();
            this.AddData();
            this.AddKeys();
            this.ListItems();
        }

        /// <summary>
        /// Encrypt, Decrypt data.
        /// </summary>
        public void EncryptDecrypt()
        {
            string textToEncrypt = "string1_ĄĘŹŻŁć_서울", encText, decText;
            Cryptography c = new Cryptography();

            byte[] b = c.Encrypt(System.Text.Encoding.UTF8.GetBytes(textToEncrypt)); // Encrypys string.

            if (b != null)
            {
                encText = "Encrypted:" + Cryptography.ByteArrayToHexViaLookup32(b); // Converts byte[] to hexadecimal string for reading only, in real app just use byte[].
                byte[] decBin = c.Decrypt(b);
                if (decBin != null)
                {
                    decText = "Orginal text: " + textToEncrypt + "\nDecrypted:    " + System.Text.Encoding.UTF8.GetString(decBin); // Displays decrypted text.
                }
                else
                {
                    decText = "Unable To decrypt text";
                }

                this.DecryptedText = decText;
            }
            else
            {
                encText = "Unable To encrypt text";
                decText = "Unable To decrypt text";
            }

            this.EncryptedText = encText;
            this.DecryptedText = decText;
            this.ListItems();
        }

        /// <summary>
        /// Gets selected Item content Prefix.
        /// </summary>
        /// <param name="selectedItem">Selected Item.</param>
        /// /// <returns>Item content prefix.</returns>
        public string GetSelectedItemPrefix(Item selectedItem)
        {
            string prefix = string.Empty;
            try
            {
                switch (selectedItem.Type)
                {
                    case AliasType.Certificate:
                        prefix = this.certificates.GetContentPrefix(selectedItem.Alias);
                        break;
                    case AliasType.Data:
                        prefix = this.data.GetContentPrefix(selectedItem.Alias);
                        break;
                    case AliasType.Key:
                        prefix = this.keys.GetContentPrefix(selectedItem.Alias);
                        break;
                }
            }
            catch (Exception ex)
            {
                // Unable to get Data,Certificate,Key related to that alias.
                Log.Error("SecureRepository_UI", "Unable to get item prefix");
                Log.Error("SecureRepository_UI", ex.Message);
            }

            return prefix;
        }

        /// <summary>
        /// Gets selected Item type.
        /// </summary>
        /// <param name="selectedItem">Selected Item.</param>
        /// <returns>Item type.</returns>
        public string GetSelectedItemType(Item selectedItem)
        {
            string type = string.Empty;
            try
            {
                switch (selectedItem.Type)
                {
                    case AliasType.Certificate:
                        type = this.certificates.GetType(selectedItem.Alias);
                        break;
                    case AliasType.Data:
                        type = this.data.GetType(); // data type is always byte[], so there is no need to pass any arguments to it.
                        break;
                    case AliasType.Key:
                        type = this.keys.GetType(selectedItem.Alias);
                        break;
                }
            }
            catch (Exception ex)
            {
                // Unable to get Data,Certificate,Key related to that alias.
                Log.Error("SecureRepository_UI", "Unable to get item data");
                Log.Error("SecureRepository_UI", ex.Message);
            }

            return type;
        }

        /// <summary>
        /// Removes Items from Managers.
        /// </summary>
        public void Remove()
        {
            this.keys.RemoveAll(); // Removes all keys from KeyManager.
            this.data.RemoveAll(); // Removes all data from DataManager.
            this.certificates.RemoveAll(); // Removes all certyficates from CertificateManager.
            this.ListItems();
        }

        /// <summary>
        /// Validates certificates and adds them to CertificateManager.
        /// </summary>
        private void AddCertificates()
        {
            // Certificates used in this example are from tizen.org. They are valid from 18 May 2017 till 18 July 2018.
            // So when running this app change your date to fit between this dates, otherwise You will get error that certificate is not valid.
            string chain_path = Tizen.Program.Current.DirectoryInfo.SharedResource + "tizen_pem_chain.crt";
            string path = Tizen.Program.Current.DirectoryInfo.SharedResource + "tizen_pem.crt";
            byte[] b = Encoding.UTF8.GetBytes("TESRASDAS");

            if (Data.CheckPath(path) && Data.CheckPath(chain_path))
            {
                IEnumerable<Certificate> certificateList = null;
                certificateList = this.certificates.GetCertificates(chain_path);
                //// Saves first certificate.
                if (certificateList != null)
                {
                    // Checks if Certificate chain is correct.
                    if (this.certificates.IsTrusted(certificateList.First(), certificateList))
                    {
                        this.certificates.Save("tizen.org_cert", path); // Saves single certificate.
                    }
                    else
                    {
                        //// Logs info that Certificate chain is incorrect.
                        Log.Error("SecureRepository_UI", "Certificate is NOT TRUSTED so app will not save it inside CertificateManager");
                    }
                }
            }
        }

        /// <summary>
        /// Adds data (user defined strings) to DataManager.
        /// </summary>
        private void AddData()
        {
            string testData = "TEST12321312dsajdas";
            string anotherData = "new_data_with_utf8_string_ĄĘŹŻŁć_서울"; // Another data to show that you can use UTF8 encoded data.
            // data.RemoveAll(); //Use it to manually remove all data from DataManager.
            this.data.Save(Encoding.UTF8.GetBytes(testData), "my_data", null);
            this.data.Save(Encoding.UTF8.GetBytes(anotherData), "my_data", null); // Will not succeed, because data is already added to this alias.
            this.data.Save(Encoding.UTF8.GetBytes(anotherData), "my_data_2", null); // This will succeed, because there is no data under "my_data_2" alias.
        }

        /// <summary>
        /// Adds Aes / Rsa Keys to KeyManager.
        /// </summary>
        private void AddKeys()
        {
            // keys.RemoveAll(); // KeyManager has previously keys even after reboot / recompile etc.
            // In normal app we don`t use this Method. Only use for debugging .
            this.keys.AddAesKey("my_aes");
            this.keys.AddPrivateRsaKey("my_private_rsa");
            this.keys.AddPublicRsaKey("my_public_rsa");
            this.keys.CreateRsaKeyPair("private_auto_rsa", "public_auto_rsa");
        }

        /// <summary>
        /// Adds available aliases from all Managers to listView, Pkcs12 not included (it`s not part of this tutorial).
        /// </summary>
        private void ListItems()
        {
            IEnumerable<string> dataAliases, keyAliases, certificateAliases;
            keyAliases = this.keys.GetAliasesShort(); // Gets aliases from KeyManager without App.Name prefix.
            dataAliases = this.data.GetAliasesShort(); // Gets aliases from DataManager without App.Name prefix.
            certificateAliases = this.certificates.GetAliasesShort(); // Gets aliases from CertificateManager without App.Name prefix.
            List<ListViewGroup> groups = new List<ListViewGroup>();
            int goupCount = 0;
            if (dataAliases != null)
            {
                groups.Add(new ListViewGroup(AliasType.Data));

                // Adds aliases to the list.
                foreach (string alias in dataAliases)
                {
                    groups[goupCount].Add(new Item(alias, AliasType.Data));
                }

                goupCount++;
            }

            if (keyAliases != null)
            {
                groups.Add(new ListViewGroup(AliasType.Key));

                // Adds aliases to the list.
                foreach (string alias in keyAliases)
                {
                    groups[goupCount].Add(new Item(alias, AliasType.Key));
                }

                goupCount++;
            }

            if (certificateAliases != null)
            {
                groups.Add(new ListViewGroup(AliasType.Certificate));

                // Adds aliases to the list.
                foreach (string alias in certificateAliases)
                {
                    groups[goupCount].Add(new Item(alias, AliasType.Certificate));
                    Log.Error("SecureRepository", "Cert");
                }
            }

            this.ItemList = groups;
        }
    }
}
