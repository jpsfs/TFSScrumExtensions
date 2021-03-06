﻿using JosePedroSilva.TFSScrumExtensions.TeamFoundationClient;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TeamFoundation;
using Microsoft.VisualStudio.TeamFoundation.WorkItemTracking;
using System;
using System.Diagnostics;

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

        public event EventHandler TeamFoundationProjectChanged;

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanWorkItemController"/> class.
        /// </summary>
        public PlanWorkItemController()
        {
            var dteService = Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
            if (dteService == null)
            {
                Debug.WriteLine("[PlanWorkItemController] DTE Service is null.");
                return;
            }

            var teamExplorer = (ITeamExplorer)(Package.GetGlobalService(typeof(ITeamExplorer)));

            teamFoundationServerExt = (dteService.GetObject("Microsoft.VisualStudio.TeamFoundation.TeamFoundationServerExt") as TeamFoundationServerExt);
            teamFoundationServerExt.ProjectContextChanged +=teamFoundationServerExt_ProjectContextChanged;
        }

        private void teamFoundationServerExt_ProjectContextChanged(object sender, EventArgs e)
        {
            if(this.TeamFoundationProjectChanged != null)
            {
                this.TeamFoundationProjectChanged(this, e);
            }
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

        /// <summary>
        /// Determines whether this instance is connected.
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            var dteService = Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
            if (dteService == null)
            {
                Debug.WriteLine("[PlanWorkItemController] DTE Service is null.");
                return false;
            }

            var teamExplorer = (ITeamExplorer)(Package.GetGlobalService(typeof(ITeamExplorer)));

            teamFoundationServerExt = (dteService.GetObject("Microsoft.VisualStudio.TeamFoundation.TeamFoundationServerExt") as TeamFoundationServerExt);
            TfsClient tfsClient = new TfsClient(teamFoundationServerExt);

            return tfsClient.IsTeamProjectConnnected();
        }



        #endregion

        #region Event handling Methods
        #endregion
    }
}
