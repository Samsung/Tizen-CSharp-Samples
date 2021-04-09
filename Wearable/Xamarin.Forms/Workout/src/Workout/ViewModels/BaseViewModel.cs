using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Workout.ViewModels
{
    /// <summary>
    /// Class that provides basic functionality for view models.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region properties

        /// <summary>
        /// PropertyChanged event handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region methods

        /// <summary>
        /// Updates value of the "storage" argument with value given by the second argument.
        /// Notifies the application about update of the property which has executed this method.
        /// </summary>
        /// <typeparam name="T">Type of storage field.</typeparam>
        /// <param name="storage">Field reference.</param>
        /// <param name="value">New value.</param>
        /// <param name="propertyName">Name of property triggered change.</param>
        /// <returns>Returns true if storage is successfully updated, false otherwise.</returns>
        protected bool SetProperty<T>(ref T storage, T value,
                                      [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Notifies the application about update of the property with name given as a parameter.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
