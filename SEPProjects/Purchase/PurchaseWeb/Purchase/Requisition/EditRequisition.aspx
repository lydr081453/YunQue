<%@ Page Language="C#" AutoEventWireup="true" Inherits="Purchase_Requisition_EditRequisition"
    MasterPageFile="~/MasterPage.master" CodeBehind="EditRequisition.aspx.cs" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register src="/UserControls/Edit/genericInfo.ascx" tagname="genericInfo" tagprefix="uc1" %>
    <%@ register src="/UserControls/View/supplierInfo.ascx" tagname="supplierInfo" tagprefix="uc1" %>
    <%@ register src="/UserControls/View/projectInfo.ascx" tagname="projectInfo" tagprefix="uc1" %>
    <%@ register src="/UserControls/Edit/productInfo.ascx" tagname="productInfo" tagprefix="uc1" %>
    <%@ register src="/UserControls/Edit/paymentInfo.ascx" tagname="paymentInfo" tagprefix="uc1" %>
    <%@ register src="/UserControls/View/auditLog.ascx" tagname="auditLog" tagprefix="uc1" %>
    <%@ register src="/UserControls/Edit/RequirementDescInfo.ascx" tagname="RequirementDescInfo"
        tagprefix="uc1" %>

    <script language="javascript" src="../../public/js/DatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function EmplyeeClick(type, con) {
            var name = document.getElementById("ctl00_ContentPlaceHolder1_genericInfo_" + con).value;
            var win = window.open('EmployeeList.aspx?clientId=ctl00_ContentPlaceHolder1_genericInfo_&name=' + name + '&type=' + type, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function settype(con) {
            document.getElementById("ctl00_ContentPlaceHolder1_txtsupplier_type").value = con.options[con.selectedIndex].value;
        }
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
                alert("��ѡ����Ҫ���ƵĲɹ���Ʒ");
                return false;
            }
        }
        function openEditSupplier() {
            var win = window.open('ChangeFSupplier.aspx?<%=RequestName.GeneralID %>=<%=Request[RequestName.GeneralID] %>&updateControl=<%=supplierInfo.FindControl("btnUpdateInfo").ClientID %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
    </script>

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
                <%--        ��Ŀ�����뵥��Ϣ          --%>
                <uc1:projectInfo ID="projectInfo" runat="server" IsEditPage="true" />
                <%--        ��Ŀ�����뵥��Ϣ          --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:genericInfo ID="genericInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%--        �ɹ���Ʒ��Ϣ          --%>
                <uc1:productInfo ID="productInfo" runat="server" />
                <%--        �ɹ���Ʒ��Ϣ          --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:supplierInfo ID="supplierInfo" runat="server" />
                <asp:Button ID="btnEditSupplier" runat="server" Text="�༭��Ӧ��" CssClass="widebuttons"
                    OnClientClick="openEditSupplier();return false;" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%--       ����������Ϣ         --%>
                <uc1:paymentInfo ID="paymentInfo" runat="server" />
                <%--       ����������Ϣ         --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:RequirementDescInfo ID="RequirementDescInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td style="width: 15%" class="oddrow">
                            ���༭ʱ��:
                        </td>
                        <td colspan="3" class="oddrow-l">
                            <asp:Label ID="lablasttime" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="palOverrule" runat="server" Visible="false">
                    <table width="100%" class="tableForm">
                        <tr>
                            <td colspan="2" class="heading" style="height: 1px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                ���뵥��˱�ע:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labOverrule" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="palFili" runat="server" Visible="false">
                    <table width="100%" class="tableForm">
                        <tr>
                            <td colspan="2" class="heading" style="height: 1px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                �ֹ�˾��˱�ע:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labfilioverrule" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="padding: 4px">
                <uc1:auditLog ID="auditLog" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td height="25" colspan="2">
                <div align="center">
                    <asp:Button ID="btnPre" runat="server" Text="��һ��" CssClass="widebuttons" OnClick="btnPre_Click" />
                    <asp:Button runat="server" ID="btnSave" value=" ����  " 
                        CausesValidation="false" class="widebuttons" OnClick="btnSave_Click" />
                    &nbsp;
                    <asp:Button runat="server" ID="btnAdd" value=" �ύ " CausesValidation="false" class="widebuttons" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnCancel" CssClass="widebuttons" CausesValidation="false" OnClick="BackUrl_Click"
                        runat="server" Text=" ���� "></asp:Button>
            </td>
        </tr>
    </table>
    <input type="hidden" value="0" runat="server" id="hidChkID" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
</asp:Content>
