<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRolesForm.aspx.cs" Inherits="SEPAdmin.Management.UserManagement.UserRolesForm" MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
<table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                用户角色信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                用户ID:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="labUserID" runat="server" />
            </td>
            <td class="oddrow" style="width: 20%">
                用户名:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label runat="server" ID="labUserName" />
            </td>
        </tr>
</tabel>
<table width="100%" class="tableForm">
<tr>
            <td class="heading">
                角色信息
            </td>
        </tr>
<tr>
<td>
    <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false">
            <Columns>                           
                <asp:BoundField HeaderText="角色名称" DataField="rolename" />                     
            </Columns>
    </asp:GridView>
</td>
</tr>
</table>
<table width="100%" class="tableForm">      
        <tr>
            <td colspan="4" class="oddrow-l">
                <input type="button" runat="server" value=" 返回 " class="widebuttons" id="btnBack" onserverclick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
