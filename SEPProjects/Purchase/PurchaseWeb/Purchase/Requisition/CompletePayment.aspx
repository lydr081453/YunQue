<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" Inherits="Purchase_Requisition_CompletePayment"
    CodeBehind="CompletePayment.aspx.cs" %>
<%@ Import Namespace="ESP.Purchase.Common"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <script type="text/javascript">

        function openPaymentPreview(ppid) {
            var win = window.open("Print/PaymantPrint.aspx?<%=ESP.Finance.Utility.RequestName.ReturnID %>=" + ppid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
            return false;
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                流水号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtGeneralId" runat="server" MaxLength="200" Width="200px" />&nbsp;
            </td>
            <td class="oddrow" style="width: 15%">
                项目号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtPrno" runat="server" MaxLength="200" Width="200px" />&nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow">供应商类型:</td>
            <td class="oddrow-l"><asp:DropDownList ID="ddlType" runat="server" /></td>
            <td class="oddrow">初审人:</td>
            <td class="oddrow-l" colspan="3"><asp:TextBox ID="txtAudit" runat="server" Width="200px" MaxLength="300" /></td>
        </tr>
        <tr>
            <td class="oddrow">PN号:</td>
            <td class="oddrow-l" colspan="3"><asp:TextBox ID="txtPN" runat="server" Width="200px" MaxLength="20" /></td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" id="tabTop" runat="server">
        <tr>
            <td align="right" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="50%">
                <asp:Panel ID="PageTop" runat="server">
                    <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                    <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                    <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                    <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                </asp:Panel>
            </td>
            <td align="right" class="recordTd">
                记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                    runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSupplier_RowDataBound"
        DataKeyNames="id" PageSize="20" AllowPaging="True" Width="100%" OnRowCommand="gvSupplier_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# int.Parse(Eval("Gid").ToString()).ToString("0000000") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请单号" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href='<%# "OrderDetailTab.aspx?" + RequestName.GeneralID + "=" + Eval("generalid").ToString() + "&" + RequestName.BackUrl + "=CompletePayment.aspx" %>'><%# Eval("prno") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PN号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                <ItemTemplate>
                    <asp:Literal ID="litPNNO" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("requestor").ToString())) %>');">
                        <%# Eval("requestorname") %></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="创建时间" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# Eval("requisition_committime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString()%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请金额" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <%# GetFormatPrice(decimal.Parse(Eval("inceptPrice").ToString()))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="实际支付金额" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <%# GetFormatPrice(decimal.Parse(Eval("finallyPaymentPrice") == DBNull.Value ? "0" : Eval("finallyPaymentPrice").ToString()))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="实际支付时间" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <%# DateTime.Parse(Eval("finallyPaymentDate").ToString()) == DateTime.Parse(ESP.Purchase.Common.State.datetime_minvalue) ? "" : DateTime.Parse(Eval("finallyPaymentDate").ToString()).ToString()%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="财务审核人" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <span class="userLabel" onclick="ShowMsg('<%# (Eval("finallyPaymentUserId") == DBNull.Value || Eval("finallyPaymentUserId").ToString() == "") ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("finallyPaymentUserId").ToString())) %>');">
                        <%# Eval("finallyPaymentUserName")%></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Literal ID="litStatus" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="打印" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                <ItemTemplate>
                    <a href="" onclick="return openPaymentPreview('<%# Eval("returnid") %>');">
                        <img src="/images/pri_pn.gif" border="0px;" title="打印"></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerSettings Visible="false" />
    </asp:GridView>
    <table width="100%" id="tabBottom" runat="server">
        <tr>
            <td width="50%">
                <asp:Panel ID="PageBottom" runat="server">
                    <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                    <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                    <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                    <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                </asp:Panel>
            </td>
            <td align="right" class="recordTd">
                记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
