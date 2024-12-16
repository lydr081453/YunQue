<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_Requisition_OperationAudit" CodeBehind="OperationAudit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register src="../../UserControls/View/genericInfo.ascx" tagname="genericInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/supplierInfo.ascx" tagname="supplierInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/projectInfo.ascx" tagname="projectInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/productInfo.ascx" tagname="productInfo"
        tagprefix="uc1" %>
    <%@ register src="/UserControls/View/operationAuditLog.ascx" tagname="operationAuditLog"
        tagprefix="uc1" %>
    <%@ register src="/UserControls/View/auditLog.ascx" tagname="auditLog" tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/paymentInfo.ascx" tagname="paymentInfo"
        tagprefix="uc1" %>
    <%@ register src="/UserControls/View/RequirementDescInfo.ascx" tagname="RequirementDescInfo"
        tagprefix="uc1" %>
    <table style="width: 100%">
        <tr>
            <td style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px">
                <%--          项目号申请单信息        --%>
                <uc1:projectinfo id="projectInfo" runat="server" iseditpage="true" />
                <%-- *********一般信息********* --%>
                <uc1:genericinfo id="GenericInfo" runat="server" />
                <%--          项目号申请单信息        --%>
                <%-- *********采购物料信息********* --%>
                <uc1:productinfo id="productInfo" runat="server" />
                <%-- *********采购物料信息********* --%>
                <%-- *********供应商信息********* --%>
                <uc1:supplierinfo id="supplierInfo" runat="server" />
                <%-- *********供应商信息********* --%>
                <uc1:paymentinfo id="paymentInfo" runat="server" />
                <uc1:requirementdescinfo id="RequirementDescInfo" runat="server" />
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
                </table>
                <uc1:operationauditlog id="operationAuditLog" runat="server" />
                <uc1:auditlog id="auditLog" runat="server" />
                <table width="100%" class="tableForm">
                    <tr>
                        <td style="width: 15%" class="oddrow">
                            业务审批备注:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="90%" Height="40px" /><font
                                color="red"> * </font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="业务审批备注为必填！"
                                ControlToValidate="txtRemark" Display="None"></asp:RequiredFieldValidator>
                                <asp:LinkButton runat="server" ID="btnTip" OnClick="btnTip_Click" CausesValidation="false"
                    ForeColor="Red" Font-Underline="true" Font-Bold="true" ToolTip="暂时留个Message,以后再操作">留言</asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <prc:checkprinputbutton runat="server" id="btnYes" value="审批通过" type="button" onclick="if(confirm('您确定要审批通过吗？')){showLoading();}else{return false;}"
                                causesvalidation="false" class="widebuttons" onserverclick="btnYes_Click" />
                            &nbsp;<prc:checkprinputbutton runat="server" id="btnNo" value="审批驳回" type="button"  
                                onclick="if(confirm('您确定要审批驳回吗？')){showLoading();}else{return false;}"
                                causesvalidation="false" class="widebuttons" onserverclick="btnNo_Click" />
                            &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CausesValidation="false"
                                CssClass="widebuttons" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                    runat="server" />
</asp:Content>
