using JosePedroSilva.TFSScrumExtensions.BusinessObjects;
using JosePedroSilva.TFSScrumExtensions.Configuration;
using JosePedroSilva.TFSScrumExtensions.TeamFoundationClient;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Controls;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TeamFoundation;
using Microsoft.VisualStudio.TeamFoundation.WorkItemTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JosePedroSilva.TFSScrumExtensions.PlanWorkItem
{
    /// <summary>
    /// 
    /// </summary>
    public class PlanWorkItemController
    {
        #region Private Variables

        private TeamFoundationServerExt teamFoundationServerExt;

        #endregion

        #region Public Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanWorkItemController"/> class.
        /// </summary>
        public PlanWorkItemController()
        {

        }

        #endregion

        #region Private & Internal Methods
        #endregion

        #region Public Methods

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        public void OnPlanWorkItem(object sender, EventArgs e)
        {
            Debug.WriteLine("WorkItemSearchReplace command invoked");

            var dteService = Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
            if (dteService == null)
            {
                Debug.WriteLine("[PlanWorkItemController] DTE Service is null.");
                return;
            }

            DocumentService documentService = Package.GetGlobalService(typeof(DocumentService)) as DocumentService;
            if (documentService == null)
            {
                Debug.WriteLine("[PlanWorkItemController] Document Service is null.");
                return;
            }

            string fullName = dteService.ActiveDocument.FullName;
            
            IWorkItemTrackingDocument activeDocument = documentService.FindDocument(fullName, null);
            if (activeDocument == null || !(activeDocument is IResultsDocument))
            {
                Debug.WriteLine("[PlanWorkItemController] Active Document is null or doesn't implement IResultsDocument");
                return;
            }

            int[] selectedItemIds = ((IResultsDocument)activeDocument).SelectedItemIds;

            if (selectedItemIds == null || selectedItemIds.Length == 0)
            {
                Debug.WriteLine("[PlanWorkItemController] Selected Work Items Ids is null or empty.");
                return;
            }

            this.OnPlanWorkItem(selectedItemIds);

        }

        /// <summary>
        /// Called when [plan work item].
        /// </summary>
        /// <param name="workItemIds">The work item ids.</param>
        public void OnPlanWorkItem(int[] workItemIds, bool areWorkItemsSelected = true)
        {
            var dteService = Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
            if (dteService == null)
            {
                Debug.WriteLine("[PlanWorkItemController] DTE Service is null.");
                return;
            }

            var teamExplorer = (ITeamExplorer)(Package.GetGlobalService(typeof(ITeamExplorer)));

            teamFoundationServerExt = (dteService.GetObject("Microsoft.VisualStudio.TeamFoundation.TeamFoundationServerExt") as TeamFoundationServerExt);

            TfsClient tfsClient = new TfsClient(teamFoundationServerExt);

            teamExplorer.NavigateToPage(new Guid(PlanWorkItemPage.PageId), new PlanWorkItemPageModel()
            {
                WorkItemIds = workItemIds,
                UsersAvailableForAssign = tfsClient.GetUsers(),
                ConfigurationFilePath = TFSScrumExtensionsPackage.ConfigurationFilePath,
                AvailableWorkItemTypes = tfsClient.GetWorkItemsTypes(),
                AvailableWorkItemLinkTypes = tfsClient.GetWorkItemLinkTypes(),
                TfsClient = tfsClient,
                AreWorkItemsSelected = areWorkItemsSelected
            });
        }

        #endregion

        #region Event handling Methods
        #endregion
    }
}
