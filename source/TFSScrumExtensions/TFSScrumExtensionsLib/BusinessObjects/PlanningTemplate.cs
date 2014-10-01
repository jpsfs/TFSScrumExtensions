using JosePedroSilva.TFSScrumExtensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JosePedroSilva.TFSScrumExtensions.BusinessObjects
{
    /// <summary>
    /// Planning Template
    /// </summary>
    public class PlanningTemplate : INotifyPropertyChanged
    {
        #region Private Variables

        private String _displayName;
        private Int32 _displayOrder;

        #endregion

        #region Public Variables

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public String DisplayName
        {
            get
            {
                return this._displayName;
            }

            set
            {
                if (this._displayName != value)
                {
                    this._displayName = value;
                    this.OnPropertyChanged("DisplayName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        /// <value>
        /// The display order.
        /// </value>
        public Int32 DisplayOrder
        {
            get
            {
                return this._displayOrder;
            }
            set
            {
                if (this._displayOrder != value)
                {
                    this._displayOrder = value;
                    this.OnPropertyChanged("DisplayOrder");
                }
            }
        }

        /// <summary>
        /// Gets or sets the planning templates.
        /// </summary>
        /// <value>
        /// The planning templates.
        /// </value>
        public ObservableCollection<TaskTemplate> TasksToCreateCollection
        {
            get;
            set;
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
