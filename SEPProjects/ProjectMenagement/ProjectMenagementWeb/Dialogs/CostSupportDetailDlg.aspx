<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Dialogs_CostSupportDetailDlg" Title="成本明细添加" CodeBehind="CostSupportDetailDlg.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function validtext() {
            var msg = "";
            if (document.getElementById("<%=txtCost.ClientID %>").value == "") {
                msg += "请输入OOP成本." + "\n";
            }
            var cost = document.getElementById("<%=txtCost.ClientID %>").value.replace(/,/g, '');
            if (!testNum(cost)) {
                msg += " OOP成本金额输入错误！" + "\n";
            }
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
            if (a != "" && !isNaN(a) && Number(a) >= 0) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">OOP添加
            </td>
        </tr>
        <tr>
            <td class="oddrow">OOP成本：
            </td>
            <td class="oddrow-l" colspan="3">OOP金额：<asp:TextBox runat="server" ID="txtCost" MaxLength="21"></asp:TextBox><%--<font color="red">*</font>--%>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Font-Names="Arial" Font-Size="9pt"
                    ValidationGroup="Save" ControlToValidate="txtCost" Display="Dynamic" ErrorMessage="金额为数字"
                    ValidationExpression="^[-+]?\d+(\.\d+)?$">OOP成本金额格式有误.</asp:RegularExpressionValidator>
                <asp:Label ID="lblOOP" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <input type="hidden" runat="server" id="hidTotalCost" />
            </td>
        </tr>
        <tr>
            <td colspan="4"></td>
        </tr>
        <tr>
            <td class="oddrow" colspan="4">第三方采购成本：
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnSubmit2" class="widebuttons" runat="server" Text="提交" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnClose2" class="widebuttons" runat="server" Text="关闭" OnClick="btnClose_Click" />
            </td>
        </tr>
    </table>
    <div id="divPayment" runat="server">
        <asp:DataList ID="rp1" ItemStyle-BorderStyle="None" runat="server" OnItemDataBound="rp1_ItemDataBound"
            RepeatDirection="Vertical">
            <ItemTemplate>
                <table width="100%" style="border: 0 0 0; margin: 0 0 0 0;">
                    <tr>
                        <td colspan="1" width="15%">
                            <asp:Label ID="lblMain" runat="server" ForeColor="SteelBlue" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="3" style="border-bottom: 1px dotted #CC3333" width="85%"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:DataList ID="ListLevel2" ItemStyle-BorderStyle="None" runat="server" OnItemDataBound="List2_ItemDataBound"
                                OnItemCommand="List2_ItemCommand" Width="100%" ItemStyle-VerticalAlign="Top"
                                RepeatColumns="4" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                    <table width="100%" style="border: 0 0 0; margin: 0 0 0 0">
                                        <tr>
                                            <td style="font: small-caption">
                                                <asp:CheckBox ID="ckxSelected" runat="server" /><asp:HiddenField ID="hidTypeID" runat="server"
                                                    Value='<%# Eval("TypeID") %>' />
                                                <asp:Label ID="lblName" Style="width: 80px" runat="server" Text='<%# Eval("TypeName") %>' />
                                                金额：<asp:TextBox ID="txtAmount" runat="server" Width="60px" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                    ValidationGroup="Save" ControlToValidate="txtAmount" Display="Dynamic" ErrorMessage="金额为数字"
                                                    ValidationExpression="^[-+]?\d+(\.\d+)?$">成本金额格式有误.</asp:RegularExpressionValidator>
                                                <asp:Label ID="lblUsedAmount" runat="server"></asp:Label>

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
                <asp:Button ID="btnSave" class="widebuttons" runat="server" Text="提交" OnClick="btnSave_Click" />
                <asp:Button ID="btnClose" class="widebuttons" runat="server" Text="关闭" OnClick="btnClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
