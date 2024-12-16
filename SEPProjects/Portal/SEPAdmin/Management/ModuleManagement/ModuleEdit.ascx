<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModuleEdit.ascx.cs"
    Inherits="SEPAdmin.Management.ModuleManagement.ModuleEdit" %>
<%@ Register Assembly="SEPAdmin" Namespace="SEPAdmin.Controls" TagPrefix="cc1" %>
<table style="width:100%">
    <tr>
        <td class="oddrow">
            模块ID
        </td>
        <td class="oddrow-l">
            <asp:TextBox ReadOnly="true" runat="server" ID="txtModuleID" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            模块名称：
        </td>
        <td class="oddrow-l">
            <asp:TextBox runat="server" ID="txtModuleName" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            描述：
        </td>
        <td class="oddrow-l">
            <asp:TextBox runat="server" ID="txtDescription" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            排序：
        </td>
        <td class="oddrow-l">
            <asp:TextBox runat="server" ID="txtOrdinal" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            节点类型
        </td>
        <td class="oddrow-l">
            <asp:TextBox ReadOnly="true" runat="server" ID="txtModuleType" />
        </td>
    </tr>
</table>
<hr />
<asp:LinkButton runat="server" ID="btnSave" Text="保存" OnClick="btnSave_Click" CssClass="widebuttons" />
<asp:LinkButton runat="server" ID="btnAddFolder" Text="添加子文件夹" OnClick="btnAddFolder_Click" CssClass="widebuttons" />
<asp:LinkButton runat="server" ID="btnAddModule" Text="添加功能" OnClick="btnAddModule_Click" CssClass="widebuttons" />
<asp:LinkButton runat="server" ID="btnDelete" Text="删除" OnClick="btnDelete_Click" CssClass="widebuttons"
    OnClientClick="if(!confirm('确定要删除吗？'))return false;" />
<hr />
<cc1:WebPageEditGrid ID="WebPageEditGrid1" runat="server" AddButtonText="添加" GridStyle-CellPadding="0"
    GridStyle-CellSpacing="0" DeleteButtonImage="/images/disable.gif" 
    DefaultPageHeaderText="默认页" PathHeaderText="页面路径" NoDefaultPageText="无默认页（不在导航中显示当前模块）">
    <HeaderStyle CssClass="oddrow" HorizontalAlign="Center" Height="25px" Font-Names="Verdana" Font-Size="9pt" />
    <SelectedItemStyle Font-Bold="true" CssClass="sep_grid_cell" Font-Names="Verdana" Font-Size="9pt" />
    <ItemStyle CssClass="sep_grid_cell" HorizontalAlign="Left" Font-Names="Verdana" Font-Size="9pt" />
    <PathBoxStyle Font-Names="Verdana" Font-Size="9pt" />
    <LabelStyle Font-Names="Verdana" Font-Size="9pt" />
    <ButtonStyle CssClass="widebuttons"  />
</cc1:WebPageEditGrid>
<hr />
<cc1:PermissionDefinitionEditGrid ID="PermissionDefinitionEditGrid1" runat="server"
    GridStyle-CellPadding="0" GridStyle-CellSpacing="0" AddButtonText="添加" DeleteButtonImage="/images/disable.gif"
    DescriptionHeaderText="权限名称" NameHeaderText="标识">
    <HeaderStyle  HorizontalAlign="Center" Height="25px" Font-Names="Verdana" Font-Size="9pt" CssClass="oddrow" />
    <ItemStyle CssClass="sep_grid_cell" HorizontalAlign="Left" Font-Names="Verdana" Font-Size="9pt" />
    <NameBoxStyle Font-Names="Verdana" Font-Size="9pt" />
    <LabelStyle Font-Names="Verdana" Font-Size="9pt" />
    <ButtonStyle CssClass="widebuttons"  />
</cc1:PermissionDefinitionEditGrid>
