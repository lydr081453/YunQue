<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.Master"
    CodeBehind="RoleForm.aspx.cs" Inherits="SEPAdmin.ModelManagement.RoleForm" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <table>
                <tr>
                    <td><a href="RoleBrowse.aspx">返回浏览</a></td>
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
                                    名称：
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">
                                    类别：
                                </td>
                                <td class="oddrow-l">
                                    <asp:DropDownList ID="ddlType" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%--<tr>
                                <td>
                                    创建人：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCreateUser" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    创建时间：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCreateDate" runat="server" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="oddrow">
                                    备注：
                                </td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtDes" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" CssClass="widebuttons" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
