<%@ Page Language="C#" AutoEventWireup="true" Inherits="Purchase_Requisition_AddRequisitionStep5"
    MasterPageFile="~/MasterPage.master" CodeBehind="AddRequisitionStep5.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" src="../../public/js/DatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

    </script>

    <%@ register src="../../UserControls/View/genericInfo.ascx" tagname="genericInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/supplierInfo.ascx" tagname="supplierInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/projectInfo.ascx" tagname="projectInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/productInfo.ascx" tagname="productInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/paymentInfo.ascx" tagname="paymentInfo"
        tagprefix="uc1" %>
    <%@ register src="/UserControls/Edit/RequirementDescInfo.ascx" tagname="RequirementDescInfo"
        tagprefix="uc1" %>
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <li>
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="RequisitionSaveList.aspx">�������뵥��Ϣ���</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%--          ��Ŀ�����뵥��Ϣ        --%>
                <uc1:projectInfo ID="projectInfo" runat="server" IsEditPage="true" />
                <%--          ��Ŀ�����뵥��Ϣ        --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:genericInfo ID="GenericInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%-- *********�ɹ�������Ϣ********* --%>
                <uc1:productInfo ID="productInfo" runat="server" />
                <%-- *********�ɹ�������Ϣ********* --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%-- *********��Ӧ����Ϣ********* --%>
                <uc1:supplierInfo ID="supplierInfo" runat="server" />
                <%-- *********��Ӧ����Ϣ********* --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%-- *********������Ϣ********* --%>
                <uc1:paymentInfo ID="paymentInfo" runat="server" />
                <%-- *********������Ϣ********* --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:RequirementDescInfo ID="RequirementDescInfo" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnPre" Text="��һ��" CssClass="widebuttons" OnClick="btnPre_Click"
                    runat="server" CausesValidation="false" />&nbsp;
                <prc:CheckPRInputButton runat="server" ID="btnSave" value=" ����  " type="button" onclick="this.disabled=true;"
                    CausesValidation="false" class="widebuttons" OnServerClick="btnSave_Click" />
                <prc:CheckPRInputButton runat="server" ID="btnCommit" value=" �ύ " type="button"
                    onclick="this.disabled=true;" CausesValidation="false" class="widebuttons" OnServerClick="btnCommit_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
</asp:Content>
