namespace CalendarComponent.ViewModels
{
    /// <summary>
    /// ViewModelLocator class.
    /// Provides one PageViewModel instance for multiple classes.
    /// </summary>
    public static class ViewModelLocator
    {
        #region fields

        /// <summary>
        /// PageViewModel instance.
        /// </summary>
        private static CalendarPageViewModel _viewModel;

        #endregion

        #region properties

        /// <summary>
        /// PageViewModel instance.
        /// Initializes PageViewModel instance if it has not been initialized.
        /// </summary>
        public static CalendarPageViewModel ViewModel => _viewModel ?? (_viewModel = new CalendarPageViewModel());

        #endregion
    }
}