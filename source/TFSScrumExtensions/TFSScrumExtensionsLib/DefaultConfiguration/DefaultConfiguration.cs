using JosePedroSilva.TFSScrumExtensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JosePedroSilva.TFSScrumExtensions.DefaultConfiguration
{
    public class DefaultConfigurations
    {
        /// <summary>
        /// The configuration
        /// </summary>
        public static ConfigurationRoot Default = new ConfigurationRoot()
        {
            UserGroups = new List<String>()
            {
                "Contributors",
                "Project Administrators"
            },
            WorkItemFieldMatches = new List<WorkItemFieldMatch>()
            {
                new WorkItemFieldMatch(){
                    Field = WorkItemField.AssignedTo,
                    Value = "Assigned To"
                },
                new WorkItemFieldMatch(){
                    Field = WorkItemField.OriginalEstimate,
                    Value = "Original Estimate"
                },
                new WorkItemFieldMatch(){
                    Field = WorkItemField.RemainingWork,
                    Value = "Remaining Work"
                },
                new WorkItemFieldMatch()
                {
                    Field = WorkItemField.CompletedWork,
                    Value = "Completed Work"
                }
            },
            Templates = new List<Template>()
            {
                new Template()
                {
                    DisplayName = "Default Template",
                    DisplayOrder = 0,
                    TemplateTasks = new List<TemplateTask>()
                    {
                       new TemplateTask()
                       {
                           Title = "Business",
                           WorkItemType = "Task",
                           WorkItemLinkType = "System.LinkTypes.Hierarchy",
                           Quantity = 1,
                           Prefix = "BUSINESS :: ",
                           IsCopyDescriptionEnabled = true,
                           CustomProperties = new List<TemplateCustomProperty>()
                           {
                               new TemplateCustomProperty()
                               {
                                   Name = "Activity",
                                   Value = "Development"
                               }
                           }
                       },

                       new TemplateTask()
                       {
                           Title = "GUI",
                           WorkItemType = "Task",
                           WorkItemLinkType = "System.LinkTypes.Hierarchy",
                           Quantity = 1,
                           Prefix = "GUI :: ",
                           IsCopyDescriptionEnabled = true,
                           CustomProperties = new List<TemplateCustomProperty>()
                           {
                               new TemplateCustomProperty()
                               {
                                   Name = "Activity",
                                   Value = "Development"
                               }
                           }
                       },

                       new TemplateTask()
                       {
                           Title = "Test",
                           WorkItemType = "Task",
                           WorkItemLinkType = "System.LinkTypes.Hierarchy",
                           Quantity = 1,
                           Prefix = "TEST :: ",
                           IsCopyDescriptionEnabled = true,
                           CustomProperties = new List<TemplateCustomProperty>()
                           {
                               new TemplateCustomProperty()
                               {
                                   Name = "Activity",
                                   Value = "Testing"
                               }
                           }
                       }
                    }
                },
                new Template()
                {
                    DisplayName = "Merge Revision",
                    DisplayOrder = 1,
                    TemplateTasks = new List<TemplateTask>()
                    {
                       new TemplateTask()
                       {
                           Title = "Test",
                           WorkItemType = "Task",
                           WorkItemLinkType = "System.LinkTypes.Hierarchy",
                           Quantity = 1,
                           Prefix = "TEST :: ",
                           IsCopyDescriptionEnabled = true,
                           CustomProperties = new List<TemplateCustomProperty>()
                           {
                               new TemplateCustomProperty()
                               {
                                   Name = "Activity",
                                   Value = "Testing"
                               }
                           }
                       }
                    }
                }
            }
        };
    }
}
