<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Purchase_Requisition_AddRequisitionStep4" MasterPageFile="~/MasterPage.master" Codebehind="AddRequisitionStep4.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" src="../../public/js/DatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

    </script>

    <script language="javascript">
        function settype(con) {
            document.getElementById("ctl00_ContentPlaceHolder1_txtsupplier_type").value = con.options[con.selectedIndex].value;
        }
    </script>

    <%@ register src="../../UserControls/View/genericInfo.ascx" tagname="genericInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/supplierInfo.ascx" tagname="supplierInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/projectInfo.ascx" tagname="projectInfo"
        tagprefix="uc1" %>
            <%@ Register Src="../../UserControls/View/productInfo.ascx" TagName="productInfo"
    TagPrefix="uc1" %>
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
                <a name="top_A" />
                <uc1:supplierInfo ID="supplierInfo" runat="server" />
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
