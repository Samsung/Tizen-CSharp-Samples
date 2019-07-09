namespace TextReader.ViewModels
{
    /// <summary>
    /// Text paragraph view model class.
    /// </summary>
    public class ParagraphViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Paragraph text value.
        /// </summary>
        private string _text;

        /// <summary>
        /// Flag indicating if paragraph is active one.
        /// </summary>
        private bool _active;

        #endregion

        #region properties

        /// <summary>
        /// Paragraph text value.
        /// </summary>
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        /// <summary>
        /// Flag indicating if paragraph is active one.
        /// </summary>
        public bool Active
        {
            get => _active;
            set => SetProperty(ref _active, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// ParagraphViewModel class constructor.
        /// </summary>
        /// <param name="text">Paragraph text value.</param>
        /// <param name="active">Flag indicating if paragraph is active one.</param>
        public ParagraphViewModel(string text, bool active)
        {
            _text = text;
            _active = active;
        }

        #endregion
    }
}
