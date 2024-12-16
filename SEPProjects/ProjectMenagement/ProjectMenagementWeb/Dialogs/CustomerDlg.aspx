<%@ Page Language="C#" AutoEventWireup="true" Inherits="Dialogs_CustomerDlg" MasterPageFile="~/MasterPage.master"
    Title="客户选择" Codebehind="CustomerDlg.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
<script type="text/javascript">
    function SelectCustomer(btn) {
        return String.format("<input type='button' id='btnSelect' value='选择'/>");
    }
    function renderEdit(value, p, record) {
        return String.format("<a href='aspx?FSID={0}'><img height='20px' width='20px'src='images/edit.gif' border='0px;'</a>", value);
    }
</script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                客户查询
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                关键字：
                <asp:TextBox ID="txtCode" runat="server" />
                <asp:Button ID="btnOK" runat="server" CssClass="widebuttons" Text=" 检索 " OnClick="btnOK_Click" />&nbsp;
                <asp:Button ID="btnClean" runat="server" CssClass="widebuttons" Text=" 重新搜索 " OnClick="btnClean_Click" />
                  <asp:Button ID="btnCreate" runat="server" CssClass="widebuttons" Text=" 新建 " OnClick="btnCreate_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:100%">
                <asp:GridView ID="gvCustomer" runat="server" AutoGenerateColumns="False" DataKeyNames="CustomerID"
                    OnRowCommand="gvCustomer_RowCommand" OnPageIndexChanging="gvCustomer_PageIndexChanging"
                    OnRowDataBound="gvCustomer_RowDataBound" PageSize="10" EmptyDataText="暂时没有客户信息" PagerSettings-Mode="NumericFirstLast"
                    AllowPaging="true" Width="100%" >
                    <Columns>
                        <asp:ButtonField Text="选择" CommandName="Add" ButtonType="button" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%"  />
                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NameCN1" HeaderText="客户名称1" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="NameCN2" HeaderText="客户名称2" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="ShortEN" HeaderText="客户简称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="NameEN1" HeaderText="英文名称1" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25%"/>
                        <asp:BoundField DataField="NameEN2" HeaderText="英文名称2" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%"/>
                    </Columns>
                    <PagerStyle ForeColor="SteelBlue" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>