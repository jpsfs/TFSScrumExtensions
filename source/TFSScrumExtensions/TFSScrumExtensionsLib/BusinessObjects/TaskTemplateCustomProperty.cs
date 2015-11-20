using System;
using System.ComponentModel;

namespace JosePedroSilva.TFSScrumExtensions.BusinessObjects
{
    public class TaskTemplateCustomProperty : INotifyPropertyChanged
    {
        #region Private Variables

        private String _name;
        private String _value;

        #endregion

        #region Public Variables

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name
        {
            get
            {
                return this._name;
            }

            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public String Value
        {
            get
            {
                return this._value;
            }

            set
            {
                if (this._value != value)
                {
                    this._value = value;
                    this.OnPropertyChanged("Value");
                }
            }
        }

        #endregion

        #region Constructors
        #endregion

        #region Private & Internal Methods
        #endregion

        #region Public Methods

        /// <summary>
        /// Called to trigger property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Event handling Methods
        #endregion
    }
}
