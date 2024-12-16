<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Purchase_Requisition_AddRequisitionStep2" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" Codebehind="AddRequisitionStep2.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" src="../../public/js/DatePicker.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
       
    </script>

    <%@ Register src="../../UserControls/Edit/projectInfo.ascx" tagname="projectInfo"
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
                <uc1:projectInfo ID="projectInfo" runat="server" IsEditPage="true" />                
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <prc:CheckPRInputButton runat="server" id="btnNext" value="下一步"  type="button"  causesvalidation="false" class="widebuttons" onserverclick="btnNext_Click" />&nbsp;
                <prc:CheckPRInputButton runat="server" id="btnSave" value=" 保存  "  type="button" causesvalidation="false" class="widebuttons" onserverclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false" ShowMessageBox="true" />
</asp:Content>
