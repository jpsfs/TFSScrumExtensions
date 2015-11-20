using JosePedroSilva.TFSScrumExtensions.BusinessObjects;
using System;
using System.Xml.Serialization;

namespace JosePedroSilva.TFSScrumExtensions.Configuration
{
    /// <summary>
    /// Template Custom Property
    /// </summary>
    public class TemplateCustomProperty
    {
        #region Private Variables
        #endregion

        #region Public Variables
        #endregion

        #region Properties

        [XmlElement]
        public String Name
        {
            get;
            set;
        }

        [XmlElement]
        public String Value
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
        /// Performs an explicit conversion from <see cref="TemplateCustomProperty"/> to <see cref="TaskTemplateCustomProperty"/>.
        /// </summary>
        /// <param name="templateCustomProperty">The template custom property.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static explicit operator TaskTemplateCustomProperty(TemplateCustomProperty templateCustomProperty)
        {
            TaskTemplateCustomProperty returnValue = new TaskTemplateCustomProperty()
            {
                Name = templateCustomProperty.Name,
                Value = templateCustomProperty.Value
            };

            return returnValue;
        }

        #endregion

        #region Event handling Methods
        #endregion
    }
}
