using System;
using Xamarin.Forms;
using System.Threading.Tasks;
namespace ScreenMirroringSample
{
    public enum ScreenMirroringState
    {
        /// <summary>
        /// Idle.
        /// </summary>
        Idle = 1,

        /// <summary>
        /// Prepared.
        /// </summary>
        /// <seealso cref="ScreenMirroring.Prepare(Display, ScreenMirroringResolutions)"/>
        Prepared,

        /// <summary>
        /// Connected to a source.
        /// </summary>
        /// <seealso cref="ScreenMirroring.ConnectAsync(string)"/>
        Connected,

        /// <summary>
        /// Playing.
        /// </summary>
        /// <seealso cref="ScreenMirroring.StartAsync"/>
        Playing,

        /// <summary>
        /// Paused while playing media.
        /// </summary>
        /// <seealso cref="ScreenMirroring.PauseAsync"/>
        Paused,

        /// <summary>
        /// Disconnected from source.
        /// </summary>
        /// <seealso cref="ScreenMirroring.Disconnect"/>
        Disconnected
    }
    public interface IScreenMirroring
    {
        bool IsPlaying { get; }

        Task ConnectAsync(string sourceIp);

        Task StartAsync();

        Object GetDisplay();

        void Disconnect();

        void Unprepare();

        void Destroy();

        void Dispose();

        event EventHandler<StateEventArgs> StateChanged;

        void SetDisplay(object value);
        bool StateFlag { get; set; }
        ScreenMirroringState State { get; set; }

        void Prepare();
    }
    public class StateEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ErrorEventArgs class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public StateEventArgs(int state)
        {
            State = state;
        }
        /// <summary>
        /// Gets the message.
        /// </summary>
        public int State { get; set; }
    }
}
