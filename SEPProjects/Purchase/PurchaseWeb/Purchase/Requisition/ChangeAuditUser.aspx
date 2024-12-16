<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_Requisition_ChangeAuditUser" CodeBehind="ChangeAuditUser.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
        function EmplyeeClick(type, con) {
            if (document.getElementById("ctl00_ContentPlaceHolder1_genericInfo_" + con) != null) {
                var name = document.getElementById("ctl00_ContentPlaceHolder1_genericInfo_" + con).value;
                var win = window.open('EmployeeList.aspx?clientId=ctl00_ContentPlaceHolder1_genericInfo_&name=' + name + '&type=' + type, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            } else {
            var name = ""; 
                var win = window.open('EmployeeList.aspx?name=' + name + '&type=' + type, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            }
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
    </script>

    <%@ register src="../../UserControls/View/genericInfo.ascx" tagname="genericInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/supplierInfo.ascx" tagname="supplierInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/projectInfo.ascx" tagname="projectInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/productInfo.ascx" tagname="productInfo"
        tagprefix="uc1" %>
    <%@ register src="/UserControls/Edit/paymentInfo.ascx" tagname="paymentInfo" tagprefix="uc1" %>
    <%@ register src="/UserControls/View/RequirementDescInfo.ascx" tagname="RequirementDescInfo"
        tagprefix="uc1" %>
    <%--          项目号申请单信息        --%>
    <uc1:projectinfo id="projectInfo" runat="server" iseditpage="true" />
    <uc1:genericinfo id="GenericInfo" runat="server" />
    <%--          项目号申请单信息        --%>
    <%-- *********采购物料信息********* --%>
    <uc1:productinfo id="productInfo" runat="server" />
    <%-- *********采购物料信息********* --%>
    <%-- *********供应商信息********* --%>
    <uc1:supplierinfo id="supplierInfo" runat="server" />
    <%-- *********供应商信息********* --%>
    <%--       付款账期信息         --%>
    <uc1:paymentinfo id="paymentInfo" runat="server" />
    <%--       付款账期信息         --%>
    <uc1:requirementdescinfo id="RequirementDescInfo" runat="server" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                审核信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                订单编号:
            </td>
            <td class="oddrow-l" width="35%">
                <asp:Label ID="txtorderid" runat="server" Width="200px"></asp:Label>
            </td>
            <td class="oddrow" width="20%">
                谈判类型:
            </td>
            <td class="oddrow-l" width="30%">
                <asp:Label ID="txttype" runat="server" Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                比价节约:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtcontrast" runat="server" Width="200px"></asp:Label>&nbsp;<asp:Label
                    ID="labdownContrast" runat="server" />
            </td>
            <td class="oddrow">
                议价节约:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtconsult" runat="server" Width="200px"></asp:Label>&nbsp;<asp:Label
                    ID="labdownConsult" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                初审人:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtfirst_assessor" runat="server" Width="120px" Enabled="false" /><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtfirst_assessor"
                    ErrorMessage="请选择初审人" Display="None"></asp:RequiredFieldValidator><font color="red">
                        * </font>
                <input type="button" value="变更初审人" class="widebuttons" onclick="return EmplyeeClick('changeAuditor','txtfirst_assessor');" /><asp:HiddenField
                    ID="hidfirst_assessor" runat="server" />
            </td>
            <td class="oddrow">
                事后申请:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labafterwards" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                紧急采购:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labEmBuy" runat="server" Width="200px"></asp:Label>
            </td>
            <td class="oddrow" style="width: 120px;">
                客户指定:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labCusAsk" runat="server" Width="200px"></asp:Label>
                <asp:Label ID="labCusName" runat="server" Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                合同号码:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labContractNo" runat="server" Width="200px" />
            </td>
            <td class="oddrow">
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
    <table width="100%">
        <tr>
            <td height="25" colspan="2" align="center">
                <input runat="server" id="btnSave" value=" 保存 " type="button" 
                    causesvalidation="false" class="widebuttons" onserverclick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnCancel" CausesValidation="false" runat="server" Text=" 返回 " CssClass="widebuttons"
                    OnClick="btn_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" ShowMessageBox="true"
        ShowSummary="false" runat="server" />
</asp:Content>
