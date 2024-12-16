<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Dialogs_BranchDlg" Title="分公司选择" Codebehind="BranchDlg.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function SelectCustomer(btn) {
            return String.format("<input type='button' id='btnSelect' value='选择'/>");
        }
      
    </script>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            分公司查询
        </td>
    </tr>
            <tr>
                <td colspan="4" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                    padding-top: 4px;">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:GridView ID="gvBranch" runat="server" AutoGenerateColumns="False" DataKeyNames="BranchID"
                                    OnRowCommand="gvBranch_RowCommand" OnPageIndexChanging="gvBranch_PageIndexChanging"
                                    OnRowDataBound="gvBranch_RowDataBound" PageSize="10" EmptyDataText="暂时没有公司信息" PagerSettings-Mode="NumericFirstLast"
                                    AllowPaging="true" Width="100%">
                                    <Columns>
                                        <asp:ButtonField Text="选择" CommandName="Add" ButtonType="button" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="BranchID" HeaderText="BranchID" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="BranchCode" HeaderText="公司代码" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="BranchName" HeaderText="公司名称" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="40%" />
                                        <asp:BoundField DataField="Des" HeaderText="描述" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="30%" />
                                    </Columns>
                                    <PagerStyle  Font-Size="X-Large" ForeColor="SteelBlue"/>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
    </table>
</asp:Content>