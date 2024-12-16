<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceBatchAuditView.aspx.cs"
    Inherits="FinanceWeb.ExpenseAccount.FinanceBatchAuditView" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">


    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                批次信息
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                批次流水:
            </td>
            <td style="width: 35%">
                <asp:Label runat="server" ID="lblBatchId"></asp:Label>
            </td>
            <td style="width: 15%">
                批次号:
            </td>
            <td style="width: 35%">
                <asp:Label runat="server" ID="lblPurchaseBatchCode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                银行凭证号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtBatchCode" runat="server" />&nbsp;&nbsp;&nbsp;
            </td>
            <td class="oddrow" style="width: 15%">
                总金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTotal"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                审批日志:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblLog"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="oddrow-l" colspan="4">
               <asp:Button runat="server" ID="btnExport" Text=" 导出财务凭证 " OnClick="btnExport_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                申请单明细
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                    OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" EmptyDataText="暂时没有相关记录"
                    AllowPaging="false" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ReturnCode" HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ReturnContent" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="费用所属组" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="预计报销金额" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPreFee" Text='<%# Eval("PreFee") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RequestEmployeeName" HeaderText="申请人" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CommitDate" HeaderText="提交日期" ItemStyle-HorizontalAlign="Center"
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="LastAuditPassTime" HeaderText="业务审批通过日期" ItemStyle-HorizontalAlign="Center"
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="ReturnTypeName" HeaderText="单据类型" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="打印预览">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink ID="hylPrint" runat="server" ImageUrl="/images/Icon_Output.gif" ToolTip="打印预览"
                                    Width="4%"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
