<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Client_ProductLineAddAndEdit"
    Title="添加产品线" Codebehind="ProductLineAddAndEdit.aspx.cs" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
<script language="javascript">
    function openClient() {
        window.open("ProductSelectClientList.aspx?alert=1&Operate=Edit", "关联客户", "<%=ESP.Media.Access.Utilities.Global.OpenClass.Common %>");   
    }
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="Hidden1" runat="server" />
    <table width="100%" border="1" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                <asp:Label ID="labHeading" runat="server">
                    <asp:Label ID="labClient" runat="server"></asp:Label></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                产品线名称：
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:TextBox ID="txtProductLineName" runat="server" Width="80%" MaxLength="50"></asp:TextBox><font
                    color="red"> *</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtProductLineName" Display="None" ErrorMessage="请填写产品线名称"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow">所属客户名称：</td>
            <td class="oddrow-l" colspan="3"><asp:TextBox ID="txtCustom" Width="250px" runat="server" Enabled="false" /><font color="red"> * </font><asp:Button ID="btnLink" CausesValidation="false" runat="server" Text="关联客户" CssClass="bigwidebuttons" OnClientClick="openClient();return false;" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtCustom" Display="None" ErrorMessage="请关联客户"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="hidCustom" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ShowMessageBox="True" ShowSummary="False" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                产品线图片：
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:FileUpload ID="fplTitle" runat="server" Width="80%" Height="24px" unselectable="on" />
                <asp:Image ID="imgTitle" runat="server" ImageAlign="bottom" Height="20px" Width="20px" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                描述：
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:TextBox ID="txtDes" runat="server" Height="137px" Width="80%" 
                    TextMode="MultiLine" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <input type="hidden" id="hidUrl" runat="server" />
        <%--<script type="text/javascript">
                   document.getElementById("<% = imgTitle.ClientID %>").width=1;
                 document.getElementById("<% = imgTitle.ClientID %>").height=1;
                </script>--%>
    </table>
    <table style="width:100%" border="0">
        <tr>
            <td style="text-align: right" colspan="4">
                <asp:Button ID="btnOk" runat="server" CssClass="widebuttons" Text="保存" CausesValidation="true"
                     OnClick="btnOk_Click"></asp:Button>
                <input type="reset" class="widebuttons" value="重置" />
                <asp:Button ID="btnBack" runat="server" Text="返回" CausesValidation="false" CssClass="widebuttons" OnClick="btnBack_Click" />
                <input type="button" value="关闭" id="backpage" causesvalidation="false" runat="server" onclick="javascipt:window.close();return false;"
                                class="widebuttons" />
                <%--<input type="button" value="返回" onclick="returnurl();" class="widebuttons" />--%>
            </td>
        </tr>
    </table>
</asp:Content>
