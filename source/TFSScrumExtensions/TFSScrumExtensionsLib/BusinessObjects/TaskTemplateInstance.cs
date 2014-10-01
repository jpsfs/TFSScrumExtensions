using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JosePedroSilva.TFSScrumExtensions.BusinessObjects
{
    public class TaskTemplateInstance : INotifyPropertyChanged
    {
        #region Private Variables

        private String _assignTo;
        private Int32 _estimatedTime;

        #endregion

        #region Public Variables

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the assigned to.
        /// </summary>
        /// <value>
        /// The assigned to.
        /// </value>
        public String AssignedTo
        {
            get
            {
                return this._assignTo;
            }

            set
            {
                if (this._assignTo != value)
                {
                    this._assignTo = value;
                    this.OnPropertyChanged("AssignedTo");
                }
            }
        }

        /// <summary>
        /// Gets or sets the estimated time.
        /// </summary>
        /// <value>
        /// The estimated time.
        /// </value>
        public Int32 EstimatedTime
        {
            get
            {
                return this._estimatedTime;
            }

            set
            {
                if (this._estimatedTime != value)
                {
                    this._estimatedTime = value;
                    this.OnPropertyChanged("EstimatedTime");
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
