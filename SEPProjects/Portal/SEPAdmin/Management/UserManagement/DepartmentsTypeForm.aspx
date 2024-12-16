<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="DepartmentsTypeForm.aspx.cs" Inherits="SEPAdmin.UserManagement.DepartmentsTypeForm" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <table>
                <tr>
                    <td><li><a href="DepartmentsTypeBrowse.aspx" style="color:Black">返回浏览</a></li></td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td class="oddrow">
                                    编号：
                                </td>
                                <td class="oddrow-l">
                                    <asp:Label ID="lblID" Text="自动生成" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">
                                    部门类别名称：
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">
                                    辅助工作人员/部门ID：
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtDes" runat="server" />
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="oddrow">
                                    是销售部门：
                                </td>
                                <td class="oddrow-l">
                                    <asp:CheckBox ID="chkSales" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">
                                    是分公司：
                                </td>
                                <td class="oddrow-l">
                                    <asp:CheckBox ID="chkSubCompany" runat="server" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>