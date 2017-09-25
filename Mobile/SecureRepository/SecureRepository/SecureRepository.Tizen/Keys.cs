using System;
using System.Collections.Generic;
using System.Text;
using Tizen.Security.SecureRepository;
using Tizen;

namespace SecureRepository.Tizen
{
    /// <summary>
    /// Example usage of Key & KeyManager class
    /// </summary>
    class Keys : Secure<Key>
    {
        public Keys() { }
        #region Implement Interface
        /// <summary>
        /// Gets Key saved under specified alias from KeyManager
        /// </summary>
        /// <param name="alias">The name of a Key.</param>
        /// <param name="password">The password used to decrypt a Key. If password is provided when adding a Key to KeyManager, the same password should be passed here.</param>
        /// <returns>Key if exists or null</returns>
        public override Key Get(string alias, string password)
        {
            Key data = null;
            if (Exists(alias, password))
            {
                try
                {
                    data = KeyManager.Get(alias, password);
                    return data;
                }
                catch (Exception ex)
                {
                    Log.Error("SecureRepository_KEYS", ex.Message);
                    return null;
                }
            }
            else
            {
                Log.Error("SecureRepository_KEYS", "No Keys stored under this alias");
                return null;
            }

        }
        /// <summary>
        /// Gets all aviable aliases from KeyManager
        /// </summary>
        /// <returns>Aliases List or null if aliases do not exist</returns>
        public override IEnumerable<string> GetAliases()
        {
            IEnumerable<string> aliases;
            try
            {
                aliases = KeyManager.GetAliases();
            }
            catch
            {
                return null;
            }
            return aliases;
        }
        /// <summary>
        /// Gets first chars of Key content no more than charCount
        /// </summary>
        /// <param name="alias">The name of a Key.</param>
        /// <param name="password">The password used to decrypt a Key. If password is provided when adding a Key to KeyManager, the same password should be passed here.</param>
        /// <param name="charCount">Maximum chars count that will be returned.</param>
        /// <returns>Key content prefix or null if does not exist</returns>
        public override string GetContentPrefix(string alias, string password = null, int charCount = 80)
        {
            Key k;
            string prefix = null;
            k = Get(alias, password);
            if (k != null)
            {
                prefix = Cryptography.ByteArrayToHexViaLookup32(k.Binary);
                if (prefix.Length < charCount)
                    return prefix;
                else
                    return prefix.Substring(0, charCount);
            }
            else
                return null;

        }
        /// <summary>
        /// Gets Key type
        /// </summary>
        /// <param name="alias">The name of a Key.</param>
        /// <param name="password">The password used to decrypt a Key. If password is provided when adding a Key to KeyManager, the same password should be passed here.</param>
        /// <returns>Key Type if Key exists or null</returns>
        public override string GetType(string alias, string password = null)
        {
            Key k;
            k = Get(alias, password);
            if (k != null)
                return k.Type.ToString();
            else
                return null;
        }
        #endregion //Implement Interface
        /// <summary>
        /// Adds Aes Key to KeyManager
        /// </summary>
        /// <param name="alias">The name of a Key.</param>
        /// <param name="key">The binary content of Key</param>
        /// <param name="password">The password that will be used to encrypt an Key value.</param>
        public void AddAesKey(string alias, byte[] key = null, string password = null)
        {
            if (key == null)
            {
                if (!Exists(alias, password))
                    KeyManager.CreateAesKey(256, alias, new Policy(password,true));// Creates new Aes key if it`s not already inside KeyManager
            }
            else
            {
                Key k = new Key(key, KeyType.Aes, password); //Creates Key (generic class for holding Keys e.g. Aes,Rsa
                try
                {
                    if (!Exists(alias, password))
                        KeyManager.Save(alias, k, new Policy(password,true)); // Save Created key to KeyManager
                }
                catch (Exception ex)
                {
                    Log.Error("SecureRepository_KEYS", ex.Message);
                }
            }

        }
        /// <summary>
        /// Adds Rsa private Key to KeyManager
        /// </summary>
        /// <param name="alias">The name of a Key.</param>
        /// <param name="privateKeyString">The binary content of Key</param>
        public void AddPrivateRsaKey(string alias, string privateKeyString = null)
        {
            string password = null;//"MY_SECURE_PASSWORD_FOR_RSA_PRIVATE_KEY";
            if (privateKeyString == null) // replace it with your own Key
            {
                privateKeyString = "-----BEGIN RSA PRIVATE KEY-----\n" +
                                   "MIICXAIBAAKBgQCqGKukO1De7zhZj6+H0qtjTkVxwTCpvKe4eCZ0FPqri0cb2JZfXJ/DgYSF6vUp\n" +
                                   "wmJG8wVQZKjeGcjDOL5UlsuusFncCzWBQ7RKNUSesmQRMSGkVb1/3j+skZ6UtW+5u09lHNsj6tQ5\n" +
                                   "1s1SPrCBkedbNf0Tp0GbMJDyR4e9T04ZZwIDAQABAoGAFijko56+qGyN8M0RVyaRAXz++xTqHBLh\n" +
                                   "3tx4VgMtrQ+WEgCjhoTwo23KMBAuJGSYnRmoBZM3lMfTKevIkAidPExvYCdm5dYq3XToLkkLv5L2\n" +
                                   "pIIVOFMDG+KESnAFV7l2c+cnzRMW0+b6f8mR1CJzZuxVLL6Q02fvLi55/mbSYxECQQDeAw6fiIQX\n" +
                                   "GukBI4eMZZt4nscy2o12KyYner3VpoeE+Np2q+Z3pvAMd/aNzQ/W9WaI+NRfcxUJrmfPwIGm63il\n" +
                                   "AkEAxCL5HQb2bQr4ByorcMWm/hEP2MZzROV73yF41hPsRC9m66KrheO9HPTJuo3/9s5p+sqGxOlF\n" +
                                   "L0NDt4SkosjgGwJAFklyR1uZ/wPJjj611cdBcztlPdqoxssQGnh85BzCj/u3WqBpE2vjvyyvyI5k\n" +
                                   "X6zk7S0ljKtt2jny2+00VsBerQJBAJGC1Mg5Oydo5NwD6BiROrPxGo2bpTbu/fhrT8ebHkTz2epl\n" +
                                   "U9VQQSQzY1oZMVX8i1m5WUTLPz2yLJIBQVdXqhMCQBGoiuSoSjafUhV7i1cEGpb88h5NBYZzWXGZ\n" +
                                   "37sJ5QsW+sJyoNde3xH8vdXhzU7eT82D6X/scw9RZz+/6rCJ4p0=\n" +
                                   "-----END RSA PRIVATE KEY-----";
            }

            Key key = new Key(Encoding.UTF8.GetBytes(privateKeyString), KeyType.RsaPrivate, password); // Create new Rsa private Key
            try
            {
                if (!Exists(alias, password))
                    KeyManager.Save(alias, key, new Policy(password,true)); // Save Created Key to KeyManager
            }
            catch (Exception ex)
            {
                Log.Error("SecureRepository_KEYS", ex.Message);
            }
        }
        /// <summary>
        /// Adds Rsa public Key to KeyManager
        /// </summary>
        /// <param name="alias">The name of a Key.</param>
        /// <param name="privateKeyString">The binary content of Key</param>
        public void AddPublicRsaKey(string alias, string publicKeyString = null)//Add public Rsa key to KeyManager
        {
            string password = null;//"MY_SECURE_PASSWORD_FOR_RSA_PUBLIC_KEY"; //You can define it here or edit method to take it as a paramether
            if (publicKeyString == null) // replace it with your own key
            {
                publicKeyString = "-----BEGIN PUBLIC KEY-----\n" +
               "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA2b1bXDa+S8/MGWnMkru4\n" +
               "T4tUddtZNi0NVjQn9RFH1NMa220GsRhRO56F77FlSVFKfSfVZKIiWg6C+DVCkcLf\n" +
               "zXJ/Z0pvwOQYBAqVMFjV6efQGN0JzJ1Unu7pPRiZl7RKGEI+cyzzrcDyrLLrQ2W7\n" +
               "0ZySkNEOv6Frx9JgC5NExuYY4lk2fQQa38JXiZkfyzif2em0px7mXbyf5LjccsKq\n" +
               "v1e+XLtMsL0ZefRcqsP++NzQAI8fKX7WBT+qK0HJDLiHrKOTWYzx6CwJ66LD/vvf\n" +
               "j55xtsKDLVDbsotvf8/m6VLMab+vqKk11TP4tq6yo0mwyTADvgl1zowQEO9I1W6o\n" +
               "zQIDAQAB\n" +
               "-----END PUBLIC KEY-----\n";
            }
            Key key = new Key(Encoding.UTF8.GetBytes(publicKeyString), KeyType.RsaPublic, password);
            try
            {
                if (!Exists(alias, password))
                    KeyManager.Save(alias, key, new Policy(password,true));
            }
            catch (Exception ex)
            {
                Log.Error("SecureRepository_KEYS", ex.Message);
            }
        }
        /// <summary>
        /// Creates Rsa Key pair and saves it inside KeyManager
        /// </summary>
        /// <param name="privateKeyAlias">The name of a private Key.</param>
        /// <param name="publicKeyAlias">The name of a public Key.</param>
        public void CreateRsaKeyPair(string privateKeyAlias, string publicKeyAlias)//You can extend this method to support password protection
        {
            if (publicKeyAlias == null || privateKeyAlias == null)
            {
                throw new System.ArgumentException("Argument is \"null\"");
            }
            if (!(Exists(privateKeyAlias) && !Exists(publicKeyAlias)))//Checks if Keys exist in KeyManager under this aliases
            {
                try
                {
                    KeyManager.CreateRsaKeyPair(1024, privateKeyAlias, publicKeyAlias, new Policy(), new Policy()); // USE 4096 in real application!!!!, lowered to speed up 
                }
                catch (Exception ex)
                {
                    Log.Error("SecureRepository_KEYS", ex.Message);
                }
            }
            else
                Log.Error("SecureRepository_KEYS", "Alias already in use.");
        }
    }
}
