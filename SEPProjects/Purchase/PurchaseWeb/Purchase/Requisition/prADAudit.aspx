<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="prADAudit.aspx.cs" Inherits="prADAudit" %>

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
    <%@ register src="/UserControls/View/RequirementDescInfo.ascx" tagname="RequirementDescInfo"
        tagprefix="uc1" %>
    <%@ register src="/UserControls/View/prLogs.ascx" tagname="prLogs" tagprefix="uc1" %>
    <%--          项目号申请单信息        --%>
    <uc1:projectInfo ID="projectInfo" runat="server" IsEditPage="true" />
    <uc1:genericInfo ID="GenericInfo" runat="server" />
    <%--          项目号申请单信息        --%>
    <%-- *********采购物料信息********* --%>
    <uc1:productInfo ID="productInfo" runat="server" />
    <%-- *********采购物料信息********* --%>
    <%-- *********供应商信息********* --%>
    <uc1:supplierInfo ID="supplierInfo" runat="server" />
    <%-- *********账期信息********* --%>
    <uc1:paymentInfo ID="paymentInfo" runat="server" />
    <%--<table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                ⑤ 付款条件信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预付金额:
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:Label ID="labPay" runat="server" Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款账期:
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:Label ID="txtpayment_terms" runat="server"></asp:Label>
            </td>
        </tr>
    </table>--%>
    <uc1:requirementdescinfo id="RequirementDescInfo" runat="server" EditRequisitionFlow="true" />
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
                <asp:Label ID="ddlfirst_assessor" runat="server" />
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
    <table width="100%" class="tableForm">
        <tr>
            <td style="width: 15%" class="oddrow">
                最后编辑时间:
            </td>
            <td style="width: 35%" class="oddrow-l">
                <asp:Label ID="lablasttime" runat="server" />
            </td>
            <td style="width: 20%" class="oddrow">
                申请单提交时间:
            </td>
            <td style="width: 30%" class="oddrow-l">
                <asp:Label ID="labrequisition_committime" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 100px;">
                审批流向:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labrequisitionflow" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 100px;">
            </td>
            <td class="oddrow-l">
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                订单生成时间:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="laborder_committime" runat="server" />
            </td>
            <td class="oddrow">
                订单审批时间:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="laborder_audittime" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="palFili" runat="server" Visible="false">
        <table width="100%" class="tableForm">
            <tr>
                <td colspan="2" class="heading" style="height: 1px">
                </td>
            </tr>
            <tr>
                <td style="width: 15%" class="oddrow">
                    分公司审核备注:
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labFili" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="palOverrulP" runat="server" Visible="false">
        <table width="100%" class="tableForm">
            <tr>
                <td colspan="2" class="heading" style="height: 1px">
                </td>
            </tr>
            <tr>
                <td style="width: 15%" class="oddrow">
                    申请单审核备注:
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labOverruleP" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="palOverrule" runat="server" Visible="false">
        <table width="100%" class="tableForm">
            <tr>
                <td colspan="2" class="heading" style="height: 1px">
                </td>
            </tr>
            <tr>
                <td style="width: 15%" class="oddrow">
                    申请单审批备注:
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labOverrule" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table class="tableForm" style="width: 100%;">
        <tr>
            <td colspan="2" class="heading" style="height: 1px">
            </td>
        </tr>
        <tr>
           <td colspan="2" class="heading" style="height: 1px">
                <uc1:prLogs ID="prLogs" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 15%" class="oddrow">
                广告媒体采买审批备注:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtprMediaRemark" TextMode="multiLine" Height="100px" Width="400px"
                    runat="server" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidState" runat="server" />
    <table width="100%">
        <tr>
            <td height="25" colspan="2" align="center">
                <asp:Button ID="btnSave" runat="server" Text=" 编辑 " CausesValidation="false" CssClass="widebuttons"
                    OnClick="btnSave_Click" Visible="false" />&nbsp;
                <prc:CheckPRInputButton runat="server" ID="btnOk" value="审批通过" type="button" CausesValidation="false"
                    class="widebuttons" OnServerClick="btnOk_Click" />
                &nbsp;<prc:CheckPRInputButton runat="server" ID="btnNo" value="审批驳回" type="button"
                    CausesValidation="false" class="widebuttons" OnServerClick="btnNo_Click" />
                &nbsp;<asp:Button ID="btnCancel" runat="server" Text=" 返回 " CausesValidation="false"
                    CssClass="widebuttons" OnClick="btn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
