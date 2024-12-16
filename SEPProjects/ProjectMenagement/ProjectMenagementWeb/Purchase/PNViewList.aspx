<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="PNViewList.aspx.cs" Inherits="FinanceWeb.Purchase.PNViewList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                关键字:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtKey" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                付款申请状态:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlStatus">
                    <asp:ListItem Text="请选择.." Value="-1"></asp:ListItem>
                    <asp:ListItem Text="待初审人审核" Value="90"></asp:ListItem>
                    <asp:ListItem Text="待一级总监审核" Value="91"></asp:ListItem>
                    <asp:ListItem Text="待二级级总监审核" Value="92"></asp:ListItem>
                    <asp:ListItem Text="待财务预审" Value="100"></asp:ListItem>
                    <asp:ListItem Text="待财务复审" Value="110"></asp:ListItem>
                    <asp:ListItem Text="待财务终审" Value="120"></asp:ListItem>
                    <asp:ListItem Text="借款待报销" Value="136"></asp:ListItem>
                    <asp:ListItem Text="已付款" Value="140"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                提交日期:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBeginDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
                --
                <asp:TextBox ID="txtEndDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
            </td>
            <td colspan="2" class="oddrow-l">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
                <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                    OnClick="btnSearchAll_Click" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td class="heading">
                付款申请列表
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                    OnRowDataBound="gvG_RowDataBound" PageSize="10"
                    EmptyDataText="暂时没有付款申请记录" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvG_PageIndexChanging"
                    AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="PurchasePayID" HeaderText="帐期流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="PRID" HeaderText="pr流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:TemplateField HeaderText="PN号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <a href="<%# int.Parse((Eval("returnType") == DBNull.Value || Eval("returnType") == null) ? "0" : Eval("returnType").ToString()) == (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift ? "/ForeGift/ForegiftDetail.aspx" : "/Purchase/ReturnDisplay.aspx"  %>?<% =ESP.Finance.Utility.RequestName.ReturnID %>=<%#Eval("ReturnID") %>"
                                    target="_blank">
                                    <%#Eval("ReturnCode")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectID" HeaderText="项目号流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:TemplateField ItemStyle-Width="8%" HeaderText="PR号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPR"><%#Eval("PRNO") %></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblName" CssClass="userLabel"><%#Eval("RequestEmployeeName")%></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:TemplateField HeaderText="预付金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labExpectPaymentPrice" Text='<%# Eval("PreFee") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否发票" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblInvoice" Text='<%# Eval("IsInvoice") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="付款状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatusName" />
                                <asp:HiddenField ID="hidStatusNameID" Value='<%# Eval("ReturnStatus") %>' runat="server" />
                                <asp:HiddenField ID="hidReturnID" Value='<%# Eval("ReturnID") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="帐期类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPaymentType" Text='<%# Eval("PaymentTypeName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="供应商" DataField="supplierName" />
                        <asp:TemplateField HeaderText="审批状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <a href="/project/ProjectWorkFlow.aspx?Type=return&FlowID=<%#Eval("ReturnID") %>"
                                    target="_blank">
                                    <img src="/images/AuditStatus.gif" border="0px;" title="审批状态"></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
