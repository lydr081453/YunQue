<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketCheckList.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="FinanceWeb.Ticket.TicketCheckList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

    </script>


    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">关键字:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtKey" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">出票日:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor:pointer;" runat="server"
                    onclick="setDate(this);" />--
                <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor:pointer;" runat="server"
                    onclick="setDate(this);" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;
            </td>
        </tr>
    </table>

    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound" AllowPaging="false" EmptyDataText="暂时没有相关记录" Width="100%">
        <Columns>
            <asp:BoundField DataField="ReturnCode" HeaderText="申请单号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="RequestEmployeeName" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="Boarder" HeaderText="登机人" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="GoAirNo" HeaderText="航班号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
              <asp:BoundField DataField="ExpenseMoney" HeaderText="机票价格" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:C}"
                ItemStyle-Width="10%" />
              <asp:BoundField DataField="ExpenseDesc" HeaderText="费用描述" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
             <asp:BoundField DataField="ExpenseDate" HeaderText="出发日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="TicketSource" HeaderText="出发地" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="TicketDestination" HeaderText="目的地" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
        </Columns>
    </asp:GridView>

</asp:Content>
