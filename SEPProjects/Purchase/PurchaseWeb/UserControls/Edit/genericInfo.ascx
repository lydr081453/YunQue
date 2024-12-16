<%@ Import Namespace="ESP.Purchase.Common"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Edit_genericInfo"
    CodeBehind="genericInfo.ascx.cs" %>

<script language="javascript">
    function setAddress(type) {
        if(type ==1)
            document.getElementById("<%=txtship_address.ClientID %>").value = "<%=State.ShunYa_Default_Address %>";
        else if (type == 3)
            document.getElementById("<%=txtship_address.ClientID %>").value = "<%=State.ShunYa_SH_Address %>";
        else
            document.getElementById("<%=txtship_address.ClientID %>").value = "<%=State.ShunYa_CQ_Address %>";

        __doPostBack('<%=btnUpdateGeneralInfo.UniqueID %>', '');
    }
    function EmplyeeClick(type, con) {
        var name = document.getElementById("ctl00_ContentPlaceHolder1_genericInfo_" + con).value;
        var win = window.open('EmployeeList.aspx?clientId=ctl00_ContentPlaceHolder1_genericInfo_&name=' + name + '&type=' + type + "&gid=<%=Request[RequestName.GeneralID] %>", null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');

        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);

    }
</script>

<asp:LinkButton ID="btnUpdateGeneralInfo" runat="server" OnClick="btnUpdateGeneralInfo_Click" />
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            ② 需求方信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            申请人:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="ddlrequestor" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">
            创建日期:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label runat="server" ID="txtrequestordate"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            申请人联络方式:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtrequestor_info" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            业务组:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtrequestor_group" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            使用人:
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtenduser" runat="server" ReadOnly="true" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtenduser"
                Display="None" ErrorMessage="使用人为必填"></asp:RequiredFieldValidator><font color="red">*</font>
            <asp:HiddenField ID="hidenduser" runat="server" />
            &nbsp;<input type="button" value="请选择..." class="widebuttons" onclick="return EmplyeeClick('enduser','txtenduser');" />
        </td>
        <td class="oddrow">
            使用人联络方式:
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtenduser_con" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            使用人业务组:
        </td>
        <td colspan="3" class="oddrow-l">
            <asp:TextBox ID="txtenduser_group" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            收货人:
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtgoods_receiver" runat="server" ReadOnly="true" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtgoods_receiver"
                Display="None" ErrorMessage="收货人为必填"></asp:RequiredFieldValidator><font color="red">*</font>
            <asp:HiddenField ID="hidreceiver" runat="server" />
            &nbsp;<input type="button" value="请选择..." class="widebuttons" onclick="return EmplyeeClick('receiver','txtgoods_receiver');" />
        </td>
        <td class="oddrow">
            收货人联络方式:
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtreceiver_con" runat="server"/>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            收货地址:
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtship_address" runat="server" Width="95%" /><font color="red"> *
            </font>
            <input type="button" class="widebuttons" value="总部" onclick="setAddress(1);" />&nbsp;
            <input type="button" class="widebuttons" value="重庆分公司" onclick="setAddress(2);" />&nbsp;
            <input type="button" class="widebuttons" value="宣亚上海分公司" onclick="setAddress(3);" />
            <asp:RequiredFieldValidator
                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtship_address"
                Display="None" ErrorMessage="收货地址为必填"></asp:RequiredFieldValidator>
        </td>
        <td class="oddrow">
            收货人业务组:
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtReceiverGroup" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            收货人其他联络方式:
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtreceiver_Otherinfo" runat="server" MaxLength="50" />
        </td>
        <td class="oddrow">
            附加收货人:
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtappendReceiver" runat="server" ReadOnly="true" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtappendReceiver"
                Display="None" ErrorMessage="附加收货人为必填"></asp:RequiredFieldValidator><font color="red">*</font>
            <asp:HiddenField ID="hidappendReceiver" runat="server" />
            &nbsp;<asp:Button runat="server" Text="请选择..." ID="btnAppendReceiver" class="widebuttons" OnClientClick="return EmplyeeClick('appendReceiver','txtappendReceiver');" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">附加收货人联络方式:</td>
        <td class="oddrow-l"><asp:TextBox ID="txtAppen_con" runat="server" />
</td>
        <td class="oddrow">附加收货人业务组:</td>
        <td class="oddrow-l"><asp:TextBox ID="txtappendReceiverGroup" runat="server" /></td>
    </tr>
</table>
