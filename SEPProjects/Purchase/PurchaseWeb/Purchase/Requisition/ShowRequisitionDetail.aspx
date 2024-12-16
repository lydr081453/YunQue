<%@ Page Language="C#" AutoEventWireup="true" Inherits="Purchase_Requisition_ShowRequisitionDetail"
    MasterPageFile="~/MasterPage.master" CodeBehind="ShowRequisitionDetail.aspx.cs" %>

<%@ Register Src="../../UserControls/View/genericInfo.ascx" TagName="genericInfo"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/View/supplierInfo.ascx" TagName="supplierInfo"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/View/projectInfo.ascx" TagName="projectInfo"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/View/productInfo.ascx" TagName="productInfo"
    TagPrefix="uc1" %>
<%@ Register Src="/UserControls/View/paymentInfo.ascx" TagName="paymentInfo" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/View/auditLog.ascx" TagName="auditLog" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/View/RequirementDescInfo.ascx" TagName="RequirementDescInfo"
    TagPrefix="uc1" %>
            <%@ register src="/UserControls/View/prLogs.ascx" tagname="prLogs"
        tagprefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table style="width: 100%">
        <tr>
            <td style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px">
                <%--          ��Ŀ�����뵥��Ϣ        --%>
                <uc1:projectinfo id="projectInfo" runat="server" />
                <%-- *********һ����Ϣ********* --%>
                <uc1:genericinfo id="GenericInfo" runat="server" />
                <%-- *********�ɹ�������Ϣ********* --%>
                <uc1:productinfo id="productInfo" runat="server" />
                <%-- *********�ɹ�������Ϣ********* --%>
                <%-- *********��Ӧ����Ϣ********* --%>
                <uc1:supplierinfo id="supplierInfo" runat="server" />
                <%-- *********��Ӧ����Ϣ********* --%>
                <%--       ����������Ϣ         --%>
                <uc1:paymentinfo id="paymentInfo" runat="server" />
                <%--       ����������Ϣ         --%>
                <uc1:requirementdescinfo id="RequirementDescInfo" runat="server" />
                <uc1:prLogs ID="prLogs" runat="server" />
                <table style="width: 100%;" border="0">
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button runat="server" ID="BackUrl" Text=" ��  �� " CssClass="widebuttons" OnClick="BackUrl_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
