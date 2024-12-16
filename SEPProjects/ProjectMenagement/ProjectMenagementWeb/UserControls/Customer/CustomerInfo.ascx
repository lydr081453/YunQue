<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_Customer_CustomerInfo" Codebehind="CustomerInfo.ascx.cs" %>

<script language="javascript">
    function CustomerClick() {
        var win = window.open('/Dialogs/CustomerDlg.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function HistoryClick() {
        var dept = '<%= GetDept %>';
        var cid = document.getElementById("<%=hidCustomerID.ClientID %>").value;
        
        var win = window.open('/Dialogs/CustomerHistoryDlg.aspx?<% =ESP.Finance.Utility.RequestName.DeptID %>=' + dept+'&<% =ESP.Finance.Utility.RequestName.CustomerID %>='+cid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function AreaClick() {
        var win = window.open('/Dialogs/AreaDlg.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function IndustryClick() {
        var win = window.open('/Dialogs/IndustryDlg.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function showCustomer() {
        var cid = document.getElementById("<%=hidCustomerTmpID.ClientID %>").value;
        var win = window.open('/Customer/CustomerDiffer.aspx?<% =ESP.Finance.Utility.RequestName.CustomerID %>=' + cid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
</script>

<%--<table width="100%" class="tableForm">
    <tr>
        <td class="oddrow" style="width: 15%">
            英文简称:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtShortEN" runat="server" MaxLength="3" /><input type="hidden"
                id="hidCustomerTmpID" runat="server" /><input type="hidden" id="hidCustomerID" runat="server" /><input
                    type="hidden" id="hidCustomerCode" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtShortEN"
                Display="None" ErrorMessage="英文简称为必填"></asp:RequiredFieldValidator><font color="red">*</font><input
                    type="button" id="btnCustomerSelect" onclick="return CustomerClick();" class="widebuttons"
                    value="搜索" />
            <input type="button" id="btnHistory" onclick="return HistoryClick();" class="widebuttons"
                value="查看历史" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            中文名称1:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtCN1" runat="server" Width="80%" MaxLength="35" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCN1"
                Display="None" ErrorMessage="中文名称为必填"></asp:RequiredFieldValidator><font color="red">*</font>
        </td>
        <td class="oddrow" style="width: 15%">
            中文名称2:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtCN2" runat="server" Width="80%" MaxLength="35" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            中文简称：
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtShortCN" runat="server" Width="80%" MaxLength="35" />
        </td>
        <td class="oddrow">
            发票抬头:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtTitle" runat="server" Width="80%" MaxLength="50" /><asp:RequiredFieldValidator
                ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle" Display="None"
                ErrorMessage="发票抬头为必填"></asp:RequiredFieldValidator><font color="red">*</font>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            英文名称1:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtEN1" runat="server" Width="80%" MaxLength="35" /><asp:RequiredFieldValidator
                ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEN1" Display="None"
                ErrorMessage="英文名称为必填"></asp:RequiredFieldValidator><font color="red">*</font>
        </td>
        <td class="oddrow" style="width: 15%">
            英文名称2:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtEN2" runat="server" Width="80%" MaxLength="35" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            客户地址1:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtAddress1" runat="server" Width="40%" MaxLength="35" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            客户地址2:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtAddress2" runat="server" Width="40%" MaxLength="35" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            公司邮编:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtPostCode" runat="server" Width="80%" MaxLength="30" />
        </td>
        <td class="oddrow" style="width: 15%">
            公司网址:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtWebSite" runat="server" Width="80%" MaxLength="50" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            所在地区:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtArea" runat="server">
            </asp:TextBox><input type="button" id="btnAreaSelect" onclick="return AreaClick();"
                value="搜索" class="widebuttons" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtArea"
                Display="None" ErrorMessage="所在地区为必填"></asp:RequiredFieldValidator><font color="red">*</font>
            <input type="hidden" id="hidAreaID" runat="server" />
            <input type="hidden" id="hidAreaCode" runat="server" />
        </td>
        <td class="oddrow" style="width: 15%">
            所在行业:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtIndustry" runat="server"> </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtIndustry"
                Display="None" ErrorMessage="所在行业为必填"></asp:RequiredFieldValidator><font color="red">*</font>
            <input type="button" id="btnIndustrySelect" onclick="return IndustryClick();" value="搜索"
                class="widebuttons" />
            <input type="hidden" id="hidIndustryID" runat="server" />
            <input type="hidden" id="hidIndustryCode" runat="server" />
        </td>
    </tr>
    </table>--%>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            客户信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            英文简称:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtShortEN" runat="server" MaxLength="3" CssClass="inputLabel" Enabled="true" /><input
                type="hidden" id="hidCustomerTmpID" runat="server" /><input type="hidden" id="hidCustomerID"
                    runat="server" /><input type="hidden" id="hidCustomerCode" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtShortEN"
                Display="None" ErrorMessage="英文简称为必填"></asp:RequiredFieldValidator><font color="red">*</font><input
                    type="button" id="btnCustomerSelect" onclick="return CustomerClick();" class="widebuttons"
                    value="搜索" />
            <%--            <input type="button" id="btnHistory" onclick="return HistoryClick();" class="widebuttons"
                value="查看历史" />--%>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            中文名称1:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtCN1" runat="server" Width="80%" MaxLength="35" CssClass="inputLabel"
                Enabled="true" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCN1"
                Display="None" ErrorMessage="中文名称为必填"></asp:RequiredFieldValidator><font color="red">*</font>
        </td>
        <td class="oddrow" style="width: 15%">
            中文名称2:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtCN2" runat="server" Width="80%" MaxLength="35" CssClass="inputLabel"
                Enabled="true" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            中文简称：
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtShortCN" runat="server" Width="80%" MaxLength="35" CssClass="inputLabel"
                Enabled="true" />
        </td>
        <td class="oddrow">
            发票抬头:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtTitle" runat="server" Width="80%" MaxLength="50" CssClass="inputLabel"
                Enabled="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ControlToValidate="txtTitle" Display="None" ErrorMessage="发票抬头为必填"></asp:RequiredFieldValidator><font
                        color="red">*</font>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            英文名称1:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtEN1" runat="server" Width="80%" MaxLength="35" CssClass="inputLabel"
                Enabled="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                    ControlToValidate="txtEN1" Display="None" ErrorMessage="英文名称为必填"></asp:RequiredFieldValidator><font
                        color="red">*</font>
        </td>
        <td class="oddrow" style="width: 15%">
            英文名称2:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtEN2" runat="server" Width="80%" MaxLength="35" CssClass="inputLabel"
                Enabled="true" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            客户地址1:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtAddress1" runat="server" Width="40%" MaxLength="35" CssClass="inputLabel"
                Enabled="true" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            客户地址2:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtAddress2" runat="server" Width="40%" MaxLength="35" CssClass="inputLabel"
                Enabled="true" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            公司邮编:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtPostCode" runat="server" Width="80%" MaxLength="30" CssClass="inputLabel"
                Enabled="true" />
        </td>
        <td class="oddrow" style="width: 15%">
            公司网址:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtWebSite" runat="server" Width="80%" MaxLength="50" CssClass="inputLabel"
                Enabled="true" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            所在地区:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtArea" runat="server" CssClass="inputLabel" Enabled="true">
            </asp:TextBox><%--<input type="button" id="btnAreaSelect" onclick="return AreaClick();"
                value="搜索" class="widebuttons" />--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtArea"
                Display="None" ErrorMessage="所在地区为必填"></asp:RequiredFieldValidator><font color="red">*</font>
            <input type="hidden" id="hidAreaID" runat="server" />
            <input type="hidden" id="hidAreaCode" runat="server" />
        </td>
        <td class="oddrow" style="width: 15%">
            所在行业:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtIndustry" runat="server" CssClass="inputLabel" Enabled="true"> </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtIndustry"
                Display="None" ErrorMessage="所在行业为必填"></asp:RequiredFieldValidator><font color="red">*</font>
            <%--  <input type="button" id="btnIndustrySelect" onclick="return IndustryClick();" value="搜索"
                class="widebuttons" />--%>
            <input type="hidden" id="hidIndustryID" runat="server" />
            <input type="hidden" id="hidIndustryCode" runat="server" />
        </td>
    </tr>
</table>
<div id="nodisplay" style="display: none">
    <table>
        <tr>
            <td class="heading" colspan="4">
                联系人信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                客户联系人:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtContact" runat="server" MaxLength="15" />
            </td>
            <td class="oddrow" style="width: 15%">
                联系人职务:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtContactPosition" runat="server" MaxLength="15" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                联系人电话:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtContactMobile" runat="server" MaxLength="15" />
            </td>
            <td class="oddrow" style="width: 15%">
                联系人传真:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtContactFax" runat="server" MaxLength="15" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                联系人Email:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtContactEmail" runat="server" Width="40%" MaxLength="50" />
            </td>
        </tr>
    </table>
</div>
