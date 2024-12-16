<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="PaymentDetailDlg.aspx.cs" Inherits="FinanceWeb.Dialogs.PaymentDetailDlg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function PaymentValid() {
            var msg = "";
            var cur = document.getElementById("<% = txtAmount.ClientID %>").value.replace(/,/g, '');
            if (document.getElementById("<% =txtContent.ClientID %>").value == "") {
                msg += "请输入付款内容" + "\n";
            }
            if (document.getElementById("<% =txtAmount.ClientID %>").value == "") {
                msg += "请输入付款金额" + "\n";
            }
            else if (!testNum(cur)) {
                msg += "付款金额输入错误！" + "\n";
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

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
               <asp:Label runat="server" ID="lblProjectCode"></asp:Label> 付款通知信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款通知号码：<br />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblPaymentCode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款通知内容：<br />
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="lblPaymentContent"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                付款通知时间：<br />
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="lblPaymentDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款通知金额：<br />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblPaymentAmount"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">
                财务确认金额：<br />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblBudgetConfirm"></asp:Label>
            </td>
        </tr>
            <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                付款明细内容
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                明细内容：<br />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtContent" MaxLength="500" runat="server" Width="40%" /><font color="red">
                    *</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                项目金额：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtAmount" runat="server" MaxLength="11" /><font color="red"> *</font>
            </td>
        </tr>
                <tr>
            <td class="oddrow" style="width: 15%">
                相关附件：
            </td>
            <td class="oddrow-l" colspan="3">
              <asp:FileUpload ID="fileupDetail" runat="server" Width="60%" /><asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="/images/ico_04.gif" Visible="false"></asp:HyperLink>
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
                <asp:Button ID="btnSave" Text=" 保存 " class="widebuttons" runat="server" OnClick="btnSave_Click" />&nbsp;&nbsp;
                 <asp:Button ID="btnExport" Text=" 导出 " class="widebuttons" runat="server" OnClick="btnExport_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " CssClass="widebuttons" OnClientClick="window.close();" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                    OnRowCommand="gvPayment_RowCommand" OnRowDataBound="gvPayment_RowDataBound" Width="100%">
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PaymentContent" HeaderText="明细内容" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="30%" />
                        <asp:TemplateField HeaderText="明细金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="PaymentPreAmount" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="相关附件" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemTemplate>
                                 <a id="aDownLoad" target="_blank" href='/Dialogs/ContractFileDownLoad.aspx?FileType=1&ContractID=<%# Eval("Id") %>'>
                                <img src="/images/ico_04.gif" border="0" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Remark" HeaderText="备注" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="25%" />
                        <asp:TemplateField HeaderText="编辑" ItemStyle-Width="5%">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:HyperLink runat="server" Text="<img src='/images/edit.gif' title='编辑' border='0'>" ID="hyperEdit"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Id") %>'
                                    CommandName="Del" Text="<img src='/images/disable.gif' title='删除' border='0'>"
                                    OnClientClick="return confirm('你确定删除吗？');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Visible="false" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
