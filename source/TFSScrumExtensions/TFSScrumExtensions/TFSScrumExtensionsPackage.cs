using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.TeamFoundation.WorkItemTracking.Extensibility;
using JosePedroSilva.TFSScrumExtensions.PlanWorkItem;
using Microsoft.TeamFoundation.Common;
using Microsoft.VisualStudio.TeamFoundation.WorkItemTracking;
using JosePedroSilva.TFSScrumExtensions.Configuration;
using System.IO;

namespace JosePedroSilva.TFSScrumExtensions
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideBindingPath]
    [Guid(GuidList.guidTFSScrumExtensionsPkgString)]
    public sealed class TFSScrumExtensionsPackage : Package
    {
        #region Private Variables

        private static string _configurationFilePath;

        #endregion

        #region Public Variables

        #endregion

        #region Properties

        /// <summary>
        /// Gets the configuration file path.
        /// </summary>
        /// <value>
        /// The configuration file path.
        /// </value>
        public static String ConfigurationFilePath
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_configurationFilePath))
                {
                    _configurationFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"TFSScrumExtensions\DefaultConfiguration.xml");
                }

                return _configurationFilePath;
            }
        }

        #endregion

        #region Constructors
        #endregion

        #region Private & Internal Methods

        #endregion

        #region Public Methods

        //
        // Summary:
        //     Gets type-based services from the VSPackage service container.
        //
        // Parameters:
        //   serviceType:
        //     The type of service to retrieve.
        //
        // Returns:
        //     An instance of the requested service, or null if the service could not be
        //     found.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     serviceType is null.
        public object GetService(Type serviceType)
        {
            return base.GetService(serviceType);
        }

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void LoadConfiguration(string path = null)
        {
            if(path == null)
            {
                path = ConfigurationFilePath;
            }

            ConfigurationManager.CurrentConfiguration = ConfigurationManager.Load(path);
        }

        #endregion

        #region Event handling Methods
        #endregion

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public TFSScrumExtensionsPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }



        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            Debug.WriteLine("Loading Configuration");

            string configFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"TFSScrumExtensions\DefaultConfiguration.xml");
            if (!File.Exists(configFilePath))
            {
                ConfigurationManager.Save(DefaultConfiguration.DefaultConfigurations.Default, configFilePath);
            }

            ConfigurationManager.CurrentConfiguration = ConfigurationManager.Load(configFilePath);

            #region PlanWorkItemController

            PlanWorkItemController planWorkItemController = new PlanWorkItemController();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = this.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the menu item.
                CommandID menuCommandID = new CommandID(GuidList.guidTFSScrumExtensionsCmdSet, (int)PkgCmdIDList.cmdPlanWorkItem);
                MenuCommand menuItem = new MenuCommand(planWorkItemController.OnPlanWorkItem, menuCommandID);
                mcs.AddCommand(menuItem);
            }

            #endregion
            
        }
        
        #endregion

    }
}
