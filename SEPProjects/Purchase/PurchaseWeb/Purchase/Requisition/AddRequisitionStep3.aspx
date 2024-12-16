<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Purchase_Requisition_AddRequisitionStep3" MasterPageFile="~/MasterPage.master" Codebehind="AddRequisitionStep3.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" src="../../public/js/DatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

    </script>

    <%@ register src="../../UserControls/Edit/genericInfo.ascx" tagname="genericInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/projectInfo.ascx" tagname="projectInfo"
        tagprefix="uc1" %>
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <li>
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="RequisitionSaveList.aspx">返回申请单信息浏览</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%--        项目号申请单信息          --%>
                <a name="top_A" />
                <uc1:projectInfo ID="projectInfo" runat="server" IsEditPage="true" />
                <%--        项目号申请单信息          --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:genericInfo ID="genericInfo" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnPre" Text="上一步" CssClass="widebuttons" OnClick="btnPre_Click"
                    runat="server" CausesValidation="false" />&nbsp;
                <prc:CheckPRInputButton runat="server" id="btnNext" value="下一步" type="button" 
                    causesvalidation="false" class="widebuttons" onserverclick="btnNext_Click" />&nbsp;
                <prc:CheckPRInputButton runat="server" id="btnSave" value=" 保存  " type="button" 
                    causesvalidation="false" class="widebuttons" onserverclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
</asp:Content>
