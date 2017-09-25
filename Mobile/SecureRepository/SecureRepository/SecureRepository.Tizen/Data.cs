using System;
using System.Collections.Generic;
using System.IO;
using Tizen;
using Tizen.Security.SecureRepository;
namespace SecureRepository.Tizen
{
    /// <summary>
    /// Class for storing / getting items from DataManager. Also has static method to validate filePath 
    /// </summary>
    class Data : Secure<byte[]>
    {
        /// <summary>
        /// Example usage of Data & DataManager class
        /// </summary>
        public Data() { }
        #region Implement Interface
        /// <summary>
        /// Gets Data saved under specified alias from DataManager
        /// </summary>
        /// <param name="alias">The name of a Data.</param>
        /// <param name="password">The password used to decrypt a Data. If password is provided when adding a Data to DataManager, the same password should be passed here.</param>
        /// <returns>Data if exists or null</returns>
        public override byte[] Get(string alias, string password = null)
        {
            byte[] data = null;
            if (Exists(alias, password))
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
        /// Gets all aviable aliases from DataManager
        /// </summary>
        /// <returns>Aliases List or null if aliases does not exist</returns>
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
        /// Gets first chars of Data content no more than charCount
        /// </summary>
        /// <param name="alias">The name of a Key.</param>
        /// <param name="password">The password used to decrypt a Data. If password is provided when adding a Data to DataManager, the same password should be passed here.</param>
        /// <param name="charCount">Maximum chars count that will be returned.</param>
        /// <returns>Key content prefix or null if does not exist</returns>
        public override string GetContentPrefix(string alias, string password = null, int charCount = 40)
        {
            byte[] data = null;
            string prefix = null;
            data = Get(alias, password); //Gets raw Data from DataManager
            if (data != null) //Gets only first charCount chars (prefix) form raw Data
            {
                prefix = System.Text.Encoding.UTF8.GetString(data);
                if (prefix.Length < charCount)
                    return prefix;
                else
                    return prefix.Substring(0, charCount);
            }
            else // Dont need to Log.Error because it`s already reported inside Get() method
                return null;
        }
        /// <summary>
        /// Gets Data type
        /// </summary>
        /// <param name="alias">The name of a Data.</param>
        /// <param name="password">The password used to decrypt a Data. If password is provided when adding a Data to DataManager, the same password should be passed here.</param>
        /// <returns>Key Type if Key exists or null</returns>
        public override string GetType(string alias = null, string password = null)
        {
            return "byte[]"; //All Data saved using DataManger is saved as byte[]. Developer has to decode / encode it to their needs
        }
        #endregion //Impement Interface
        /// <summary>
        /// Adds data from file to DataManager
        /// </summary>
        /// <param name="filePath">File path to data.</param>
        /// <param name="alias">The name of a Data.</param>
        /// <param name="password">The password used to encrypt a Data value.</param>
        public void Add(string filePath, string alias = "C#API_DATA_TEST", string password = null)
        {
            try
            {
                if (!Exists(alias, password))
                {
                    if (CheckPath(filePath)) //Checks if filePath is valid
                    {
                        byte[] file = File.ReadAllBytes(filePath);
                        Add(file, alias, password); //Adds data to DataManager
                    } //You can add else statement e.g. with some Log.Error()
                }
                else // You should log it because Exists() method does not log anything 
                    Log.Error("SecureRepository_DATA", "Data is already stored under this alias");
            }
            catch (Exception ex) //Log DataManager errors
            {
                Log.Error("SecureRepository_DATA", ex.Message);
            }

        }
        /// <summary>
        /// Adds data provided data to DataManager
        /// </summary>
        /// <param name="data">Data that will be added to DataManager</param>
        /// <param name="alias">The name of a Data.</param>
        /// <param name="password">The password used to encrypt an Data value.</param>
        public void Add(byte[] data, string alias, string password = null)
        {

            try
            {
                if (!Exists(alias, password)) //Checks if data is already saved under this alias 
                    DataManager.Save(alias, data, new Policy(password, true)); //If you do not want to use password protection just use DataManager.Save(alias,data,new Policy());
                else //You should log it because Exists() method does not log anything 
                    Log.Error("SecureRepository_DATA", "Data is already stored under this alias");
            }
            catch (Exception ex)  //Log DataManager errors
            {
                Log.Error("SecureRepository_DATA", ex.Message);
            }
        }
        /// <summary>
        /// Checks if provided file path is valid
        /// </summary>
        /// <param name="filePath">File path to data.</param>
        /// <returns>true if file path is valid, false if not or if encured error</returns>
        public static bool CheckPath(string filePath)
        {
            if (filePath.Length == 0)
                throw new System.ArgumentException("filePath lenght = 0");

            if (filePath.IndexOfAny(Path.GetInvalidPathChars()) == -1) //Checks if path does not contain invalid path characters
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    if (fileInfo.Length == -1)
                        throw new System.ArgumentException("file does not exist or it is a directory");
                    return true;
                }
                catch (Exception ex) //e.g. File IO exception 
                {
                    Log.Error("SecureRepository_DATA", ex.Message);
                }
            }
            else
                Log.Error("SecureRepository_DATA", "filePath contains invalid path characters");
            return false;
        }
    }
}
