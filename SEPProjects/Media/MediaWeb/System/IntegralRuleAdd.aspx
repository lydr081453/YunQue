<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="System_IntegralRuleAdd" Title="Untitled Page" Codebehind="IntegralRuleAdd.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="Server">

    <script type="text/javascript">
    function check()
    {

        //关键字
        if (document.getElementById("<% = txtkey.ClientID %>").value =="")
        {
            document.getElementById("<% = labkey.ClientID %>").innerHTML="关键字不能为空！";
            document.getElementById("<% = txtkey.ClientID %>").focus();
            return  false;
        }

        //操作名称
        if (document.getElementById("<% = txtAltname.ClientID %>").value =="")
        {
            document.getElementById("<% = labAltnamee.ClientID %>").innerHTML="操作名称不能为空！";
            document.getElementById("<% = txtAltname.ClientID %>").focus();
            return  false;
        }

    //判断积分必须是数字
     if (document.getElementById("<% = txtIntegral.ClientID %>").value.search(/^\d+$/) == -1)
        {
          document.getElementById("<% = labIntegral.ClientID %>").innerHTML="积分必须是数字！";
           document.getElementById("<% = txtIntegral.ClientID %>").focus();
            return  false;
        }

    }

    </script>

</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="contentMain" runat="Server">
    <table width="100%" border="1" class="tableForm">
        <tr>
            <td colspan="2" class="heading">
                积分规则
            </td>
        </tr>
        <tr>
            <td width="40%" class="oddrow">
                操作类型：
            </td>
            <td width="60%" class="oddrow-l">
                <asp:DropDownList ID="ddlOperateType" runat="server" CssClass="fixddl" OnSelectedIndexChanged="ddlOperateType_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="40%" style="height: 23px" class="oddrow">
                操作对象：
            </td>
            <td width="60%" style="height: 23px" class="oddrow-l">
                <asp:DropDownList ID="ddlTable" runat="server" CssClass="fixddl" OnSelectedIndexChanged="ddlTable_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="40%" class="oddrow">
                关键字：
            </td>
            <td width="60%" class="oddrow-l">
                <asp:TextBox ID="txtkey" runat="server" Width="120px" ReadOnly="true"></asp:TextBox><asp:Label
                    ID="labkey" runat="server" ForeColor="Red"></asp:Label>
                <font color="red"> *</font>
            </td>
        </tr>
        <tr>
            <td width="40%" class="oddrow">
                操作名称：
            </td>
            <td width="60%" class="oddrow-l">
                <asp:TextBox ID="txtAltname" runat="server" Width="120px" ReadOnly="true"></asp:TextBox><asp:Label
                    ID="labAltnamee" runat="server" ForeColor="Red"></asp:Label>
                <font color="red"> *</font>
            </td>
        </tr>
        <tr>
            <td width="40%" class="oddrow">
                积分：
            </td>
            <td width="60%" class="oddrow-l">
                <asp:TextBox ID="txtIntegral" runat="server" Width="120px"></asp:TextBox><font color="red"> *</font><asp:Label
                    ID="labIntegral" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width:100%" border="0">
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" CssClass="widebuttons"
                    OnClientClick="return check();" />
                <asp:Button ID="btnBack" Text="返回" OnClick="btnBack_Click" runat="server" CssClass="widebuttons" />
            </td>
        </tr>
    </table>
</asp:Content>
