using System;
using System.Text;
using Tizen.Security.SecureRepository;
using Tizen.Security.SecureRepository.Crypto;
using Tizen;
namespace SecureRepository.Tizen
{
    /// <summary>
    /// Class for Encrypting, Decrypting data
    /// <para>Also has static method responsible for converting byte[] to hexadecimal encoded string.</para>
    /// </summary>
    class Cryptography
    {
        private string _alias; // alias for aes key using for encryption / decryption
        private Cipher _crypto; //Class for encrypting / decrypting data
        private Keys _keys; //Used to remove key from KeyManager if its not Aes key
        private string _password;
        public Cryptography(string alias = "AES_ALIAS_FOR_ALL_MY_APPS", string password = null)
        {
            _alias = alias;
            byte[] random = Encoding.ASCII.GetBytes("3ivs1Uhl!KgdAAa1"); //Generate initialization vector for AesCbc, in your App change it to YOUR RANDOM string e.g. form /dev/random
            AesCbcCipherParameters aes = new AesCbcCipherParameters(); // Cipher class suports different Algorythms check api for more information
            aes.IV = random; //assign Random bytes to initialization vector
            _crypto = new Cipher(aes); //Create instance of class responsible for encryption / decryption
            _keys = new Keys(); //Create instance of the class responsible for removing key from KeyManager if its not Aes key
            //Keys.RemoveAllKeys(); // Use it in debuging / when you want to clear all keys form KeyManager
            CreateAesKey();
        }
        /// <summary>
        /// Creates Aes Key if one does not already exists inside KeyManager under _alias
        /// </summary>
        private void CreateAesKey()
        {
            bool hasKey = true;
            try
            {
                Key k;
                k = KeyManager.Get(_alias, _password); //checks if Aes Key exists inside KeyManager
                if (k.Type != KeyType.Aes) //checks if Key is proper Aes Key, if not we will have to delete it and generate proper one
                {
                    hasKey = false;
                    _keys.Remove(_alias);
                }

            }
            catch//e.g. key is not in KeyManager
            {
                hasKey = false;
            }
            if (!hasKey) //Generate new Aes key
            {
                try
                {
                    KeyManager.CreateAesKey(256, _alias, new Policy(_password, true));
                }
                catch (Exception ex)
                {
                    Log.Error("SecureRepository_CRYPTOGRAPHY", ex.Message);
                }
            }

        }
        /// <summary>
        /// Encrypt data using Aes Key from KeyManager
        /// </summary>
        /// <param name="data">data to encrypt</param>
        /// <returns>encrypted data or null if error occurs</returns>
        public byte[] Encrypt(byte[] data)
        {
            try
            {
                KeyManager.Get(_alias, _password); //checks if Aes Key exists inside KeyManager, Alternative method of checking if item exists that I talked about in Secure class line 29
                byte[] b;
                b = _crypto.Encrypt(_alias, _password, data);
                return b;
            }
            catch (Exception ex)
            {
                Log.Error("SecureRepository_CRYPTOGRAPHY", ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Decrypt data using Aes Key from KeyManager
        /// </summary>
        /// <param name="data">data to decrypt</param>
        /// <returns>decrypted data or null if any error occurs</returns>
        public byte[] Decrypt(byte[] data)
        {
            try
            {
                KeyManager.Get(_alias, _password); //checks if Aes Key exists inside KeyManager, Alternative method of checking if item exists that I talked about in Secure class line 29
                byte[] b;
                b = _crypto.Decrypt(_alias, _password, data);
                return b;
            }
            catch (Exception ex)
            {
                Log.Error("SecureRepository_CRYPTOGRAPHY", ex.Message);
                return null;
            }
        }
        #region converting
        private static readonly uint[] _lookup32 = CreateLookup32();
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
        /// Converts byte array to hexadecimal encoded string
        /// </summary>
        /// <param name="bytes">Byte array that will be converted</param>
        /// <returns>Hexadecimal string</returns>
        public static string ByteArrayToHexViaLookup32(byte[] bytes)
        {
            var lookup32 = _lookup32;
            var result = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                var val = lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[2 * i + 1] = (char)(val >> 16);
            }
            return new string(result);
        }
        #endregion
    }
}
