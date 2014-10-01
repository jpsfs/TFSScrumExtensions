using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JosePedroSilva.TFSScrumExtensions.Configuration
{
    /// <summary>
    /// Root Configuration Class
    /// </summary>
    [XmlRoot("TFSScrumExtensionsConfiguration")]
    public class ConfigurationRoot
    {
        #region Private Variables
        #endregion

        #region Public Variables
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the user groups.
        /// </summary>
        /// <value>
        /// The user groups.
        /// </value>
        [XmlElement]
        public List<String> UserGroups
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the work item field matches.
        /// </summary>
        /// <value>
        /// The work item field matches.
        /// </value>
        [XmlElement]
        public List<WorkItemFieldMatch> WorkItemFieldMatches
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the templates.
        /// </summary>
        /// <value>
        /// The templates.
        /// </value>
        [XmlElement]
        public List<Template> Templates
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
