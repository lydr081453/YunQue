<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferAuditList.aspx.cs" Inherits="SEPAdmin.HR.Transfer.TransferAuditList" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" class="tableForm">
        <tr>
            <td class="heading" colspan="4">检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">关键字:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>&nbsp;
            </td>
            <td class="oddrow" colspan="2">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click"></asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvTransfer" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvTransfer_RowDataBound" OnRowCommand="gvTransfer_RowCommand"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TransName" HeaderText="姓名" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="TransCode" HeaderText="编号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="OldGroupName" HeaderText="转出部门" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NewGroupName" HeaderText="转入部门" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NewPositionName" HeaderText="新职务" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="TransInDate" HeaderText="转入日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Creater" HeaderText="创建人" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CreateDate" HeaderText="创建日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"
                            ItemStyle-Width="10%" />
                        <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatus"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a href="TransferView.aspx?id=<%#Eval("Id").ToString() %>" target="_blank"><img src="/images/dc.gif" alt="查看" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="审批" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="hlAudit" ImageUrl="/images/audit.gif"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
