TFSScrumExtensions
=======================

TFS Scrum Extensions is a VS 2012 and 2013 Extension that helps Project Managers and Developers to easily plan an estimate tasks on Team Foundation Server.

## Getting Started

Simply right-click on a TFS work item from a query result and you will see the "Plan Work Item" button on the context menu.

![](https://github.com/jpsfs/TFSScrumExtensions/blob/master/documentation/screens/tfs-page-1.JPG)

## Configuration and Customization

This tool can be fully customized to attend your project needs. Just click on "Edit Config" on the extension planning page and then reload.

The default configuration file shipped is:

```xml
<?xml version="1.0" encoding="utf-8"?>
<TFSScrumExtensionsConfiguration>
  
	<!-- TFS Group of users used on the assignment of tasks -->
	<UserGroups>Contributors</UserGroups>

	<!-- Help the tool match the defined Work Item Fields with the wizard fields -->
	<WorkItemFieldMatches> 
		<Field>AssignedTo</Field>
		<Value>Assigned To</Value>
		</WorkItemFieldMatches>
		<WorkItemFieldMatches>
		<Field>OriginalEstimate</Field>
		<Value>Original Estimate</Value>
		</WorkItemFieldMatches>
		<WorkItemFieldMatches>
		<Field>RemainingWork</Field>
		<Value>Remaining Work</Value>
		</WorkItemFieldMatches>
		<WorkItemFieldMatches>
		<Field>CompletedWork</Field>
		<Value>Completed Work</Value>
	</WorkItemFieldMatches>
  
	<!-- An example template definition. DisplayOrder orders the options on the page combo box. -->
	<Templates DisplayName="Default Template" DisplayOrder="0">
  
		<!-- Each template can contain an infinite number of 'Tasks'. -->
		<!-- 'Tasks' represent groups of work items that can be create. -->
		<TemplateTasks>
			<!-- Work Item Type (can be any of the TFS work items available on your system) -->
			<WorkItemType>Task</WorkItemType>
			
			<!-- Type of the link to be create with the parent work item. -->
			<WorkItemLinkType>System.LinkTypes.Hierarchy</WorkItemLinkType> 
			
			<!-- Number of tasks to be created. -->
			<Quantity>1</Quantity>
			
			<!-- Title used on the extension wizard. -->
			<Title>Business</Title>
			
			<!-- Prefix to add to each created task -->
			<Prefix>BUSINESS :: </Prefix>
			
			<!-- List of custom properties that will be assigned to the created tasks -->
			<CustomProperties>
				<Name>Activity</Name>
				<Value>Development</Value>
			</CustomProperties>
			
			<!-- Should the description be copied from the original Work Item? -->
			<IsCopyDescriptionEnabled>true</IsCopyDescriptionEnabled> 
		</TemplateTasks>
		
	</Templates>
	
</TFSScrumExtensionsConfiguration>
```

## How to report problems

This is the very first release of this Extension. Although stable, bugs can be found.
If you have any issue using this tool please report and issue using Github mechanisms.