using Xamarin.Forms;

namespace SystemInfo
{

    /// <summary>
    /// a utility class
    /// </summary>
    class Util
    {

        /// <summary>
        /// a static method to access native layer
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        public static string IsSupport(string key)
        {
            string Result = "";
            Result = DependencyService.Get<ISystemInfo>().TryGetValue(key);
            return Result;
        }
    }
}
