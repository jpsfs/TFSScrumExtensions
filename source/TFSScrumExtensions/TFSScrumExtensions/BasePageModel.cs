using System.ComponentModel;

namespace JosePedroSilva.TFSScrumExtensions
{
    public class BasePageModel : INotifyPropertyChanged
    {
        #region Private Variables
        #endregion

        #region Public Variables

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Private & Internal Methods

        /// <summary>
        /// Called to trigger property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Public Methods
        #endregion

        #region Event handling Methods
        #endregion
    }
}
