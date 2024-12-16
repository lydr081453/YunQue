<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeadAccountAuditList.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.Join.HeadAccountAuditList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" class="tableForm">
        <tr>
            <td class="heading" colspan="4">检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">员工工号:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>&nbsp;
            </td>
            <td class="oddrow" style="width: 10%">员工姓名:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click"></asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvHeadAccount" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvHeadAccount_RowDataBound"
                    Width="100%">
                    <Columns>
                         <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="部门" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblGroup"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Position" HeaderText="职务" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="LevelName" HeaderText="职级" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="工资级别" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSalary"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreateDate" HeaderText="创建日期" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="120" />
                        <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# ESP.HumanResource.Common.Status.HeadAccountStatus_Names[int.Parse(Eval("Status").ToString())]%><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a href="HeadCountView.aspx?haid=<%#Eval("Id").ToString() %>" target="_blank">查看</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="审批" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a href="HeadAccountAudit.aspx?haid=<%#Eval("Id").ToString() %>">审批</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
