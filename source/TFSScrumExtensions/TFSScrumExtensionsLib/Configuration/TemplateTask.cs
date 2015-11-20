using JosePedroSilva.TFSScrumExtensions.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace JosePedroSilva.TFSScrumExtensions.Configuration
{
    /// <summary>
    /// Template Task
    /// </summary>
    public class TemplateTask
    {
        #region Private Variables
        #endregion

        #region Public Variables
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [XmlElement]
        public String WorkItemType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the work item link.
        /// </summary>
        /// <value>
        /// The type of the work item link.
        /// </value>
        [XmlElement]
        public String WorkItemLinkType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [XmlElement]
        public Int32 Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [XmlElement]
        public String Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>
        /// The prefix.
        /// </value>
        [XmlElement]
        public String Prefix
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sufix.
        /// </summary>
        /// <value>
        /// The sufix.
        /// </value>
        [XmlElement]
        public String Sufix
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the custom properties.
        /// </summary>
        /// <value>
        /// The custom properties.
        /// </value>
        [XmlElement]
        public List<TemplateCustomProperty> CustomProperties
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is copy description enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is copy description enabled; otherwise, <c>false</c>.
        /// </value>
        [XmlElement]
        public Boolean IsCopyDescriptionEnabled
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
        /// Performs an explicit conversion from <see cref="TemplateTask"/> to <see cref="TaskTemplate"/>.
        /// </summary>
        /// <param name="templateTask">The template task.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static explicit operator TaskTemplate(TemplateTask templateTask)
        {
            ObservableCollection<TaskTemplateCustomProperty> taskTemplateCustomProperties = null;

            if (templateTask.CustomProperties != null && templateTask.CustomProperties.Count > 0)
            {
                taskTemplateCustomProperties = new ObservableCollection<TaskTemplateCustomProperty>();

                foreach (TemplateCustomProperty templateCustomProperty in templateTask.CustomProperties)
                {
                    taskTemplateCustomProperties.Add((TaskTemplateCustomProperty)templateCustomProperty);
                }
            }

            TaskTemplate taskTemplate = new TaskTemplate()
            {
                Title = templateTask.Title,
                Prefix = templateTask.Prefix,
                Sufix = templateTask.Sufix,
                Quantity = templateTask.Quantity,
                IsCopyDescriptionEnabled = templateTask.IsCopyDescriptionEnabled,
                WorkItemType = templateTask.WorkItemType,
                WorkItemLinkType = templateTask.WorkItemLinkType,
                CustomProperties = taskTemplateCustomProperties
            };

            return taskTemplate;
        }

        #endregion

        #region Event handling Methods
        #endregion
    }
}
