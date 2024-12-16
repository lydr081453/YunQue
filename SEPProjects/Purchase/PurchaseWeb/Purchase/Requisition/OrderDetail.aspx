<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_Requisition_OrderDetail" Title="Untitled Page" CodeBehind="OrderDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <%@ register src="/UserControls/View/operationAuditLog.ascx" tagname="operationAuditLog"
        tagprefix="uc1" %>
    <%@ register src="/UserControls/View/RequirementDescInfo.ascx" tagname="RequirementDescInfo"
        tagprefix="uc1" %>
    <%@ register src="/UserControls/View/prLogs.ascx" tagname="prLogs"
        tagprefix="uc1" %>
    <%--          项目号申请单信息        --%>
    <uc1:projectInfo ID="projectInfo" runat="server" />
    <uc1:genericInfo ID="GenericInfo" runat="server" />
    <%--          项目号申请单信息        --%>
    <%-- *********采购物料信息********* --%>
    <uc1:productInfo ID="productInfo" runat="server" />
    <%-- *********采购物料信息********* --%>
    <%-- *********供应商信息********* --%>
    <uc1:supplierInfo ID="supplierInfo" runat="server" />
    <%-- *********账期信息********* --%>
    <uc1:paymentInfo ID="paymentInfo" runat="server" />
    <uc1:RequirementDescInfo ID="RequirementDescInfo" runat="server" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                审核信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                订单编号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="txtorderid" runat="server" Width="200px"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                谈判类型:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="txttype" runat="server" Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 100px">
                比价节约:
            </td>
            <td class="oddrow-l" style="width: 300px">
                <asp:Label ID="txtcontrast" runat="server" Width="200px"></asp:Label>&nbsp;<asp:Label
                    ID="labdownContrast" runat="server" />
            </td>
            <td class="oddrow" style="width: 100px">
                议价节约:
            </td>
            <td class="oddrow-l" style="width: 300px">
                <asp:Label ID="txtconsult" runat="server" Width="200px"></asp:Label>&nbsp;<asp:Label
                    ID="labdownConsult" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 100px">
                初审人:
            </td>
            <td class="oddrow-l" style="width: 300px">
                <asp:Label ID="ddlfirst_assessor" runat="server" SkinID="userLabel" />
            </td>
            <td class="oddrow" style="width: 100px">
                事后申请:
            </td>
            <td class="oddrow-l" style="width: 300px">
                <asp:Label ID="labafterwards" runat="server" /><br />
                <asp:Label ID="labafterwardsReason" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 100px;">
                紧急采购:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labEmBuy" runat="server" Width="200px" /><br />
                <asp:Label ID="labEmBuyReason" runat="server" />
            </td>
            <td class="oddrow" style="width: 120px;">
                客户指定:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labCusAsk" runat="server" Width="200px"></asp:Label><br />
                <asp:Label ID="labCusName" runat="server" Width="200px"></asp:Label><br />
                <asp:Label ID="labCusAskYesReason" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 100px;">
                合同号码:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labContractNo" runat="server" Width="200px" />
            </td>
            <td class="oddrow" style="width: 100px;">
                其它:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtothers" runat="server" Width="200px" />
            </td>
        </tr>
    </table>
    <uc1:prLogs ID="prLogs" runat="server" />
    <asp:Panel ID="tabOverrule" runat="server">
        <table class="tableForm" style="width: 100%;">
            <tr>
                <td colspan="2" class="heading" style="height: 1px">
                </td>
            </tr>
            <tr>
                <td style="width: 15%" class="oddrow">
                    申请单审批备注:
                </td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtoverrule" TextMode="multiLine" Height="100px" Width="400px" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtoverrule"
                        ErrorMessage="审批备注为必填" Display="None"></asp:RequiredFieldValidator><font color="red">*</font>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table class="tableForm" style="width: 100%" id="tabMedia" runat="server" visible="false">
        <tr>
            <td colspan="2" class="heading" style="height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 15%" class="oddrow">
                媒介审批备注:
            </td>
            <td class="oddrow-l">
                <asp:Literal ID="litprMediaRemark" runat="server" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
    <asp:HiddenField ID="hidState" runat="server" />
    <table width="100%">
        <tr>
            <td height="25" colspan="2" align="center">
                <input runat="server" id="btnOk" value="审批通过" visible="false" type="button" causesvalidation="false"
                    class="widebuttons" onserverclick="btnOk_Click" />
                &nbsp;<input runat="server" id="btnNo" value="审批驳回" visible="false" type="button"
                    causesvalidation="false" class="widebuttons" onserverclick="btnNo_Click" />
                &nbsp;<asp:Button ID="btnCancel" runat="server" Text=" 返回 " CausesValidation="false"
                    CssClass="widebuttons" OnClick="btn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
