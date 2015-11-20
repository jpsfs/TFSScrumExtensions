using System;
using System.Xml.Serialization;

namespace JosePedroSilva.TFSScrumExtensions.Configuration
{
    public class WorkItemFieldMatch
    {
        #region Private Variables
        #endregion

        #region Public Variables
        #endregion

        #region Properties

        [XmlElement]
        public WorkItemField Field
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
        #endregion

        #region Event handling Methods
        #endregion
    }
}
