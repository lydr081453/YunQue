<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="DeadLineList.aspx.cs" Inherits="FinanceWeb.DeadLine.DeadLineList" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <li>
                                <asp:LinkButton ID="lbNewDeadLine" runat="server" Text="新建结账日" OnClick="lbNewDeadLine_Click" /></li>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                            padding-top: 4px;">
                            <table width="100%">
                                <tr>
                                    <td class="heading" colspan="4">
                                       结账日列表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="DeadLineID"
                                            OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" PageSize="10"
                                            EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvG_PageIndexChanging"
                                            AllowPaging="true" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="DeadLineID" HeaderText="DeadLineID" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="DeadLine" HeaderText="结账日1" ItemStyle-HorizontalAlign="right" />
                                                <asp:BoundField DataField="DeadLine2" HeaderText="结账日2" ItemStyle-HorizontalAlign="right" />
                                                <asp:BoundField DataField="ProjectDeadLine" HeaderText="项目结账日" ItemStyle-HorizontalAlign="right" />
                                                <asp:BoundField DataField="SalaryDate" HeaderText="工资发放日" ItemStyle-HorizontalAlign="right" />
                                                <asp:BoundField DataField="CreateUserEmpName" HeaderText="创建人" ItemStyle-HorizontalAlign="center" />
                                               </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;
            </td>
        </tr>
    </table>
                
</asp:Content>
