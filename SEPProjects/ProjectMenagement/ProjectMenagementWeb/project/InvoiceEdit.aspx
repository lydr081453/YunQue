<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InvoiceEdit.aspx.cs" Inherits="FinanceWeb.project.InvoiceEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script language="javascript" src="/public/js/dialog.js"></script>
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="js/iframeTools.js"></script>

    <script language="javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function setProjectInfo(projectId, projectCode) {
            $("#<%=lblProjectCode.ClientID %>").text(projectCode);
            $("#<%=hidprojectId.ClientID %>").val(projectId);
        }

        function bindList(paymentIds) {
            $("#<%=hidpaymentIds.ClientID %>").val("," + paymentIds + ",");
            document.getElementById("<%=lnkList.ClientID %>").click();
        }

        function chkPayment(source, args) {
            if ($("#<%=hidpaymentIds.ClientID %>").val() == "")
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function chkProject(source, args) {
            if ($("#<%=hidprojectId.ClientID %>").val() == "")
                args.IsValid = false;
            else
                args.IsValid = true;
        }
        
        $(function() {
            $("#<%=txtInvoicePrice.ClientID %>").blur(function() {
                var invoicePrice = $("#<%=txtInvoicePrice.ClientID %>").val();
                var projectId = $("#<%=hidprojectId.ClientID %>").val();
                $.ajax({
                    type: "Post",
                    url: "InvoiceEdit.aspx/changePirce",
                    data: "{'projectId1':'"+projectId+"','invoicePrice1':'"+invoicePrice+"'}",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function(data) {
                        $("#<%=labPirce.ClientID %>").text(data.d["price"]);
                        $("#<%=labTax.ClientID %>").text(data.d["tax"]);
                    },
                    error: function(err) {
                        
                    }
                });
                return false;
            });
        });

    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                销项税信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                项目号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblProjectCode" runat="server"><asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="请选择项目号" Display="Static" ClientValidationFunction="chkProject"></asp:CustomValidator></asp:Label><asp:HiddenField ID="hidprojectId" runat="server" />
                &nbsp;<input type="button" value=" 选择项目号 " onclick="dialog('选择项目号','iframe:InvoiceProjectList.aspx', '600px', '400px','text');" />
            </td>
        </tr>
                <tr>
            <td class="oddrow" style="width: 15%">
                发票号:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtInvoiceCode" runat="server" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtInvoiceCode" Display="Static"
                    runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 15%">
                发票金额:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtInvoicePrice" runat="server" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtInvoicePrice" Display="Static"
                    runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                不含税金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="labPirce" />
            </td>
            <td class="oddrow" style="width: 20%">
                税金:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label runat="server" ID="labTax" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                开票日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtInvoiceDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtInvoiceDate" Display="Static"
                    runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 15%">
                退票日期:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="labCancelDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="80%" Height="60px" /></td>
        </tr>
    </table>
    <asp:UpdatePanel runat="server"><ContentTemplate>
    <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False"
             Width="100%">
            <Columns>
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
                <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:LinkButton Text="删除" OnClick="lnkDel_Click" CausesValidation="false" ID="lnkDel" runat="server" CommandArgument='<%# Eval("paymentId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
    <asp:HiddenField ID="hidpaymentIds" runat="server" />
    <asp:LinkButton ID="lnkList" runat="server" OnClick="lnkList_Click" CausesValidation="false" />
    </ContentTemplate></asp:UpdatePanel>
    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="请选择付款通知" Display="Static" ClientValidationFunction="chkPayment"></asp:CustomValidator>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button type="button" ID="btnYes" runat="server" Text=" 保存 "
                    class="widebuttons" OnClick="btnYes_Click" />&nbsp;
                                    <asp:Button type="button" ID="btnSubmit" runat="server" Text=" 提交 "
                    class="widebuttons" OnClick="btnSubmit_Click" />
                &nbsp;<asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click" CausesValidation="false"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
