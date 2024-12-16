<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewIndustry.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="MediaWeb.NewMedia.BaseData.NewIndustry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="server">
<script type="text/javascript">
    function check() {
        //内容
        var meg = "";

        if (document.getElementById("<% =txtName.ClientID %>").value == "") {
            meg += "请输入行业属性名称！";
        }
        if (meg != "") {
            alert(meg);
            return false;
        }

    }
</script>
    <table width="100%">
        <tr>
            <td>
                <table width="100%" border="0" class="tableForm">
                    <tr>
                        <td colspan="4" class="menusection-Packages">
                            行业属性信息维护
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="heading">
                            行业属性
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            行业属性名称：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox><font color="red"> *</font>
                        </td>
                        <td class="oddrow" style="width: 20%">
                            &nbsp;
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table style="width:100%" border="0">
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Button ID="btnAdd" runat="server" OnClientClick="return check();"  OnClick="btnAdd_Click" CssClass="widebuttons" />
                            <asp:Button ID="btnReturn" runat="server" Text="返回" OnClick="btnReturn_Click" CssClass="widebuttons" /><asp:Button ID="btnClose" runat="server" Text="关闭" Visible="false" CssClass="widebuttons" OnClientClick="window.close();return false;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>     
    </table>
</asp:Content>

