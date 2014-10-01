using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JosePedroSilva.TFSScrumExtensions.Configuration
{
    public class ConfigurationManager
    {
        #region Private Variables

        private const String defaultConfigurationPath = "";

        #endregion

        #region Public Variables
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current configuration.
        /// </summary>
        /// <value>
        /// The current configuration.
        /// </value>
        public static ConfigurationRoot CurrentConfiguration
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
        /// Loads the configuration.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        public static ConfigurationRoot Load(String filePath = defaultConfigurationPath)
        {
            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException(String.Format(Resources.Resources.Error_FileNotFound, filePath));
            }

            XmlSerializer serializer = new XmlSerializer(typeof(ConfigurationRoot));
            return serializer.Deserialize(File.OpenRead(filePath)) as ConfigurationRoot;
        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="configuration">The configuration.</param>
        public static void Save(ConfigurationRoot configuration, String filePath = defaultConfigurationPath)
        {
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(ConfigurationRoot));

            using (TextWriter textWriter = new StreamWriter(filePath))
            {
                serializer.Serialize(textWriter, configuration);
            }
        }

        #endregion

        #region Event handling Methods
        #endregion
    }
}
