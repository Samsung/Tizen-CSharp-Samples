using AudioManagerSample.Tizen.Mobile;
using Xamarin.Forms;

[assembly: Dependency(typeof(Log))]
namespace AudioManagerSample.Tizen.Mobile
{
    class Log : ILog
    {
        private readonly string LOGTAG = "AudioManagerSample";

        public void Debug(string message, string file = "", string func = "", int line = 0)
        {
            global::Tizen.Log.Debug(LOGTAG, message, file, func, line);
        }

        public void Error(string message, string file = "", string func = "", int line = 0)
        {
            global::Tizen.Log.Error(LOGTAG, message, file, func, line);
        }

        public void Info(string message, string file = "", string func = "", int line = 0)
        {
            global::Tizen.Log.Info(LOGTAG, message, file, func, line);
        }

        public void Warn(string message, string file = "", string func = "", int line = 0)
        {
            global::Tizen.Log.Warn(LOGTAG, message, file, func, line);
        }
    }
}
