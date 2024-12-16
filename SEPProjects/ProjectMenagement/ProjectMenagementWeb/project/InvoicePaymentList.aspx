<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoicePaymentList.aspx.cs"
    Inherits="FinanceWeb.project.InvoicePaymentList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script language="javascript" type="text/javascript">
        function chkSelect() {
            if ($("[name=chk]:checked").length == 0) {
                alert("请选择付款通知！");
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False"
             Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <input type="checkbox" name="chk" id="chk" value='<%# Eval("paymentId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PaymentContent" HeaderText="付款通知内容" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="20%" />
                <asp:TemplateField HeaderText="付款通知时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("PaymentPreDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="付款通知金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="lblPaymentBudget" runat="server" Text='<%# Decimal.Parse(Eval("PaymentBudget").ToString()).ToString("#,##0.00")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnOk" runat="server" Text="确定选择" OnClick="btnOk_Click" OnClientClick="return chkSelect();" />&nbsp;
    </div>
    </form>
</body>
</html>
