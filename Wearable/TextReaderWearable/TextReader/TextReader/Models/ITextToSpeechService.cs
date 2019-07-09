using System;

namespace TextReader.Models
{
    /// <summary>
    /// Interface of text-to-speech service which allows synthesizing text into speech.
    /// </summary>
    public interface ITextToSpeechService
    {
        #region properties

        /// <summary>
        /// Event invoked when the TTS state is changed to 'ready' for the first time.
        /// </summary>
        event EventHandler StateChangedToReady;

        /// <summary>
        /// Event fired when the utterance is completed.
        /// </summary>
        event EventHandler UtteranceCompleted;

        /// <summary>
        /// Flag indicating if the service is ready to use.
        /// The service requires "Init" method to be called before use.
        /// </summary>
        bool Ready { get; }

        /// <summary>
        /// Currently used text.
        /// If the text is changed, the current utterance is stopped immediately
        /// and then starts the new one.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Flag indicating if the text is being read by the TTS engine.
        /// </summary>
        bool Playing { get; }

        #endregion

        #region methods

        /// <summary>
        /// Creates instance of the text-to-speech service.
        /// </summary>
        void Create();

        /// <summary>
        /// Releases resources used by the text-to-speech service.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Initializes the service.
        /// Service can be used when the "Ready" property returns "true" value.
        /// </summary>
        void Init();

        /// <summary>
        /// Starts or resumes the current utterance.
        /// </summary>
        void Play();

        /// <summary>
        /// Pauses the current utterance.
        /// </summary>
        void Pause();

        /// <summary>
        /// Stops and clears the current utterance.
        /// </summary>
        void Stop();

        #endregion
    }
}
