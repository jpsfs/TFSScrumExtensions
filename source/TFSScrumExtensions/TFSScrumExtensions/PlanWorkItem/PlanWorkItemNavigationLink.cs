using JosePedroSilva.TFSScrumExtensions.Base;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Composition;

namespace JosePedroSilva.TFSScrumExtensions.PlanWorkItem
{
    //[TeamExplorerNavigationLink(PlanWorkItemNavigationLink.NavigationLinkId, TeamExplorerNavigationItemIds.MyWork, 200)]
    public class PlanWorkItemNavigationLink : TeamExplorerBaseNavigationLink
    {
        #region Private Variables
        #endregion

        #region Public Variables

        /// <summary>
        /// The page identifier
        /// </summary>
        public const string NavigationLinkId = "05521739-7465-4365-8749-c8a4d7f2c0aa";

        #endregion

        #region Properties
        #endregion

        #region Constructors

        [ImportingConstructor]
        public PlanWorkItemNavigationLink([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.Text = TFSScrumExtensions.Resources.PlanWorkItem_TeamExplorer_Home;
            this.IsVisible = true;
            this.IsEnabled = true;
        }

        #endregion

        #region Private & Internal Methods
        #endregion

        #region Public Methods

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public override void Execute()
        {
            try
            {
                ITeamExplorer teamExplorer = GetService<ITeamExplorer>();
                if (teamExplorer != null)
                {
                    teamExplorer.NavigateToPage(new Guid(PlanWorkItemPage.PageId), null);
                }
            }
            catch (Exception ex)
            {
                this.ShowNotification(ex.Message, NotificationType.Error);
            }
        }

        #endregion

        #region Event handling Methods
        #endregion
    }
}
