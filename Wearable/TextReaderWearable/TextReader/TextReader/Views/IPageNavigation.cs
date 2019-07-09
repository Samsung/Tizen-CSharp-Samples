namespace TextReader.Views
{
    /// <summary>
    /// Provides methods which allow to navigate between pages.
    /// </summary>
    public interface IPageNavigation
    {
        #region methods

        /// <summary>
        /// Navigates to the 'how-to' page.
        /// </summary>
        void NavigateToHowToPage();

        /// <summary>
        /// Navigates to the text reader page.
        /// </summary>
        void NavigateToTextReader();

        /// <summary>
        /// Navigates to the welcome page.
        /// </summary>
        void NavigateToWelcomePage();

        #endregion
    }
}
