<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  CodeBehind="AssetList.aspx.cs" Inherits="FinanceWeb.ITAsset.AssetList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <li><a href="AssetEdit.aspx">添加固定资产</a></li>
    <br /><br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">关键字:
            </td>
            <td class="oddrow-l" style="width: 30%"><asp:TextBox ID="txtKey" runat="server" />
            </td>
             <td class="oddrow" style="width: 20%">资产状态:
                 <asp:DropDownList runat="server" ID="ddlStatus">
                     <asp:ListItem Value="-1" Text="全部" Selected="True"></asp:ListItem>
                     <asp:ListItem Value="1" Text="在库"></asp:ListItem>
                     <asp:ListItem Value="2" Text="领用待确认"></asp:ListItem>
                     <asp:ListItem Value="3" Text="已领用"></asp:ListItem>
                     <asp:ListItem Value="4" Text="报废待确认"></asp:ListItem>
                     <asp:ListItem Value="5" Text="已报废"></asp:ListItem>
                     <asp:ListItem Value="6" Text="报损"></asp:ListItem>
                 </asp:DropDownList>
            </td>
        </tr>
        <tr>
             <td class="oddrow-l" colspan="2" align="center">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="gvList_RowCommand"
        PageSize="100" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast" OnRowDataBound="gvList_RowDataBound"
        OnPageIndexChanging="gvList_PageIndexChanging" PagerStyle-ForeColor="Black" AllowPaging="true" Width="100%">
        <Columns>
            <asp:BoundField HeaderText="资产名称" DataField="AssetName" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
            <asp:BoundField HeaderText="资产编号" DataField="SerialCode" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
             <asp:BoundField HeaderText="资产分类" DataField="CategoryName" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
            <asp:BoundField HeaderText="品牌" DataField="Brand" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
            <asp:BoundField HeaderText="型号" DataField="Model" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
            <asp:BoundField HeaderText="价格" DataField="Price" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"/>
            <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                <ItemTemplate>
                   <%# ESP.Finance.BusinessLogic.ITAssetsManager.GetStatus(int.Parse(Eval("Status").ToString()))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemTemplate>
                    <a href='AssetView.aspx?op=1&assetId=<%# Eval("Id")%>'>
                        <img src="../images/dc.gif" border="0px;" title="查看"></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <a href='AssetEdit.aspx?assetId=<%# Eval("Id")%>'>
                        <img src="../images/edit.gif" border="0px;" title="编辑"></a>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="领用" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <a href='AssetView.aspx?op=2&assetId=<%# Eval("Id")%>'>
                        <img src="../images/edit.gif" border="0px;" title="领用"></a>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="报废" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <a href='AssetView.aspx?op=3&assetId=<%# Eval("Id")%>'>
                        <img src="../images/edit.gif" border="0px;" title="报废"></a>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="还回" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <a href='AssetView.aspx?op=6&assetId=<%# Eval("Id")%>'>
                        <img src="../images/edit.gif" border="0px;" title="还回"></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="损坏" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Id") %>'
                        CommandName="Del" Text="<img src='../../images/disable.gif' title='损坏' border='0'>"
                        OnClientClick="return confirm('确定将该资产转入损坏吗？');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
