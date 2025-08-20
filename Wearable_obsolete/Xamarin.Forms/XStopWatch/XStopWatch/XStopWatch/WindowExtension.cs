using ElmSharp;
using System;
using System.Runtime.InteropServices;

namespace XStopWatch
{
    static class WindowExtension
    {
        // native shared object name of ui-efl-util API.
        const string EFLUtil = "libcapi-ui-efl-util.so.0";

        // A mode for the Window AlwaysOn
        enum ScreenMode
        {
            Default, // default don't block the screen off
            AlwaysOn // the screen will not turn off on the AlwaysOn mode
        }

        /// <summary>
        /// PInvoke AlwaysOn setter Native function
        /// </summary>
        /// <param name="window">A handle of the Elementary Window</param>
        /// <param name="mode">A mode for the Window AlwaysOn</param>
        /// <returns></returns>
        [DllImport(EFLUtil)]
        static extern int efl_util_set_window_screen_mode(IntPtr window, ScreenMode mode);

        /// <summary>
        /// PInvoke AlwaysOn getter Native function
        /// </summary>
        /// <param name="window">A handle of the Elementary Window</param>
        /// <param name="mode">A mode for the Window AlwaysOn output parameter</param>
        /// <returns></returns>
        [DllImport(EFLUtil)]
        static extern int efl_util_get_window_screen_mode(IntPtr window, out ScreenMode mode);

        /// <summary>
        /// Extension method for ElmSharp Window object for set a AlwaysOn mode.
        /// </summary>
        /// <param name="window">ElmSharp.Window object</param>
        /// <param name="isAlwaysOn">true if AlwaysOn mode or not</param>
        public static void SetAlwaysOn(this Window window, bool isAlwaysOn)
        {
            if (efl_util_set_window_screen_mode(window, isAlwaysOn ? ScreenMode.AlwaysOn : ScreenMode.Default) != 0)
            {
                // efl_util_set_window_screen_mode return TIZEN_ERROR_NONE on success
                // throw exception if not success
                throw new ArgumentException($"native efl_util_set_window_screen_mode return error, window=[{window.Handle.ToInt32()}] isAlwaysOn = [{isAlwaysOn}]");
            }
        }

        /// <summary>
        /// Extension method for ElmSharp Window object for get a AlwaysOn mode.
        /// </summary>
        /// <param name="window">ElmSharp.Window object</param>
        /// <returns>true if AlwaysOn mode or not</returns>
        public static bool GetAlwaysOn(this Window window)
        {
            if (efl_util_get_window_screen_mode(window, out var mode) != 0)
            {
                // efl_util_set_window_screen_mode return TIZEN_ERROR_NONE on success
                // throw exception if not success
                throw new ArgumentException($"native efl_util_set_window_screen_mode return error, window=[{window.Handle.ToInt32()}] isAlwaysOn = [{mode == ScreenMode.AlwaysOn}]");
            }

            return mode == ScreenMode.AlwaysOn;
        }
    }
}
