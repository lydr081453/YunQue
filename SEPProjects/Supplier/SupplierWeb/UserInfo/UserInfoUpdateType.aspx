<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoUpdateType.aspx.cs" Inherits="SupplierWeb.UserInfo.UserInfoUpdateType" MasterPageFile="~/MainPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function validtext() {
            var msg = "";
            if (msg != "") {
                alert(msg);
                return false;
            }
            else
                return true;
        }

        function testNum(a) {
            a += "";
            a = a.replace(/(^[\\s]*)|([\\s]*$)/g, "");
            if (a != "" && !isNaN(a) && Number(a) > 0) {
                return true;
            }
            else {
                return false;
            }
        }
    
    </script>
    <div id="divPayment" runat="server" width="100%">
        <asp:DataList ID="rp1" ItemStyle-BorderStyle="None" runat="server" OnItemDataBound="rp1_ItemDataBound"
            RepeatDirection="Vertical" Width="100%">
            <ItemTemplate>
                <table width="100%" style="border: 0 0 0; margin: 0 0 0 0;">
                    <tr>
                        <td colspan="1" width="15%">
                            <asp:Label ID="lblMain" runat="server" ForeColor="SteelBlue" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="3" style="border-bottom: 1px dotted #CC3333" width="85%">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:DataList ID="ListLevel2" ItemStyle-BorderStyle="None" runat="server" OnItemDataBound="List2_ItemDataBound"
                                 Width="100%" ItemStyle-VerticalAlign="Top"
                                RepeatColumns="4" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                    <table width="100%" style="border: 0 0 0; margin: 0 0 0 0">
                                        <tr>
                                            <td style=" font:small-caption">
                                                <asp:CheckBox ID="ckxSelected" runat="server" /><asp:HiddenField ID="hidTypeID" runat="server"
                                                    Value='<%# Eval("id") %>' />
                                                <asp:Label ID="lblName" Style="width: 80px" runat="server" Text='<%# Eval("TypeName") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DataList ID="ListLevel3" ItemStyle-BorderStyle="None" runat="server" Width="100%"
                                                    ItemStyle-VerticalAlign="Top" RepeatColumns="1" RepeatDirection="Horizontal">
                                                    <ItemTemplate>
                                                        <table width="100%" style="border: 0 0 0; margin: 0 0 0 0">
                                                            <tr>
                                                                <td>
                                                                    <%# Eval("TypeName") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                        <br />
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <table width="100%" class="tableForm">
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" class="widebuttons" runat="server" Text="保存" ValidationGroup="Save"
                    OnClick="btnSave_Click" OnClientClick="return validtext();" />
                <asp:Button ID="btnClose" class="widebuttons" runat="server" Text="返回" OnClick="btnClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
