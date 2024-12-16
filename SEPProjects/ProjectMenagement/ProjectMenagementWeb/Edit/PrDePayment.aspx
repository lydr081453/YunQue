<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="PrDePayment.aspx.cs" Inherits="FinanceWeb.Edit.PrDePayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="/UserControls/Project/EditTab.ascx" TagName="tab" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>

    <table style="width: 100%">
        <tr>
            <td width="100%" align="center">
                <uc1:tab ID="tab" runat="server" TabIndex="5" />
            </td>
        </tr>
        <tr>
            <td style="padding: 4px;">
                <table style="width: 100%;">
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
                                        <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                                            OnClick="btnSearchAll_Click" />
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
                                        采购付款列表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                                            OnRowDataBound="gvG_RowDataBound" PageSize="10" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast"
                                            OnPageIndexChanging="gvG_PageIndexChanging" AllowPaging="true" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="returnid" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="PurchasePayID" HeaderText="帐期流水" ItemStyle-HorizontalAlign="Center"
                                                    Visible="false" />
                                                <asp:BoundField DataField="PRID" HeaderText="pr流水" ItemStyle-HorizontalAlign="Center"
                                                    Visible="false" />
                                                <asp:BoundField DataField="ProjectID" HeaderText="项目流水" ItemStyle-HorizontalAlign="Center"
                                                    Visible="false" />
                                                <asp:TemplateField ItemStyle-Width="5%" HeaderText="PR号" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPR"></asp:Label>
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
                                                <asp:TemplateField HeaderText="打印预览" ItemStyle-Width="5%">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylPrint" runat="server" ImageUrl="/images/Icon_Output.gif" ToolTip="打印预览"
                                                            Width="4%"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="冲销" ItemStyle-Width="5%">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylDePayment" runat="server" ImageUrl="/images/edit.gif" ToolTip="冲销"
                                                            Width="4%"></asp:HyperLink>
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
