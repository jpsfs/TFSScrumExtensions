using JosePedroSilva.TFSScrumExtensions.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JosePedroSilva.TFSScrumExtensions.Configuration
{
    /// <summary>
    /// [Configuration]
    /// 
    /// Template
    /// </summary>
    public class Template
    {
        #region Private Variables
        #endregion

        #region Public Variables
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        [XmlAttribute]
        public String DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        /// <value>
        /// The display order.
        /// </value>
        [XmlAttribute]
        public Int32 DisplayOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the template tasks.
        /// </summary>
        /// <value>
        /// The template tasks.
        /// </value>
        [XmlElement]
        public List<TemplateTask> TemplateTasks
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
        /// Performs an explicit conversion from <see cref="Template"/> to <see cref="PlanningTemplate"/>.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static explicit operator PlanningTemplate(Template template)
        {
            ObservableCollection<TaskTemplate> tasksToCreate = null;

            if (template.TemplateTasks != null && template.TemplateTasks.Count > 0)
            {
                tasksToCreate = new System.Collections.ObjectModel.ObservableCollection<TaskTemplate>();

                foreach (TemplateTask templateTask in template.TemplateTasks)
                {
                    tasksToCreate.Add((TaskTemplate)templateTask);
                }
            }

            PlanningTemplate returnValue = new PlanningTemplate()
            {
                DisplayName = template.DisplayName,
                DisplayOrder = template.DisplayOrder,
                TasksToCreateCollection = tasksToCreate
            };

            return returnValue;
        }

        #endregion

        #region Event handling Methods
        #endregion
    }
}
