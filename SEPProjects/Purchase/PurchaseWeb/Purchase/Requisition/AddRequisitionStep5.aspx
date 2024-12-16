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
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="RequisitionSaveList.aspx">返回申请单信息浏览</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%--          项目号申请单信息        --%>
                <uc1:projectInfo ID="projectInfo" runat="server" IsEditPage="true" />
                <%--          项目号申请单信息        --%>
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
                <%-- *********采购物料信息********* --%>
                <uc1:productInfo ID="productInfo" runat="server" />
                <%-- *********采购物料信息********* --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%-- *********供应商信息********* --%>
                <uc1:supplierInfo ID="supplierInfo" runat="server" />
                <%-- *********供应商信息********* --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%-- *********付款信息********* --%>
                <uc1:paymentInfo ID="paymentInfo" runat="server" />
                <%-- *********付款信息********* --%>
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
                <asp:Button ID="btnPre" Text="上一步" CssClass="widebuttons" OnClick="btnPre_Click"
                    runat="server" CausesValidation="false" />&nbsp;
                <prc:CheckPRInputButton runat="server" ID="btnSave" value=" 保存  " type="button" onclick="this.disabled=true;"
                    CausesValidation="false" class="widebuttons" OnServerClick="btnSave_Click" />
                <prc:CheckPRInputButton runat="server" ID="btnCommit" value=" 提交 " type="button"
                    onclick="this.disabled=true;" CausesValidation="false" class="widebuttons" OnServerClick="btnCommit_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
</asp:Content>
