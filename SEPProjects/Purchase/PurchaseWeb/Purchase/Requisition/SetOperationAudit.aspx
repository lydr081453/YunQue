<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="Purchase_Requisition_SetOperationAudit" Codebehind="SetOperationAudit.aspx.cs" %>
            <%@ register src="../../UserControls/Edit/newSetAuditor.ascx" tagname="newSetAuditor"
        tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:newSetAuditor runat="server" ID="newSetAuditor" />
</asp:Content>

