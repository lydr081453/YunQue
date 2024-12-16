<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="RefundView.aspx.cs" Inherits="FinanceWeb.Refund.RefundView" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">采购申请息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">PR单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
               <asp:Label ID="lblPRNO" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">项目号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">申请人姓名:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblApplicant" runat="server" CssClass="userLabel"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">采购金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblTotalprice" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">供应商名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblSupplierName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">开户行名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblBank"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">开户行帐号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblAccount"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">付款信息
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvG_RowDataBound"
                    DataKeyNames="ReturnID" EmptyDataText="暂时没有相关记录" AllowPaging="false"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ReturnCode" HeaderText="付款单号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="ReturnFactDate" HeaderText="付款日期" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="ReturnContent" HeaderText="付款内容" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />
                        <asp:BoundField DataField="SupplierName" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />
                        <asp:TemplateField HeaderText="付款金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAmounts"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="付款状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>

        <tr>
            <td class="heading" colspan="4">退款申请
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">退款金额:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                <asp:Label runat="server" ID="lblRefund"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">退款日期:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                 <asp:Label runat="server" ID="lblRefundDate"></asp:Label>
            </td>
        </tr>
           <tr>
            <td class="oddrow" style="width: 15%">成本项:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
               <asp:Label runat="server" ID="lblCost"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">退款说明:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblDesc"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">审批历史:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblLog" runat="server"></asp:Label>
            </td>
        </tr>
    </table>

    </asp:Content>
