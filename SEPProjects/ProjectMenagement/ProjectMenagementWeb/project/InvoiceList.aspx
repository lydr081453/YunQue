<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InvoiceList.aspx.cs" Inherits="FinanceWeb.project.InvoiceList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" type="text/javascript">


        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
       
    </script>

    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <li>
                                <a href="InvoiceEdit.aspx">添加销项税</a></li>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
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
                                    <td class="oddrow-l" colspan="2">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                            padding-top: 4px;">
                            <table width="100%">
                                <tr>
                                    <td class="heading" colspan="4">
                                        销项税登记列表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False"
                                            OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" PageSize="20"
                                            EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvG_PageIndexChanging"
                                            AllowPaging="true" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                    <asp:BoundField DataField="groupname" HeaderText="成本所属组" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                    <asp:BoundField DataField="InvoiceCode" HeaderText="发票号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:TemplateField HeaderText="发票金额" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <%# decimal.Parse(Eval("InvoiceAmounts").ToString()).ToString("#,##0.00")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="发票日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <%# DateTime.Parse(Eval("InvoiceDate").ToString()).ToString("yyyy-MM-dd")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="退票日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <%# Eval("CancelDate") == null?"" : DateTime.Parse(Eval("CancelDate").ToString()).ToString("yyyy-MM-dd")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="CreatorEmployeeName" HeaderText="创建人" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" />
                                                <asp:TemplateField HeaderText="创建日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <%# DateTime.Parse(Eval("CreateDate").ToString()).ToString("yyyy-MM-dd") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="9%">
                                                    <ItemTemplate>
                                                        <%# ESP.Finance.Utility.InvoiceStatus.GetStatusDesc(int.Parse(Eval("invoicestatus").ToString())) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        
                                                        <a href='InvoiceEdit.aspx?invoiceId=<%#Eval("invoiceId").ToString() %>' ><img src="/images/edit.gif" border="0" /></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="退票" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkConfirm" runat="server" CommandArgument='<%# Eval("invoiceId") %>'
                                                            CommandName="Confirm" Text="<img src='../../images/Audit.gif' title='确认' border='0'>"
                                                            OnClientClick="return confirm('你确定要退票吗？');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
