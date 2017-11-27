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
 * @brief       This file contains example usage of Certificate & CertificateManager class
 */

namespace SecureRepository.Tizen
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using global::Tizen;
    using global::Tizen.Security.SecureRepository;

    /// <summary>
    /// Example usage of Certificate and CertificateManager class.
    /// </summary>
    public class Certificates : Secure<Certificate>
    {
        #region Implement Intrerface

        /// <summary>
        /// Gets Certificate saved under specified alias from CertificateManager.
        /// </summary>
        /// <param name="alias">The name of a Certificate.</param>
        /// <param name="password">The password used to decrypt a Certificate. If password is provided when adding a Certificate to CertificateManager, the same password should be passed here.</param>
        /// <returns>Certificate if exists, else null.</returns>
        public override Certificate Get(string alias, string password)
        {
            Certificate data = null;
            if (this.Exists(alias, password))
            {
                try
                {
                    data = CertificateManager.Get(alias, password);
                    return data;
                }
                catch (Exception ex)
                {
                    Log.Error("SecureRepository_CERTIFICATES", ex.Message);
                }
            }
            else
            {
                Log.Error("SecureRepository_CERTIFICATES", "No Certificates stored under this alias");
            }

            return null;
        }

        /// <summary>
        /// Gets all available aliases from CertificateManager.
        /// </summary>
        /// <returns>Aliases List if exists, else null.</returns>
        public override IEnumerable<string> GetAliases()
        {
            IEnumerable<string> aliases;
            try
            {
                aliases = CertificateManager.GetAliases();
            }
            catch
            {
                return null;
            }

            // alias: "App.name ALIAS" Alias has two parts App.name and actual ALIAS.
            // To get the short version just use GetAliasesShort() method defined in Secure Class.
            return aliases;
        }

        /// <summary>
        /// Gets first chars of Certificate content no more than charCount.
        /// </summary>
        /// <param name="alias">The name of a Key.</param>
        /// <param name="password">The password used to decrypt a Certificate. If password is provided when adding a Certificate to CertificateManager, the same password should be passed here.</param>
        /// <param name="charCount">Maximum chars count that will be returned.</param>
        /// <returns>Key content prefix if exists, else null.</returns>
        public override string GetContentPrefix(string alias, string password = null, int charCount = 40)
        {
            Certificate c = this.Get(alias, password);
            string prefix = null;
            if (c != null)
            {
                prefix = Cryptography.ByteArrayToHexViaLookup32(c.Binary);
                if (prefix.Length > charCount)
                {
                    prefix = prefix.Substring(0, charCount);
                }

                return prefix;
            }

            return null;
        }

        /// <summary>
        /// Gets Certificate type.
        /// </summary>
        /// <param name="alias">The name of a Certificate.</param>
        /// <param name="password">The password used to decrypt a Certificate. If password is provided when adding a Certificate to CertificateManager, the same password should be passed here.</param>
        /// <returns>Certificate Type if Certificate exists, else null.</returns>
        public override string GetType(string alias, string password = null)
        {
            Certificate c;
            c = this.Get(alias, password);
            if (c != null)
            {
                return c.Format.ToString();
            }
            else
            {
                return null;
            }
        }

        #endregion // Implement Interface

        /// <summary>
        /// Generates Certificate chain.
        /// </summary>
        /// <param name="certificate">Certificate that will be verified.</param>
        /// <param name="untrustedCertificates">Certificates list that Certificate will be checked against.</param>
        /// <returns>Certificate Chain if verification will be successful, null if not or if occurred error.</returns>
        public IEnumerable<Certificate> Check(Certificate certificate, IEnumerable<Certificate> untrustedCertificates)
        {
            try
            {
                IEnumerable<Certificate> certs = null;
                //// I am Using This method because Emulator may not have all root CA Certificates.
                certs = CertificateManager.GetCertificateChain(certificate, null, untrustedCertificates, false);

                ////There is also short method but it uses root CA stored inside emulator / product.
                ////Not all root CA`s are stored inside emulator / product, so check it before you deploy App to store.
                // certs = CertificateManager.GetCertificateChain(certificate, untrustedCertificates);
                return certs;
            }
            catch (Exception ex)
            {
                // e.g. Can not generate chain because chain does not end in root CA Certificate.
                Log.Error("SecureRepository_CERTIFICATES", ex.Message);
                return null;
            }
        }

        /// <summary>
        ///  Reads single Certificate from file path, supports only single certificate NOT CERTIFICATE CHAIN.
        /// </summary>
        /// <param name="filePath">File path to data.</param>
        /// <returns>Certificates chain, null if occurred error.</returns>
        public Certificate Get(string filePath)
        {
            if (Data.CheckPath(filePath))
            {
                try
                {
                    Certificate cert = Certificate.Load(filePath);
                    return cert;
                }
                catch (Exception ex)
                {
                    Log.Error("SecureRepository_CERTIFICATES", ex.Message);
                }
            }

            return null;
        }

        /// <summary>
        ///  Reads Certificates from filePath, supports only PEM FORMAT FILE, you can extend it to other formats.
        /// </summary>
        /// <param name="filePath">File path to data.</param>
        /// <returns>Certificates list.</returns>
        public IEnumerable<Certificate> GetCertificates(string filePath)
        {
            // Checks if filePath is valid.
            if (Data.CheckPath(filePath))
            {
                List<string> lines = new List<string>(File.ReadLines(filePath));
                List<Certificate> certs = new List<Certificate>();
                Certificate cert;
                bool tryToGenerateCertificate = false;
                //// Process each line and try to get Certicates.
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains("-----BEGIN CERTIFICATE-----"))
                    {
                        int j = i;
                        for (j++; j < lines.Count; j++)
                        {
                            if (lines[j].Contains("-----END CERTIFICATE-----"))
                            {
                                tryToGenerateCertificate = true;
                                break;
                            }
                        }
                        //// Checks if Certyficate lines is not empty excluding BEGIN CERT... and END CERT... lines.
                        if (tryToGenerateCertificate && ((j - 1) - (i + 1) > 0))
                        {
                            string[] strArray = lines.GetRange(i, j + 1 - i).ToArray(); // Adds lines form BEGIN CERT.. to END CERT... including this lines.
                            cert = this.CreatePemCertificate(strArray);
                            if (cert != null)
                            {
                                certs.Add(cert);
                            }
                        }

                        tryToGenerateCertificate = false;
                        i = j; // Changed i value to not iterate over lines from BEGIN CERT... to END CERT... (lines that we already processed in second loop).
                    }
                }

                return certs;
            }

            return null;
        }

        /// <summary>
        /// Checks if Certificate is trusted.
        /// </summary>
        /// <param name="certificate">Certificate that will be verified.</param>
        /// <param name="untrustedCertificates">Certificates list that Certificate will be checked against.</param>
        /// <returns>true if verification will be successful, false if not or if occurred error.</returns>
        public bool IsTrusted(Certificate certificate, IEnumerable<Certificate> untrustedCertificates)
        {
            return this.Check(certificate, untrustedCertificates) != null;
        }

        /// <summary>
        /// Saves Certificate to CertificateManager.
        /// </summary>
        /// <param name="alias">The name of a Certificate.</param>
        /// <param name="filePath">File path to data.</param>
        public void Save(string alias, string filePath)
        {
            if (Data.CheckPath(filePath))
            {
                Certificate cert = null;
                try
                {
                    cert = Certificate.Load(filePath);
                    if (cert != null)
                    {
                        this.Save(alias, cert);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("SecureRepository_CERTIFICATES", ex.Message);
                }
            }
        }

        /// <summary>
        /// Saves Certificate to CertificateManager.
        /// </summary>
        /// <param name="alias">The name of a Certificate.</param>
        /// <param name="certificate">Certificate that will be saved.</param>
        public void Save(string alias, Certificate certificate)
        {
            try
            {
                if (!this.Exists(alias))
                {
                    CertificateManager.Save(alias, certificate, new Policy());
                }
            }
            catch (Exception ex)
            {
                Log.Error("SecureRepository_CERTIFICATES", ex.Message);
            }
        }

        /// <summary>
        /// Creates Pem certificate from string array.
        /// </summary>
        /// <param name="singleCertificate">String array with single Pem certificate.</param>
        /// <returns>Certificate or null if occurred error.</returns>
        private Certificate CreatePemCertificate(string[] singleCertificate)
        {
            string str = string.Empty;
            //// Converts string array to single string.
            foreach (string s in singleCertificate)
            {
                str += s + "\n";
            }
            //// Tries to Create Certificate from this lines.
            try
            {
                Certificate cert = new Certificate(Encoding.UTF8.GetBytes(str), DataFormat.Pem);
                return cert;

                // Log.Debug("SecureRepository_CERT", str); //Only used for testing dont use it in YOUR APP.
            }
            catch (Exception ex)
            {
                // e.g. unable to create Certificate.
                Log.Error("SecureRepository_CERTIFICATES", ex.Message);
            }

            return null;
        }
    }
}
