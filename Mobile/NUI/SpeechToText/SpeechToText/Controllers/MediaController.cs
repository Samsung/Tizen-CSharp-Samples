using System;
using System.Collections.Generic;
using MediaContent = Tizen.Content.MediaContent;


namespace SpeechToText.Controllers
{
    public class MediaController
    {
        private MediaContent.MediaDatabase _mediaDatabase;

        public MediaController()
        {
            _mediaDatabase = new MediaContent.MediaDatabase();
            _mediaDatabase.Connect();
        }

        /// <summary>
        /// Returns a list of sound files (paths) which can be used
        /// as start and end sounds for STT client.
        /// </summary>
        /// <returns>A list of file names and file paths.</returns>
        public List<string> GetAvailableStartEndSounds()
        {
            var result = new List<string>();
            var command = new MediaContent.MediaInfoCommand(_mediaDatabase);
            var reader = command.SelectMedia(new MediaContent.SelectArguments()
            {
                FilterExpression = string.Format(
                    "{0} IN ('audio/wav', 'audio/x-wav')",
                    MediaContent.MediaInfoColumns.MimeType),
                SortOrder = MediaContent.MediaInfoColumns.DisplayName + " ASC"
            });
            result.Add("None");
            while (reader.Read())
            {
                MediaContent.AudioInfo info = reader.Current as MediaContent.AudioInfo;
                if (info == null)
                {
                    continue;
                }
                result.Add(info.Path);
            }
            return result;
        }
        ~MediaController()
        {
            _mediaDatabase.Disconnect();
        }
    }
}
