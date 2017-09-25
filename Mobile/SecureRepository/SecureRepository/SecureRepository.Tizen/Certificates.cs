using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Tizen.Security.SecureRepository;
using Tizen;

namespace SecureRepository.Tizen
{
    /// <summary>
    /// Example usage of Certificate & CertificateManager class
    /// </summary>
    class Certificates : Secure<Certificate>
    {
        /// <summary>
        /// 
        /// </summary>
        public Certificates() { }
        #region Implement Intrerface
        /// <summary>
        /// Gets Key saved under specified alias from KeyManager
        /// </summary>
        /// <param name="alias">The name of a Data.</param>
        /// <param name="password">The password used to decrypt a Certificate. If password is provided when adding a Certificate to CertificateManager, the same password should be passed here.</param>
        /// <returns>Certificate if exists or null</returns>
        public override Certificate Get(string alias, string password)
        {
            Certificate data = null;
            if (Exists(alias, password))
            {
                try
                {
                    data = CertificateManager.Get(alias, password);
                    return data;
                }
                catch (Exception ex)
                {
                    Log.Error("SecureRepository_CERTIFICATES", ex.Message);
                    return null;
                }
            }
            else
            {
                Log.Error("SecureRepository_CERTIFICATES", "No Certificates stored under this alias");
                return null;
            }
        }
        /// <summary>
        /// Gets all aviable aliases from CertificateManager
        /// </summary>
        /// <returns>Aliases List or null if aliases does not exist</returns>
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
            //alias: "App.name ALIAS" Alias has two parts App.name and actual ALIAS
            //To get the short version just use GetAliasesShort() method defined in Secure Class
            return aliases;
        }
        /// <summary>
        /// Gets first charCount chars of Certificate content
        /// </summary>
        /// <param name="alias">The name of a Key.</param>
        /// <param name="password">The password used to decrypt a Certificate. If password is provided when adding a Certificate to CertificateManager, the same password should be passed here.</param>
        /// <param name="charCount">Maximum chars count that will be returned.</param>
        /// <returns>Key content prefix or null if does not exist</returns>
        public override string GetContentPrefix(string alias, string password = null, int charCount = 40)
        {
            Certificate c;
            string prefix = null;
            c = Get(alias, password);
            if (c != null)
            {
                prefix = Cryptography.ByteArrayToHexViaLookup32(c.Binary);
                if (prefix.Length < charCount)
                    return prefix;
                else
                    return prefix.Substring(0, charCount);
            }
            else
                return null;
        }
        /// <summary>
        /// Gets Certificate type
        /// </summary>
        /// <param name="alias">The name of a Certificate.</param>
        /// <param name="password">The password used to decrypt a Certificate. If password is provided when adding a Certificate to CertificateManager, the same password should be passed here.</param>
        /// <returns>Certificate Type if Certificate exists or null</returns>
        public override string GetType(string alias, string password = null)
        {
            Certificate c;
            c = Get(alias, password);
            if (c != null)
                return c.Format.ToString();
            else
                return null;
        }
        #endregion //Implement Interface
        /// <summary>
        /// Generates Certificate chain
        /// </summary>
        /// <param name="certificate">Certificate that will be verified.</param>
        /// <param name="untrustedCertificates">Certificates list that Certificate will be checked against.</param>
        /// <returns>Certificate Chain if verifycation will be successfull, null if not or if encured error</returns>
        public IEnumerable<Certificate> Check(Certificate certificate, IEnumerable<Certificate> untrustedCertificates)
        {
            try
            {
                IEnumerable<Certificate> certs = null;
                certs = CertificateManager.GetCertificateChain(certificate, null, untrustedCertificates, false); // Using This method because Emulator may not have all root CA Certificates 
                //certs = CertificateManager.GetCertificateChain(certificate, untrustedCertificates); //In real App you can use this method 
                return certs;
            }
            catch (Exception ex)// e.g. Can`t generate chain because chain doesn`t end in root CA Certificate 
            {
                Log.Error("SecureRepository_CERTIFICATES", ex.Message);
                return null;
            }
        }
        /// <summary>
        ///  Reads single Certificate from file path, supports only single certificate NOT CERTIFICATE CHAIN
        /// </summary>
        /// <param name="filePath">File path to data.</param>
        /// <returns>Certificates chain, null if encured error</returns>
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
        ///  Reads Certificates from filePath, supports only PEM FORMAT FILE, you can extend it to other formats
        /// </summary>
        /// <param name="filePath">File path to data.</param>
        /// <returns>Certificates list</returns>
        public IEnumerable<Certificate> GetCertificates(string filePath)
        {
            if (Data.CheckPath(filePath))//Check if filePath is valid
            {
                List<string> lines = new List<string>(File.ReadLines(filePath));
                List<Certificate> certs = new List<Certificate>();
                bool tryToGenerateCertificate = false;
                for (int i = 0; i < lines.Count; i++) //Process each line and try to get Certicates 
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
                        if (tryToGenerateCertificate && ((j - 1) - (i + 1) > 0))//Checks if Certyficate lines is not empty excluding BEGIN CERT... and END CERT... lines
                        {
                            string[] strArray = lines.GetRange(i, j + 1 - i).ToArray(); //Adds lines form BEGIN CERT.. to END CERT... including this lines
                            string str = "";
                            foreach (string s in strArray) //Converts string array to single string
                            {
                                str += s + "\n";
                            }
                            try //Tries to Create Certificate from this lines 
                            {
                                Certificate cert = new Certificate(Encoding.UTF8.GetBytes(str), DataFormat.Pem);
                                certs.Add(cert);
                                //Log.Debug("SecureRepository_CERT", str); //Only used for testing dont use it in YOUR APP
                            }
                            catch (Exception ex)// e.g. unable to create Certificate
                            {
                                Log.Error("SecureRepository_CERTIFICATES", ex.Message);
                            }

                        }
                        tryToGenerateCertificate = false;
                        i = j; //Changed i value to not iterate over lines from BEGIN CERT... to END CERT... (lines that we already processed in second loop)
                    }
                }
                return certs;
            }
            return null;
        }
        /// <summary>
        /// Checks if Certificate is trusted
        /// </summary>
        /// <param name="certificate">Certificate that will be verified.</param>
        /// <param name="untrustedCertificates">Certificates list that Certificate will be checked against.</param>
        /// <returns>true if verifycation will be successfull, false if not or if encured error</returns>
        public bool IsTrusted(Certificate certificate, IEnumerable<Certificate> untrustedCertificates)
        {

            if (Check(certificate, untrustedCertificates) != null)
                return true;
            return false;
        }
        /// <summary>
        /// Saves Certificate to CertificateManager
        /// </summary>
        /// <param name="alias">The name of a Certificate.</param>
        /// <param name="filePath">File path to data.</param>
        public void Save(string alias, string filePath)
        {
            if (Data.CheckPath(filePath))//Checks if filePath is valid
            {
                Certificate cert = null;
                try
                {
                    cert = Certificate.Load(filePath);
                    if (cert != null)
                        Save(alias, cert);
                }
                catch (Exception ex)
                {
                    Log.Error("SecureRepository_CERTIFICATES", ex.Message);
                }
            }
        }
        /// <summary>
        /// Saves Certificate to CertificateManager
        /// </summary>
        /// <param name="alias">The name of a Certificate.</param>
        /// <param name="certificate">Certificate that will be saved.</param>
        public void Save(string alias, Certificate certificate)
        {
            try
            {
                if (!Exists(alias))
                    CertificateManager.Save(alias, certificate, new Policy());
            }
            catch (Exception ex)
            {
                Log.Error("SecureRepository_CERTIFICATES", ex.Message);
            }
        }
    }
}
