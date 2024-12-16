<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_AddRequisitionStep6"
    Title="Untitled Page" Codebehind="AddRequisitionStep6.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <link href="../../public/css/dialog.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="../../public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="../../public/js/dialog.js"></script>
    <script language="javascript" src="../../public/js/DatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

    </script>

    <script language="javascript">

function selectone() {
    var Element = document.getElementsByName("chkRep");
    hidChkID = document.getElementById("<% = hidChkID.ClientID %>");
    hidChkID.value = "";
    var ids = "";
    for (var j = 0; j < Element.length; j++) {
        if (Element[j].checked) {
            ids += Element[j].value + ",";
            hidChkID.value += Element[j].value + ",";
        }
    }
    ids = ids.substring(0, ids.length - 1);
    hidChkID.value = ids;
    if (ids != "") {
        return ids;
    } else {
        alert("请选择需要复制的采购物品");
        return false;
    }
}
    </script>

    <%@ register src="../../UserControls/View/genericInfo.ascx" tagname="genericInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/projectInfo.ascx" tagname="projectInfo"
        tagprefix="uc1" %>
            <%@ register src="../../UserControls/Edit/productInfo.ascx" tagname="productInfo"
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
                <uc1:projectInfo ID="projectInfo" runat="server"  IsEditPage="true"/>
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
                    <%--        采购物品信息          --%>
                    <uc1:productInfo ID="productInfo" runat="server" />
                    <%--        采购物品信息          --%>
            </td>
        </tr>
        <tr><td colspan="2" style="display:none"><asp:FileUpload ID="a1" runat="server" /></td></tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnPre" Text="上一步" CssClass="widebuttons" OnClick="btnPre_Click"
                    runat="server" CausesValidation="false" />&nbsp;
                <prc:CheckPRInputButton runat="server" id="btnNext" value="下一步"  type="button"  causesvalidation="false" class="widebuttons" onserverclick="btnNext_Click" />&nbsp;
		        <prc:CheckPRInputButton runat="server" id="btnSave" value=" 保存  "  type="button"  causesvalidation="false" class="widebuttons" onserverclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <input type="hidden" value="0" runat="server" id="hidChkID" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
</asp:Content>
