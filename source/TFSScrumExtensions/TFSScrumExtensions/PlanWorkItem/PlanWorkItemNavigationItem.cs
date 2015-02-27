using Microsoft.TeamFoundation.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Controls.WPF.TeamExplorer;
using JosePedroSilva.TFSScrumExtensions.Base;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Shell;

namespace JosePedroSilva.TFSScrumExtensions.PlanWorkItem
{
    [TeamExplorerNavigationItem(PlanWorkItemNavigationItem.NavigationItemId, Int32.MaxValue)]
    public class PlanWorkItemNavigationItem : TeamExplorerBaseNavigationItem
    {
        #region Private Variables
        #endregion

        #region Public Variables

        /// <summary>
        /// The page identifier
        /// </summary>
        public const string NavigationItemId = "05521739-7465-4365-8749-c8a4d7f2c0aa";

        #endregion

        #region Properties
        #endregion

        #region Constructors

        [ImportingConstructor]
        public PlanWorkItemNavigationItem([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.Text = TFSScrumExtensions.Resources.PlanWorkItem_TeamExplorer_Home;
            this.IsVisible = true;
            this.Image = TFSScrumExtensions.Resources.PlanWorkItemNavigationItem;
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
                TFSScrumExtensions.TFSScrumExtensionsPackage.InternalInitialize();

                PlanWorkItemController controller = new PlanWorkItemController();
                controller.OnPlanWorkItem(new int[] { }, false);
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
