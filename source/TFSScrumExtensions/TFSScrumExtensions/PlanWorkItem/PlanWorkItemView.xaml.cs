//-----------------------------------------------------------------------
// <copyright file="PlanWorkItemView.cs" company="jpsfs.com">
//     Copyright © 2014 jpsfs.com . All rights reserved.
// </copyright>
// <Author>José Pedro Silva</Author>
//-----------------------------------------------------------------------

using JosePedroSilva.TFSScrumExtensions.BusinessObjects;
using JosePedroSilva.TFSScrumExtensions.Configuration;
using JosePedroSilva.TFSScrumExtensions.TeamFoundationClient;
using Microsoft.TeamFoundation.Controls;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TeamFoundation.WorkItemTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JosePedroSilva.TFSScrumExtensions.PlanWorkItem
{
    /// <summary>
    /// Interaction logic for PlanWorkItemView.xaml
    /// </summary>
    public partial class PlanWorkItemView : UserControl
    {
        #region Private Variables

        /// <summary>
        /// The _text changed
        /// </summary>
        private bool _textChanged = false;

        #endregion

        #region Public Variables
        #endregion

        #region Properties

        /// <summary>
        /// Gets the page model.
        /// </summary>
        /// <value>
        /// The page model.
        /// </value>
        public PlanWorkItemPageModel PageModel
        {
            get
            {
                return this.DataContext as PlanWorkItemPageModel;
            }
        }

        #endregion

        #region Constructors
        #endregion

        #region Private & Internal Methods

        private async void LoadWorkItems()
        {
            if(this.PageModel.AreWorkItemsSelected)
            {
                PlanWorkItemPageModel pgModel = this.PageModel;
                pgModel.IsBusy = true;
                // Load WorkItems
                await System.Threading.Tasks.Task.Run(() =>
                {
                    var workItems = pgModel.TfsClient.GetWorkItems(pgModel.WorkItemIds);

                    String aggregatedTitle = null;
                    String aggregatedIterationPath = null;
                    for (int i = 0; i < workItems.Length; i++)
                    {
                        if (aggregatedTitle == null)
                        {
                            aggregatedTitle = workItems[i].Title;
                        }
                        else if (aggregatedTitle != workItems[i].Title)
                        {
                            aggregatedTitle = "(multiple work items selected)";
                        }

                        if (aggregatedIterationPath == null)
                        {
                            aggregatedIterationPath = workItems[i].IterationPath;
                        }
                        else if (aggregatedIterationPath != workItems[i].IterationPath)
                        {
                            aggregatedIterationPath = "(multiple work items selected)";
                        }
                    }

                    pgModel.AggregatedWorkItemTitle = aggregatedTitle;
                    pgModel.AggregatedWorkItemIterationPath = aggregatedIterationPath;

                });

                pgModel.AreWorkItemsSelected = true;
                pgModel.IsBusy = false;
            }
        }

        /// <summary>
        /// Builds the planning templates.
        /// </summary>
        /// <param name="pageModel">The page model.</param>
        /// <param name="configuration">The configuration.</param>
        private void BuildPlanningTemplates(PlanWorkItemPageModel pageModel, ConfigurationRoot configuration)
        {
            if (configuration.Templates != null && configuration.Templates.Count > 0)
            {
                ObservableCollection<PlanningTemplate> planningTemplates = new ObservableCollection<PlanningTemplate>();

                foreach (Template template in configuration.Templates.OrderBy(x => x.DisplayOrder).ThenBy(x => x.DisplayName))
                {
                    planningTemplates.Add((PlanningTemplate)template);
                }

                pageModel.PlanningTemplates = planningTemplates;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanWorkItemView"/> class.
        /// </summary>
        public PlanWorkItemView()
        {
            InitializeComponent();
        }

        #endregion

        #region Event handling Methods

        /// <summary>
        /// Handles the Click event of the HyperlinkEditConfiguration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void HyperlinkEditConfiguration_Click(object sender, RoutedEventArgs e)
        {
            var dteService = Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
            if (dteService == null)
            {
                Debug.WriteLine("[PlanWorkItemView] DTE Service is null.");
                return;
            }

            dteService.ItemOperations.OpenFile(this.PageModel.ConfigurationFilePath, EnvDTE.Constants.vsViewKindAny);
        }

        /// <summary>
        /// Handles the Click event of the HyperlinkReloadConfiguration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void HyperlinkReloadConfiguration_Click(object sender, RoutedEventArgs e)
        {
            this.PageModel.IsBusy = true;

            var pageModel = this.PageModel;

            await System.Threading.Tasks.Task.Run(() =>
            {
                TFSScrumExtensionsPackage.LoadConfiguration();

                this.BuildPlanningTemplates(pageModel, ConfigurationManager.CurrentConfiguration);
            });

            this.DataContext = pageModel;

            this.PageModel.IsBusy = false;
        }

        /// <summary>
        /// Handles the Click event of the buttonCreateWorkItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void buttonCreateWorkItems_Click(object sender, RoutedEventArgs e)
        {
            this.PageModel.IsBusy = true;

            PlanWorkItemPageModel pgModel = this.PageModel;

            foreach (TaskTemplate taskTemplate in pgModel.SelectedPlanningTemplate.TasksToCreateCollection)
            {
                foreach (TaskTemplateInstance taskTemplateInstance in taskTemplate.InstancesCollection)
                {
                    if (String.IsNullOrWhiteSpace(taskTemplateInstance.AssignedTo))
                    {
                        var dlgResult = MessageBox.Show("There are instances without an assigned User. Do you want to continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);

                        if (dlgResult != MessageBoxResult.Yes)
                        {
                            this.PageModel.IsBusy = false;
                            return;
                        }
                    }
                }
            }

            await System.Threading.Tasks.Task.Run(() =>
            {
                var addedCreatedWorkItems = pgModel.TfsClient.CreateRelatedWorkItems(pgModel.WorkItemIds, pgModel.SelectedPlanningTemplate);

                if (pgModel.CreatedWorkItems != null)
                {
                    List<WorkItem> previousWorkItems = pgModel.CreatedWorkItems.ToList();
                    previousWorkItems.AddRange(addedCreatedWorkItems);

                    pgModel.CreatedWorkItems = new ObservableCollection<WorkItem>(previousWorkItems.OrderByDescending(x => x.CreatedDate));
                    
                }
                else
                {
                    pgModel.CreatedWorkItems = new ObservableCollection<WorkItem>(addedCreatedWorkItems.OrderByDescending(x => x.CreatedDate));
                }
            });

            this.PageModel.IsBusy = false;
        }

        private void HyperlinkGoToWorkItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Documents.Hyperlink hyper = sender as System.Windows.Documents.Hyperlink;
            WorkItem currentWorkItem = hyper.Tag as WorkItem;

            DocumentService documentService = Package.GetGlobalService(typeof(DocumentService)) as DocumentService;
            var doc = documentService.GetWorkItem(this.PageModel.TfsClient.GetTeamProjectCollection(), currentWorkItem.Id, new object());

            documentService.ShowWorkItem(doc);
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.BuildPlanningTemplates(this.PageModel, ConfigurationManager.CurrentConfiguration);
            this.LoadWorkItems();
        }

        private void buttonSelectWorkItems_Click(object sender, RoutedEventArgs e)
        {
            this.PageModel.AreWorkItemsSelected = true;

            this.LoadWorkItems();
        }

        #endregion

    }
}
