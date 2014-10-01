using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JosePedroSilva.TFSScrumExtensions.BusinessObjects
{
    public class TaskTemplate : INotifyPropertyChanged
    {
        #region Private Variables

        /// <summary>
        /// The _quantity
        /// </summary>
        private Int32 _quantity;

        private String _title;
        private String _prefix;
        private String _sufix;
        private String _workItemType;
        private String _workItemLinkType;
        private Boolean _isCopyDescriptionEnabled;

        #endregion

        #region Public Variables

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public Int32 Quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                if (this._quantity != value)
                {
                    this.UpdateInstancesCollection(this._quantity, value);

                    this._quantity = value;
                    this.OnPropertyChanged("Quantity");
                }
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public String Title
        {
            get
            {
                return this._title;
            }
            set
            {
                if (this._title != value)
                {
                    this._title = value;
                    this.OnPropertyChanged("Title");
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of the work item.
        /// </summary>
        /// <value>
        /// The type of the work item.
        /// </value>
        public String WorkItemType
        {
            get
            {
                return this._workItemType;
            }

            set
            {
                if (this._workItemType != value)
                {
                    this._workItemType = value;
                    this.OnPropertyChanged("WorkItemType");
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of the work item link.
        /// </summary>
        /// <value>
        /// The type of the work item link.
        /// </value>
        public String WorkItemLinkType
        {
            get
            {
                return this._workItemLinkType;
            }

            set
            {
                if (this._workItemLinkType != value)
                {
                    this._workItemLinkType = value;
                    this.OnPropertyChanged("WorkItemLinkType");
                }
            }
        }

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>
        /// The prefix.
        /// </value>
        public String Prefix
        {
            get
            {
                return this._prefix;
            }
            set
            {
                if (this._prefix != value)
                {
                    this._prefix = value;
                    this.OnPropertyChanged("Prefix");
                }
            }
        }

        /// <summary>
        /// Gets or sets the sufix.
        /// </summary>
        /// <value>
        /// The sufix.
        /// </value>
        public String Sufix
        {
            get
            {
                return this._sufix;
            }
            set
            {
                if (this._sufix != value)
                {
                    this._sufix = value;
                    this.OnPropertyChanged("Sufix");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is to copy description.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is to copy description; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsCopyDescriptionEnabled
        {
            get
            {
                return this._isCopyDescriptionEnabled;
            }
            set
            {
                if (this._isCopyDescriptionEnabled != value)
                {
                    this._isCopyDescriptionEnabled = value;
                    this.OnPropertyChanged("IsCopyDescriptionEnabled");
                }
            }
        }

        /// <summary>
        /// Gets or sets the custom properties.
        /// </summary>
        /// <value>
        /// The custom properties.
        /// </value>
        public ObservableCollection<TaskTemplateCustomProperty> CustomProperties
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the instances collection.
        /// </summary>
        /// <value>
        /// The instances collection.
        /// </value>
        public ObservableCollection<TaskTemplateInstance> InstancesCollection
        {
            get;
            set;
        }

        #endregion

        #region Constructors
        #endregion

        #region Private & Internal Methods

        private void UpdateInstancesCollection(int oldValue, int newValue)
        {
            if (this.InstancesCollection == null)
            {
                this.InstancesCollection = new ObservableCollection<TaskTemplateInstance>();
            }

            if (oldValue != this.InstancesCollection.Count)
            {
                throw new InvalidOperationException("Old Value doesn't match the know instances collection size");
            }

            if (oldValue == newValue) return;

            if (oldValue > newValue)
            {
                for (int i = oldValue; i > newValue; i--)
                {
                    this.InstancesCollection.RemoveAt(i - 1);
                }

            }
            else
            {
                for (int i = newValue - oldValue; i > 0; i--)
                {
                    this.InstancesCollection.Add(new TaskTemplateInstance());
                }
            }
        }

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
