<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Dialogs_PaymentInvoiceDlg" MasterPageFile="~/MasterPage.master" Title="付款内容" Codebehind="PaymentInvoiceDlg.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function PaymentValid() {
            var msg = "";
            var total = parseFloat(document.getElementById("<%=hidTotal.ClientID %>").value);
            var cur = document.getElementById("<% = txtAmount.ClientID %>").value.replace(/,/g, '');
            var differ = document.getElementById("<% = txtDiffer.ClientID %>").value.replace(/,/g, '');
            if (differ == "")
                differ = "0";
            if (document.getElementById("<% =txtInvoiceNo.ClientID %>").value == "") {
                msg += "请输入付款内容" + "\n";
            }
            if (document.getElementById("<% =txtAmount.ClientID %>").value == "") {
                msg += "请输入付款金额" + "\n";
            }
            else if (!testNum(cur)) {
                msg += "付款金额输入错误！" + "\n";
            }
            else if (parseFloat(cur) > total) {
                msg += "付款金额大于申请总金额！" + "\n";
            }
            if (!testNum(differ)) {
                msg += "美金汇兑差额输入错误！" + "\n";
            }
            if (msg == "")
                return true;
            else {
                alert(msg);
                return false;
            }
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

        function ShowGrid() {
            var div = document.getElementById("divInvoice");
            if (div.style.display == "" || div.style.display == "none") {
                div.style.display = "block";
            }
            else {
                div.style.display = "none";
            }
            return true;
        }
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                付款内容
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                发票号：<br />
            </td>
            <td class="oddrow-l" colspan="3">
             <asp:TextBox ID="txtInvoiceNo" MaxLength="500" runat="server" Width="40%" />
   <%--             <asp:Label runat="server"  ID="txtInvoiceNo" Width="40%"></asp:Label>--%>
                <font
                    color="red"> *</font>
                <asp:Button ID="btnSelect" Text="搜索" CausesValidation="false" class="widebuttons"
                    runat="server" OnClick="btnSelect_OnClick" />
                <input type="hidden" id="hidInvoiceID" runat="server" />
                <img src="/images/differ.jpg" width="10px" height="10px" /><font color="red">请从列表中选择发票号</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款通知余额：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
            </td>
        </tr>
          <tr>
            <td class="oddrow" style="width: 15%">
                当前发票余额：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblInvoiceBalance" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款通知金额：
            </td>
            <td class="oddrow-l" colspan="3">
                <input type="hidden" id="hidTotal" runat="server" />
                <asp:TextBox ID="txtAmount" runat="server" MaxLength="11" /><font color="red"> *</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                美金汇兑差额：
            </td>
            <td class="oddrow-l" colspan="3">
                <input type="hidden" id="hidDiffer" runat="server" />
                <asp:TextBox ID="txtDiffer" runat="server" MaxLength="11" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                描述：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtDes" runat="server" Width="70%" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:Button ID="btnNewInvoiceDetail" Text=" 保存并添加 " class="widebuttons" runat="server"
                    OnClientClick="return PaymentValid();" OnClick="btnNewInvoiceDetail_Click" />
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnClose_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvInvoiceDetail" runat="server" AutoGenerateColumns="False" DataKeyNames="InvoiceDetailID"
                    OnRowCommand="gvInvoiceDetail_RowCommand" OnRowDataBound="gvInvoiceDetail_RowDataBound"
                    PageSize="20" OnPageIndexChanging="gvInvoiceDetail_PageIndexChanging" AllowPaging="True"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="InvoiceNo" HeaderText="发票号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="15%" />
                        <asp:BoundField DataField="Amounts" HeaderText="付款金额" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />
                        <asp:TemplateField HeaderText="美金汇兑差额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="lblDiffer" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                  <%--      <asp:TemplateField HeaderText="付款通知金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentAmount" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField="Description" HeaderText="描述" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="25%" />
                        <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("InvoiceDetailID") %>'
                                    CommandName="Del" Text="<img src='/images/disable.gif' title='删除' border='0'>"
                                    OnClientClick="return confirm('你确定删除吗？');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Visible="false" />
                </asp:GridView>
            </td>
        </tr>
        <tr id="trTotal" runat="server">
            <td class="oddrow-l" colspan="4" align="right">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 5%; border: 0 0 0 0">
                        </td>
                        <td style="width: 15%; border: 0 0 0 0">
                        </td>
                        <td style="width: 30%; border: 0 0 0 0">
                        </td>
                        <td style="width: 15%; border: 0 0 0 0" align="right">
                            <asp:Label ID="lblTotal" runat="server" Style="text-align: right" Width="100%" />
                        </td>
                        <td style="width: 25%; border: 0 0 0 0">
                            <asp:Label ID="lblBlance" runat="server" Style="text-align: right" Width="100%" />
                        </td>
                        <td style="width: 10%; border: 0 0 0 0">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divInvoice" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <asp:GridView ID="gvInvoice" runat="server" AutoGenerateColumns="False" DataKeyNames="InvoiceID"
                        OnPageIndexChanging="gvInvoice_PageIndexChanging" OnRowCommand="gvInvoice_RowCommand"
                        OnRowDataBound="gvInvoice_RowDataBound" PageSize="10" EmptyDataText="暂时没有发票信息"
                        PagerSettings-Mode="NumericFirstLast" AllowPaging="true" Width="100%">
                        <Columns>
                            <asp:ButtonField Text="选择" CommandName="Add" ButtonType="button" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />
                            <asp:BoundField DataField="InvoiceID" HeaderText="InvoiceID" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="InvoiceStatus" HeaderText="InvoiceStatus" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="InvoiceCode" HeaderText="发票号" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />
                            <asp:BoundField DataField="CustomerName" HeaderText="客户" />
                            <asp:BoundField DataField="BranchName" HeaderText="分公司" />
                            <asp:TemplateField HeaderText="发票总额" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblInvoiceAmounts" Text='<%# Eval("InvoiceAmounts") == null ? "0.00" : Convert.ToDecimal(Eval("InvoiceAmounts")).ToString("#,##0.00") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="登记日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPaymentPreDate" Text='<%# Eval("CreateDate") == null ? "" : DateTime.Parse(Eval("CreateDate").ToString()).ToString("yyyy-MM-dd")  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CreatorEmployeeName" HeaderText="登记人" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <PagerStyle Font-Size="X-Large" ForeColor="SteelBlue" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
