using JosePedroSilva.TFSScrumExtensions.BusinessObjects;
using JosePedroSilva.TFSScrumExtensions.Configuration;
using JosePedroSilva.TFSScrumExtensions.Extensions;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.VisualStudio.TeamFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JosePedroSilva.TFSScrumExtensions.TeamFoundationClient
{
    public class TfsClient
    {
        #region Private Variables

        /// <summary>
        /// The _TFS server
        /// </summary>
        private TeamFoundationServerExt _tfsServer;

        #endregion

        #region Public Variables
        #endregion

        #region Properties

        /// <summary>
        /// Gets the TFS server.
        /// </summary>
        /// <value>
        /// The TFS server.
        /// </value>
        public TeamFoundationServerExt TfsServer
        {
            get
            {
                return this._tfsServer;
            }

            private set
            {
                this._tfsServer = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsClient"/> class.
        /// </summary>
        /// <param name="tfsServer">The TFS server.</param>
        public TfsClient(TeamFoundationServerExt tfsServer)
        {
            this.TfsServer = tfsServer;
        }

        #endregion

        #region Private & Internal Methods
        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
        public List<TeamFoundationIdentity> GetUsers()
        {
            TfsTeamProjectCollection projectCollection = this.GetTeamProjectCollection();
            ICommonStructureService css = (ICommonStructureService)projectCollection.GetService(typeof(ICommonStructureService));
            IGroupSecurityService gss = projectCollection.GetService<IGroupSecurityService>();
            IIdentityManagementService ims = projectCollection.GetService<IIdentityManagementService>(); 

            // get the tfs project
            var projectList = css.ListAllProjects();
            var project = projectList.FirstOrDefault(o => o.Name.Equals(this.TfsServer.ActiveProjectContext.ProjectName, StringComparison.InvariantCultureIgnoreCase));

            // project doesn't exist
            if (project == null) return null;

            // get the tfs group
            var groupList = gss.ListApplicationGroups(project.Uri);

            List<Identity> groups;
            if (ConfigurationManager.CurrentConfiguration.UserGroups != null && ConfigurationManager.CurrentConfiguration.UserGroups.Count > 0)
            {
                groups = groupList.Where(o => ConfigurationManager.CurrentConfiguration.UserGroups.Contains(o.AccountName)).ToList();  // you can also use DisplayName
            }
            else
            {
                groups = groupList.ToList();
            }

            List<TeamFoundationIdentity> contributors = new List<TeamFoundationIdentity>();
            foreach (Identity group in groups)
            {
                Identity sids = gss.ReadIdentity(SearchFactor.Sid, group.Sid, QueryMembership.Expanded);

                // there are no users
                if (sids.Members.Length == 0) continue;

                // convert to a list
                contributors.AddRange(ims.ReadIdentities(IdentitySearchFactor.Identifier, sids.Members, MembershipQuery.Direct, ReadIdentityOptions.None).SelectMany(x => x).Where(x => x.IsContainer == false));
            }

            return contributors.GroupBy(x => x.DisplayName).Select(g => g.First()).OrderBy(x => x.DisplayName).ToList();
        }

        /// <summary>
        /// Ges the work items types.
        /// </summary>
        /// <param name="serverExt">The server ext.</param>
        /// <returns></returns>
        public WorkItemTypeCollection GetWorkItemsTypes()
        {
            TfsTeamProjectCollection projectCollection = this.GetTeamProjectCollection();
            WorkItemStore store = projectCollection.GetService<WorkItemStore>();

            return store.Projects[this.TfsServer.ActiveProjectContext.ProjectName].WorkItemTypes;
        }

        /// <summary>
        /// Gets the work item link types.
        /// </summary>
        /// <returns></returns>
        public WorkItemLinkTypeCollection GetWorkItemLinkTypes()
        {
            TfsTeamProjectCollection projectCollection = this.GetTeamProjectCollection();
            WorkItemStore store = projectCollection.GetService<WorkItemStore>();

            return store.WorkItemLinkTypes;
        }

        /// <summary>
        /// Gets the team project collection.
        /// </summary>
        /// <returns></returns>
        public TfsTeamProjectCollection GetTeamProjectCollection()
        {
            return TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(this.TfsServer.ActiveProjectContext.DomainUri));
        }

        /// <summary>
        /// Creates the work items.
        /// </summary>
        /// <param name="baseWorkItemIds">The base work item ids.</param>
        /// <param name="selectedPlanningTemplate">The selected planning template.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Invalid WorkItemType
        /// or
        /// or
        /// Invalid WorkItemLinkType
        /// </exception>
        public WorkItem[] CreateRelatedWorkItems(int[] baseWorkItemIds, PlanningTemplate selectedPlanningTemplate)
        {
            TfsTeamProjectCollection projectCollection = this.GetTeamProjectCollection();
            WorkItemStore store = projectCollection.GetService<WorkItemStore>();

            Dictionary<WorkItemField, String> workItemFieldMatches = new Dictionary<WorkItemField, string>(ConfigurationManager.CurrentConfiguration.WorkItemFieldMatches.Count);

            // Load default fields from configuration
            foreach (WorkItemFieldMatch workItemFieldMatch in ConfigurationManager.CurrentConfiguration.WorkItemFieldMatches)
            {
                workItemFieldMatches.Add(workItemFieldMatch.Field, workItemFieldMatch.Value);
            }


            List<WorkItem> workItemsToSave = new List<WorkItem>();
            foreach (int workItemId in baseWorkItemIds)
            {
                WorkItem baseWorkItem = store.GetWorkItem(workItemId);
                Project project = baseWorkItem.Project;

                foreach (TaskTemplate taskTemplate in selectedPlanningTemplate.TasksToCreateCollection)
                {
                    // If Task Quantity is zero, ignore it
                    if (taskTemplate.Quantity <= 0) continue;

                    #region Get Work Item Type

                    // Check if the Work Item Type exists
                    if (!project.WorkItemTypes.Contains(taskTemplate.WorkItemType))
                    {
                        throw new ArgumentException("Invalid WorkItemType");
                    }

                    WorkItemType wiType = project.WorkItemTypes[taskTemplate.WorkItemType];

                    #endregion

                    // Predefined fields match
                    foreach (KeyValuePair<WorkItemField, String> fields in workItemFieldMatches)
                    {
                        if (!wiType.FieldDefinitions.Contains(workItemFieldMatches[WorkItemField.AssignedTo]))
                        {
                            throw new ArgumentException(String.Format("WorkItem {0} doesn't contain field with name {1}", taskTemplate.WorkItemType, workItemFieldMatches[WorkItemField.AssignedTo]));
                        }
                    }

                    // Get the relation link type
                    if (!store.WorkItemLinkTypes.Contains(taskTemplate.WorkItemLinkType))
                    {
                        throw new ArgumentException("Invalid WorkItemLinkType");
                    }

                    WorkItemLinkTypeEnd relationLinkType = store.WorkItemLinkTypes[taskTemplate.WorkItemLinkType].ReverseEnd;

                    foreach (TaskTemplateInstance taskTemplateInstance in taskTemplate.InstancesCollection)
                    {
                        WorkItem newWorkItem = wiType.NewWorkItem();

                        newWorkItem.AreaId = baseWorkItem.AreaId;
                        newWorkItem.AreaPath = baseWorkItem.AreaPath;
                        newWorkItem.IterationPath = baseWorkItem.IterationPath;

                        // Copy Tags
                        if (!String.IsNullOrWhiteSpace(baseWorkItem.Tags))
                        {
                            newWorkItem["Tags"] = baseWorkItem.Tags;
                        }

                        newWorkItem.Title = String.Format("{0}{1}{2}", taskTemplate.Prefix, baseWorkItem.Title, taskTemplate.Sufix);

                        // AssignedTo
                        newWorkItem.UpdateField(workItemFieldMatches[WorkItemField.AssignedTo], taskTemplateInstance.AssignedTo);

                        // Original Estimate
                        newWorkItem.UpdateField(workItemFieldMatches[WorkItemField.OriginalEstimate], taskTemplateInstance.EstimatedTime);

                        // Remaining Work
                        newWorkItem.UpdateField(workItemFieldMatches[WorkItemField.RemainingWork], taskTemplateInstance.EstimatedTime);

                        // Custom Properties
                        if (taskTemplate.CustomProperties != null && taskTemplate.CustomProperties.Count > 0)
                        {
                            foreach (TaskTemplateCustomProperty customProperty in taskTemplate.CustomProperties)
                            {
                                newWorkItem.UpdateField(customProperty.Name, customProperty.Value);
                            }
                        }

                        if (taskTemplate.IsCopyDescriptionEnabled)
                        {
                            newWorkItem.Description = baseWorkItem.Description;

                            if (String.IsNullOrWhiteSpace(newWorkItem.Description))
                            {
                                // Maybe this is a bug
                                if (baseWorkItem.Fields.Contains("Repro Steps"))
                                {
                                    newWorkItem.Description = baseWorkItem.Fields["Repro Steps"].Value.ToString();
                                }
                            }
                        }

                        newWorkItem.Links.Add(new RelatedLink(relationLinkType, baseWorkItem.Id));

                        workItemsToSave.Add(newWorkItem);
                    }
                }
            }

            var witems = workItemsToSave.ToArray();

            store.BatchSave(witems);

            return witems;

        }

        /// <summary>
        /// Gets the work items.
        /// </summary>
        /// <param name="workItemIds">The work item ids.</param>
        /// <returns>
        /// WorkItems
        /// </returns>
        /// <exception cref="System.ArgumentNullException">If the work item doesn't exist.</exception>
        public WorkItem[] GetWorkItems(int[] workItemIds)
        {
            TfsTeamProjectCollection projectCollection = this.GetTeamProjectCollection();
            WorkItemStore store = projectCollection.GetService<WorkItemStore>();

            WorkItem[] workItems = new WorkItem[workItemIds.Length];
            
            for(int i = 0; i < workItemIds.Length; i++)
            {
                WorkItem workItem = store.GetWorkItem(workItemIds[i]);

                if(workItem == null)
                {
                    throw new ArgumentNullException("WorkItem");
                }

                workItems[i] = workItem;
            }

            return workItems;
        }

        #endregion

        #region Event handling Methods
        #endregion
    }
}
