<%@ Page Language="C#" AutoEventWireup="true" Inherits="Dialogs_PaymentDlg"
    MasterPageFile="~/MasterPage.master" Title="付款内容" Codebehind="PaymentDlg.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function PaymentValid() {
            var msg = "";
            var total = parseFloat(document.getElementById("<%=hidTotal.ClientID %>").value);
            var cur = document.getElementById("<% = txtContent.ClientID %>").value.replace(/,/g, '');
            if (document.getElementById("<% =dpDailyStartTime.ClientID %>").value == "") {
                msg += "请输入付款时间" + "\n";
            }
            if (document.getElementById("<% =txtpaymentContent.ClientID %>").value == "") {
                msg += "请输入付款内容" + "\n";
            }
            if (document.getElementById("<% =txtContent.ClientID %>").value == "") {
                msg += "请输入付款金额" + "\n";
            }
            //else if (!testNum(cur)) {
            //    msg += "付款金额输入错误！" + "\n";
            //}
            //else if (parseFloat(cur) > total) {
            //    msg += "付款金额大于申请总金额！" + "\n";
            //}
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
            var div = document.getElementById("divPayment");
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
               &nbsp;
            </td>
            <td class="oddrow-l" colspan="3">
                 <p style="color:red;">全额开票:金额为含客返项目金额，客返填写负数<br />
                    差额开票:金额为不含客返项目金额
                </p>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款通知内容：<br />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtpaymentContent" MaxLength="500" runat="server" Width="40%" /><font color="red">
                    *</font>
                <asp:Button ID="btnSelect" Text="搜索" CausesValidation="false" class="widebuttons"
                    runat="server" OnClick="btnSelect_OnClick" />
                <input type="hidden" id="hidPaymentContentID" runat="server" />
                <img src="/images/differ.jpg" width="10px" height="10px" /><font color="red">请从列表中选择付款内容</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款通知时间：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="dpDailyStartTime" onkeyDown="return false; " Style="cursor: hand"
                    runat="server" onclick="setDate(this);" /><font color="red"> *</font>
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
                付款通知金额：
            </td>
            <td class="oddrow-l" colspan="3">
                <input type="hidden" id="hidTotal" runat="server" />
                <asp:TextBox ID="txtContent" runat="server" MaxLength="11"/><font color="red"> *</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                备注：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Width="70%" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
             <asp:Button ID="btnAutoComplete" Text=" 系统自动添加 " class="widebuttons" runat="server" 
                    OnClick="btnAutoComplete_Click" Visible ="false"/>
                <asp:Button ID="btnNewPayment" Text=" 保存并添加 " class="widebuttons" runat="server" OnClientClick="return PaymentValid();"
                    OnClick="btnNewPayment_Click" />
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnClose_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" DataKeyNames="PaymentID"
                    OnRowCommand="gvPayment_RowCommand" OnRowDataBound="gvPayment_RowDataBound" PageSize="20" OnPageIndexChanging="gvPayment_PageIndexChanging"
                    AllowPaging="True" Width="100%">
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PaymentPreDate" HeaderText="付款通知时间" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="15%" />
                        <asp:BoundField DataField="PaymentContent" HeaderText="付款通知内容" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="30%" />
                        <asp:TemplateField HeaderText="付款通知金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentBudget" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Remark" HeaderText="备注" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="25%" />
                         <asp:TemplateField HeaderText="编辑" ItemStyle-Width="5%">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                 <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("PaymentID") %>'
                                    CommandName="Edit" Text="<img src='/images/edit.gif' title='编辑' border='0'>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("PaymentID") %>'
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
    <div id="divPayment" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <asp:GridView ID="gvPaymentContent" runat="server" AutoGenerateColumns="False" DataKeyNames="PaymentContentID"
                        OnRowCommand="gvPaymentContent_RowCommand" OnPageIndexChanging="gvPaymentContent_PageIndexChanging"
                        OnRowDataBound="gvPaymentContent_RowDataBound" PageSize="10" EmptyDataText="暂时没有付款通知信息"
                        PagerSettings-Mode="NumericFirstLast" AllowPaging="true" Width="100%">
                        <Columns>
                            <asp:ButtonField Text="选择" CommandName="Add" ButtonType="button" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />
                            <asp:BoundField DataField="PaymentContentID" HeaderText="PaymentContentID" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="PaymentContent" HeaderText="付款内容" ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="80%" />
                            <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("PaymentContentID") %>'
                                        CommandName="Del" Text="<img src='/images/disable.gif' title='删除' border='0'>"
                                        OnClientClick="return confirm('你确定删除吗？');" />
                                </ItemTemplate>
                            </asp:TemplateField>
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
