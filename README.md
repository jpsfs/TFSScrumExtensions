TFSScrumExtensions
=======================

TFS Scrum Extensions is a VS 2012 and 2013 Extension that helps Project Managers and Developers to easily plan an estimate tasks on Team Foundation Server.

== How to use

Simply right-click on a TFS work item from a query result and you will see the "Plan Work Item" button on the context menu.

== Configuration and Customization

This tool can be fully customized to attend your project needs. Just click on "Edit Config" on the extension planning page and then reload.

The default configuration file shipped is:

```xml
<?xml version="1.0" encoding="utf-8"?>
<TFSScrumExtensionsConfiguration xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <UserGroups>Contributors</UserGroups> <!-- TFS Group of users used on the assignment of tasks -->
  <WorkItemFieldMatches> <!-- Help the tool match the defined Work Item Fields with the wizard fields -->
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
  <Templates DisplayName="Default Template" DisplayOrder="0"> <!-- An example template definition -->
    <TemplateTasks> <!-- Each template can contain an infinite number of 'Tasks'. 'Tasks' represent groups of work items that can be create. -->
      <WorkItemType>Task</WorkItemType><!-- Work Item Type (can be any of the TFS work items available on your system) -->
      <WorkItemLinkType>System.LinkTypes.Hierarchy</WorkItemLinkType> <!-- Type of the link to be create with the parent work item. -->
      <Quantity>1</Quantity><!-- Number of tasks to be created. -->
      <Title>Business</Title><!-- Title used on the extension wizard. -->
      <Prefix>BUSINESS :: </Prefix><!-- Prefix to add to each created task -->
      <CustomProperties><!-- List of custom properties that will be assigned to the created tasks -->
        <Name>Activity</Name>
        <Value>Development</Value>
      </CustomProperties>
      <IsCopyDescriptionEnabled>true</IsCopyDescriptionEnabled> <!-- Should the description be copied from the original Work Item? -->
    </TemplateTasks>
    <TemplateTasks>
      <WorkItemType>Task</WorkItemType>
      <WorkItemLinkType>System.LinkTypes.Hierarchy</WorkItemLinkType>
      <Quantity>1</Quantity>
      <Title>GUI</Title>
      <Prefix>GUI :: </Prefix>
      <CustomProperties>
        <Name>Activity</Name>
        <Value>Development</Value>
      </CustomProperties>
      <IsCopyDescriptionEnabled>true</IsCopyDescriptionEnabled>
    </TemplateTasks>
    <TemplateTasks>
      <WorkItemType>Task</WorkItemType>
      <WorkItemLinkType>System.LinkTypes.Hierarchy</WorkItemLinkType>
      <Quantity>1</Quantity>
      <Title>Test</Title>
      <Prefix>TEST :: </Prefix>
      <CustomProperties>
        <Name>Activity</Name>
        <Value>Testing</Value>
      </CustomProperties>
      <IsCopyDescriptionEnabled>true</IsCopyDescriptionEnabled>
    </TemplateTasks>
  </Templates>
  <Templates DisplayName="Merge Revision" DisplayOrder="1">
    <TemplateTasks>
      <WorkItemType>Task</WorkItemType>
      <WorkItemLinkType>System.LinkTypes.Hierarchy</WorkItemLinkType>
      <Quantity>1</Quantity>
      <Title>Test</Title>
      <Prefix>TEST :: </Prefix>
      <CustomProperties>
        <Name>Activity</Name>
        <Value>Testing</Value>
      </CustomProperties>
      <IsCopyDescriptionEnabled>true</IsCopyDescriptionEnabled>
    </TemplateTasks>
  </Templates>
</TFSScrumExtensionsConfiguration>
```

== How to report problems

This is the very first release of this Extension. Although stable, bugs can be found.
If you have any issue using this tool please report and issue using Github mechanisms.