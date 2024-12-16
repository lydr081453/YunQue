<%@ Page Language="C#" Inherits="ESP.Mail.AspxMail,ESP.Core" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%
    ESP.Workflow.DataAccess.WorkItem workItem = (ESP.Workflow.DataAccess.WorkItem)this.DataSource;
    ESP.Finance.Entity.ReturnInfo info = ESP.Finance.BusinessLogic.ReturnManager.GetModel(workItem.EntityId);
    if ("收货".Equals(workItem.WorkItemName))
    {
        this.Title = "财务系统 现金借款单" + workItem.WorkItemName;
    }
    else
    {
        this.Title = "财务系统 报销单" + workItem.WorkItemName;
    }
    

%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
    <div>
        <%// =workItem.WorkflowName %><br />
        工作项：<% =workItem.WorkItemName %><br />
        角色：<% =workItem.ParticipantName %><br />
        <% if (info != null) %>
        <% { %>
        单号：<% = info.ReturnCode %><br />
        提交人：<% = info.RequestEmployeeName %><br />
        提交日期：<% = info.RequestDate %><br />
        <% } %>
    </div>
</body>
</html>
