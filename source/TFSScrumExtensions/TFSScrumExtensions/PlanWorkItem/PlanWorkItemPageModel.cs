using JosePedroSilva.TFSScrumExtensions.BusinessObjects;
using JosePedroSilva.TFSScrumExtensions.TeamFoundationClient;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JosePedroSilva.TFSScrumExtensions.PlanWorkItem
{
    public class PlanWorkItemPageModel : BasePageModel
    {
        #region Private Variables

        private Int32[] workItemIds;
        private WorkItem[] _workItems;
        private List<TeamFoundationIdentity> _usersAvailableForAssign;
        private WorkItemTypeCollection _availableWorkItemTypes;
        private WorkItemLinkTypeCollection _availableWorkItemLinkTypes;

        private PlanningTemplate _selectedPlanningTemplate;

        private String _configurationFilePath;
        private ObservableCollection<WorkItem> _createdWorkItems;
        private Visibility _createdWorkItemsVisibility = Visibility.Collapsed;

        /// <summary>
        /// The _is busy
        /// </summary>
        private Boolean _isBusy;
        private ObservableCollection<PlanningTemplate> _planningTemplates;

        private Boolean _areWorkItemsSelected;
        private String _aggregatedWorkItemTitle = "(no information available at this moment)";
        private String _aggregatedWorkItemIterationPath = "(no information available at this moment)";

        #endregion

        #region Public Variables
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the work item ids.
        /// </summary>
        /// <value>
        /// The work item ids.
        /// </value>
        public Int32[] WorkItemIds
        {
            get
            {
                return this.workItemIds;
            }

            set
            {
                this.workItemIds = value;
                this.OnPropertyChanged("WorkItemIds");
            }
        }

        /// <summary>
        /// Gets or sets the planning templates.
        /// </summary>
        /// <value>
        /// The planning templates.
        /// </value>
        public ObservableCollection<PlanningTemplate> PlanningTemplates
        {
            get
            {
                return this._planningTemplates;
            }
            set
            {
                this._planningTemplates = value;
                this.OnPropertyChanged("PlanningTemplates");

                if(this._planningTemplates != null && this._planningTemplates.Count > 0)
                {
                    this.SelectedPlanningTemplate = this._planningTemplates[0];
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected planning template.
        /// </summary>
        /// <value>
        /// The selected planning template.
        /// </value>
        public PlanningTemplate SelectedPlanningTemplate
        {
            get
            {
                return this._selectedPlanningTemplate;
            }

            set
            {
                this._selectedPlanningTemplate = value;
                this.OnPropertyChanged("SelectedPlanningTemplate");
            }
        }

        /// <summary>
        /// Gets or sets the configuration file path.
        /// </summary>
        /// <value>
        /// The configuration file path.
        /// </value>
        public String ConfigurationFilePath
        {
            get
            {
                return this._configurationFilePath;
            }

            set
            {
                if (this._configurationFilePath != value)
                {
                    this._configurationFilePath = value;
                    this.OnPropertyChanged("ConfigurationFilePath");
                }
            }
        }

        /// <summary>
        /// Gets or sets the users available for assign.
        /// </summary>
        /// <value>
        /// The users available for assign.
        /// </value>
        public List<TeamFoundationIdentity> UsersAvailableForAssign
        {
            get
            {
                return this._usersAvailableForAssign;
            }

            set
            {
                this._usersAvailableForAssign = value;
                this.OnPropertyChanged("UsersAvailableForAssign");
            }
        }

        /// <summary>
        /// Gets or sets the available work item types.
        /// </summary>
        /// <value>
        /// The available work item types.
        /// </value>
        public WorkItemTypeCollection AvailableWorkItemTypes
        {
            get
            {
                return this._availableWorkItemTypes;
            }
            set
            {
                this._availableWorkItemTypes = value;
                this.OnPropertyChanged("AvailableWorkItemTypes");
            }
        }

        /// <summary>
        /// Gets or sets the available work item link types.
        /// </summary>
        /// <value>
        /// The available work item link types.
        /// </value>
        public WorkItemLinkTypeCollection AvailableWorkItemLinkTypes
        {
            get
            {
                return this._availableWorkItemLinkTypes;
            }

            set
            {
                this._availableWorkItemLinkTypes = value;
                this.OnPropertyChanged("AvailableWorkItemLinkTypes");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsBusy
        {
            get
            {
                return this._isBusy;
            }
            set
            {
                if (this._isBusy != value)
                {
                    this._isBusy = value;
                    this.OnPropertyChanged("IsBusy");
                }
            }
        }

        /// <summary>
        /// Gets or sets the plan work item controller.
        /// </summary>
        /// <value>
        /// The plan work item controller.
        /// </value>
        public TfsClient TfsClient
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the created work items.
        /// </summary>
        /// <value>
        /// The created work items.
        /// </value>
        public ObservableCollection<WorkItem> CreatedWorkItems
        {
            get
            {
                return this._createdWorkItems;
            }
            set
            {
                this._createdWorkItems = value;

                this.CreatedWorkItemsVisibility = this._createdWorkItems != null && this._createdWorkItems.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

                this.OnPropertyChanged("CreatedWorkItems");
            }
        }

        /// <summary>
        /// Gets or sets the created work items visibility.
        /// </summary>
        /// <value>
        /// The created work items visibility.
        /// </value>
        public Visibility CreatedWorkItemsVisibility
        {
            get
            {
                return this._createdWorkItemsVisibility;
            }

            set
            {
                if (this._createdWorkItemsVisibility != value)
                {
                    this._createdWorkItemsVisibility = value;
                    this.OnPropertyChanged("CreatedWorkItemsVisibility");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow work item declaration].
        /// </summary>
        /// <value>
        /// <c>true</c> if [allow work item declaration]; otherwise, <c>false</c>.
        /// </value>
        public Boolean AreWorkItemsSelected
        {
            get
            {
                return this._areWorkItemsSelected;
            }
            set
            {
                if(this._areWorkItemsSelected != value)
                {
                    this._areWorkItemsSelected = value;
                    this.OnPropertyChanged("AreWorkItemsSelected");
                }
            }
        }

        public String AggregatedWorkItemTitle
        {
            get
            {
                return this._aggregatedWorkItemTitle;
            }
            set
            {
                if (this._aggregatedWorkItemTitle != value)
                {
                    this._aggregatedWorkItemTitle = value;
                    this.OnPropertyChanged("AggregatedWorkItemTitle");
                }
            }
        }

        public String AggregatedWorkItemIterationPath
        {
            get
            {
                return this._aggregatedWorkItemIterationPath;
            }
            set
            {
                if (this._aggregatedWorkItemIterationPath != value)
                {
                    this._aggregatedWorkItemIterationPath = value;
                    this.OnPropertyChanged("AggregatedWorkItemIterationPath");
                }
            }
        }

        /// <summary>
        /// Gets or sets the work items.
        /// </summary>
        /// <value>
        /// The work items.
        /// </value>
        public WorkItem[] WorkItems
        {
            get
            {
                return this._workItems;
            }

            set
            {
                this._workItems = value;
                this.OnPropertyChanged("WorkItems");
            }
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
