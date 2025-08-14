using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.Converters
{
    /// <summary>
    /// Converter class to convert STT supported language code to corresponding display name.
    /// </summary>
    public class SupportedLanguageToDisplayNameConverter
    {
        #region methods

        /// <summary>
        /// Converts STT supported language code to corresponding display name.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <returns>Converted value.</returns>
        public string Convert(string value)
        {
            switch (value)
            {
                case "en_US":
                    return "English US";
                case "es_US":
                    return "Spanish US";
                case "fr_FR":
                    return "French";
                case "ja_JP":
                    return "Japanese";
                case "ko_KR":
                    return "Korean";
                case "zh_CN":
                    return "Chinese";
                case "zh_TW":
                    return "Chinese Taiwan";
                case "zh_SG":
                    return "Chinese Singapore";
                case "zh_HK":
                    return "Chinese Hong Kong";
                case "de_DE":
                    return "German";
                case "ru_RU":
                    return "Russian";
                case "pt_BR":
                    return "Portuguese Brasil";
                case "es_ES":
                    return "Spanish";
                case "en_GB":
                    return "English GB";
                case "it_IT":
                    return "Italian";
                default:
                    return value;
            }
        }

        /// <summary>
        /// Converts back STT supported language display name to corresponding code (string).
        ///
        /// Not required by the application (not implemented).
        /// </summary>
        /// <param name="value">Value to be converted back.</param>
        /// <returns>Converted value.</returns>
        public string ConvertBack(string value)
        {
            switch (value)
            {
                case "English US":
                    return "en_US";
                case "Spanish US":
                    return "es_US";
                case "French":
                    return "fr_FR";
                case "Japanese":
                    return "ja_JP";
                case "Korean":
                    return "ko_KR";
                case "Chinese":
                    return "zh_CN";
                case "Chinese Taiwan":
                    return "zh_TW";
                case "Chinese Singapore":
                    return "zh_SG";
                case "Chinese Hong Kong":
                    return "zh_HK";
                case "German":
                    return "de_DE";
                case "Russian":
                    return "ru_RU";
                case "Portuguese Brasil":
                    return "pt_BR";
                case "Spanish":
                    return "es_ES";
                case "English GB":
                    return "en_GB";
                case "Italian":
                    return "it_IT";
                default:
                    return value;
            }
        }
        #endregion
    }
}