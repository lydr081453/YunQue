<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="RefundmentEdit.aspx.cs" Inherits="FinanceWeb.Purchase.RefundmentEdit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript">
      function selectPN(returnId,returncode,projectcode,amount)
      {
         var hid =document.getElementById("<%=hidReturnId.ClientID %>");
         hid.value = returnId;
         document.getElementById("<%=lblReturnCode.ClientID %>").innerHTML = returncode;
         document.getElementById("<%=lblProjectCode.ClientID %>").innerHTML = projectcode;
         document.getElementById("<%=lblAmount.ClientID %>").innerHTML = amount;
      }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                退款申请
            </td>
        </tr>
        <tr>
             <td class="oddrow" style="width: 15%">
                关键字:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtKeyword" runat="server" /> &nbsp; <asp:Button runat="server" ID="btnSelect" OnClick="btnSelect_Click" CssClass="widebuttons" Text=" 查询 " />
            </td>
        </tr>
         <tr>
            <td class="oddrow-l" colspan="4">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"  OnRowDataBound="gvG_RowDataBound"
                    PageSize="10" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvG_PageIndexChanging"
                     AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="returnid" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PRID" HeaderText="PRID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField ItemStyle-Width="5%" HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            <asp:Literal runat="server" ID="literal"></asp:Literal>
                            </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="PN号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <a href="/Purchase/ReturnDisplay.aspx?<% =ESP.Finance.Utility.RequestName.ReturnID %>=<%#Eval("ReturnID") %>"
                                    target="_blank">
                                    <%#Eval("ReturnCode")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("RequestEmployeeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:TemplateField HeaderText="预付金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labExpectPaymentPrice" Text='<%# Eval("PreFee") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="起始时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBeginDate" Text='<%# Eval("PReBeginDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="付款状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatusName" />
                                <asp:HiddenField ID="hidStatusNameID" Value='<%# Eval("ReturnStatus") %>' runat="server" />
                                <asp:HiddenField ID="hidReturnID" Value='<%# Eval("ReturnID") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="帐期类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPaymentType" Text='<%# Eval("PaymentTypeName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="供应商名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="13%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSupplier" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                申请单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
            <asp:Label runat="server" ID="lblReturnCode"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                项目号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                 <asp:Label runat="server" ID="lblProjectCode"></asp:Label>
            </td>
        </tr>
        <tr>
         <td class="oddrow" style="width: 15%">
                申请金额:
            </td>
             <td class="oddrow-l" colspan="3">
                 <asp:Label runat="server" ID="lblAmount"></asp:Label>
            </td>
            </tr>
            <tr>
            <td class="oddrow" style="width: 15%">
                退款金额:<input type="hidden" runat="server" id="hidReturnId" />
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtRefund"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                退款原因:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRemark" Width="60%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                &nbsp;<input type="button" id="btnSubmit" value=" 提交 " runat="server" class="widebuttons"
                      onserverclick="btnSubmit_Click" />&nbsp;
                &nbsp;<asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
