using Microsoft.TeamFoundation.Controls;
using Microsoft.TeamFoundation.Controls.WPF.TeamExplorer;
using System;
using System.Diagnostics;

namespace JosePedroSilva.TFSScrumExtensions.PlanWorkItem
{
    /// <summary>
    /// Plan WorkItem Page
    /// </summary>
    [TeamExplorerPage(PlanWorkItemPage.PageId)]
    public class PlanWorkItemPage : TeamExplorerPageBase
    {
        #region Private Variables

        /// <summary>
        /// The _is busy
        /// </summary>
        private Boolean _isBusy;

        /// <summary>
        /// The _stored page model
        /// </summary>
        private static PlanWorkItemPageModel _storedPageModel;

        #endregion

        #region Public Variables

        /// <summary>
        /// The page identifier
        /// </summary>
        public const string PageId = "45dd429a-4fba-451a-b2e2-d2aef452637b";

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        public override bool IsBusy
        {
            get
            {
                return _isBusy;
            }
        }

        #endregion

        #region Constructors
        #endregion

        #region Private & Internal Methods
        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PageInitializeEventArgs"/> instance containing the event data.</param>
        public override void Initialize(object sender, PageInitializeEventArgs e)
        {
            base.Initialize(sender, e);

            this.Title = Resources.PlanWorkItemPage_Title;

            PlanWorkItemPageModel pageModel = e.Context as PlanWorkItemPageModel;

            if (pageModel == null)
            {
                pageModel = _storedPageModel;

                if (pageModel == null)
                {
                    Debug.WriteLine("[PlanWorkItemPage] CreateViewModel: e.Context must be of type PlanWorkItemPageModel");
                    return;
                }
            }
            else
            {
                _storedPageModel = pageModel;
            }

            pageModel.PropertyChanged -= pageModel_PropertyChanged;
            pageModel.PropertyChanged += pageModel_PropertyChanged;

            // Assign the WPF Control
            this.PageContent = new PlanWorkItemView() { DataContext = pageModel };

        }

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <param name="e">The <see cref="PageInitializeEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        protected override ITeamExplorerPage CreateViewModel(PageInitializeEventArgs e)
        {
            return base.CreateViewModel(e);
        }

        #endregion

        #region Event handling Methods

        /// <summary>
        /// Handles the PropertyChanged event of the pageModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void pageModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            PlanWorkItemPageModel pgModel = sender as PlanWorkItemPageModel;

            switch (e.PropertyName)
            {
                case "IsBusy":
                    this._isBusy = pgModel.IsBusy;
                    this.RaisePropertyChanged("IsBusy");
                    break;
            }
        }

        #endregion
    }
}
